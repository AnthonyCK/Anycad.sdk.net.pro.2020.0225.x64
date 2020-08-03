using AnyCAD.Platform;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EPunch.Multibend
{
    public class DataManage
    {
        public DataSet BendingSet { get; }

        public delegate void DataChangeHandler();
        public event DataChangeHandler DataChange;

        protected virtual void OnDataChange()
        {
            DataChange?.Invoke();
        }
        public DataManage() 
        {
            BendingSet = new DataSet("BendingSet");
            DataTable bendingTable = BendingSet.Tables.Add("Bendings");
            DataTable vertexTable = BendingSet.Tables.Add("Vertexes");

            DataColumn pkBendingID 
                = bendingTable.Columns.Add("BendingID", typeof(int));
            bendingTable.Columns.Add("Orientation", typeof(double));
;           bendingTable.Columns.Add("Direction", typeof(EnumDir));
            bendingTable.Columns.Add("Angle", typeof(double));
            bendingTable.Columns.Add("Radius", typeof(double));
            bendingTable.Columns.Add("Length", typeof(double));
            bendingTable.PrimaryKey = new DataColumn[] { pkBendingID };

            DataColumn pkVertexID 
                = vertexTable.Columns.Add("VertexID", typeof(int));
            vertexTable.Columns.Add("Vertex", typeof(Vector3));
            //pkVertexID.AutoIncrement = true;
            //pkVertexID.AutoIncrementStep = 1;
            vertexTable.PrimaryKey = new DataColumn[] { pkVertexID };
        }
        public void AddVertex(Vector3 vector)
        {
            DataRow row;
            row = BendingSet.Tables["Vertexes"].NewRow();
            row["Vertex"] = vector;
            row["VertexID"] = BendingSet.Tables["Vertexes"].Rows.Count;
            BendingSet.Tables["Vertexes"].Rows.Add(row);
        }
        public void AddBending(Bending bending)
        {
            DataRow row;
            row = BendingSet.Tables["Bendings"].NewRow();
            row["BendingID"] = BendingSet.Tables["Bendings"].Rows.Count;
            row["Orientation"] = bending.Orientation;
            row["Direction"] = bending.Direction;
            row["Angle"] = bending.Angle;
            row["Radius"] = bending.Radius;
            row["Length"] = bending.Length;
            BendingSet.Tables["Bendings"].Rows.Add(row);
        }
        public void DeleteVertex(int id)
        {
            DataRow row;
            row = BendingSet.Tables["Vertexes"].Rows.Find(id);
            BendingSet.Tables["Vertexes"].Rows.Remove(row);
        }
        public void DeleteVertex(int[] ids)
        {
            DataRow row;
            row = BendingSet.Tables["Vertexes"].Rows.Find(ids);
            BendingSet.Tables["Vertexes"].Rows.Remove(row);

        }
        public void DeleteBending(int id)
        {
            DataRow row;
            row = BendingSet.Tables["Bendings"].Rows.Find(id);
            BendingSet.Tables["Bendings"].Rows.Remove(row);
        }
        public void DeleteBending(int[] ids)
        {
            DataRow row;
            row = BendingSet.Tables["Bendings"].Rows.Find(ids);
            BendingSet.Tables["Bendings"].Rows.Remove(row);
        }
        public void EditVertex(int id, Vector3 vector)
        {
            var row = BendingSet.Tables["Vertexes"].Rows.Find(id);
            row.BeginEdit();
            row["Vertex"] = vector;
            row.EndEdit();
        }
        public void EditBending(int id, Bending bending)
        {
            var row = BendingSet.Tables["Bendings"].Rows.Find(id);
            row.BeginEdit();
            row["BendingID"] = bending.Index;
            row["Orientation"] = bending.Orientation;
            row["Direction"] = bending.Direction;
            row["Angle"] = bending.Angle;
            row["Radius"] = bending.Radius;
            row["Length"] = bending.Length;
            row.EndEdit();
        }
        public void SetBendingGroup(BendingGroup group)
        {
            BendingSet.Tables[0].Rows.Clear();
            BendingSet.Tables[1].Rows.Clear();
            foreach(var item in group.Vertexes)
            {
                this.AddVertex(item);
            }
            foreach(var item in group.Bendings)
            {
                this.AddBending(item);
            }
        }
        public void AcceptChanges()
        {
            BendingSet.AcceptChanges();
            OnDataChange();
        }
    }
}
