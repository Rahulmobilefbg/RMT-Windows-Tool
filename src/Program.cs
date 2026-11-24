using System;
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
            Text = "RMT WINDOWS TOOL";
            Size = new Size(800, 500);
            StartPosition = FormStartPosition.CenterScreen;
            BackColor = Color.FromArgb(24, 24, 30);
            ForeColor = Color.White;

            Label title = new Label
            {
                Text = "RMT WINDOWS TOOL",
                Font = new Font("Segoe UI", 22, FontStyle.Bold),
                ForeColor = Color.DeepSkyBlue,
                Location = new Point(25, 20),
                Size = new Size(700, 50)
            };
            Controls.Add(title);

            Label subtitle = new Label
            {
                Text = "USB / COM PORT DIAGNOSTIC",
                Font = new Font("Segoe UI", 11),
                ForeColor = Color.LightGray,
                Location = new Point(30, 75),
                Size = new Size(500, 30)
            };
            Controls.Add(subtitle);

            Label portHeading = new Label
            {
                Text = "Detected COM Ports",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Location = new Point(30, 125),
                Size = new Size(300, 30)
            };
            Controls.Add(portHeading);

            portList = new ListBox
            {
                Location = new Point(30, 165),
                Size = new Size(700, 200),
                BackColor = Color.FromArgb(40, 40, 48),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 11)
            };
            Controls.Add(portList);

            refreshButton = new Button
            {
                Text = "REFRESH PORTS",
                Location = new Point(30, 385),
                Size = new Size(180, 45),
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                BackColor = Color.DeepSkyBlue,
                ForeColor = Color.Black,
                FlatStyle = FlatStyle.Flat
            };

            refreshButton.Click += RefreshPorts;
            Controls.Add(refreshButton);

            statusLabel = new Label
            {
                Text = "Ready",
                Location = new Point(30, 440),
                Size = new Size(700, 25),
                ForeColor = Color.LightGreen
            };
            Controls.Add(statusLabel);

            RefreshPortList();
        }

        private void RefreshPorts(object? sender, EventArgs e)
        {
            RefreshPortList();
        }

        private void RefreshPortList()
        {
            portList.Items.Clear();

            try
            {
                string[] ports =
                    System.IO.Ports.SerialPort.GetPortNames();

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

                    statusLabel.Text =
                        ports.Length + " COM port(s) detected.";
                }
            }
            catch (Exception ex)
            {
                statusLabel.Text = "Error: " + ex.Message;
            }
        }
    }
}
