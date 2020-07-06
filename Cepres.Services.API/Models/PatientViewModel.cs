using Cepres.Service.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cepres.Services.API.Models
{
    public class PatientViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int OfficialId { get; set; }
        public string DateOfBirth { get; set; }
        public string Email { get; set; }
    }
}
