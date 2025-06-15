using System;
using System.Windows.Forms;
using Gma.System.MouseKeyHook;

namespace etteeDontRun
{
    public partial class Form1 : Form
    {
        private IKeyboardMouseEvents globalHook;
        private bool wHeld = false;
        private int eventCounter = 0;

        private NotifyIcon notifyIcon;
        private ContextMenuStrip contextMenuStrip;
        private ToolStripMenuItem exitMenuItem;

        public Form1()
        {
            InitializeComponent();
            InitializeTrayIcon();
            StartHook();

            this.WindowState = FormWindowState.Minimized;
            this.ShowInTaskbar = false;
            this.Hide();
        }

        private void ResetButton_Click(object sender, EventArgs e)
        {
            eventCounter = 0;
            listBoxEvents.Items.Clear();
        }

        private void StartHook()
        {
            globalHook = Hook.GlobalEvents();
            globalHook.KeyDown += GlobalHook_KeyDown;
            globalHook.KeyUp += GlobalHook_KeyUp;
            globalHook.MouseDown += GlobalHook_MouseDown;
        }

        private void GlobalHook_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W)
                wHeld = true;
        }

        private void GlobalHook_MouseClick(object sender, MouseEventArgs e)
        {
            if (wHeld && e.Button == MouseButtons.Left)
            {
                eventCounter++;
                string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                string message = $"#{eventCounter} - {timestamp}: W held + Left Click";

                listBoxEvents.Invoke((MethodInvoker)(() =>
                {
                    listBoxEvents.Items.Insert(0, message);
                }));
            }
        }

        private void GlobalHook_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W)
                wHeld = false;
        }

        private void GlobalHook_MouseDown(object sender, MouseEventArgs e)
        {
            if (wHeld && e.Button == MouseButtons.Left)
            {
                eventCounter++;
                string timestamp = DateTime.Now.ToString("HH:mm:ss");
                string message = $"#{eventCounter} at {timestamp}: W held + Left Click";

                listBoxEvents.Invoke((MethodInvoker)(() =>
                {
                    listBoxEvents.Items.Insert(0, message);
                }));
            }
        }

        private void InitializeTrayIcon()
        {
            notifyIcon = new NotifyIcon();
            contextMenuStrip = new ContextMenuStrip();
            exitMenuItem = new ToolStripMenuItem("Exit");

            exitMenuItem.Click += (s, e) =>
            {
                notifyIcon.Visible = false;
                Application.Exit();
            };

            contextMenuStrip.Items.Add(exitMenuItem);

            notifyIcon.Icon = SystemIcons.Application;
            notifyIcon.Text = "W + Left Click Tracker";
            notifyIcon.Visible = true;
            notifyIcon.ContextMenuStrip = contextMenuStrip;
            notifyIcon.DoubleClick += NotifyIcon_DoubleClick;
        }

        private void NotifyIcon_DoubleClick(object sender, EventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            this.Activate();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }
    }
}
