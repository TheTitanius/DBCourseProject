using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DBCourseProject.Entities
{
    public class Bill : INotifyPropertyChanged
    {
        public int BillId { get; set; }
        public int BillNumber { get; set; }
        public DateTime Date { get; set; }
        
        public int Sum { get; set; }

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
