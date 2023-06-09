using DBCourseProject.Entities;
using DBCourseProject.View;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;

namespace DBCourseProject.ViewModels
{
    public class GoodDeliveryViewModel
    {
        public Context db;
        public ObservableCollection<GoodDelivery> GoodsDelivery { get; set; }
        public ObservableCollection<Contract> Contracts { get; set; }

        private RelayCommand? addCommand;
        private RelayCommand? editCommand;
        private RelayCommand? deleteCommand;
        public GoodDeliveryViewModel(Context db)
        {
            this.db = db;

            db.GoodsDelivery.Load();
            GoodsDelivery = db.GoodsDelivery.Local.ToObservableCollection();

            db.Contracts.Load();
            Contracts = db.Contracts.Local.ToObservableCollection();

            db.PurchaseOrders.Load();
        }

        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ?? (
                    addCommand = new RelayCommand((selectedItems) =>
                    {
                        // получаем выделенныe объект
                        if (selectedItems is not IList goodsDelivery) return;
                        AddPurchaseOrder addPurchaseOrder = new(new PurchaseOrder(), Contracts);

                        if (addPurchaseOrder.ShowDialog() == true)
                        {
                            PurchaseOrder purchaseOrder = addPurchaseOrder.PurchaseOrder;
                            int id = int.Parse(addPurchaseOrder.ContractsNormalName.Split(' ', ',')[1]);
                            purchaseOrder.Contract = db.Contracts.Where(p => p.ContractId == id).First();

                            uint sum = 0;

                            for (int i = 0; i < goodsDelivery.Count; i++)
                            {
                                for (int j = 0; j < GoodsDelivery.Count; j++)
                                {
                                    GoodDelivery goodDelivery = (GoodDelivery)goodsDelivery[i];

                                    if (goodDelivery.GoodDeliveryId == GoodsDelivery[j].GoodDeliveryId)
                                    {
                                        sum += GoodsDelivery[j].Sum;
                                    }
                                }
                            }

                            purchaseOrder.Sum = sum;

                            db.PurchaseOrders.Add(purchaseOrder);
                            db.SaveChanges();

                            for (int i = 0; i < goodsDelivery.Count; i++)
                            {
                                for (int j = 0; j < GoodsDelivery.Count; j++)
                                {
                                    GoodDelivery goodDelivery = (GoodDelivery)goodsDelivery[i];

                                    if (goodDelivery.GoodDeliveryId == GoodsDelivery[j].GoodDeliveryId)
                                    {
                                        GoodDelivery gD = GoodsDelivery[j];

                                        gD.PurchaseOrder = purchaseOrder;

                                        db.Entry(gD).State = EntityState.Modified;
                                        db.SaveChanges();
                                    }
                                }
                            }
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
                      if (selectedItem is not GoodDelivery goodDelivery) return;
                      db.GoodsDelivery.Remove(goodDelivery);
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
                        if (selectedItem is not GoodDelivery goodDelivery) return;

                        GoodDelivery oldGoodDelivery = new()
                        {
                            GoodDeliveryId = goodDelivery.GoodDeliveryId,
                            Quantity = goodDelivery.Quantity,
                            TV = goodDelivery.TV,
                            Sum = goodDelivery.Sum,
                            PurchaseOrder = goodDelivery.PurchaseOrder,
                        };
                        AddTVToCart addTVToCart = new(oldGoodDelivery, oldGoodDelivery.TV);

                        if (addTVToCart.ShowDialog() == true)
                        {
                            goodDelivery.GoodDeliveryId = addTVToCart.GoodDelivery.GoodDeliveryId;
                            goodDelivery.Quantity = addTVToCart.GoodDelivery.Quantity;
                            goodDelivery.TV = addTVToCart.GoodDelivery.TV;
                            goodDelivery.PurchaseOrder = addTVToCart.GoodDelivery.PurchaseOrder;

                            goodDelivery.Sum = goodDelivery.Quantity * db.TVs.Where(p => p.TVId == goodDelivery.TV.TVId).First().Price;

                            db.Entry(goodDelivery).State = EntityState.Modified;
                            db.SaveChanges();
                        }
                    }));
            }
        }
    }
}
