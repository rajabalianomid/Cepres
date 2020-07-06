using Cepres.Common.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cepres.Domain
{
    public class PatientList : BaseEntity
    {
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime LastTimeOfEntry { get; set; }
        public int MetaDataCount { get; set; }
    }
}
