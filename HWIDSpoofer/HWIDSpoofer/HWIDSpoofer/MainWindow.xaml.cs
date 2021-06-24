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
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HWIDSpoofer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //generates random string
        private static Random random = new Random();
        public static string randomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public MainWindow()
        {
            InitializeComponent();
        }

        private void CloseButton(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void SpoofButton(object sender, RoutedEventArgs e)
        {
            SpoofHWID spoof = new SpoofHWID(randomString(20));
            spoof.takeAction();

            ChangeVolumeID changeVolumeID = new ChangeVolumeID();
            changeVolumeID.changeVolumeID();

            MessageBox.Show("Spoofed!");
        }

        private void bringBack_button(object sender, RoutedEventArgs e)
        {
            RestoreHWIDKeys restoreHWIDKeys = new RestoreHWIDKeys(@"C:/HWID.txt");
            restoreHWIDKeys.restoreHWID();

            MessageBox.Show("HWID restored");
        }

        private void memorize_button(object sender, RoutedEventArgs e)
        {
            MemorizeHWID memorizeHWID = new MemorizeHWID(@"C:/HWID.txt");
            memorizeHWID.memorizeHWID();

            MessageBox.Show("HWID memorized!");
        }

        private void Hyperlink_RequestNavigate(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://discord.gg/C8sMqTRkDM");
        }
    }
}
