using DBCourseProject.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace DBCourseProject.View
{
    /// <summary>
    /// Логика взаимодействия для AddTV.xaml
    /// </summary>
    public partial class AddTV : Window
    {
        public TV TV { get; set; }
        public ObservableCollection<Manufacturer> Manufacturers { get; set; }
        public ObservableCollection<TVType> TVTypes { get; set; }
        public string TVType { get; set; }
        public string ManufacturerName { get; set; }
        public AddTV(TV tv, ObservableCollection<Manufacturer> manufacturers, ObservableCollection<TVType> tvTypes)
        {
            InitializeComponent();

            TV = tv;
            DataContext = this;

            Manufacturers = manufacturers;
            List<string> manufacturersName = new();
            foreach (Manufacturer manufacturer in Manufacturers)
            {
                manufacturersName.Add(manufacturer.Name);
            }

            manufacturerComboBox.ItemsSource = manufacturersName;
            if(TV.Manufacturer != null)
            {
                ManufacturerName = TV.Manufacturer.Name;
            }

            TVTypes = tvTypes;
            List<string> tvType = new();
            foreach (TVType type in TVTypes)
            {
                tvType.Add(type.Type);
            }

            typeTVComboBox.ItemsSource = tvType;

            if(TV.TVType != null)
            {
                TVType = TV.TVType.Type;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
