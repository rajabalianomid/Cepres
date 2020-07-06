using Cepres.Common;
using Cepres.Common.Data;
using Cepres.Common.Domain;
using Cepres.Domain;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Cepres.Data.Repositories
{
    public interface IPatientRepository : IRepository<Patient>
    {
        public List<PatientList> GetAllPatient(Pagination pagination);
        List<PatientReport> Report();
        List<Patient> GetSimilar(int id);
        int Count();
    }
    public class PatientRepository : StaticSqlRepository<Patient>, IPatientRepository
    {
        private readonly IDbContext _context;
        public PatientRepository(IDbContext context) : base(context)
        {
            _context = context;
        }
        public List<PatientList> GetAllPatient(Pagination pagination)
        {
            var parameters = CreatedSqlParameter(pagination);
            return _context.ExecuteStoredProc<PatientList>("sp_GetAll_Patient", parameters).ToList();
        }
        public List<PatientReport> Report()
        {
            return _context.ExecuteStoredProc<PatientReport>("sp_Report_Patient").ToList();
        }
        public List<Patient> GetSimilar(int id)
        {
            var parameters = new List<SqlParameter> { new SqlParameter("@PatientId", id) };
            return _context.ExecuteStoredProc<Patient>("sp_Get_Similar_Patient", parameters).ToList();
        }
        public int Count()
        {
            return _context.Set<Patient>().Count();
        }
    }
}
