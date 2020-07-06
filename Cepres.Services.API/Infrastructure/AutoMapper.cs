using AutoMapper;
using Cepres.Domain;
using Cepres.Services.API.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Cepres.Services.API.Infrastructure
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<Patient, PatientViewModel>().ForMember(f => f.DateOfBirth, m => m.MapFrom(f => f.DateOfBirth.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)));
            CreateMap<PatientViewModel, Patient>().ForMember(f => f.DateOfBirth, m => m.MapFrom(f => DateTime.Parse(f.DateOfBirth)));
            CreateMap<PatientMetaData, PatientMetaDataViewModel>();
            CreateMap<PatientMetaDataViewModel, PatientMetaData>();
            CreateMap<Patient, AutoCompleteViewModel>()
                .ForMember(f => f.Value, m => m.MapFrom(f => f.Id.ToString()))
                .ForMember(f => f.Label, m => m.MapFrom(f => f.Name));
            CreateMap<Record, RecordViewModel>()
                .ForMember(f => f.TimeOfEntry, m => m.MapFrom(f => f.TimeOfEntry.ToShortDateString()))
                .ForMember(f => f.Name, m => m.MapFrom(f => f.Patient.Name));
            CreateMap<RecordViewModel, Record>();
        }
    }
}
