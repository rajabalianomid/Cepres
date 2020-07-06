using Cepres.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cepres.Service
{
    public interface IPatientMetaDataService
    {
        PatientMetaData Add(PatientMetaData patient);
        List<PatientMetaData> GetAllByPatientId(int patientId);
        List<General> Report();
        void Remove(int id);
    }
}
