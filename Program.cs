using System.Diagnostics;
using System.Drawing;
using System.IO.Ports;
using System.Windows.Forms;

namespace RMTNative;

static class Program {
  [STAThread]
  static void Main() { ApplicationConfiguration.Initialize(); Application.Run(new MainForm()); }
}

class MainForm : Form {
  TextBox log = new();
  TextBox adbPath = new();
  TextBox fastbootPath = new();
  ListBox devices = new();
  System.Windows.Forms.Timer timer = new();
  public MainForm() {
    Text = "RMT ALL-IN-ONE TOOL"; Width=900; Height=620; MinimumSize=new Size(760,520); StartPosition=FormStartPosition.CenterScreen;
    BackColor=Color.FromArgb(24,27,32); ForeColor=Color.White; Font=new Font("Segoe UI",10);
    var top=new Label { Text="RMT ALL-IN-ONE TOOL", Font=new Font("Segoe UI",22,FontStyle.Bold), ForeColor=Color.DeepSkyBlue, Dock=DockStyle.Top, Height=60, TextAlign=ContentAlignment.MiddleCenter };
    Controls.Add(top);
    var tabs=new TabControl { Dock=DockStyle.Fill };
    Controls.Add(tabs); tabs.BringToFront(); top.BringToFront();
    var adb=new TabPage("ADB") { BackColor=BackColor, ForeColor=ForeColor }; tabs.TabPages.Add(adb);
    var fast=new TabPage("Fastboot") { BackColor=BackColor, ForeColor=ForeColor }; tabs.TabPages.Add(fast);
    var ports=new TabPage("USB / COM") { BackColor=BackColor, ForeColor=ForeColor }; tabs.TabPages.Add(ports);
    var about=new TabPage("About") { BackColor=BackColor, ForeColor=ForeColor }; tabs.TabPages.Add(about);
    adbPath.Text="adb"; fastbootPath.Text="fastboot";
    BuildToolTab(adb,adbPath,new[]{("List Devices","devices"),("Device Info","shell getprop"),("Reboot","reboot"),("Recovery","reboot recovery"),("Bootloader","reboot bootloader"),("Save Logcat","logcat -d")},true);
    BuildToolTab(fast,fastbootPath,new[]{("List Devices","devices"),("Get Info","getvar all"),("Reboot","reboot"),("Bootloader","reboot-bootloader")},false);
    var refresh=new Button { Text="Refresh COM Ports", Left=20,Top=20,Width=180,Height=38 }; refresh.Click+=(_,_)=>RefreshPorts(); ports.Controls.Add(refresh);
    devices.SetBounds(20,70,820,240); devices.BackColor=Color.FromArgb(35,40,48); devices.ForeColor=Color.White; ports.Controls.Add(devices);
    var portNote=new Label { Text="Detection only: port descriptions/VID-PID are shown when Windows exposes them. This does not identify every chipset mode.", Left=20,Top=325,Width=820,Height=50 }; ports.Controls.Add(portNote);
    timer.Interval=2000; timer.Tick+=(_,_)=>RefreshPorts(); timer.Start(); RefreshPorts();
    about.Controls.Add(new Label { Text="RMT diagnostic utility\nBasic ADB / Fastboot command launcher and Windows COM-port display.\nADB/Fastboot binaries must be available in PATH or beside this app.\nUse only on devices you own or are authorized to service.", Left=30,Top=30,Width=780,Height=180,ForeColor=Color.White });
  }
  void BuildToolTab(TabPage page,TextBox path, (string,string)[] buttons,bool isAdb) {
    var label=new Label { Text="Executable (PATH name or full path):", Left=20,Top=20,Width=270 }; page.Controls.Add(label);
    path.SetBounds(300,18,520,30); page.Controls.Add(path);
    int y=65; foreach(var (name,arg) in buttons) { var b=new Button { Text=name, Left=20+(buttons.ToList().IndexOf((name,arg))%3)*185,Top=y+(buttons.ToList().IndexOf((name,arg))/3)*50,Width=170,Height=38}; b.Click+=(_,_)=>RunCommand(path.Text,arg); page.Controls.Add(b); }
    var output=new TextBox { Multiline=true,ScrollBars=ScrollBars.Vertical,ReadOnly=true,Left=20,Top=170,Width=820,Height=320,Anchor=AnchorStyles.Top|AnchorStyles.Bottom|AnchorStyles.Left|AnchorStyles.Right,BackColor=Color.FromArgb(35,40,48),ForeColor=Color.White }; page.Controls.Add(output); log=output;
  }
  void RunCommand(string exe,string args) { try { var p=new Process(); p.StartInfo.FileName=exe; p.StartInfo.Arguments=args; p.StartInfo.UseShellExecute=false; p.StartInfo.RedirectStandardOutput=true; p.StartInfo.RedirectStandardError=true; p.StartInfo.CreateNoWindow=true; p.Start(); string o=p.StandardOutput.ReadToEnd(); string e=p.StandardError.ReadToEnd(); p.WaitForExit(8000); log.AppendText($"\r\n> {exe} {args}\r\n{o}{e}\r\n"); } catch(Exception ex){log.AppendText("Error: "+ex.Message+"\r\n");} }
  void RefreshPorts(){ if(!devices.IsHandleCreated)return; try { var entries=SerialPort.GetPortNames().Order().Select(p=>p+" (Windows serial port)").ToArray(); var old=devices.Items.Cast<string>().ToArray(); if(!old.SequenceEqual(entries)){devices.Items.Clear();devices.Items.AddRange(entries);} } catch(Exception ex){log.AppendText("COM detection: "+ex.Message+"\r\n");} }
}
