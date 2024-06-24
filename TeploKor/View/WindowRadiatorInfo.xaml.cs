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
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml.Style;
using OfficeOpenXml;
using System.IO;

namespace TeploKor.View
{
    public partial class WindowRadiatorInfo : Window
    {
        private CurrentUser currentUser;
        private Radiator selectedRadiator;
        public WindowRadiatorInfo(Radiator selectedRadiator, CurrentUser currentUser)
        {
            InitializeComponent();
            this.currentUser = currentUser;
            this.DataContext = selectedRadiator;
            this.selectedRadiator = selectedRadiator;
            using (MyDbContext db = new MyDbContext())
            {
                int radiatorId = selectedRadiator.radiatorId;

                // Получаем информацию о котле из базы данных
                var radiator = db.Radiator.FromSqlRaw($"SELECT * FROM Radiator WHERE radiatorId = {radiatorId}").FirstOrDefault();

                // Отображаем информацию о котле в TextBlock
                radiatorNameTextBlock.Text = radiator.radiatorName;
                radiatorBrandTextBlock.Text = radiator.radiatorBrand;
                radiatorMaterialTextBlock.Text = radiator.radiatorMaterial;
                radiatorThicknessTextBlock.Text = $"Площадь: {radiator.radiatorThickness} м²";
                radiatorLengthTextBlock.Text = $"Мощность: {radiator.radiatorLength} кВт";
                radiatorHeightTextBlock.Text = $"Место установки: {radiator.radiatorHeight}";
                radiatorPriceTextBlock.Text = $"Турбонаддув: {radiator.radiatorPrice}";
                var imageSource = new BitmapImage(new Uri(radiator.radiatorImageSource));
                radiatorImage.Source = imageSource;
                radiatorPriceTextBlock.Text = $"Цена: {radiator.radiatorPrice}";
            }
            if (currentUser.IsClient)
            {
                ExcelButton.Visibility = Visibility.Collapsed;
            }
        }
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            WindowRadiator windowRadiator = new WindowRadiator(currentUser);
            windowRadiator.Show();
            this.Close();
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
        private void Cart_Click(object sender, RoutedEventArgs e)
        {
            WindowCart windowCart = new WindowCart(currentUser);
            windowCart.Show();
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
        private void Excel_Click(object sender, RoutedEventArgs e)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (ExcelPackage excelPackage = new ExcelPackage())
            {
                ExcelWorksheet excelWorksheet = excelPackage.Workbook.Worksheets.Add(radiatorNameTextBlock.Text);

                // данные

                excelWorksheet.Cells["A1"].Value = radiatorNameTextBlock.Text;

                excelWorksheet.Cells["A2"].Value = radiatorHeightTextBlock.Text;

                excelWorksheet.Cells["A3"].Value = radiatorMaterialTextBlock.Text;

                excelWorksheet.Cells["A4"].Value = radiatorThicknessTextBlock.Text;

                excelWorksheet.Cells["A5"].Value = radiatorPriceTextBlock.Text;
                excelWorksheet.Cells["A6"].Value = radiatorLengthTextBlock.Text;

                excelWorksheet.Cells["A7"].Value = "Заказы";
                using (MyDbContext db = new MyDbContext()) // добавляем объявление и инициализацию переменной db
                {
                    int radiatorId = selectedRadiator.radiatorId;
                    var orders = db.Order
                .Where(o => o.RadiatorId == radiatorId)
                .ToList();
                    var name = db.Client.FromSqlRaw(
                        $"SELECT cl.clientSurname + ' ' + cl.clientName + ' ' + cl.clientPatronymic " +
                        $"FROM Order ord " +
                        $"JOIN Client cl on cl.clientId = ord.clientId " +
                        $"WHERE ord.RadiatorId = {selectedRadiator.radiatorId}");
                    // добавляем данные заказов в таблицу Excel
                    int rowIndex = 8;
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

                // сохраняем файл Excel
                FileInfo fileInfo = new FileInfo(@"D:\TeploKor\TeploKor\Excel\radiator.xlsx");
                excelPackage.SaveAs(fileInfo);
                MessageBox.Show("Отчет успешно создан", "Успех", MessageBoxButton.OK);
            }
        }
    }
}
