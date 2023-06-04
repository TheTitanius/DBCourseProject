using System.Collections.Generic;

namespace DBCourseProject.Entities
{
    public class PurchaseOrder
    {
        public int PurchaseOrderId { get; set; }
        public int OrderNumber { get; set; }
        public bool PaymentStat { get; set; }
        public int Sum { get; set; }
        public Contract Contract { get; set; }
    }
}
