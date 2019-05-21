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
    /// The DBToken class is the base class for all the 
    /// SQL statement elements in the Query library
    /// </summary>
    public abstract class DBToken
    {

        
#if SILVERLIGHT
        // no statement building
#else
        //
        // abstract BuildStatement method
        //

        /// <summary>
        /// Abstract method that inheriting classes must override to build statements
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public abstract bool BuildStatement(DBStatementBuilder builder);

#endif

    }
}
