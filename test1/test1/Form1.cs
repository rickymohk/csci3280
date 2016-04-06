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
        private IntPtr astream, wavedata;     //ppavi
        private Avi.AVISTREAMINFO astreamInfo;
        private int sample_rate, sample_size, channels, lsamples;
        private short format_tag;
        private WaveFormat wf;
        private int lstart, len, astream_i;
        private IntPtr temp1, temp2;
        private int err;

        private VideoFileReader reader;
        private Bitmap[] vbuf;
        private int h, w, vbuf_i;
        private long frame_i, max_frame;
        private int fps, duration;
        private long count, count2;
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
        private string songList_path;
        private int delete;


        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            vol.Left = (float)trackBar1.Value / 100;
            vol.Right = (float)trackBar1.Value / 100;
        }

  /*      private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            String file = songList.SelectedItems[0].SubItems[3].Text;
            toolStripStatusLabel1.Text = file;
            if (avi_path != file)
            {
                avi_path = file;
                avi = 0;
            }
        } */



        //private float[][] abuf;


        private int abuf_size, abuf_i;

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

            /*          String file = listView1.SelectedItems[0].SubItems[3].Text;
                      toolStripStatusLabel1.Text = file;
                      if(avi_path != file)
                      {
                          avi_path = file;
                          avi = 0;
                      }
                     */

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
            display_packet = null;
            done = false;
            packet_size = 8192;
            testppm = null;
            uc = null;
        }

        private void nextFrame(Object source, System.EventArgs e)
        {
            if (vbuf[vbuf_i] != null)
            {

                pictureBox1.Image = vbuf[vbuf_i];
                if (frame_i <= max_frame)
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
                    if (err == 0)
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

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void filesFToolStripMenuItem_Click(object sender, EventArgs e)
        {

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
            if (astream != null)
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
                    wf.FormatTag = (WaveFormatTag)format_tag;
                    
                    wf.SamplesPerSecond = sample_rate;
                    aPlayer.Open(wf);
                }
                catch (MMSystemException e)
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


            if (avi == 0)
            {
                Avi.AVIFILEINFO aviHeader = new Avi.AVIFILEINFO();
                int streams;
                hasAudio = false;
                int res = Avi.AVIFileOpen(ref avi, filepath, Avi.OF_READWRITE, 0);
                if (res == 0)
                {
                    if (avi != 0)
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
                    int lsize = new int();
                    Avi.AVIStreamReadFormat(astream, 0, 0, ref lsize);
                    Avi.PCMWAVEFORMAT pWave = new Avi.PCMWAVEFORMAT();
                    Avi.AVIStreamReadFormat(astream, 0, ref pWave, ref lsize);

                    format_tag =  pWave.wFormatTag;
                    
                    sample_rate = pWave.nSamplesPerSec;

                    sample_size = pWave.wBitsPerSample;
                    toolStripStatusLabel1.Text = sample_size.ToString();
                    channels = pWave.nChannels;
                    lstart = Avi.AVIStreamStart(astream.ToInt32());
                    astream_i = lstart;

                    len = Avi.AVIStreamLength(astream.ToInt32()) * sample_size;
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
            if (hasAudio)
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
            string[] lines = System.IO.File.ReadAllLines(@filepath);
            string[] delimiterChars = { "\' \'", "\'" ,"\n"};
            foreach (string line in lines)
            {
                DataGridViewRow row = (DataGridViewRow)songList.Rows[0].Clone();
                string[] words = line.Split(delimiterChars, StringSplitOptions.RemoveEmptyEntries);
                int i = 0;
                foreach (string s in words)
                {
                    if(String.Compare(s,"NULL",true)==0)
                    {
                        row.Cells[i].Value = "N/A";
                    }
                    else
                        row.Cells[i].Value = s;
                    i++; 
                }
                songList.Rows.Add(row);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text == "Play")
            {
                if (play(avi_path) == 0)
                {
                    button1.Text = "Pause";
                }

            }
            else if (button1.Text == "Pause")
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
                if (openFileDialog1.FilterIndex == 1) /*avi*/
                {
                    if (avi_path != openFileDialog1.FileName)
                    {
                        avi_path = openFileDialog1.FileName;
                        avi = 0;
                    }

                }
                else if (openFileDialog1.FilterIndex == 2)            /*txt*/
                {
                    songList_path = openFileDialog1.FileName;
                    loadPlayList(songList_path);
                }
            }
            openFileDialog1.FileName = "";
        }


        private void button3_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
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


        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            done = true;
        }


        public string[] peerIP;
        public string localIP;
        private int port, max_peer, peer_no, packet_size;
        public byte[] display_packet;
        private bool done;



        private UdpClient uc;

        private void addSong_Click(object sender, EventArgs e)
        {
            popupForm popup = new popupForm();
            DialogResult dialogresult = popup.ShowDialog();
            string title = popup.title;
            string singer = popup.singer;
            string album = popup.album;
            if (title == null)
                title = "NULL";
            if (singer == null)
                singer = "NULL";
            if (album == null)
                album = "NULL";
            if (dialogresult == DialogResult.OK)
            {
                DataGridViewRow row = (DataGridViewRow)songList.Rows[0].Clone();
                row.Cells[0].Value = avi_path;
                row.Cells[1].Value = title;
                row.Cells[2].Value = singer;
                row.Cells[3].Value = album;
                songList.Rows.Add(row);
            }
            popup.Dispose();
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(songList_path, true))
            {
                file.WriteLine("\'" + avi_path + "\' \'" + title + "\' \'" + singer + "\' \'" + album + "\'");
            }
        }

        private void songList_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            t.Stop();
            aStop();
            avi_path = songList.Rows[e.RowIndex].Cells[0].Value.ToString();
            avi = 0;
            button1.Text = "Play";
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void songList_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            delete = e.RowIndex;
        }
        private void deleteList_Click(object sender, EventArgs e)
        {
            int line_number = 0;
            string line_empty = null;
            string tempFile = Path.GetTempFileName();
            foreach (DataGridViewRow item in this.songList.SelectedRows)
            {
                songList.Rows.RemoveAt(item.Index);
            }
            using (StreamReader reader = new StreamReader(songList_path))
            {
                using (StreamWriter writer = new StreamWriter(tempFile))
                {
                    while ((line_empty = reader.ReadLine()) != null)
                    {
                        line_number++;

                        if (line_number == (delete+1))
                            continue;

                        writer.WriteLine(line_empty);
                    }
                }
            }
            File.Delete(songList_path);
            File.Move(tempFile, songList_path);



        }

        private byte[] testppm;
        private int packet_count;
        private Thread serverThread, clientThread;
        

        [StructLayout(LayoutKind.Sequential,Pack =1)]
        private struct packet               //packet structure, modify if necessary. if modified, remember to modify packet2array and array2packet, and all building packet processes accordingly
        {
            public byte code;
            public int senderIP;
            public int frameNo;
            public int order;
            public int total;
            public int size;
            public byte[] data;
        };

        private enum packetCode : byte      //define available packet code
        {
            Ack,
            Reply,
            TestRequest,
            TestFilePart
        };


        private void assembleTest(packet p)
        {
            Array.Copy(p.data, 0, testppm, p.order * packet_size, p.size);
            packet_count++;
        }

        private void sendTest(IPEndPoint ipep, int i,int peer)
        {
            using (FileStream fs = File.OpenRead("1.ppm"))
            {
                packet p = new packet();
                p.code = (byte)packetCode.TestFilePart;
                p.senderIP = localIP==""?0:BitConverter.ToInt32(IPAddress.Parse(localIP).GetAddressBytes(), 0);
                p.frameNo = 0;
                p.total = (int)(fs.Length /packet_size);
                if((fs.Length % packet_size)!=0)
                {
                    p.total++;
                }
                int j = 0;
                bool flag = true;
                while(flag)
                {
                    if(j%peer==i)
                    {
                        long remain = (fs.Length - fs.Position);

                        p.data = new byte[packet_size];
                        int c = fs.Read(p.data, 0, packet_size);
                        if(c<packet_size)
                        {
                            flag = false;
                        }
                        if(c>0)
                        {
                            p.size = packet_size;
                            p.order = j;
                            byte[] b = packet2array(p);
                            uc.Send(b, b.Length, ipep);
                        }

                    }
                    else
                    {
                        fs.Seek(packet_size, SeekOrigin.Current);    
                    }
                    j++;
                }
            }
            
            
        }

        private byte[] packet2array(packet p)
        {
            byte[] b;
            using (MemoryStream ms = new MemoryStream())
            {
                using (BinaryWriter bw = new BinaryWriter(ms))
                {
                    bw.Write(p.code);
                    bw.Write(p.senderIP);
                    bw.Write(p.frameNo);
                    bw.Write(p.order);
                    bw.Write(p.total);
                    bw.Write(p.size);
                    bw.Write(p.data);
                    b = ms.ToArray();
                }
            }
            return b;
        }

        private packet array2packet(byte[] b)
        {
            packet p = new packet();
            using (MemoryStream ms = new MemoryStream(b))
            {
                using (BinaryReader br = new BinaryReader(ms))
                {
                    
                    p.code = br.ReadByte();
                    p.senderIP = br.ReadInt32();
                    p.frameNo = br.ReadInt32();
                    p.order = br.ReadInt32();
                    p.total = br.ReadInt32();
                    p.size = br.ReadInt32();
                    p.data = br.ReadBytes(p.size);
                    
                }
            }
            return p;
        }



        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (peer_no >= 2)
            {

                IPEndPoint ipep0 = new IPEndPoint(IPAddress.Parse(peerIP[0]), port);
                IPEndPoint ipep1 = new IPEndPoint(IPAddress.Parse(peerIP[1]), port);
                UdpClient uc = new UdpClient();
                packet p = new packet();        //Start building the packet to send
                p.code = (byte)packetCode.TestRequest;
                p.senderIP = BitConverter.ToInt32(IPAddress.Parse(localIP).GetAddressBytes(), 0);
                p.frameNo = 0;
                p.order = 0;
                p.total = 1;
                p.size = 2;
                p.data = new byte[p.size];
                p.data[0] = 0;                  //index of the peer , i.e. 0 responsible for the even number portion, a responsible for the odd number portion.
                p.data[1] = 2;                  //number of peer responsible to send the file 
                byte[] b = packet2array(p);     //Convert the packet to byte array
                uc.Send(b,b.Length ,ipep0);     //send out the packet
                p.data[0] = 1;
                b = packet2array(p);
                uc.Send(b, b.Length, ipep1);

            }
            else if(peerIP[0]!="")
            {
                IPEndPoint ipep0 = new IPEndPoint(IPAddress.Parse(peerIP[0]), port);
                UdpClient uc = new UdpClient();
                packet p = new packet();
                p.code = (byte)packetCode.TestRequest;
                p.senderIP = localIP==""?0:BitConverter.ToInt32(IPAddress.Parse(localIP).GetAddressBytes(), 0);
                p.frameNo = 0;
                p.order = 0;
                p.total = 1;
                p.size = 2;
                p.data = new byte[p.size];
                p.data[0] = 0;
                p.data[1] = 1;
                byte[] b = packet2array(p);
                uc.Send(b, b.Length, ipep0);
            }
            else if(peerIP[1]!="")
            {
                IPEndPoint ipep1 = new IPEndPoint(IPAddress.Parse(peerIP[1]), port);
                UdpClient uc = new UdpClient();
                packet p = new packet();
                p.code = (byte)packetCode.TestRequest;
                p.senderIP = localIP == "" ? 0 : BitConverter.ToInt32(IPAddress.Parse(localIP).GetAddressBytes(), 0);
                p.frameNo = 0;
                p.order = 0;
                p.total = 1;
                p.size = 1;
                p.data = new byte[p.size];
                p.data[0] = 0;
                p.data[1] = 1;
                byte[] b = packet2array(p);
                uc.Send(b, b.Length, ipep1);
            }
        }


        private void p2PConnectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 connectForm = new Form2() { Owner = this };
            connectForm.StartPosition = FormStartPosition.Manual;
            connectForm.Show(this);
        }

        public void setPeerIP(string local, string[] ipaddr)
        {
            localIP = local;
            peerIP = ipaddr;
            toolStripStatusLabel1.Text = peerIP[0];
            toolStripStatusLabel2.Text = peerIP[1];
            peer_no = 0;
            if(peerIP[0]!="")
            {
                peer_no++;
            }
            if(peerIP[1]!="")
            {
                peer_no++;
            }
            //            serverThread = new Thread(new ThreadStart(serverMain));
            //            serverThread.IsBackground = true;
            //            serverThread.Start();
            IPEndPoint ipep = new IPEndPoint(IPAddress.Any, port);

            if(peer_no>0)
            {
                if (uc != null)
                {
                    uc.Close();
                }
                uc = new UdpClient(ipep.Port);
                backgroundWorker1.WorkerReportsProgress = true;
                backgroundWorker1.RunWorkerAsync();             //background worker act as the server thread
            }
            

            

        }
