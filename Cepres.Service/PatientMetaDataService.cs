using Cepres.Common;
using Cepres.Data.Repositories;
using Cepres.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cepres.Service
{
    public class PatientMetaDataService : IPatientMetaDataService
    {
        private readonly IPatientMetaDataRepository _repositoryPatientMetaData;
        public PatientMetaDataService(IPatientMetaDataRepository repositoryPatientMetaData)
        {
            _repositoryPatientMetaData = repositoryPatientMetaData;
        }
        public PatientMetaData Add(PatientMetaData patientMetaData)
        {
            patientMetaData.CreateUtc = DateTime.UtcNow;
            return _repositoryPatientMetaData.Insert(patientMetaData);
        }
        public List<PatientMetaData> GetAllByPatientId(int patientId)
        {
            return _repositoryPatientMetaData.GetAllByPatientId(patientId);
        }
        public List<General> Report()
        {
            return _repositoryPatientMetaData.Report();
        }
        public void Remove(int id)
        {
            _repositoryPatientMetaData.Delete(new PatientMetaData { Id = id });
        }
    }
}
