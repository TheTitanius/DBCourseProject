using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;

namespace DBCourseProject.Entities
{
    public class Bill
    {
        public int BillId { get; set; }
        public int BillNumber { get; set;}
        public DateTime Date {get; set; }
        public int Sum { get; set;}
        public PurchaseOrder PurchaseOrder { get; set; }
    }
}
