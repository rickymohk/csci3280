using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Video.FFMPEG;
using System.Timers;

namespace test1
{
    public partial class Form1 : Form
    {
        private VideoFileReader reader = new VideoFileReader();
        private System.Timers.Timer t;
        private Bitmap[] buf;
        private double fps;
        private double duration;

        public Form1()
        {
            InitializeComponent();
            
        }

        private void nextFrame(Object source, ElapsedEventArgs e)
        {
            t.Dispose();
            Bitmap img = reader.ReadVideoFrame();
            if (img != null)
            {
                pictureBox1.Image = img;
                t = new System.Timers.Timer(duration);
                t.Elapsed += nextFrame;
                t.AutoReset = false;
                t.Enabled = true;
            }
                
            else
            {
                t.Stop();
                reader.Close();
            }
                
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(!reader.IsOpen)
            {
                reader.Open("C:\\Users\\Ricky\\Documents\\cuhk\\CSCI\\3280\\SHE_uncompressed.avi");
            }
            fps = (double)reader.FrameRate;
            duration = 1 / fps;
            t = new System.Timers.Timer(duration);
            t.Elapsed += nextFrame;
            t.AutoReset = false;
            t.Enabled = true;


        }

        private void button2_Click(object sender, EventArgs e)
        {
            t.Stop();
        }
    }
}
