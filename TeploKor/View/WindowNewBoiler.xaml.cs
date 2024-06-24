using Microsoft.Win32;
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
    public partial class WindowNewBoiler : Window
    {
        private CurrentUser currentUser;
        public List<string> BoilerTypes { get; } = new List<string> { "Комби", "Отопительный", "Водонагреватель", "Тепловой насос" };
        public List<string> BoilerElectricTypes { get; } = new List<string> { "Газовый", "Электрический" };
        public List<string> BoilerInstallationLocations { get; } = new List<string> { "Настенное", "Напольное" };
        public List<string> BoilerTurbochargedTypes { get; } = new List<string> { "Есть", "Нет" };

        public WindowNewBoiler(CurrentUser currentUser)
        {
            InitializeComponent();
            this.currentUser = currentUser;
        }
        private void BtSave_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
        private void BtCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
        public Boiler boiler = new Boiler();
        public void SelectFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png)|*.jpg;*.jpeg;*.png";

            if (openFileDialog.ShowDialog() == true)
            {
                boiler.boilerImageSource = openFileDialog.FileName;
            }
        }
    }
}
