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
using System.Diagnostics;

namespace AnyCAD.Basic
{
    public partial class TestForm : Form
    {
        // Render Control
        private Presentation.RenderWindow3d renderView;
        private Presentation.RenderWindow3d renderViewXZ;
        private Presentation.RenderWindow3d renderViewYZ;
        private Presentation.RenderWindow3d renderViewDraw;
        private int shapeId = 100;
        private TopoShape topoShape = new TopoShape();
        public TestForm()
        {
            InitializeComponent();
            this.renderView = new AnyCAD.Presentation.RenderWindow3d();
            this.renderViewXZ = new Presentation.RenderWindow3d();
            this.renderViewYZ = new Presentation.RenderWindow3d();
            this.renderViewDraw = new Presentation.RenderWindow3d();
            //初始化视窗大小
            System.Drawing.Size size = this.panel1.ClientSize;
            Size sizeXZ = panel2.ClientSize;
            Size sizeYZ = panel3.ClientSize;
            Size sizeDraw = panel4.ClientSize;
            this.renderView.Size = size;
            renderViewXZ.Size = sizeXZ;
            renderViewYZ.Size = sizeYZ;
            renderViewDraw.Size = sizeDraw;

            this.renderView.TabIndex = 1;
            renderViewXZ.TabIndex = 2;
            renderViewYZ.TabIndex = 3;
            this.panel1.Controls.Add(this.renderView);
            panel2.Controls.Add(renderViewXZ);
            panel3.Controls.Add(renderViewYZ);
            panel4.Controls.Add(renderViewDraw);

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
        private void panel1_SizeChanged(object sender, EventArgs e)
        {
            if (renderView != null)
            {
                System.Drawing.Size size = this.panel1.ClientSize;
                renderView.Size = size;
            }
        }
        private void panel2_SizeChanged(object sender, EventArgs e)
        {
            if (renderViewXZ != null)
            {
                renderViewXZ.Size = panel2.ClientSize;
                renderViewXZ.Renderer.SetStandardView(EnumStandardView.SV_Back);
                renderViewXZ.ExecuteCommand("Pan");
                renderViewXZ.RequestDraw();
            }
        }
        private void panel3_SizeChanged(object sender, EventArgs e)
        {
            if (renderViewYZ != null)
            {
                renderViewYZ.Size = panel3.ClientSize;
                renderViewYZ.Renderer.SetStandardView(EnumStandardView.SV_Right);
                renderViewYZ.ExecuteCommand("Pan");
                renderViewYZ.RequestDraw();
            }
        }
        private void panel4_SizeChanged(object sender, EventArgs e)
        {
            if (renderView != null)
            {
                System.Drawing.Size size = this.panel4.ClientSize;
                renderViewDraw.Size = size;
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
        private void singlePickToolStripMenuItem_Click(object sender, EventArgs e)
        {
            renderView.ExecuteCommand("PickClearMode", "SinglePick");
        }
        private void multiPickToolStripMenuItem_Click(object sender, EventArgs e)
        {
            renderView.ExecuteCommand("PickClearMode", "MultiPick");
        }
        private void mouseBtn_Click(object sender, EventArgs e)
        {
            renderView.ExecuteCommand("Pick");
        }
        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            renderView.ClearScene();
            renderViewXZ.ClearScene();
            renderViewYZ.ClearScene();
        }
        private void importToolStripMenuItem_Click(object sender, EventArgs e)
        {

            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "STEP (*.stp;*.step)|*.stp;*.step|STL (*.stl)|*.stl|IGES (*.igs;*.iges)|*.igs;*.iges|BREP (*.brep)|*.brep|All Files(*.*)|*.*";

            if (DialogResult.OK != dlg.ShowDialog())
                return;

            TopoShape shape = GlobalInstance.BrepTools.LoadFile(new AnyCAD.Platform.Path(dlg.FileName));
            topoShape = shape;
            #region Render Shape
            renderView.RenderTimer.Enabled = false;
            if (shape != null)
            {
                renderView.ShowGeometry(shape, shapeId);
            }
            renderView.RenderTimer.Enabled = true;
            renderView.FitAll();
            renderView.RequestDraw(EnumRenderHint.RH_LoadScene);

            #endregion 
        }

        private void moveNodeBtn_Click(object sender, EventArgs e)
        {
            renderView.ExecuteCommand("MoveNode");
        }

        #region Hit Test
        private bool m_PickPoint = false;
        private void hitTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_PickPoint = !m_PickPoint;
        }
        private void OnRenderWindow_MouseClick(object sender, MouseEventArgs e)
        {
            if (!m_PickPoint)
                return;

            Platform.PickHelper pickHelper = renderView.PickShape(e.X, e.Y);

            if (pickHelper != null)
            {
                // add a ball
                Platform.TopoShape shape = GlobalInstance.BrepTools.MakeSphere(pickHelper.GetPointOnShape(), 2);
                renderView.ShowGeometry(shape, 100);
            }
            // Try the grid
            Vector3 pt = renderView.HitPointOnGrid(e.X, e.Y);
            if (pt != null)
            {
                Platform.TopoShape shape = GlobalInstance.BrepTools.MakeSphere(pt, 2);
                renderView.ShowGeometry(shape, 100);
            }
        }

        #endregion

        private void testBtn_Click(object sender, EventArgs e)
        {
            //SelectedShapeQuery context = new SelectedShapeQuery();
            //renderView.QuerySelection(context);
            //var shape = context.GetGeometry();
            TopoShape shapeXZ = new TopoShape();
            TopoShape shapeYZ = new TopoShape();
            if (topoShape != null)
            {
                shapeXZ = section(topoShape, new Vector3(0, 1, 0));
                shapeYZ = section(topoShape, new Vector3(1, 0, 0));
            }
            #region Render
            if (topoShape != null)
            {
                renderViewXZ.ClearScene();
                renderViewXZ.ShowGeometry(shapeXZ, shapeId);
                renderViewYZ.ClearScene();
                renderViewYZ.ShowGeometry(shapeYZ, shapeId);
            }
            renderViewXZ.FitAll();
            renderViewYZ.FitAll();
            renderViewXZ.RequestDraw(EnumRenderHint.RH_LoadScene);
            renderViewYZ.RequestDraw(EnumRenderHint.RH_LoadScene);
            #endregion

        }

        private void transOnMaxBtn_Click(object sender, EventArgs e)
        {
            SelectedShapeQuery context = new SelectedShapeQuery();
            renderView.QuerySelection(context);
            var shape = context.GetGeometry();
            transOnMax(shape);
        }

        private void transOnMax(TopoShape shape)
        {
            double areaM = 0;
            Vector3 dirN = new Vector3();
            Vector3 pos = new Vector3();
            TopoExplor topo = new TopoExplor();
            TopoShapeGroup group2 = topo.ExplorFaces(shape);
            for (int i = 0; i < group2.Size(); i++)
            {
                TopoShape face = group2.GetTopoShape(i);

                #region 计算面积
                TopoShapeProperty property = new TopoShapeProperty();
                property.SetShape(face);
                Console.WriteLine("Face {0}:\n\tArea {1}\n\tOrientation {2}", i, property.SurfaceArea(), face.GetOrientation());
                #endregion
                #region 计算法向量
                GeomSurface surface = new GeomSurface();
                surface.Initialize(face);
                //参数域UV范围
                double uFirst = surface.FirstUParameter();
                double uLast = surface.LastUParameter();
                double vFirst = surface.FirstVParameter();
                double vLast = surface.LastVParameter();
                //取中点
                double umid = uFirst + (uLast - uFirst) * 0.5f;
                double vmid = vFirst + (vLast - vFirst) * 0.5f;
                //计算法向量
                var data = surface.D1(umid, vmid);
                Vector3 dirU = data[1];
                Vector3 dirV = data[2];
                Vector3 dir = dirV.CrossProduct(dirU);
                dir.Normalize();
                Console.WriteLine("\tDir {0}", dir);
                #endregion

                #region 取最大的面
                if (property.SurfaceArea() > areaM)
                {
                    areaM = property.SurfaceArea();
                    pos = data[0];
                    Console.WriteLine(data[0]);
                    if (face.GetOrientation() == EnumShapeOrientation.ShapeOrientation_REVERSED)
                    {
                        dirN = dir * -1;
                    }
                    else
                    {
                        dirN = dir;
                    }
                }
                #endregion
            }

            #region 坐标变换
            //Translation
            shape = GlobalInstance.BrepTools.Translate(shape, -pos);
            //Rotation
            Vector3 dirZ = new Vector3(0, 0, -1);
            shape = GlobalInstance.BrepTools.Rotation(shape, dirN.CrossProduct(dirZ), dirN.AngleBetween(dirZ));
            #endregion

            if (shape != null)
            {
                topoShape = shape;
                renderView.ClearScene();
                renderView.ShowGeometry(shape, shapeId);
            }
            renderView.FitAll();
            renderView.RequestDraw(EnumRenderHint.RH_LoadScene);

        }

        private void transOnSelectBtn_Click(object sender, EventArgs e)
        {
            //Get selected shape
            SelectedShapeQuery context = new SelectedShapeQuery();
            renderView.QuerySelection(context);
            var shape = context.GetGeometry();
            var face = context.GetSubGeometry();
            if (shape == null)
            {
                return;
            }
            var center = shape.GetBBox().GetCenter();

            #region 计算法向量
            GeomSurface surface = new GeomSurface();
            surface.Initialize(face);
            //参数域UV范围
            double uFirst = surface.FirstUParameter();
            double uLast = surface.LastUParameter();
            double vFirst = surface.FirstVParameter();
            double vLast = surface.LastVParameter();
            //取中点
            double umid = uFirst + (uLast - uFirst) * 0.5f;
            double vmid = vFirst + (vLast - vFirst) * 0.5f;
            //计算法向量
            var data = surface.D1(umid, vmid);
            Vector3 dirU = data[1];
            Vector3 dirV = data[2];
            Vector3 dir = dirV.CrossProduct(dirU);
            dir.Normalize();
            Console.WriteLine("\tDir {0}", dir);
            #endregion

            #region 坐标变换
            Vector3 dirN = new Vector3();
            if (face.GetOrientation() == EnumShapeOrientation.ShapeOrientation_REVERSED)
            {
                dirN = dir * -1;
            }
            else
            {
                dirN = dir;
            }

            //Translation
            shape = GlobalInstance.BrepTools.Translate(shape, -center);
            //Rotation
            Vector3 dirZ = new Vector3(0, 0, -1);
            shape = GlobalInstance.BrepTools.Rotation(shape, dirN.CrossProduct(dirZ), dirN.AngleBetween(dirZ));
            #endregion

            #region Render
            if (shape != null)
            {
                topoShape = shape;
                renderView.ClearScene();
                renderView.ShowGeometry(shape, shapeId);
            }
            renderView.FitAll();
            renderView.RequestDraw(EnumRenderHint.RH_LoadScene);

            #endregion

        }
        private TopoShape section(TopoShape shape, Vector3 dir)
        {
            Vector3 origion = new Vector3(0, 0, 0);
            TopoShape plane = GlobalInstance.BrepTools.MakePlaneFace(origion,dir,-100,100,-100,100);
            shape = GlobalInstance.BrepTools.BooleanCommon(shape, plane);
            return shape;
        }

        TopoShapeGroup group = new TopoShapeGroup();
        BendingGroup bendings = new BendingGroup();
        private void btnDraw_Click(object sender, EventArgs e)
        {
            bendings.Length = Convert.ToDouble(txtL.Text);
            bendings.Width = Convert.ToDouble(txtW.Text);

            TopoShape rect = GlobalInstance.BrepTools.MakeRectangle(bendings.Length, bendings.Width, 0, Coordinate3.UNIT_XYZ);
            TopoShape face = GlobalInstance.BrepTools.MakeFace(rect);

            renderViewDraw.ClearScene();
            group.Add(face);
            SceneManager sceneMgr = renderViewDraw.SceneManager;
            SceneNode rootNode = GlobalInstance.TopoShapeConvert.ToSceneNode(face, 0.1f);
            if (rootNode != null)
            {
                sceneMgr.AddNode(rootNode);
            }
            renderViewDraw.FitAll();
            renderViewDraw.RequestDraw(EnumRenderHint.RH_LoadScene);
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            //Get selected shape
            SelectedShapeQuery context = new SelectedShapeQuery();
            renderViewDraw.QuerySelection(context);
            var face = context.GetGeometry();
            var line = context.GetSubGeometry();
            if (face == null)
            {
                return;
            }

            //记录输入参数
            Bending bending = new Bending()
            {
                Orientation = EnumEdge.Edge_1,
                Direction = EnumDir.Edge_UP,
                Angle = Convert.ToDouble(txtAngle.Text),
                Radius = Convert.ToDouble(txtRadius.Text),
                Length = Convert.ToDouble(txtLength.Text)
            };

            #region 计算平面法向量
            GeomSurface surface = new GeomSurface();
            surface.Initialize(face);
            //参数域UV范围
            double uFirst = surface.FirstUParameter();
            double uLast = surface.LastUParameter();
            double vFirst = surface.FirstVParameter();
            double vLast = surface.LastVParameter();
            //取中点
            double umid = uFirst + (uLast - uFirst) * 0.5f;
            double vmid = vFirst + (vLast - vFirst) * 0.5f;
            //计算法向量
            var data = surface.D1(umid, vmid);
            Vector3 dirU = data[1];
            Vector3 dirV = data[2];
            Vector3 dirF = dirV.CrossProduct(dirU);
            dirF.Normalize();
            #endregion

            #region 计算边线参数
            GeomCurve curve = new GeomCurve();
            curve.Initialize(line);
            Vector3 dirL = -curve.DN(curve.FirstParameter(), 1);
            Vector3 stPt = curve.Value(curve.FirstParameter()); //起点
            Vector3 edPt = curve.Value(curve.LastParameter());  //终点
            #endregion

            #region 绘制草图
            TopoShapeGroup lineGroup = new TopoShapeGroup();

            Vector3 center = stPt - dirF * bending.Radius; //圆心
            Vector3 radius = stPt - center;    //半径
            double theta = bending.Angle * (Math.PI / 180.0);
            Vector3 radius2 = radius * Math.Cos(theta) + dirL.CrossProduct(radius) * Math.Sin(theta);
            Vector3 edArc = center + radius2;  //圆弧终点
            TopoShape arc = GlobalInstance.BrepTools.MakeArc(stPt, edArc, center, dirL);    //绘制圆弧
            lineGroup.Add(arc);
            Vector3 edLine = dirL.CrossProduct(radius2) * (bending.Length / bending.Radius) + edArc;
            arc = GlobalInstance.BrepTools.MakeLine(edArc, edLine);
            lineGroup.Add(arc);
            //扫描生成折弯
            TopoShape wireSketch = GlobalInstance.BrepTools.MakeWire(lineGroup);
            TopoShape path = GlobalInstance.BrepTools.MakeLine(stPt, edPt);
            TopoShape sweep = GlobalInstance.BrepTools.Sweep(wireSketch, path, true);
            #endregion

            #region 渲染
            group.Add(sweep);
            SceneManager sceneMgr = renderViewDraw.SceneManager;
            SceneNode rootNode = GlobalInstance.TopoShapeConvert.ToSceneNode(sweep, 0.1f);
            if (rootNode != null)
            {
                sceneMgr.AddNode(rootNode);
            }
            renderViewDraw.FitAll();
            renderViewDraw.RequestDraw(EnumRenderHint.RH_LoadScene);

            #endregion

            bendings.Add(bending);
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            //Get selected shape
            SelectedShapeQuery context = new SelectedShapeQuery();
            renderViewDraw.QuerySelection(context);
            var face = context.GetGeometry();
            var line = context.GetSubGeometry();
            if (face == null)
            {
                return;
            }

            //记录输入参数
            Bending bending = new Bending()
            {
                Orientation = EnumEdge.Edge_2,
                Direction = EnumDir.Edge_DOWN,
                Angle = Convert.ToDouble(txtAngle.Text),
                Radius = Convert.ToDouble(txtRadius.Text),
                Length = Convert.ToDouble(txtLength.Text)
            };
                

            #region 计算平面法向量
            GeomSurface surface = new GeomSurface();
            surface.Initialize(face);
            //参数域UV范围
            double uFirst = surface.FirstUParameter();
            double uLast = surface.LastUParameter();
            double vFirst = surface.FirstVParameter();
            double vLast = surface.LastVParameter();
            //取中点
            double umid = uFirst + (uLast - uFirst) * 0.5f;
            double vmid = vFirst + (vLast - vFirst) * 0.5f;
            //计算法向量
            var data = surface.D1(umid, vmid);
            Vector3 dirU = data[1];
            Vector3 dirV = data[2];
            Vector3 dirF = dirV.CrossProduct(dirU);
            dirF.Normalize();
            #endregion

            #region 计算边线参数
            GeomCurve curve = new GeomCurve();
            curve.Initialize(line);
            Vector3 dirL = curve.DN(curve.FirstParameter(), 1);
            Vector3 stPt = curve.Value(curve.FirstParameter()); //起点
            Vector3 edPt = curve.Value(curve.LastParameter());  //终点
            #endregion

            #region 绘制草图
            TopoShapeGroup lineGroup = new TopoShapeGroup();

            Vector3 center = stPt + dirF * bending.Radius; //圆心
            Vector3 radius = stPt - center;    //半径
            double theta = bending.Angle * (Math.PI / 180.0);
            Vector3 radius2 = radius * Math.Cos(theta) + dirL.CrossProduct(radius) * Math.Sin(theta);
            Vector3 edArc = center + radius2;  //圆弧终点
            TopoShape arc = GlobalInstance.BrepTools.MakeArc(stPt, edArc, center, dirL);    //绘制圆弧
            lineGroup.Add(arc);
            Vector3 edLine = dirL.CrossProduct(radius2) * (bending.Length / bending.Radius) + edArc;
            arc = GlobalInstance.BrepTools.MakeLine(edArc, edLine);
            lineGroup.Add(arc);
            //扫描生成折弯
            TopoShape wireSketch = GlobalInstance.BrepTools.MakeWire(lineGroup);
            TopoShape path = GlobalInstance.BrepTools.MakeLine(stPt, edPt);
            TopoShape sweep = GlobalInstance.BrepTools.Sweep(wireSketch, path, true);
            #endregion

            #region 渲染
            group.Add(sweep);
            SceneManager sceneMgr = renderViewDraw.SceneManager;
            SceneNode rootNode = GlobalInstance.TopoShapeConvert.ToSceneNode(sweep, 0.1f);
            if (rootNode != null)
            {
                sceneMgr.AddNode(rootNode);
            }
            renderViewDraw.FitAll();
            renderViewDraw.RequestDraw(EnumRenderHint.RH_LoadScene);

            #endregion

            bendings.Add(bending);
        }

        private void btnExportXml_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog();
        }

        private void btnReadXml_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            ExportXml.GenerateXml(bendings,saveFileDialog1.FileName);
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            ExportXml.ReadXml(openFileDialog1.FileName);
        }
    }
}


