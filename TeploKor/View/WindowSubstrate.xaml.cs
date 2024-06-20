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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TeploKor.Model;

namespace TeploKor.View
{
    public partial class WindowSubstrate : Window
    {
        public bool IsMenuOpen { get; set; } = false;
        public ObservableCollection<DataItems> DataItems { get; set; }
        public ObservableCollection<DataItems> FilteredDataItems { get; set; }

        public WindowSubstrate()
        {
            InitializeComponent();
            FilteredDataItems = new ObservableCollection<DataItems>(DataItems.Where(item => item.dataItemsSubstrateId.HasValue));
            myItemsControl.ItemsSource = FilteredDataItems;

            WindowMenu menuWindow = new WindowMenu();
            menuWindow.Closed += MenuWindow_Closed;
        }
        private void MenuWindow_Closed(object sender, EventArgs e)
        {
            IsMenuOpen = false;
        }

        private void Menu_Click(object sender, RoutedEventArgs e)
        {
            if (IsMenuOpen)
            {
                // закрываем окно меню
                WindowMenu menuWindow = new WindowMenu();
                menuWindow.Close();
                IsMenuOpen = false;
            }
            else
            {
                // открываем окно меню
                WindowMenu menuWindow = new WindowMenu();
                if (CurrentUser.IsEmployee == true)
                {
                    menuWindow.OrdersButton.Visibility = Visibility.Visible;
                    menuWindow.ProductsButton.Visibility = Visibility.Visible;
                }
                else if (CurrentUser.IsAdmin == true)
                {
                    menuWindow.OrdersButton.Visibility = Visibility.Visible;
                    menuWindow.ProductsButton.Visibility = Visibility.Visible;
                    menuWindow.EmployeesButton.Visibility = Visibility.Visible;
                }
                menuWindow.Show();
                IsMenuOpen = true;
            }
        }
    }
}
