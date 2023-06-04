using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;

namespace DBCourseProject.Entities
{
    public class Contract
    {
        public int ContractId { get; set; }
        public int ContractNumber { get; set; }
        public DateTime Date { get ; set; }
        public string? ShortText { get; set; }
        public string Product { get; set; }
        public Manufacturer? Manufacturer { get; set; }
        public Client? Client { get; set; }
    }
}
