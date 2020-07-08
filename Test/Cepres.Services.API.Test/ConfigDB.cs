using Autofac.Core.Activators;
using Cepres.Data;
using Cepres.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cepres.Services.API.Test
{
    public class ConfigDB
    {
        public readonly static ConfigDB Instance = new ConfigDB();
        public DbContextOptions<Context> DbContextOptions { get; private set; }
        private ConfigDB()
        {
            DbContextOptionsBuilder<Context> builder = new DbContextOptionsBuilder<Context>();
            builder.UseInMemoryDatabase(databaseName: "CepresDb");
            DbContextOptions = builder.Options;
        }
        public void AddTestData()
        {
            using (Context context = new Context(DbContextOptions))
            {
                context.AddRange(GetAllData());
                context.SaveChanges();
            }
        }
        private List<Patient> GetAllData()
        {
            return new List<Patient>
            {
                new Patient{Id=1, Name="Omid",CreateUtc=DateTime.UtcNow,DateOfBirth=DateTime.UtcNow.AddYears(-32),Email="Omid@gmail.com",OfficialId=1},
                new Patient{Id=2, Name="Amir",CreateUtc=DateTime.UtcNow,DateOfBirth=DateTime.UtcNow.AddYears(-36),Email="Amir@gmail.com",OfficialId=2  },
                new Patient{Id=3, Name="Mahsa",CreateUtc=DateTime.UtcNow,DateOfBirth=DateTime.UtcNow.AddYears(-29),Email="Mahsa@gmail.com",OfficialId=3 },
                new Patient{Id=4, Name="Sepideh",CreateUtc=DateTime.UtcNow,DateOfBirth=DateTime.UtcNow.AddYears(-33),Email="Sepideh@gmail.com",OfficialId=4  },
            };
        }
    }
}
