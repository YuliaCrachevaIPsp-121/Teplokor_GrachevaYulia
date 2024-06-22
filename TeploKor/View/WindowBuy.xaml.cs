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
    public partial class WindowBuy : Window
    {
        public List<string> DeliveryMethods { get; } = new List<string> { "Самовывоз", "Доставка" };

        public List<string> PaymentMethods { get; } = new List<string> { "Наличные", "Картой" };

        public WindowBuy()
        {
            InitializeComponent();
        }
    }
}
