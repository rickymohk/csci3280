using System;
using System.Runtime.InteropServices;
using System.Media;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
using System.Windows.Forms;
using AForge.Video.FFMPEG;
using Multimedia;
using AviFile;
using Accord.DirectSound;
using WaveLib;

namespace test1
{
    public partial class Form1 : Form
    {
        private Multimedia.Timer t;
        private String avi_path;
        int avi;       //pfile

        private bool hasAudio;
        //        private AudioOutputDevice aPlayer;
        private WaveOutPlayer aPlayer;
        private IntPtr astream,wavedata;     //ppavi
        private Avi.AVISTREAMINFO astreamInfo;
        private int sample_rate, sample_size, channels;
        private int lstart,len, astream_i;

        private VideoFileReader reader;
        private Bitmap[] vbuf;
        private int h, w, vbuf_i; 
        private long  frame_i,max_frame;
        private int fps, duration;
        private long count,count2;

        private Byte[][] abuf;
        //private float[][] abuf;


        private int abuf_size,abuf_i;

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            String file = listView1.SelectedItems[0].SubItems[3].Text;
            toolStripStatusLabel1.Text = file;
            if(avi_path != file)
            {
                avi_path = file;
                avi = 0;
            }
            
        }

        public Form1()
        {
            InitializeComponent();
            Avi.AVIFileInit();
            avi = 0;
            hasAudio = false;
            reader = new VideoFileReader();
            vbuf = new Bitmap[2];
            abuf = new Byte[2][];
            //abuf = new float[2][];
            t = new Multimedia.Timer();
            t.Tick += new System.EventHandler(this.nextFrame);
            t.SynchronizingObject = this;
            t.Mode = TimerMode.Periodic;
            count = count2 = 0;
        }

