using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

namespace PuffyProject {
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            this.WindowState = WindowState.Minimized;
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            Image image = new Image();
            BitmapImage src = new BitmapImage();
            src.BeginInit();
            src.UriSource = new Uri("Story/Snowman/story.png", UriKind.Relative);
            src.CacheOption = BitmapCacheOption.OnLoad;
            src.EndInit();
            image.Source = src;
            image.Stretch = Stretch.Uniform;
            imgPhoto.Children.Add(image);
        }

        public void changePicture(string picturePath) {
            Image image = new Image();
            BitmapImage src = new BitmapImage();
            src.BeginInit();
            src.UriSource = new Uri(picturePath, UriKind.Relative);
            src.CacheOption = BitmapCacheOption.OnLoad;
            src.EndInit();
            image.Source = src;
            image.Stretch = Stretch.Uniform;
            imgPhoto.Children.Clear();
            imgPhoto.Children.Add(image);
            this.WindowState = WindowState.Normal;
        }
    }
}
