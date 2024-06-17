﻿using System;
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
    public partial class WindowEntrance : Window
    {
        public WindowEntrance()
        {
            InitializeComponent();
        }

        private void Registration_Click (object sender, RoutedEventArgs e)
        {
            WindowRegistration windowRegistration = new WindowRegistration();
            windowRegistration.Show();
            this.Close();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string login = LoginTextBox.Text.Trim();
            string password = PasswordTextBox.Text.Trim();

            using (MyDbContext db = new MyDbContext())
            {
                // Проверяем, существует ли пользователь в таблице Client
                Client client = db.Client.FirstOrDefault(c => c.clientEmail == login && c.clientPassword == password);
                if (client != null)
                {
                    MessageBox.Show($"Добро пожаловать, {client.clientSurname} {client.clientName}!");
                    WindowBoiler windowBoiler = new WindowBoiler();
                    windowBoiler.Show();
                    this.Close();
                    return;
                }

                // Проверяем, существует ли пользователь в таблице Employee
                Employee employee = db.Employee.FirstOrDefault(e => e.employeeLogin == login && e.employeePassword == password);
                if (employee != null)
                {
                    if (employee.IsAdmin)
                    {
                        MessageBox.Show($"Добро пожаловать, администратор {employee.employeeSurname} {employee.employeeName}!");
                        WindowEmployee windowEmployee = new WindowEmployee();
                        windowEmployee.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show($"Добро пожаловать, сотрудник {employee.employeeSurname} {employee.employeeName}!");
                        WindowEmployeeControl windowEmployee = new WindowEmployeeControl();
                        windowEmployee.Show();
                        this.Close();
                    }
                    return;
                }
                else
                {
                    MessageBox.Show("Неправильный логин или пароль.");
                    LoginTextBox.Clear();
                    PasswordTextBox.Clear();
                }
            }
        }
    }
}