/*
        public void serverMain()
        {
            IPEndPoint ipep = new IPEndPoint(IPAddress.Any, port);
            UdpClient uc = new UdpClient(ipep.Port);         
            while(!done)
            {
                
                IPEndPoint senderIPEP = new IPEndPoint(IPAddress.Any, port);
                byte[] b = uc.Receive(ref senderIPEP);
                packet p = array2packet(b);
                p.senderIP = BitConverter.ToInt32(senderIPEP.Address.GetAddressBytes(), 0);
                while (backgroundWorker1.IsBusy) ;
                backgroundWorker1.RunWorkerAsync(p);

            }
        }
*/
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)             
        {
   
            while((!done) && (!backgroundWorker1.CancellationPending))
            {
                IPEndPoint senderIPEP = new IPEndPoint(IPAddress.Any, port);
                backgroundWorker1.ReportProgress(0, "Receiving packet");
                byte[] b = uc.Receive(ref senderIPEP);                  //blocking call that receive a packet
                packet p = array2packet(b);                             //convert received byte array to pakcet structure
                senderIPEP = new IPEndPoint(new IPAddress(BitConverter.GetBytes(p.senderIP)),port);  //get the sender ip end point
                
                switch (p.code)                 //Switch jobs according to the p.code 
                {
                    case (byte)packetCode.Ack:                  //acknowledgement packet, just for testing
                        backgroundWorker1.ReportProgress(0, "Received Ack");
                        packet reply = new packet();            //build a reply packet
                        reply.code = (byte)packetCode.Reply;
                        reply.senderIP = localIP == "" ? 0 : BitConverter.ToInt32(IPAddress.Parse(localIP).GetAddressBytes(), 0);
                        reply.frameNo = 0;
                        reply.order = 0;
                        reply.total = 1;
                        reply.size = 1;
                        reply.data = new byte[reply.size];
                        reply.data[0] = 0;
                        b = packet2array(reply);
                        uc.Send(b, b.Length, senderIPEP);
                        break;
                    case (byte)packetCode.Reply:            //Reply acknowledgement packet, just for testing
                        backgroundWorker1.ReportProgress(0, "Received Ack reply");
                        break;
                    case (byte)packetCode.TestRequest:      //Packet that ask for the interleaveing test (1.ppm)
                        sendTest(senderIPEP, p.data[0],p.data[1]);
                        backgroundWorker1.ReportProgress(0, "Acknowledge test request");
                        break;
                    case (byte)packetCode.TestFilePart:     //Packet of part of 1.ppm
                        if (testppm == null)
                        {
                            testppm = new byte[p.total * packet_size];      //Construct the array for storing 1.ppm
                            packet_count = 0;
                        }
                        assembleTest(p);                    //Assemble the packet
                        if (packet_count == p.total)        //If received enough packet
                        {
                            backgroundWorker1.ReportProgress(0, "Display test ppm");        //send signal to rais the progress changed event to display the image
                        }
                        break;
                    //TO DO add other packet code necessary for streaming video and audio, unlike sending 1.ppm, sending video stream should open new thread
                }
            }



        }
        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)       //Run when backgroundworker report progress
        {
            if ((string)e.UserState=="Receiving packet")
            {
                toolStripStatusLabel2.Text = (string)e.UserState;
            }
            else
            {
                toolStripStatusLabel2.Text = "";
                textBox1.Text = (string)e.UserState;
            }
            
            if ((string)e.UserState == "Display test ppm")
            {                                                               //Read the ppm file and build a bitmap to display
                using (MemoryStream ms = new MemoryStream(testppm))
                {
                    using (BinaryReader br = new BinaryReader(ms))
                    {
                        short magic = br.ReadInt16();
                        ms.Seek(1, SeekOrigin.Current);
                        int width = int.Parse(Encoding.ASCII.GetString(br.ReadBytes(3)));
                        ms.Seek(1, SeekOrigin.Current);
                        int height = int.Parse(Encoding.ASCII.GetString(br.ReadBytes(3)));
                        ms.Seek(1, SeekOrigin.Current);
                        int max = int.Parse(Encoding.ASCII.GetString(br.ReadBytes(3)));
                        ms.Seek(1, SeekOrigin.Current);
                        Bitmap bmp = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);           
                        for (int i = 0; i < height; i++)
                        {
                            for (int j = 0; j < width; j++)
                            {
                                try
                                {
                                    int r, g, b;
                                    r = br.ReadByte();                        
                                    g = br.ReadByte();
                                    b = br.ReadByte();
                                    bmp.SetPixel(j, i, Color.FromArgb(255, r, g, b));
                                }
                                catch(System.Exception ex)
                                {
                                    textBox1.Text = i.ToString() + j.ToString();
                                }
                            }
                        }               
                        pictureBox1.Image = bmp;
                    }

                    
                }



            }

        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            
        }

        public void ClientMain(String ip)
        {
 //           IPEndPoint ipep = new IPEndPoint(IPAddress.Parse(ip), port);
 //           UdpClient uc = new UdpClient();

        }
    }
}

