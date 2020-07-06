using Cepres.Common.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cepres.Domain
{
    public class Record : BaseEntity
    {
        public int PatientId { get; set; }
        public string DiseaseName { get; set; }
        public DateTime TimeOfEntry { get; set; }
        public string Description { get; set; }
        public decimal Bill { get; set; }

        public virtual Patient Patient { get; set; }
    }
}
