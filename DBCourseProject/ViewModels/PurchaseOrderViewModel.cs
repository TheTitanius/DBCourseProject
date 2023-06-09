using DBCourseProject.Entities;
using DBCourseProject.View;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace DBCourseProject.ViewModels
{
    public class PurchaseOrderViewModel
    {
        public Context db;
        public ObservableCollection<PurchaseOrder> PurchaseOrders { get; set; }

        private RelayCommand? deleteCommand;
        private RelayCommand? payCommand;
        private RelayCommand? openCommand;
        public PurchaseOrderViewModel(Context db)
        {
            this.db = db;
            db.Database.EnsureCreated();

            db.PurchaseOrders.Load();
            PurchaseOrders = db.PurchaseOrders.Local.ToObservableCollection();

            db.Bills.Load();
        }

        public RelayCommand DeleteCommand
        {
            get
            {
                return deleteCommand ??
                  (deleteCommand = new RelayCommand((selectedItem) =>
                  {
                      // получаем выделенный объект
                      if (selectedItem is not PurchaseOrder purchaseOrder) return;
                      db.PurchaseOrders.Remove(purchaseOrder);
                      db.SaveChanges();
                  }));
            }
        }

        public RelayCommand PayCommand
        {
            get
            {
                return payCommand ?? (
                    payCommand = new RelayCommand((selectedItem) =>
                    {
                        if (selectedItem is not PurchaseOrder purchaseOrder) return;
                        if (purchaseOrder.PaymentStat) return;

                        PurchaseOrder pO = db.PurchaseOrders.Local.Where(p => p.PurchaseOrderId == purchaseOrder.PurchaseOrderId).First();
                        pO.PaymentStat = true;

                        db.Entry(pO).State = EntityState.Modified;
                        db.SaveChanges();

                        Bill bill = new()
                        {
                            Sum = pO.Sum,
                            PurchaseOrder = pO,
                        };

                        db.Bills.Add(bill);
                        db.SaveChanges();

                        var goodDeliveries = db.GoodsDelivery.Where(p => p.PurchaseOrder != null).Where(p => p.PurchaseOrder.PurchaseOrderId == pO.PurchaseOrderId).ToList();


                        foreach (GoodDelivery goodDelivery in goodDeliveries)
                        {
                            TV tv = db.TVs.Where(p => p.TVId == goodDelivery.TV.TVId).First();
                            if (pO.Contract.IsSupply)
                            {
                                tv.TvInStock += goodDelivery.Quantity;
                            }
                            else
                            {
                                tv.TvInStock -= goodDelivery.Quantity;
                            }

                            db.Entry(tv).State = EntityState.Modified;
                            db.SaveChanges();
                        }
                    }));
            }
        }

        public RelayCommand OpenCommand
        {
            get
            {
                return openCommand ?? (
                    openCommand = new RelayCommand((selectedItem) =>
                    {
                        if (selectedItem is not PurchaseOrder purchaseOrder) return;

                        PurchaseOrder pO = new()
                        {
                            PurchaseOrderId = purchaseOrder.PurchaseOrderId,
                            OrderNumber = purchaseOrder.OrderNumber,
                            PaymentStat = purchaseOrder.PaymentStat,
                            Sum = purchaseOrder.Sum,
                            Contract = purchaseOrder.Contract,
                        };

                        var goodDeliveries = db.GoodsDelivery.Where(p => p.PurchaseOrder != null).Where(p => p.PurchaseOrder.PurchaseOrderId == purchaseOrder.PurchaseOrderId).ToList();

                        List<TV> tvs = new();
                        List<uint> tvsQuantity = new();

                        foreach (GoodDelivery goodDelivery in goodDeliveries)
                        {
                            tvs.Add(db.TVs.Where(p => p.TVId == goodDelivery.TV.TVId).First());
                            tvsQuantity.Add(goodDelivery.Quantity);
                        }

                        PurchaseOrderInfo purchaseOrderInfo = new(pO, tvs, tvsQuantity);
                        if (purchaseOrderInfo.ShowDialog() == false)
                        {
                        }
                    }));
            }
        }
    }
}
