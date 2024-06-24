using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TeploKor.Helper;
using TeploKor.Model;
using TeploKor.View;

namespace TeploKor.ViewModel
{
    public class BoilerViewModel : INotifyPropertyChanged
    {
        private CurrentUser currentUser;
        public ObservableCollection<Boiler> ListBoiler { get; set; }
        public static int BoilerId;
        public int MaxId()
        {
            int max = 0;
            if (this.ListBoiler != null)
            {
                foreach (var cl in this.ListBoiler)
                {
                    if (max < cl.boilerId) max = cl.boilerId;
                }
            }
            return max;
        }

        public ObservableCollection<Radiator> ListRadiator { get; set; }
        public static int RadiatorId;
        public int MaxRadiatorId()
        {
            int max = 0;
            if (this.ListRadiator != null)
            {
                foreach (var cl in this.ListRadiator)
                {
                    if (max < cl.radiatorId) max = cl.radiatorId;
                }
            }
            return max;
        }

        public BoilerViewModel()
        {
            // Строка подключения к SQLite
            string connectionString = "Data Source=D:\\TeploKor\\TeploKor\\BD\\TeploKor.db";

            List<Boiler> boiler = MyDbContext.GetEntities<Boiler>(connectionString, "SELECT * FROM Boiler");

            // Преобразование в ObservableCollection
            ListBoiler = new ObservableCollection<Boiler>(boiler);

            List<Radiator> radiator = MyDbContext.GetEntities<Radiator>(connectionString, "SELECT * FROM Radiator");

            // Преобразование в ObservableCollection
            ListRadiator = new ObservableCollection<Radiator>(radiator);
        }

        private RelayCommand editBoiler;
        public RelayCommand EditBoiler
        {
            get
            {
                return editBoiler ??
                (editBoiler = new RelayCommand(obj =>
                {
                    WindowNewBoiler wnBoiler = new WindowNewBoiler(currentUser)
                    { Title = "Редактирование котла" };

                    Boiler boiler = SelectedBoiler;
                    Boiler tempBoiler = new Boiler();

                    tempBoiler = boiler.ShallowCopy();
                    wnBoiler.DataContext = tempBoiler;
                    if (wnBoiler.ShowDialog() == true)
                    {
                        boiler.boilerId = tempBoiler.boilerId;
                        boiler.boilerBrand = tempBoiler.boilerBrand;
                        boiler.boilerName = tempBoiler.boilerName;
                        boiler.boilerDescription = tempBoiler.boilerDescription;
                        boiler.boilerPrice = tempBoiler.boilerPrice;
                        boiler.boilerSquare = tempBoiler.boilerSquare;
                        boiler.boilerElectricOrNot = tempBoiler.boilerElectricOrNot;
                        boiler.boilerKikowatt = tempBoiler.boilerKikowatt;
                        boiler.boilerInstallationLocation = tempBoiler.boilerInstallationLocation;
                        boiler.boilerTurbochargedOrNot = tempBoiler.boilerTurbochargedOrNot;
                        boiler.boilerType = tempBoiler.boilerType;
                        boiler.boilerImageSource = tempBoiler.boilerImageSource;

                        MyDbContext dbContext = new MyDbContext();
                        dbContext.UpdateEntity<Boiler>(tempBoiler);
                    }
                }, (obj) => SelectedBoiler != null && ListBoiler.Count > 0));
            }
        }

        private Boiler selectedBoiler;
        public Boiler SelectedBoiler
        {
            get
            {
                return selectedBoiler;
            }
            set
            {
                selectedBoiler = value;
                OnPropertyChanged("SelectedBoiler");
                EditBoiler.CanExecute(true);
            }
        }

        private RelayCommand addBoiler;
        public RelayCommand AddBoiler
        {
            get
            {
                return addBoiler ??
                 (addBoiler = new RelayCommand(obj =>
                 {
                     WindowNewBoiler wnBoiler = new WindowNewBoiler(currentUser)
                     {
                         Title = "Новый котел",
                     };
                     int maxIdBoiler = MaxId() + 1;
                     Boiler boiler = new Boiler { boilerId = maxIdBoiler };
                     wnBoiler.DataContext = boiler;
                     if (wnBoiler.ShowDialog() == true)
                     {
                         ListBoiler.Add(boiler);
                         MyDbContext dbContext = new MyDbContext();
                         dbContext.SaveEntity<Boiler>(dbContext, boiler);
                     }
                     SelectedBoiler = boiler;
                 }));
            }
        }

