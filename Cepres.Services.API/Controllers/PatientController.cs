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
    public class PatientController : ControllerBase
    {
        private readonly IPatientService _patientService;
        private readonly IMapper _mapper;
        public PatientController(IPatientService patientService, IMapper mapper)
        {
            _patientService = patientService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("api/patient/get/{id}")]
        public CommonResponse Get(int id)
        {
            CommonResponse<PatientViewModel> response = new CommonResponse<PatientViewModel>();
            try
            {
                response.Result = _mapper.Map<PatientViewModel>(_patientService.Get(id));
            }
            catch (ServiceException ex)
            {
                response.StatusText = ex.Message;
            }
            return response;
        }

        [HttpGet]
        [Route("api/patient/getbyname/{name}")]
        public CommonResponse GetByName(string name)
        {
            CommonResponse<List<AutoCompleteViewModel>> response = new CommonResponse<List<AutoCompleteViewModel>>();
            try
            {
                response.Result = _mapper.Map<List<AutoCompleteViewModel>>(_patientService.GetByName(name));
            }
            catch (ServiceException ex)
            {
                response.StatusText = ex.Message;
            }
            return response;
        }

        [HttpGet]
        [Route("api/patient/getsimilar/{id}")]
        public CommonResponse GetSimilar(int id)
        {
            CommonResponse<List<PatientViewModel>> response = new CommonResponse<List<PatientViewModel>>();
            try
            {
                response.Result = _mapper.Map<List<PatientViewModel>>(_patientService.GetSimilar(id));
            }
            catch (ServiceException ex)
            {
                response.StatusText = ex.Message;
            }
            return response;
        }

        [HttpGet]
        [Route("api/patient/report")]
        public CommonResponse Report()
        {
            CommonResponse<List<PatientReport>> response = new CommonResponse<List<PatientReport>>();
            try
            {
                response.Result = _patientService.Report();
            }
            catch (ServiceException ex)
            {
                response.StatusText = ex.Message;
            }
            return response;
        }

        [HttpPost]
        [Route("api/patient/getall")]
        public CommonResponse GetAll(Pagination pagination)
        {
            CommonResponse<Pagination<List<PatientList>>> response = new CommonResponse<Pagination<List<PatientList>>>();
            try
            {
                var data = _patientService.GetAll(pagination);
                var count = _patientService.Count();
                response.Result = new Pagination<List<PatientList>>(count, pagination.PageSize, pagination.PageIndex, pagination.Sort, data);
            }
            catch (ServiceException ex)
            {
                response.StatusText = ex.Message;
            }
            return response;
        }

        [HttpPost]
        [Route("api/patient/add")]
        public CommonResponse Add(PatientViewModel patientViewModel)
        {
            CommonResponse<PatientViewModel> response = new CommonResponse<PatientViewModel>();
            try
            {
                var model = _patientService.Add(_mapper.Map<Patient>(patientViewModel));
                response.Result = _mapper.Map<PatientViewModel>(model);
            }
            catch (ServiceException ex)
            {
                response.StatusText = ex.Message;
            }
            return response;
        }

        [HttpPut]
        [Route("api/patient/update")]
        public CommonResponse Update(PatientViewModel patientViewModel)
        {
            CommonResponse<PatientViewModel> response = new CommonResponse<PatientViewModel>() { ActionName = nameof(Update) };
            try
            {
                var model = _patientService.Update(_mapper.Map<Patient>(patientViewModel));
                response.Result = _mapper.Map<PatientViewModel>(model);
            }
            catch (ServiceException ex)
            {
                response.StatusText = ex.Message;
            }
            return response;
        }

        [HttpDelete]
        [Route("api/patient/remove/{id}")]
        public CommonResponse Remove(int id)
        {
            CommonResponse response = new CommonResponse();
            try
            {
                _patientService.Remove(id);
            }
            catch (ServiceException ex)
            {
                response.StatusText = ex.Message;
            }
            return response;
        }
    }
}
