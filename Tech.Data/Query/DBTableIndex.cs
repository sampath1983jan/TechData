﻿/*  Copyright 2009 Tech Limited
 *  This file is part of the DynaSQL library.
 *
*  DynaSQL is free software: you can redistribute it and/or modify
 *  it under the terms of the GNU Lesser General Public License as published by
 *  the Free Software Foundation, either version 3 of the License, or
 *  (at your option) any later version.
 * 
 *  DynaSQL is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU Lesser General Public License for more details.
 * 
 *  You should have received a copy of the GNU Lesser General Public License
 *  along with Query in the COPYING.txt file.  If not, see <http://www.gnu.org/licenses/>.
 * 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tech.Data.Query
{
    public abstract class DBTableIndex : DBClause
    {
        /// <summary>
        /// Gets or sets the name of this index
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets the list of columns (and their ordering) in this index
        /// </summary>
        public DBOrderList ColumnOrders { get; private set; }


        protected DBTableIndex()
            : base()
        {
            ColumnOrders = new DBOrderList();
        }

        public DBTableIndex AddColumn(string name)
        {
            return AddColumn(name, Order.Default);
        }

        public DBTableIndex AddColumn(string name, Order order)
        {
            DBOrder orderitem = DBOrder.OrderBy(order, DBField.Field(name));
            ColumnOrders.Add(orderitem);
            return this;
        }

        public static DBTableIndex Index(string name)
        {
            DBTableIndex index = new DBTableIndexRef();
            index.Name = name;
            return index;
        }
    }

    public class DBTableIndexRef : DBTableIndex
    {
        protected override string XmlElementName
        {
            get { return XmlHelper.TableIndex; ; }
        }

#if SILVERLIGHT
        // no statement building
#else
        public override bool BuildStatement(DBStatementBuilder builder, bool isInorNot = false)
        {
            builder.BeginCreate(DBSchemaTypes.Index, string.Empty, this.Name, string.Empty, false);
            builder.BeginBlock(false);
            foreach (DBOrder order in this.ColumnOrders)
            {
                builder.BeginOrderClause(order.Order);
                order.Clause.BuildStatement(builder);
                builder.EndOrderClause(order.Order);
            }
            builder.EndBlock(false);
            builder.EndCreate(DBSchemaTypes.Index, false);
            return true;
        }

#endif

        protected override bool WriteAllAttributes(System.Xml.XmlWriter writer, XmlWriterContext context)
        {
            if (!string.IsNullOrEmpty(this.Name))
                this.WriteAttribute(writer, XmlHelper.Name, this.Name, context);
            return base.WriteAllAttributes(writer, context);
        }

        protected override bool WriteInnerElements(System.Xml.XmlWriter writer, XmlWriterContext context)
        {
            if (this.ColumnOrders.Count > 0)
            {
                this.WriteStartElement(XmlHelper.ColumnList, writer, context);
                this.ColumnOrders.WriteXml(writer, context);
                this.WriteEndElement(XmlHelper.ColumnList, writer, context);
            }

            return base.WriteInnerElements(writer, context);
        }

        protected override bool ReadAnAttribute(System.Xml.XmlReader reader, XmlReaderContext context)
        {
            if (this.IsAttributeMatch(XmlHelper.Name, reader, context))
            {
                this.Name = reader.Value;
                return true;
            }
            else
                return base.ReadAnAttribute(reader, context);
        }

        protected override bool ReadAnInnerElement(System.Xml.XmlReader reader, XmlReaderContext context)
        {
            if (this.IsElementMatch(XmlHelper.ColumnList, reader, context))
            {
                this.ColumnOrders.ReadXml(XmlHelper.ColumnList, reader, context);
                return true;
            }
            return base.ReadAnInnerElement(reader, context);
        }
    }


    public class DBTableIndexList : DBTokenList<DBTableIndex>
    {
        /// <summary>
        /// Gets the characters that should prepend the list when built
        /// </summary>
        public override string ListStart { get { return "\r\n"; } }

        /// <summary>
        /// Gets the characters that should be appended to the list after being built
        /// </summary>
        public override string ListEnd { get { return ""; } }

        /// <summary>
        /// Gets the separator for each token when the statements are built
        /// </summary>
        public override string TokenSeparator { get { return "\r\n, "; } }

        public DBTableIndexList()
        {
            
        }
    }
}
