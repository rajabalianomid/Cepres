using Cepres.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using Cepres.Data;
using Cepres.Services.API.Controllers;
using Cepres.Service;
using Cepres.Data.Repositories;
using Xunit;
using Cepres.Service.API.Models;
using Cepres.Services.API.Models;

namespace Cepres.Services.API.Test
{
    public class PatientControllerTest
    {
        [Fact]
        public void Check_Get_Patient_By_Id()
        {
            ConfigDB.Instance.AddTestData();
            using (Context context = new Context(ConfigDB.Instance.DbContextOptions))
            {
                IPatientRepository patientRepository = new PatientRepository(context);
                IPatientService patientService = new PatientService(patientRepository);
                var controller = new PatientController(patientService, AutoMapperConfig.Instance.Mapper);
                var result = controller.Get(2);
                var response = Assert.IsType<CommonResponse<PatientViewModel>>(result);
                var model = Assert.IsAssignableFrom<CommonResponse<PatientViewModel>>(result);
                Assert.Equal(2, model.Result.OfficialId);
            }
        }
    }
}
