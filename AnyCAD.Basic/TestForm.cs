using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AnyCAD.Platform;
using System.IO;
using System.Text.RegularExpressions;

namespace AnyCAD.Basic
{
    public partial class TestForm : Form
    {
        // Render Control
        private Presentation.RenderWindow3d renderView;
        private int shapeId = 100;
        public TestForm()
        {
            InitializeComponent();
            //MessageBox.Show("AnyCAD .Net SDK PRO");
            // 
            // Create renderView
            // 
            this.renderView = new AnyCAD.Presentation.RenderWindow3d();

            //this.renderView.Location = new System.Drawing.Point(0, 27);
            System.Drawing.Size size = this.panel1.ClientSize;
            this.renderView.Size = size;

            this.renderView.TabIndex = 1;
            this.panel1.Controls.Add(this.renderView);

            this.renderView.MouseClick += new System.Windows.Forms.MouseEventHandler(this.OnRenderWindow_MouseClick);

            GlobalInstance.EventListener.OnChangeCursorEvent += OnChangeCursor;
            GlobalInstance.EventListener.OnSelectElementEvent += OnSelectElement;
        }

        private void OnSelectElement(SelectionChangeArgs args)
        {
            if (!args.IsHighlightMode())
            {
                SelectedShapeQuery query = new SelectedShapeQuery();
                renderView.QuerySelection(query);
                var shape = query.GetGeometry();
                if (shape != null)
                {
                    GeomCurve curve = new GeomCurve();
                    if (curve.Initialize(shape))
                    {
                        TopoShapeProperty property = new TopoShapeProperty();
                        property.SetShape(shape);
                        Console.WriteLine("Edge Length {0}", property.EdgeLength());
                    }
                }
            }
        }
        private void OnChangeCursor(String commandId, String cursorHint)
        {

            if (cursorHint == "Pan")
            {
                this.renderView.Cursor = System.Windows.Forms.Cursors.SizeAll;
            }
            else if (cursorHint == "Orbit")
            {
                this.renderView.Cursor = System.Windows.Forms.Cursors.Hand;
            }
            else if (cursorHint == "Cross")
            {
                this.renderView.Cursor = System.Windows.Forms.Cursors.Cross;
            }
            else
            {
                if (commandId == "Pick")
                {
                    this.renderView.Cursor = System.Windows.Forms.Cursors.Arrow;
                }
                else
                {
                    this.renderView.Cursor = System.Windows.Forms.Cursors.Default;
                }
            }

        }

        private bool m_PickPoint = false;
        private void OnRenderWindow_MouseClick(object sender, MouseEventArgs e)
        {
            if (!m_PickPoint)
                return;

            Platform.PickHelper pickHelper = renderView.PickShape(e.X, e.Y);
            Vector3 pt = renderView.HitPointOnGrid(e.X, e.Y);
            if (pickHelper != null)
            {
                // add a ball
                Platform.TopoShape shape = GlobalInstance.BrepTools.MakeSphere(pt, 2);
                renderView.ShowGeometry(shape, 100);
            }
            // Try the grid
            
            if (pt != null)
            {
                Platform.TopoShape shape = GlobalInstance.BrepTools.MakeSphere(pt, 2);
                renderView.ShowGeometry(shape, 100);
            }
        }
        private void FormMain_SizeChanged(object sender, EventArgs e)
        {
            if (renderView != null)
            {

                System.Drawing.Size size = this.panel1.ClientSize;
                renderView.Size = size;
            }
        }

        private void importToolStripMenuItem_Click(object sender, EventArgs e)
        {

            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "STEP (*.stp;*.step)|*.stp;*.step|STL (*.stl)|*.stl|IGES (*.igs;*.iges)|*.igs;*.iges|BREP (*.brep)|*.brep|All Files(*.*)|*.*";

            if (DialogResult.OK != dlg.ShowDialog())
                return;


            TopoShape shape = GlobalInstance.BrepTools.LoadFile(new AnyCAD.Platform.Path(dlg.FileName));
            renderView.RenderTimer.Enabled = false;
            if (shape != null)
            {
                TopoShapeGroup group = new TopoShapeGroup();
                group.Add(shape);
                SceneManager sceneMgr = renderView.SceneManager;
                SceneNode rootNode = GlobalInstance.TopoShapeConvert.ToSceneNode(shape, 0.1f);
                if (rootNode != null)
                {
                    sceneMgr.AddNode(rootNode);
                }
            }
            renderView.RenderTimer.Enabled = true;


            renderView.FitAll();
            renderView.RequestDraw(EnumRenderHint.RH_LoadScene);

            TopoExplor topo = new TopoExplor();
            TopoShapeGroup group2 = topo.ExplorFaces(shape);
            for (int i = 0; i < group2.Size(); i++)
            {
                TopoShape face = group2.GetTopoShape(i);

                #region 计算面积
                TopoShapeProperty property = new TopoShapeProperty();
                property.SetShape(face);
                Console.WriteLine("Face {0}:\n\tArea {1}", i, property.SurfaceArea());
                #endregion
                #region 计算法向量
                GeomSurface surface = new GeomSurface();
                surface.Initialize(face);
                //参数域UV范围
                double ufirst = surface.FirstUParameter();
                double uLast = surface.LastUParameter();
                double vfirst = surface.FirstVParameter();
                double vLast = surface.LastVParameter();
                //取中点
                double umid = ufirst + (uLast - ufirst) * 0.5f;
                double vmid = vfirst + (vLast - vfirst) * 0.5f;
                //计算法向量
                var data = surface.D1(umid, vmid);
                Vector3 pos = data[0];
                Vector3 dirU = data[1];
                Vector3 dirV = data[2];
                Vector3 dir = dirV.CrossProduct(dirU);
                dir.Normalize();
                Console.WriteLine("\tDir {0}", dir);
                
                #endregion
            }
        }
        private void orbitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            renderView.ExecuteCommand("Orbit");
        }
        private void panToolStripMenuItem_Click(object sender, EventArgs e)
        {
            renderView.ExecuteCommand("Pan");
        }
        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            renderView.ClearScene();
        }

        private void moveNodeBtn_Click(object sender, EventArgs e)
        {
            renderView.ExecuteCommand("MoveNode");
        }
    }
}
