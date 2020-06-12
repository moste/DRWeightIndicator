using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using YamlDotNet.RepresentationModel;

namespace DRWeightIndicator
{
    public struct TelemetryData
    {
        public float Time;
        public float LapTime;
        public float LapDistance;
        public float TotalDistance;
        public float X;
        public float Y;
        public float Z;
        public float Speed;
        public float Xv;
        public float Yv;
        public float Zv;
        public float Xr;
        public float Yr;
        public float Zr;
        public float Xd;
        public float Yd;
        public float Zd;
        public float Susp_pos_bl;
        public float Susp_pos_br;
        public float Susp_pos_fl;
        public float Susp_pos_fr;
        public float Susp_vel_bl;
        public float Susp_vel_br;
        public float Susp_vel_fl;
        public float Susp_vel_fr;
        public float Wheel_speed_bl;
        public float Wheel_speed_br;
        public float Wheel_speed_fl;
        public float Wheel_speed_fr;
        public float Throttle;
        public float Steer;
        public float Brake;
        public float Clutch;
        public float Gear;
        public float Gforce_lat;
        public float Gforce_lon;
        public float Lap;
        public float EngineRate;
        public float Sli_pro_native_support;
        public float Car_position;
        public float Kers_level;
        public float Kers_max_level;
        public float Drs;
        public float Traction_control;
        public float Anti_lock_brakes;
        public float Fuel_in_tank;
        public float Fuel_capacity;
        public float In_pits;
        public float Sector;
        public float Sector1_time;
        public float Sector2_time;
        public float Brakes_temp;
        public float Wheels_pressure;
        public float Teainfo;
        public float Total_laps;
        public float Track_size;
        public float Last_lap_time;
        public float Max_rpm;
        public float Idle_rpm;
        public float Max_gears;
        public float SessionType;
        public float DrsAllowed;
        public float Track_number;
        public float VehicleFIAFlags;
    }

    public partial class FormMain : System.Windows.Forms.Form
    {
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetWindowPos(IntPtr hWnd,
            int hWndInsertAfter, int x, int y, int cx, int cy, int uFlags);

        private const int HWND_TOPMOST = -1;
        private const int HWND_NOTOPMOST = -2;
        private const int SWP_NOMOVE = 0x0002;
        private const int SWP_NOSIZE = 0x0001;
        private const int SWP_SHOWWINDOW = 0x0040;

        public class ConfigItem
        {
            public int ratio_steering = 800;
            public int ratio_glatitue = 30;
            public int ratio_glongtitue = 30;
            public int listen_port = 9050;
            public bool debug = false;
        }
        

        private Rectangle ini_pos;
        private Rectangle new_pos;
        private Process my_process;
        private Point center_pos;

        private delegate void DataUpdateDelegate(int index, TelemetryData data);
        private DataUpdateDelegate mDateUpdater;
        private Thread mUdpServerThread;

        ConfigItem mConfig;

        public FormMain()
        {
            this.mDateUpdater = new DataUpdateDelegate(this.UpdateDataBar);
            InitializeComponent();
            ReadConfig();
        }

        private void ReadConfig()
        {

            using (StreamReader r = new StreamReader("./config.json"))
            {
                string json = r.ReadToEnd();
                mConfig = JsonConvert.DeserializeObject<ConfigItem>(json);                
            }
        }

