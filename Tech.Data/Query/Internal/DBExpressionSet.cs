/*  Copyright 2009 Tech Limited
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
using System.Text;

namespace Tech.Data.Query
{

    /// <summary>
    /// Abstract class for containing specific lists of 
    /// expressions such as a select list or order list
    /// </summary>
    public abstract class DBExpressionSet : DBClause
    {
        private DBClause _last;

        #region public DBClause Last {get;set;}

        /// <summary>
        /// Gets or sets the last Clause used whilds contructing a statement
        /// </summary>
        public DBClause Last
        {
            get { return _last; }
            protected set { _last = value; }
        }

        #endregion

        #region protected override string XmlElementName

        /// <summary>
        /// overrides the base implemntation to return an empty element name
        /// </summary>
        protected override string XmlElementName
        {
            get { return XmlHelper.EmptyName; }
        }

        #endregion

    }

   
}
