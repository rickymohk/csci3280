﻿using System;
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
using NAudio;
using NAudio.Wave;

namespace test1
{
    public partial class Form1 : Form
    {
        private VideoFileReader reader = new VideoFileReader();
        private System.Timers.Timer t;
        private Bitmap[] buf;
        private double fps;
        private double duration;
        private WaveStream wave;
        private int buf_i;

        public Form1()
        {
            InitializeComponent();
            buf_i = 0;
            buf = new Bitmap[2];
        }

        private void nextFrame(Object source, ElapsedEventArgs e)
        {
            t.Stop();
            if (buf[buf_i] != null)
            {
                pictureBox1.Image = buf[buf_i];
                t.Start();
                buf[buf_i] = reader.ReadVideoFrame();
                buf_i = 1 - buf_i;
            }
                
            else
            {
                t.Dispose();
                reader.Close();
            }
                
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(!reader.IsOpen)
            {
                reader.Open("H:\\SHE_uncompressed.avi");
                buf[0] = reader.ReadVideoFrame();
                buf[1] = reader.ReadVideoFrame();
            }
            fps = (double)reader.FrameRate;
            duration = 1 / fps * 1000;
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
