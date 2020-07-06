using Cepres.Common.Domain;
using Cepres.Data.Repositories;
using Cepres.Domain;
using FileManager.Service.API;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cepres.Service
{
    public class RecordService : IRecordService
    {
        private readonly IRecordRepository _repositoryRecord;
        public RecordService(IRecordRepository repositoryRecord)
        {
            _repositoryRecord = repositoryRecord;
        }
        public Record Add(Record record)
        {
            try
            {
                record.CreateUtc = DateTime.UtcNow;
                record.TimeOfEntry = DateTime.UtcNow;
                return _repositoryRecord.Insert(record);
            }
            catch (Exception ex)
            {
                throw new ServiceException("occour a problem, please try later!");
            }
        }
        public Record Update(Record record)
        {
            try
            {
                return _repositoryRecord.Update(record);
            }
            catch (Exception ex)
            {
                throw new ServiceException("occour a problem, please try later!");
            }
        }
        public Record Get(int id)
        {
            try
            {
                return _repositoryRecord.Table.FirstOrDefault(w => w.Id == id);
            }
            catch (Exception)
            {
                throw new ServiceException("occour a problem, please try later!");
            }
        }
        public List<RecordList> GetAll(Pagination pagination)
        {
            try
            {
                return _repositoryRecord.GetAll(pagination);
            }
            catch (Exception)
            {
                throw new ServiceException("occour a problem, please try later!");
            }
        }
        public int Count()
        {
            try
            {
                return _repositoryRecord.Count();
            }
            catch (Exception)
            {
                throw new ServiceException("occour a problem, please try later!");
            }
        }
        public void Remove(int id)
        {
            try
            {
                _repositoryRecord.Delete(new Record { Id = id });
            }
            catch (Exception ex)
            {
                string message = "occour a problem, please try later!";
                if (ex.InnerException.Message.Contains("UK"))
                    message = "you need remove meta data at first!";
                throw new ServiceException(message);
            }
        }
    }
}
