using System;
using System.Drawing;
using System.Media;
using System.Runtime.InteropServices;
using System.Timers;
using System.Windows.Forms;

namespace TrollProject
{
    public partial class EntryFRM : Form
    {
        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(out POINT lpPoint);

        public Forms.TrollFRM TrollFRM;

        public EntryFRM() => InitializeComponent();

        private void EntryFRM_Load(object sender, EventArgs e)
        {
            var splayer = new SoundPlayer(Properties.Resources.TrollSong);

            splayer.PlayLooping();
            StartTrolling();

            FormBorderStyle = FormBorderStyle.None;
            ShowInTaskbar = false;
            Size = new Size(0, 0);

            var timer = new System.Timers.Timer();

            timer.Elapsed += ReCheck;
            timer.AutoReset = true;
            timer.Interval = 100;
            timer.Start();
        }

        private void EntryFRM_FormClosing(object sender, FormClosingEventArgs e) => e.Cancel = true;

        public void StartTrolling()
        {
            if (TrollFRM != null)
            {
                TrollFRM.CanClose = true;
                TrollFRM.Close();
                TrollFRM = null;
            }
            TrollFRM = new Forms.TrollFRM();

            TrollFRM.Show();
        }

        private void ReCheck(Object source, ElapsedEventArgs e)
        {
            Invoke(new MethodInvoker(() =>
            {
                if (TrollFRM != null)
                {
                    if (TrollFRM.Visible)
                    {
                        if (TrollFRM.Location.X == 0 && TrollFRM.Location.Y == 0)
                        {
                            var cursor = GetCursorPosition();

                            if (!(cursor.X == 0 && cursor.Y == 0))
                            {
                                StartTrolling();
                            }
                        }
                    }
                }
            }));
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int X;
            public int Y;

            public static implicit operator Point(POINT point) => new Point(point.X, point.Y);
        }

        public static Point GetCursorPosition()
        {
            GetCursorPos(out POINT lpPoint);

            return lpPoint;
        }
    }
}
