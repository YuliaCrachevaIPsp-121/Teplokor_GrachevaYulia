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
    public partial class WindowClient : Window
    {
        private CurrentUser currentUser;
        public int? ClientId { get; private set; }
        public string FullName { get; private set; }
        public string Email { get; private set; }
        public string PhoneNumber { get; private set; }

        public WindowClient(CurrentUser currentUser)
        {
            InitializeComponent();
            this.currentUser = currentUser;
            if (currentUser.IsClient)
            {
                if (currentUser.UserId != null)
                {
                    using (MyDbContext db = new MyDbContext())
                    {
                        Client client = db.Client.FirstOrDefault(c => c.clientId == currentUser.UserId);
                        if (client != null)
                        {
                            ClientId = client.clientId;
                            FullNameTextBlock.Text = $"{client.clientSurname} {client.clientName}";
                            EmailTextBlock.Text = client.clientEmail;
                            PhoneNumberTextBlock.Text = client.clientContactNumber;

                            if (string.IsNullOrEmpty(Email))
                            {
                                PhoneNumberTextBlock.Visibility = Visibility.Collapsed;
                            }
                        }
                    }
                }
            }
            else if (currentUser.IsAdmin || currentUser.IsEmployee)
            {
                if (currentUser.UserId != null)
                {
                    using (MyDbContext db = new MyDbContext())
                    {
                        Employee employee = db.Employee.FirstOrDefault(c => c.employeeId == currentUser.UserId);
                        if (employee != null)
                        {
                            FullNameTextBlock.Text = $"{employee.employeeSurname} {employee.employeeName}";
                            EmailTextBlock.Text = employee.employeeEmail;
                            PhoneNumberTextBlock.Text = employee.employeeContactNumber;
                        }
                    }
                }
            }
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
        private void Cart_Click(object sender, RoutedEventArgs e)
        {
            WindowCart windowCart = new WindowCart(currentUser);
            windowCart.Show();
            this.Close();
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

        private void SetButtonsAvailability()
        {
            if (currentUser.IsClient)
            {
                HistoryButton.IsEnabled = true;
                DeleteButton.IsEnabled = true;
                EditButton.IsEnabled = true;
            }
            else
            {
                HistoryButton.IsEnabled = false;
                DeleteButton.IsEnabled = false;
                EditButton.IsEnabled = false;
            }
        }
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите удалить свой аккаунт?", "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                using (MyDbContext db = new MyDbContext())
                {
                    if (currentUser.IsClient)
                    {
                        Client client = db.Client.FirstOrDefault(c => c.clientId == currentUser.UserId);
                        if (client != null)
                        {
                            db.Client.Remove(client);
                            db.SaveChanges();
                        }
                    }
                    else if (currentUser.IsEmployee)
                    {
                        Employee employee = db.Employee.FirstOrDefault(e => e.employeeId == currentUser.UserId);
                        if (employee != null)
                        {
                            db.Employee.Remove(employee);
                            db.SaveChanges();
                        }
                    }
                }

                MessageBox.Show("Ваш аккаунт успешно удален.");
                WindowEntrance entrance = new WindowEntrance();
                entrance.Show();
                this.Close();
            }
        }
        private void History_Click(object sender, RoutedEventArgs e)
        {
            WindowHistoryСlient historyClient = new WindowHistoryСlient(currentUser);
            historyClient.Show();
            this.Close();
        }
    }
}