using DBCourseProject.Entities;
using DBCourseProject.View;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Linq;

namespace DBCourseProject.ViewModels
{
    public class TVViewModel
    {
        public Context db;
        public ObservableCollection<TV> TVs { get; set; }
        public ObservableCollection<Manufacturer> Manufacturers { get; set; }
        public ObservableCollection<TVType> TVTypes { get; set; }

        private RelayCommand? addCommand;
        private RelayCommand? editCommand;
        private RelayCommand? deleteCommand;
        public TVViewModel(Context db)
        {
            this.db = db;
            db.Database.EnsureCreated();

            db.TVs.Load();
            TVs = db.TVs.Local.ToObservableCollection();

            db.Manufacturers.Load();
            Manufacturers = db.Manufacturers.Local.ToObservableCollection();

            db.TVTypes.Load();
            TVTypes = db.TVTypes.Local.ToObservableCollection();
        }

        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ?? (
                    addCommand = new RelayCommand((o) =>
                    {
                        AddTV addTV = new(new TV(), Manufacturers, TVTypes);
                        if (addTV.ShowDialog() == true)
                        {
                            TV tv = addTV.TV;
                            if(db.TVTypes.Where(p => p.Type == addTV.TVType).FirstOrDefault() == null)
                            {
                                TVType type = new()
                                {
                                    Type = addTV.TVType
                                };

                                db.TVTypes.Add(type);

                                tv.TVType = type;
                            }
                            else
                            {
                                tv.TVType = db.TVTypes.Where(p => p.Type == addTV.TVType).First();
                            }

                            tv.Manufacturer = db.Manufacturers.Where(p => p.Name == addTV.ManufacturerName).First();
                            db.TVs.Add(tv);
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
                      if (selectedItem is not TV tv) return;
                      db.TVs.Remove(tv);
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
                        if (selectedItem is not TV tv) return;

                        TV oldTV = new()
                        {
                            TVId = tv.TVId,
                            Name = tv.Name,
                            Price = tv.Price,
                            TVType = tv.TVType,
                            Manufacturer = tv.Manufacturer,
                        };
                        AddTV addTV = new(oldTV, Manufacturers, TVTypes);

                        if (addTV.ShowDialog() == true)
                        {
                            tv.Name = addTV.TV.Name;
                            tv.Price = addTV.TV.Price;

                            if (db.TVTypes.Where(p => p.Type == addTV.TVType).FirstOrDefault() == null)
                            {
                                TVType type = new()
                                {
                                    Type = addTV.TVType
                                };

                                db.TVTypes.Add(type);

                                tv.TVType = type;
                            }
                            else
                            {
                                tv.TVType = db.TVTypes.Where(p => p.Type == addTV.TVType).First();
                            }

                            tv.Manufacturer = db.Manufacturers.Where(p => p.Name == addTV.ManufacturerName).First();

                            db.Entry(tv).State = EntityState.Modified;
                            db.SaveChanges();
                        }
                    }));
            }
        }
    }
}
