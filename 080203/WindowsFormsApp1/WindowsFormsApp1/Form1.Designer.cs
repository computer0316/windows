namespace WindowsFormsApp1
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.titleLabel = new System.Windows.Forms.Label();
            this.startButton = new System.Windows.Forms.Button();
            this.stopButton = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.加载初始数据ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.退出ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logoPictureBox = new System.Windows.Forms.PictureBox();
            this.dataLabel10 = new System.Windows.Forms.Label();
            this.dataLabel1 = new System.Windows.Forms.Label();
            this.dataLabel2 = new System.Windows.Forms.Label();
            this.dataLabel3 = new System.Windows.Forms.Label();
            this.dataLabel4 = new System.Windows.Forms.Label();
            this.dataLabel5 = new System.Windows.Forms.Label();
            this.dataLabel6 = new System.Windows.Forms.Label();
            this.dataLabel7 = new System.Windows.Forms.Label();
            this.dataLabel8 = new System.Windows.Forms.Label();
            this.dataLabel9 = new System.Windows.Forms.Label();
            this.roundLabel = new System.Windows.Forms.Label();
            this.pringButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logoPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // titleLabel
            // 
            this.titleLabel.BackColor = System.Drawing.Color.Transparent;
            this.titleLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.titleLabel.Font = new System.Drawing.Font("宋体", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.titleLabel.ForeColor = System.Drawing.Color.Gold;
            this.titleLabel.Location = new System.Drawing.Point(0, 0);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(1372, 160);
            this.titleLabel.TabIndex = 0;
            this.titleLabel.Text = "莱恩公司摇号软件";
            this.titleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // startButton
            // 
            this.startButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.startButton.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.startButton.Location = new System.Drawing.Point(892, 690);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(145, 56);
            this.startButton.TabIndex = 2;
            this.startButton.Text = "开  始";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // stopButton
            // 
            this.stopButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.stopButton.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.stopButton.Location = new System.Drawing.Point(1140, 690);
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(145, 56);
            this.stopButton.TabIndex = 3;
            this.stopButton.Text = "停  止";
            this.stopButton.UseVisualStyleBackColor = true;
            this.stopButton.Click += new System.EventHandler(this.StopButton_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1372, 780);
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.加载初始数据ToolStripMenuItem,
            this.退出ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(149, 48);
            // 
            // 加载初始数据ToolStripMenuItem
            // 
            this.加载初始数据ToolStripMenuItem.Name = "加载初始数据ToolStripMenuItem";
            this.加载初始数据ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.加载初始数据ToolStripMenuItem.Text = "加载初始数据";
            this.加载初始数据ToolStripMenuItem.Click += new System.EventHandler(this.加载初始数据ToolStripMenuItem_Click);
            // 
            // 退出ToolStripMenuItem
            // 
            this.退出ToolStripMenuItem.Name = "退出ToolStripMenuItem";
            this.退出ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.退出ToolStripMenuItem.Text = "退出";
            this.退出ToolStripMenuItem.Click += new System.EventHandler(this.退出ToolStripMenuItem_Click);
            // 
            // logoPictureBox
            // 
            this.logoPictureBox.Location = new System.Drawing.Point(43, 41);
            this.logoPictureBox.Name = "logoPictureBox";
            this.logoPictureBox.Size = new System.Drawing.Size(179, 73);
            this.logoPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.logoPictureBox.TabIndex = 6;
            this.logoPictureBox.TabStop = false;
            // 
            // dataLabel10
            // 
            this.dataLabel10.AutoSize = true;
            this.dataLabel10.BackColor = System.Drawing.Color.Transparent;
            this.dataLabel10.Font = new System.Drawing.Font("宋体", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dataLabel10.ForeColor = System.Drawing.Color.White;
            this.dataLabel10.Location = new System.Drawing.Point(1006, 512);
            this.dataLabel10.Name = "dataLabel10";
            this.dataLabel10.Size = new System.Drawing.Size(295, 48);
            this.dataLabel10.TabIndex = 7;
            this.dataLabel10.Text = "dataLabel10";
            // 
            // dataLabel1
            // 
            this.dataLabel1.AutoSize = true;
            this.dataLabel1.BackColor = System.Drawing.Color.Transparent;
            this.dataLabel1.Font = new System.Drawing.Font("宋体", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dataLabel1.ForeColor = System.Drawing.Color.White;
            this.dataLabel1.Location = new System.Drawing.Point(158, 206);
            this.dataLabel1.Name = "dataLabel1";
            this.dataLabel1.Size = new System.Drawing.Size(270, 48);
            this.dataLabel1.TabIndex = 8;
            this.dataLabel1.Text = "dataLabel1";
            // 
            // dataLabel2
            // 
            this.dataLabel2.AutoSize = true;
            this.dataLabel2.BackColor = System.Drawing.Color.Transparent;
            this.dataLabel2.Font = new System.Drawing.Font("宋体", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dataLabel2.ForeColor = System.Drawing.Color.White;
            this.dataLabel2.Location = new System.Drawing.Point(158, 274);
            this.dataLabel2.Name = "dataLabel2";
            this.dataLabel2.Size = new System.Drawing.Size(270, 48);
            this.dataLabel2.TabIndex = 9;
            this.dataLabel2.Text = "dataLabel2";
            // 
            // dataLabel3
            // 
            this.dataLabel3.AutoSize = true;
            this.dataLabel3.BackColor = System.Drawing.Color.Transparent;
            this.dataLabel3.Font = new System.Drawing.Font("宋体", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dataLabel3.ForeColor = System.Drawing.Color.White;
            this.dataLabel3.Location = new System.Drawing.Point(158, 349);
            this.dataLabel3.Name = "dataLabel3";
            this.dataLabel3.Size = new System.Drawing.Size(270, 48);
            this.dataLabel3.TabIndex = 10;
            this.dataLabel3.Text = "dataLabel3";
            // 
            // dataLabel4
            // 
            this.dataLabel4.AutoSize = true;
            this.dataLabel4.BackColor = System.Drawing.Color.Transparent;
            this.dataLabel4.Font = new System.Drawing.Font("宋体", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dataLabel4.ForeColor = System.Drawing.Color.White;
            this.dataLabel4.Location = new System.Drawing.Point(158, 432);
            this.dataLabel4.Name = "dataLabel4";
            this.dataLabel4.Size = new System.Drawing.Size(270, 48);
            this.dataLabel4.TabIndex = 11;
            this.dataLabel4.Text = "dataLabel4";
            // 
            // dataLabel5
            // 
            this.dataLabel5.AutoSize = true;
            this.dataLabel5.BackColor = System.Drawing.Color.Transparent;
            this.dataLabel5.Font = new System.Drawing.Font("宋体", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dataLabel5.ForeColor = System.Drawing.Color.White;
            this.dataLabel5.Location = new System.Drawing.Point(158, 512);
            this.dataLabel5.Name = "dataLabel5";
            this.dataLabel5.Size = new System.Drawing.Size(270, 48);
            this.dataLabel5.TabIndex = 12;
            this.dataLabel5.Text = "dataLabel5";
            // 
            // dataLabel6
            // 
            this.dataLabel6.AutoSize = true;
            this.dataLabel6.BackColor = System.Drawing.Color.Transparent;
            this.dataLabel6.Font = new System.Drawing.Font("宋体", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dataLabel6.ForeColor = System.Drawing.Color.White;
            this.dataLabel6.Location = new System.Drawing.Point(1006, 206);
            this.dataLabel6.Name = "dataLabel6";
            this.dataLabel6.Size = new System.Drawing.Size(270, 48);
            this.dataLabel6.TabIndex = 13;
            this.dataLabel6.Text = "dataLabel6";
            // 
            // dataLabel7
            // 
            this.dataLabel7.AutoSize = true;
            this.dataLabel7.BackColor = System.Drawing.Color.Transparent;
            this.dataLabel7.Font = new System.Drawing.Font("宋体", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dataLabel7.ForeColor = System.Drawing.Color.White;
            this.dataLabel7.Location = new System.Drawing.Point(1006, 274);
            this.dataLabel7.Name = "dataLabel7";
            this.dataLabel7.Size = new System.Drawing.Size(270, 48);
            this.dataLabel7.TabIndex = 14;
            this.dataLabel7.Text = "dataLabel7";
            // 
            // dataLabel8
            // 
            this.dataLabel8.AutoSize = true;
            this.dataLabel8.BackColor = System.Drawing.Color.Transparent;
            this.dataLabel8.Font = new System.Drawing.Font("宋体", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dataLabel8.ForeColor = System.Drawing.Color.White;
            this.dataLabel8.Location = new System.Drawing.Point(1006, 349);
            this.dataLabel8.Name = "dataLabel8";
            this.dataLabel8.Size = new System.Drawing.Size(270, 48);
            this.dataLabel8.TabIndex = 15;
            this.dataLabel8.Text = "dataLabel8";
            // 
            // dataLabel9
            // 
            this.dataLabel9.AutoSize = true;
            this.dataLabel9.BackColor = System.Drawing.Color.Transparent;
            this.dataLabel9.Font = new System.Drawing.Font("宋体", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dataLabel9.ForeColor = System.Drawing.Color.White;
            this.dataLabel9.Location = new System.Drawing.Point(1006, 432);
            this.dataLabel9.Name = "dataLabel9";
            this.dataLabel9.Size = new System.Drawing.Size(270, 48);
            this.dataLabel9.TabIndex = 16;
            this.dataLabel9.Text = "dataLabel9";
            // 
            // roundLabel
            // 
            this.roundLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.roundLabel.AutoSize = true;
            this.roundLabel.BackColor = System.Drawing.Color.Transparent;
            this.roundLabel.Font = new System.Drawing.Font("宋体", 42F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.roundLabel.ForeColor = System.Drawing.Color.White;
            this.roundLabel.Location = new System.Drawing.Point(171, 690);
            this.roundLabel.Name = "roundLabel";
            this.roundLabel.Size = new System.Drawing.Size(304, 56);
            this.roundLabel.TabIndex = 17;
            this.roundLabel.Text = "roundLabel";
            // 
            // pringButton
            // 
            this.pringButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pringButton.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.pringButton.Location = new System.Drawing.Point(644, 690);
            this.pringButton.Name = "pringButton";
            this.pringButton.Size = new System.Drawing.Size(145, 56);
            this.pringButton.TabIndex = 18;
            this.pringButton.Text = "打印结果";
            this.pringButton.UseVisualStyleBackColor = true;
            this.pringButton.Click += new System.EventHandler(this.pringButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1372, 780);
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.Controls.Add(this.pringButton);
            this.Controls.Add(this.roundLabel);
            this.Controls.Add(this.dataLabel9);
            this.Controls.Add(this.dataLabel8);
            this.Controls.Add(this.dataLabel7);
            this.Controls.Add(this.dataLabel6);
            this.Controls.Add(this.dataLabel5);
            this.Controls.Add(this.dataLabel4);
            this.Controls.Add(this.dataLabel3);
            this.Controls.Add(this.dataLabel2);
            this.Controls.Add(this.dataLabel1);
            this.Controls.Add(this.dataLabel10);
            this.Controls.Add(this.logoPictureBox);
            this.Controls.Add(this.stopButton);
            this.Controls.Add(this.titleLabel);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.logoPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.Button stopButton;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 退出ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 加载初始数据ToolStripMenuItem;
        private System.Windows.Forms.PictureBox logoPictureBox;
        private System.Windows.Forms.Label dataLabel10;
        private System.Windows.Forms.Label dataLabel1;
        private System.Windows.Forms.Label dataLabel2;
        private System.Windows.Forms.Label dataLabel3;
        private System.Windows.Forms.Label dataLabel4;
        private System.Windows.Forms.Label dataLabel5;
        private System.Windows.Forms.Label dataLabel6;
        private System.Windows.Forms.Label dataLabel7;
        private System.Windows.Forms.Label dataLabel8;
        private System.Windows.Forms.Label dataLabel9;
        private System.Windows.Forms.Label roundLabel;
        private System.Windows.Forms.Button pringButton;
    }
}

