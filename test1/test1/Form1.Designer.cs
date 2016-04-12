namespace test1
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.saveScreenShot = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.addSong = new System.Windows.Forms.Button();
            this.songList = new System.Windows.Forms.DataGridView();
            this.path = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.title = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.singer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.album = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.filesFToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip2 = new System.Windows.Forms.MenuStrip();
            this.connectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.p2PConnectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.button1 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.deleteList = new System.Windows.Forms.Button();
            this.info = new System.Windows.Forms.Label();
            this.Search = new System.Windows.Forms.Button();
            this.SearchBox = new System.Windows.Forms.TextBox();
            this.SearchList = new System.Windows.Forms.DataGridView();
            this.search_path = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Search_Title = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Search_Singer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Serach_Album = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.songList)).BeginInit();
            this.flowLayoutPanel2.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.menuStrip2.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SearchList)).BeginInit();
            this.SuspendLayout();
            // 
            // saveScreenShot
            // 
            this.saveScreenShot.Filter = "JPeg Image|*.jpg|Bitmap Image|*.bmp|Gif Image|*.gif|PNG Image|*.png";
            this.saveScreenShot.Title = "Save Screenshot";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "AVI File|*.avi|Play List|*.txt";
            this.openFileDialog1.Title = "Open File";
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // addSong
            // 
            this.addSong.Dock = System.Windows.Forms.DockStyle.Fill;
            this.addSong.Enabled = false;
            this.addSong.Location = new System.Drawing.Point(371, 304);
            this.addSong.Name = "addSong";
            this.addSong.Size = new System.Drawing.Size(137, 28);
            this.addSong.TabIndex = 15;
            this.addSong.Text = "Add Song to List";
            this.addSong.UseVisualStyleBackColor = true;
            this.addSong.Click += new System.EventHandler(this.addSong_Click);
            // 
            // songList
            // 
            this.songList.AllowUserToOrderColumns = true;
            this.songList.BackgroundColor = System.Drawing.Color.White;
            this.songList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.songList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.path,
            this.title,
            this.singer,
            this.album});
            this.tableLayoutPanel1.SetColumnSpan(this.songList, 2);
            this.songList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.songList.Location = new System.Drawing.Point(371, 31);
            this.songList.Name = "songList";
            this.songList.ReadOnly = true;
            this.songList.RowHeadersVisible = false;
            this.tableLayoutPanel1.SetRowSpan(this.songList, 2);
            this.songList.RowTemplate.Height = 24;
            this.songList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.songList.Size = new System.Drawing.Size(285, 267);
            this.songList.TabIndex = 14;
            this.songList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.songList_CellClick_1);
            this.songList.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.songList_CellContentClick);
            this.songList.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.songList_CellDoubleClick);
            // 
            // path
            // 
            this.path.HeaderText = "Path";
            this.path.Name = "path";
            this.path.ReadOnly = true;
            this.path.Visible = false;
            // 
            // title
            // 
            this.title.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.title.HeaderText = "Title";
            this.title.Name = "title";
            this.title.ReadOnly = true;
            // 
            // singer
            // 
            this.singer.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.singer.HeaderText = "Singer";
            this.singer.Name = "singer";
            this.singer.ReadOnly = true;
            // 
            // album
            // 
            this.album.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.album.HeaderText = "Album";
            this.album.Name = "album";
            this.album.ReadOnly = true;
            // 
            // flowLayoutPanel2
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.flowLayoutPanel2, 6);
            this.flowLayoutPanel2.Controls.Add(this.menuStrip1);
            this.flowLayoutPanel2.Controls.Add(this.menuStrip2);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(653, 22);
            this.flowLayoutPanel2.TabIndex = 13;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.filesFToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(69, 22);
            this.menuStrip1.TabIndex = 7;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // filesFToolStripMenuItem
            // 
            this.filesFToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openFileToolStripMenuItem});
            this.filesFToolStripMenuItem.Name = "filesFToolStripMenuItem";
            this.filesFToolStripMenuItem.ShortcutKeyDisplayString = "F";
            this.filesFToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F)));
            this.filesFToolStripMenuItem.Size = new System.Drawing.Size(61, 18);
            this.filesFToolStripMenuItem.Text = "Files (F)";
            this.filesFToolStripMenuItem.Click += new System.EventHandler(this.filesFToolStripMenuItem_Click);
            // 
            // openFileToolStripMenuItem
            // 
            this.openFileToolStripMenuItem.Name = "openFileToolStripMenuItem";
            this.openFileToolStripMenuItem.Size = new System.Drawing.Size(108, 22);
            this.openFileToolStripMenuItem.Text = "Open";
            this.openFileToolStripMenuItem.Click += new System.EventHandler(this.openFileToolStripMenuItem_Click);
            // 
            // menuStrip2
            // 
            this.menuStrip2.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.connectionToolStripMenuItem});
            this.menuStrip2.Location = new System.Drawing.Point(69, 0);
            this.menuStrip2.Name = "menuStrip2";
            this.menuStrip2.Size = new System.Drawing.Size(112, 22);
            this.menuStrip2.TabIndex = 11;
            this.menuStrip2.Text = "menuStrip2";
            // 
            // connectionToolStripMenuItem
            // 
            this.connectionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.p2PConnectionToolStripMenuItem,
            this.testToolStripMenuItem});
            this.connectionToolStripMenuItem.Name = "connectionToolStripMenuItem";
            this.connectionToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.C)));
            this.connectionToolStripMenuItem.Size = new System.Drawing.Size(104, 18);
            this.connectionToolStripMenuItem.Text = "Connection (C)";
            // 
            // p2PConnectionToolStripMenuItem
            // 
            this.p2PConnectionToolStripMenuItem.Name = "p2PConnectionToolStripMenuItem";
            this.p2PConnectionToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.p2PConnectionToolStripMenuItem.Text = "P2P connection";
            this.p2PConnectionToolStripMenuItem.Click += new System.EventHandler(this.p2PConnectionToolStripMenuItem_Click);
            // 
            // testToolStripMenuItem
            // 
            this.testToolStripMenuItem.Name = "testToolStripMenuItem";
            this.testToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.testToolStripMenuItem.Text = "Test";
            this.testToolStripMenuItem.Click += new System.EventHandler(this.testToolStripMenuItem_Click);
            // 
            // flowLayoutPanel1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.flowLayoutPanel1, 3);
            this.flowLayoutPanel1.Controls.Add(this.button1);
            this.flowLayoutPanel1.Controls.Add(this.button3);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 304);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(270, 28);
            this.flowLayoutPanel1.TabIndex = 12;
            // 
            // button1
            // 
            this.button1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.button1.AutoSize = true;
            this.button1.Location = new System.Drawing.Point(3, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(50, 28);
            this.button1.TabIndex = 1;
            this.button1.Text = "Play";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button3
            // 
            this.button3.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.button3.Location = new System.Drawing.Point(59, 3);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(100, 28);
            this.button3.TabIndex = 5;
            this.button3.Text = "ScreenShot";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // trackBar1
            // 
            this.trackBar1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.trackBar1.Location = new System.Drawing.Point(279, 304);
            this.trackBar1.Maximum = 100;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(86, 28);
            this.trackBar1.TabIndex = 10;
            this.trackBar1.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.WindowText;
            this.tableLayoutPanel1.SetColumnSpan(this.pictureBox1, 4);
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(3, 51);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(362, 247);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 6;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.6713F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 26.38216F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 32.9777F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 24.96883F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.pictureBox1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.trackBar1, 3, 3);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.deleteList, 5, 3);
            this.tableLayoutPanel1.Controls.Add(this.addSong, 4, 3);
            this.tableLayoutPanel1.Controls.Add(this.songList, 4, 1);
            this.tableLayoutPanel1.Controls.Add(this.info, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.Search, 5, 4);
            this.tableLayoutPanel1.Controls.Add(this.SearchBox, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.SearchList, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.label1, 3, 4);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 163F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 18F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 37F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(659, 525);
            this.tableLayoutPanel1.TabIndex = 0;
            this.tableLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel1_Paint);
            // 
            // deleteList
            // 
            this.deleteList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.deleteList.Enabled = false;
            this.deleteList.Location = new System.Drawing.Point(514, 304);
            this.deleteList.Name = "deleteList";
            this.deleteList.Size = new System.Drawing.Size(142, 28);
            this.deleteList.TabIndex = 16;
            this.deleteList.Text = "Delete";
            this.deleteList.UseVisualStyleBackColor = true;
            this.deleteList.Click += new System.EventHandler(this.deleteList_Click);
            // 
            // info
            // 
            this.info.AutoSize = true;
            this.info.BackColor = System.Drawing.SystemColors.Info;
            this.tableLayoutPanel1.SetColumnSpan(this.info, 4);
            this.info.Dock = System.Windows.Forms.DockStyle.Fill;
            this.info.Location = new System.Drawing.Point(3, 28);
            this.info.Name = "info";
            this.info.Size = new System.Drawing.Size(362, 20);
            this.info.TabIndex = 17;
            this.info.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.info.Click += new System.EventHandler(this.label1_Click);
            // 
            // Search
            // 
            this.Search.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Search.Location = new System.Drawing.Point(514, 338);
            this.Search.Name = "Search";
            this.Search.Size = new System.Drawing.Size(142, 21);
            this.Search.TabIndex = 18;
            this.Search.Text = "Search";
            this.Search.UseVisualStyleBackColor = true;
            this.Search.Click += new System.EventHandler(this.Search_Click);
            // 
            // SearchBox
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.SearchBox, 3);
            this.SearchBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SearchBox.Location = new System.Drawing.Point(3, 338);
            this.SearchBox.Name = "SearchBox";
            this.SearchBox.Size = new System.Drawing.Size(270, 22);
            this.SearchBox.TabIndex = 19;
            this.SearchBox.TextChanged += new System.EventHandler(this.SearchBox_TextChanged);
            // 
            // SearchList
            // 
            this.SearchList.AllowUserToOrderColumns = true;
            this.SearchList.BackgroundColor = System.Drawing.Color.GhostWhite;
            this.SearchList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.SearchList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.search_path,
            this.Search_Title,
            this.Search_Singer,
            this.Serach_Album});
            this.tableLayoutPanel1.SetColumnSpan(this.SearchList, 6);
            this.SearchList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SearchList.GridColor = System.Drawing.SystemColors.ActiveCaption;
            this.SearchList.Location = new System.Drawing.Point(3, 365);
            this.SearchList.Name = "SearchList";
            this.SearchList.ReadOnly = true;
            this.SearchList.RowHeadersVisible = false;
            this.SearchList.RowTemplate.Height = 24;
            this.SearchList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.SearchList.Size = new System.Drawing.Size(653, 157);
            this.SearchList.TabIndex = 20;
            this.SearchList.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.SearchList_CellContentClick);
            this.SearchList.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.SearchList_CellDoubleClick);
            // 
            // search_path
            // 
            this.search_path.HeaderText = "Path";
            this.search_path.Name = "search_path";
            this.search_path.ReadOnly = true;
            this.search_path.Visible = false;
            this.search_path.Width = 200;
            // 
            // Search_Title
            // 
            this.Search_Title.HeaderText = "Title";
            this.Search_Title.Name = "Search_Title";
            this.Search_Title.ReadOnly = true;
            this.Search_Title.Width = 250;
            // 
            // Search_Singer
            // 
            this.Search_Singer.HeaderText = "Singer";
            this.Search_Singer.Name = "Search_Singer";
            this.Search_Singer.ReadOnly = true;
            this.Search_Singer.Width = 200;
            // 
            // Serach_Album
            // 
            this.Serach_Album.HeaderText = "Album";
            this.Serach_Album.Name = "Serach_Album";
            this.Serach_Album.ReadOnly = true;
            this.Serach_Album.Width = 200;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.label1, 2);
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(279, 335);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(229, 27);
            this.label1.TabIndex = 21;
            this.label1.Text = "Keywords should be divided by comma (e.g. ABC,DEF,GHI)";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(659, 525);
            this.Controls.Add(this.tableLayoutPanel1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Media Player";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.songList)).EndInit();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.menuStrip2.ResumeLayout(false);
            this.menuStrip2.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SearchList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.SaveFileDialog saveScreenShot;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Button addSong;
        private System.Windows.Forms.DataGridView songList;
        private System.Windows.Forms.DataGridViewTextBoxColumn path;
        private System.Windows.Forms.DataGridViewTextBoxColumn title;
        private System.Windows.Forms.DataGridViewTextBoxColumn singer;
        private System.Windows.Forms.DataGridViewTextBoxColumn album;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem filesFToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openFileToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip2;
        private System.Windows.Forms.ToolStripMenuItem connectionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem p2PConnectionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem testToolStripMenuItem;
        private System.Windows.Forms.Button deleteList;
        private System.Windows.Forms.Label info;
        private System.Windows.Forms.Button Search;
        private System.Windows.Forms.TextBox SearchBox;
        private System.Windows.Forms.DataGridView SearchList;
        private System.Windows.Forms.DataGridViewTextBoxColumn search_path;
        private System.Windows.Forms.DataGridViewTextBoxColumn Search_Title;
        private System.Windows.Forms.DataGridViewTextBoxColumn Search_Singer;
        private System.Windows.Forms.DataGridViewTextBoxColumn Serach_Album;
        private System.Windows.Forms.Label label1;
    }
}

