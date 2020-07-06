using Cepres.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cepres.Services.API.Models
{
    public class MetaDataReportViewModel
    {
        public List<General> MetaData { get; set; }
        public double Average { get; set; }
        public int Max { get; set; }
    }
}
