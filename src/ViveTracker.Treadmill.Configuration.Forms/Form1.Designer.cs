namespace ViveTracker.Treadmill.Configuration.Forms
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.unityHWNDLabel = new System.Windows.Forms.Label();
            this.panel1 = new ViveTracker.Treadmill.Configuration.Forms.SelectablePanel();
            this.rootContainer = new System.Windows.Forms.SplitContainer();
            this.selectablePanel1 = new ViveTracker.Treadmill.Configuration.Forms.SelectablePanel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rootContainer)).BeginInit();
            this.rootContainer.Panel1.SuspendLayout();
            this.rootContainer.Panel2.SuspendLayout();
            this.rootContainer.SuspendLayout();
            this.selectablePanel1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.panel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.unityHWNDLabel);
            this.splitContainer1.Size = new System.Drawing.Size(908, 449);
            this.splitContainer1.SplitterDistance = 449;
            this.splitContainer1.TabIndex = 1;
            // 
            // unityHWNDLabel
            // 
            this.unityHWNDLabel.AutoSize = true;
            this.unityHWNDLabel.Location = new System.Drawing.Point(3, 9);
            this.unityHWNDLabel.Name = "unityHWNDLabel";
            this.unityHWNDLabel.Size = new System.Drawing.Size(35, 13);
            this.unityHWNDLabel.TabIndex = 1;
            this.unityHWNDLabel.Text = "label2";
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(449, 449);
            this.panel1.TabIndex = 1;
            this.panel1.TabStop = true;
            this.panel1.Resize += new System.EventHandler(this.panel1_Resize);
            // 
            // rootContainer
            // 
            this.rootContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rootContainer.Location = new System.Drawing.Point(0, 0);
            this.rootContainer.Name = "rootContainer";
            this.rootContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // rootContainer.Panel1
            // 
            this.rootContainer.Panel1.Controls.Add(this.selectablePanel1);
            // 
            // rootContainer.Panel2
            // 
            this.rootContainer.Panel2.Controls.Add(this.splitContainer1);
            this.rootContainer.Size = new System.Drawing.Size(908, 478);
            this.rootContainer.SplitterDistance = 25;
            this.rootContainer.TabIndex = 2;
            // 
            // selectablePanel1
            // 
            this.selectablePanel1.Controls.Add(this.menuStrip1);
            this.selectablePanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.selectablePanel1.Location = new System.Drawing.Point(0, 0);
            this.selectablePanel1.Name = "selectablePanel1";
            this.selectablePanel1.Size = new System.Drawing.Size(908, 25);
            this.selectablePanel1.TabIndex = 1;
            this.selectablePanel1.TabStop = true;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(908, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(908, 478);
            this.Controls.Add(this.rootContainer);
            this.Name = "Form1";
            this.Text = "ViveTracker Gamepad Configurator";
            this.Activated += new System.EventHandler(this.Form1_Activated);
            this.Deactivate += new System.EventHandler(this.Form1_Deactivate);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.rootContainer.Panel1.ResumeLayout(false);
            this.rootContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.rootContainer)).EndInit();
            this.rootContainer.ResumeLayout(false);
            this.selectablePanel1.ResumeLayout(false);
            this.selectablePanel1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label unityHWNDLabel;
        private SelectablePanel panel1;
        private System.Windows.Forms.SplitContainer rootContainer;
        private SelectablePanel selectablePanel1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
    }
}

