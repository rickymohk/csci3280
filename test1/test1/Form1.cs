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
using System.Net;
using System.Net.Sockets;
using AForge.Video.FFMPEG;
using Multimedia;
using AviFile;
//using SharpFFmpeg;
using WinMM;

namespace test1
{
    public partial class Form1 : Form
    {
        
        private Multimedia.Timer t;
        private String avi_path;
        private int avi;       //pfile
        

        private bool hasAudio;
        //        private AudioOutputDevice aPlayer;
        private WaveOut aPlayer;
        Volume vol;
        private IntPtr astream,wavedata;     //ppavi
        private Avi.AVISTREAMINFO astreamInfo;
        private int sample_rate, sample_size, channels, lsamples;
        private WaveFormat wf;
        private int lstart,len, astream_i;
        private IntPtr temp1, temp2;
        private int err;

        private VideoFileReader reader;
        private Bitmap[] vbuf;
        private int h, w, vbuf_i; 
        private long  frame_i,max_frame;
        private int fps, duration;
        private long count,count2;
        /*
        private IntPtr pFormatCtx;
        private FFmpeg.AVFormatContext FormatCtx;
        private IntPtr pAudioCodecCtx;
        private FFmpeg.AVCodecContext AudioCodecCtx;
        private IntPtr pAudioCodec;
        private IntPtr pSamples;
        private int frame_size_ptr;
        */
        private Byte[][] abuf;


        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            vol.Left = (float)trackBar1.Value / 100;
            vol.Right = (float)trackBar1.Value / 100;
        }

        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            String file = listView1.SelectedItems[0].SubItems[3].Text;
            toolStripStatusLabel1.Text = file;
            if (avi_path != file)
            {
                avi_path = file;
                avi = 0;
            }
        }



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
         //   FFmpeg.av_register_all();
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
            temp1 = Marshal.AllocHGlobal(4);
            temp2 = Marshal.AllocHGlobal(4);
            vol = new Volume((float)0.5);
            trackBar1.Value = 50;
            count = count2 = 0;
            port = 7689;
            max_peer = 2;
            outpacket = null;

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
                pictureBox1.Image = null;
                aStop();
                button1.Text = "Play";
                t.Stop();
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


         
            if (hasAudio && abuf[abuf_i] != null)
            //     if(true)
            {
                aPlayer.Volume = vol;
                toolStripStatusLabel1.Text = aPlayer.Volume.Left.ToString();
                toolStripStatusLabel2.Text = aPlayer.Volume.Right.ToString();
                count2++;
                //        Marshal.Copy(abuf[abuf_i], 0, data, size);
                aPlayer.Write(abuf[abuf_i]);
                if (astream != null && astream_i < lstart + len)
                {
                    count++;
                    err = Avi.AVIStreamRead(astream, astream_i, lsamples, wavedata, abuf_size, temp1.ToInt32(), temp2.ToInt32());
               //     err = FFmpeg.avcodec_decode_audio(pAudioCodecCtx, pSamples, out frame_size_ptr, wavedata, abuf_size);
                    if(err==0)
                    {
                        astream_i += lsamples;
                        Marshal.Copy(wavedata, abuf[abuf_i], 0, abuf_size);
                    }
                    else
                    {
                        abuf[abuf_i] = null;
                    }
                    
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

        private void aStop()
        {
            
            if (aPlayer != null)
            {
                try
                {
                    aPlayer.Stop();
                }
                catch { }
            }
            
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
                try
                {
                    aPlayer = new WaveOut(-1);
                    wf = new WaveFormat();
                    wf.Channels = (short)channels;
                    wf.BitsPerSample = (short)sample_size;
                    wf.FormatTag = WaveFormatTag.Pcm;
                    wf.SamplesPerSecond = sample_rate;
                    aPlayer.Open(wf);
                }
                catch(MMSystemException e)
                {
                    hasAudio = false;
                    MessageBox.Show("Cannot play audio.");
                    
                }

            }
        }

        private int play(String filepath)
        {
 /*
            if (avi == 0)
            {
                
                if (FFmpeg.av_open_input_file(out pFormatCtx, filepath, IntPtr.Zero, 0, IntPtr.Zero) < 0)
                {
                    MessageBox.Show("Cannot open file");
                }
                else if ( FFmpeg.av_find_stream_info(pFormatCtx) < 0)
                {
                    MessageBox.Show("Cannot find stream info");
                }
                else
                {
                    avi = 1;
                    FormatCtx = new FFmpeg.AVFormatContext();

                    FormatCtx = (FFmpeg.AVFormatContext)Marshal.PtrToStructure(pFormatCtx, typeof(FFmpeg.AVFormatContext));
                    int audioStream = -1;
                    hasAudio = false;
                    for (int i = 0; i < FormatCtx.nb_streams; i++)
                    {
                        FFmpeg.AVStream stream = (FFmpeg.AVStream)Marshal.PtrToStructure(FormatCtx.streams[i], typeof(FFmpeg.AVStream));
                        FFmpeg.AVCodecContext codec = (FFmpeg.AVCodecContext)Marshal.PtrToStructure(stream.codec, typeof(FFmpeg.AVCodecContext));
                        if (codec.codec_type == FFmpeg.CodecType.CODEC_TYPE_AUDIO && audioStream == -1)
                        {
                            audioStream = i;
                            hasAudio = true;
                            pAudioCodecCtx = stream.codec;
                            AudioCodecCtx = codec;
                            pAudioCodec = FFmpeg.avcodec_find_decoder(AudioCodecCtx.codec_id);
                            if (pAudioCodec == IntPtr.Zero)
                            {
                                MessageBox.Show("Connot find audio decoder");
                            }
                            else
                            {
                                FFmpeg.avcodec_open(stream.codec, pAudioCodec);
                                sample_rate = AudioCodecCtx.sample_rate;
                                channels = AudioCodecCtx.channels;
                                sample_size = AudioCodecCtx.bits_per_sample;
                                abuf_size = channels * duration * sample_rate * sample_size / 8000;
                                lsamples = 8 * abuf_size / sample_size / channels;
                                pSamples = Marshal.AllocHGlobal(4);
                                Marshal.WriteInt32(pSamples, lsamples);
                                abuf[0] = new Byte[abuf_size];
                                abuf[1] = new Byte[abuf_size];
                                if (wavedata != null)
                                    Marshal.FreeHGlobal(wavedata);
                                wavedata = Marshal.AllocHGlobal(abuf_size);
                                frame_size_ptr = abuf_size;
                                FFmpeg.avcodec_decode_audio(pAudioCodecCtx, pSamples, out frame_size_ptr, wavedata, abuf_size);
                                Marshal.Copy(wavedata, abuf[0], 0, abuf_size);
                                FFmpeg.avcodec_decode_audio(pAudioCodecCtx, pSamples, out frame_size_ptr, wavedata, abuf_size);
                                Marshal.Copy(wavedata, abuf[1], 0, abuf_size);
                                abuf_i = 0;
                            }
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
                }
            }
            else
            {
                t.Start();
            }
            if (hasAudio)
            {
                Thread aThread = new Thread(new ThreadStart(aPlay));
                aThread.Start();
            }
            else
            {
                aStop();
            }
        
  */          


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
                        MessageBox.Show("Cannot open Video file");
                        return -1;
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
                        MessageBox.Show("Cannot open Video file");
                        return -1;
                    }
                }
                catch (System.IO.IOException e)
                {
                    if (e.Data.Equals("Cannot open Video file"))
                    {
                        MessageBox.Show("Cannot open Video file");
                    }
                    return -1;
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
                    lsamples = 8 * abuf_size / sample_size / channels;

                       
                    abuf[0] = new Byte[abuf_size];
                    abuf[1] = new Byte[abuf_size];
                    if (wavedata != null) 
                        Marshal.FreeHGlobal(wavedata);
                    wavedata = Marshal.AllocHGlobal(abuf_size);
                    Avi.AVIStreamRead(astream, astream_i, lsamples, wavedata, abuf_size, 0, 0);
                    astream_i += lsamples;
                    Marshal.Copy(wavedata, abuf[0], 0, abuf_size);
                    Avi.AVIStreamRead(astream, astream_i, lsamples, wavedata, abuf_size, 0, 0);
                    astream_i += lsamples;
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
            return 0;

        }

        private void loadPlayList(string filepath)
        {

        }

        private void button1_Click(object sender, EventArgs e)  
        {
            if(button1.Text=="Play")
            {
                if(play(avi_path)==0)
                {
                    button1.Text = "Pause";
                }
                
            }
            else if(button1.Text=="Pause")
            {
                t.Stop();
                aStop();
                button1.Text = "Play";
            }
           
        }


        private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            if (openFileDialog1.FileName != "")
            {
                if(openFileDialog1.FilterIndex==1) /*avi*/
                {
                    if(avi_path != openFileDialog1.FileName)
                    {
                        avi_path = openFileDialog1.FileName;
                        avi = 0;
                        listView1.Refresh();
                    }

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

        public string[] peerIP;
        private int port,max_peer,peer_no;
        public char[] outpacket;

        private void p2PConnectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 connectForm = new Form2() { Owner = this };
            connectForm.StartPosition = FormStartPosition.Manual;
            connectForm.Show(this);
        }

        public void setPeerIP(string[] ipaddr)
        {
            peerIP = ipaddr;
            toolStripStatusLabel1.Text = peerIP[0];
            toolStripStatusLabel2.Text = peerIP[1];
            peer_no = 2;
        }

        public void serverMain()
        {
            IPEndPoint ipep = new IPEndPoint(IPAddress.Any, port);
            Socket newsock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            newsock.Bind(ipep);
            newsock.Listen(10);
            Socket[] client = new Socket[peer_no];
            for(int i=0;i<peer_no;i++)
            {
                client[i] = newsock.Accept();
                new TcpListener(client[i],this);
            }
            newsock.Close();

        }

        public void ClientMain(String ip)
        {
            IPEndPoint ipep = new IPEndPoint(IPAddress.Parse(ip), port);
            Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            server.Connect(ipep);
            new TcpListener(server,this);
            server.Shutdown(SocketShutdown.Both);
            server.Close();
        }
    }
    
    public class TcpListener
    {
        Socket socket;
        Thread inThread, outThread;
        NetworkStream stream;
        StreamReader reader;
        StreamWriter writer;
        Form1 parent;
        int bufsize;

        public TcpListener (Socket s,Form1 f)
        {
            bufsize = 8192;
            socket = s;
            stream = new NetworkStream(s);
            reader = new StreamReader(stream);
            writer = new StreamWriter(stream);
            inThread = new Thread(new ThreadStart(inLoop));
            inThread.Start();
            outThread = new Thread(new ThreadStart(outLoop));
            outThread.Start();
            inThread.Join();
            parent = f;
        }

        public void inLoop()
        {
            while(true)
            {
                char[] packet = new char[bufsize];
                reader.ReadBlock(packet, 0, bufsize);
            }
        }

        public void outLoop()
        {
            while(true)
            {
                char[] packet = new char[bufsize];
                if(parent.outpacket!=null)
                {
                    writer.Write(packet, 0, bufsize);
                    writer.Flush();
                }
            }
        }
    }
}
