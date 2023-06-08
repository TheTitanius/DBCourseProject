using NodaTime;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DBCourseProject.Entities
{
    public class Bill : INotifyPropertyChanged
    {
        public int BillId { get; set; }

        private int billNumber;
        public int BillNumber { get { return billNumber; } set
            {
                billNumber = value;
                OnPropertyChanged(nameof(BillNumber));
            }
        }

        private Instant date;
        public Instant Date { get { return date; } set
            {
                date = value;
                OnPropertyChanged(nameof(Date));
            }
        }

        private int sum;
        public int Sum
        {
            get { return sum; }
            set
            {
                sum = value;
                OnPropertyChanged(nameof(Sum));
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
