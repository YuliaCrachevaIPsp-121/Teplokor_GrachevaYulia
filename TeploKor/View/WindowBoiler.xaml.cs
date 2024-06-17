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
using TeploKor.Model;

namespace TeploKor.View
{
    public partial class WindowBoiler : Window
    {
        public WindowBoiler()
        {
            InitializeComponent();
        }
        private void Menu_Click(object sender, RoutedEventArgs e)
        {
            WindowMenu menu = new WindowMenu();

            // Проверяем роль пользователя и добавляем соответствующие кнопки в меню
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
    }
}
