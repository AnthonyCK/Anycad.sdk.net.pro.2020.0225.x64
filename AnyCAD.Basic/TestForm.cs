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
            renderViewDraw.MouseClick += new MouseEventHandler(OnRenderWindow_MouseClick);

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
                renderViewDraw.Cursor = Cursors.SizeAll;
            }
            else if (cursorHint == "Orbit")
            {
                this.renderView.Cursor = System.Windows.Forms.Cursors.Hand;
                renderViewDraw.Cursor = Cursors.Hand;
            }
            else if (cursorHint == "Cross")
            {
                this.renderView.Cursor = System.Windows.Forms.Cursors.Cross;
                renderViewDraw.Cursor = Cursors.Cross;
            }
            else
            {
                if (commandId == "Pick")
                {
                    this.renderView.Cursor = System.Windows.Forms.Cursors.Arrow;
                    renderViewDraw.Cursor = Cursors.Arrow;
                }
                else
                {
                    this.renderView.Cursor = System.Windows.Forms.Cursors.Default;
                    renderViewDraw.Cursor = Cursors.Default;
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
            if (renderViewDraw != null)
            {
                System.Drawing.Size size = this.panel4.ClientSize;
                renderViewDraw.Size = size;
            }
        }

        private void PanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            renderView.ExecuteCommand("Pan");
            if (renderViewDraw != null)
            {
                renderViewDraw.ExecuteCommand("Pan"); 
            }
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
            if (renderViewDraw != null)
            {
                renderViewDraw.ExecuteCommand("Pick"); 
            }
        }
        private void ClearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            renderView.ClearScene();
            renderViewXZ.ClearScene();
            renderViewYZ.ClearScene();
            if (renderViewDraw != null)
            {
                renderViewDraw.ClearScene();
                Vecs.Clear();
            }
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

        #region Draw sketch
        private bool m_PickPoint = false;
        private List<Vector3> Vecs = new List<Vector3>();
        private TopoShapeGroup EdgeG = new TopoShapeGroup();
        private void HitTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_PickPoint = !m_PickPoint;
        }
        private void OnRenderWindow_MouseClick(object sender, MouseEventArgs e)
        {
            if (!m_PickPoint)
                return;

            //PickHelper pickHelper = renderViewDraw.PickShape(e.X, e.Y);
            //if (pickHelper != null)
            //{
            //    // add a ball
            //    Platform.TopoShape shape = GlobalInstance.BrepTools.MakeSphere(pickHelper.GetPointOnShape(), 2);
            //    renderView.ShowGeometry(shape, 100);
            //}
            //// Try the grid
            Vector3 pt = renderViewDraw.HitPointOnGrid(e.X, e.Y);
            if (pt != null)
            {
                if (Vecs.Count() == 0)
                {
                    Vecs.Add(pt);
                    TopoShape shape = GlobalInstance.BrepTools.MakeSphere(pt, 1);
                    renderViewDraw.ShowGeometry(shape, 100);
                }
                else
                {
                    var c = from m in Vecs
                            where m.Distance(pt) <= 1
                            select m;

                    if (c.Count() == 0)
                    {
                        var pt0 = Vecs.Last();
                        Vecs.Add(pt);
                        TopoShape shape = GlobalInstance.BrepTools.MakeSphere(pt, 1);
                        TopoShape edge = GlobalInstance.BrepTools.MakeLine(pt0, pt);
                        EdgeG.Add(edge);
                        renderViewDraw.ShowGeometry(shape, 100);
                        renderViewDraw.ShowGeometry(edge, 100);
                    }
                    else if (Vecs.First().Equals(c.Last()))
                    {
                        TopoShape edge = GlobalInstance.BrepTools.MakeLine(Vecs.Last(), Vecs.First());
                        EdgeG.Add(edge);
                        renderViewDraw.ShowGeometry(edge, 100);
                        m_PickPoint = !m_PickPoint;
                    }
                }
            }
        }

        #endregion

        private void SectionBtn_Click(object sender, EventArgs e)
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

        private BendingGroup bendings = new BendingGroup();
        private void BtnDraw_Click(object sender, EventArgs e)
        {
            //var w = GlobalInstance.BrepTools.MakeWire(EdgeG);
            //var t = GlobalInstance.BrepTools.MakeFace(w);
            //t = GlobalInstance.BrepTools.FillFace(Vecs);
            //renderViewDraw.ShowGeometry(t, 100);
            Vecs.Clear();
            Vecs.Add(new Vector3(0, 0, 0));
            Vecs.Add(new Vector3(Convert.ToDouble(txtL.Text), 0, 0));
            Vecs.Add(new Vector3(Convert.ToDouble(txtL.Text), Convert.ToDouble(txtW.Text), 0));
            Vecs.Add(new Vector3(0, Convert.ToDouble(txtW.Text), 0));
            bendings = new BendingGroup
            {
                Vertexes = Vecs,
            };
            var face = DrawRect(Vecs);
        }
        private TopoShape DrawRect(List<Vector3> vertex)
        {
            //TopoShape rect = GlobalInstance.BrepTools.MakeRectangle(length, width, 0, Coordinate3.UNIT_XYZ);
            //TopoShape face = GlobalInstance.BrepTools.MakeFace(rect);
            var face = GlobalInstance.BrepTools.FillFace(vertex);
            
            renderViewDraw.ClearScene();
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
            if (dirL.Y >= 0)
            {
                bending.Orientation = Math.Round(dirL.AngleBetween(Vector3.UNIT_X), 3);
            }
            else
            {
                bending.Orientation = Math.Round(360 - dirL.AngleBetween(Vector3.UNIT_X), 3);
            }
            
            //if (dirL.X == 1)
            //{
            //    bending.Orientation = 0;
            //}
            //else if (dirL.Y == 1)
            //{
            //    bending.Orientation = EnumEdge.Edge_2;
            //}
            //else if (dirL.X == -1)
            //{
            //    bending.Orientation = EnumEdge.Edge_3;
            //}
            //else
            //{
            //    bending.Orientation = EnumEdge.Edge_4;
            //}

            TopoShape sweep = BendUp(face, line, bending).Sweep;

            bendings.Add(bending);

            #region 渲染
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
            if (dirL.Y >= 0)
            {
                bending.Orientation = Math.Round(dirL.AngleBetween(Vector3.UNIT_X), 3);
            }
            else
            {
                bending.Orientation = Math.Round(360 - dirL.AngleBetween(Vector3.UNIT_X), 3);
            }
            //if (dirL.X == 1)
            //{
            //    bending.Orientation = EnumEdge.Edge_1;
            //}
            //else if (dirL.Y == 1)
            //{
            //    bending.Orientation = EnumEdge.Edge_2;
            //}
            //else if (dirL.X == -1)
            //{
            //    bending.Orientation = EnumEdge.Edge_3;
            //}
            //else
            //{
            //    bending.Orientation = EnumEdge.Edge_4;
            //}

            TopoShape sweep = BendDown(face, line, bending).Sweep;

            bendings.Add(bending);

            #region 渲染
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
            if (arc != null)
            {
                lineGroup.Add(arc);
            }
            Vector3 edLine = dirL.CrossProduct(radius2) * (bending.Length / bending.Radius) + edArc;
            arc = GlobalInstance.BrepTools.MakeLine(edArc, edLine);
            lineGroup.Add(arc);
            //扫描生成折弯
            TopoShape wireSketch = GlobalInstance.BrepTools.MakeWire(lineGroup);
            TopoShape sweep;
            if (wireSketch != null)
            {
                sweep = GlobalInstance.BrepTools.Sweep(wireSketch, line, true);
            }
            else
            {
                sweep = GlobalInstance.BrepTools.Sweep(arc, line, true);
            }
            TopoShape oFace = GlobalInstance.BrepTools.Sweep(arc, line, true).GetSubShape(0, 1);
            TopoShape oEdge = GlobalInstance.BrepTools.MakeLine(edLine, edLine + edPt - stPt);
            #endregion
            BendHelper bend = new BendHelper()
            {
                Sweep = sweep,
                EdFace = oFace,
                EdLine = oEdge
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
            if (arc != null)
            {
                lineGroup.Add(arc);
            }
            Vector3 edLine = dirL.CrossProduct(radius2) * (bending.Length / bending.Radius) + edArc;
            arc = GlobalInstance.BrepTools.MakeLine(edArc, edLine);
            lineGroup.Add(arc);
            //扫描生成折弯
            TopoShape wireSketch = GlobalInstance.BrepTools.MakeWire(lineGroup);
            TopoShape sweep;
            if (wireSketch != null)
            {
                sweep = GlobalInstance.BrepTools.Sweep(wireSketch, line, true);
            }
            else
            {
                sweep = GlobalInstance.BrepTools.Sweep(arc, line, true);
            }
            TopoShape oFace = GlobalInstance.BrepTools.Sweep(arc, line, true).GetSubShape(0, 1);
            TopoShape oEdge = GlobalInstance.BrepTools.MakeLine(edLine, edLine + edPt - stPt);
            #endregion
            BendHelper bend = new BendHelper()
            {
                Sweep = sweep,
                EdFace = oFace,
                EdLine = oEdge
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
        private void SaveFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            ExportXml.GenerateXml(bendings,saveFileDialog1.FileName);
        }
        private void OpenFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            bendings = ExportXml.ReadXml(openFileDialog1.FileName);
            DrawBendingGroup(bendings);
        }
        private void BtnUnfold_Click(object sender, EventArgs e)
        {
            BendingGroup group = new BendingGroup(bendings);
            stepBendings.Add(bendings);
            DrawUnfoldGroup(bendings);
            foreach(var item in GetStepShapes(group))
            {
                if (stepBendings.Contains(item))
                {
                    break;
                }
                stepBendings.Add(new BendingGroup(item));
            }
        }
        private void DrawBendingGroup(BendingGroup bends)
        {
            renderViewDraw.ClearScene();

            #region 绘制底面
            //var pt0 = new Vector3(0, 0, 0);
            //var pt1 = new Vector3(bends.Length, 0, 0);
            //var pt2 = new Vector3(bends.Length, bends.Width, 0);
            //var pt3 = new Vector3(0, bends.Width, 0);
            //TopoShape baseEdge1 = GlobalInstance.BrepTools.MakeLine(pt0, pt1);
            //TopoShape baseEdge2 = GlobalInstance.BrepTools.MakeLine(pt1, pt2);
            //TopoShape baseEdge3 = GlobalInstance.BrepTools.MakeLine(pt2, pt3);
            //TopoShape baseEdge4 = GlobalInstance.BrepTools.MakeLine(pt3, pt0);
            //TopoShape rect = GlobalInstance.BrepTools.MakeRectangle(bends.Length, bends.Width, 0, Coordinate3.UNIT_XYZ);
            //TopoShape baseShape = GlobalInstance.BrepTools.MakeFace(rect);
            TopoShape baseShape = GlobalInstance.BrepTools.FillFace(bends.Vertexes);

            SceneManager sceneMgr = renderViewDraw.SceneManager;
            SceneNode root = GlobalInstance.TopoShapeConvert.ToSceneNode(baseShape, 0.1f);
            sceneMgr.AddNode(root);
            #endregion

            #region 按逆时针方向依次折弯
            //var oris = bends.Bendings.OrderBy(m => m.Orientation).Select(m => m.Orientation).Distinct();
            Queue<Vector3> vertexQueue = new Queue<Vector3>(bends.Vertexes);
            for (int i = 0; i < vertexQueue.Count(); i++)
            {
                var sPt = vertexQueue.Dequeue();
                var ePt = vertexQueue.Peek();
                vertexQueue.Enqueue(sPt);
                var line = GlobalInstance.BrepTools.MakeLine(sPt, ePt);
                var face = baseShape;
                var groupEdge = from m in bends.Bendings
                                where m.Orientation == Math.Round(((ePt - sPt).Y >= 0 ? (ePt - sPt).AngleBetween(Vector3.UNIT_X) : (360 - (ePt - sPt).AngleBetween(Vector3.UNIT_X))), 3)
                                orderby m.Index
                                select m;
                foreach (var bending in groupEdge)
                {
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
                    sceneMgr.AddNode(node);
                    face = helper.EdFace;
                    line = helper.EdLine;
                }

            } 
            #endregion
            #region 按四个方向分别折弯
            //var groupEdge1 = from m in bends.Bendings
            //                 where m.Orientation == 0
            //                 orderby m.Index
            //                 select m ;
            //var groupEdge2 = from m in bends.Bendings
            //                 where m.Orientation == 1
            //                 orderby m.Index
            //                 select m;
            //var groupEdge3 = from m in bends.Bendings
            //                 where m.Orientation == 2
            //                 orderby m.Index
            //                 select m;
            //var groupEdge4 = from m in bends.Bendings
            //                 where m.Orientation == 3
            //                 orderby m.Index
            //                 select m;

            ////TopoShape line = baseEdge1;
            ////TopoShape face = baseShape;
            //foreach (var bending in groupEdge1)
            //{
            //    if (face == null || line == null)
            //    {
            //        return;
            //    }
            //    if (face.GetShapeType() != EnumTopoShapeType.Topo_FACE || line.GetShapeType() != EnumTopoShapeType.Topo_EDGE)
            //    {
            //        break;
            //    }
            //    BendHelper helper = new BendHelper();
            //    if (bending.Direction.Equals(EnumDir.Edge_UP))
            //    {
            //        helper = BendUp(face, line, bending);
            //    }
            //    else
            //    {
            //        helper = BendDown(face, line, bending);
            //    }
            //    ElementId id = new ElementId(bending.Index);
            //    SceneNode node = GlobalInstance.TopoShapeConvert.ToSceneNode(helper.Sweep, 0.1f);
            //    node.SetId(id);
            //    sceneMgr.AddNode(node);
            //    face = helper.EdFace;
            //    line = helper.EdLine;
            //}
            //face = baseShape;
            //line = baseEdge2;
            //foreach (var bending in groupEdge2)
            //{
            //    if (face == null)
            //    {
            //        break;
            //    }
            //    BendHelper helper = new BendHelper();
            //    if (bending.Direction.Equals(EnumDir.Edge_UP))
            //    {
            //        helper = BendUp(face, line, bending);
            //    }
            //    else
            //    {
            //        helper = BendDown(face, line, bending);
            //    }

            //    ElementId id = new ElementId(bending.Index);
            //    SceneNode node = GlobalInstance.TopoShapeConvert.ToSceneNode(helper.Sweep, 0.1f);
            //    node.SetId(id);
            //    sceneMgr.AddNode(node);
            //    face = helper.EdFace;
            //    line = helper.EdLine;
            //}
            //face = baseShape;
            //line = baseEdge3;
            //foreach (var bending in groupEdge3)
            //{
            //    if (face == null)
            //    {
            //        break;
            //    }
            //    BendHelper helper = new BendHelper();
            //    if (bending.Direction.Equals(EnumDir.Edge_UP))
            //    {
            //        helper = BendUp(face, line, bending);
            //    }
            //    else
            //    {
            //        helper = BendDown(face, line, bending);
            //    }

            //    ElementId id = new ElementId(bending.Index);
            //    SceneNode node = GlobalInstance.TopoShapeConvert.ToSceneNode(helper.Sweep, 0.1f);
            //    node.SetId(id);
            //    sceneMgr.AddNode(node);
            //    face = helper.EdFace;
            //    line = helper.EdLine;
            //}
            //face = baseShape;
            //line = baseEdge4;
            //foreach (var bending in groupEdge4)
            //{
            //    if (face == null)
            //    {
            //        break;
            //    }
            //    BendHelper helper = new BendHelper();
            //    if (bending.Direction.Equals(EnumDir.Edge_UP))
            //    {
            //        helper = BendUp(face, line, bending);
            //    }
            //    else
            //    {
            //        helper = BendDown(face, line, bending);
            //    }

            //    ElementId id = new ElementId(bending.Index);
            //    SceneNode node = GlobalInstance.TopoShapeConvert.ToSceneNode(helper.Sweep, 0.1f);
            //    node.SetId(id);
            //    sceneMgr.AddNode(node);
            //    face = helper.EdFace;
            //    line = helper.EdLine;
            //}
            #endregion

            renderViewDraw.FitAll();
            renderViewDraw.RequestDraw(EnumRenderHint.RH_LoadScene);

        }
        private void DrawUnfoldGroup(BendingGroup bends)
        {
            renderViewDraw.ClearScene();

            #region 绘制底面
            TopoShape baseShape = GlobalInstance.BrepTools.FillFace(bends.Vertexes);

            SceneManager sceneMgr = renderViewDraw.SceneManager;
            SceneNode root = GlobalInstance.TopoShapeConvert.ToSceneNode(baseShape, 0.1f);
            sceneMgr.AddNode(root);
            #endregion

            #region 按逆时针方向依次折弯
            Queue<Vector3> vertexQueue = new Queue<Vector3>(bends.Vertexes);
            for (int i = 0; i < vertexQueue.Count(); i++)
            {
                var sPt = vertexQueue.Dequeue();
                var ePt = vertexQueue.Peek();
                vertexQueue.Enqueue(sPt);
                var line = GlobalInstance.BrepTools.MakeLine(sPt, ePt);
                var face = baseShape;
                var groupEdge = from m in bends.Bendings
                                where m.Orientation == Math.Round(((ePt - sPt).Y >= 0 ? (ePt - sPt).AngleBetween(Vector3.UNIT_X) : (360 - (ePt - sPt).AngleBetween(Vector3.UNIT_X))), 3)
                                orderby m.Index
                                select m;
                foreach (var bending in groupEdge)
                {
                    if (face == null || line == null)
                    {
                        return;
                    }
                    if (face.GetShapeType() != EnumTopoShapeType.Topo_FACE || line.GetShapeType() != EnumTopoShapeType.Topo_EDGE)
                    {
                        break;
                    }
                    BendHelper helper = new BendHelper();
                    var temp = new Bending(bending);
                    temp.Angle = 0;
                    helper = BendUp(face, line, temp);
                    ElementId id = new ElementId(bending.Index);
                    SceneNode node = GlobalInstance.TopoShapeConvert.ToSceneNode(helper.Sweep, 0.1f);
                    node.SetId(id);
                    sceneMgr.AddNode(node);
                    face = helper.EdFace;
                    line = helper.EdLine;
                }

            }
            #endregion

            renderViewDraw.FitAll();
            renderViewDraw.RequestDraw(EnumRenderHint.RH_LoadScene);

        }
        private static IEnumerable<BendingGroup> GetStepShapes(BendingGroup bends)
        {
            BendingGroup group = new BendingGroup(bends);
            List<Bending> temp = new List<Bending>(group.Bendings.OrderBy(m => m.Index).ToList());
            foreach(var item in temp)
            {
                item.Angle = 0;
                group.Bendings = temp;
                yield return group;
            }
        }
        private int posOfStep = 0;
        private List<BendingGroup> stepBendings = new List<BendingGroup>();
        private void BtnNext_Click(object sender, EventArgs e)
        {
            if (posOfStep >= stepBendings.Count())
            {
                return;
            }
            var item = stepBendings.ElementAt(posOfStep++);
            DrawBendingGroup(item);
        }
        private void BtnLast_Click(object sender, EventArgs e)
        {
            if (posOfStep <= 0)
            {
                return;
            }
            var item = stepBendings.ElementAt(--posOfStep);
            DrawBendingGroup(item);
        }

    }
}


