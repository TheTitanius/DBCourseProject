using NodaTime;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DBCourseProject.Entities
{
    public class Bill : INotifyPropertyChanged
    {
        public int BillId { get; set; }

        private Instant date = Instant.FromUtc(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute);
        public Instant Date { get { return date; } set
            {
                date = value;
                OnPropertyChanged(nameof(Date));
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
