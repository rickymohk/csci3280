using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace test1
{
    public partial class Form2 : Form
    {
        public string[][] peerIP { get; set; }
        private Form1 parent;
        public Form2()
        {
            InitializeComponent();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void connectForm_Load(object sender, EventArgs e)
        {
            parent = (Form1)this.Owner;
            this.Location = this.Owner.Location;
            this.Left += this.Owner.ClientSize.Width / 2 - this.Width / 2;
            this.Top += this.Owner.ClientSize.Height / 2 - this.Height / 2;
        }

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            string[] ip = new string[2];
            ip[0] = textBox1.Text;
            ip[1] = textBox2.Text;
            string[] separators = new string[] { "." };
            string[][] ipaddr = new string[2][];
            bool valid = true;
            ipaddr[0] = ip[0].Split(separators, StringSplitOptions.None);
            ipaddr[1] = ip[1].Split(separators, StringSplitOptions.None);
            for(int i=0;i<ipaddr.Length;i++)
            {
                valid = valid && checkIP(ipaddr[i]);
            }
            if(valid)
            {
                parent.setPeerIP(ip);
                this.Close();
            }
            else
            {
                MessageBox.Show("Please enter valid ip addresses");
            }

        }

        private bool checkIP(string[] addr)
        {
            if(addr.Length!=4)
            {
                return false;
            }
            else
            {
                int[] ip = new int[4];
                for(int i=0;i<4;i++)
                {
                    if(Int32.TryParse(addr[i], out ip[i]))
                    {
                        if(ip[i]<0 || ip[i]>255)
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                return true;
            }

        }
    }
}
