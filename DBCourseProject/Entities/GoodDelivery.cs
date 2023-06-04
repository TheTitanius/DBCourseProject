using System.Collections.Generic;

namespace DBCourseProject.Entities
{
    public class GoodDelivery
    {
        public int GoodDeliveryId { get; set; }
        public int Quantity { get; set; }
        public string Name { get; set; }
        public TV TV { get; set; }
        public PurchaseOrder PurchaseOrder { get; set;}
    }
}
