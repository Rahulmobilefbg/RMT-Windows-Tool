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

        public RMTMainForm()
        {
            Text = "RMT Windows Tool";
            Size = new Size(800, 500);
            StartPosition = FormStartPosition.CenterScreen;
            BackColor = Color.FromArgb(24, 24, 30);
            ForeColor = Color.White;

            Label title = new Label();
            title.Text = "RMT WINDOWS TOOL";
            title.Font = new Font("Segoe UI", 22, FontStyle.Bold);
            title.ForeColor = Color.DeepSkyBlue;
            title.Location = new Point(25, 20);
            title.Size = new Size(700, 50);
            Controls.Add(title);

            Label subtitle = new Label();
            subtitle.Text = "USB / COM PORT DIAGNOSTIC";
            subtitle.Font = new Font("Segoe UI", 11);
            subtitle.ForeColor = Color.LightGray;
            subtitle.Location = new Point(30, 75);
            subtitle.Size = new Size(500, 30);
            Controls.Add(subtitle);

            Label portHeading = new Label();
            portHeading.Text = "Detected COM Ports";
            portHeading.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            portHeading.Location = new Point(30, 125);
            portHeading.Size = new Size(300, 30);
            Controls.Add(portHeading);

            portList = new ListBox();
            portList.Location = new Point(30, 165);
            portList.Size = new Size(700, 200);
            portList.BackColor = Color.FromArgb(40, 40, 48);
            portList.ForeColor = Color.White;
            portList.Font = new Font("Segoe UI", 11);
            Controls.Add(portList);

            refreshButton = new Button();
            refreshButton.Text = "REFRESH PORTS";
            refreshButton.Location = new Point(30, 385);
            refreshButton.Size = new Size(180, 45);
            refreshButton.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            refreshButton.BackColor = Color.DeepSkyBlue;
            refreshButton.ForeColor = Color.Black;
            refreshButton.FlatStyle = FlatStyle.Flat;
            refreshButton.Click += RefreshPorts;
            Controls.Add(refreshButton);

            statusLabel = new Label();
            statusLabel.Text = "Ready";
            statusLabel.Location = new Point(30, 440);
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
                    statusLabel.Text = "No COM ports detected.";
                }
                else
                {
                    foreach (string port in ports)
                    {
                        portList.Items.Add("Detected: " + port);
                    }

                    statusLabel.Text = ports.Length + " COM port(s) detected.";
                }
            }
            catch (Exception ex)
            {
                statusLabel.Text = "Error: " + ex.Message;
            }
        }
    }
}
