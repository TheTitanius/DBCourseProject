using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DBCourseProject.Entities
{
    public class PurchaseOrder : INotifyPropertyChanged
    {
        public int PurchaseOrderId { get; set; }

        private bool paymentStat = false;
        public bool PaymentStat
        {
            get { return paymentStat; }
            set
            {
                paymentStat = value;
                OnPropertyChanged(nameof(PaymentStat));
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

        private Contract contract;
        public Contract Contract
        {
            get { return contract; }
            set
            {
                contract = value;
                OnPropertyChanged(nameof(Contract));
            }
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
