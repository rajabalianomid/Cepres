using Cepres.Common;
using Cepres.Common.Data;
using Cepres.Common.Domain;
using Cepres.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cepres.Data.Repositories
{
    public interface IPatientMetaDataRepository : IRepository<PatientMetaData>
    {
        List<PatientMetaData> GetAllByPatientId(int patientId);
        List<General> Report();
    }
    public class PatientMetaDataRepository : StaticSqlRepository<PatientMetaData>, IPatientMetaDataRepository
    {
        #region Fields

        private readonly IDbContext _context;

        #endregion

        public PatientMetaDataRepository(IDbContext context) : base(context)
        {
            _context = context;
        }

        public List<PatientMetaData> GetAllByPatientId(int patientId)
        {
            var pStoreId = DataProviderHelper.GetInt32Parameter("StoreId", patientId);
            return _context.EntityFromSql<PatientMetaData>("sp_GetAll_Patient_ByPatientId", pStoreId).ToList();
        }

        public List<General> Report()
        {
            return _context.ExecuteStoredProc<General>("sp_Report_MetaData").ToList();
        }
    }
}
