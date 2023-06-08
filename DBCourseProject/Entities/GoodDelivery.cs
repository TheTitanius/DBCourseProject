using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DBCourseProject.Entities
{
    public class GoodDelivery : INotifyPropertyChanged
    {
        public int GoodDeliveryId { get; set; }

        private int quantity;
        public int Quantity
        {
            get { return quantity; }
            set
            {
                quantity = value;

            }
        }

        private TV tv;
        public TV TV
        {
            get { return tv; }
            set
            {
                tv = value;
                OnPropertyChanged(nameof(TV));
            }
        }

        private PurchaseOrder purchaseOrder;
        public PurchaseOrder PurchaseOrder
        {
            get { return purchaseOrder; }
            set
            {
                purchaseOrder = value;
                OnPropertyChanged(nameof(PurchaseOrder));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
