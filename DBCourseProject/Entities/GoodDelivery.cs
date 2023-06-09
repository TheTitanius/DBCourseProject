using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DBCourseProject.Entities
{
    public class GoodDelivery : INotifyPropertyChanged
    {
        public int GoodDeliveryId { get; set; }

        private uint quantity;
        public uint Quantity
        {
            get { return quantity; }
            set
            {
                quantity = value;
                OnPropertyChanged(nameof(Quantity));
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

        private uint sum;
        public uint Sum
        {
            get { return sum; }
            set
            {
                sum = value;
                OnPropertyChanged(nameof(Sum));
            }
        }

        private PurchaseOrder? purchaseOrder;
        public PurchaseOrder? PurchaseOrder
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
