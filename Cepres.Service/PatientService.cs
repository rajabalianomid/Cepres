using Cepres.Common;
using Cepres.Common.Domain;
using Cepres.Data.Repositories;
using Cepres.Domain;
using FileManager.Service.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace Cepres.Service
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _repositoryPatient;
        public PatientService(IPatientRepository repositoryPatient)
        {
            _repositoryPatient = repositoryPatient;
        }
        public Patient Add(Patient patient)
        {
            try
            {
                patient.CreateUtc = DateTime.UtcNow;
                return _repositoryPatient.Insert(patient);
            }
            catch (Exception)
            {
                throw new ServiceException("occour a problem, please try later!");
            }
        }
        public Patient Update(Patient patient)
        {
            try
            {
                return _repositoryPatient.Update(patient);
            }
            catch (Exception)
            {
                throw new ServiceException("occour a problem, please try later!");
            }
        }
        public List<Patient> GetByName(string name)
        {
            try
            {
                //SELECT * FROM Patient WHERE [Name] LIKE '{name}%'
                return _repositoryPatient.Table.Where(w => w.Name.StartsWith(name)).ToList();
            }
            catch (Exception)
            {
                throw new ServiceException("occour a problem, please try later!");
            }
        }
        public Patient Get(int id)
        {
            try
            {
                return _repositoryPatient.Table.FirstOrDefault(w => w.Id == id);
            }
            catch (Exception)
            {
                throw new ServiceException("occour a problem, please try later!");
            }
        }
        public List<PatientList> GetAll(Pagination pagination)
        {
            try
            {
                return _repositoryPatient.GetAllPatient(pagination);
            }
            catch (Exception)
            {
                throw new ServiceException("occour a problem, please try later!");
            }
        }
        public List<PatientReport> Report()
        {
            try
            {
                return _repositoryPatient.Report();
            }
            catch (Exception)
            {
                throw new ServiceException("occour a problem, please try later!");
            }
        }
        public List<Patient> GetSimilar(int id)
        {
            try
            {
                return _repositoryPatient.GetSimilar(id);
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
                return _repositoryPatient.Count();
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
                _repositoryPatient.Delete(new Patient { Id = id });
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
