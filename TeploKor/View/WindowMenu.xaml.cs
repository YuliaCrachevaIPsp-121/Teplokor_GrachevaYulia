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

namespace TeploKor.View
{
    public partial class WindowMenu : Window
    {
        public WindowMenu()
        {
            InitializeComponent();
        }

        private void Profile_Click(object sender, RoutedEventArgs e)
        {
            WindowClient windowClient = new WindowClient();
            windowClient.Show();
            this.Close();
        }

        private void Boiler_Click (object sender, RoutedEventArgs e)
        {
            WindowBoiler windowBoiler = new WindowBoiler();
            windowBoiler.Show();
            this.Close();
        }
        private void Pipes_Click(object sender, RoutedEventArgs e)
        {
            WindowPipes windowPipes = new WindowPipes();
            windowPipes.Show();
            this.Close();
        }
        private void Radiator_Click(object sender, RoutedEventArgs e)
        {
            WindowRadiator windowRadiator = new WindowRadiator();
            windowRadiator.Show();
            this.Close();
        }
        private void Chimney_Click(object sender, RoutedEventArgs e)
        {
            WindowSubstrate windowSubstrate = new WindowSubstrate();
            windowSubstrate.Show();
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
    }
}
