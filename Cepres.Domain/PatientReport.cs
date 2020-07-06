using System;
using System.Collections.Generic;
using System.Text;

namespace Cepres.Domain
{
    public class PatientReport
    {
        public int Id { get; set; }
        public int RecordId { get; set; }
        public int PatientId { get; set; }
        public double Avrage { get; set; }
        public double StandardDiviations { get; set; }
        public decimal Bill { get; set; }
        public string Name { get; set; }
        public string DiseaseName { get; set; }
        public int Age { get; set; }
        public long RowNum { get; set; }
        public string MaxVisitMonth { get; set; }
        public double OutliersStandardDiviations { get; set; }
    }
}
