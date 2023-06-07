using DBCourseProject.Entities;
using DBCourseProject.View;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;

namespace DBCourseProject.ViewModels
{
    public class ContractViewModel
    {
        public Context db;
        public ObservableCollection<Contract> Contracts { get; set; }
        public ObservableCollection<Manufacturer> Manufacturers { get; set; }
        public ObservableCollection<Client> Clients { get; set; }

        private RelayCommand? manufacturerAddCommand;
        private RelayCommand? clientAddCommand;
        private RelayCommand? editCommand;
        private RelayCommand? deleteCommand;
        public ContractViewModel(Context db)
        {
            this.db = db;
            db.Database.EnsureCreated();

            db.Contracts.Load();
            Contracts = db.Contracts.Local.ToObservableCollection();

            db.Manufacturers.Load();
            Manufacturers = db.Manufacturers.Local.ToObservableCollection();

            db.Clients.Load();
            Clients = db.Clients.Local.ToObservableCollection();
        }

        public RelayCommand ManufacturerAddCommand
        {
            get
            {
                return manufacturerAddCommand ?? (
                    manufacturerAddCommand = new RelayCommand((o) =>
                    {
                        AddContract addContract = new(new Contract(), Manufacturers, Clients, true);
                        if (addContract.ShowDialog() == true)
                        {
                            Contract contract = addContract.Contract;
                            contract.IsSupply = true;

                            contract.Manufacturer = db.Manufacturers.Where(p => p.Name == addContract.ManufacturerName).First();

                            db.Contracts.Add(contract);
                            db.SaveChanges();
                        }
                    }));
            }
        }

        public RelayCommand ClientAddCommand
        {
            get
            {
                return clientAddCommand ?? (
                    clientAddCommand = new RelayCommand((o) =>
                    {
                        AddContract addContract = new(new Contract(), Manufacturers, Clients, false);
                        if (addContract.ShowDialog() == true)
                        {
                            Contract contract = addContract.Contract;
                            contract.IsSupply = false;

                            contract.Client = db.Clients.Where(p => p.FullName == addContract.FullName).First();

                            db.Contracts.Add(contract);
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
                      if (selectedItem is not Contract contract) return;
                      db.Contracts.Remove(contract);
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
                        if (selectedItem is not Contract contract) return;

                        Contract oldContract = new()
                        {
                            ContractId = contract.ContractId,
                            ContractNumber = contract.ContractNumber,
                            Date = contract.Date,
                            IsSupply = contract.IsSupply,
                            Manufacturer = contract.Manufacturer,
                            Client = contract.Client,
                        };
                        AddContract addContract = new(oldContract, Manufacturers, Clients, oldContract.IsSupply);

                        if (addContract.ShowDialog() == true)
                        {
                            contract.ContractNumber = addContract.Contract.ContractNumber;
                            contract.Date = addContract.Contract.Date;
                            contract.IsSupply = addContract.Contract.IsSupply;

                            if(contract.IsSupply == true)
                            {
                                contract.Manufacturer = db.Manufacturers.Where(p => p.Name == addContract.ManufacturerName).First();
                            }
                            else
                            {
                                contract.Client = db.Clients.Where(p => p.FullName == addContract.FullName).First();
                            }

                            db.Entry(contract).State = EntityState.Modified;
                            db.SaveChanges();
                        }
                    }));
            }
        }
    }
}
