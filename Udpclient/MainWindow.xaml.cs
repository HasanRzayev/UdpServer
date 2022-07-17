
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Udpclient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        public Stream MyStream { get; set; }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var Client = new TcpClient();
            Client.Connect("192.168.1.5", 62100);

            MyStream = Client.GetStream();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            try
            {
                byte[] buffer = new byte[1000000];

                MyStream.Read(buffer, 0, buffer.Length);


                var image = new BitmapImage();
                using (var mem = new MemoryStream(buffer))
                {
                    mem.Position = 0;
                    image.BeginInit();
                    image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                    image.CacheOption = BitmapCacheOption.OnLoad;
                    image.UriSource = null;
                    image.StreamSource = mem;
                    image.EndInit();
                }
                image.Freeze();
                Img.Source = image;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

          

        }
    }
}
