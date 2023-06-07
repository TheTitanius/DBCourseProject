using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DBCourseProject.Entities
{
    public class TV : INotifyPropertyChanged
    {
        public int TVId { get; set; }

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

        private uint price;
        public uint Price
        {
            get { return price; }
            set
            {
                price = value;
                OnPropertyChanged(nameof(Price));
            }
        }

        private TVType tvType;
        public TVType TVType
        {
            get { return tvType; }
            set
            {
                tvType = value;
                OnPropertyChanged(nameof(TVType));
            }
        }

        private Manufacturer manufacturer;
        public Manufacturer Manufacturer
        {
            get { return manufacturer; }
            set
            {
                manufacturer = value;
                OnPropertyChanged(nameof(Manufacturer));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
