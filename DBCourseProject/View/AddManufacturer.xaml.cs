using DBCourseProject.Entities;
using System.Windows;

namespace DBCourseProject.View
{
    /// <summary>
    /// Логика взаимодействия для AddManufacturer.xaml
    /// </summary>
    public partial class AddManufacturer : Window
    {
        public Manufacturer Manufacturer { get; set; }
        public AddManufacturer(Manufacturer manufacturer)
        {
            InitializeComponent();
            Manufacturer = manufacturer;
            DataContext = Manufacturer;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
