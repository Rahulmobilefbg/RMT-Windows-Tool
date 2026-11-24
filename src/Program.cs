using System;
using System.IO.Ports;
using System.Drawing;
using System.Windows.Forms;

namespace RMTNative
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new RMTMainForm());
        }
    }

    public class RMTMainForm : Form
    {
        private ListBox portList;
        private Label statusLabel;
        private Button refreshButton;
        private Label infoLabel;

        public RMTMainForm()
        {
            Text = "RMT Windows Tool";
            Size = new Size(760, 520);
            StartPosition = FormStartPosition.CenterScreen;
            BackColor = Color.FromArgb(18, 22, 30);
            ForeColor = Color.White;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;

            Panel header = new Panel();
            header.Dock = DockStyle.Top;
            header.Height = 100;
            header.BackColor = Color.FromArgb(25, 35, 48);
            Controls.Add(header);

            Label title = new Label();
            title.Text = "RMT WINDOWS TOOL";
            title.Font = new Font("Segoe UI", 22, FontStyle.Bold);
            title.ForeColor = Color.DeepSkyBlue;
            title.Location = new Point(25, 15);
            title.Size = new Size(700, 45);
            header.Controls.Add(title);

            Label subtitle = new Label();
            subtitle.Text = "MOBILE DEVICE DIAGNOSTIC";
            subtitle.Font = new Font("Segoe UI", 10);
            subtitle.ForeColor = Color.LightGray;
            subtitle.Location = new Point(30, 62);
            subtitle.Size = new Size(500, 25);
            header.Controls.Add(subtitle);

            Label portHeading = new Label();
            portHeading.Text = "USB / COM PORT DETECTION";
            portHeading.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            portHeading.ForeColor = Color.White;
            portHeading.Location = new Point(25, 120);
            portHeading.Size = new Size(400, 30);
            Controls.Add(portHeading);

            portList = new ListBox();
            portList.Location = new Point(25, 160);
            portList.Size = new Size(690, 240);
            portList.BackColor = Color.FromArgb(32, 38, 48);
            portList.ForeColor = Color.White;
            portList.Font = new Font("Segoe UI", 11);
            portList.BorderStyle = BorderStyle.FixedSingle;
            Controls.Add(portList);

            refreshButton = new Button();
            refreshButton.Text = "REFRESH DEVICES";
            refreshButton.Location = new Point(25, 420);
            refreshButton.Size = new Size(200, 45);
            refreshButton.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            refreshButton.BackColor = Color.DeepSkyBlue;
            refreshButton.ForeColor = Color.Black;
            refreshButton.FlatStyle = FlatStyle.Flat;
            refreshButton.Click += RefreshPorts;
            Controls.Add(refreshButton);

            infoLabel = new Label();
            infoLabel.Text = "USB diagnostic and COM port monitoring";
            infoLabel.Location = new Point(250, 430);
            infoLabel.Size = new Size(450, 25);
            infoLabel.ForeColor = Color.LightGray;
            Controls.Add(infoLabel);

            statusLabel = new Label();
            statusLabel.Text = "STATUS: READY";
            statusLabel.Location = new Point(25, 475);
            statusLabel.Size = new Size(700, 25);
            statusLabel.ForeColor = Color.LightGreen;
            Controls.Add(statusLabel);

            RefreshPortList();
        }

        private void RefreshPorts(object sender, EventArgs e)
        {
            RefreshPortList();
        }

        private void RefreshPortList()
        {
            portList.Items.Clear();

            try
            {
                string[] ports = SerialPort.GetPortNames();
                Array.Sort(ports);

                if (ports.Length == 0)
                {
                    portList.Items.Add("No COM ports detected.");
                    statusLabel.Text = "STATUS: NO COM PORT FOUND";
                    statusLabel.ForeColor = Color.Orange;
                }
                else
                {
                    foreach (string port in ports)
                    {
                        portList.Items.Add("USB SERIAL DEVICE  |  " + port);
                    }

                    statusLabel.Text =
                        "STATUS: " + ports.Length + " COM PORT(S) DETECTED";

                    statusLabel.ForeColor = Color.LightGreen;
                }
            }
            catch (Exception ex)
            {
                statusLabel.Text = "STATUS: ERROR - " + ex.Message;
                statusLabel.ForeColor = Color.Red;
            }
        }
    }
}
