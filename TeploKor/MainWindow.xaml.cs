using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using TeploKor.Helper;
using TeploKor.View;

namespace TeploKor
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            WindowState = WindowState.Normal;

            var dbContext = new MyDbContext();
            dbContext.Database.EnsureCreated();

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(3);
            timer.Tick += new EventHandler((sender, e) =>
            {
                DispatcherTimer t = (DispatcherTimer)sender;
                t.Stop();

                WindowEntrance entrance = new WindowEntrance();
                entrance.Show();

                this.Close();
            });
            timer.Start();
        }
    }
}