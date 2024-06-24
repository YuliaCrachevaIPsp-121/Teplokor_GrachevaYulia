using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.IO;
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
    public partial class WindowBoilerInfo : Window
    {
        private CurrentUser currentUser;
        public WindowBoilerInfo(Boiler selectedBoiler, CurrentUser currentUser)
        {
            InitializeComponent();
            this.currentUser = currentUser;

            this.DataContext = selectedBoiler;
            this.selectedBoiler = selectedBoiler;
            using (MyDbContext db = new MyDbContext())
            {
                int boilerid = selectedBoiler.boilerId;

                // Получаем информацию о котле из базы данных
                var boiler = db.Boiler.FromSqlRaw($"SELECT * FROM Boiler WHERE boilerId = {boilerid}").FirstOrDefault();

                // Отображаем информацию о котле в TextBlock
                boilerNameTextBlock.Text = boiler.boilerName;
                boilerBrandTextBlock.Text = boiler.boilerBrand;
                boilerDescriptionTextBlock.Text = boiler.boilerDescription;
                boilerSquareTextBlock.Text = $"Площадь: {boiler.boilerSquare} м²";
                boilerKikowattTextBlock.Text = $"Мощность: {boiler.boilerKikowatt} кВт";
                boilerInstallationLocationTextBlock.Text = $"Место установки: {boiler.boilerInstallationLocation}";
                boilerTurbochargedOrNotTextBlock.Text = $"Турбонаддув: {boiler.boilerTurbochargedOrNot}";
                boilerTypeTextBlock.Text = $"Тип: {boiler.boilerType}";
                var imageSource = new BitmapImage(new Uri(boiler.boilerImageSource));
                boilerImage.Source = imageSource;
                boilerPriceTextBlock.Text = $"Цена: {boiler.boilerPrice}";
            }
            if (currentUser.IsClient)
            {
                ExcelButton.Visibility = Visibility.Collapsed;
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
            this.Close();
        }
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            WindowBoiler windowBoiler = new WindowBoiler(currentUser);
            windowBoiler.Show();
            this.Close();
        }
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
        private void Boiler_Click(object sender, RoutedEventArgs e)
        {
            WindowBoiler windowBoiler = new WindowBoiler( currentUser);
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
        private Boiler selectedBoiler;
        private void Excel_Click(object sender, RoutedEventArgs e)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using(ExcelPackage excelPackage = new ExcelPackage())
            {
                ExcelWorksheet excelWorksheet = excelPackage.Workbook.Worksheets.Add(boilerNameTextBlock.Text);

                // данные

                excelWorksheet.Cells["A1"].Value = boilerNameTextBlock.Text;

                excelWorksheet.Cells["A2"].Value = boilerBrandTextBlock.Text;

                excelWorksheet.Cells["A3"].Value = boilerDescriptionTextBlock.Text;

                excelWorksheet.Cells["A3"].Value = boilerSquareTextBlock.Text;

                excelWorksheet.Cells["A4"].Value = boilerKikowattTextBlock.Text;

                excelWorksheet.Cells["A5"].Value = boilerKikowattTextBlock.Text;

                excelWorksheet.Cells["A6"].Value = boilerInstallationLocationTextBlock.Text;

                excelWorksheet.Cells["A7"].Value = boilerTurbochargedOrNotTextBlock.Text;

                excelWorksheet.Cells["A8"].Value = boilerTypeTextBlock.Text;

                excelWorksheet.Cells["A10"].Value = "Заказы";
                using (MyDbContext db = new MyDbContext()) // добавляем объявление и инициализацию переменной db
                {
                    int boilerid = selectedBoiler.boilerId;
                    var orders = db.Order
                .Where(o => o.BoilerId == boilerid)
                .ToList();
                    var name = db.Client.FromSqlRaw(
                        $"SELECT cl.clientSurname + ' ' + cl.clientName + ' ' + cl.clientPatronymic " +
                        $"FROM Order ord " +
                        $"JOIN Client cl on cl.clientId = ord.clientId " +
                        $"WHERE ord.boilerId = {selectedBoiler.boilerId}");
                    // добавляем данные заказов в таблицу Excel
                    int rowIndex = 11;
                    if (orders.Any())
                    {
                        foreach (var order in orders)
                        {
                            excelWorksheet.Cells[rowIndex, 1].Value = name;
                            excelWorksheet.Cells[rowIndex, 2].Value = order.orderDeliveryMethod;
                            excelWorksheet.Cells[rowIndex, 3].Value = order.orderPaymentMethod;
                            excelWorksheet.Cells[rowIndex, 4].Value = order.orderDeliveryAddressCountry;
                            excelWorksheet.Cells[rowIndex, 5].Value = order.orderDeliveryAddressStreet;
                            excelWorksheet.Cells[rowIndex, 6].Value = order.orderDeliveryAddressHome;
                            excelWorksheet.Cells[rowIndex, 7].Value = order.orderDeliveryAddressNumberApartament;
                            excelWorksheet.Cells[rowIndex, 8].Value = order.orderCost;
                            excelWorksheet.Cells[rowIndex, 9].Value = order.orderStatus;

                            using (ExcelRange range = excelWorksheet.Cells[rowIndex, 1, rowIndex, 9])
                            {
                                range.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                range.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                range.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                            }

                            rowIndex++;
                        }
                    }
                    else
                    {
                        excelWorksheet.Cells[rowIndex, 1].Value = "Заказов нет";
                    }
                }

                // стиль

                using (ExcelRange range = excelWorksheet.Cells["A1:G1"])
                {
                    range.Merge = true;
                    range.Style.Font.Bold = true;
                    range.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    range.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    range.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                }
                using (ExcelRange range = excelWorksheet.Cells["A2:G2"])
                {
                    range.Merge = true;
                    range.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    range.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    range.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                }
                using (ExcelRange range = excelWorksheet.Cells["A3:G3"])
                {
                    range.Merge = true;
                    range.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    range.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    range.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                }
                using (ExcelRange range = excelWorksheet.Cells["A4:G4"])
                {
                    range.Merge = true;
                    range.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    range.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    range.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                }
                using (ExcelRange range = excelWorksheet.Cells["A5:G5"])
                {
                    range.Merge = true;
                    range.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    range.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    range.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                }
                using (ExcelRange range = excelWorksheet.Cells["A6:G6"])
                {
                    range.Merge = true;
                    range.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    range.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    range.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                }
                using (ExcelRange range = excelWorksheet.Cells["A7:G7"])
                {
                    range.Merge = true;
                    range.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    range.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    range.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                }
                using (ExcelRange range = excelWorksheet.Cells["A8:G8"])
                {
                    range.Merge = true;
                    range.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    range.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    range.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                }
                using (ExcelRange range = excelWorksheet.Cells["A9:G9"])
                {
                    range.Merge = true;
                    range.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    range.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    range.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                }
                using (ExcelRange range = excelWorksheet.Cells["A10:G10"])
                {
                    range.Merge = true;
                    range.Style.Font.Bold = true;
                    range.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    range.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    range.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                }

                // сохраняем файл Excel
                FileInfo fileInfo = new FileInfo(@"D:\TeploKor\TeploKor\Excel\boiler.xlsx");
                excelPackage.SaveAs(fileInfo);
                MessageBox.Show("Отчет успешно создан", "Успех", MessageBoxButton.OK);
            }
        }
    }
}
