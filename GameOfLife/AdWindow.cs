using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace GameOfLife
{
    internal class AdWindow : Window
    {
        private readonly DispatcherTimer _adTimer;
        private int _imgNmb; // currently shown image 
        private string _link; // image URL


        public AdWindow(Window owner)
        {
            var rnd = new Random();
            Owner = owner;
            Width = 350;
            Height = 100;
            ResizeMode = ResizeMode.NoResize;
            WindowStyle = WindowStyle.ToolWindow;
            Title = "Support us by clicking the ads";
            Cursor = Cursors.Hand;
            ShowActivated = false;
            MouseDown += OnClick;

            _imgNmb = rnd.Next(1, 3);
            ChangeAds(this, new EventArgs());

            // Run timer that changes ad's image 
            _adTimer = new DispatcherTimer {Interval = TimeSpan.FromSeconds(3)};
            _adTimer.Tick += ChangeAds;
            _adTimer.Start();
        }

        private void OnClick(object sender, MouseButtonEventArgs e)
        {
            System.Diagnostics.Process.Start(_link);
            Close();
        }

        protected override void OnClosed(EventArgs e)
        {
            //Unsubscribe();
            base.OnClosed(e);
        }

        public void Unsubscribe()
        {
            _adTimer.Tick -= ChangeAds;
        }

        private void ChangeAds(object sender, EventArgs eventArgs)
        {
            var myBrush = new ImageBrush();

            switch (_imgNmb)
            {
                case 1:
                    myBrush.ImageSource =
                        CreateBitmapSourceFromGdiBitmap(Properties.Resources.ad1);
                    Background = myBrush;
                    _link = "http://example.com";
                    _imgNmb++;
                    break;
                case 2:
                    myBrush.ImageSource =
                        CreateBitmapSourceFromGdiBitmap(Properties.Resources.ad2);
                    Background = myBrush;
                    _link = "http://example.com";
                    _imgNmb++;
                    break;
                case 3:
                    myBrush.ImageSource =
                        CreateBitmapSourceFromGdiBitmap(Properties.Resources.ad3);
                    Background = myBrush;
                    _link = "http://example.com";
                    _imgNmb = 1;
                    break;
            }
        }

        public static BitmapSource CreateBitmapSourceFromGdiBitmap(Bitmap bitmap)
        {
            if (bitmap == null)
                throw new ArgumentNullException(nameof(bitmap));

            var rect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);

            var bitmapData = bitmap.LockBits(
                rect,
                ImageLockMode.ReadWrite, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            try
            {
                var size = (rect.Width * rect.Height) * 4;

                return BitmapSource.Create(
                    bitmap.Width,
                    bitmap.Height,
                    bitmap.HorizontalResolution,
                    bitmap.VerticalResolution,
                    PixelFormats.Bgra32,
                    null,
                    bitmapData.Scan0,
                    size,
                    bitmapData.Stride);
            }
            finally
            {
                bitmap.UnlockBits(bitmapData);
            }
        }
    }
}