        private void nextFrame(Object source, System.EventArgs e)
        {
            toolStripStatusLabel1.Text = (count).ToString();
            toolStripStatusLabel2.Text = (count).ToString();
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

        private void aStop()
        {
            if (aPlayer != null)
            {
                try
                {
                    aPlayer.Dispose();
                } 
                finally
                {
                    aPlayer = null;
                }
            }

        }
       
        private void afiller(IntPtr data, int size)
        {
            // toolStripStatusLabel1.Text = (count++).ToString();
            count++;
            if (abuf[abuf_i] != null)
            {
                count2++;
                Marshal.Copy(abuf[abuf_i], 0, data, size);
              
                if (astream != null && astream_i<lstart+len)
                {
                    Avi.AVIStreamRead(astream, astream_i, abuf_size / (sample_size / 8) / channels, wavedata, abuf_size, 0, 0);
                    astream_i += abuf_size / (sample_size / 8) / channels;
                    Marshal.Copy(wavedata, abuf[abuf_i], 0, abuf_size);
                }
                else
                {
                    /*
                    for (int i = 0; i < abuf_size ; i++)
                    {
                        abuf[abuf_i][i] = 0;
                    }
                    */
                    abuf[abuf_i] = null;
                }
            }
            else
            {
                aStop();
            }
            abuf_i = 1 - abuf_i;

        }
       
        
/*
        private void afiller(object sender, Accord.Audio.NewFrameRequestedEventArgs arg )
        {
            arg.Frames = 8*abuf_size / sample_size / channels;
            if (abuf[abuf_i]!=null)
            {
                int n = sample_size / 8;
                if(n==0)
                {
                    n = 1;
                }
                for(int i=0;i< abuf_size-n;i+=n)
                {
                    float sample = 0;
                    for (int j= i;j<i+ n;j++)
                    {
                        sample = sample*256;
                        sample += abuf[abuf_i][j];
                    }
                    sample /= (float)Math.Pow(2, sample_size-1);
                    arg.Buffer[i / n] = sample;
                }
               
                if (astream != null)
                {
                    Avi.AVIStreamRead(astream, astream_i, 8 * abuf_size / sample_size / channels, wavedata, abuf_size, 0, 0);
                    astream_i += 8* abuf_size / sample_size  / channels;
                    Marshal.Copy(wavedata, abuf[abuf_i], 0, abuf_size);
                }
                else
                {
                    for (int i = 0; i < abuf_size / 4; i++)
                    {
                        abuf[abuf_i][i] = 0;
                    }
                }
            }
            else
            {
                aStop();
            }
            abuf_i = 1 - abuf_i;


        }
*/        
        private void aPlay()
        {
            aStop();
            if(astream != null)
            {
                
               // aPlayer = new AudioOutputDevice(Handle, sample_rate, channels);
               // aPlayer.NewFrameRequested += new EventHandler<Accord.Audio.NewFrameRequestedEventArgs>(afiller);
               // aPlayer.Play();
                 aPlayer = new WaveOutPlayer(-1,new WaveFormat(sample_rate,sample_size,channels),abuf_size,3,new BufferFillEventHandler(afiller));
            }
        }

        private void play(String filepath)
        {
            if(avi==0)
            {
                Avi.AVIFILEINFO aviHeader = new Avi.AVIFILEINFO();
                int streams;
                hasAudio = false;
                int res = Avi.AVIFileOpen(ref avi, filepath, Avi.OF_READWRITE, 0); 
                if (res==0)
                {
                    if(avi != 0)
                    {
                        Avi.AVIFileInfo(avi, ref aviHeader, Marshal.SizeOf(aviHeader));
                        streams = aviHeader.dwStreams;
                        if (streams == 2)
                        {
                            hasAudio = true;
                        }

                    }
                    else
                    {
                        MessageBox.Show("Cannot open Video file3");
                    }

                }
                try
                {
                    reader.Close();
                    reader.Open(avi_path);
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
                        MessageBox.Show("Cannot open Video file2");
                    }
                }
                catch (System.IO.IOException e)
                {
                    if (e.Data.Equals("Cannot open Video file"))
                    {
                        MessageBox.Show("Cannot open Video file1");
                    }
                }
                if (hasAudio)
                { 
                    Avi.AVIFileGetStream(avi, out astream, Avi.streamtypeAUDIO, 0);
                    Avi.AVIStreamInfo(astream, ref astreamInfo, Marshal.SizeOf(astreamInfo));
                    int lsize = new int() ;
                    Avi.AVIStreamReadFormat(astream, 0, 0, ref lsize);
                    Avi.PCMWAVEFORMAT pWave = new Avi.PCMWAVEFORMAT();
                    Avi.AVIStreamReadFormat(astream, 0, ref pWave, ref lsize);
                    sample_rate = pWave.nSamplesPerSec;
                    
                    sample_size = pWave.wBitsPerSample;
                    toolStripStatusLabel1.Text = sample_size.ToString();
                    channels = pWave.nChannels;
                    lstart = Avi.AVIStreamStart(astream.ToInt32());
                    astream_i = lstart;
                    
                    len = Avi.AVIStreamLength(astream.ToInt32())*sample_size;
                    abuf_size = channels * duration * sample_rate * sample_size / 8000;
                    
                    /*              
                                   abuf[0] = new Byte[abuf_size];
                                   abuf[1] = new Byte[abuf_size];
                       */
                    abuf[0] = new Byte[abuf_size];
                    abuf[1] = new Byte[abuf_size];
                    if (wavedata != null) 
                        Marshal.FreeHGlobal(wavedata);
                    wavedata = Marshal.AllocHGlobal(abuf_size);
                    Avi.AVIStreamRead(astream, astream_i, 8*abuf_size / sample_size / channels , wavedata, abuf_size, 0, 0);
                    astream_i += 8*abuf_size / sample_size  / channels;
                    Marshal.Copy(wavedata, abuf[0], 0, abuf_size);
                    Avi.AVIStreamRead(astream, astream_i, 8*abuf_size / sample_size  / channels , wavedata, abuf_size, 0, 0);
                    astream_i += 8*abuf_size / sample_size/ channels;
                    Marshal.Copy(wavedata, abuf[1], 0, abuf_size);
                    abuf_i = 0;



                }
                



            }
            else
            {
                t.Start();
            }
            if(hasAudio)
            {
                aPlay();
            }
            else
            {
                aStop();
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
            aStop();
        }

        private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            if (openFileDialog1.FileName != "")
            {
                if(openFileDialog1.FilterIndex==1) /*avi*/
                {
                    avi_path = openFileDialog1.FileName;
                    avi = 0;
                }
                else if(openFileDialog1.FilterIndex==2)            /*txt*/
                {
                    loadPlayList(openFileDialog1.FileName);
                }
            }
            openFileDialog1.FileName = "";
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
