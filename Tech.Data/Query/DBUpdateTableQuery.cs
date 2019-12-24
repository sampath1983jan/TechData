using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tech.Data.Query
{
   public enum AlterType
    {
        addcolumn,
        altercolumn,
        dropcolumn,
        addprimary,
        dropprimary
    }

    public abstract class DBUpdateTableQuery: DBUpdateTable
    {
       
        internal string TableName { get; set; }
        internal string TableOwner { get; set; }
        protected internal DBColumnList Columns { get; protected set; }
        internal AlterType Type { get; set; }

        public static DBUpdateTableQuery Table(string owner, string name)
        {
            return new DBUpdateTableQueryRef(owner, name);
        }
        public static DBUpdateTableQuery Table(string name)
        {
            return Table(string.Empty, name);
        }
        public DBUpdateTableQuery(string name,string owner) {
            this.TableName = name;
            this.TableOwner = owner;
            this.Columns = new DBColumnList();
        }
        public void AlterTableAddColumn(DBColumn dBColumn) {
            Type = AlterType.addcolumn;
              this.add(dBColumn);            
        }
        public void AlterTableChangeColumn(DBColumn dBColumn)
        {
            Type = AlterType.altercolumn ;
            this.add(dBColumn);
        }
        public void AlterTableDropColumn(DBColumn dBColumn)
        {
            Type = AlterType.dropcolumn;
            this.add(dBColumn);
        }
        public void AlterTableAddPrimary(DBColumn dBColumn) {
            Type = AlterType.addprimary;
            var dbc= DBColumn.Column(dBColumn.Name ,dBColumn.Type, DBColumnFlags.PrimaryKey);
              this.add(dbc);
        }
        public void AlterTableDropPrimary(DBColumn dBColumn)
        {
            Type = AlterType.dropprimary;
            var dbc = DBColumn.Column(dBColumn.Name, dBColumn.Type,DBColumnFlags.Nullable);
              this.add(dbc);
        }
        private DBUpdateTableQuery add(DBColumn dBColumn) {
            this.Columns.Add(dBColumn);
            return this;
        }        

      }

    internal class DBUpdateTableQueryRef : DBUpdateTableQuery
    {
       
        internal DBUpdateTableQueryRef(string owner, string name)
            : base(owner, name)
        {

        }
        protected override string XmlElementName
        {
            get { return XmlHelper.CreateTable; }
        }
        public override bool BuildStatement(DBStatementBuilder builder, bool isInorNot = false)
        {
            bool checknotexists = this.CheckExists == DBExistState.NotExists;
            builder.BeginCreate(DBSchemaTypes.Table, this.TableOwner, this.TableName, string.Empty, checknotexists);

        //    builder.BeginBlock(true);

            this.Columns.BuildStatement(builder, true, true);
            //if (this.HasConstraints)
            //{
            //    builder.BeginTableConstraints();
            //    this.ConstraintList.BuildStatement(builder, true, true);
            //    builder.EndTableConstraints();
            //}
            //builder.EndBlock(true);
            //builder.EndCreate(DBSchemaTypes.Table, checknotexists);
            return true;
        }


    }

}
