using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tech.Data.Query
{
    public abstract class DBUpdateTable : DBQuery
    {
        public DBExistState CheckExists { get; protected set; }

        

        public DBUpdateTable()
            : base()
        {
            this.CheckExists = DBExistState.Unknown;
        }        
        protected override bool ReadAnAttribute(System.Xml.XmlReader reader, XmlReaderContext context)
        {
            if (this.IsAttributeMatch(XmlHelper.CheckExists, reader, context))
            {
                this.CheckExists = XmlHelper.ParseEnum<DBExistState>(reader.Value);
                return true;
            }
            else
                return base.ReadAnAttribute(reader, context);
        }
        protected override bool WriteAllAttributes(System.Xml.XmlWriter writer, XmlWriterContext context)
        {
            if (this.CheckExists != DBExistState.Unknown)
                this.WriteAttribute(writer, XmlHelper.CheckExists, this.CheckExists.ToString(), context);

            return base.WriteAllAttributes(writer, context);
        }

    }
}
