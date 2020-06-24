namespace AnyCAD.Basic
{
    partial class TestForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TestForm));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.importBtn = new System.Windows.Forms.ToolStripButton();
            this.mouseBtn = new System.Windows.Forms.ToolStripButton();
            this.moveBtn = new System.Windows.Forms.ToolStripButton();
            this.rotateBtn = new System.Windows.Forms.ToolStripButton();
            this.clearBtn = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.moveNodeBtn = new System.Windows.Forms.ToolStripButton();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.singlePickToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.multiPickToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hitTestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testBtn = new System.Windows.Forms.ToolStripButton();
            this.transOnMaxBtn = new System.Windows.Forms.ToolStripButton();
            this.transOnSelectBtn = new System.Windows.Forms.ToolStripButton();
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.panel4 = new System.Windows.Forms.Panel();
            this.btnReadXml = new System.Windows.Forms.Button();
            this.btnExportXml = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnDown = new System.Windows.Forms.Button();
            this.btnUp = new System.Windows.Forms.Button();
            this.txtLength = new System.Windows.Forms.TextBox();
            this.txtRadius = new System.Windows.Forms.TextBox();
            this.txtAngle = new System.Windows.Forms.TextBox();
            this.btnDraw = new System.Windows.Forms.Button();
            this.txtW = new System.Windows.Forms.TextBox();
            this.txtL = new System.Windows.Forms.TextBox();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.toolStrip1.SuspendLayout();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importBtn,
            this.mouseBtn,
            this.moveBtn,
            this.rotateBtn,
            this.clearBtn,
            this.toolStripSeparator1,
            this.moveNodeBtn,
            this.toolStripDropDownButton1,
            this.testBtn,
            this.transOnMaxBtn,
            this.transOnSelectBtn});
            this.toolStrip1.Location = new System.Drawing.Point(4, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(531, 31);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // importBtn
            // 
            this.importBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.importBtn.Image = ((System.Drawing.Image)(resources.GetObject("importBtn.Image")));
            this.importBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.importBtn.Name = "importBtn";
            this.importBtn.Size = new System.Drawing.Size(29, 28);
            this.importBtn.Text = "Import";
            this.importBtn.Click += new System.EventHandler(this.ImportToolStripMenuItem_Click);
            // 
            // mouseBtn
            // 
            this.mouseBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mouseBtn.Image = ((System.Drawing.Image)(resources.GetObject("mouseBtn.Image")));
            this.mouseBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mouseBtn.Name = "mouseBtn";
            this.mouseBtn.Size = new System.Drawing.Size(29, 28);
            this.mouseBtn.Text = "toolStripButton1";
            this.mouseBtn.Click += new System.EventHandler(this.MouseBtn_Click);
            // 
            // moveBtn
            // 
            this.moveBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.moveBtn.Image = ((System.Drawing.Image)(resources.GetObject("moveBtn.Image")));
            this.moveBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.moveBtn.Name = "moveBtn";
            this.moveBtn.Size = new System.Drawing.Size(29, 28);
            this.moveBtn.Text = "Move";
            this.moveBtn.Click += new System.EventHandler(this.PanToolStripMenuItem_Click);
            // 
            // rotateBtn
            // 
            this.rotateBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.rotateBtn.Image = ((System.Drawing.Image)(resources.GetObject("rotateBtn.Image")));
            this.rotateBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.rotateBtn.Name = "rotateBtn";
            this.rotateBtn.Size = new System.Drawing.Size(29, 28);
            this.rotateBtn.Text = "Rotate";
            this.rotateBtn.Click += new System.EventHandler(this.OrbitToolStripMenuItem_Click);
            // 
            // clearBtn
            // 
            this.clearBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.clearBtn.Image = ((System.Drawing.Image)(resources.GetObject("clearBtn.Image")));
            this.clearBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.clearBtn.Name = "clearBtn";
            this.clearBtn.Size = new System.Drawing.Size(29, 28);
            this.clearBtn.Text = "Clear";
            this.clearBtn.Click += new System.EventHandler(this.ClearToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 31);
            // 
            // moveNodeBtn
            // 
            this.moveNodeBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.moveNodeBtn.Image = ((System.Drawing.Image)(resources.GetObject("moveNodeBtn.Image")));
            this.moveNodeBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.moveNodeBtn.Name = "moveNodeBtn";
            this.moveNodeBtn.Size = new System.Drawing.Size(55, 28);
            this.moveNodeBtn.Text = "Move";
            this.moveNodeBtn.Click += new System.EventHandler(this.MoveNodeBtn_Click);
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.singlePickToolStripMenuItem,
            this.multiPickToolStripMenuItem,
            this.hitTestToolStripMenuItem});
            this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(52, 28);
            this.toolStripDropDownButton1.Text = "Pick";
            // 
            // singlePickToolStripMenuItem
            // 
            this.singlePickToolStripMenuItem.Name = "singlePickToolStripMenuItem";
            this.singlePickToolStripMenuItem.Size = new System.Drawing.Size(166, 26);
            this.singlePickToolStripMenuItem.Text = "SinglePick";
            this.singlePickToolStripMenuItem.Click += new System.EventHandler(this.SinglePickToolStripMenuItem_Click);
            // 
            // multiPickToolStripMenuItem
            // 
            this.multiPickToolStripMenuItem.Name = "multiPickToolStripMenuItem";
            this.multiPickToolStripMenuItem.Size = new System.Drawing.Size(166, 26);
            this.multiPickToolStripMenuItem.Text = "MultiPick";
            this.multiPickToolStripMenuItem.Click += new System.EventHandler(this.MultiPickToolStripMenuItem_Click);
            // 
            // hitTestToolStripMenuItem
            // 
            this.hitTestToolStripMenuItem.Name = "hitTestToolStripMenuItem";
            this.hitTestToolStripMenuItem.Size = new System.Drawing.Size(166, 26);
            this.hitTestToolStripMenuItem.Text = "Hit Test";
            this.hitTestToolStripMenuItem.Click += new System.EventHandler(this.HitTestToolStripMenuItem_Click);
            // 
            // testBtn
            // 
            this.testBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.testBtn.Image = ((System.Drawing.Image)(resources.GetObject("testBtn.Image")));
            this.testBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.testBtn.Name = "testBtn";
            this.testBtn.Size = new System.Drawing.Size(44, 28);
            this.testBtn.Text = "Test";
            this.testBtn.Click += new System.EventHandler(this.TestBtn_Click);
            // 
            // transOnMaxBtn
            // 
            this.transOnMaxBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.transOnMaxBtn.Image = ((System.Drawing.Image)(resources.GetObject("transOnMaxBtn.Image")));
            this.transOnMaxBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.transOnMaxBtn.Name = "transOnMaxBtn";
            this.transOnMaxBtn.Size = new System.Drawing.Size(101, 28);
            this.transOnMaxBtn.Text = "transOnMax";
            this.transOnMaxBtn.Click += new System.EventHandler(this.TransOnMaxBtn_Click);
            // 
            // transOnSelectBtn
            // 
            this.transOnSelectBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.transOnSelectBtn.Image = ((System.Drawing.Image)(resources.GetObject("transOnSelectBtn.Image")));
            this.transOnSelectBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.transOnSelectBtn.Name = "transOnSelectBtn";
            this.transOnSelectBtn.Size = new System.Drawing.Size(115, 28);
            this.transOnSelectBtn.Text = "transOnSelect";
            this.transOnSelectBtn.Click += new System.EventHandler(this.TransOnSelectBtn_Click);
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.splitContainer1);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(800, 419);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new System.Drawing.Size(800, 450);
            this.toolStripContainer1.TabIndex = 3;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip1);
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tabControl1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(800, 419);
            this.splitContainer1.SplitterDistance = 431;
            this.splitContainer1.TabIndex = 1;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(429, 417);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.panel1);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(421, 392);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "3D";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(415, 386);
            this.panel1.TabIndex = 0;
            this.panel1.SizeChanged += new System.EventHandler(this.Panel1_SizeChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.splitContainer3);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(421, 388);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "展开";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(3, 3);
            this.splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.panel4);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.btnReadXml);
            this.splitContainer3.Panel2.Controls.Add(this.btnExportXml);
            this.splitContainer3.Panel2.Controls.Add(this.label3);
            this.splitContainer3.Panel2.Controls.Add(this.label2);
            this.splitContainer3.Panel2.Controls.Add(this.label1);
            this.splitContainer3.Panel2.Controls.Add(this.btnDown);
            this.splitContainer3.Panel2.Controls.Add(this.btnUp);
            this.splitContainer3.Panel2.Controls.Add(this.txtLength);
            this.splitContainer3.Panel2.Controls.Add(this.txtRadius);
            this.splitContainer3.Panel2.Controls.Add(this.txtAngle);
            this.splitContainer3.Panel2.Controls.Add(this.btnDraw);
            this.splitContainer3.Panel2.Controls.Add(this.txtW);
            this.splitContainer3.Panel2.Controls.Add(this.txtL);
            this.splitContainer3.Size = new System.Drawing.Size(415, 382);
            this.splitContainer3.SplitterDistance = 137;
            this.splitContainer3.TabIndex = 0;
            // 
            // panel4
            // 
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(137, 382);
            this.panel4.TabIndex = 0;
            this.panel4.SizeChanged += new System.EventHandler(this.Panel4_SizeChanged);
            // 
            // btnReadXml
            // 
            this.btnReadXml.Location = new System.Drawing.Point(98, 307);
            this.btnReadXml.Name = "btnReadXml";
            this.btnReadXml.Size = new System.Drawing.Size(75, 23);
            this.btnReadXml.TabIndex = 12;
            this.btnReadXml.Text = "Read";
            this.btnReadXml.UseVisualStyleBackColor = true;
            this.btnReadXml.Click += new System.EventHandler(this.BtnReadXml_Click);
            // 
            // btnExportXml
            // 
            this.btnExportXml.Location = new System.Drawing.Point(98, 278);
            this.btnExportXml.Name = "btnExportXml";
            this.btnExportXml.Size = new System.Drawing.Size(75, 23);
            this.btnExportXml.TabIndex = 11;
            this.btnExportXml.Text = "Export";
            this.btnExportXml.UseVisualStyleBackColor = true;
            this.btnExportXml.Click += new System.EventHandler(this.BtnExportXml_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 214);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(15, 15);
            this.label3.TabIndex = 10;
            this.label3.Text = "L";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 183);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(15, 15);
            this.label2.TabIndex = 9;
            this.label2.Text = "R";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 152);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 15);
            this.label1.TabIndex = 8;
            this.label1.Text = "Angle";
            // 
            // btnDown
            // 
            this.btnDown.Location = new System.Drawing.Point(195, 191);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(75, 23);
            this.btnDown.TabIndex = 7;
            this.btnDown.Text = "-";
            this.btnDown.UseVisualStyleBackColor = true;
            this.btnDown.Click += new System.EventHandler(this.BtnDown_Click);
            // 
            // btnUp
            // 
            this.btnUp.Location = new System.Drawing.Point(195, 162);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(75, 23);
            this.btnUp.TabIndex = 6;
            this.btnUp.Text = "+";
            this.btnUp.UseVisualStyleBackColor = true;
            this.btnUp.Click += new System.EventHandler(this.BtnUp_Click);
            // 
            // txtLength
            // 
            this.txtLength.Location = new System.Drawing.Point(61, 211);
            this.txtLength.Name = "txtLength";
            this.txtLength.Size = new System.Drawing.Size(100, 25);
            this.txtLength.TabIndex = 5;
            // 
            // txtRadius
            // 
            this.txtRadius.Location = new System.Drawing.Point(61, 180);
            this.txtRadius.Name = "txtRadius";
            this.txtRadius.Size = new System.Drawing.Size(100, 25);
            this.txtRadius.TabIndex = 4;
            // 
            // txtAngle
            // 
            this.txtAngle.Location = new System.Drawing.Point(61, 149);
            this.txtAngle.Name = "txtAngle";
            this.txtAngle.Size = new System.Drawing.Size(100, 25);
            this.txtAngle.TabIndex = 3;
            // 
            // btnDraw
            // 
            this.btnDraw.Location = new System.Drawing.Point(195, 35);
            this.btnDraw.Name = "btnDraw";
            this.btnDraw.Size = new System.Drawing.Size(75, 23);
            this.btnDraw.TabIndex = 2;
            this.btnDraw.Text = "Draw";
            this.btnDraw.UseVisualStyleBackColor = true;
            this.btnDraw.Click += new System.EventHandler(this.BtnDraw_Click);
            // 
            // txtW
            // 
            this.txtW.Location = new System.Drawing.Point(61, 49);
            this.txtW.Name = "txtW";
            this.txtW.Size = new System.Drawing.Size(100, 25);
            this.txtW.TabIndex = 1;
            // 
            // txtL
            // 
            this.txtL.Location = new System.Drawing.Point(61, 18);
            this.txtL.Name = "txtL";
            this.txtL.Size = new System.Drawing.Size(100, 25);
            this.txtL.TabIndex = 0;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.panel2);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.panel3);
            this.splitContainer2.Size = new System.Drawing.Size(363, 417);
            this.splitContainer2.SplitterDistance = 171;
            this.splitContainer2.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(363, 171);
            this.panel2.TabIndex = 0;
            this.panel2.SizeChanged += new System.EventHandler(this.Panel2_SizeChanged);
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(363, 242);
            this.panel3.TabIndex = 0;
            this.panel3.SizeChanged += new System.EventHandler(this.Panel3_SizeChanged);
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.SaveFileDialog1_FileOk);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.OpenFileDialog1_FileOk);
            // 
            // TestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.toolStripContainer1);
            this.Name = "TestForm";
            this.Text = "TestForm";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            this.splitContainer3.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton importBtn;
        private System.Windows.Forms.ToolStripButton moveBtn;
        private System.Windows.Forms.ToolStripButton rotateBtn;
        private System.Windows.Forms.ToolStripButton clearBtn;
        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton moveNodeBtn;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem singlePickToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem multiPickToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hitTestToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton mouseBtn;
        private System.Windows.Forms.ToolStripButton testBtn;
        private System.Windows.Forms.ToolStripButton transOnMaxBtn;
        private System.Windows.Forms.ToolStripButton transOnSelectBtn;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.TextBox txtW;
        private System.Windows.Forms.TextBox txtL;
        private System.Windows.Forms.Button btnDraw;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnDown;
        private System.Windows.Forms.Button btnUp;
        private System.Windows.Forms.TextBox txtLength;
        private System.Windows.Forms.TextBox txtRadius;
        private System.Windows.Forms.TextBox txtAngle;
        private System.Windows.Forms.Button btnReadXml;
        private System.Windows.Forms.Button btnExportXml;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}