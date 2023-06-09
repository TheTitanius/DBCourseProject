using DBCourseProject.Entities;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace DBCourseProject.View
{
    /// <summary>
    /// Логика взаимодействия для AddPurchaseOrder.xaml
    /// </summary>
    public partial class AddPurchaseOrder : Window
    {
        public PurchaseOrder PurchaseOrder { get; set; }
        public ObservableCollection<Contract> Contracts { get; set; }
        public string ContractsNormalName { get; set; }
        public AddPurchaseOrder(PurchaseOrder purchaseOrder, ObservableCollection<Contract> contracts)
        {
            InitializeComponent();

            DataContext = this;

            PurchaseOrder = purchaseOrder;
            Contracts = contracts;


            List<string> contractsNormalname = new();
            foreach (Contract contract in Contracts)
            {
                string str = "Id: ";
                str += contract.ContractId;
                str += ", №: ";
                str += contract.ContractNumber.ToString();
                str += ", ";

                if(contract.Client != null)
                {
                    str += "Покупатель: ";
                    str += contract.Client.FullName;
                }

                if (contract.Manufacturer != null)
                {
                    str += "Поставщик: ";
                    str += contract.Manufacturer.Name;
                }

                contractsNormalname.Add(str);
            }

            contractComboBox.ItemsSource = contractsNormalname;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
