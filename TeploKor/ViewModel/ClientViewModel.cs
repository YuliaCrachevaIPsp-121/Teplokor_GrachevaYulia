using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TeploKor.Model;
using System.IO;
using System.Windows;
using TeploKor.View;
using TeploKor.Helper;

namespace TeploKor.ViewModel
{
    class ClientViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Client> ListClient { get; set; }
        public static int ClientId;
        public int MaxId()
        {
            int max = 0;
            if (this.ListClient != null)
            {
                foreach (var cl in this.ListClient)
                {
                    if (max < cl.clientId) max = cl.clientId;
                }
            }
            return max;
        }

        public ClientViewModel()
        {
            string connectionString = "Data Source=D:\\TeploKor\\TeploKor\\BD\\TeploKor.db";

            List<Client> clients = MyDbContext.GetEntities<Client>(connectionString, "SELECT * FROM Client");

            // Преобразование в ObservableCollection
            ListClient = new ObservableCollection<Client>(clients);
        }

        private RelayCommand editClient;
        public RelayCommand EditClient
        {
            get
            {
                return editClient ??
                (editClient = new RelayCommand(obj =>
                {
                    WindowRegistration wnClient = new WindowRegistration
                    { Title = "Редактирование клиента" };

                    Client client = SelectedClient;
                    Client tempClient = new Client();

                    tempClient = client.ShallowCopy();
                    wnClient.DataContext = tempClient;
                    if (wnClient.ShowDialog() == true)
                    {
                        client.clientId = tempClient.clientId;
                        client.clientSurname = tempClient.clientSurname;
                        client.clientName= tempClient.clientName;
                        client.clientPatronymic = tempClient.clientPatronymic;
                        client.clientContactNumber = tempClient.clientContactNumber;
                        client.clientEmail = tempClient.clientEmail;
                        client.clientPassword = tempClient.clientPassword;
                    }
                }, (obj) => SelectedClient != null && ListClient.Count > 0));
            }
        }


        private Client selectedClient;
        public Client SelectedClient
        {
            get
            {
                return selectedClient;
            }
            set
            {
                selectedClient = value;
                OnPropertyChanged("SelectedClient");
                EditClient.CanExecute(true);
            }
        }

        private RelayCommand addClient;
        public RelayCommand AddClient
        {
            get
            {
                return addClient ??
                 (addClient = new RelayCommand(obj =>
                 {
                     WindowRegistration wnClient = new WindowRegistration
                     {
                         Title = "Новый клиент",
                     };
                     int maxIdClient = MaxId() + 1;
                     Client client = new Client { clientId = maxIdClient };
                     wnClient.DataContext = client;
                     if (wnClient.ShowDialog() == true)
                     {
                         ListClient.Add(client);
                     }
                     SelectedClient = client;
                 }));
            }
        }

        private RelayCommand deleteClient;
        public RelayCommand DeleteClient
        {
            get
            {
                return deleteClient ??
                (deleteClient = new RelayCommand(obj =>
                {
                    Client client = SelectedClient;
                    MessageBoxResult result = MessageBox.Show("Вы точно хотите удалить свой профиль? ", "Предупреждение", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                    if (result == MessageBoxResult.OK)
                    {
                        ListClient.Remove(client);
                    }
                }, (obj) => SelectedClient != null && ListClient.Count > 0));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}