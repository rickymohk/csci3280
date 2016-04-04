namespace test1
{
    partial class popupForm
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
            this.title_input = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.singer_input = new System.Windows.Forms.TextBox();
            this.album_input = new System.Windows.Forms.TextBox();
            this.RetrieveInput = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // title_input
            // 
            this.title_input.Location = new System.Drawing.Point(69, 9);
            this.title_input.Name = "title_input";
            this.title_input.Size = new System.Drawing.Size(172, 22);
            this.title_input.TabIndex = 0;
            this.title_input.TextChanged += new System.EventHandler(this.title_input_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "Title";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "Singer";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 72);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "Album";
            // 
            // singer_input
            // 
            this.singer_input.Location = new System.Drawing.Point(69, 37);
            this.singer_input.Name = "singer_input";
            this.singer_input.Size = new System.Drawing.Size(172, 22);
            this.singer_input.TabIndex = 4;
            this.singer_input.TextChanged += new System.EventHandler(this.singer_input_TextChanged);
            // 
            // album_input
            // 
            this.album_input.Location = new System.Drawing.Point(69, 65);
            this.album_input.Name = "album_input";
            this.album_input.Size = new System.Drawing.Size(172, 22);
            this.album_input.TabIndex = 5;
            this.album_input.TextChanged += new System.EventHandler(this.album_input_TextChanged);
            // 
            // RetrieveInput
            // 
            this.RetrieveInput.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.RetrieveInput.Location = new System.Drawing.Point(192, 93);
            this.RetrieveInput.Name = "RetrieveInput";
            this.RetrieveInput.Size = new System.Drawing.Size(49, 22);
            this.RetrieveInput.TabIndex = 6;
            this.RetrieveInput.Text = "Finish";
            this.RetrieveInput.UseVisualStyleBackColor = true;
            this.RetrieveInput.Click += new System.EventHandler(this.RetrieveInput_Click);
            // 
            // popupForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(253, 121);
            this.Controls.Add(this.RetrieveInput);
            this.Controls.Add(this.album_input);
            this.Controls.Add(this.singer_input);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.title_input);
            this.Name = "popupForm";
            this.Text = "popupForm";
            this.Load += new System.EventHandler(this.popupForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox title_input;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox singer_input;
        private System.Windows.Forms.TextBox album_input;
        private System.Windows.Forms.Button RetrieveInput;
    }
}