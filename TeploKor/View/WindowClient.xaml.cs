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
        public string FullName { get; private set; }
        public string Email { get; private set; }
        public string PhoneNumber { get; private set; }
        private int? clientId;
        private int? employeeId;
        public WindowClient()
        {
            InitializeComponent();

            this.clientId = clientId;
            this.employeeId = employeeId;

            if (clientId.HasValue)
            {
                using (MyDbContext db = new MyDbContext())
                {
                    Client client = db.Client.FirstOrDefault(c => c.clientId == clientId.Value);
                    if (client != null)
                    {
                        FullName = $"{client.clientSurname} {client.clientName}";
                        Email = client.clientEmail;
                        PhoneNumber = client.clientContactNumber;

                        if (string.IsNullOrEmpty(Email))
                        {
                            PhoneNumberTextBlock.Visibility = Visibility.Collapsed;
                        }
                    }
                }
            }
            else if (employeeId.HasValue)
            {
                using (MyDbContext db = new MyDbContext())
                {
                    Employee employee = db.Employee.FirstOrDefault(c => c.employeeId == employeeId.Value);
                    if (employee != null)
                    {
                        FullName = $"{employee.employeeSurname} {employee.employeeName}";
                        Email = employee.employeeEmail;
                        PhoneNumber = employee.employeeContactNumber;
                    }
                }
            }
        }
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите удалить свой аккаунт?", "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                using (MyDbContext db = new MyDbContext())
                {
                    if (clientId.HasValue)
                    {
                        Client client = db.Client.FirstOrDefault(c => c.clientId == clientId.Value);
                        if (client != null)
                        {
                            db.Client.Remove(client);
                            db.SaveChanges();
                        }
                    }
                    else if (employeeId.HasValue)
                    {
                        Employee employee = db.Employee.FirstOrDefault(e => e.employeeId == employeeId.Value);
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
            WindowHistoryСlient historyClient = new WindowHistoryСlient(clientId);
            historyClient.Show();
            this.Close();
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
