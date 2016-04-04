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
    public partial class popupForm : Form
    {
        public string title = string.Empty, singer = string.Empty, album = string.Empty;
        public popupForm()
        {
            InitializeComponent();
        }

        private void popupForm_Load(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void RetrieveInput_Click(object sender, EventArgs e)
        {
            title = title_input.Text.Trim();
            singer = singer_input.Text.Trim();
            album = album_input.Text.Trim();
        }

        private void title_input_TextChanged(object sender, EventArgs e)
        {

        }

        private void singer_input_TextChanged(object sender, EventArgs e)
        {

        }

        private void album_input_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
