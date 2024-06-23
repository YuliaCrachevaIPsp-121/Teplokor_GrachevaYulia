using OfficeOpenXml.Export.HtmlExport.StyleCollectors.StyleContracts;
using System;
using System.Collections.Generic;
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

        public WindowBuy(CurrentUser currentUser)
        {
            InitializeComponent();
            this.currentUser = currentUser;
            _order = new Order();
            DeliveryMethodComboBox.ItemsSource = new List<string> { "Самовывоз", "Доставка" };
            DeliveryMethodComboBox.SelectedItem = _order.orderDeliveryMethod;

            PaymentMethodComboBox.ItemsSource = new List<string> { "Наличные", "Картой" };
            PaymentMethodComboBox.SelectedItem = _order.orderPaymentMethod;
        }
        private void BtSave_Click(object sender, RoutedEventArgs e)
        {
            using (MyDbContext db = new MyDbContext())
            {
                // Создаем новый заказ
                Order order = new Order
                {
                    clientId = currentUser.UserId,
                    orderDeliveryMethod = DeliveryMethodComboBox.SelectedItem.ToString(),
                    orderPaymentMethod = PaymentMethodComboBox.SelectedItem.ToString(),
                    orderDeliveryAddressCountry = orderDeliveryAddressCountryTextBox.Text,
                    orderDeliveryAddressStreet = orderDeliveryAddressStreetTextBox.Text,
                    orderDeliveryAddressHome = orderDeliveryAddressHomeTextBox.Text,
                    orderDeliveryAddressNumberApartament = orderDeliveryAddressNumberApartamentTextBox.Text,
                    orderCost = GetCostFromCart(),
                    orderStatus = "Новый"
                };

                // Добавляем заказ в базу данных
                db.Order.Add(order);
                db.SaveChanges();

                // Получаем id только что созданного заказа
                int orderId = order.orderId;

                // Получаем все товары из корзины
                List<Cart> cartItems = GetCartItems();

                // Очищаем корзину
                ClearCart();

                // Закрываем окно
                this.Close();
            }
        }

        private decimal GetCostFromCart()
        {
            decimal cost = 0;

            // Получаем все товары из корзины
            List<Cart> cartItems = GetCartItems();

            // Вычисляем общую стоимость товаров в корзине
            foreach (Cart cartItem in cartItems)
            {
                cost +=  cartItem.cartPrice;
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
