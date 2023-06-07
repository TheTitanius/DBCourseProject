using DBCourseProject.Entities;
using DBCourseProject.View;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace DBCourseProject.ViewModels
{
    public class ClientViewModel
    {
        public Context db;
        public ObservableCollection<Client> Clients { get; set; }
        private RelayCommand? addCommand;
        private RelayCommand? editCommand;
        private RelayCommand? deleteCommand;
        public ClientViewModel(Context db)
        {
            this.db = db;
            db.Database.EnsureCreated();
            db.Clients.Load();
            Clients = db.Clients.Local.ToObservableCollection();
        }

        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ?? (
                    addCommand = new RelayCommand((o) =>
                    {
                        AddClient addClient = new(new Client());
                        if (addClient.ShowDialog() == true)
                        {
                            Client client = addClient.Client;
                            db.Clients.Add(client);
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
                      if (selectedItem is not Client client) return;
                      db.Clients.Remove(client);
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
                        if (selectedItem is not Client client) return;

                        Client oldClient = new()
                        {
                            ClientId = client.ClientId,
                            FullName = client.FullName,
                            Telephone = client.Telephone,
                            BankAccount = client.BankAccount,
                        };
                        AddClient addClient = new (oldClient);


                        if (addClient.ShowDialog() == true)
                        {
                            client.FullName = addClient.Client.FullName;
                            client.Telephone = addClient.Client.Telephone;
                            client.BankAccount = addClient.Client.BankAccount;
                            db.Entry(client).State = EntityState.Modified;
                            db.SaveChanges();
                        }
                    }));
            }
        }
    }
}
