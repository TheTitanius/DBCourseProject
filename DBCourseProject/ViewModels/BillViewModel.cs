using DBCourseProject.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;

namespace DBCourseProject.ViewModels
{
    public class BillViewModel
    {
        public Context db;
        public ObservableCollection<Bill> Bills { get; set; }

        public BillViewModel(Context db)
        {
            this.db = db;
            db.Database.EnsureCreated();

            db.Bills.Load();
            Bills = db.Bills.Local.ToObservableCollection();

            db.PurchaseOrders.Load();
            db.Contracts.Load();
            db.Manufacturers.Load();
            db.Clients.Load();
        }
    }
}