        private void UpdateDataBar(int index, TelemetryData data)
        {
            //Update the progress value.
            string debugStr = string.Format("Packet: {0} / Lat: {1} / Long: {2} / Gear: {3} / Thro: {4} / Brak: {5} / Speed: {6} / Engine: {7} / latTIme: {8} / steer: {9}",
                index.ToString(), data.Gforce_lat, data.Gforce_lon, data.Gear, data.Throttle, data.Brake, data.Speed*3.6, data.EngineRate*10, data.LapTime, data.Steer);
            if (mConfig.debug)
            {
                labelDebug.Text = debugStr;
                Console.WriteLine(debugStr);
            }

            //steerIndicator.Location = new Point((int)(this.center_pos.X) + (int)(data.Steer* mConfig.ratio_steering), this.center_pos.Y);
            gLatIndicator.Location = new Point((int)(this.center_pos.X) + (int)(data.Gforce_lat * mConfig.ratio_steering), this.center_pos.Y);

            int new_y = this.ini_pos.Y - (int)(data.Gforce_lon * mConfig.ratio_glongtitue);
            if (new_y > 0)
            {
                new_y = 0;
            }

            int new_x = this.ini_pos.X + (int)(data.Gforce_lat * mConfig.ratio_glatitue);


            this.new_pos = new Rectangle(new_x, new_y, this.ini_pos.Width, this.ini_pos.Height);
            SetWindowPos(this.my_process.MainWindowHandle, HWND_TOPMOST, this.new_pos.X, this.new_pos.Y, this.new_pos.Width, this.new_pos.Height, SWP_SHOWWINDOW);

            labelSteer.Text = (data.Steer*100).ToString();
        }


        private void FormMain_Load(object sender, EventArgs e)
        {
            // Creating thread 
            // Using thread class 
            mUdpServerThread = new Thread(new ThreadStart(this.udpServer));
            mUdpServerThread.Start();

            if (mConfig.debug)
            {
                labelDebug.Text = "DEBUG ON, listening at " + mConfig.listen_port.ToString();
            }
        }

        private void timerDelay_Tick(object sender, EventArgs e)
        {
            timerDelay.Stop();
            Rectangle screenSize = Screen.FromControl(this).Bounds;
            this.center_pos = new Point(screenSize.Width / 2-200, 0);

            Process[] processes = Process.GetProcesses();           
            foreach (Process process in processes)
            {
                if ("DRWeightIndicator" == process.ProcessName.ToString())
                {
                    this.my_process = process;
                    break;
                }                
            }
            
            this.ini_pos = new Rectangle((int)(screenSize.Width * 0.1), -20, (int)(screenSize.Width * 0.8), (int)(screenSize.Height * 0.05));
            SetWindowPos(this.my_process.MainWindowHandle, HWND_TOPMOST, this.ini_pos.X, this.ini_pos.Y, this.ini_pos.Width, this.ini_pos.Height, SWP_SHOWWINDOW);

            //Thread.Sleep(1000);
            //move_Banner(new Rectangle(10, 10, 1000, 1000));
            gLatIndicator.Location = this.center_pos;
        }

        TelemetryData ByteArrayToTelemetryData(byte[] bytes)
        {
            TelemetryData stuff;
            GCHandle handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
            try
            {
                stuff = (TelemetryData)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(TelemetryData));
            }
            finally
            {
                handle.Free();
            }
            return stuff;
        }

        public void udpServer()
        {
            byte[] data = new byte[1024];
            IPEndPoint ipep = new IPEndPoint(IPAddress.Any, mConfig.listen_port);
            UdpClient newsock = new UdpClient(ipep);

            Console.WriteLine("Waiting for a client at port: " + mConfig.listen_port);

            IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);

            //data = newsock.Receive(ref sender);

            //Console.WriteLine("Message received from {0}:", sender.ToString());
            //Console.WriteLine(Encoding.ASCII.GetString(data, 0, data.Length));
            //this.ByteArrayToFile("data.dat", data);

            //string welcome = "Welcome to my test server";
            //data = Encoding.ASCII.GetBytes(welcome);
            //newsock.Send(data, data.Length, sender);

            int i = 0;

            while (true)
            {
                data = newsock.Receive(ref sender);
                //Console.WriteLine("recv: " + System.Text.Encoding.Default.GetString(data));
                //this.ByteArrayToFile("data" + i.ToString() + ".dat", data);
                i++;

                TelemetryData parsedData = new TelemetryData();
                parsedData = ByteArrayToTelemetryData(data);

                this.Invoke(this.mDateUpdater, i, parsedData);
            }
        }

        public bool ByteArrayToFile(string fileName, byte[] byteArray)
        {
            try
            {
                Console.WriteLine("save data to " + fileName);

                using (var fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
                {
                    fs.Write(byteArray, 0, byteArray.Length);
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception caught in process: {0}", ex);
                return false;
            }
        }

        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            mUdpServerThread.Abort();
        }
    }
}
