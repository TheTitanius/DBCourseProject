using DBCourseProject.Entities;
using DBCourseProject.View;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;

namespace DBCourseProject.ViewModels
{
    public class ManufacturerViewModel
    {
        public Context db;
        public ObservableCollection<Manufacturer> Manufacturers { get; set; }
        private RelayCommand? addCommand;
        private RelayCommand? editCommand;
        private RelayCommand? deleteCommand;
        public ManufacturerViewModel(Context db)
        {
            this.db = db;
            db.Database.EnsureCreated();
            db.Manufacturers.Load();
            Manufacturers = db.Manufacturers.Local.ToObservableCollection();
        }

        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ?? (
                    addCommand = new RelayCommand((o) =>
                    {
                        AddManufacturer addManufacturer = new(new Manufacturer());
                        if (addManufacturer.ShowDialog() == true)
                        {
                            Manufacturer manufacturer = addManufacturer.Manufacturer;
                            db.Manufacturers.Add(manufacturer);
                            db.SaveChanges();
                        }
                    }));
            }
        }

        public RelayCommand DeleteCommand
        {
            get
            {
                return deleteCommand ??
                  (deleteCommand = new RelayCommand((selectedItem) =>
                  {
                      // получаем выделенный объект
                      if (selectedItem is not Manufacturer manufacturer) return;
                      db.Manufacturers.Remove(manufacturer);
                      db.SaveChanges();
                  }));
            }
        }

        public RelayCommand EditCommand
        {
            get
            {
                return editCommand ??
                    (editCommand = new RelayCommand((selectedItem) =>
                    {
                        // получаем выделенный объект
                        if (selectedItem is not Manufacturer manufacturer) return;

                        Manufacturer oldManufacturer = new()
                        {
                            ManufacturerId = manufacturer.ManufacturerId,
                            Name = manufacturer.Name,
                            Director = manufacturer.Director,
                            ChiefAccountant = manufacturer.ChiefAccountant,
                            BankDetails = manufacturer.BankDetails,
                        };
                        AddManufacturer addManufacturer = new(oldManufacturer);

                        if (addManufacturer.ShowDialog() == true)
                        {
                            manufacturer.Name = addManufacturer.Manufacturer.Name;
                            manufacturer.Director = addManufacturer.Manufacturer.Director;
                            manufacturer.ChiefAccountant = addManufacturer.Manufacturer.ChiefAccountant;
                            manufacturer.BankDetails = addManufacturer.Manufacturer.BankDetails;
                            db.Entry(manufacturer).State = EntityState.Modified;
                            db.SaveChanges();
                        }
                    }));
            }
        }
    }
}
