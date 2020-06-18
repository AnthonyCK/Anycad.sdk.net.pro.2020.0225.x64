using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

    class Bending
    {
        public EnumEdge Orientation;
        public int Index;
        public EnumDir Direction;
        public double Angle;
        public double Radius;
        public double Length;

        public Bending() { }
        public Bending(EnumEdge edge, int id, EnumDir dir, double ag, double ra, double le)
        {
            Orientation = edge;
            Index = id;
            Direction = dir;
            Angle = ag;
            Radius = ra;
            Length = le;
        }
    }
    class ExportXml
    {
        public static void GenerateXml(List<Bending> bendings)
        {
            XmlSerializer writer = new XmlSerializer(typeof(List<Bending>));
            var path = new System.IO.StreamWriter(@"c:\batch\Serialization.xml");
            writer.Serialize(path, bendings);
            path.Close();
        }
        public static void ReadXml()
        {
            XmlSerializer reader = new XmlSerializer(typeof(List<Bending>));
            var path = new System.IO.StreamReader(@"c:\batch\Serialization.xml");
            List<Bending> bendings = reader.Deserialize(path) as List<Bending>;
            path.Close();
            Console.WriteLine(bendings.First().Index);
        }
    }
}
