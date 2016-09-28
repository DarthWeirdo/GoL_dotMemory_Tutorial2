using System;
using System.Windows;
using System.Windows.Threading;

namespace GameOfLife
{
    public partial class MainWindow
    {
        private readonly Grid _mainGrid;
        private readonly DispatcherTimer _timer; //  Generation timer
        private int _genCounter;
        private AdWindow[] _adWindow;


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
            _adWindow = new AdWindow[2];
            for (var i = 0; i < 2; i++)
            {
                if (_adWindow[i] != null) continue;
                _adWindow[i] = new AdWindow(this);
                _adWindow[i].Closed += AdWindowOnClosed;
                _adWindow[i].Top = this.Top + (330*i) + 70;
                _adWindow[i].Left = this.Left + 240;
                _adWindow[i].Show();
            }
        }

        private void AdWindowOnClosed(object sender, EventArgs eventArgs)
        {
            for (var i = 0; i < 2; i++)
            {
                _adWindow[i].Closed -= AdWindowOnClosed;
                _adWindow[i] = null;
            }
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