        private RelayCommand deleteBoiler;
        public RelayCommand DeleteBoiler
        {
            get
            {
                return deleteBoiler ??
                (deleteBoiler = new RelayCommand(obj =>
                {
                    Boiler boiler = SelectedBoiler;
                    MessageBoxResult result = MessageBox.Show("Удалить данные по котлу: " + boiler.boilerName, "Предупреждение", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                    if (result == MessageBoxResult.OK)
                    {
                        ListBoiler.Remove(boiler);
                        MyDbContext dbContext = new MyDbContext();
                        dbContext.DeleteEntityFromDatabase<Boiler>(boiler);
                    }
                }, (obj) => SelectedBoiler != null && ListBoiler.Count > 0));
            }
        }

        private RelayCommand editRadiator;
        public RelayCommand EditRadiator
        {
            get
            {
                return editRadiator ??
                (editRadiator = new RelayCommand(obj =>
                {
                    WindowNewRadiator wnRadiator = new WindowNewRadiator
                    { Title = "Редактирование радиатора" };

                    Radiator radiator = SelectedRadiator;
                    Radiator tempRadiator = new Radiator();

                    tempRadiator = radiator.ShallowCopy();
                    wnRadiator.DataContext = tempRadiator;
                    if (wnRadiator.ShowDialog() == true)
                    {
                        radiator.radiatorId = tempRadiator.radiatorId;
                        radiator.radiatorMaterial = tempRadiator.radiatorMaterial;
                        radiator.radiatorThickness = tempRadiator.radiatorThickness;
                        radiator.radiatorLength = tempRadiator.radiatorLength;
                        radiator.radiatorHeight = tempRadiator.radiatorHeight;
                        radiator.radiatorBrand = tempRadiator.radiatorBrand;
                        radiator.radiatorName = tempRadiator.radiatorName;
                        radiator.radiatorPrice = tempRadiator.radiatorPrice;
                        radiator.radiatorImageSource = tempRadiator.radiatorImageSource;

                        MyDbContext dbContext = new MyDbContext();
                        dbContext.UpdateEntity<Radiator>(tempRadiator);
                    }
                }, (obj) => SelectedRadiator != null && ListRadiator.Count > 0));
            }
        }

        private Radiator selectedRadiator;
        public Radiator SelectedRadiator
        {
            get
            {
                return selectedRadiator;
            }
            set
            {
                selectedRadiator = value;
                OnPropertyChanged("SelectedRadiator");
                EditRadiator.CanExecute(true);
            }
        }

        private RelayCommand addRadiator;
        public RelayCommand AddRadiator
        {
            get
            {
                return addRadiator ??
                 (addRadiator = new RelayCommand(obj =>
                 {
                     WindowNewRadiator wnRadiator = new WindowNewRadiator
                     {
                         Title = "Новый радиатор",
                     };
                     int maxIdRadiator = MaxRadiatorId() + 1;
                     Radiator boiler = new Radiator { radiatorId = maxIdRadiator };
                     wnRadiator.DataContext = boiler;
                     if (wnRadiator.ShowDialog() == true)
                     {
                         ListRadiator.Add(boiler);
                         MyDbContext dbContext = new MyDbContext();
                         dbContext.SaveEntity<Radiator>(dbContext, boiler);
                     }
                     SelectedRadiator = boiler;
                 }));
            }
        }

        private RelayCommand deleteRadiator;
        public RelayCommand DeleteRadiator
        {
            get
            {
                return deleteRadiator ??
                (deleteRadiator = new RelayCommand(obj =>
                {
                    Radiator radiator = SelectedRadiator;
                    MessageBoxResult result = MessageBox.Show("Удалить данные по радиатору: " + radiator.radiatorName, "Предупреждение", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                    if (result == MessageBoxResult.OK)
                    {
                        ListRadiator.Remove(radiator);
                        MyDbContext dbContext = new MyDbContext();
                        dbContext.DeleteEntityFromDatabase<Radiator>(radiator);
                    }
                }, (obj) => SelectedRadiator != null && ListRadiator.Count > 0));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
