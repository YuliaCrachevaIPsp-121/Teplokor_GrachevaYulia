using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TeploKor.Helper;
using TeploKor.Model;
using System.Linq;

namespace TeploKor.View
{
    public partial class WindowBoiler : Window
    {
        private CurrentUser currentUser;
        public ObservableCollection<DataItems> DataItems { get; set; }
        public ObservableCollection<DataItems> FilteredDataItems { get; set; }
        public ObservableCollection<Boiler> ListBoiler { get; set; }

        public WindowBoiler(CurrentUser currentUser)
        {
            InitializeComponent();
            string connectionString = "Data Source=D:\\TeploKor\\TeploKor\\BD\\TeploKor.db";

            List<Boiler> boiler = MyDbContext.GetEntities<Boiler>(connectionString, "SELECT * FROM Boiler");
            ListBoiler = new ObservableCollection<Boiler>(boiler);

            DataItems = new ObservableCollection<DataItems>();

            using (var context = new MyDbContext())
            {
                var boilers = context.Boiler.ToList();

                foreach (var boiler1 in boilers)
                {
                    var dataItem = new DataItems
                    {
                        dataItemsName = boiler1.boilerName,
                        dataItemsPrice = boiler1.boilerPrice,
                        dataItemsImageSource = boiler1.boilerImageSource,
                        dataItemsBoilerId = boiler1.boilerId
                    };

                    DataItems.Add(dataItem);
                }
            }

            FilteredDataItems = new ObservableCollection<DataItems>(DataItems.Where(item => item.dataItemsBoilerId.HasValue));
            myItemsControl.ItemsSource = FilteredDataItems;
            this.currentUser = currentUser;
        }
        private void Menu_Click(object sender, RoutedEventArgs e)
        {
            if (MenuBorder.Opacity == 0)
            {
                // делаем меню видимым
                MenuBorder.Opacity = 1;
            }
            else
            {
                // делаем меню невидимым
                MenuBorder.Opacity = 0;
            }
        }

        private void Profile_Click(object sender, RoutedEventArgs e)
        {
            WindowClient windowClient = new WindowClient(currentUser);
            windowClient.Show();
            this.Close();
        }
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
        private void Boiler_Click(object sender, RoutedEventArgs e)
        {
            WindowBoiler windowBoiler = new WindowBoiler( currentUser);
            windowBoiler.Show();
            this.Close();
        }
        private void Cart_Click(object sender, RoutedEventArgs e)
        {
            WindowCart windowCart = new WindowCart( currentUser );
            windowCart.Show();
            this.Close();
        }
        private void Radiator_Click(object sender, RoutedEventArgs e)
        {
            WindowRadiator windowRadiator = new WindowRadiator();
            windowRadiator.Show();
            this.Close();
        }
        private void Orders_Click(object sender, RoutedEventArgs e)
        {
            WindowHistory windowHistory = new WindowHistory();
            windowHistory.Show();
            this.Close();
        }
        private void Products_Click(object sender, RoutedEventArgs e)
        {
            WindowEmployeeControl windowEmployeeControl = new WindowEmployeeControl();
            windowEmployeeControl.Show();
            this.Close();
        }
        private void Employees_Click(object sender, RoutedEventArgs e)
        {
            WindowEmployee windowEmployee = new WindowEmployee();
            windowEmployee.Show();
            this.Close();
        }
        private void Product_Click(object sender, RoutedEventArgs e)
        {
            WindowEmployeeControl windowEmployeeControl = new WindowEmployeeControl();
            windowEmployeeControl.Show();
            this.Close();
        }

        private void AddToCart_Click(object sender, RoutedEventArgs e)
        {
            string connectionString = "Data Source=D:\\TeploKor\\TeploKor\\BD\\TeploKor.db";
            Button button = (Button)sender;

            DataItems item = (DataItems)button.DataContext;

            int? boilerId = 0;
            int itemId = item.dataItemsId;
            if (itemId != null)
            {
                boilerId = item.dataItemsBoilerId;
            }

            try
            {
                using (var context = new MyDbContext())
                {
                    var boiler = context.Boiler.FirstOrDefault(b => b.boilerId == boilerId);

                    if (boiler != null)
                    {
                        var cart = new Cart();
                        cart.cartName = boiler.boilerName;
                        cart.cartPrice = boiler.boilerPrice;
                        cart.cartImageSource = boiler.boilerImageSource;
                        cart.clientId = currentUser.UserId; 

                        context.Cart.Add(cart);
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    MessageBox.Show("Возникла ошибка при добавлении в корзину: " + ex.InnerException.Message);
                }
                else
                {
                    MessageBox.Show("Возникла ошибка при добавлении в корзину: " + ex.Message);
                }
            }
        }

        public void MoreInfo_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            if (button != null && button.Tag != null)
            {
                int selectedDataItemsId = (int)button.Tag; // Считываем Tag сразу
                var matchingDataItem = ListBoiler.FirstOrDefault(cp => cp.boilerId == selectedDataItemsId);
                if (matchingDataItem != null)
                {
                        WindowBoilerInfo infoWindow = new WindowBoilerInfo(matchingDataItem, currentUser);
                        infoWindow.Show();
                        this.Close();
                }
            }
        }
    }
}