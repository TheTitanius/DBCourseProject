using DBCourseProject.Entities;
using System.Windows;

namespace DBCourseProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new Context())
            {
                
                Client client = new() { FullName = "Мигас Александр Сергеевич", Telephone = "+79516885750", BankAccount = 12345678901};

                context.Clients.Add(client);
                context.SaveChanges();
                
            }
        }
    }
}
