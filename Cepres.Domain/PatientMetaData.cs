using Cepres.Common.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cepres.Domain
{
    public class PatientMetaData : BaseEntity
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public int PatientId { get; set; }

        public virtual Patient Patient { get; set; }
    }
}
