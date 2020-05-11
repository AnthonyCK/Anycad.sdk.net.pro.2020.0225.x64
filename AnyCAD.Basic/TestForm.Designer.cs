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
            this.moveBtn = new System.Windows.Forms.ToolStripButton();
            this.rotateBtn = new System.Windows.Forms.ToolStripButton();
            this.clearBtn = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.moveNodeBtn = new System.Windows.Forms.ToolStripButton();
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
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
            this.moveBtn,
            this.rotateBtn,
            this.clearBtn,
            this.toolStripSeparator1,
            this.moveNodeBtn});
            this.toolStrip1.Location = new System.Drawing.Point(4, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(190, 31);
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
            this.importBtn.Click += new System.EventHandler(this.importToolStripMenuItem_Click);
            // 
            // moveBtn
            // 
            this.moveBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.moveBtn.Image = ((System.Drawing.Image)(resources.GetObject("moveBtn.Image")));
            this.moveBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.moveBtn.Name = "moveBtn";
            this.moveBtn.Size = new System.Drawing.Size(29, 28);
            this.moveBtn.Text = "Move";
            this.moveBtn.Click += new System.EventHandler(this.panToolStripMenuItem_Click);
            // 
            // rotateBtn
            // 
            this.rotateBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.rotateBtn.Image = ((System.Drawing.Image)(resources.GetObject("rotateBtn.Image")));
            this.rotateBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.rotateBtn.Name = "rotateBtn";
            this.rotateBtn.Size = new System.Drawing.Size(29, 28);
            this.rotateBtn.Text = "Rotate";
            this.rotateBtn.Click += new System.EventHandler(this.orbitToolStripMenuItem_Click);
            // 
            // clearBtn
            // 
            this.clearBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.clearBtn.Image = ((System.Drawing.Image)(resources.GetObject("clearBtn.Image")));
            this.clearBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.clearBtn.Name = "clearBtn";
            this.clearBtn.Size = new System.Drawing.Size(29, 28);
            this.clearBtn.Text = "Clear";
            this.clearBtn.Click += new System.EventHandler(this.clearToolStripMenuItem_Click);
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
            this.moveNodeBtn.Click += new System.EventHandler(this.moveNodeBtn_Click);
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
    }
}