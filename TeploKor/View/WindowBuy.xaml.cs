using OfficeOpenXml.Export.HtmlExport.StyleCollectors.StyleContracts;
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
    public partial class WindowBuy : Window
    {
        private Order _order;
        private CurrentUser currentUser;
        private Cart CurrentCart;
        public ObservableCollection<DataItemsCart> FilteredDataItemsCart { get; set; }
        public WindowBuy(CurrentUser currentUser, ObservableCollection<DataItemsCart> FilteredDataItemsCart)
        {
            InitializeComponent();
            this.currentUser = currentUser;
            _order = new Order();
            DeliveryMethodComboBox.ItemsSource = new List<string> { "Самовывоз", "Доставка" };
            DeliveryMethodComboBox.SelectedItem = _order.orderDeliveryMethod;

            PaymentMethodComboBox.ItemsSource = new List<string> { "Наличные", "Картой" };
            PaymentMethodComboBox.SelectedItem = _order.orderPaymentMethod;

            this.FilteredDataItemsCart = FilteredDataItemsCart;
        }
        private void BtSave_Click(object sender, RoutedEventArgs e)
        {
            //Order newOrder = new Order();
            //newOrder.orderDeliveryMethod = DeliveryMethodComboBox.SelectedItem.ToString();
            //newOrder.orderPaymentMethod = PaymentMethodComboBox.SelectedItem.ToString();
            //newOrder.orderDeliveryAddressCountry = orderDeliveryAddressCountryTextBox.Text;
            //newOrder.orderDeliveryAddressStreet = orderDeliveryAddressStreetTextBox.Text;
            //newOrder.orderDeliveryAddressHome = orderDeliveryAddressHomeTextBox.Text;
            //newOrder.orderDeliveryAddressNumberApartament = orderDeliveryAddressNumberApartamentTextBox.Text;
            //newOrder.clientId = currentUser.UserId; // Предполагая, что currentUser содержит текущего пользователя
            //newOrder.orderStatus = "Создан";

            //// Получаем id радиатора или бойлера из корзины
            //int? productId = null;
            //DataItemsCart cartItem = null;
            //if (FilteredDataItemsCart.Count == 1)
            //{
            //    cartItem = FilteredDataItemsCart.First();
            //    if (cartItem.d == typeof(Radiator))
            //    {
            //        productId = cartItem.radiatorId;
            //    }
            //    else if (cartItem.dataItemsCartType == typeof(Boiler))
            //    {
            //        productId = cartItem.boilerId;
            //    }
            //}

            //// Сохраняем id радиатора или бойлера в заказе
            //if (productId.HasValue && cartItem != null)
            //{
            //    newOrder.RadiatorId = cartItem.dataItemsCartType == typeof(Radiator) ? productId.Value : (int?)null;
            //    newOrder.BoilerId = cartItem.dataItemsCartType == typeof(Boiler) ? productId.Value : (int?)null;
            //}

            //// Здесь вы можете добавить логику для подсчета стоимости заказа (orderCost)
            //newOrder.orderCost = GetCostFromCart();

            //// Сохраняем заказ в базе данных
            //using (MyDbContext db = new MyDbContext())
            //{
            //    db.Order.Add(newOrder);
            //    db.SaveChanges();
            //}

            ClearCart();

            // Закрываем текущее окно
            this.Close();
        }

        private decimal GetCostFromCart()
        {
            decimal cost = 0;

            // Получаем все товары из корзины
            List<Cart> cartItems = GetCartItems();

            // Вычисляем общую стоимость товаров в корзине
            foreach (Cart cartItem in cartItems)
            {
                cost += cartItem.cartPrice;
            }

            return cost;
        }

        private List<Cart> GetCartItems()
        {
            using (MyDbContext db = new MyDbContext())
            {
                // Получаем все товары из корзины
                return db.Cart
                    .Where(ci => ci.clientId == currentUser.UserId)
                    .ToList();
            }
        }

        private void ClearCart()
        {
            using (MyDbContext db = new MyDbContext())
            {
                // Получаем текущую корзину
                CurrentCart = db.Cart.FirstOrDefault(c => c.clientId == currentUser.UserId);
                if (CurrentCart != null)
                {
                    // Удаляем все товары из корзины
                    db.Cart.RemoveRange(db.Cart.Where(ci => ci.clientId == CurrentCart.clientId));
                    db.SaveChanges();
                }
            }
        }

        private void BtCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
