using DBCourseProject.ViewModels;
using System.Windows;

namespace DBCourseProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Context db;
        public MainWindow()
        {
            InitializeComponent();
            db = new Context();
            ClientItem.DataContext = new ClientViewModel(db);
            ManufacturerItem.DataContext = new ManufacturerViewModel(db);
            TVItem.DataContext = new TVViewModel(db);
            ContractsItem.DataContext = new ContractViewModel(db);
        }
    }
}
