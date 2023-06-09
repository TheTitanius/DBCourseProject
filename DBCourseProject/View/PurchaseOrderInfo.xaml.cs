using DBCourseProject.Entities;
using System.Collections.Generic;
using System.Windows;

namespace DBCourseProject.View
{
    /// <summary>
    /// Логика взаимодействия для PurchaseOrderViewModel.xaml
    /// </summary>
    public partial class PurchaseOrderInfo : Window
    {
        public PurchaseOrder PurchaseOrder { get; set; }
        public List<TV> TVs { get; set; }
        public List<uint> TVsQuantity { get; set; }
        public class Sourses
        {
            public TV TV { get; set; }
            public uint TVQuantity { get; set; }
        }
        public PurchaseOrderInfo(PurchaseOrder purchaseOrder, List<TV> tvs, List<uint> tvsQuantity)
        {
            InitializeComponent();

            DataContext = this;

            PurchaseOrder = purchaseOrder;
            TVs = tvs;
            TVsQuantity = tvsQuantity;

            List<Sourses> sourses = new();

            for (int i = 0; i < TVs.Count; i++)
            {
                sourses.Add(new()
                {
                    TV = TVs[i],
                    TVQuantity = TVsQuantity[i]
                });
            }

            tvList.ItemsSource = sourses;

            if (PurchaseOrder.Contract.IsSupply)
            {
                buyerInfo.Visibility = Visibility.Collapsed;
                supplierInfo.Visibility = Visibility.Visible;
            }
            else
            {
                buyerInfo.Visibility = Visibility.Visible;
                supplierInfo.Visibility = Visibility.Collapsed;
            }
        }
    }
}
