using Cepres.Common.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Cepres.Domain
{
    public class Patient : BaseEntity
    {
        public int OfficialId { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }

        public virtual ICollection<Record> Records { get; set; }
        public virtual ICollection<PatientMetaData> PatientMetaDatas { get; set; }
    }
}
