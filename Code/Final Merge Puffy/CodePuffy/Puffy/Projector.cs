using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using PuffyProject;

namespace PuffyProject {

    class Projector {
        MainWindow window = new MainWindow();

        public Projector() {
            window.Show();
        }

        public void projectPicture(string storyName, string storyMoment) {
            string picturePath = "Story/" + storyName + "/" + storyMoment + ".png";
            window.changePicture(picturePath);
        }
    }
}