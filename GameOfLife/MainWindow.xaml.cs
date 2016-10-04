using System;
using System.Windows;
using System.Windows.Threading;

namespace GameOfLife
{
    public partial class MainWindow
    {
        private readonly Grid _mainGrid;
        private readonly DispatcherTimer _timer;
        private int _genCounter;
        private AdWindow _adWindow;


        public MainWindow()
        {
            InitializeComponent();
            _mainGrid = new Grid(MainCanvas);

            _timer = new DispatcherTimer();
            _timer.Tick += OnTimer;
            _timer.Interval = TimeSpan.FromMilliseconds(200);
        }


        private void StartAd()
        {
            if (_adWindow != null) return;
            _adWindow = new AdWindow(this);                        
            _adWindow.Closed += AdWindowOnClosed;
            _adWindow.Top = Top + 400;
            _adWindow.Left = Left + 240;
            _adWindow.Show();            
        }

        private void AdWindowOnClosed(object sender, EventArgs eventArgs)
        {
            _adWindow.Closed -= AdWindowOnClosed;
            _adWindow = null;         
        }


        private void Button_OnClick(object sender, EventArgs e)
        {
            if (!_timer.IsEnabled)
            {
                _timer.Start();
                ButtonStart.Content = "Stop";
                StartAd();
            }
            else
            {
                _timer.Stop();
                ButtonStart.Content = "Start";
            }
        }

        private void OnTimer(object sender, EventArgs e)
        {
            _mainGrid.Update();
            _genCounter++;
            lblGenCount.Content = "Generations: " + _genCounter;
        }

        private void ButtonClear_Click(object sender, RoutedEventArgs e)
        {
            _mainGrid.Clear();
        }
    }
}