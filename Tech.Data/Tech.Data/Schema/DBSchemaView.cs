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
using System.Text;
using System.Xml.Serialization;

namespace Tech.Data.Schema
{
    /// <summary>
    /// Defines the schema of a view in the database
    /// </summary>
    [XmlRoot("view", Namespace = "http://schemas.Tech.co.uk/Query/schema/")]
    public class DBSchemaView : DBSchemaItem
    {
        #region ivars

        private bool _update;
        private DBSchemaViewColumnCollection _cols;

        #endregion

        //
        // public properties
        //

        #region public bool IsUpdateable {get;set;}

        /// <summary>
        /// Gets or Sets whether this view can be modified, or is readonly
        /// </summary>
        [XmlAttribute("updatable")]
        public bool IsUpdateable
        {
            get { return this._update; }
            set { this._update = value; }
        }

        #endregion

        #region public DBSchemaViewColumnCollection Columns {get; set;}

        /// <summary>
        /// Gets the collection of columns in this view
        /// </summary>
        [XmlArray("columns")]
        [XmlArrayItem("column")]
        public DBSchemaViewColumnCollection Columns
        {
            get { return this._cols; }
            set { this._cols = value; }
        }

        #endregion


        //
        // .ctors
        //

        #region public DBSchemaView()

        /// <summary>
        /// Creates a new empty view
        /// </summary>
        public DBSchemaView()
            : this(DBSchemaTypes.View, string.Empty, string.Empty)
        {
        }

        #endregion

        #region public DBSchemaView(string owner, string name)
        /// <summary>
        /// Creates a new view with the specified name and schema owner
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="name"></param>
        public DBSchemaView(string owner, string name)
            : this(DBSchemaTypes.View, owner, name)
        {
        }
        
        #endregion

        #region protected DBSchemaView(DBSchemaTypes type, string owner, string name)
        /// <summary>
        /// Creates a new view with the specified type, owner, and name
        /// </summary>
        /// <param name="type"></param>
        /// <param name="owner"></param>
        /// <param name="name"></param>
        protected DBSchemaView(DBSchemaTypes type, string owner, string name)
            : base(name, owner, type)
        {
            _update = false;
            _cols = new DBSchemaViewColumnCollection();
        }

        #endregion

    }
}
