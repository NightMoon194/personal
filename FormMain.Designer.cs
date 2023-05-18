namespace BookManage
{
    partial class FormMain
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.基础数据ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.图书信息维护ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.书籍类别信息ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.书籍库存信息ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.读者信息维护ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.还书ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.借书信息ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.还书信息ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.罚单查询ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.修改密码ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.退出系统ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tssl_lognUser = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tssl_time = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel8 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tssl_type = new System.Windows.Forms.ToolStripStatusLabel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.基础数据ToolStripMenuItem,
            this.还书ToolStripMenuItem,
            this.修改密码ToolStripMenuItem,
            this.退出系统ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(600, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 基础数据ToolStripMenuItem
            // 
            this.基础数据ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.图书信息维护ToolStripMenuItem,
            this.读者信息维护ToolStripMenuItem});
            this.基础数据ToolStripMenuItem.Name = "基础数据ToolStripMenuItem";
            this.基础数据ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.基础数据ToolStripMenuItem.Text = "数据维护";
            // 
            // 图书信息维护ToolStripMenuItem
            // 
            this.图书信息维护ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.书籍类别信息ToolStripMenuItem,
            this.书籍库存信息ToolStripMenuItem});
            this.图书信息维护ToolStripMenuItem.Name = "图书信息维护ToolStripMenuItem";
            this.图书信息维护ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.图书信息维护ToolStripMenuItem.Text = "图书信息维护";
            // 
            // 书籍类别信息ToolStripMenuItem
            // 
            this.书籍类别信息ToolStripMenuItem.Name = "书籍类别信息ToolStripMenuItem";
            this.书籍类别信息ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.书籍类别信息ToolStripMenuItem.Text = "书籍类别信息";
            this.书籍类别信息ToolStripMenuItem.Click += new System.EventHandler(this.书籍类别信息ToolStripMenuItem_Click);
            // 
            // 书籍库存信息ToolStripMenuItem
            // 
            this.书籍库存信息ToolStripMenuItem.Name = "书籍库存信息ToolStripMenuItem";
            this.书籍库存信息ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.书籍库存信息ToolStripMenuItem.Text = "书籍库存信息";
            this.书籍库存信息ToolStripMenuItem.Click += new System.EventHandler(this.书籍库存信息ToolStripMenuItem_Click);
            // 
            // 读者信息维护ToolStripMenuItem
            // 
            this.读者信息维护ToolStripMenuItem.Name = "读者信息维护ToolStripMenuItem";
            this.读者信息维护ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.读者信息维护ToolStripMenuItem.Text = "读者信息维护";
            this.读者信息维护ToolStripMenuItem.Click += new System.EventHandler(this.读者信息维护ToolStripMenuItem_Click);
            // 
            // 还书ToolStripMenuItem
            // 
            this.还书ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.借书信息ToolStripMenuItem,
            this.还书信息ToolStripMenuItem,
            this.toolStripMenuItem1,
            this.罚单查询ToolStripMenuItem});
            this.还书ToolStripMenuItem.Name = "还书ToolStripMenuItem";
            this.还书ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.还书ToolStripMenuItem.Text = "书籍管理";
            // 
            // 借书信息ToolStripMenuItem
            // 
            this.借书信息ToolStripMenuItem.Name = "借书信息ToolStripMenuItem";
            this.借书信息ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.借书信息ToolStripMenuItem.Text = "借书信息";
            this.借书信息ToolStripMenuItem.Click += new System.EventHandler(this.借书信息ToolStripMenuItem_Click);
            // 
            // 还书信息ToolStripMenuItem
            // 
            this.还书信息ToolStripMenuItem.Name = "还书信息ToolStripMenuItem";
            this.还书信息ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.还书信息ToolStripMenuItem.Text = "还书信息";
            this.还书信息ToolStripMenuItem.Click += new System.EventHandler(this.还书信息ToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(177, 6);
            // 
            // 罚单查询ToolStripMenuItem
            // 
            this.罚单查询ToolStripMenuItem.Name = "罚单查询ToolStripMenuItem";
            this.罚单查询ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.罚单查询ToolStripMenuItem.Text = "罚单查询";
            this.罚单查询ToolStripMenuItem.Click += new System.EventHandler(this.罚单查询ToolStripMenuItem_Click);
            // 
            // 修改密码ToolStripMenuItem
            // 
            this.修改密码ToolStripMenuItem.Name = "修改密码ToolStripMenuItem";
            this.修改密码ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.修改密码ToolStripMenuItem.Text = "修改密码";
            this.修改密码ToolStripMenuItem.Click += new System.EventHandler(this.修改密码ToolStripMenuItem_Click);
            // 
            // 退出系统ToolStripMenuItem
            // 
            this.退出系统ToolStripMenuItem.Name = "退出系统ToolStripMenuItem";
            this.退出系统ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.退出系统ToolStripMenuItem.Text = "退出系统";
            this.退出系统ToolStripMenuItem.Click += new System.EventHandler(this.退出系统ToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel2,
            this.tssl_lognUser,
            this.toolStripStatusLabel1,
            this.tssl_time,
            this.toolStripStatusLabel8,
            this.tssl_type});
            this.statusStrip1.Location = new System.Drawing.Point(0, 338);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 10, 0);
            this.statusStrip1.Size = new System.Drawing.Size(600, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(68, 17);
            this.toolStripStatusLabel2.Text = "登录用户：";
            // 
            // tssl_lognUser
            // 
            this.tssl_lognUser.Name = "tssl_lognUser";
            this.tssl_lognUser.Size = new System.Drawing.Size(44, 17);
            this.tssl_lognUser.Text = "管理员";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(68, 17);
            this.toolStripStatusLabel1.Text = "当前日期：";
            // 
            // tssl_time
            // 
            this.tssl_time.Name = "tssl_time";
            this.tssl_time.Size = new System.Drawing.Size(124, 17);
            this.tssl_time.Text = "2020年12月02日8:00";
            // 
            // toolStripStatusLabel8
            // 
            this.toolStripStatusLabel8.Name = "toolStripStatusLabel8";
            this.toolStripStatusLabel8.Size = new System.Drawing.Size(68, 17);
            this.toolStripStatusLabel8.Text = "用户类别：";
            // 
            // tssl_type
            // 
            this.tssl_type.Name = "tssl_type";
            this.tssl_type.Size = new System.Drawing.Size(44, 17);
            this.tssl_type.Text = "管理员";
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(600, 360);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "FormMain";
            this.Text = "图书管理系统-管理员";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 基础数据ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 图书信息维护ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 书籍类别信息ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 书籍库存信息ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 读者信息维护ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 还书ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 借书信息ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 还书信息ToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel tssl_time;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel tssl_lognUser;
        private System.Windows.Forms.ToolStripMenuItem 退出系统ToolStripMenuItem;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 罚单查询ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 修改密码ToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel8;
        private System.Windows.Forms.ToolStripStatusLabel tssl_type;
    }
}