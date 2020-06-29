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
        public Presentation.RenderWindow3d renderViewDraw;
        private int shapeId = 1000;
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
        private void Panel1_SizeChanged(object sender, EventArgs e)
        {
            if (renderView != null)
            {
                System.Drawing.Size size = this.panel1.ClientSize;
                renderView.Size = size;
            }
        }
        private void Panel2_SizeChanged(object sender, EventArgs e)
        {
            if (renderViewXZ != null)
            {
                renderViewXZ.Size = panel2.ClientSize;
                renderViewXZ.Renderer.SetStandardView(EnumStandardView.SV_Back);
                renderViewXZ.ExecuteCommand("Pan");
                renderViewXZ.RequestDraw();
            }
        }
        private void Panel3_SizeChanged(object sender, EventArgs e)
        {
            if (renderViewYZ != null)
            {
                renderViewYZ.Size = panel3.ClientSize;
                renderViewYZ.Renderer.SetStandardView(EnumStandardView.SV_Right);
                renderViewYZ.ExecuteCommand("Pan");
                renderViewYZ.RequestDraw();
            }
        }
        private void Panel4_SizeChanged(object sender, EventArgs e)
        {
            if (renderView != null)
            {
                System.Drawing.Size size = this.panel4.ClientSize;
                renderViewDraw.Size = size;
            }
        }

        private void OrbitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            renderView.ExecuteCommand("Orbit");
        }
        private void PanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            renderView.ExecuteCommand("Pan");
        }
        private void SinglePickToolStripMenuItem_Click(object sender, EventArgs e)
        {
            renderView.ExecuteCommand("PickClearMode", "SinglePick");
        }
        private void MultiPickToolStripMenuItem_Click(object sender, EventArgs e)
        {
            renderView.ExecuteCommand("PickClearMode", "MultiPick");
        }
        private void MouseBtn_Click(object sender, EventArgs e)
        {
            renderView.ExecuteCommand("Pick");
        }
        private void ClearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            renderView.ClearScene();
            renderViewXZ.ClearScene();
            renderViewYZ.ClearScene();
            renderViewDraw.ClearScene();
        }
        private void ImportToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void MoveNodeBtn_Click(object sender, EventArgs e)
        {
            renderView.ExecuteCommand("MoveNode");
        }

        #region Hit Test
        private bool m_PickPoint = false;
        private void HitTestToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void TestBtn_Click(object sender, EventArgs e)
        {
            //SelectedShapeQuery context = new SelectedShapeQuery();
            //renderView.QuerySelection(context);
            //var shape = context.GetGeometry();
            TopoShape shapeXZ = new TopoShape();
            TopoShape shapeYZ = new TopoShape();
            if (topoShape != null)
            {
                shapeXZ = Section(topoShape, new Vector3(0, 1, 0));
                shapeYZ = Section(topoShape, new Vector3(1, 0, 0));
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

        private void TransOnMaxBtn_Click(object sender, EventArgs e)
        {
            SelectedShapeQuery context = new SelectedShapeQuery();
            renderView.QuerySelection(context);
            var shape = context.GetGeometry();
            TransOnMax(shape);
        }

        private void TransOnMax(TopoShape shape)
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

        private void TransOnSelectBtn_Click(object sender, EventArgs e)
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
        private TopoShape Section(TopoShape shape, Vector3 dir)
        {
            Vector3 origion = new Vector3(0, 0, 0);
            TopoShape plane = GlobalInstance.BrepTools.MakePlaneFace(origion,dir,-100,100,-100,100);
            shape = GlobalInstance.BrepTools.BooleanCommon(shape, plane);
            return shape;
        }

        TopoShapeGroup group = new TopoShapeGroup();
        BendingGroup bendings = new BendingGroup();
        private void BtnDraw_Click(object sender, EventArgs e)
        {
            bendings.Length = Convert.ToDouble(txtL.Text);
            bendings.Width = Convert.ToDouble(txtW.Text);
            var face = DrawRect(bendings.Length, bendings.Width);
            bendings.Buffer = GlobalInstance.BrepTools.SaveBuffer(face);
        }
        private TopoShape DrawRect(double length, double width)
        {
            TopoShape rect = GlobalInstance.BrepTools.MakeRectangle(length, width, 0, Coordinate3.UNIT_XYZ);
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
            return face;
        }
        private void BtnUp_Click(object sender, EventArgs e)
        {
            //Get selected shape
            SelectedShapeQuery context = new SelectedShapeQuery();
            renderViewDraw.QuerySelection(context);
            var face = context.GetGeometry();
            var line = context.GetSubGeometry();
            if (face == null || line == null)
            {
                return;
            }
            if (face.GetShapeType() != EnumTopoShapeType.Topo_FACE || line.GetShapeType() != EnumTopoShapeType.Topo_EDGE)
            {
                return;
            }

            //记录输入参数
            Bending bending = new Bending()
            {
                Direction = EnumDir.Edge_UP,
                Angle = Convert.ToDouble(txtAngle.Text),
                Radius = Convert.ToDouble(txtRadius.Text),
                Length = Convert.ToDouble(txtLength.Text)
            };
            GeomCurve curve = new GeomCurve();
            curve.Initialize(line);
            Vector3 dirL = curve.DN(curve.FirstParameter(), 1);
            if (dirL.X == 1)
            {
                bending.Orientation = EnumEdge.Edge_1;
            }
            else if (dirL.Y == -1)
            {
                bending.Orientation = EnumEdge.Edge_2;
            }
            else if (dirL.X == -1)
            {
                bending.Orientation = EnumEdge.Edge_3;
            }
            else
            {
                bending.Orientation = EnumEdge.Edge_4;
            }

            TopoShape sweep = BendUp(face, line, bending).Sweep;

            bending.Buffer = GlobalInstance.BrepTools.SaveBuffer(sweep);
            bendings.Add(bending);

            #region 渲染
            group.Add(sweep);
            ElementId faceId = new ElementId(bending.Index + shapeId);
            ElementId edgeId = new ElementId(bending.Index);
            SceneManager sceneMgr = renderViewDraw.SceneManager;
            SceneNode rootNode = GlobalInstance.TopoShapeConvert.ToSceneNode(sweep, 0.1f);
            SceneNode faceNode = GlobalInstance.TopoShapeConvert.ToSceneNode(face, 0.1f);
            SceneNode edgeNode = GlobalInstance.TopoShapeConvert.ToSceneNode(line, 0.1f);
            faceNode.SetId(faceId);
            faceNode.SetVisible(false);
            edgeNode.SetId(edgeId);
            edgeNode.SetVisible(false);
            if (rootNode != null)
            {
                sceneMgr.AddNode(rootNode);
                sceneMgr.AddNode(faceNode);
                sceneMgr.AddNode(edgeNode);
            }
            renderViewDraw.FitAll();
            renderViewDraw.RequestDraw(EnumRenderHint.RH_LoadScene);

            #endregion

        }
        private void BtnDown_Click(object sender, EventArgs e)
        {
            //Get selected shape
            SelectedShapeQuery context = new SelectedShapeQuery();
            renderViewDraw.QuerySelection(context);
            var face = context.GetGeometry();
            var line = context.GetSubGeometry();
            if (face == null || line == null)
            {
                return;
            }
            if (face.GetShapeType() != EnumTopoShapeType.Topo_FACE || line.GetShapeType() != EnumTopoShapeType.Topo_EDGE)
            {
                return;
            }

            //记录输入参数
            Bending bending = new Bending()
            {
                Direction = EnumDir.Edge_DOWN,
                Angle = Convert.ToDouble(txtAngle.Text),
                Radius = Convert.ToDouble(txtRadius.Text),
                Length = Convert.ToDouble(txtLength.Text)
            };
            GeomCurve curve = new GeomCurve();
            curve.Initialize(line);
            Vector3 dirL = curve.DN(curve.FirstParameter(), 1);
            if (dirL.X == 1)
            {
                bending.Orientation = EnumEdge.Edge_1;
            }
            else if (dirL.Y == -1)
            {
                bending.Orientation = EnumEdge.Edge_2;
            }
            else if (dirL.X == -1)
            {
                bending.Orientation = EnumEdge.Edge_3;
            }
            else
            {
                bending.Orientation = EnumEdge.Edge_4;
            }

            TopoShape sweep = BendDown(face, line, bending).Sweep;

            bending.Buffer = GlobalInstance.BrepTools.SaveBuffer(sweep);
            bendings.Add(bending);

            #region 渲染
            group.Add(sweep);
            ElementId faceId = new ElementId(bending.Index + shapeId);
            ElementId edgeId = new ElementId(bending.Index);
            SceneManager sceneMgr = renderViewDraw.SceneManager;
            SceneNode rootNode = GlobalInstance.TopoShapeConvert.ToSceneNode(sweep, 0.1f);
            SceneNode faceNode = GlobalInstance.TopoShapeConvert.ToSceneNode(face, 0.1f);
            SceneNode edgeNode = GlobalInstance.TopoShapeConvert.ToSceneNode(line, 0.1f);
            faceNode.SetId(faceId);
            faceNode.SetVisible(false);
            edgeNode.SetId(edgeId);
            edgeNode.SetVisible(false);
            if (rootNode != null)
            {
                sceneMgr.AddNode(rootNode);
                sceneMgr.AddNode(faceNode);
                sceneMgr.AddNode(edgeNode);
            }
            renderViewDraw.FitAll();
            renderViewDraw.RequestDraw(EnumRenderHint.RH_LoadScene);

            #endregion

        }
        private BendHelper BendUp(TopoShape face, TopoShape line, Bending bending)
        {
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
            Vector3 edPt = curve.Value(curve.LastParameter()); //终点
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
            TopoShape sweep = GlobalInstance.BrepTools.Sweep(wireSketch, line, true);
            TopoShape endEdge = GlobalInstance.BrepTools.MakeLine(edLine, edLine + edPt - stPt);
            #endregion
            BendHelper bend = new BendHelper()
            {
                Sweep = sweep,
                EdLine = endEdge
            };
            return bend;
        }
        private BendHelper BendDown (TopoShape face, TopoShape line, Bending bending)
        {
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
            Vector3 edPt = curve.Value(curve.LastParameter()); //终点
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
            TopoShape sweep = GlobalInstance.BrepTools.Sweep(wireSketch, line, true);
            TopoShape endEdge = GlobalInstance.BrepTools.MakeLine(edLine, edLine + edPt - stPt);
            #endregion
            BendHelper bend = new BendHelper()
            {
                Sweep = sweep,
                EdLine = endEdge
            };
            return bend;
        }

        private void BtnExportXml_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog();
        }
        private void BtnReadXml_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }
        private void SaveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            ExportXml.GenerateXml(bendings,saveFileDialog1.FileName);
        }
        private void OpenFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            bendings = ExportXml.ReadXml(openFileDialog1.FileName);
            DrawBendingGroup2(bendings);
        }
        private void DrawBendingGroup(BendingGroup bendings)
        {
            TopoShape face = GlobalInstance.BrepTools.LoadBuffer(bendings.Buffer);

            renderViewDraw.ClearScene();
            //group.Add(face);
            SceneManager sceneMgr = renderViewDraw.SceneManager;
            SceneNode rootNode = GlobalInstance.TopoShapeConvert.ToSceneNode(face, 0.1f);
            if (rootNode != null)
            {
                sceneMgr.AddNode(rootNode);
            }

            foreach (Bending bending in bendings.Bendings)
            {
                TopoShape shape = GlobalInstance.BrepTools.LoadBuffer(bending.Buffer);
                ElementId id = new ElementId(bending.Index);
                SceneNode node = GlobalInstance.TopoShapeConvert.ToSceneNode(shape, 0.1f);
                node.SetId(id);
                if (node != null)
                {
                    sceneMgr.AddNode(node);
                }
            }
            renderViewDraw.FitAll();
            renderViewDraw.RequestDraw(EnumRenderHint.RH_LoadScene);
        }
        private void DrawBendingGroup2(BendingGroup bendings)
        {
            #region 绘制矩形并标记四条边
            var pt0 = new Vector3(bendings.Length, 0, 0);
            var pt1 = new Vector3(0, 0, 0);
            var pt2 = new Vector3(0, bendings.Width, 0);
            var pt3 = new Vector3(bendings.Length, bendings.Width, 0);
            TopoShape baseEdge1 = GlobalInstance.BrepTools.MakeLine(pt0, pt1);
            TopoShape baseEdge2 = GlobalInstance.BrepTools.MakeLine(pt1, pt2);
            TopoShape baseEdge3 = GlobalInstance.BrepTools.MakeLine(pt2, pt3);
            TopoShape baseEdge4 = GlobalInstance.BrepTools.MakeLine(pt3, pt0);
            TopoShapeGroup group = new TopoShapeGroup();
            group.Add(baseEdge1);
            group.Add(baseEdge2);
            group.Add(baseEdge3);
            group.Add(baseEdge4);
            TopoShape rect = GlobalInstance.BrepTools.MakeSpline(group);
            TopoShape baseShape = GlobalInstance.BrepTools.MakeFace(rect); 

            SceneManager sceneMgr = renderViewDraw.SceneManager;
            SceneNode rootNode1 = GlobalInstance.TopoShapeConvert.ToSceneNode(baseEdge1, 0.1f);
            SceneNode rootNode2 = GlobalInstance.TopoShapeConvert.ToSceneNode(baseEdge2, 0.1f);
            SceneNode rootNode3 = GlobalInstance.TopoShapeConvert.ToSceneNode(baseEdge3, 0.1f);
            SceneNode rootNode4 = GlobalInstance.TopoShapeConvert.ToSceneNode(baseEdge4, 0.1f);
            SceneNode root = GlobalInstance.TopoShapeConvert.ToSceneNode(baseShape, 0.1f);
            rootNode1.SetVisible(false);
            rootNode2.SetVisible(false);
            rootNode3.SetVisible(false);
            rootNode4.SetVisible(false);
            sceneMgr.AddNode(rootNode1);
            sceneMgr.AddNode(rootNode2);
            sceneMgr.AddNode(rootNode3);
            sceneMgr.AddNode(rootNode4);
            sceneMgr.AddNode(root);
            #endregion

            #region 按四个方向分别折弯
            var groupEdge1 = from m in bendings.Bendings
                             where m.Orientation == EnumEdge.Edge_1
                             select m;
            var groupEdge2 = from m in bendings.Bendings
                             where m.Orientation == EnumEdge.Edge_2
                             select m;
            var groupEdge3 = from m in bendings.Bendings
                             where m.Orientation == EnumEdge.Edge_3
                             select m;
            var groupEdge4 = from m in bendings.Bendings
                             where m.Orientation == EnumEdge.Edge_4
                             select m;
            
            SceneNode stNode = rootNode1;
            foreach (var bending in groupEdge1)
            {
                sceneMgr.ClearSelection();
                sceneMgr.SelectNode(stNode);
                SelectedShapeQuery context = new SelectedShapeQuery();
                renderViewDraw.QuerySelection(context);
                var face = context.GetGeometry();
                var line = context.GetSubGeometry();
                if (face == null || line == null)
                {
                    return;
                }
                if (face.GetShapeType() != EnumTopoShapeType.Topo_FACE || line.GetShapeType() != EnumTopoShapeType.Topo_EDGE)
                {
                    break;
                }
                BendHelper helper = new BendHelper();
                if (bending.Direction.Equals(EnumDir.Edge_UP))
                {
                    helper = BendUp(face, line, bending);
                }
                else
                {
                    helper = BendDown(face, line, bending);
                }
                ElementId id = new ElementId(bending.Index);
                SceneNode node = GlobalInstance.TopoShapeConvert.ToSceneNode(helper.Sweep, 0.1f);
                node.SetId(id);
                stNode = GlobalInstance.TopoShapeConvert.ToSceneNode(helper.EdLine, 0.1f);
                stNode.SetVisible(false);
                sceneMgr.AddNode(node);
                sceneMgr.AddNode(stNode);
            }
            stNode = rootNode2;
            foreach (var bending in groupEdge2)
            {
                sceneMgr.ClearSelection();
                sceneMgr.SelectNode(stNode);
                SelectedShapeQuery context = new SelectedShapeQuery();
                renderViewDraw.QuerySelection(context);
                var face = context.GetGeometry();
                var line = context.GetSubGeometry();
                if (face == null)
                {
                    break;
                }
                BendHelper helper = new BendHelper();
                if (bending.Direction.Equals(EnumDir.Edge_UP))
                {
                    helper = BendUp(face, line, bending);
                }
                else
                {
                    helper = BendDown(face, line, bending);
                }

                ElementId id = new ElementId(bending.Index);
                SceneNode node = GlobalInstance.TopoShapeConvert.ToSceneNode(helper.Sweep, 0.1f);
                node.SetId(id);
                stNode = GlobalInstance.TopoShapeConvert.ToSceneNode(helper.EdLine, 0.1f);
                stNode.SetVisible(false);
                sceneMgr.AddNode(node);
                sceneMgr.AddNode(stNode);
            }
            stNode = rootNode3;
            foreach (var bending in groupEdge3)
            {
                sceneMgr.ClearSelection();
                sceneMgr.SelectNode(stNode);
                SelectedShapeQuery context = new SelectedShapeQuery();
                renderViewDraw.QuerySelection(context);
                var face = context.GetGeometry();
                var line = context.GetSubGeometry();
                if (face == null)
                {
                    break;
                }
                BendHelper helper = new BendHelper();
                if (bending.Direction.Equals(EnumDir.Edge_UP))
                {
                    helper = BendUp(face, line, bending);
                }
                else
                {
                    helper = BendDown(face, line, bending);
                }

                ElementId id = new ElementId(bending.Index);
                SceneNode node = GlobalInstance.TopoShapeConvert.ToSceneNode(helper.Sweep, 0.1f);
                node.SetId(id);
                stNode = GlobalInstance.TopoShapeConvert.ToSceneNode(helper.EdLine, 0.1f);
                stNode.SetVisible(false);
                sceneMgr.AddNode(node);
                sceneMgr.AddNode(stNode);
            }
            stNode = rootNode4;
            foreach (var bending in groupEdge4)
            {
                sceneMgr.ClearSelection();
                sceneMgr.SelectNode(stNode);
                SelectedShapeQuery context = new SelectedShapeQuery();
                renderViewDraw.QuerySelection(context);
                var face = context.GetGeometry();
                var line = context.GetSubGeometry();
                if (face == null)
                {
                    break;
                }
                BendHelper helper = new BendHelper();
                if (bending.Direction.Equals(EnumDir.Edge_UP))
                {
                    helper = BendUp(face, line, bending);
                }
                else
                {
                    helper = BendDown(face, line, bending);
                }

                ElementId id = new ElementId(bending.Index);
                SceneNode node = GlobalInstance.TopoShapeConvert.ToSceneNode(helper.Sweep, 0.1f);
                node.SetId(id);
                stNode = GlobalInstance.TopoShapeConvert.ToSceneNode(helper.EdLine, 0.1f);
                stNode.SetVisible(false);
                sceneMgr.AddNode(node);
                sceneMgr.AddNode(stNode);
            }
            #endregion

            renderViewDraw.ClearScene();
            renderViewDraw.FitAll();
            renderViewDraw.RequestDraw(EnumRenderHint.RH_LoadScene);

        }
    }
}


