using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DBCourseProject.Entities
{
    public class Manufacturer : INotifyPropertyChanged
    {
        public int ManufacturerId { get; set; }

        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        private string director;
        public string Director
        {
            get { return director; }
            set
            {
                director = value;
                OnPropertyChanged(nameof(Director));
            }
        }

        private string chiefAccountant;
        public string ChiefAccountant
        {
            get { return chiefAccountant; }
            set
            {
                chiefAccountant = value;
                OnPropertyChanged(nameof(ChiefAccountant));
            }
        }

        private long bankDetails;
        public long BankDetails
        {
            get { return bankDetails; }
            set
            {
                bankDetails = value;
                OnPropertyChanged(nameof(BankDetails));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
