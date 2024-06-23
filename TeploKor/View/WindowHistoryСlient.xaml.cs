using Microsoft.EntityFrameworkCore;
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
    public partial class WindowHistoryСlient : Window
    {
        private CurrentUser currentUser;
        public ObservableCollection<DataItemsHistory> DataItemsHistory { get; set; }
        public ObservableCollection<DataItemsHistory> FilteredDataItemsHistory { get; set; }
        public ObservableCollection<Order> ListOrder { get; set; }
        public WindowHistoryСlient(CurrentUser currentUser)
        {
            InitializeComponent();
            //List<Order> orders = MyDbContext.GetEntities<Order>(connectionString, "SELECT * FROM Order");
            //ListOrder = new ObservableCollection<Order>(orders);

            //DataItemsHistory = new ObservableCollection<DataItemsHistory>();

            //using (var context = new MyDbContext())
            //{
            //    var order = context.Order.ToList();
            //    var nameClient = context.Order.FromSqlRaw(
            //                $"SELECT cl.clientSurname + ' ' + cl.clientName + ' ' + cl.clientPatronymic" +
            //                $"FROM Order ord " +
            //                $"JOIN Client cl on ord.clientId = cl.clientId");
            //    var nameProduct = context.Order.FromSqlRaw(
            //        $"SELECT ");
            //    foreach (var Order in order)
            //    {
            //        var dataItems = new DataItemsHistory
            //        {
            //            dataItemsHistoryName = Order.,
            //            dataItemsCartPrice = Order.cartPrice,
            //            dataItemsCartImageSource = Order.cartImageSource,
            //            clientId = Order.clientId
            //        };

            //        DataItemsCart.Add(dataItemsCart);
            //    }
            //}

            //FilteredDataItemsCart = new ObservableCollection<DataItemsCart>(DataItemsCart.Where(item => item.clientId == currentUser.UserId));

            //myItemsControl.ItemsSource = FilteredDataItemsCart;
            //using (MyDbContext db = new MyDbContext())
            //{
            //    var prod = db.Cart.FromSqlRaw(
            //        $"SELECT COUNT(*) FROM Cart WHERE clientId = '{currentUser.UserId}'")
            //        .FirstOrDefault();
            //    totalProduct.Text = $"Всего {prod} товаров ";
            //    var total = db.Cart.FromSqlRaw($"SELECT SUM(cartPrice) FROM Cart WHERE clientId = '{currentUser.UserId}'")
            //        .FirstOrDefault();
            //    totalPrice.Text = $"На сумму {total} рублей";
            //}
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
        private void Cart_Click(object sender, RoutedEventArgs e)
        {
            WindowCart windowCart = new WindowCart(currentUser);
            windowCart.Show();
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
        private void Excel_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
