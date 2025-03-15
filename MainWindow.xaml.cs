﻿using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SmartFit
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // Enable window dragging by clicking on the title bar
        private void TitleBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        // Minimize window
        private void Minimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        // Maximize or Restore window
        // Store original size and position
        private double _originalWidth, _originalHeight, _originalLeft, _originalTop;

        private void SearchBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (SearchBox.Text == "ابحث عن تمرينك")
            {
                SearchBox.IsReadOnly = false;
                SearchBox.Text = "";
                SearchBox.Foreground = Brushes.Black;
            }

        }

        private void SearchBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(SearchBox.Text))
            {
                SearchBox.Text = "ابحث عن تمرينك";
                SearchBox.Foreground = Brushes.Gray;
                SearchBox.IsReadOnly = true;
            }
        }

        private void HomeBorder_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                WindowsContainer.SelectedIndex = 0;

                HomeBorder.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4A7AB0"));
                HomeText.Foreground = Brushes.White;
                HomeIcon.Source = new BitmapImage(new Uri("pack://application:,,,/Assets/Images/vector.png"));

                ExcBorder.Background = Brushes.White;
                ExcText.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4A7AB0"));
                ExcIcon.Source = new BitmapImage(new Uri("pack://application:,,,/Assets/Images/Dumbbells2.png"));

                AiBorder.Background = Brushes.White;
                AiText.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4A7AB0"));
                AiIcon.Source = new BitmapImage(new Uri("pack://application:,,,/Assets/Images/StarsMinimalistic2.png"));

                Userborder.Background = Brushes.White;
                UserText.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4A7AB0"));
                UserIcon.Source = new BitmapImage(new Uri("pack://application:,,,/Assets/Images/User2.png"));

            }
        }

        private void ExcBorder_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                WindowsContainer.SelectedIndex = 1;

                ExcBorder.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4A7AB0"));
                ExcText.Foreground = Brushes.White;
                ExcIcon.Source = new BitmapImage(new Uri("pack://application:,,,/Assets/Images/Dumbbells1.png"));

                HomeBorder.Background = Brushes.White;
                HomeText.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4A7AB0"));
                HomeIcon.Source = new BitmapImage(new Uri("pack://application:,,,/Assets/Images/Home.png"));

                AiBorder.Background = Brushes.White;
                AiText.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4A7AB0"));
                AiIcon.Source = new BitmapImage(new Uri("pack://application:,,,/Assets/Images/StarsMinimalistic2.png"));

                Userborder.Background = Brushes.White;
                UserText.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4A7AB0"));
                UserIcon.Source = new BitmapImage(new Uri("pack://application:,,,/Assets/Images/User2.png"));

            }
        }

        private void AiBorder_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                WindowsContainer.SelectedIndex = 2;


                AiBorder.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4A7AB0"));
                AiText.Foreground = Brushes.White;
                AiIcon.Source = new BitmapImage(new Uri("pack://application:,,,/Assets/Images/StarsMinimalistic1.png"));

                ExcBorder.Background = Brushes.White;
                ExcText.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4A7AB0"));
                ExcIcon.Source = new BitmapImage(new Uri("pack://application:,,,/Assets/Images/Dumbbells3.png"));

                HomeBorder.Background = Brushes.White;
                HomeText.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4A7AB0"));
                HomeIcon.Source = new BitmapImage(new Uri("pack://application:,,,/Assets/Images/Home.png"));

                Userborder.Background = Brushes.White;
                UserText.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4A7AB0"));
                UserIcon.Source = new BitmapImage(new Uri("pack://application:,,,/Assets/Images/User2.png"));

            }
        }

        private void Userborder_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                WindowsContainer.SelectedIndex = 3;


                Userborder.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4A7AB0"));
                UserText.Foreground = Brushes.White;
                UserIcon.Source = new BitmapImage(new Uri("pack://application:,,,/Assets/Images/User1.png"));

                ExcBorder.Background = Brushes.White;
                ExcText.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4A7AB0"));
                ExcIcon.Source = new BitmapImage(new Uri("pack://application:,,,/Assets/Images/Dumbbells2.png"));

                HomeBorder.Background = Brushes.White;
                HomeText.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4A7AB0"));
                HomeIcon.Source = new BitmapImage(new Uri("pack://application:,,,/Assets/Images/Home.png"));

                AiBorder.Background = Brushes.White;
                AiText.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4A7AB0"));
                AiIcon.Source = new BitmapImage(new Uri("pack://application:,,,/Assets/Images/StarsMinimalistic2.png"));
            }
        }

        private bool _isMaximized = false;

        private void MaximizeRestore_Click(object sender, RoutedEventArgs e)
        {
            if (_isMaximized) // Restore the window to its original size
            {
                Width = _originalWidth;
                Height = _originalHeight;
                Left = _originalLeft;
                Top = _originalTop;

                // Restore rounded corners
                MainBorder.CornerRadius = new CornerRadius(10);
                TitleBar.CornerRadius = new CornerRadius(10, 10, 0, 0);

                _isMaximized = false;
            }

            else // Maximize the window without making it fullscreen
            {
                // Save current size and position before maximizing
                _originalWidth = Width;
                _originalHeight = Height;
                _originalLeft = Left;
                _originalTop = Top;

                // Get the screen working area (excluding taskbar)
                var screen = SystemParameters.WorkArea;

                Left = screen.Left;
                Top = screen.Top;
                Width = screen.Width;
                Height = screen.Height;

                // Remove the border radius when maximized
                MainBorder.CornerRadius = new CornerRadius(0);
                TitleBar.CornerRadius = new CornerRadius(0);

                _isMaximized = true;
            }
        }





        // Close window
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
