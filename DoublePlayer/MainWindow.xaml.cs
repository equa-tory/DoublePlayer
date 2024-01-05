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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace DoublePlayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer;
        double a_offset;

        public MainWindow()
        {
            InitializeComponent();
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(500);
            timer.Tick += new EventHandler(timer_Tick);

        }

        private void timer_Tick(object? sender, EventArgs e)
        {
            slider_seek.Value = mediaElement1.Position.TotalSeconds;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (mediaElement1.Source != null) mediaElement1.Play();
            if(mediaElement2.Source != null) mediaElement2.Play();
            b_Play.Visibility = Visibility.Hidden;
            b_Pause.Visibility = Visibility.Visible;
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (mediaElement1.Source != null) mediaElement1.Stop();
            if (mediaElement2.Source != null) mediaElement2.Stop();
            b_Play.Visibility = Visibility.Visible;
            b_Pause.Visibility = Visibility.Hidden;
        }
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (mediaElement1.Source != null) mediaElement1.Pause();
            if (mediaElement2.Source != null) mediaElement2.Pause();
            b_Play.Visibility = Visibility.Visible;
            b_Pause.Visibility = Visibility.Hidden;
        }

        private void B_In(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".mp4";
            dlg.Filter = "MP4 Files|*.mp4";
            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                string filename = dlg.FileName;
                mediaElement1.Source = new Uri(filename);

                mediaElement1.LoadedBehavior = MediaState.Manual;
                mediaElement1.UnloadedBehavior = MediaState.Manual;
                mediaElement1.Volume = (double)slider_v.Value;
                //mediaElement1.Play();

                b_Play.Visibility = Visibility.Visible;
                b_Pause.Visibility = Visibility.Hidden;
            }
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            mediaElement1.Volume = (double)slider_v.Value;
        }
        private void Slider_ValueChanged1(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            mediaElement2.Volume = (double)slider_av.Value;
        }

        private void Slider_ValueChanged_1(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            mediaElement1.Position = TimeSpan.FromSeconds(slider_seek.Value);
            mediaElement2.Position = TimeSpan.FromSeconds(slider_seek.Value + a_offset);
        }

        private void Window_Drop(object sender, DragEventArgs e)
        {
            string filename = (string)((DataObject)e.Data).GetFileDropList()[0];
            mediaElement1.Source = new Uri(filename);

            mediaElement1.LoadedBehavior = MediaState.Manual;
            mediaElement1.UnloadedBehavior = MediaState.Manual;
            mediaElement1.Volume = (double)slider_v.Value;
            //mediaElement1.Play();

            b_Play.Visibility = Visibility.Visible;
            b_Pause.Visibility = Visibility.Hidden;
        }

        private void mediaElement1_MediaOpened(object sender, RoutedEventArgs e)
        {
            TimeSpan ts = mediaElement1.NaturalDuration.TimeSpan;
            slider_seek.Maximum = ts.TotalSeconds;
            timer.Start();
        }

        private void Button_Click16(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".m4a";
            dlg.Filter = "M4A Files|*.m4a|" +
                "Audio from Video Files (*.mp4)|*.mp4|" +
                "Audio Files (*.mp3)|*.mp3";
            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                string filename = dlg.FileName;
                mediaElement2.Source = new Uri(filename);
                mediaElement2.Position = mediaElement1.Position + TimeSpan.FromSeconds(a_offset);

                mediaElement2.LoadedBehavior = MediaState.Manual;
                mediaElement2.UnloadedBehavior = MediaState.Manual;
                mediaElement2.Volume = (double)slider_av.Value;
            }
        }

        private void Button_Click8(object sender, RoutedEventArgs e)
        {
            mediaElement2.Source = null;
        }

        private void ao_0(object sender, RoutedEventArgs e)
        {
            a_offset -= 0.5f;
            OffsetText.Text = "Audio Offset: " + a_offset.ToString();
        }
        private void ao_1(object sender, RoutedEventArgs e)
        {
            a_offset += 0.5f;
            OffsetText.Text = "Audio Offset: " + a_offset.ToString();
        }
    }
}
