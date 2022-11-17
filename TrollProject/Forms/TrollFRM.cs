using System;
using System.Drawing;
using System.Timers;
using System.Windows.Forms;

namespace TrollProject.Forms
{
    public partial class TrollFRM : Form
    {
        public bool CanClose = false;

        private System.Timers.Timer timer;

        public TrollFRM() => InitializeComponent();

        private void TrollFRM_Load(object sender, EventArgs e)
        {
            Location = new Point(0, 0);
            BackColor = Color.Fuchsia;
            TransparencyKey = BackColor;
            FormBorderStyle = FormBorderStyle.None;
            ShowInTaskbar = false;
            TopMost = true;

            timer = new System.Timers.Timer();
            timer.Elapsed += OnTimedEvent;
            timer.AutoReset = true;
            timer.Interval = 10;

            timer.Start();

#if DEBUG // For the better
            CanClose = true;
#endif
        }

        private void TrollFRM_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (CanClose)
            {
                timer.Dispose();
                return;
            }
            e.Cancel = true;
        }

        public void TelWin()
        {
            if (Visible)
            {
                Location = new Point(Cursor.Position.X - Width / 2, Cursor.Position.Y - Height / 2);
            }
            Focus();
        }

        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            try
            {
                Invoke(new MethodInvoker(() =>
                {
                    TelWin();
                }));
            }
            catch (Exception) { }
        }
    }
}
