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
    public class RecordController : ControllerBase
    {
        private readonly IRecordService _recordService;
        private readonly IMapper _mapper;
        public RecordController(IRecordService recordService, IMapper mapper)
        {
            _recordService = recordService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("api/record/get/{id}")]
        public CommonResponse Get(int id)
        {
            CommonResponse<RecordViewModel> response = new CommonResponse<RecordViewModel>();
            try
            {
                response.Result = _mapper.Map<RecordViewModel>(_recordService.Get(id));
            }
            catch (ServiceException ex)
            {
                response.StatusText = ex.Message;
            }
            return response;
        }

        [HttpPost]
        [Route("api/record/getall")]
        public CommonResponse GetAll(Pagination pagination)
        {
            CommonResponse<Pagination<List<RecordList>>> response = new CommonResponse<Pagination<List<RecordList>>>();
            try
            {
                var data = _recordService.GetAll(pagination);
                var count = _recordService.Count();
                response.Result = new Pagination<List<RecordList>>(count, pagination.PageSize, pagination.PageIndex, pagination.Sort, data);
            }
            catch (ServiceException ex)
            {
                response.StatusText = ex.Message;
            }
            return response;
        }

        [HttpPost]
        [Route("api/record/add")]
        public CommonResponse Add(RecordViewModel recordViewModel)
        {
            CommonResponse<RecordViewModel> response = new CommonResponse<RecordViewModel>();
            try
            {
                var model = _recordService.Add(_mapper.Map<Record>(recordViewModel));
                response.Result = _mapper.Map<RecordViewModel>(model);
            }
            catch (ServiceException ex)
            {
                response.StatusText = ex.Message;
            }
            return response;
        }

        [HttpPut]
        [Route("api/record/update")]
        public CommonResponse Update(RecordViewModel recordViewModel)
        {
            CommonResponse<RecordViewModel> response = new CommonResponse<RecordViewModel>() { ActionName = nameof(Update) };
            try
            {
                var model = _recordService.Update(_mapper.Map<Record>(recordViewModel));
                response.Result = _mapper.Map<RecordViewModel>(model);
            }
            catch (ServiceException ex)
            {
                response.StatusText = ex.Message;
            }
            return response;
        }

        [HttpDelete]
        [Route("api/record/remove/{id}")]
        public CommonResponse Remove(int id)
        {
            CommonResponse response = new CommonResponse();
            try
            {
                _recordService.Remove(id);
            }
            catch (ServiceException ex)
            {
                response.StatusText = ex.Message;
            }
            return response;
        }
    }
}
