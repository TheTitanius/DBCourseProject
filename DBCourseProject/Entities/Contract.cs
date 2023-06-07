using NodaTime;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DBCourseProject.Entities
{
    public class Contract : INotifyPropertyChanged
    {
        public int ContractId { get; set; }

        private int contractNumber;
        public int ContractNumber
        {
            get { return contractNumber; }
            set
            {
                contractNumber = value;
                OnPropertyChanged(nameof(ContractNumber));
            }
        }

        private Instant date = Instant.FromUtc(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute);
        public Instant Date
        {
            get { return date; }
            set
            {
                date = value;
                OnPropertyChanged(nameof(Date));
            }
        }

        private bool isSupply;
        public bool IsSupply
        {
            get { return isSupply; }
            set
            {
                isSupply = value;
                OnPropertyChanged(nameof(IsSupply));
            }
        }

        private Manufacturer? manufacturer;
        public Manufacturer? Manufacturer
        {
            get { return manufacturer; }
            set
            {
                manufacturer = value;
                OnPropertyChanged(nameof(Manufacturer));
            }
        }

        private Client? client;
        public Client? Client
        {
            get { return client; }
            set
            {
                client = value;
                OnPropertyChanged(nameof(Client));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
