﻿using Cepres.Common.Data;
using Cepres.Common.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace Cepres.Data
{
    public static class DbContextExtensions
    {
        #region Fields

        private static string databaseName;
        private static readonly ConcurrentDictionary<string, string> tableNames = new ConcurrentDictionary<string, string>();
        private static readonly ConcurrentDictionary<string, IEnumerable<(string, int?)>> columnsMaxLength = new ConcurrentDictionary<string, IEnumerable<(string, int?)>>();
        private static readonly ConcurrentDictionary<string, IEnumerable<(string, decimal?)>> decimalColumnsMaxValue = new ConcurrentDictionary<string, IEnumerable<(string, decimal?)>>();

        #endregion

        #region Utilities

        /// <summary>
        /// Loads a copy of the entity using the passed function
        /// </summary>
        /// <typeparam name="TEntity">Entity type</typeparam>
        /// <param name="context">Database context</param>
        /// <param name="entity">Entity</param>
        /// <param name="getValuesFunction">Function to get the values of the tracked entity</param>
        /// <returns>Copy of the passed entity</returns>
        private static TEntity LoadEntityCopy<TEntity>(IDbContext context, TEntity entity, Func<EntityEntry<TEntity>, PropertyValues> getValuesFunction)
            where TEntity : BaseEntity
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            //try to get the EF database context
            if (!(context is DbContext dbContext))
            {
                throw new InvalidOperationException("Context does not support operation");
            }

            //try to get the entity tracking object
            EntityEntry<TEntity> entityEntry = dbContext.ChangeTracker.Entries<TEntity>().FirstOrDefault(entry => entry.Entity == entity);
            if (entityEntry == null)
            {
                return null;
            }

            //get a copy of the entity
            TEntity entityCopy = getValuesFunction(entityEntry)?.ToObject() as TEntity;

            return entityCopy;
        }

        /// <summary>
        /// Get SQL commands from the script
        /// </summary>
        /// <param name="sql">SQL script</param>
        /// <returns>List of commands</returns>
        private static IList<string> GetCommandsFromScript(string sql)
        {
            List<string> commands = new List<string>();

            //origin from the Microsoft.EntityFrameworkCore.Migrations.SqlServerMigrationsSqlGenerator.Generate method
            sql = Regex.Replace(sql, @"\\\r?\n", string.Empty);
            string[] batches = Regex.Split(sql, @"^\s*(GO[ \t]+[0-9]+|GO)(?:\s+|$)", RegexOptions.IgnoreCase | RegexOptions.Multiline);

            for (int i = 0; i < batches.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(batches[i]) || batches[i].StartsWith("GO", StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                int count = 1;
                if (i != batches.Length - 1 && batches[i + 1].StartsWith("GO", StringComparison.OrdinalIgnoreCase))
                {
                    Match match = Regex.Match(batches[i + 1], "([0-9]+)");
                    if (match.Success)
                    {
                        count = int.Parse(match.Value);
                    }
                }

                StringBuilder builder = new StringBuilder();
                for (int j = 0; j < count; j++)
                {
                    builder.Append(batches[i]);
                    if (i == batches.Length - 1)
                    {
                        builder.AppendLine();
                    }
                }

                commands.Add(builder.ToString());
            }

            return commands;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Loads the original copy of the entity
        /// </summary>
        /// <typeparam name="TEntity">Entity type</typeparam>
        /// <param name="context">Database context</param>
        /// <param name="entity">Entity</param>
        /// <returns>Copy of the passed entity</returns>
        public static TEntity LoadOriginalCopy<TEntity>(this IDbContext context, TEntity entity) where TEntity : BaseEntity
        {
            return LoadEntityCopy(context, entity, entityEntry => entityEntry.OriginalValues);
        }

        /// <summary>
        /// Loads the database copy of the entity
        /// </summary>
        /// <typeparam name="TEntity">Entity type</typeparam>
        /// <param name="context">Database context</param>
        /// <param name="entity">Entity</param>
        /// <returns>Copy of the passed entity</returns>
        public static TEntity LoadDatabaseCopy<TEntity>(this IDbContext context, TEntity entity) where TEntity : BaseEntity
        {
            return LoadEntityCopy(context, entity, entityEntry => entityEntry.GetDatabaseValues());
        }

        /// <summary>
        /// Drop a plugin table
        /// </summary>
        /// <param name="context">Database context</param>
        /// <param name="tableName">Table name</param>
        public static void DropPluginTable(this IDbContext context, string tableName)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (string.IsNullOrEmpty(tableName))
            {
                throw new ArgumentNullException(nameof(tableName));
            }

            //drop the table
            string dbScript = $"IF OBJECT_ID('{tableName}', 'U') IS NOT NULL DROP TABLE [{tableName}]";
            context.ExecuteSqlScript(dbScript);
            context.SaveChanges();
        }

        /// <summary>
        /// Get table name of entity
        /// </summary>
        /// <typeparam name="TEntity">Entity type</typeparam>
        /// <param name="context">Database context</param>
        /// <returns>Table name</returns>
        public static string GetTableName<TEntity>(this IDbContext context) where TEntity : BaseEntity
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            //try to get the EF database context
            if (!(context is DbContext dbContext))
            {
                throw new InvalidOperationException("Context does not support operation");
            }

            string entityTypeFullName = typeof(TEntity).FullName;
            if (!tableNames.ContainsKey(entityTypeFullName))
            {
                //get entity type
                Microsoft.EntityFrameworkCore.Metadata.IEntityType entityType = dbContext.Model.FindRuntimeEntityType(typeof(TEntity));

                //get the name of the table to which the entity type is mapped
                tableNames.TryAdd(entityTypeFullName, entityType.GetTableName());
            }

            tableNames.TryGetValue(entityTypeFullName, out string tableName);

            return tableName;
        }

        /// <summary>
        /// Gets the maximum lengths of data that is allowed for the entity properties
        /// </summary>
        /// <typeparam name="TEntity">Entity type</typeparam>
        /// <param name="context">Database context</param>
        /// <returns>Collection of name - max length pairs</returns>
        public static IEnumerable<(string Name, int? MaxLength)> GetColumnsMaxLength<TEntity>(this IDbContext context) where TEntity : BaseEntity
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            //try to get the EF database context
            if (!(context is DbContext dbContext))
            {
                throw new InvalidOperationException("Context does not support operation");
            }

            string entityTypeFullName = typeof(TEntity).FullName;
            if (!columnsMaxLength.ContainsKey(entityTypeFullName))
            {
                //get entity type
                Microsoft.EntityFrameworkCore.Metadata.IEntityType entityType = dbContext.Model.FindEntityType(typeof(TEntity));

                //get property name - max length pairs
                columnsMaxLength.TryAdd(entityTypeFullName,
                    entityType.GetProperties().Select(property => (property.Name, property.GetMaxLength())));
            }

            columnsMaxLength.TryGetValue(entityTypeFullName, out IEnumerable<(string, int?)> result);

            return result;
        }

        /// <summary>
        /// Get maximum decimal values
        /// </summary>
        /// <typeparam name="TEntity">Entity type</typeparam>
        /// <param name="context">Database context</param>
        /// <returns>Collection of name - max decimal value pairs</returns>
        public static IEnumerable<(string Name, decimal? MaxValue)> GetDecimalColumnsMaxValue<TEntity>(this IDbContext context)
            where TEntity : BaseEntity
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            //try to get the EF database context
            if (!(context is DbContext dbContext))
            {
                throw new InvalidOperationException("Context does not support operation");
            }

            string entityTypeFullName = typeof(TEntity).FullName;
            if (!decimalColumnsMaxValue.ContainsKey(entityTypeFullName))
            {
                //get entity type
                Microsoft.EntityFrameworkCore.Metadata.IEntityType entityType = dbContext.Model.FindEntityType(typeof(TEntity));

                //get entity decimal properties
                IEnumerable<Microsoft.EntityFrameworkCore.Metadata.IProperty> properties = entityType.GetProperties().Where(property => property.ClrType == typeof(decimal));

                //return property name - max decimal value pairs
                decimalColumnsMaxValue.TryAdd(entityTypeFullName, properties.Select(property =>
                {
                    RelationalTypeMappingInfo mapping = new RelationalTypeMappingInfo(property);
                    if (!mapping.Precision.HasValue || !mapping.Scale.HasValue)
                    {
                        return (property.Name, null);
                    }

                    return (property.Name, new decimal?((decimal)Math.Pow(10, mapping.Precision.Value - mapping.Scale.Value)));
                }));
            }

            decimalColumnsMaxValue.TryGetValue(entityTypeFullName, out IEnumerable<(string, decimal?)> result);

            return result;
        }

        /// <summary>
        /// Get database name
        /// </summary>
        /// <param name="context">Database context</param>
        /// <returns>Database name</returns>
        public static string DbName(this IDbContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            //try to get the EF database context
            if (!(context is DbContext dbContext))
            {
                throw new InvalidOperationException("Context does not support operation");
            }

            if (!string.IsNullOrEmpty(databaseName))
            {
                return databaseName;
            }

            //get database connection
            System.Data.Common.DbConnection dbConnection = dbContext.Database.GetDbConnection();

            //return the database name
            databaseName = dbConnection.Database;

            return databaseName;
        }

        /// <summary>
        /// Execute commands from the SQL script against the context database
        /// </summary>
        /// <param name="context">Database context</param>
        /// <param name="sql">SQL script</param>
        public static void ExecuteSqlScript(this IDbContext context, string sql)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            IList<string> sqlCommands = GetCommandsFromScript(sql);
            foreach (string command in sqlCommands)
            {
                context.ExecuteSqlCommand(command);
            }
        }

        /// <summary>
        /// Execute commands from a file with SQL script against the context database
        /// </summary>
        /// <param name="context">Database context</param>
        /// <param name="filePath">Path to the file</param>
        public static void ExecuteSqlScriptFromFile(this IDbContext context, string filePath)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (!File.Exists(filePath))
            {
                return;
            }

            context.ExecuteSqlScript(File.ReadAllText(filePath));
        }

        public static void ExecuteSeedFromJsonFile(this IDbContext context, string filePath)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (!File.Exists(filePath))
            {
                return;
            }

            //JsonSchema data = JsonConvert.DeserializeObject<JsonSchema>(File.ReadAllText(filePath));
            //context.Set<Folder>().AddRange(data.Folder);
            context.SaveChanges();
        }

        public static List<T> ToList<T>(this DataTable dt)
        {
            const BindingFlags flags = BindingFlags.Public | BindingFlags.Instance;
            List<string> columnNames = dt.Columns.Cast<DataColumn>()
                .Select(c => c.ColumnName)
                .ToList();
            PropertyInfo[] objectProperties = typeof(T).GetProperties(flags);
            List<T> targetList = dt.AsEnumerable().Select(dataRow =>
            {
                T instanceOfT = Activator.CreateInstance<T>();

                foreach (PropertyInfo properties in objectProperties.Where(properties => columnNames.Contains(properties.Name) && dataRow[properties.Name] != DBNull.Value))
                {
                    properties.SetValue(instanceOfT, dataRow[properties.Name], null);
                }
                return instanceOfT;
            }).ToList();

            return targetList;
        }

        #endregion
    }
}
