using System;
using System.Windows.Forms;

namespace etteeDontRun
{
    partial class Form1
    {
        private ListBox listBoxEvents;
        private Button resetButton;

        private void InitializeComponent()
        {
            this.listBoxEvents = new ListBox();
            this.resetButton = new Button();
            this.SuspendLayout();

            // listBoxEvents
            this.listBoxEvents.FormattingEnabled = true;
            this.listBoxEvents.ItemHeight = 16;
            this.listBoxEvents.Location = new System.Drawing.Point(12, 12);
            this.listBoxEvents.Size = new System.Drawing.Size(360, 180);
            this.listBoxEvents.Name = "listBoxEvents";

            // resetButton
            this.resetButton.Text = "Reset";
            this.resetButton.Location = new System.Drawing.Point(12, 200);
            this.resetButton.Size = new System.Drawing.Size(75, 30);
            this.resetButton.Name = "resetButton";
            this.resetButton.Click += new EventHandler(this.ResetButton_Click);

            // Form1
            this.ClientSize = new System.Drawing.Size(384, 241);
            this.Controls.Add(this.listBoxEvents);
            this.Controls.Add(this.resetButton);
            this.Name = "Form1";
            this.Text = "W + Left Click Tracker";
            this.ResumeLayout(false);
        }

    }
}
