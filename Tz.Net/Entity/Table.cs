using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tz.Net.Entity
{
    /// <summary>
    /// 
    /// </summary>
    public interface ITable {
        string TableID { get; set; }
        string TableName { get; set; }
        string Category { get; set; }
          List<IField> Fields { get; }
    }
    /// <summary>
    /// 
    /// </summary>
  public  class Table:ITable
    {
        private string _tableid;
        private string _tablename;
        private string _category;
        private List<IField> _fields;
        /// <summary>
        /// 
        /// </summary>
        public string TableID { get => _tableid; set => _tableid=value; }
        /// <summary>
        /// 
        /// </summary>
        public string TableName { get => _tablename; set => _tablename=value; }
        /// <summary>
        /// 
        /// </summary>
        public string Category { get => _category; set => _category=value; }
        /// <summary>
        /// 
        /// </summary>
        public List<IField> Fields { get => _fields; }
        /// <summary>
        /// 
        /// </summary>
        public Table() {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableid"></param>
        public Table(string tableid) {
            _tableid = tableid;
        }   
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IField NewField() {
            var f= new Field();
            return f;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        public Table Add(IField field) {
            this.Fields.Add(field);
            return this;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        public Table Remove(IField field) {
            this.Fields.Remove(field);
            return this;
        }

    }
}
