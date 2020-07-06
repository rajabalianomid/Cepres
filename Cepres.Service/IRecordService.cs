using Cepres.Common.Domain;
using Cepres.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cepres.Service
{
    public interface IRecordService
    {
        Record Add(Record patient);
        Record Update(Record patient);
        Record Get(int id);
        List<RecordList> GetAll(Pagination pagination);
        int Count();
        void Remove(int id);
    }
}
