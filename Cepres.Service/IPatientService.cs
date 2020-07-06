using Cepres.Common.Domain;
using Cepres.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cepres.Service
{
    public interface IPatientService
    {
        Patient Add(Patient patient);
        Patient Update(Patient patient);
        Patient Get(int id);
        List<Patient> GetByName(string name);
        List<PatientList> GetAll(Pagination pagination);
        List<PatientReport> Report();
        int Count();
        void Remove(int id);
        List<Patient> GetSimilar(int id);
    }
}
