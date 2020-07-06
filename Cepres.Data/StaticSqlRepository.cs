using Cepres.Common;
using Cepres.Common.Data;
using Cepres.Common.Domain;
using Cepres.Domain;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Cepres.Data
{
    public partial class StaticSqlRepository<TEntity> : EfRepository<TEntity> where TEntity : BaseEntity
    {
        #region Fields

        private readonly IDbContext _context;

        private DbSet<TEntity> _entities;

        #endregion

        #region Ctor

        public StaticSqlRepository(IDbContext context) : base(context)
        {
            _context = context;
        }

        #endregion

        public override void Delete(TEntity entity)
        {
            _context.ExecuteStoredProc($"sp_{nameof(Delete)}_{entity.GetType().Name}", new List<SqlParameter> { new SqlParameter("@Id", entity.Id) });
        }

        public override TEntity GetById(TEntity entity)
        {
            return _context.EntityFromSql<TEntity>($"sp_{nameof(GetById)}_{entity.GetType().Name} @id", entity.Id).FirstOrDefault();
        }

        public override TEntity Insert(TEntity entity)
        {
            var parameters = CreatedSqlParameter(entity);
            var result = _context.ExecuteStoredProc<TEntity>($"sp_{nameof(Insert)}_{entity.GetType().Name}", parameters);
            return result.FirstOrDefault();
        }

        public override TEntity Update(TEntity entity)
        {
            var excpetparameters = new List<string>
            {
               nameof( entity.CreateUtc)
            };
            var parameters = CreatedSqlParameter(entity).Where(w => !excpetparameters.Any(a => w.ParameterName.EndsWith(a))).ToList(); ;
            var result = _context.ExecuteStoredProc<TEntity>($"sp_{nameof(Update)}_{entity.GetType().Name}", parameters);
            return result.FirstOrDefault();
        }

        protected List<SqlParameter> CreatedSqlParameter<TSql>(TSql entity)
        {
            PropertyInfo[] propertyInfos;
            propertyInfos = entity.GetType().GetProperties().Where(w => !w.GetAccessors().Any(a => a.IsVirtual)).ToArray();
            Array.Sort(propertyInfos, delegate (PropertyInfo propertyInfo1, PropertyInfo propertyInfo2) { return propertyInfo1.Name.CompareTo(propertyInfo2.Name); });
            return propertyInfos.Select(s => new SqlParameter("@" + s.Name, s.GetValue(entity, null))).ToList();
        }

    }
}
