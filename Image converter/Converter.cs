using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Image_converter
{
    class Converter
    {
        public static readonly List<string> ImageExtensions = new List<string> { ".JPG", ".JPE", ".JPEG",  ".BMP", ".GIF", ".PNG" };
        public static int Width = 0;
        public static int Height = 0;
        public static string dpi = string.Empty;
        public static string Dir = string.Empty;
        public static string DirDest = string.Empty;

        public static void Convert()
        {
            // Get values from UI
            MainWindow.UI.Dispatcher.Invoke((Action)(() =>
            {
                MainWindow.UI.LogBox.Text += "Starting image processor.\n\n";
                Dir = MainWindow.UI.SourceF.Text.Trim();
                DirDest = MainWindow.UI.DestF.Text.Trim();
                Width = int.Parse(MainWindow.UI.WidthBox.Text.Trim());
                Height = int.Parse(MainWindow.UI.HeightBox.Text.Trim());
                dpi = MainWindow.UI.DPIBox.Text.Trim();
            }));

            //Process all images in folder and subfolders
            ProcessDirectory(Dir);             

            //Log status
            MainWindow.UI.Dispatcher.Invoke((Action)(() =>
            {
                MainWindow.UI.LogBox.Text += "Finished.\n";
            }));
        }

        public static void ProcessDirectory(string Dir)
        {
            string[] fileEntries = Directory.GetFiles(Dir);
            foreach (string fileName in fileEntries)
            {
                if (ImageExtensions.Contains(Path.GetExtension(fileName).ToUpperInvariant()))
                {
                    Image i = Image.FromFile(fileName);
                    MainWindow.UI.Dispatcher.Invoke((Action)(() =>
                    {
                        MainWindow.UI.ImageBox.Source = new BitmapImage(new Uri(fileName));
                    }));
                    //Change name of processed file.
                    string ImageName = Path.GetFileNameWithoutExtension(fileName) + "_processed.jpg";
                    //Save in origin directory or destination dirctory
                    if(DirDest != Dir && DirDest != string.Empty)
                    {
                        Image_processor.ResizeImage(fileName, DirDest, ImageName, Width, Height, true, false, dpi);
                    }
                    else
                    {
                        Image_processor.ResizeImage(fileName, Dir, ImageName, Width, Height, true, false, dpi);
                    }                   
                    //log Progress
                    MainWindow.UI.Dispatcher.Invoke((Action)(() =>
                    {
                        MainWindow.UI.LogBox.Text += fileName + " processed.\n\n";
                    }));
                }
            }
            // Recursion for subdirectories
            string[] subdirectoryEntries = Directory.GetDirectories(Dir);
            foreach (string subdirectory in subdirectoryEntries)
            {
                ProcessDirectory(subdirectory);
            }               
        }
    }
}
