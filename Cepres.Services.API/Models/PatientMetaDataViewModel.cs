using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cepres.Services.API.Models
{
    public class PatientMetaDataViewModel
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public int PatientId { get; set; }
    }
}
