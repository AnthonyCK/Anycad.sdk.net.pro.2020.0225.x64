using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace AnyCAD.Basic
{
    public enum EnumEdge
    {
        Edge_1 = 0,
        Edge_2 = 1,
        Edge_3 = 2,
        Edge_4 = 3
    }
    public enum EnumDir
    {
        Edge_UP = 0,
        Edge_DOWN =1
    }

    public class Bending
    {
        public EnumEdge Orientation;
        public int Index;
        public EnumDir Direction;
        public double Angle;
        public double Radius;
        public double Length;
    }
    public class BengdingGroup
    {
        public List<Bending> bendings = new List<Bending>();

        public void Add (Bending bending)
        {
            switch (bending.Orientation)
            {
                case EnumEdge.Edge_1:
                    var group1 = from bend in bendings
                                where bend.Orientation == EnumEdge.Edge_1
                                select bend;
                    if (group1.Count() == 0)
                    {
                        bending.Index = 0;
                    }
                    else
                    {
                        bending.Index = group1.Last().Index + 1;
                    }
                    bendings.Add(bending);
                    break;
                case EnumEdge.Edge_2:
                    var group2 = from bend in bendings
                                where bend.Orientation == EnumEdge.Edge_2
                                select bend;
                    if (group2.Count() == 0)
                    {
                        bending.Index = 0;
                    }
                    else
                    {
                        bending.Index = group2.Last().Index + 1;
                    }
                    bendings.Add(bending);
                    break;
                case EnumEdge.Edge_3:
                    var group3 = from bend in bendings
                                 where bend.Orientation == EnumEdge.Edge_3
                                 select bend;
                    if (group3.Count() == 0)
                    {
                        bending.Index = 0;
                    }
                    else
                    {
                        bending.Index = group3.Last().Index + 1;
                    }
                    bendings.Add(bending);
                    break;
                case EnumEdge.Edge_4:
                    var group4 = from bend in bendings
                                 where bend.Orientation == EnumEdge.Edge_4
                                 select bend;
                    if (group4.Count() == 0)
                    {
                        bending.Index = 0;
                    }
                    else
                    {
                        bending.Index = group4.Last().Index + 1;
                    }
                    bendings.Add(bending);
                    break;
                default:
                    break;
            }
        }
    }
    class ExportXml
    {
        public static void GenerateXml (BengdingGroup bendings)
        {
            XmlSerializer writer = new XmlSerializer(typeof(BengdingGroup));
            var path = new System.IO.StreamWriter(@"c:\batch\Serialization.xml");
            writer.Serialize(path, bendings);
            path.Close();
            MessageBox.Show("输出成功！", "输出提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        public static void ReadXml()
        {
            XmlSerializer reader = new XmlSerializer(typeof(BengdingGroup));
            var path = new System.IO.StreamReader(@"c:\batch\Serialization.xml");
            BengdingGroup bendings = reader.Deserialize(path) as BengdingGroup;
            path.Close();
            MessageBox.Show("Last id: " + bendings.bendings.Last().Index.ToString(), "输入提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
