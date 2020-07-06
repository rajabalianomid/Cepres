using Cepres.Common;
using Cepres.Common.Data;
using Cepres.Common.Domain;
using Cepres.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cepres.Data.Repositories
{
    public interface IRecordRepository : IRepository<Record>
    {
        public List<RecordList> GetAll(Pagination pagination);
        int Count();
    }
    public class RecordRepository : StaticSqlRepository<Record>, IRecordRepository
    {
        private readonly IDbContext _context;
        public RecordRepository(IDbContext context) : base(context)
        {
            _context = context;
        }
        public List<RecordList> GetAll(Pagination pagination)
        {
            var parameters = CreatedSqlParameter(pagination);
            return _context.ExecuteStoredProc<RecordList>("sp_GetAll_Record", parameters).ToList();
        }
        public int Count()
        {
            return _context.Set<Patient>().Count();
        }
    }
}
