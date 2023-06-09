using DBCourseProject.Entities;
using System.Windows;

namespace DBCourseProject.View
{
    /// <summary>
    /// Логика взаимодействия для AddTVToCart.xaml
    /// </summary>
    public partial class AddTVToCart : Window
    {
        public GoodDelivery GoodDelivery { get; set; }
        public AddTVToCart(GoodDelivery goodDelivery, TV tv)
        {
            InitializeComponent();

            GoodDelivery = goodDelivery;
            GoodDelivery.TV = tv;

            DataContext = GoodDelivery;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
