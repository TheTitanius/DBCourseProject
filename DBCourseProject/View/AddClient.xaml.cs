using DBCourseProject.Entities;
using System.Windows;

namespace DBCourseProject.View
{
    /// <summary>
    /// Логика взаимодействия для AddClient.xaml
    /// </summary>
    public partial class AddClient : Window
    {
        public Client Client { get; set; }
        public AddClient(Client client)
        {
            InitializeComponent();
            Client = client;
            DataContext = Client;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
