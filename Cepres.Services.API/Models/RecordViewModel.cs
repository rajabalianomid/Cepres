using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cepres.Services.API.Models
{
    public class RecordViewModel
    {
        public int Id { get; set; }
        public string DiseaseName { get; set; }
        public string TimeOfEntry { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int PatientId { get; set; }
        public decimal Bill { get; set; }
    }
}
