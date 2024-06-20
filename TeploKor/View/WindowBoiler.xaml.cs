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
    public partial class WindowBoiler : Window
    {
        public ObservableCollection<DataItems> DataItems { get; set; }
        public ObservableCollection<DataItems> FilteredDataItems { get; set; }

        public WindowBoiler()
        {
            InitializeComponent();
            FilteredDataItems = new ObservableCollection<DataItems>(DataItems.Where(item => item.dataItemsBoilerId.HasValue));
            myItemsControl.ItemsSource = FilteredDataItems;
        }
        private void Menu_Click(object sender, RoutedEventArgs e)
        {
            WindowMenu menu = new WindowMenu();

            if (CurrentUser.IsEmployee == true)
            {
                menu.OrdersButton.Visibility = Visibility.Visible;
                menu.ProductsButton.Visibility = Visibility.Visible;
            }
            else if (CurrentUser.IsAdmin == true)
            {
                menu.OrdersButton.Visibility = Visibility.Visible;
                menu.ProductsButton.Visibility = Visibility.Visible;
                menu.EmployeesButton.Visibility = Visibility.Visible;
            }

            menu.Show();
        }
        public CurrentUser CurrentUser { get; private set; } = new CurrentUser();
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
                        cart.clientId = CurrentUser.UserId; // здесь предполагается, что есть доступ к текущему пользователю

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