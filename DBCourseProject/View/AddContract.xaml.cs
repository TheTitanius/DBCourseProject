using DBCourseProject.Entities;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace DBCourseProject.View
{
    /// <summary>
    /// Логика взаимодействия для AddContract.xaml
    /// </summary>
    public partial class AddContract : Window
    {
        public Contract Contract { get; set; }
        public ObservableCollection<Manufacturer> Manufacturers { get; set; }
        public ObservableCollection<Client> Clients { get; set; }
        public string FullName { get; set; }
        public string ManufacturerName { get; set; }
        public AddContract(Contract contract, ObservableCollection<Manufacturer> manufacturers, ObservableCollection<Client> clients, bool isSupply)
        {
            InitializeComponent();

            Contract = contract;
            DataContext = this;

            if (isSupply)
            {
                manufacturerGrid.Visibility = Visibility.Visible;
                buyerGrid.Visibility = Visibility.Collapsed;

                Manufacturers = manufacturers;
                List<string> manufacturersName = new();
                foreach (Manufacturer manufacturer in Manufacturers)
                {
                    manufacturersName.Add(manufacturer.Name);
                }

                manufacturerComboBox.ItemsSource = manufacturersName;

                if (Contract.Manufacturer != null)
                {
                    ManufacturerName = Contract.Manufacturer.Name;
                }
            }
            else
            {
                manufacturerGrid.Visibility = Visibility.Collapsed;
                buyerGrid.Visibility = Visibility.Visible;

                Clients = clients;
                List<string> clientsName = new();
                foreach (Client client in Clients)
                {
                    clientsName.Add(client.FullName);
                }

                buyerComboBox.ItemsSource = clientsName;

                if (Contract.Client != null)
                {
                    FullName = Contract.Client.FullName;
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
