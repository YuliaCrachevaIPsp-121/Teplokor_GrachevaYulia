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

namespace TeploKor.View
{
    public partial class WindowRadiator : Window
    {
        private CurrentUser currentUser;
        public ObservableCollection<DataItems> DataItems { get; set; }
        public ObservableCollection<DataItems> FilteredDataItems { get; set; }

        public WindowRadiator()
        {
            InitializeComponent();
            using (var context = new MyDbContext())
            {
                var radiators = context.Radiator.ToList();

                foreach (var radiator in radiators)
                {
                    var dataItem = new DataItems
                    {
                        dataItemsName = radiator.radiatorName,
                        dataItemsPrice = radiator.radiatorPrice,
                        dataItemsImageSource = radiator.radiatorImageSource,
                        dataItemsRadiatorId = radiator.radiatorId
                    };

                    DataItems.Add(dataItem);
                }
            }
            FilteredDataItems = new ObservableCollection<DataItems>(DataItems.Where(item => item.dataItemsRadiatorId.HasValue));
            myItemsControl.ItemsSource = FilteredDataItems;
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
        private void Cart_Click(object sender, RoutedEventArgs e)
        {
            WindowCart windowCart = new WindowCart(currentUser);
            windowCart.Show();
            this.Close();
        }
        private void Boiler_Click(object sender, RoutedEventArgs e)
        {
            WindowBoiler windowBoiler = new WindowBoiler(currentUser);
            windowBoiler.Show();
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

        public void MoreInfo_Click(object sender, RoutedEventArgs e)
        {
            int selectedDataItemsId = (int)((Button)sender).Tag; // Считываем Tag сразу
            string connectionString = "Data Source=D:\\TeploKor\\TeploKor\\BD\\TeploKor.db";
            MyDbContext db = new MyDbContext();
            List<Radiator> radiator = MyDbContext.GetEntities<Radiator>(connectionString, "SELECT * FROM Radiator");
            if (radiator != null)
            {
                var matchingRadiator = radiator.FirstOrDefault(cp => cp.radiatorId == selectedDataItemsId);
                if (matchingRadiator != null)
                {
                    WindowRadiatorInfo infoWindow = new WindowRadiatorInfo(matchingRadiator, currentUser);
                    infoWindow.Show();
                    this.Close();
                }
            }
        }
        public CurrentUser CurrentUser { get; private set; } = new CurrentUser();
        private void AddToCart_Click(object sender, RoutedEventArgs e)
        {
            string connectionString = "Data Source=D:\\TeploKor\\TeploKor\\BD\\TeploKor.db";
            Button button = (Button)sender;

            DataItems item = (DataItems)button.DataContext;

            int? radiatorId = 0;
            int itemId = item.dataItemsId;
            if (itemId != null)
            {
                radiatorId = item.dataItemsBoilerId;
            }

            try
            {
                using (var context = new MyDbContext())
                {
                    var radiator = context.Radiator.FirstOrDefault(b => b.radiatorId == radiatorId);

                    if (radiator != null)
                    {
                        var cart = new Cart();
                        cart.cartName = radiator.radiatorName;
                        cart.cartPrice = radiator.radiatorPrice;
                        cart.cartImageSource = radiator.radiatorImageSource;
                        cart.clientId = CurrentUser.UserId;

                        context.Cart.Add(cart);
                        context.SaveChanges();
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Возникла ошибка при добавлении в корзину: " + ex.Message);
            }
        }
    }
}
