﻿using System;
using System.Runtime.InteropServices;
using System.Media;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using AForge.Video.FFMPEG;
using Multimedia;
using NAudio.Wave;
using AviFile;
using Accord.DirectSound;

namespace test1
{
    public partial class Form1 : Form
    {
        private Multimedia.Timer t;
        private AviManager manager;
        private String avi_path;
        int avi;

        private bool hasAudio;
        private AudioStream astream;
        private Avi.AVISTREAMINFO astreamInfo;
        private Avi.PCMWAVEFORMAT astreamFormat;
        private IntPtr waveData;
        private int astreamLength,astream_i;

        private VideoFileReader reader;
        private Bitmap[] vbuf;
        private int h, w, vbuf_i; 
        private long  frame_i,max_frame;
        private int fps, duration;

        private IWaveProvider wave;
        private AudioOutputDevice audio_out;
        private MemoryStream audio_stream;
        private int sample_rate, sample_size,channels;



        private Byte[][] abuf;



        private int abuf_size,abuf_i;

        public Form1()
        {
            InitializeComponent();
            Avi.AVIFileInit();
            avi = 0;
            hasAudio = false;
            astreamLength = 0;
            reader = new VideoFileReader();
            vbuf = new Bitmap[2];
            abuf = new Byte[2][];
            t = new Multimedia.Timer();
            t.Tick += new System.EventHandler(this.nextFrame);
            t.SynchronizingObject = this;
            t.Mode = TimerMode.Periodic;
        }

        private void nextFrame(Object source, System.EventArgs e)
        {
            if (vbuf[vbuf_i] != null)
            {

                pictureBox1.Image = vbuf[vbuf_i];
                if(frame_i<=max_frame)
                {
                    vbuf[vbuf_i] = reader.ReadVideoFrame();
                    frame_i++;
                }
                else
                {
                    vbuf[vbuf_i] = null;
                }
                vbuf_i = 1 - vbuf_i;
            }              
            else
            {
                t.Dispose();
                manager.Close();
            }
/*
            if(abuf[abuf_i] != null)
            {
                textBox1.Text = sample_rate.ToString();
                if (astream_i<(astreamLength-abuf_size))
                {
                    Marshal.Copy(waveData, abuf[abuf_i], astream_i, abuf_size);
                    astream_i += abuf_size;
                }
                else if(astream_i<astreamLength)
                {
                    Marshal.Copy(waveData, abuf[abuf_i], astream_i, astreamLength-astream_i);
                    astream_i += abuf_size;
                }
                else
                {
                    abuf[abuf_i] = null;
                }
                abuf_i = 1 - abuf_i;
            }
*/         
                
        }

        private void play(String filepath)
        {
            if(avi==0)
            {
                Avi.AVIFILEINFO aviHeader = new Avi.AVIFILEINFO();
                int streams;
                int res = Avi.AVIFileOpen(ref avi, filepath, Avi.OF_READWRITE, 0);
                if(res==0)
                {
                    if(avi != 0)
                    {
                        Avi.AVIFileInfo(avi, ref aviHeader, Marshal.SizeOf(aviHeader));
                        streams = aviHeader.dwStreams;
                        manager = new AviManager(filepath, true);
                        if (streams == 2)
                        {
                            hasAudio = true;
                        }
                        if (!reader.IsOpen)
                        {
                            try
                            {
                                reader.Open(avi_path);
                            }
                            catch (System.IO.IOException e)
                            {
                                if (e.Data.Equals("Cannot open Video file"))
                                {
                                    MessageBox.Show("Cannot open Video file");
                                }
                            }

                            if (reader.IsOpen)
                            {
                                max_frame = reader.FrameCount;
                                frame_i = 0;
                                vbuf_i = 0;
                                vbuf[0] = reader.ReadVideoFrame();
                                vbuf[1] = reader.ReadVideoFrame();
                                fps = reader.FrameRate;
                                duration = 1000 / fps;
                                t.Period = duration;
                                t.Start();

                            }
                            else
                            {
                                MessageBox.Show("Cannot open Video file");
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Cannot open Video file");
                    }

                }
                else
                {
                    MessageBox.Show("Cannot open Video file");
                }
                /*
                                if(hasAudio)
                                {
                                    astream = manager.GetWaveStream();
                                    sample_rate = astream.CountSamplesPerSecond;
                                    sample_size = astream.CountBitsPerSample;
                                    channels = astream.CountChannels;
                                    abuf_size = duration * sample_rate * sample_size / 8000;
                                    float[] temp = new float[astreamLength / 4];
                                    waveData = astream.GetStreamData(ref astreamInfo, ref astreamFormat, ref astreamLength);
                                    Marshal.Copy(waveData, temp, astream_i, astreamLength/4-1);
                                    audio_out = new AudioOutputDevice(Handle, sample_rate, channels);
                                    audio_out.Play(temp);

                                    abuf_i = 0;
                                    abuf[0] = new Byte[abuf_size];
                                    abuf[1] = new Byte[abuf_size];
                                    astreamInfo = new Avi.AVISTREAMINFO();
                                    astreamFormat = new Avi.PCMWAVEFORMAT();
                                    waveData = astream.GetStreamData(ref astreamInfo, ref astreamFormat, ref astreamLength);
                                    Byte[] temp = new byte[astreamLength];
                                    Marshal.Copy(waveData, temp, astream_i, astreamLength);
                                    using (MemoryStream ms = new MemoryStream(temp))
                                    {
                                         SoundPlayer player = new SoundPlayer(audio_stream);
                                         player.Play();
                                    }



                                }
                */



            }
            else
            {
                t.Start();
            }



        }

        private void loadPlayList(string filepath)
        {

        }

        private void button1_Click(object sender, EventArgs e)  
        {
            play(avi_path);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            t.Stop();
        }

        private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            if (openFileDialog1.FileName != "")
            {
                if(openFileDialog1.FilterIndex==1) /*avi*/
                {
                    avi_path = openFileDialog1.FileName;
                }
                else if(openFileDialog1.FilterIndex==2)            /*txt*/
                {
                    loadPlayList(openFileDialog1.FileName);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(pictureBox1.Image != null)
            {
                saveScreenShot.ShowDialog();
                if (saveScreenShot.FileName != "")
                {
                    FileStream fs = (FileStream)saveScreenShot.OpenFile();
                    switch (saveScreenShot.FilterIndex)
                    {
                        case 1:
                            pictureBox1.Image.Save(fs, System.Drawing.Imaging.ImageFormat.Jpeg);
                            break;
                        case 2:
                            pictureBox1.Image.Save(fs, System.Drawing.Imaging.ImageFormat.Bmp);
                            break;
                        case 3:
                            pictureBox1.Image.Save(fs, System.Drawing.Imaging.ImageFormat.Gif);
                            break;
                        case 4:
                            pictureBox1.Image.Save(fs, System.Drawing.Imaging.ImageFormat.Png);
                            break;

                    }
                    fs.Close();
                    saveScreenShot.FileName = "";
                }
            }
            else
            {
                MessageBox.Show("No video playing.");
                
            }

        }
    }
}
