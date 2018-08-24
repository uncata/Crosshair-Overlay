using MahApps.Metro.Controls;
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
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.ComponentModel;

namespace Crosshair_Overlay
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        [DllImport("User32.dll")]
        static extern IntPtr GetDC(IntPtr hwnd);

        [DllImport("User32.dll")]
        static extern int ReleaseDC(IntPtr hwnd, IntPtr dc);

        public MainWindow()
        {
            InitializeComponent();
        }

        BackgroundWorker worker;

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (!worker.CancellationPending)
            {
                draw();
            }

            if (worker.CancellationPending)
            {
                e.Cancel = true;
            }
        }

        private void draw()
        {
            int a = 2;
            int b = 10;

            IntPtr desktop = GetDC(IntPtr.Zero);
            using (Graphics g = Graphics.FromHdc(desktop))
            {
                g.FillRectangle(System.Drawing.Brushes.Red, (Screen.PrimaryScreen.Bounds.Width / 2) - (b / 2), (Screen.PrimaryScreen.Bounds.Height / 2) - (a / 2), b, a);
                g.FillRectangle(System.Drawing.Brushes.Red, (Screen.PrimaryScreen.Bounds.Width / 2) - (a / 2), (Screen.PrimaryScreen.Bounds.Height / 2) - (b / 2), a, b);
            }
            ReleaseDC(IntPtr.Zero, desktop);
        }
        
        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            worker = new BackgroundWorker();
            worker.WorkerSupportsCancellation = true;
            worker.DoWork += new DoWorkEventHandler(worker_DoWork);
            worker.RunWorkerAsync();
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            worker.CancelAsync();
        }
    }
}
