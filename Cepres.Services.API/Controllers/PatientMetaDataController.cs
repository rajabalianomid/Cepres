using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Cepres.Common.Domain;
using Cepres.Domain;
using Cepres.Service;
using Cepres.Service.API.Models;
using Cepres.Services.API.Models;
using FileManager.Service.API;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Cepres.Services.API.Controllers
{
    [ApiController]
    [Authorize]
    public class PatientMetaDataController : ControllerBase
    {
        private readonly IPatientMetaDataService _patientMetaDataService;
        private readonly IMapper _mapper;
        public PatientMetaDataController(IPatientMetaDataService patientMetaDataService, IMapper mapper)
        {
            _patientMetaDataService = patientMetaDataService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("api/patientmetadata/gettall/{patientId}")]
        public CommonResponse GetAllByPatientId(int patientId)
        {
            CommonResponse<List<PatientMetaDataViewModel>> response = new CommonResponse<List<PatientMetaDataViewModel>>();
            try
            {
                var model = _patientMetaDataService.GetAllByPatientId(patientId).ConvertAll(_mapper.Map<PatientMetaDataViewModel>);
                response.Result = model;
            }
            catch (ServiceException ex)
            {
                response.StatusText = ex.Message;
            }
            return response;
        }

        [HttpGet]
        [Route("api/patientmetadata/report")]
        public CommonResponse Report()
        {
            CommonResponse<MetaDataReportViewModel> response = new CommonResponse<MetaDataReportViewModel>();
            response.Result = new MetaDataReportViewModel();
            try
            {
                var report = _patientMetaDataService.Report();
                response.Result.Average = report.Where(w => w.Key == "System_Generate_Avrage").Select(s => double.Parse(s.Value)).DefaultIfEmpty(0).FirstOrDefault();
                response.Result.Max = report.Where(w => w.Key == "System_Generate_MaxCount").Select(s => int.Parse(s.Value)).DefaultIfEmpty(0).FirstOrDefault();
                response.Result.MetaData = report.Where(w => !w.Key.StartsWith("System_Generate_")).Select(s => new General { Key = s.Key, Value = s.Value }).ToList();
            }
            catch (ServiceException ex)
            {
                response.StatusText = ex.Message;
            }
            return response;
        }

        [HttpPost]
        [Route("api/patientmetadata/add")]
        public CommonResponse Add(PatientMetaDataViewModel patientMetaDataView)
        {
            CommonResponse<PatientMetaDataViewModel> response = new CommonResponse<PatientMetaDataViewModel>();
            try
            {
                var model = _patientMetaDataService.Add(_mapper.Map<PatientMetaData>(patientMetaDataView));
                response.Result = _mapper.Map<PatientMetaDataViewModel>(model);
            }
            catch (ServiceException ex)
            {
                response.StatusText = ex.Message;
            }
            return response;
        }

        [HttpDelete]
        [Route("api/patientmetadata/remove/{id}")]
        public CommonResponse Remove(int id)
        {
            CommonResponse response = new CommonResponse();
            try
            {
                _patientMetaDataService.Remove(id);
            }
            catch (ServiceException ex)
            {
                response.StatusText = ex.Message;
            }
            return response;
        }
    }
}
