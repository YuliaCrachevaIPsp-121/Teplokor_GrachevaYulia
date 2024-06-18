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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TeploKor.Model;

namespace TeploKor.View
{
    public partial class WindowPipes : Window
    {
        public ObservableCollection<DataItems> DataItems { get; set; }
        public ObservableCollection<DataItems> FilteredDataItems { get; set; }

        public WindowPipes()
        {
            InitializeComponent();
            FilteredDataItems = new ObservableCollection<DataItems>(DataItems.Where(item => item.dataItemsPipesId.HasValue));
            myItemsControl.ItemsSource = FilteredDataItems;
        }
    }
}
