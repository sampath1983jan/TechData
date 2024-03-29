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
    /// <summary>
    /// Base class for all Drop opertations (DropTable, DropView, DropSproc)
    /// </summary>
    public abstract class DBDropQuery : DBQuery
    {
        /// <summary>
        /// Gets the flag to check if the object to be dropped 
        /// should be checked to see if it exists.
        /// </summary>
        public DBExistState CheckExists { get; protected set; }


        public DBDropQuery()
            : base()
        {
            this.CheckExists = DBExistState.Unknown;
        }


        protected override bool ReadAnAttribute(System.Xml.XmlReader reader, XmlReaderContext context)
        {
            bool b;
            if (this.IsAttributeMatch(XmlHelper.CheckExists, reader, context))
            {
                this.CheckExists = XmlHelper.ParseEnum<DBExistState>(reader.Value);
                b = true;
            }
            else
                b = base.ReadAnAttribute(reader, context);

            return b;
        }

        protected override bool WriteAllAttributes(System.Xml.XmlWriter writer, XmlWriterContext context)
        {
            if (this.CheckExists != DBExistState.Unknown)
                this.WriteAttribute(writer, XmlHelper.CheckExists, this.CheckExists.ToString(), context);

            return base.WriteAllAttributes(writer, context);
        }
    }
}
