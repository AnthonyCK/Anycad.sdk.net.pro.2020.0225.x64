using AnyCAD.Platform;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace EPunch.Multibend
{
    public class EdgeOri
    {
        public double Id;
        public Vector3 Ori;
    }
    public enum EnumDir
    {
        Edge_UP = 0,
        Edge_DOWN =1
    }
    public class BendHelper
    {
        public TopoShape Sweep;
        public TopoShape EdFace;
        public TopoShape EdLine;
    }
    public class Bending
    {
        [XmlAttribute]
        public double Orientation;

        public int Index;
        public EnumDir Direction;
        public double Angle;
        public double Radius;
        public double Length;

        public Bending() { }
        public Bending(Bending previousB)
        {
            Orientation = previousB.Orientation;
            Index = previousB.Index;
            Direction = previousB.Direction;
            Angle = previousB.Angle;
            Radius = previousB.Radius;
            Length = previousB.Length;
        }
    }

    [XmlRoot("BendingGroup", IsNullable = false)]
    public class BendingGroup
    {
        [XmlArray("Vertexes")]
        public List<Vector3> Vertexes = new List<Vector3>();
        //public double Length;
        //public double Width;

        [XmlArray("Bendings")]
        public List<Bending> Bendings = new List<Bending>();

        public BendingGroup() { }
        public BendingGroup(BendingGroup previousBG)
        {
            Vertexes = new List<Vector3>(previousBG.Vertexes);
            var temp = new List<Bending>();
            foreach(var m in previousBG.Bendings)
            {
                temp.Add(new Bending(m));
            }
            Bendings = temp;
        }
        public void AddVertex(Vector3 vector)
        {
            Vertexes.Add(vector);
        }
        public void AddBending (Bending bending)
        {
            #region 总体编号
            if (Bendings.Count() == 0)
            {
                bending.Index = 0;
            }
            else
            {
                bending.Index = Bendings.Last().Index + 1;
            }
            Bendings.Add(bending);
            #endregion
            #region 按方向分别编号
            //switch (bending.Orientation)
            //{
            //    case EnumEdge.Edge_1:
            //        var group1 = from bend in Bendings
            //                     where bend.Orientation == EnumEdge.Edge_1
            //                     select bend;
            //        if (group1.Count() == 0)
            //        {
            //            bending.Index = 0;
            //        }
            //        else
            //        {
            //            bending.Index = group1.Last().Index + 1;
            //        }
            //        Bendings.Add(bending);
            //        break;
            //    case EnumEdge.Edge_2:
            //        var group2 = from bend in Bendings
            //                     where bend.Orientation == EnumEdge.Edge_2
            //                     select bend;
            //        if (group2.Count() == 0)
            //        {
            //            bending.Index = 0;
            //        }
            //        else
            //        {
            //            bending.Index = group2.Last().Index + 1;
            //        }
            //        Bendings.Add(bending);
            //        break;
            //    case EnumEdge.Edge_3:
            //        var group3 = from bend in Bendings
            //                     where bend.Orientation == EnumEdge.Edge_3
            //                     select bend;
            //        if (group3.Count() == 0)
            //        {
            //            bending.Index = 0;
            //        }
            //        else
            //        {
            //            bending.Index = group3.Last().Index + 1;
            //        }
            //        Bendings.Add(bending);
            //        break;
            //    case EnumEdge.Edge_4:
            //        var group4 = from bend in Bendings
            //                     where bend.Orientation == EnumEdge.Edge_4
            //                     select bend;
            //        if (group4.Count() == 0)
            //        {
            //            bending.Index = 0;
            //        }
            //        else
            //        {
            //            bending.Index = group4.Last().Index + 1;
            //        }
            //        Bendings.Add(bending);
            //        break;
            //    default:
            //        break;
            //}

            #endregion
        }
        public void SetBendingGroup(BendingGroup bendingGroup)
        {
            Vertexes = new List<Vector3>(bendingGroup.Vertexes);
            var temp = new List<Bending>();
            foreach (var m in bendingGroup.Bendings)
            {
                temp.Add(new Bending(m));
            }
            Bendings = temp;
        }
        public void SetBendingGroup(DataManage dataSet)
        {
            var ver = new List<Vector3>();
            var ben = new List<Bending>();
            foreach(DataRow item in dataSet.BendingSet.Tables["Vertexes"].Rows)
            {
                ver.Add(item.Field<Vector3>("Vertex"));
            }
            foreach(DataRow item in dataSet.BendingSet.Tables["Bendings"].Rows)
            {
                Bending temp = new Bending
                {
                    Angle = item.Field<double>("Angle"),
                    Direction = item.Field<EnumDir>("Direction"),
                    Index = item.Field<int>("BendingID"),
                    Length = item.Field<double>("Length"),
                    Orientation = item.Field<double>("Orientation"),
                    Radius = item.Field<double>("Radius")
                };
                ben.Add(temp);
            }
            Vertexes = ver;
            Bendings = ben;
        }
    }
    class ExportXml
    {
        public static void GenerateXml (BendingGroup bendings,string file)
        {
            XmlSerializer writer = new XmlSerializer(typeof(BendingGroup));
            var path = new System.IO.StreamWriter(file);
            writer.Serialize(path, bendings);
            path.Close();
            MessageBox.Show("输出成功！", "输出提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        public static BendingGroup ReadXml(string file)
        {
            XmlSerializer reader = new XmlSerializer(typeof(BendingGroup));
            var path = new System.IO.StreamReader(file);
            BendingGroup bendings = reader.Deserialize(path) as BendingGroup;
            path.Close();
            return bendings;
        }
    }
}
