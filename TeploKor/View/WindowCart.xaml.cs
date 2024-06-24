using Microsoft.Data.Sqlite;
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
    public partial class WindowCart : Window
    {
        private CurrentUser currentUser;
        public ObservableCollection<DataItemsCart> DataItemsCart { get; set; }
        public ObservableCollection<DataItemsCart> FilteredDataItemsCart { get; set; }
        public ObservableCollection<Cart> ListCart { get; set; }
        public WindowCart(CurrentUser currentUser)
        {
            InitializeComponent();
            this.currentUser = currentUser;
            string connectionString = "Data Source=D:\\TeploKor\\TeploKor\\BD\\TeploKor.db";

            List<Cart> cart = MyDbContext.GetEntities<Cart>(connectionString, "SELECT * FROM Cart");
            ListCart = new ObservableCollection<Cart>(cart);

            DataItemsCart = new ObservableCollection<DataItemsCart>();

            using (var context = new MyDbContext())
            {
                var carts = context.Cart.ToList();

                foreach (var Cart in carts)
                {
                    var dataItemsCart = new DataItemsCart
                    {
                        dataItemsCartName = Cart.cartName,
                        dataItemsCartPrice = Cart.cartPrice,
                        dataItemsCartImageSource = Cart.cartImageSource,
                        clientId = Cart.clientId,
                        cartId = Cart.cartId
                    };

                    DataItemsCart.Add(dataItemsCart);
                }
            }

            FilteredDataItemsCart = new ObservableCollection<DataItemsCart>(DataItemsCart.Where(item => item.clientId == currentUser.UserId));

            myItemsControl.ItemsSource = FilteredDataItemsCart;
            //using (MyDbContext db = new MyDbContext())
            //{
            //    try
            //    {
            //        var prodResult = db.Cart.FromSqlRaw($"SELECT IFNULL(COUNT(*), 0) FROM Cart WHERE clientId = '{currentUser.UserId}'").FirstOrDefault();
            //        int prod = Convert.ToInt32(prodResult);

            //        totalProduct.Text = $"Всего {prod} товаров ";

            //        var totalResult = db.Cart.FromSqlRaw($"SELECT IFNULL(SUM(cartPrice), 0) FROM Cart WHERE clientId = '{currentUser.UserId}'").FirstOrDefault();
            //        decimal total = Convert.ToDecimal(totalResult);

            //        totalPrice.Text = $"На сумму {total} рублей";
            //    }
            //    catch (SqliteException ex)
            //    {
            //        MessageBox.Show($"Произошла ошибка базы данных: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            //        return;
            //    }
            //    catch (InvalidCastException ex)
            //    {
            //        MessageBox.Show($"Произошла ошибка преобразования типов: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            //        return;
            //    }
            //    catch (FormatException ex)
            //    {
            //        MessageBox.Show($"Произошла ошибка форматирования: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            //        return;
            //    }
            //}

        }
        private void Buy_Click(object sender, RoutedEventArgs e)
        {
            if (FilteredDataItemsCart.Count == 0)
            {
                MessageBox.Show("Корзина пуста", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            WindowBuy windowBuy = new WindowBuy(currentUser, FilteredDataItemsCart);
            windowBuy.Show();
            this.Close();
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
            if (currentUser.IsEmployee)
            {
                CartButton.Visibility = Visibility.Collapsed;
                EmployeesButton.Visibility = Visibility.Collapsed;
            }
            else if (currentUser.IsAdmin)
            {

                CartButton.Visibility = Visibility.Collapsed;
            }
            else
            {
                EmployeesButton.Visibility = Visibility.Collapsed;
                ProductButton.Visibility = Visibility.Collapsed;
            }
        }

        private void Profile_Click(object sender, RoutedEventArgs e)
        {
            WindowClient windowClient = new WindowClient(currentUser);
            windowClient.Show();
            this.Close();
        }
        private void Cart_Click(object sender, RoutedEventArgs e)
        {
            WindowCart windowCart = new WindowCart(currentUser);
            windowCart.Show();
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
            WindowRadiator windowRadiator = new WindowRadiator(currentUser);
            windowRadiator.Show();
            this.Close();
        }
        private void Products_Click(object sender, RoutedEventArgs e)
        {
            WindowEmployeeControl windowEmployeeControl = new WindowEmployeeControl(currentUser);
            windowEmployeeControl.Show();
            this.Close();
        }
        private void Employees_Click(object sender, RoutedEventArgs e)
        {
            WindowEmployee windowEmployee = new WindowEmployee(currentUser);
            windowEmployee.Show();
            this.Close();
        }
        private void Product_Click(object sender, RoutedEventArgs e)
        {
            WindowEmployeeControl windowEmployeeControl = new WindowEmployeeControl(currentUser);
            windowEmployeeControl.Show();
            this.Close();
        }

        private void RemoveFromCart_Click(object sender, RoutedEventArgs e)
        {
            
            Button button = (Button)sender;

            DataItemsCart itemCart = (DataItemsCart)button.DataContext;
            Cart item = new Cart();
            item.cartId = itemCart.cartId;

            using (MyDbContext db = new MyDbContext())
            {
                db.DeleteEntityFromDatabase(item);
            }
            FilteredDataItemsCart.Remove(itemCart);
        }
    }
}
