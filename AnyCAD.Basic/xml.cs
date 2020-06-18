using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

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

        public Bending (EnumEdge edge, int id, EnumDir dir, double ag, double ra, double le)
        {
            Orientation = edge;
            Index = id;
            Direction = dir;
            Angle = ag;
            Radius = ra;
            Length = le;
        }
    }
}
