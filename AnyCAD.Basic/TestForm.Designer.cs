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
            this.panel1 = new System.Windows.Forms.Panel();
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
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.transOnSelectBtn = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 419);
            this.panel1.TabIndex = 0;
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
            this.toolStrip1.Size = new System.Drawing.Size(570, 27);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // importBtn
            // 
            this.importBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.importBtn.Image = ((System.Drawing.Image)(resources.GetObject("importBtn.Image")));
            this.importBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.importBtn.Name = "importBtn";
            this.importBtn.Size = new System.Drawing.Size(29, 24);
            this.importBtn.Text = "Import";
            this.importBtn.Click += new System.EventHandler(this.importToolStripMenuItem_Click);
            // 
            // mouseBtn
            // 
            this.mouseBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mouseBtn.Image = ((System.Drawing.Image)(resources.GetObject("mouseBtn.Image")));
            this.mouseBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mouseBtn.Name = "mouseBtn";
            this.mouseBtn.Size = new System.Drawing.Size(29, 24);
            this.mouseBtn.Text = "toolStripButton1";
            this.mouseBtn.Click += new System.EventHandler(this.mouseBtn_Click);
            // 
            // moveBtn
            // 
            this.moveBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.moveBtn.Image = ((System.Drawing.Image)(resources.GetObject("moveBtn.Image")));
            this.moveBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.moveBtn.Name = "moveBtn";
            this.moveBtn.Size = new System.Drawing.Size(29, 24);
            this.moveBtn.Text = "Move";
            this.moveBtn.Click += new System.EventHandler(this.panToolStripMenuItem_Click);
            // 
            // rotateBtn
            // 
            this.rotateBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.rotateBtn.Image = ((System.Drawing.Image)(resources.GetObject("rotateBtn.Image")));
            this.rotateBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.rotateBtn.Name = "rotateBtn";
            this.rotateBtn.Size = new System.Drawing.Size(29, 24);
            this.rotateBtn.Text = "Rotate";
            this.rotateBtn.Click += new System.EventHandler(this.orbitToolStripMenuItem_Click);
            // 
            // clearBtn
            // 
            this.clearBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.clearBtn.Image = ((System.Drawing.Image)(resources.GetObject("clearBtn.Image")));
            this.clearBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.clearBtn.Name = "clearBtn";
            this.clearBtn.Size = new System.Drawing.Size(29, 24);
            this.clearBtn.Text = "Clear";
            this.clearBtn.Click += new System.EventHandler(this.clearToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 27);
            // 
            // moveNodeBtn
            // 
            this.moveNodeBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.moveNodeBtn.Image = ((System.Drawing.Image)(resources.GetObject("moveNodeBtn.Image")));
            this.moveNodeBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.moveNodeBtn.Name = "moveNodeBtn";
            this.moveNodeBtn.Size = new System.Drawing.Size(55, 24);
            this.moveNodeBtn.Text = "Move";
            this.moveNodeBtn.Click += new System.EventHandler(this.moveNodeBtn_Click);
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
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(52, 24);
            this.toolStripDropDownButton1.Text = "Pick";
            // 
            // singlePickToolStripMenuItem
            // 
            this.singlePickToolStripMenuItem.Name = "singlePickToolStripMenuItem";
            this.singlePickToolStripMenuItem.Size = new System.Drawing.Size(166, 26);
            this.singlePickToolStripMenuItem.Text = "SinglePick";
            this.singlePickToolStripMenuItem.Click += new System.EventHandler(this.singlePickToolStripMenuItem_Click);
            // 
            // multiPickToolStripMenuItem
            // 
            this.multiPickToolStripMenuItem.Name = "multiPickToolStripMenuItem";
            this.multiPickToolStripMenuItem.Size = new System.Drawing.Size(166, 26);
            this.multiPickToolStripMenuItem.Text = "MultiPick";
            this.multiPickToolStripMenuItem.Click += new System.EventHandler(this.multiPickToolStripMenuItem_Click);
            // 
            // hitTestToolStripMenuItem
            // 
            this.hitTestToolStripMenuItem.Name = "hitTestToolStripMenuItem";
            this.hitTestToolStripMenuItem.Size = new System.Drawing.Size(166, 26);
            this.hitTestToolStripMenuItem.Text = "Hit Test";
            this.hitTestToolStripMenuItem.Click += new System.EventHandler(this.hitTestToolStripMenuItem_Click);
            // 
            // testBtn
            // 
            this.testBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.testBtn.Image = ((System.Drawing.Image)(resources.GetObject("testBtn.Image")));
            this.testBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.testBtn.Name = "testBtn";
            this.testBtn.Size = new System.Drawing.Size(44, 24);
            this.testBtn.Text = "Test";
            this.testBtn.Click += new System.EventHandler(this.testBtn_Click);
            // 
            // transOnMaxBtn
            // 
            this.transOnMaxBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.transOnMaxBtn.Image = ((System.Drawing.Image)(resources.GetObject("transOnMaxBtn.Image")));
            this.transOnMaxBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.transOnMaxBtn.Name = "transOnMaxBtn";
            this.transOnMaxBtn.Size = new System.Drawing.Size(101, 24);
            this.transOnMaxBtn.Text = "transOnMax";
            this.transOnMaxBtn.Click += new System.EventHandler(this.transOnMaxBtn_Click);
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.panel1);
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
            // transOnSelectBtn
            // 
            this.transOnSelectBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.transOnSelectBtn.Image = ((System.Drawing.Image)(resources.GetObject("transOnSelectBtn.Image")));
            this.transOnSelectBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.transOnSelectBtn.Name = "transOnSelectBtn";
            this.transOnSelectBtn.Size = new System.Drawing.Size(115, 24);
            this.transOnSelectBtn.Text = "transOnSelect";
            this.transOnSelectBtn.Click += new System.EventHandler(this.transOnSelectBtn_Click);
            // 
            // TestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.toolStripContainer1);
            this.Name = "TestForm";
            this.Text = "TestForm";
            this.SizeChanged += new System.EventHandler(this.FormMain_SizeChanged);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
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
    }
}