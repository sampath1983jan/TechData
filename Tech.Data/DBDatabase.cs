/*  Copyright 2009 PerceiveIT Limited
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
using System.Data;
using System.Data.Common;
using Tech.Data.Schema;
using Tech.Data.Query;
using Tech.Data.Profile;
using Tech.Data.Configuration;

namespace Tech.Data
{
    /// <summary>
    /// The DBDatabase is the core instance for interacting with a database.
    /// Use the static Create(...) methods to instantiate, and then execute common methods against the database
    /// </summary>
    /// <remarks>Using the DBDatabase is simple.<br/>
    /// Create a new instance via the static (shared) Create(..) method. 
    /// <para>You can then call any of the ExecXXX methods with DBQuery's, SQL strings or actual commands.</para></remarks>
    public partial class DBDatabase
    {
        //
        // inner classes
        //

        #region protected enum ReThrowAction

        /// <summary>
        /// Defines the action that should be taken
        /// when an exception is caught.
        /// </summary>
        protected enum ReThrowAction
        {
            /// <summary>
            /// Simply throw the exception back up
            /// </summary>
            Original,
            /// <summary>
            /// Throw a wrapped exception
            /// </summary>
            Wrapped,
            /// <summary>
            /// This has been handled and no action needs to be taken
            /// </summary>
            None
        }

        #endregion

        //
        // events
        //

        #region public event DBExceptionHandler HandleException

        /// <summary>
        /// Raised when there is an error during the execution of a command 
        /// (and the exception is not handled by any provided method by the caller)
        /// </summary>
        public event DBExceptionHandler HandleException;

        protected virtual void OnHandleException(DBExceptionEventArgs args)
        {
            if (null != HandleException)
                this.HandleException(this, args);
        }
        #endregion

        //
        // instance variables
        //

        #region ivars

        private DbProviderFactory _factory;
        private string _connectionStr;
        private string _configname;
        private string _provname;
        private DBDatabaseProperties _properties;
        private Schema.DBSchemaProvider _schemaprov;
        private IDBProfiler _profiler;
        private bool _isprofiling;

        #endregion

        //
        // properties
        //

        #region public string Name {get; set;}

        /// <summary>
        /// Gets or sets an identifiable name for this DBDatabase instance. 
        /// </summary>
        /// <remarks>If the connection is loaded from a ConnectionStrings section in the config file
        /// then the name is initialized to this value. This can also be set to any required value.
        /// </remarks>
        public string Name
        {
            get { return (null == _configname) ? "" : this._configname; }
            set { _configname = value; }
        }

        #endregion

        #region public string ConnectionString {get;}

        /// <summary>
        /// Gets the connection string associated with this DBDatabase
        /// </summary>
        public string ConnectionString
        {
            get { return _connectionStr; }
        }

        #endregion

        #region protected DbProviderFactory Factory {get;}

        /// <summary>
        /// Gets the provider factory associated with this database
        /// </summary>
        protected DbProviderFactory Factory
        {
            get { return this._factory; }
        }

        #endregion

        #region public string ProviderName {get;}

        /// <summary>
        /// Gets the name of the provider for this database
        /// </summary>
        public string ProviderName
        {
            get { return _provname; }
        }

        #endregion

        #region public bool IsProfiling {get}

        /// <summary>
        /// Returns true if this DBDatabase is profiling (recording database command executions).
        /// </summary>
        public bool IsProfiling
        {
            get { return _isprofiling; }
        }

        #endregion

        #region public IDBProfiler Profiler {get;}

        /// <summary>
        /// Gets the IDBProfiler associated with this DBDatabase. 
        /// Use the AttachProfiler method to set this value,
        /// or the configuration file to automatically attach a profiler
        /// </summary>
        protected IDBProfiler Profiler
        {
            get { return _profiler; }
        }


        #endregion

        //
        // .ctors
        //

        #region protected DBDataBase(string connection, string providerName, DbProviderFactory provider)

        /// <summary>
        /// Protected constructor for the DBDatabase that requires valid (not null or empty parameters)
        /// </summary>
        /// <param name="connection">The valid database connection</param>
        /// <param name="providerName">The valid provider name</param>
        /// <param name="factory">The provider factory</param>
        protected internal DBDatabase(string connection, string providerName, DbProviderFactory factory)
        {
            if (null == factory)
                throw new ArgumentNullException("factory");
            if (string.IsNullOrEmpty(connection))
                throw new ArgumentNullException("connection");
            if (string.IsNullOrEmpty(providerName))
                throw new ArgumentNullException("providerName");

            this._connectionStr = connection;
            this._factory = factory;
            this._provname = providerName;
        }

        #endregion


        //
        // database properties and schema
        //

        #region public DBDatabaseProperties GetProperties()

        /// <summary>
        /// Gets the database properties for this DBDatabase
        /// </summary>
        /// <returns></returns>
        public DBDatabaseProperties GetProperties()
        {
            if (_properties == null)
                _properties = this.CreateProperties();
            return _properties;

        }

        #endregion

        #region public DBSchemaProvider GetSchemaProvider()

        /// <summary>
        /// Gets the DBSchemaProvider for this database
        /// </summary>
        /// <returns></returns>
        public DBSchemaProvider GetSchemaProvider()
        {
            if (null == this._schemaprov)
                _schemaprov = this.CreateSchemaProvider();

            return this._schemaprov;
        }

        #endregion

        //
        // Execute read methods
        //

        #region public object[] ExecuteReadEach(string sqltext, DBRecordCallback populator)

        /// <summary>
        /// Executes the specified SQL text as a command against the this DBDatabase connection, 
        /// and invokes the callback method with the IDataRecord FOR EACH result from the command
        /// </summary>
        /// <param name="sqltext">The SQL statement to execute</param>
        /// <param name="callback">A delegate method that accepts the IDataRecord as a parameter and returns an object</param>
        /// <param name="onerror">If an error is raised during the execution of the query the onerror method will be invoked</param>
        /// <returns>The array of objects returned from each of your DBRecordCallback delegate method invocations</returns>
        public object[] ExecuteReadEach(string sqltext, DBRecordCallback populator)
        {
            return ExecuteReaderEach(sqltext, populator, null);
        }

        /// <summary>
        /// Executes the specified SQL text as a command against the this DBDatabase connection, 
        /// and invokes the callback method with the IDataRecord FOR EACH result from the command
        /// </summary>
        /// <param name="sqltext">The SQL statement to execute</param>
        /// <param name="callback">A delegate method that accepts the IDataRecord as a parameter and returns an object</param>
        /// <param name="onerror">If an error is raised during the execution of the query the onerror method will be invoked</param>
        /// <returns>The array of objects returned from each of your DBRecordCallback delegate method invocations</returns>
        public object[] ExecuteReaderEach(string sqltext, DBRecordCallback populator, DBOnErrorCallback onerror)
        {
            return (object[])ExecuteReader(sqltext, delegate (DbDataReader reader)
            {
                List<object> all = new List<object>();
                while (reader.Read())
                    all.Add(populator(reader));
                return all.ToArray();

            }, onerror);
        }

        #endregion

        #region public object ExecuteReadOne(string sqltext, DBRecordCallback populator)

        /// <summary>
        /// Executes the specified SQL text as a command against the this DBDatabase connection, 
        /// and invokes the callback method with the IDataRecord for the FIRST result from the command
        /// </summary>
        /// <param name="sqltext">The SQL statement to execute</param>
        /// <param name="callback">A delegate method that accepts the IDataRecord as a parameter and returns an object</param>
        /// <returns>The object returned from your DBRecordCallback delegate method</returns>
        public object ExecuteReadOne(string sqltext, DBRecordCallback populator)
        {
            return ExecuteReaderOne(sqltext, populator, null);
        }

        /// <summary>
        /// Executes the specified SQL text as a command against the this DBDatabase connection, 
        /// and invokes the callback method with the IDataRecord for the FIRST result from the command
        /// </summary>
        /// <param name="sqltext">The SQL statement to execute</param>
        /// <param name="callback">A delegate method that accepts the IDataRecord as a parameter and returns an object</param>
        /// <param name="onerror">If an error is raised during the execution of the query the onerror method will be invoked</param>
        /// <returns>The object returned from your DBRecordCallback delegate method</returns>
        public object ExecuteReaderOne(string sqltext, DBRecordCallback populator, DBOnErrorCallback onerror)
        {
            return ExecuteReader(sqltext, delegate (DbDataReader reader)
            {
                if (reader.Read())
                    return populator(reader);
                else
                    return null;
            }, onerror);
        }

        #endregion

        #region public object ExecuteRead(string sqltext, DBCallback callback)

        /// <summary>
        /// Executes the specified SQL text as a command against the this DBDatabase connection, 
        /// and invokes the callback method with the DbDataReader from the command
        /// </summary>
        /// <param name="sqltext">The SQL statement to execute</param>
        /// <param name="callback">A delegate method that accepts the DbDataReader as a parameter and returns an object</param>
        /// <returns>The object returned from your DBCallback delegate method</returns>
        public object ExecuteRead(string sqltext, DBCallback callback)
        {
            return ExecuteReader(sqltext, callback, null);
        }

        /// <summary>
        /// Executes the specified SQL text as a command against the this DBDatabase connection, 
        /// and invokes the callback method with the DbDataReader from the command
        /// </summary>
        /// <param name="sqltext">The SQL statement to execute</param>
        /// <param name="callback">A delegate method that accepts the DbDataReader as a parameter and returns an object</param>
        /// <param name="onerror">If an error is raised during the execution of the query the onerror method will be invoked</param>
        /// <returns>The object returned from your DBCallback delegate method</returns>
        public object ExecuteReader(string sqltext, DBCallback callback, DBOnErrorCallback onerror)
        {
            if (string.IsNullOrEmpty(sqltext))
                throw new ArgumentNullException("sqltext");

            if (null == callback)
                throw new ArgumentNullException("callback");

            object returns;

            using (DbCommand cmd = this.CreateCommand(sqltext))
            {
                returns = this.ExecuteReader(cmd, callback, onerror);
            }

            return returns;
        }

        #endregion



        #region public object[] ExecuteReadEach(string text, CommandType type, DBRecordCallback populator)

        /// <summary>
        /// Executes the specified SQL text as a command (of the specified CommandType) against the this DBDatabase connection, 
        /// and invokes the callback method with the IDataReacord FOR EACH result from the command
        /// </summary>
        /// <param name="text">The SQL Statement to execute</param>
        /// <param name="type">The type of command the SQL Statement corresponds to</param>
        /// <param name="callback">A delegate method that accepts the IDataRecord as a parameter and returns an object</param>
        /// <returns>The array of objects returned from your DBRecordCallback method</returns>
        public object[] ExecuteReadEach(string text, CommandType type, DBRecordCallback populator)
        {
            return ExecuteReaderEach(text, type, populator, null);
        }

        /// <summary>
        /// Executes the specified SQL text as a command (of the specified CommandType) against the this DBDatabase connection, 
        /// and invokes the callback method with the IDataReacord FOR EACH result from the command
        /// </summary>
        /// <param name="text">The SQL Statement to execute</param>
        /// <param name="type">The type of command the SQL Statement corresponds to</param>
        /// <param name="callback">A delegate method that accepts the IDataRecord as a parameter and returns an object</param>
        /// <param name="onerror">If an error is raised during the execution of the query the onerror method will be invoked</param>
        /// <returns>The array of objects returned from your DBRecordCallback method</returns>
        public object[] ExecuteReaderEach(string text, CommandType type, DBRecordCallback populator, DBOnErrorCallback onerror)
        {
            return (object[])ExecuteReader(text, type, delegate (DbDataReader reader)
            {
                List<object> all = new List<object>();
                while (reader.Read())
                    all.Add(populator(reader));

                return all.ToArray();

            }, onerror);
        }

        #endregion

        #region public object ExecuteReadOne(string text, CommandType type, DBRecordCallback populator)

        /// <summary>
        /// Executes the specified SQL text as a command (of the specified CommandType) against the this DBDatabase connection, 
        /// and invokes the callback method with the IDataReacord for the FIRST result from the command
        /// </summary>
        /// <param name="text">The SQL Statement to execute</param>
        /// <param name="type">The type of command the SQL Statement corresponds to</param>
        /// <param name="callback">A delegate method that accepts the IDataRecord as a parameter and returns an object</param>
        /// <returns>The object returned from your DBRecordCallback method</returns>
        public object ExecuteReadOne(string text, CommandType type, DBRecordCallback populator)
        {
            return ExecuteReaderOne(text, type, populator, null);
        }

        /// <summary>
        /// Executes the specified SQL text as a command (of the specified CommandType) against the this DBDatabase connection, 
        /// and invokes the callback method with the IDataReacord for the FIRST result from the command
        /// </summary>
        /// <param name="text">The SQL Statement to execute</param>
        /// <param name="type">The type of command the SQL Statement corresponds to</param>
        /// <param name="callback">A delegate method that accepts the IDataRecord as a parameter and returns an object</param>
        /// <param name="onerror">If an error is raised during the execution of the query the onerror method will be invoked</param>
        /// <returns>The object returned from your DBRecordCallback method</returns>
        public object ExecuteReaderOne(string text, CommandType type, DBRecordCallback populator, DBOnErrorCallback onerror)
        {
            return ExecuteReader(text, type, delegate (DbDataReader reader)
            {
                if (reader.Read())
                    return populator(reader);
                else
                    return null;
            }, onerror);
        }

        #endregion

        #region public object ExecuteRead(string text, CommandType type, DBCallback callback)

        /// <summary>
        /// Executes the specified SQL text as a command (of the specified CommandType) against the this DBDatabase connection, 
        /// and invokes the callback method with the DbDataReader from the command
        /// </summary>
        /// <param name="text">The SQL Statement to execute</param>
        /// <param name="type">The type of command the SQL Statement corresponds to</param>
        /// <param name="callback">A delegate method that accepts the DbDataReader as a parameter and returns an object</param>
        /// <returns>The object returned from your DBCallback method</returns>
        public object ExecuteRead(string text, CommandType type, DBCallback callback)
        {
            return ExecuteReader(text, type, callback, null);
        }

        /// <summary>
        /// Executes the specified SQL text as a command (of the specified CommandType) against the this DBDatabase connection, 
        /// and invokes the callback method with the DbDataReader from the command
        /// </summary>
        /// <param name="text">The SQL Statement to execute</param>
        /// <param name="type">The type of command the SQL Statement corresponds to</param>
        /// <param name="callback">A delegate method that accepts the DbDataReader as a parameter and returns an object</param>
        /// <param name="onerror">If an error is raised during the execution of the query the onerror method will be invoked</param>
        /// <returns>The object returned from your DBCallback method</returns>
        public object ExecuteReader(string text, CommandType type, DBCallback callback, DBOnErrorCallback onerror)
        {
            if (string.IsNullOrEmpty(text))
                throw new ArgumentNullException("text");
            if (null == callback)
                throw new ArgumentNullException("callback");

            object returns;

            using (DbCommand cmd = this.CreateCommand(text, type))
            {
                returns = this.DoExecuteRead(cmd, callback, onerror);
            }

            return returns;
        }

        #endregion



        #region public object[] ExecuteReadEach(DBQuery query, DBRecordCallback populator)

        /// <summary>
        /// Generates the DBQuery as a command to execute against the this DBDatabase connection, 
        /// and invokes the callback method with the IDataRecord FOR EACH result from the command
        /// </summary>
        /// <param name="query">The DBQuery to build the SQL statement from and execute</param>
        /// <param name="populator">A delegate method that accepts the IDataRecord as a parameter and returns and array of EACH object</param>
        /// <returns>The object array returned from each call to your DBRecordCallback delegate method</returns>
        public object[] ExecuteReadEach(DBQuery query, DBRecordCallback populator)
        {
            return this.ExecuteReaderEach(query, populator, null);
        }

        /// <summary>
        /// Generates the DBQuery as a command to execute against the this DBDatabase connection, 
        /// and invokes the callback method with the IDataRecord FOR EACH result from the command
        /// </summary>
        /// <param name="query">The DBQuery to build the SQL statement from and execute</param>
        /// <param name="populator">A delegate method that accepts the IDataRecord as a parameter and returns and array of EACH object</param>
        /// <param name="onerror">If an error is raised during the execution of the query the onerror method will be invoked</param>
        /// <returns>The object array returned from each call to your DBRecordCallback delegate method</returns>
        public object[] ExecuteReaderEach(DBQuery query, DBRecordCallback populator, DBOnErrorCallback onerror)
        {
            return (object[])ExecuteReader(query, delegate (DbDataReader reader)
            {
                List<object> all = new List<object>();
                while (reader.Read())
                    all.Add(populator(reader));
                return all.ToArray();
            }, onerror);
        }

        #endregion

        #region public object ExecuteReadOne(DBQuery query, DBRecordCallback populator)

        /// <summary>
        /// Generates the DBQuery as a command to execute against the this DBDatabase connection, 
        /// and invokes the callback method with the IDataRecord for the FIRST result from the command
        /// </summary>
        /// <param name="query">The DBQuery to build the SQL statement from and execute</param>
        /// <param name="populator">A delegate method that accepts the IDataRecord as a parameter and returns an object</param>
        /// <returns>The object returned from your DBCallback delegate method</returns>
        public object ExecuteReadOne(DBQuery query, DBRecordCallback populator)
        {
            return ExecuteReaderOne(query, populator, null);
        }

        /// <summary>
        /// Generates the DBQuery as a command to execute against the this DBDatabase connection, 
        /// and invokes the callback method with the IDataRecord for the FIRST result from the command
        /// </summary>
        /// <param name="query">The DBQuery to build the SQL statement from and execute</param>
        /// <param name="populator">A delegate method that accepts the IDataRecord as a parameter and returns an object</param>
        /// <param name="onerror">If an error is raised during the execution of the query the onerror method will be invoked</param>
        /// <returns>The object returned from your DBCallback delegate method</returns>
        public object ExecuteReaderOne(DBQuery query, DBRecordCallback populator, DBOnErrorCallback onerror)
        {
            return ExecuteReader(query, delegate (DbDataReader reader)
            {
                if (reader.Read())
                    return populator(reader);
                else
                    return null;
            }, onerror);
        }

        #endregion

        #region public object ExecuteRead(DBQuery query, DBCallback populator)

        /// <summary>
        /// Generates the DBQuery as a command to execute against the this DBDatabase connection, 
        /// and invokes the callback method with the DbDataReader from the command
        /// </summary>
        /// <param name="query">The DBQuery to build the SQL statement from and execute</param>
        /// <param name="populator">A delegate method that accepts the DbDataReader as a parameter and returns an object</param>
        /// <returns>The object returned from your DBCallback delegate method</returns>
        public object ExecuteRead(DBQuery query, DBCallback populator)
        {
            return ExecuteReader(query, populator, null);
        }

        /// <summary>
        /// Generates the DBQuery as a command to execute against the this DBDatabase connection, 
        /// and invokes the callback method with the DbDataReader from the command
        /// </summary>
        /// <param name="query">The DBQuery to build the SQL statement from and execute</param>
        /// <param name="populator">A delegate method that accepts the DbDataReader as a parameter and returns an object</param>
        /// <param name="onerror">If an error is raised during the execution of the query the onerror method will be invoked</param>
        /// <returns>The object returned from your DBCallback delegate method</returns>
        public object ExecuteReader(DBQuery query, DBCallback populator, DBOnErrorCallback onerror)
        {
            if (null == query)
                throw new ArgumentNullException("query");
            if (null == populator)
                throw new ArgumentNullException("populator");

            object returns;

            using (DbCommand cmd = this.CreateCommand(query))
            {
                returns = this.DoExecuteRead(cmd, populator, onerror);
            }

            return returns;
        }

        #endregion



        #region public object[] ExecuteReadEach(DbTransaction transaction, DBQuery query, DBRecordCallback populator)

        /// <summary>
        /// Generates the DBQuery as a command to execute against the this DBDatabase using the provided transaction, 
        /// and invokes the callback method with the IDataRecord FOR EACH result from the command
        /// </summary>
        /// <param name="transaction">A currently open and active transaction</param>
        /// <param name="query">The DBQuery to build the SQL statement from and execute</param>
        /// <param name="populator">A delegate method that accepts the IDataRecord as a parameter and returns an object</param>
        /// <returns>The object array of results returned from your DBRecordCallback delegate method</returns>
        public object[] ExecuteReadEach(DbTransaction transaction, DBQuery query, DBRecordCallback populator)
        {
            return ExecuteReaderEach(transaction, query, populator, null);
        }

        /// <summary>
        /// Generates the DBQuery as a command to execute against the this DBDatabase using the provided transaction, 
        /// and invokes the callback method with the IDataRecord FOR EACH result from the command
        /// </summary>
        /// <param name="transaction">A currently open and active transaction</param>
        /// <param name="query">The DBQuery to build the SQL statement from and execute</param>
        /// <param name="populator">A delegate method that accepts the IDataRecord as a parameter and returns an object</param>
        /// <param name="onerror">If an error is raised during the execution of the query the onerror method will be invoked</param>
        /// <returns>The object array of results returned from your DBRecordCallback delegate method</returns>
        public object[] ExecuteReaderEach(DbTransaction transaction, DBQuery query, DBRecordCallback populator, DBOnErrorCallback onerror)
        {
            return (object[])ExecuteReader(transaction, query, delegate (DbDataReader reader)
            {
                List<object> all = new List<object>();
                while (reader.Read())
                    all.Add(populator(reader));
                return all.ToArray();
            }, onerror);
        }

        #endregion

        #region public object ExecuteReadOne(DbTransaction transaction, DBQuery query, DBRecordCallback populator)

        /// <summary>
        /// Generates the DBQuery as a command to execute against the this DBDatabase using the provided transaction, 
        /// and invokes the callback method with the IDataRecord of the FIRST result from the command
        /// </summary>
        /// <param name="transaction">A currently open and active transaction</param>
        /// <param name="query">The DBQuery to build the SQL statement from and execute</param>
        /// <param name="populator">A delegate method that accepts the IDataRecord as a parameter and returns an object</param>
        /// <returns>The object returned from your DBCallback delegate method</returns>
        public object ExecuteReadOne(DbTransaction transaction, DBQuery query, DBRecordCallback populator)
        {
            return ExecuteReaderOne(transaction, query, populator, null);
        }

        /// <summary>
        /// Generates the DBQuery as a command to execute against the this DBDatabase using the provided transaction, 
        /// and invokes the callback method with the IDataRecord of the FIRST result from the command
        /// </summary>
        /// <param name="transaction">A currently open and active transaction</param>
        /// <param name="query">The DBQuery to build the SQL statement from and execute</param>
        /// <param name="populator">A delegate method that accepts the IDataRecord as a parameter and returns an object</param>
        /// <param name="onerror">If an error is raised during the execution of the query the onerror method will be invoked</param>
        /// <returns>The object returned from your DBCallback delegate method</returns>
        public object ExecuteReaderOne(DbTransaction transaction, DBQuery query, DBRecordCallback populator, DBOnErrorCallback onerror)
        {
            return ExecuteReader(transaction, query, delegate (DbDataReader reader)
            {
                if (reader.Read())
                    return populator(reader);
                else
                    return null;

            }, onerror);

        }

        #endregion

        #region public object ExecuteRead(DbTransaction transaction, DBQuery query, DBCallback populator)

        /// <summary>
        /// Generates the DBQuery as a command to execute against the this DBDatabase using the provided transaction, 
        /// and invokes the callback method with the DbDataReader from the command
        /// </summary>
        /// <param name="transaction">A currently open and active transaction</param>
        /// <param name="query">The DBQuery to build the SQL statement from and execute</param>
        /// <param name="populator">A delegate method that accepts the DbDataReader as a parameter and returns an object</param>
        /// <returns>The object returned from your DBCallback delegate method</returns>
        public object ExecuteRead(DbTransaction transaction, DBQuery query, DBCallback populator)
        {
            return ExecuteReader(transaction, query, populator, null);
        }

        /// <summary>
        /// Generates the DBQuery as a command to execute against the this DBDatabase using the provided transaction, 
        /// and invokes the callback method with the DbDataReader from the command
        /// </summary>
        /// <param name="transaction">A currently open and active transaction</param>
        /// <param name="query">The DBQuery to build the SQL statement from and execute</param>
        /// <param name="populator">A delegate method that accepts the DbDataReader as a parameter and returns an object</param>
        /// <param name="onerror">If an error is raised during the execution of the query the onerror method will be invoked</param>
        /// <returns>The object returned from your DBCallback delegate method</returns>
        public object ExecuteReader(DbTransaction transaction, DBQuery query, DBCallback populator, DBOnErrorCallback onerror)
        {
            if (null == transaction)
                throw new ArgumentNullException("transaction");
            if (null == transaction.Connection)
                throw new ArgumentNullException("transaction.Connection");
            if (null == query)
                throw new ArgumentNullException("query");
            if (null == populator)
                throw new ArgumentNullException("populator");

            object returns;


            using (DbCommand cmd = this.CreateCommand(transaction, query))
            {
                returns = this.DoExecuteRead(cmd, populator, onerror);
            }

            return returns;
        }

        #endregion



        #region public object[] ExecuteReadEach(DbCommand cmd, DBRecordCallback populator)

        /// <summary>
        /// Executes the specified Command against it's connection (opening as required), 
        /// and invokes the callback method with the IDataRecord FOR EACH result from the command
        /// </summary>
        /// <param name="cmd">The initialiazed DbCommand with a non null DbConnection (if the connection is not open it will be opened and closed within the method)</param>
        /// <param name="callback">A delegate method that accepts the IDataRecord as a parameter and returns an object</param>
        /// <returns>The object arrary returned from each of your DBCallback delegate method calls</returns>
        public object[] ExecuteReadEach(DbCommand cmd, DBRecordCallback populator)
        {
            return ExecuteReaderEach(cmd, populator, null);
        }

        /// <summary>
        /// Executes the specified Command against it's connection (opening as required), 
        /// and invokes the callback method with the IDataRecord FOR EACH result from the command
        /// </summary>
        /// <param name="cmd">The initialiazed DbCommand with a non null DbConnection (if the connection is not open it will be opened and closed within the method)</param>
        /// <param name="callback">A delegate method that accepts the IDataRecord as a parameter and returns an object</param>
        /// <param name="onerror">If an error is raised during the execution of the query the onerror method will be invoked</param>
        /// <returns>The object arrary returned from each of your DBCallback delegate method calls</returns>
        public object[] ExecuteReaderEach(DbCommand cmd, DBRecordCallback populator, DBOnErrorCallback onerror)
        {
            return (object[])ExecuteReader(cmd, delegate (DbDataReader reader)
            {
                List<object> all = new List<object>();

                while (reader.Read())
                {
                    all.Add(populator(reader));
                }
                return all.ToArray();
            }, onerror);
        }

        #endregion

        #region public object ExecuteReadOne(DbCommand cmd, DBRecordCallback populator)

        /// <summary>
        /// Executes the specified Command against it's connection (opening as required), 
        /// and invokes the callback method with the IDataRecord for the FIRST result from the command
        /// </summary>
        /// <param name="cmd">The initialiazed DbCommand with a non null DbConnection (if the connection is not open it will be opened and closed within the method)</param>
        /// <param name="callback">A delegate method that accepts the IDataRecord as a parameter and returns an object</param>
        /// <returns>The object returned from your DBCallback delegate method</returns>
        public object ExecuteReadOne(DbCommand cmd, DBRecordCallback populator)
        {
            return ExecuteReaderOne(cmd, populator, null);
        }

        /// <summary>
        /// Executes the specified Command against it's connection (opening as required), 
        /// and invokes the callback method with the IDataRecord for the FIRST result from the command
        /// </summary>
        /// <param name="cmd">The initialiazed DbCommand with a non null DbConnection (if the connection is not open it will be opened and closed within the method)</param>
        /// <param name="callback">A delegate method that accepts the IDataRecord as a parameter and returns an object</param>
        /// <param name="onerror">If an error is raised during the execution of the query the onerror method will be invoked</param>
        /// <returns>The object returned from your DBCallback delegate method</returns>
        public object ExecuteReaderOne(DbCommand cmd, DBRecordCallback populator, DBOnErrorCallback onerror)
        {
            return ExecuteReader(cmd, delegate (DbDataReader reader)
            {
                if (reader.Read())
                    return populator(reader);
                else
                    return null;

            }, onerror);
        }

        #endregion

        #region public object ExecuteRead(DbCommand cmd, DBCallback populator)

        /// <summary>
        /// Executes the specified Command against it's connection (opening as required), 
        /// and invokes the callback method with the DbDataReader from the command
        /// </summary>
        /// <param name="cmd">The initialiazed DbCommand with a non null DbConnection (if the connection is not open it will be opened and closed within the method)</param>
        /// <param name="callback">A delegate method that accepts the DbDataReader as a parameter and returns an object</param>
        /// <returns>The object returned from your DBCallback delegate method</returns>
        public object ExecuteRead(DbCommand cmd, DBCallback callback)
        {
            return this.ExecuteReader(cmd, callback, null);
        }

        /// <summary>
        /// Executes the specified Command against it's connection (opening as required), 
        /// and invokes the callback method with the DbDataReader from the command
        /// </summary>
        /// <param name="cmd">The initialiazed DbCommand with a non null DbConnection (if the connection is not open it will be opened and closed within the method)</param>
        /// <param name="callback">A delegate method that accepts the DbDataReader as a parameter and returns an object</param>
        /// <param name="onerror">If an error is raised during the execution of the query the onerror method will be invoked</param>
        /// <returns>The object returned from your DBCallback delegate method</returns>
        public object ExecuteReader(DbCommand cmd, DBCallback callback, DBOnErrorCallback onerror)
        {
            if (cmd == null)
                throw new ArgumentNullException("cmd");

            if (cmd.Connection == null)
                throw new ArgumentNullException("cmd.Connection");

            if (callback == null)
                throw new ArgumentNullException("callback");

            object returns = DoExecuteRead(cmd, callback, onerror);

            return returns;
        }



        #endregion


        #region protected virtual object DoExecuteRead(DbCommand cmd, DBCallback callback)


        /// <summary>
        /// Virtual (overridable) execute read method accepting a DBCallback. All ExecuteRead methods that 
        /// use the default callback ultimately call this method
        /// </summary>
        /// <param name="cmd">The DbCommand to execute</param>
        /// <param name="callback">The delegate method to perform the actual reading</param>
        /// <returns>Any object returned from the DBCallback will by returned from this method (can be null)</returns>
        /// <remarks>If the commands' connection is closed it will be opened. Any reader will be disposed, and profiling will be undertaken if started.<br/>
        /// It is assumed all parameters are not null, and is up to the calling methods to check this beforehand.</remarks>
        protected virtual object DoExecuteRead(DbCommand cmd, DBCallback callback, DBOnErrorCallback onerror)
        {
            object returns = null;
            DbDataReader reader = null;
            bool opened = false;
            Profile.IDBProfileExecData exec = null;

            try
            {
                if (cmd.Connection.State == ConnectionState.Closed)
                {
                    cmd.Connection.Open();
                    opened = true;
                }

                if (IsProfiling)
                    exec = ProfileBegin(cmd);

                reader = cmd.ExecuteReader();
                returns = callback(reader);

            }
            catch (Exception ex)
            {
                ReThrowAction action;
                this.HandleExecutionError(onerror, ref ex, out action);

                if (action == ReThrowAction.Original)
                    throw;
                else if (action == ReThrowAction.Wrapped)
                    throw ex;
                else if (action == ReThrowAction.None)
                    ; //consume it.
                else
                    throw new ArgumentOutOfRangeException("Unknown action to perform", ex);

            }
            finally
            {
                if (null != reader)
                    reader.Dispose();

                if (opened)
                    cmd.Connection.Close();

                if (IsProfiling)
                    ProfileEnd(exec);
            }

            return returns;
        }

        #endregion

        // read with context callback

        #region public object[] ExecuteReadEach(string text, object context, DBContextRecordCallback populator)

        /// <summary>
        /// Executes the specified SQL text as a command against the this DBDatabase connection, 
        /// and invokes the callback method with the IDataReader for the FIRST result from the command and the context provided in the 
        /// call to this method
        /// </summary>
        /// <param name="text">The SQL text to execute</param>
        /// <param name="context">Any context to be passed to the callback method</param>
        /// <param name="populator">A delegate method that accepts the IDataRecord for the FIRST result and the context object as parameters and returns an object</param>
        /// <returns>The collected object array returned from your DBContextReceodCallback delegate method</returns>
        /// <remarks>The DBContextCallback has a method signature that not only accepts a DBDataReader, 
        /// but also any argument passed to this method as the context parameter</remarks>
        public object[] ExecuteReadEach(string text, object context, DBContextRecordCallback populator)
        {
            return ExecuteReadEach(text, context, populator, null);
        }

        /// <summary>
        /// Executes the specified SQL text as a command against the this DBDatabase connection, 
        /// and invokes the callback method with the IDataReader FOR EACH result from the command and the context provided in the 
        /// call to this method
        /// </summary>
        /// <param name="text">The SQL text to execute</param>
        /// <param name="context">Any context to be passed to the callback method</param>
        /// <param name="populator">A delegate method that accepts the IDataRecord FOR EACH result and the context object as parameters and returns an object</param>
        /// <param name="onerror">If an error is raised during the execution of the query the onerror method will be invoked</param>
        /// <returns>The collected object array returned from your DBContextRecordCallback delegate method</returns>
        /// <remarks>The DBContextCallback has a method signature that not only accepts a DBDataReader, 
        /// but also any argument passed to this method as the context parameter</remarks>
        public object[] ExecuteReadEach(string text, object context, DBContextRecordCallback populator, DBOnErrorCallback onerror)
        {

            return (object[])ExecuteRead(text, context, delegate (DbDataReader reader, object innercontext)
            {
                List<object> all = new List<object>();
                while (reader.Read())
                {
                    all.Add(populator(reader, innercontext));
                }

                return all.ToArray();
            }, onerror);
        }

        #endregion

        #region public object ExecuteReadOne(string text, object context, DBContextRecordCallback populator)

        /// <summary>
        /// Executes the specified SQL text as a command against the this DBDatabase connection, 
        /// and invokes the callback method with the IDataReader for the FIRST result from the command and the context provided in the 
        /// call to this method
        /// </summary>
        /// <param name="text">The SQL text to execute</param>
        /// <param name="context">Any context to be passed to the callback method</param>
        /// <param name="populator">A delegate method that accepts the IDataRecord for the FIRST result and the context object as parameters and returns an object</param>
        /// <returns>The object returned from your DBContextCallback delegate method</returns>
        /// <remarks>The DBContextCallback has a method signature that not only accepts a DBDataReader, 
        /// but also any argument passed to this method as the context parameter</remarks>
        public object ExecuteReadOne(string text, object context, DBContextRecordCallback populator)
        {
            return ExecuteReadOne(text, context, populator, null);
        }

        /// <summary>
        /// Executes the specified SQL text as a command against the this DBDatabase connection, 
        /// and invokes the callback method with the IDataReader for the FIRST result from the command and the context provided in the 
        /// call to this method
        /// </summary>
        /// <param name="text">The SQL text to execute</param>
        /// <param name="context">Any context to be passed to the callback method</param>
        /// <param name="populator">A delegate method that accepts the IDataRecord for the FIRST result and the context object as parameters and returns an object</param>
        /// <param name="onerror">If an error is raised during the execution of the query the onerror method will be invoked</param>
        /// <returns>The object returned from your DBContextCallback delegate method</returns>
        /// <remarks>The DBContextCallback has a method signature that not only accepts a DBDataReader, 
        /// but also any argument passed to this method as the context parameter</remarks>
        public object ExecuteReadOne(string text, object context, DBContextRecordCallback populator, DBOnErrorCallback onerror)
        {

            return ExecuteRead(text, context, delegate (DbDataReader reader, object innercontext)
            {
                if (reader.Read())
                {
                    return populator(reader, innercontext);
                }
                else
                    return null;
            }, onerror);
        }

        #endregion

        #region public object ExecuteRead(string text, object context, DBContextCallback populator)

        /// <summary>
        /// Executes the specified SQL text as a command against the this DBDatabase connection, 
        /// and invokes the callback method with the DbDataReader from the command and the context provided in the 
        /// call to this method
        /// </summary>
        /// <param name="text">The SQL text to execute</param>
        /// <param name="context">Any context to be passed to the callback method</param>
        /// <param name="populator">A delegate method that accepts the DbDataReader and the context object as parameters and returns an object</param>
        /// <returns>The object returned from your DBContextCallback delegate method</returns>
        /// <remarks>The DBContextCallback has a method signature that not only accepts a DBDataReader, 
        /// but also any argument passed to this method as the context parameter</remarks>
        public object ExecuteRead(string text, object context, DBContextCallback populator)
        {
            return ExecuteRead(text, context, populator, null);
        }

        /// <summary>
        /// Executes the specified SQL text as a command against the this DBDatabase connection, 
        /// and invokes the callback method with the DbDataReader from the command and the context provided in the 
        /// call to this method
        /// </summary>
        /// <param name="text">The SQL text to execute</param>
        /// <param name="context">Any context to be passed to the callback method</param>
        /// <param name="populator">A delegate method that accepts the DbDataReader and the context object as parameters and returns an object</param>
        /// <param name="onerror">If an error is raised during the execution of the query the onerror method will be invoked</param>
        /// <returns>The object returned from your DBContextCallback delegate method</returns>
        /// <remarks>The DBContextCallback has a method signature that not only accepts a DBDataReader, 
        /// but also any argument passed to this method as the context parameter</remarks>
        public object ExecuteRead(string text, object context, DBContextCallback populator, DBOnErrorCallback onerror)
        {
            if (string.IsNullOrEmpty(text))
                throw new ArgumentNullException("text");

            if (null == populator)
                throw new ArgumentNullException("populator");

            object returns;

            using (DbCommand cmd = this.CreateCommand(text))
            {
                returns = this.DoExecuteContextRead(cmd, context, populator, onerror);
            }

            return returns;
        }

        #endregion



        #region public object[] ExecuteReadEach(string text, CommandType type, object context, DBContextRecordCallback populator)

        /// <summary>
        /// Executes the specified SQL text as a command (of the specified CommandType) against this DBDatabase connection, 
        /// and invokes the callback method with the IDataRecord FOR EACH result of the command and the context provided in the
        /// call to this method
        /// </summary>
        /// <param name="context">An object to be passed to the callback method</param>
        /// <param name="text">The SQL Statement to execute</param>
        /// <param name="type">The type of command the SQL Statement corresponds to</param>
        /// <param name="populator">A delegate method that accepts the IDataRecord FOR EACH result and the object context as parameters and returns an object</param>
        /// <returns>The object returned from your DBContextCallback method</returns>
        /// <remarks>The DBContextCallback has a method signature that not only accepts a IDataRecord, 
        /// but also any argument passed to this method as the context parameter</remarks>
        public object[] ExecuteReadEach(string text, CommandType type, object context, DBContextRecordCallback populator)
        {
            return ExecuteReadEach(text, type, context, populator, null);
        }

        /// <summary>
        /// Executes the specified SQL text as a command (of the specified CommandType) against this DBDatabase connection, 
        /// and invokes the callback method with the IDataRecord FOR EACH result of the command and the context provided in the
        /// call to this method
        /// </summary>
        /// <param name="context">An object to be passed to the callback method</param>
        /// <param name="text">The SQL Statement to execute</param>
        /// <param name="type">The type of command the SQL Statement corresponds to</param>
        /// <param name="populator">A delegate method that accepts the IDataRecord FOR EACH result and the object context as parameters and returns an object</param>
        /// <param name="onerror">If an error is raised during the execution of the query the onerror method will be invoked</param>
        /// <returns>The object returned from your DBContextCallback method</returns>
        /// <remarks>The DBContextCallback has a method signature that not only accepts a IDataRecord, 
        /// but also any argument passed to this method as the context parameter</remarks>
        public object[] ExecuteReadEach(string text, CommandType type, object context, DBContextRecordCallback populator, DBOnErrorCallback onerror)
        {
            return (object[])ExecuteRead(text, type, context, delegate (DbDataReader reader, object innercontext)
            {
                List<object> all = new List<object>();
                while (reader.Read())
                    all.Add(populator(reader, innercontext));
                return all.ToArray();
            }, onerror);
        }

        #endregion

        #region public object ExecuteReadOne(string text, CommandType type, object context, DBContextRecordCallback populator)

        /// <summary>
        /// Executes the specified SQL text as a command (of the specified CommandType) against this DBDatabase connection, 
        /// and invokes the callback method with the IDataRecord from the FIRST result of the command and the context provided in the
        /// call to this method
        /// </summary>
        /// <param name="context">An object to be passed to the callback method</param>
        /// <param name="text">The SQL Statement to execute</param>
        /// <param name="type">The type of command the SQL Statement corresponds to</param>
        /// <param name="populator">A delegate method that accepts the IDataRecord for the FIRST result and the object context as parameters and returns an object</param>
        /// <returns>The object returned from your DBContextCallback method</returns>
        /// <remarks>The DBContextCallback has a method signature that not only accepts a IDataRecord, 
        /// but also any argument passed to this method as the context parameter</remarks>
        public object ExecuteReadOne(string text, CommandType type, object context, DBContextRecordCallback populator)
        {
            return ExecuteReadOne(text, type, context, populator, null);
        }

        /// <summary>
        /// Executes the specified SQL text as a command (of the specified CommandType) against this DBDatabase connection, 
        /// and invokes the callback method with the IDataRecord from the FIRST result of the command and the context provided in the
        /// call to this method
        /// </summary>
        /// <param name="context">An object to be passed to the callback method</param>
        /// <param name="text">The SQL Statement to execute</param>
        /// <param name="type">The type of command the SQL Statement corresponds to</param>
        /// <param name="populator">A delegate method that accepts the IDataRecord for the FIRST result and the object context as parameters and returns an object</param>
        /// <param name="onerror">If an error is raised during the execution of the query the onerror method will be invoked</param>
        /// <returns>The object returned from your DBContextCallback method</returns>
        /// <remarks>The DBContextCallback has a method signature that not only accepts a IDataRecord, 
        /// but also any argument passed to this method as the context parameter</remarks>
        public object ExecuteReadOne(string text, CommandType type, object context, DBContextRecordCallback populator, DBOnErrorCallback onerror)
        {
            return this.ExecuteRead(text, type, context, delegate (DbDataReader reader, object innercontext)
            {
                if (reader.Read())
                    return populator(reader, context);
                else
                    return null;
            }, onerror);
        }

        #endregion

        #region public object ExecuteRead(string text, CommandType type, object context, DBContextCallback populator)

        /// <summary>
        /// Executes the specified SQL text as a command (of the specified CommandType) against this DBDatabase connection, 
        /// and invokes the callback method with the DbDataReader from the command and the context provided in the
        /// call to this method
        /// </summary>
        /// <param name="context">An object to be passed to the callback method</param>
        /// <param name="text">The SQL Statement to execute</param>
        /// <param name="type">The type of command the SQL Statement corresponds to</param>
        /// <param name="populator">A delegate method that accepts the DbDataReader and the object context as parameters and returns an object</param>
        /// <returns>The object returned from your DBContextCallback method</returns>
        /// <remarks>The DBContextCallback has a method signature that not only accepts a DBDataReader, 
        /// but also any argument passed to this method as the context parameter</remarks>
        public object ExecuteRead(string text, CommandType type, object context, DBContextCallback populator)
        {
            return ExecuteRead(text, type, context, populator, null);
        }

        /// <summary>
        /// Executes the specified SQL text as a command (of the specified CommandType) against this DBDatabase connection, 
        /// and invokes the callback method with the DbDataReader from the command and the context provided in the
        /// call to this method
        /// </summary>
        /// <param name="context">An object to be passed to the callback method</param>
        /// <param name="text">The SQL Statement to execute</param>
        /// <param name="type">The type of command the SQL Statement corresponds to</param>
        /// <param name="populator">A delegate method that accepts the DbDataReader and the object context as parameters and returns an object</param>
        /// <param name="onerror">If an error is raised during the execution of the query the onerror method will be invoked</param>
        /// <returns>The object returned from your DBContextCallback method</returns>
        /// <remarks>The DBContextCallback has a method signature that not only accepts a DBDataReader, 
        /// but also any argument passed to this method as the context parameter</remarks>
        public object ExecuteRead(string text, CommandType type, object context, DBContextCallback populator, DBOnErrorCallback onerror)
        {
            if (string.IsNullOrEmpty(text))
                throw new ArgumentNullException("text");

            if (null == populator)
                throw new ArgumentNullException("populator");

            object returns;

            using (DbCommand cmd = this.CreateCommand(text, type))
            {
                returns = this.DoExecuteContextRead(cmd, context, populator, onerror);
            }

            return returns;
        }

        #endregion



        #region public object[] ExecuteReadEach(DbTransaction transaction, DBQuery query, object context, DBContextRecordCallback populator) + 1 overload

        /// <summary>
        /// Executes the specified DBQuery as a command against this DBDatabase transaction, 
        /// and invokes the callback method with the IDataRecord FOR EACH result of the command and the context provided in the
        /// call to this method
        /// </summary>
        /// <param name="transaction">A currently open and active transaction</param>
        /// <param name="context">An object to be passed to the callback method</param>
        /// <param name="query">The DBQuery to generate the SQL command</param>
        /// <param name="populator">A delegate method that accepts the IDataRecord FOR EACH record and the object context as parameters and returns an object</param>
        /// <returns>The object array returned from your DBContextCallback method</returns>
        /// <remarks>The DBContextCallback has a method signature that not only accepts a IDataRecord, 
        /// but also any argument passed to this method as the context parameter</remarks>
        public object[] ExecuteReadEach(DbTransaction transaction, DBQuery query, object context, DBContextRecordCallback populator)
        {
            return ExecuteReadEach(transaction, query, context, populator, null);
        }

        /// <summary>
        /// Executes the specified DBQuery as a command against this DBDatabase transaction, 
        /// and invokes the callback method with the IDataRecord FOR EACH result of the command and the context provided in the
        /// call to this method
        /// </summary>
        /// <param name="transaction">A currently open and active transaction</param>
        /// <param name="context">An object to be passed to the callback method</param>
        /// <param name="query">The DBQuery to generate the SQL command</param>
        /// <param name="populator">A delegate method that accepts the IDataRecord FOR EACH result and the object context as parameters and returns an object</param>
        /// <param name="onerror">If an error is raised during the execution of the query the onerror method will be invoked</param>
        /// <returns>The object returned from your DBContextCallback method</returns>
        /// <remarks>The DBContextCallback has a method signature that not only accepts a IDataReader, 
        /// but also any argument passed to this method as the context parameter</remarks>
        public object[] ExecuteReadEach(DbTransaction transaction, DBQuery query, object context, DBContextRecordCallback populator, DBOnErrorCallback onerror)
        {
            return (object[])ExecuteRead(transaction, query, context, delegate (DbDataReader reader, object innercontext)
            {
                List<object> all = new List<object>();
                while (reader.Read())
                {
                    all.Add(populator(reader, innercontext));
                }
                return all.ToArray();

            }, onerror);
        }

        #endregion

        #region public object ExecuteReadOne(DbTransaction transaction, DBQuery query, object context, DBContextRecordCallback populator) + 1 overload

        /// <summary>
        /// Executes the specified DBQuery as a command against this DBDatabase transaction, 
        /// and invokes the callback method with the IDataRecord from the FIRST result of the command and the context provided in the
        /// call to this method
        /// </summary>
        /// <param name="transaction">A currently open and active transaction</param>
        /// <param name="context">An object to be passed to the callback method</param>
        /// <param name="query">The DBQuery to generate the SQL command</param>
        /// <param name="populator">A delegate method that accepts the IDataRecord of the FIRST record and the object context as parameters and returns an object</param>
        /// <returns>The object returned from your DBContextCallback method</returns>
        /// <remarks>The DBContextCallback has a method signature that not only accepts a IDataRecord, 
        /// but also any argument passed to this method as the context parameter</remarks>
        public object ExecuteReadOne(DbTransaction transaction, DBQuery query, object context, DBContextRecordCallback populator)
        {
            return ExecuteReadOne(transaction, query, context, populator, null);
        }

        /// <summary>
        /// Executes the specified DBQuery as a command against this DBDatabase transaction, 
        /// and invokes the callback method with the IDataRecord from the FIRST result of the command and the context provided in the
        /// call to this method
        /// </summary>
        /// <param name="transaction">A currently open and active transaction</param>
        /// <param name="context">An object to be passed to the callback method</param>
        /// <param name="query">The DBQuery to generate the SQL command</param>
        /// <param name="populator">A delegate method that accepts the IDataRecord of the FIRST record and the object context as parameters and returns an object</param>
        /// <param name="onerror">If an error is raised during the execution of the query the onerror method will be invoked</param>
        /// <returns>The object returned from your DBContextCallback method</returns>
        /// <remarks>The DBContextCallback has a method signature that not only accepts a IDataReader, 
        /// but also any argument passed to this method as the context parameter</remarks>
        public object ExecuteReadOne(DbTransaction transaction, DBQuery query, object context, DBContextRecordCallback populator, DBOnErrorCallback onerror)
        {
            return ExecuteRead(transaction, query, context, delegate (DbDataReader reader, object innercontext)
            {
                if (reader.Read())
                {
                    return populator(reader, innercontext);
                }
                else
                    return null;
            }, onerror);
        }

        #endregion

        #region public object ExecuteRead(DbTransaction transaction, DBQuery query, object context, DBContextCallback populator)

        /// <summary>
        /// Executes the specified DBQuery as a command against this DBDatabase connection, 
        /// and invokes the callback method with the DbDataReader from the command and the context provided in the
        /// call to this method
        /// </summary>
        /// <param name="transaction">A currently open and active transaction</param>
        /// <param name="context">An object to be passed to the callback method</param>
        /// <param name="query">The DBQuery to generate the SQL command</param>
        /// <param name="populator">A delegate method that accepts the DbDataReader and the object context as parameters and returns an object</param>
        /// <returns>The object returned from your DBContextCallback method</returns>
        /// <remarks>The DBContextCallback has a method signature that not only accepts a DBDataReader, 
        /// but also any argument passed to this method as the context parameter</remarks>
        public object ExecuteRead(DbTransaction transaction, DBQuery query, object context, DBContextCallback populator)
        {
            return ExecuteRead(transaction, query, context, populator, null);
        }
        /// <summary>
        /// Executes the specified DBQuery as a command against this DBDatabase connection, 
        /// and invokes the callback method with the DbDataReader from the command and the context provided in the
        /// call to this method
        /// </summary>
        /// <param name="transaction">A currently open and active transaction</param>
        /// <param name="context">An object to be passed to the callback method</param>
        /// <param name="query">The DBQuery to generate the SQL command</param>
        /// <param name="populator">A delegate method that accepts the DbDataReader and the object context as parameters and returns an object</param>
        /// <param name="onerror">If an error is raised during the execution of the query the onerror method will be invoked</param>
        /// <returns>The object returned from your DBContextCallback method</returns>
        /// <remarks>The DBContextCallback has a method signature that not only accepts a DBDataReader, 
        /// but also any argument passed to this method as the context parameter</remarks>
        public object ExecuteRead(DbTransaction transaction, DBQuery query, object context, DBContextCallback populator, DBOnErrorCallback onerror)
        {
            object returns;

            if (null == transaction)
                throw new ArgumentNullException("transaction");

            if (null == transaction.Connection)
                throw new ArgumentNullException("transaction.Connection");

            if (null == query)
                throw new ArgumentNullException("query");

            if (null == populator)
                throw new ArgumentNullException("populator");

            using (DbCommand cmd = this.CreateCommand(transaction, query))
            {
                returns = this.DoExecuteContextRead(cmd, context, populator, onerror);
            }

            return returns;
        }

        #endregion



        #region public object[] ExecuteReadEach(DBQuery query, object context, DBContextRecordCallback populator)

        /// <summary>
        /// Executes the specified DBQuery as a command against this DBDatabase connection, 
        /// and invokes the callback method with the IDataRecord FOR EACH record from the command and the context provided in the
        /// call to this method
        /// </summary>
        /// <param name="context">An object to be passed to the callback method</param>
        /// <param name="query">The DBQuery to generate the SQL command</param>
        /// <param name="populator">A delegate method that accepts the IDataRecord and the object context as parameters and returns an object</param>
        /// <returns>The object returned from your DBCallback method</returns>
        /// <remarks>The DBContextCallback has a method signature that not only accepts a IDataRecord, 
        /// but also any argument passed to this method as the context parameter</remarks>
        public object[] ExecuteReadEach(DBQuery query, object context, DBContextRecordCallback populator)
        {
            return ExecuteReadEach(query, context, populator, null);
        }

        /// <summary>
        /// Executes the specified DBQuery as a command against this DBDatabase connection, 
        /// and invokes the callback method ONCE with the IDataRecord from the FIRST command and the context provided in the
        /// call to this method
        /// </summary>
        /// <param name="context">An object to be passed to the callback method</param>
        /// <param name="query">The DBQuery to generate the SQL command</param>
        /// <param name="populator">A delegate method that accepts the IDataRecord and the object context as parameters and returns an object</param>
        /// <param name="onerror">If an error is raised during the execution of the query the onerror method will be invoked</param>
        /// <returns>The array of objects returned from your DBCallback method</returns>
        /// <remarks>The DBContextCallback has a method signature that not only accepts a IDataRecord, 
        /// but also any argument passed to this method as the context parameter</remarks>
        public object[] ExecuteReadEach(DBQuery query, object context, DBContextRecordCallback populator, DBOnErrorCallback onerror)
        {
            return (object[])ExecuteRead(query, context, delegate (DbDataReader reader, object innercontext)
            {
                List<object> all = new List<object>();
                while (reader.Read())
                {
                    all.Add(populator(reader, innercontext));
                }
                return all.ToArray();

            }, onerror);
        }

        #endregion

        #region public object ExecuteReadOne(DBQuery query, object context, DBContextRecordCallback populator)

        /// <summary>
        /// Executes the specified DBQuery as a command against this DBDatabase connection, 
        /// and invokes the callback method ONCE with the IDataRecord from the FIRST command and the context provided in the
        /// call to this method
        /// </summary>
        /// <param name="context">An object to be passed to the callback method</param>
        /// <param name="query">The DBQuery to generate the SQL command</param>
        /// <param name="populator">A delegate method that accepts the IDataRecord and the object context as parameters and returns an object</param>
        /// <returns>The object returned from your DBCallback method</returns>
        /// <remarks>The DBContextCallback has a method signature that not only accepts a IDataRecord, 
        /// but also any argument passed to this method as the context parameter</remarks>
        public object ExecuteReadOne(DBQuery query, object context, DBContextRecordCallback populator)
        {
            return ExecuteReadOne(query, context, populator, null);
        }

        /// <summary>
        /// Executes the specified DBQuery as a command against this DBDatabase connection, 
        /// and invokes the callback method ONCE with the IDataRecord from the FIRST command and the context provided in the
        /// call to this method
        /// </summary>
        /// <param name="context">An object to be passed to the callback method</param>
        /// <param name="query">The DBQuery to generate the SQL command</param>
        /// <param name="populator">A delegate method that accepts the IDataRecord and the object context as parameters and returns an object</param>
        /// <param name="onerror">If an error is raised during the execution of the query the onerror method will be invoked</param>
        /// <returns>The object returned from your DBCallback method</returns>
        /// <remarks>The DBContextCallback has a method signature that not only accepts a IDataRecord, 
        /// but also any argument passed to this method as the context parameter</remarks>
        public object ExecuteReadOne(DBQuery query, object context, DBContextRecordCallback populator, DBOnErrorCallback onerror)
        {
            return ExecuteRead(query, context, delegate (DbDataReader reader, object innercontext)
            {
                if (reader.Read())
                {
                    return populator(reader, innercontext);
                }
                else
                    return null;

            }, onerror);
        }

        #endregion

        #region public object ExecuteRead(DBQuery query, object context, DBContextCallback populator)

        /// <summary>
        /// Executes the specified DBQuery as a command against this DBDatabase connection, 
        /// and invokes the callback method with the DbDataReader from the command and the context provided in the
        /// call to this method
        /// </summary>
        /// <param name="context">An object to be passed to the callback method</param>
        /// <param name="query">The DBQuery to generate the SQL command</param>
        /// <param name="populator">A delegate method that accepts the DbDataReader and the object context as parameters and returns an object</param>
        /// <returns>The object returned from your DBCallback method</returns>
        /// <remarks>The DBContextCallback has a method signature that not only accepts a DBDataReader, 
        /// but also any argument passed to this method as the context parameter</remarks>
        public object ExecuteRead(DBQuery query, object context, DBContextCallback populator)
        {
            return ExecuteRead(query, context, populator, null);
        }

        /// <summary>
        /// Executes the specified DBQuery as a command against this DBDatabase connection, 
        /// and invokes the callback method with the DbDataReader from the command and the context provided in the
        /// call to this method
        /// </summary>
        /// <param name="context">An object to be passed to the callback method</param>
        /// <param name="query">The DBQuery to generate the SQL command</param>
        /// <param name="populator">A delegate method that accepts the DbDataReader and the object context as parameters and returns an object</param>
        /// <param name="onerror">If an error is raised during the execution of the query the onerror method will be invoked</param>
        /// <returns>The object returned from your DBCallback method</returns>
        /// <remarks>The DBContextCallback has a method signature that not only accepts a DBDataReader, 
        /// but also any argument passed to this method as the context parameter</remarks>
        public object ExecuteRead(DBQuery query, object context, DBContextCallback populator, DBOnErrorCallback onerror)
        {
            object returns;

            if (null == query)
                throw new ArgumentNullException("query");

            if (null == populator)
                throw new ArgumentNullException("populator");

            using (DbCommand cmd = this.CreateCommand(query))
            {
                returns = this.DoExecuteContextRead(cmd, context, populator, onerror);
            }

            return returns;
        }

        #endregion


        #region public object[] ExecuteReadEach(DbCommand cmd, object context, DBContextRecordCallback populator)

        /// <summary>
        /// Executes the specified DBQuery as a command against this DBDatabase connection, 
        /// and invokes the callback method with the IDataRecord FOR EACH record from the command and the context provided in the
        /// call to this method
        /// </summary>
        /// <param name="context">An object to be passed to the callback method</param>
        /// <param name="cmd">The initialiazed DbCommand with a non null DbConnection (if the connection is not open it will be opened and closed within the method)</param>
        /// <param name="populator">A delegate method that accepts the IDataRecord and the object context as parameters and returns an object</param>
        /// <returns>An array of all the objects returned from your DBCallback method</returns>
        /// <remarks>The DBContextRecordCallback has a method signature that not only accepts a IDataRecord, 
        /// but also any argument passed to this method as the context parameter</remarks>
        public object[] ExecuteReadEach(DbCommand cmd, object context, DBContextRecordCallback populator)
        {
            return this.ExecuteReadEach(cmd, context, populator, null);
        }

        /// <summary>
        /// Executes the specified DBQuery as a command against this DBDatabase connection, 
        /// and invokes the callback method with the IDataRecord FOR EACH record from the command and the context provided in the
        /// call to this method
        /// </summary>
        /// <param name="context">An object to be passed to the callback method</param>
        /// <param name="cmd">The initialiazed DbCommand with a non null DbConnection (if the connection is not open it will be opened and closed within the method)</param>
        /// <param name="populator">A delegate method that accepts the IDataRecord and the object context as parameters and returns an object</param>
        /// <param name="onerror">A delegate method that is called when there is an error executing the statement</param>
        /// <returns>An array of all the objects returned from your DBCallback method</returns>
        /// <remarks>The DBContextRecordCallback has a method signature that not only accepts a IDataRecord, 
        /// but also any argument passed to this method as the context parameter</remarks>
        public object[] ExecuteReadEach(DbCommand cmd, object context, DBContextRecordCallback populator, DBOnErrorCallback onerror)
        {
            return (object[])this.ExecuteRead(cmd, context, delegate (DbDataReader reader, object innercontext)
            {
                List<object> all = new List<object>();

                while (reader.Read())
                {
                    all.Add(populator(reader, context));
                }
                return all.ToArray();

            }, onerror);
        }

        #endregion

        #region public object ExecuteReadOne(DbCommand cmd, object context, DBContextRecordCallback populator)

        /// <summary>
        /// Executes the specified DBQuery as a command against this DBDatabase connection, 
        /// and invokes the callback method with the IDataRecord ONCE from the command and the context provided in the
        /// call to this method
        /// </summary>
        /// <param name="context">An object to be passed to the callback method</param>
        /// <param name="cmd">The initialiazed DbCommand with a non null DbConnection (if the connection is not open it will be opened and closed within the method)</param>
        /// <param name="populator">A delegate method that accepts the IDataRecord and the object context as parameters and returns an object</param>
        /// <returns>The object returned from your DBCallback method</returns>
        /// <remarks>The DBContextRecordCallback has a method signature that not only accepts a IDataRecord, 
        /// but also any argument passed to this method as the context parameter</remarks>
        public object ExecuteReadOne(DbCommand cmd, object context, DBContextRecordCallback populator)
        {
            return this.ExecuteReadOne(cmd, context, populator, null);
        }

        /// <summary>
        /// Executes the specified DBQuery as a command against this DBDatabase connection, 
        /// and invokes the callback method with the IDataRecord ONCE from the command and the context provided in the
        /// call to this method
        /// </summary>
        /// <param name="context">An object to be passed to the callback method</param>
        /// <param name="cmd">The initialiazed DbCommand with a non null DbConnection (if the connection is not open it will be opened and closed within the method)</param>
        /// <param name="populator">A delegate method that accepts the IDataRecord and the object context as parameters and returns an object</param>
        /// <param name="onerror">A delegate method that is called when there is an error executing the statement</param>
        /// <returns>The object returned from your DBCallback method</returns>
        /// <remarks>The DBContextRecordCallback has a method signature that not only accepts a IDataRecord, 
        /// but also any argument passed to this method as the context parameter</remarks>
        public object ExecuteReadOne(DbCommand cmd, object context, DBContextRecordCallback populator, DBOnErrorCallback onerror)
        {
            return this.ExecuteRead(cmd, context, delegate (DbDataReader reader, object innercontext)
            {
                if (reader.Read())
                    return populator(reader, context);
                else
                    return null;
            }, onerror);
        }

        #endregion

        #region public object ExecuteRead(DbCommand cmd, object context, DBContextCallback populator)

        /// <summary>
        /// Executes the specified DBQuery as a command against this DBDatabase connection, 
        /// and invokes the callback method with the DbDataReader from the command and the context provided in the
        /// call to this method
        /// </summary>
        /// <param name="context">An object to be passed to the callback method</param>
        /// <param name="cmd">The initialiazed DbCommand with a non null DbConnection (if the connection is not open it will be opened and closed within the method)</param>
        /// <param name="populator">A delegate method that accepts the DbDataReader and the object context as parameters and returns an object</param>
        /// <returns>The object returned from your DBCallback method</returns>
        /// <remarks>The DBContextCallback has a method signature that not only accepts a DBDataReader, 
        /// but also any argument passed to this method as the context parameter</remarks>
        public object ExecuteRead(DbCommand cmd, object context, DBContextCallback populator)
        {
            return this.ExecuteRead(cmd, context, populator, null);
        }

        /// <summary>
        /// Executes the specified DBQuery as a command against this DBDatabase connection, 
        /// and invokes the callback method with the DbDataReader from the command and the context provided in the
        /// call to this method
        /// </summary>
        /// <param name="context">An object to be passed to the callback method</param>
        /// <param name="cmd">The initialiazed DbCommand with a non null DbConnection (if the connection is not open it will be opened and closed within the method)</param>
        /// <param name="populator">A delegate method that accepts the DbDataReader and the object context as parameters and returns an object</param>
        /// <param name="onerror">A delegate method that is called when there is an error executing the statement</param>
        /// <returns>The object returned from your DBCallback method</returns>
        /// <remarks>The DBContextCallback has a method signature that not only accepts a DBDataReader, 
        /// but also any argument passed to this method as the context parameter</remarks>
        public object ExecuteRead(DbCommand cmd, object context, DBContextCallback populator, DBOnErrorCallback onerror)
        {
            if (cmd == null)
                throw new ArgumentNullException("cmd");

            if (cmd.Connection == null)
                throw new ArgumentNullException("cmd.Connection");

            if (populator == null)
                throw new ArgumentNullException("populator");

            object returns = DoExecuteContextRead(cmd, context, populator, onerror);

            return returns;
        }


        #endregion


        #region protected virtual object DoExecuteContextRead(DbCommand cmd, object context, DBContextCallback populator)

        /// <summary>
        /// Virtual (overridable) execute read method accepting a DBContextCallback. All ExecuteRead methods that 
        /// use the context callback ultimately call this method
        /// </summary>
        /// <param name="cmd">The DbCommand to execute</param>
        /// <param name="context">The provided context to pass on to the populator</param>
        /// <param name="populator">The delegate method to perform the actual reading</param>
        /// <returns>Any object retruned from the DBContaxtCallback will by retruned from this method (can be null)</returns>
        /// <remarks>If the commands' connection is closed it will be opened. Any reader will be disposed, and profiling will be undertaken if started.
        /// It is assumed all parameters are not null, and is up to the calling methods to check this beforehand.</remarks>
        protected virtual object DoExecuteContextRead(DbCommand cmd, object context, DBContextCallback populator, DBOnErrorCallback onerror)
        {
            object returns = null;
            DbDataReader reader = null;
            bool opened = false;
            Profile.IDBProfileExecData exec = null;

            try
            {
                if (cmd.Connection.State == ConnectionState.Closed)
                {
                    cmd.Connection.Open();
                    opened = true;
                }

                if (IsProfiling)
                    exec = ProfileBegin(cmd);

                reader = cmd.ExecuteReader();
                returns = populator(reader, context);
            }
            catch (Exception ex)
            {
                ReThrowAction action;
                this.HandleExecutionError(onerror, ref ex, out action);

                if (action == ReThrowAction.Original)
                    throw;
                else if (action == ReThrowAction.Wrapped)
                    throw ex;
                else if (action == ReThrowAction.None)
                    ; //consume it.
                else
                    throw new ArgumentOutOfRangeException("Unknown action to perform", ex);
            }
            finally
            {
                if (null != reader)
                    reader.Dispose();

                if (opened)
                    cmd.Connection.Close();

                if (IsProfiling)
                    ProfileEnd(exec);
            }
            return returns;
        }

        #endregion

        // ExecuteRead() no return value

        #region public void ExecuteReadEach(string text, DBEmptyRecordCallback populator)

        /// <summary>
        /// Executes the specified SQL text as a command against the this DBDatabase connection, 
        /// and invokes the callback method with the IDataRecord FOR EACH record from the command
        /// </summary>
        /// <param name="text">The SQL statement to execute</param>
        /// <param name="populator">A delegate method that accepts the IDataRecord as a parameter</param>
        /// <remarks>The DBEmptyCallback does not require a return value</remarks>
        public void ExecuteReadEach(string text, DBEmptyRecordCallback populator)
        {
            ExecuteReaderEach(text, CommandType.Text, populator, null);
        }

        /// <summary>
        /// Executes the specified SQL text as a command against the this DBDatabase connection, 
        /// and invokes the callback method with the IDataRecord FOR EACH record from the command
        /// </summary>
        /// <param name="text">The SQL statement to execute</param>
        /// <param name="populator">A delegate method that accepts the IDataRecord as a parameter</param>
        /// <remarks>The DBEmptyCallback does not require a return value</remarks>
        public void ExecuteReadEach(string text, DBEmptyRecordCallback populator, DBOnErrorCallback onerror)
        {
            ExecuteReaderEach(text, CommandType.Text, populator, onerror);
        }

        #endregion

        #region public void ExecuteReadOne(string text, DBEmptyRecordCallback populator)

        /// <summary>
        /// Executes the specified SQL text as a command against the this DBDatabase connection, 
        /// and invokes the callback method with the IDataRecord ONCE from the command
        /// </summary>
        /// <param name="text">The SQL statement to execute</param>
        /// <param name="populator">A delegate method that accepts the IDataRecord as a parameter</param>
        /// <remarks>The DBEmptyCallback does not require a return value</remarks>
        public void ExecuteReadOne(string text, DBEmptyRecordCallback populator)
        {
            ExecuteReaderOne(text, CommandType.Text, populator, null);
        }

        /// <summary>
        /// Executes the specified SQL text as a command against the this DBDatabase connection, 
        /// and invokes the callback method with the IDataRecord ONCE from the command
        /// </summary>
        /// <param name="text">The SQL statement to execute</param>
        /// <param name="populator">A delegate method that accepts the IDataRecord as a parameter</param>
        /// <remarks>The DBEmptyCallback does not require a return value</remarks>
        public void ExecuteReaderOne(string text, DBEmptyRecordCallback populator, DBOnErrorCallback onerror)
        {
            ExecuteReaderOne(text, CommandType.Text, populator, onerror);
        }

        #endregion

        #region public void ExecuteRead(string text, DBEmptyCallback populator)

        /// <summary>
        /// Executes the specified SQL text as a command against the this DBDatabase connection, 
        /// and invokes the callback method with the DbDataReader from the command
        /// </summary>
        /// <param name="text">The SQL statement to execute</param>
        /// <param name="populator">A delegate method that accepts the DbDataReader as a parameter</param>
        /// <remarks>The DBEmptyCallback does not require a return value</remarks>
        public void ExecuteRead(string text, DBEmptyCallback populator)
        {
            ExecuteRead(text, CommandType.Text, populator, null);
        }

        /// <summary>
        /// Executes the specified SQL text as a command against the this DBDatabase connection, 
        /// and invokes the callback method with the DbDataReader from the command
        /// </summary>
        /// <param name="text">The SQL statement to execute</param>
        /// <param name="populator">A delegate method that accepts the DbDataReader as a parameter</param>
        /// <remarks>The DBEmptyCallback does not require a return value</remarks>
        public void ExecuteRead(string text, DBEmptyCallback populator, DBOnErrorCallback onerror)
        {
            ExecuteReader(text, CommandType.Text, populator, onerror);
        }

        #endregion



        #region public void ExecuteReadEach(string text, CommandType type, DBEmptyRecordCallback populator)

        /// <summary>
        /// Executes the specified SQL text as a command (of the specified CommandType) against the this DBDatabase connection, 
        /// and invokes the callback method with the IDataRecord FOR EACH record from the command.
        /// </summary>
        /// <param name="text">The SQL Statement to execute</param>
        /// <param name="type">The type of command the SQL Statement corresponds to</param>
        /// <param name="populator">A delegate method that accepts the IDataRecord as a parameter</param>
        /// <remarks>The DBEmptyCallback does not require a return value</remarks>
        public void ExecuteReadEach(string text, CommandType type, DBEmptyRecordCallback populator)
        {
            this.ExecuteReaderEach(text, type, populator, null);
        }

        /// <summary>
        /// Executes the specified SQL text as a command (of the specified CommandType) against the this DBDatabase connection, 
        /// and invokes the callback method with the IDataRecord FOR EACH record from the command.
        /// </summary>
        /// <param name="text">The SQL Statement to execute</param>
        /// <param name="type">The type of command the SQL Statement corresponds to</param>
        /// <param name="populator">A delegate method that accepts the IDataRecord as a parameter</param>
        /// <param name="onerror">A delegate method that is called if there is an error executing the statement</param>
        /// <remarks>The DBEmptyCallback does not require a return value</remarks>
        public void ExecuteReaderEach(string text, CommandType type, DBEmptyRecordCallback populator, DBOnErrorCallback onerror)
        {
            this.ExecuteReader(text, type, reader =>
            {
                while (reader.Read())
                    populator(reader);
            },
            onerror);
        }




        #endregion

        #region public void ExecuteReadOne(string text, CommandType type, DBEmptyRecordCallback populator)

        /// <summary>
        /// Executes the specified SQL text as a command (of the specified CommandType) against the this DBDatabase connection, 
        /// and invokes the callback method with the IDataRecord ONCE from the command.
        /// </summary>
        /// <param name="text">The SQL Statement to execute</param>
        /// <param name="type">The type of command the SQL Statement corresponds to</param>
        /// <param name="populator">A delegate method that accepts the IDataRecord as a parameter</param>
        /// <remarks>The DBEmptyCallback does not require a return value</remarks>
        public void ExecuteReadOne(string text, CommandType type, DBEmptyRecordCallback populator)
        {
            this.ExecuteReaderOne(text, type, populator, null);
        }

        /// <summary>
        /// Executes the specified SQL text as a command (of the specified CommandType) against the this DBDatabase connection, 
        /// and invokes the callback method with the IDataRecord ONCE from the command.
        /// </summary>
        /// <param name="text">The SQL Statement to execute</param>
        /// <param name="type">The type of command the SQL Statement corresponds to</param>
        /// <param name="populator">A delegate method that accepts the IDataRecord as a parameter</param>
        /// <param name="onerror">A delegate method that is called if there is an error executing the statement</param>
        /// <remarks>The DBEmptyCallback does not require a return value</remarks>
        public void ExecuteReaderOne(string text, CommandType type, DBEmptyRecordCallback populator, DBOnErrorCallback onerror)
        {
            this.ExecuteReader(text, type, reader =>
            {
                if (reader.Read())
                    populator(reader);
            },
            onerror);
        }

        #endregion

        #region public void ExecuteRead(string text, CommandType type, DBEmptyCallback populator)

        /// <summary>
        /// Executes the specified SQL text as a command (of the specified CommandType) against the this DBDatabase connection, 
        /// and invokes the callback method with the DbDataReader from the command.
        /// </summary>
        /// <param name="text">The SQL Statement to execute</param>
        /// <param name="type">The type of command the SQL Statement corresponds to</param>
        /// <param name="populator">A delegate method that accepts the DbDataReader as a parameter and returns an object</param>
        /// <remarks>The DBEmptyCallback does not require a return value</remarks>
        public void ExecuteRead(string text, CommandType type, DBEmptyCallback populator)
        {
            this.ExecuteReader(text, type, populator, null);
        }

        /// <summary>
        /// Executes the specified SQL text as a command (of the specified CommandType) against the this DBDatabase connection, 
        /// and invokes the callback method with the DbDataReader from the command.
        /// </summary>
        /// <param name="text">The SQL Statement to execute</param>
        /// <param name="type">The type of command the SQL Statement corresponds to</param>
        /// <param name="populator">A delegate method that accepts the DbDataReader as a parameter and returns an object</param>
        /// <param name="onerror">A delegate method that is called if there is an error executing the statement</param>
        /// <remarks>The DBEmptyCallback does not require a return value</remarks>
        public void ExecuteReader(string text, CommandType type, DBEmptyCallback populator, DBOnErrorCallback onerror)
        {
            if (string.IsNullOrEmpty(text))
                throw new ArgumentNullException("text");
            if (null == populator)
                throw new ArgumentNullException("populator");

            using (DbCommand cmd = this.CreateCommand(text, type))
            {
                this.DoExecuteEmptyRead(cmd, populator, onerror);
            }
        }

        #endregion



        #region public void ExecuteReadEach(DbTransaction transaction, DBQuery query, DBEmptyRecordCallback populator)

        /// <summary>
        /// Generates the DBQuery as a command to execute against the this DBDatabase using the provided transaction, 
        /// and invokes the callback method with the IDataRecord FOR EACH from the command
        /// </summary>
        /// <param name="transaction">A currently open and active transaction</param>
        /// <param name="query">The DBQuery to build the SQL statement from and execute</param>
        /// <param name="populator">A delegate method that accepts the IDataRecord as a parameter</param>
        /// <remarks>The DBEmptyRecordCallback does not require a return value</remarks>
        public void ExecuteReadEach(DbTransaction transaction, DBQuery query, DBEmptyRecordCallback populator)
        {
            this.ExecuteReaderEach(transaction, query, populator, null);
        }

        /// <summary>
        /// Generates the DBQuery as a command to execute against the this DBDatabase using the provided transaction, 
        /// and invokes the callback method with the IDataRecord FOR EACH record from the command
        /// </summary>
        /// <param name="transaction">A currently open and active transaction</param>
        /// <param name="query">The DBQuery to build the SQL statement from and execute</param>
        /// <param name="populator">A delegate method that accepts the IDataRecord as a parameter</param>
        /// <remarks>The DBEmptyRecordCallback does not require a return value</remarks>
        public void ExecuteReaderEach(DbTransaction transaction, DBQuery query, DBEmptyRecordCallback populator, DBOnErrorCallback onerror)
        {
            this.ExecuteReader(transaction, query, reader =>
            {
                while (reader.Read())
                    populator(reader);
            }, onerror);
        }

        #endregion

        #region public void ExecuteReadOne(DbTransaction transaction, DBQuery query, DBEmptyRecordCallback populator)

        /// <summary>
        /// Generates the DBQuery as a command to execute against the this DBDatabase using the provided transaction, 
        /// and invokes the callback method with the IDataRecord ONCE from the command
        /// </summary>
        /// <param name="transaction">A currently open and active transaction</param>
        /// <param name="query">The DBQuery to build the SQL statement from and execute</param>
        /// <param name="populator">A delegate method that accepts the IDataRecord as a parameter</param>
        /// <remarks>The DBEmptyRecordCallback does not require a return value</remarks>
        public void ExecuteReadOne(DbTransaction transaction, DBQuery query, DBEmptyRecordCallback populator)
        {
            this.ExecuteReaderOne(transaction, query, populator, null);
        }

        /// <summary>
        /// Generates the DBQuery as a command to execute against the this DBDatabase using the provided transaction, 
        /// and invokes the callback method with the IDataRecord ONCE with the first record from the command
        /// </summary>
        /// <param name="transaction">A currently open and active transaction</param>
        /// <param name="query">The DBQuery to build the SQL statement from and execute</param>
        /// <param name="populator">A delegate method that accepts the IDataRecord as a parameter</param>
        /// <remarks>The DBEmptyRecordCallback does not require a return value</remarks>
        public void ExecuteReaderOne(DbTransaction transaction, DBQuery query, DBEmptyRecordCallback populator, DBOnErrorCallback onerror)
        {
            this.ExecuteReader(transaction, query, reader =>
            {
                if (reader.Read())
                    populator(reader);
            }, onerror);
        }

        #endregion

        #region public void ExecuteRead(DbTransaction transaction, DBQuery query, DBEmptyCallback populator)

        /// <summary>
        /// Generates the DBQuery as a command to execute against the this DBDatabase using the provided transaction, 
        /// and invokes the callback method with the DbDataReader from the command
        /// </summary>
        /// <param name="transaction">A currently open and active transaction</param>
        /// <param name="query">The DBQuery to build the SQL statement from and execute</param>
        /// <param name="populator">A delegate method that accepts the DbDataReader as a parameter</param>
        /// <remarks>The DBEmptyCallback does not require a return value</remarks>
        public void ExecuteRead(DbTransaction transaction, DBQuery query, DBEmptyCallback populator)
        {
            this.ExecuteReader(transaction, query, populator, null);
        }

        /// <summary>
        /// Generates the DBQuery as a command to execute against the this DBDatabase using the provided transaction, 
        /// and invokes the callback method with the DbDataReader from the command
        /// </summary>
        /// <param name="transaction">A currently open and active transaction</param>
        /// <param name="query">The DBQuery to build the SQL statement from and execute</param>
        /// <param name="populator">A delegate method that accepts the DbDataReader as a parameter</param>
        /// <remarks>The DBEmptyCallback does not require a return value</remarks>
        public void ExecuteReader(DbTransaction transaction, DBQuery query, DBEmptyCallback populator, DBOnErrorCallback onerror)
        {
            if (null == transaction)
                throw new ArgumentNullException("transaction");
            if (null == populator)
                throw new ArgumentNullException("populator");

            using (DbCommand cmd = this.CreateCommand(transaction, query))
            {
                this.DoExecuteEmptyRead(cmd, populator, onerror);
            }
        }

        #endregion



        #region public void ExecuteReadEach(DBQuery query, DBEmptyCallback populator) + 1 overload

        /// <summary>
        /// Generates the DBQuery as a command to execute against the this DBDatabase connection, 
        /// and invokes the callback method with the IDataRecord that will be called FOR EACH with the first item from the query
        /// </summary>
        /// <param name="query">The DBQuery to build the SQL statement from and execute</param>
        /// <param name="populator">A delegate method that accepts the IDataRecord as a parameter.</param>
        /// <remarks>The DBEmptyCallback does not require a return value</remarks>
        public void ExecuteReadEach(DBQuery query, DBEmptyRecordCallback populator)
        {
            this.ExecuteReaderEach(query, populator, null);
        }

        /// <summary>
        /// Generates the DBQuery as a command to execute against the this DBDatabase connection, 
        /// and invokes the callback method with the IDataRecord that will be called FOR EACH with the first item from the query
        /// </summary>
        /// <param name="query">The DBQuery to build the SQL statement from and execute</param>
        /// <param name="populator">A delegate method that accepts the IDataRecord as a parameter.</param>
        /// <remarks>The DBEmptyCallback does not require a return value</remarks>
        public void ExecuteReaderEach(DBQuery query, DBEmptyRecordCallback populator, DBOnErrorCallback onerror)
        {
            this.ExecuteReader(query, reader =>
            {
                while (reader.Read())
                {
                    populator(reader);
                }

            }, onerror);
        }

        #endregion

        #region public void ExecuteReadOne(DBQuery query, DBEmptyRecordCallback populator) + 1 overload

        /// <summary>
        /// Generates the DBQuery as a command to execute against the this DBDatabase connection, 
        /// and invokes the callback method with the IDataRecord that will be called ONCE with the first item from the query
        /// </summary>
        /// <param name="query">The DBQuery to build the SQL statement from and execute</param>
        /// <param name="populator">A delegate method that accepts the IDataRecord as a parameter.</param>
        /// <remarks>The DBEmptyCallback does not require a return value</remarks>
        public void ExecuteReadOne(DBQuery query, DBEmptyRecordCallback populator)
        {
            this.ExecuteReaderOne(query, populator, null);
        }

        /// <summary>
        /// Generates the DBQuery as a command to execute against the this DBDatabase connection, 
        /// and invokes the callback method with the IDataRecord that will be called ONCE with the first item from the query
        /// </summary>
        /// <param name="query">The DBQuery to build the SQL statement from and execute</param>
        /// <param name="populator">A delegate method that accepts the IDataRecord as a parameter.</param>
        /// <remarks>The DBEmptyCallback does not require a return value</remarks>
        public void ExecuteReaderOne(DBQuery query, DBEmptyRecordCallback populator, DBOnErrorCallback onerror)
        {
            this.ExecuteReader(query, reader =>
            {
                if (reader.Read())
                    populator(reader);

            }, onerror);
        }

        #endregion

        #region public void ExecuteRead(DBQuery query, DBEmptyCallback populator)

        /// <summary>
        /// Generates the DBQuery as a command to execute against the this DBDatabase connection, 
        /// and invokes the callback method with the DbDataReader from the command
        /// </summary>
        /// <param name="query">The DBQuery to build the SQL statement from and execute</param>
        /// <param name="populator">A delegate method that accepts the DbDataReader as a parameter.</param>
        /// <remarks>The DBEmptyCallback does not require a return value</remarks>
        public void ExecuteRead(DBQuery query, DBEmptyCallback populator)
        {
            this.ExecuteReader(query, populator, null);
        }

        /// <summary>
        /// Generates the DBQuery as a command to execute against the this DBDatabase connection, 
        /// and invokes the callback method with the DbDataReader from the command
        /// </summary>
        /// <param name="query">The DBQuery to build the SQL statement from and execute</param>
        /// <param name="populator">A delegate method that accepts the DbDataReader as a parameter.</param>
        /// <remarks>The DBEmptyCallback does not require a return value</remarks>
        public void ExecuteReader(DBQuery query, DBEmptyCallback populator, DBOnErrorCallback onerror)
        {
            if (null == query)
                throw new ArgumentNullException("query");
            if (null == populator)
                throw new ArgumentNullException("populator");

            using (DbCommand cmd = this.CreateCommand(query))
            {
                this.DoExecuteEmptyRead(cmd, populator, onerror);
            }
        }

        #endregion



        #region public void ExecuteReadEach(DbCommand cmd, DBEmptyRecordCallback populator)

        /// <summary>
        /// Executes the specified Command against it's connection (opening as required), 
        /// and invokes the callback method with the IDataRecord from the command FOR EACH read entry
        /// </summary>
        /// <param name="cmd">The initialiazed DbCommand with a non null DbConnection (if the connection is not open it will be opened and closed within the method)</param>
        /// <param name="populator">A delegate method that accepts the IDataRecord as a parameter</param>
        /// <remarks>The DBEmptyCallback does not require a return value</remarks>
        public void ExecuteReadEach(DbCommand cmd, DBEmptyRecordCallback populator)
        {
            this.ExecuteReaderEach(cmd, populator, null);
        }

        /// <summary>
        /// Executes the specified Command against it's connection (opening as required), 
        /// and invokes the callback method with the IDataRecord from the command FOR EACH read entry
        /// </summary>
        /// <param name="cmd">The initialiazed DbCommand with a non null DbConnection (if the connection is not open it will be opened and closed within the method)</param>
        /// <param name="populator">A delegate method that accepts the IDataRecord as a parameter</param>
        /// <param name="onerror">A delegate that will be called if there is an error executing the command</param>
        /// <remarks>The DBEmptyCallback does not require a return value</remarks>
        public void ExecuteReaderEach(DbCommand cmd, DBEmptyRecordCallback populator, DBOnErrorCallback onerror)
        {
            this.ExecuteReader(cmd, reader =>
            {
                while (reader.Read())
                {
                    populator(reader);
                }

            }, onerror);
        }

        #endregion

        #region public void ExecuteReadOne(DbCommand cmd, DBEmptyCallback populator) + 1 overload

        /// <summary>
        /// Executes the specified Command against it's connection (opening as required), 
        /// and invokes the callback method ONCE with the IDataRecord from the command for the first read entry
        /// </summary>
        /// <param name="cmd">The initialiazed DbCommand with a non null DbConnection (if the connection is not open it will be opened and closed within the method)</param>
        /// <param name="populator">A delegate method that accepts the IDataRecord as a parameter</param>
        /// <remarks>The DBEmptyCallback does not require a return value</remarks>
        public void ExecuteReadOne(DbCommand cmd, DBEmptyRecordCallback populator)
        {
            this.ExecuteReaderOne(cmd, populator, null);
        }

        /// <summary>
        /// Executes the specified Command against it's connection (opening as required), 
        /// and invokes the callback method ONCE with the IDataRecord from the command for the first read entry
        /// </summary>
        /// <param name="cmd">The initialiazed DbCommand with a non null DbConnection (if the connection is not open it will be opened and closed within the method)</param>
        /// <param name="populator">A delegate method that accepts the IDataRecord as a parameter</param>
        /// <param name="onerror">A delegate that will be called if there is an error executing the command</param>
        /// <remarks>The DBEmptyCallback does not require a return value</remarks>
        public void ExecuteReaderOne(DbCommand cmd, DBEmptyRecordCallback populator, DBOnErrorCallback onerror)
        {
            this.ExecuteReader(cmd, reader =>
            {
                if (reader.Read())
                    populator(reader);

            }, onerror);
        }

        #endregion

        #region public void ExecuteRead(DbCommand cmd, DBEmptyCallback populator)

        /// <summary>
        /// Executes the specified Command against it's connection (opening as required), 
        /// and invokes the callback method with the DbDataReader from the command
        /// </summary>
        /// <param name="cmd">The initialiazed DbCommand with a non null DbConnection (if the connection is not open it will be opened and closed within the method)</param>
        /// <param name="populator">A delegate method that accepts the DbDataReader as a parameter</param>
        /// <remarks>The DBEmptyCallback does not require a return value</remarks>
        public void ExecuteRead(DbCommand cmd, DBEmptyCallback populator)
        {
            ExecuteReader(cmd, populator, null);
        }

        /// <summary>
        /// Executes the specified Command against it's connection (opening as required), 
        /// and invokes the callback method with the DbDataReader from the command
        /// </summary>
        /// <param name="cmd">The initialiazed DbCommand with a non null DbConnection (if the connection is not open it will be opened and closed within the method)</param>
        /// <param name="populator">A delegate method that accepts the DbDataReader as a parameter</param>
        /// <remarks>The DBEmptyCallback does not require a return value</remarks>
        public void ExecuteReader(DbCommand cmd, DBEmptyCallback populator, DBOnErrorCallback onerror)
        {
            if (null == cmd)
                throw new ArgumentNullException("cmd");
            if (null == cmd.Connection)
                throw new ArgumentNullException("cmd.Connection");
            if (null == populator)
                throw new ArgumentNullException("populator");

            DoExecuteEmptyRead(cmd, populator, onerror);
        }



        #endregion


        #region protected virtual void DoExecuteEmptyRead(DbCommand cmd, DBEmptyCallback populator)

        /// <summary>
        /// Virtual (overridable) execute read method accepting a DBCallback. All ExecuteRead methods that 
        /// use the empty callback ultimately call this method
        /// </summary>
        /// <param name="cmd">The DbCommand to execute</param>
        /// <param name="populator">The delegate method to perform the actual reading</param>
        /// <remarks>If the commands' connection is closed it will be opened. Any reader will be disposed, and profiling will be undertaken if started.<br/>
        /// It is assumed all parameters are not null, and is up to the calling methods to check this beforehand.</remarks>
        protected virtual void DoExecuteEmptyRead(DbCommand cmd, DBEmptyCallback populator, DBOnErrorCallback onerror)
        {
            DbDataReader reader = null;
            bool opened = false;
            Profile.IDBProfileExecData exec = null;

            try
            {

                if (cmd.Connection.State == ConnectionState.Closed)
                {
                    cmd.Connection.Open();
                    opened = true;
                }

                if (IsProfiling)
                    exec = ProfileBegin(cmd);

                reader = cmd.ExecuteReader();
                populator(reader);

            }
            catch (Exception ex)
            {
                ReThrowAction action;
                this.HandleExecutionError(onerror, ref ex, out action);

                if (action == ReThrowAction.Original)
                    throw;
                else if (action == ReThrowAction.Wrapped)
                    throw ex;
                else if (action == ReThrowAction.None)
                    ; //consume it.
                else
                    throw new ArgumentOutOfRangeException("Unknown action to perform", ex);
            }
            finally
            {
                if (null != reader)
                    reader.Dispose();

                if (null != cmd)
                {
                    if (opened)
                        cmd.Connection.Close();

                    cmd.Dispose();
                }

                if (IsProfiling)
                    ProfileEnd(exec);
            }
        }

        #endregion

        //
        // ExecuteScalar methods
        //

        #region public object ExecuteScalar(DBQuery query)

        /// <summary>
        /// Generates a SQL Command from the DBQuery and execute this against this instances database, 
        /// returning the scalar result
        /// </summary>
        /// <param name="query">The DBQuery to execute</param>
        /// <returns>The resultant object from the database query</returns>
        public object ExecuteScalar(DBQuery query)
        {
            return ExecuteScalar(query, null);
        }
        /// <summary>
        /// Generates a SQL Command from the DBQuery and execute this against this instances database, 
        /// returning the scalar result
        /// </summary>
        /// <param name="query">The DBQuery to execute</param>
        /// <returns>The resultant object from the database query</returns>
        public object ExecuteScalar(DBQuery query, DBOnErrorCallback onerror)
        {
            if (null == query)
                throw new ArgumentNullException("query");

            object returns;

            using (DbCommand cmd = this.CreateCommand(query))
            {
                returns = this.DoExecuteScalar(cmd, onerror);
            }

            return returns;
        }


        #endregion

        #region "Public DataTable GetDataTable(query)"
        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public DataTable GetDatatable(DBQuery query)
        {
            return GetDatatable(query, null);
        }

        public DataTable GetDatatable(DBQuery query, DBOnErrorCallback onerror)
        {
            if (null == query)
                throw new ArgumentNullException("query");

            DataTable returns;

            using (DbCommand cmd = this.CreateCommand(query))
            {
                returns = this.DoGetDataTable(cmd, onerror);
            }
            return returns;
        }

        #endregion

        #region public object ExecuteScalar(DbConnection connection, DBQuery query)

        /// <summary>
        /// Generates a SQL Command from the DBQuery and executes this against the specifed connection, 
        /// returning the scalar result
        /// </summary>
        /// <param name="query">The DBQuery to execute</param>
        /// <param name="connection">The database connection to use</param>
        /// <returns>The resultant object from the database query</returns>
        public object ExecuteScalar(DbConnection connection, DBQuery query)
        {
            return ExecuteScalar(connection, query, null);
        }

        /// <summary>
        /// Generates a SQL Command from the DBQuery and executes this against the specifed connection, 
        /// returning the scalar result
        /// </summary>
        /// <param name="query">The DBQuery to execute</param>
        /// <param name="connection">The database connection to use</param>
        /// <returns>The resultant object from the database query</returns>
        public object ExecuteScalar(DbConnection connection, DBQuery query, DBOnErrorCallback onerror)
        {
            if (null == connection)
                throw new ArgumentNullException("connection");
            if (null == query)
                throw new ArgumentNullException("query");

            object returns;

            using (DbCommand cmd = this.CreateCommand(connection, query))
            {
                returns = this.DoExecuteScalar(cmd, onerror);
            }

            return returns;
        }

        #endregion

        #region public object ExecuteScalar(DbTransaction transaction, DBQuery query) + 1 overload

        /// <summary>
        /// Generates a SQL Command from the DBQuery and execute this with the specified transaction, 
        /// returning the scalar result.
        /// </summary>
        /// <param name="query">The DBQuery to execute</param>
        /// <param name="transaction">The valid and active transaction to execute under</param>
        /// <returns>The resultant object from the database query</returns>
        public object ExecuteScalar(DbTransaction transaction, DBQuery query)
        {
            return ExecuteScalar(transaction, query, null);
        }

        /// <summary>
        /// Generates a SQL Command from the DBQuery and execute this with the specified transaction, 
        /// returning the scalar result.
        /// </summary>
        /// <param name="query">The DBQuery to execute</param>
        /// <param name="transaction">The valid and active transaction to execute under</param>
        /// <returns>The resultant object from the database query</returns>
        public object ExecuteScalar(DbTransaction transaction, DBQuery query, DBOnErrorCallback onerror)
        {
            if (null == transaction)
                throw new ArgumentNullException("transaction");
            if (null == transaction.Connection)
                throw new ArgumentNullException("transaction.Connection");
            if (null == query)
                throw new ArgumentNullException("query");

            object returns;

            using (DbCommand cmd = this.CreateCommand(transaction, query))
            {
                returns = this.DoExecuteScalar(cmd, onerror);
            }

            return returns;
        }

        #endregion

        #region public object ExecuteScalar(string text) + 1 overload

        /// <summary>
        /// Executes a SQL string against this instances database, 
        /// returning the scalar result
        /// </summary>
        /// <param name="text">The SQL string to execute</param>
        /// <returns>The resultant object from the database execution</returns>
        public object ExecuteScalar(string text)
        {
            return ExecuteScalar(text, null);
        }

        /// <summary>
        /// Executes a SQL string against this instances database, 
        /// returning the scalar result
        /// </summary>
        /// <param name="text">The SQL string to execute</param>
        /// <returns>The resultant object from the database execution</returns>
        public object ExecuteScalar(string text, DBOnErrorCallback onerror)
        {
            return this.ExecuteScalar(text, CommandType.Text, onerror);
        }

        #endregion

        #region public object ExecuteScalar(string text, CommandType type) + 1 overload

        /// <summary>
        /// Executes a SQL string (of the specified type) against this instances database, 
        /// returning the scalar result
        /// </summary>
        /// <param name="text">The SQL string to execute</param>
        /// <param name="type">The type of command hte sql text represents</param>
        /// <returns>The resultant object from the database execution</returns>
        public object ExecuteScalar(string text, CommandType type)
        {
            return this.ExecuteScalar(text, type, null);
        }

        /// <summary>
        /// Executes a SQL string (of the specified type) against this instances database, 
        /// returning the scalar result
        /// </summary>
        /// <param name="text">The SQL string to execute</param>
        /// <param name="type">The type of command hte sql text represents</param>
        /// <returns>The resultant object from the database execution</returns>
        public object ExecuteScalar(string text, CommandType type, DBOnErrorCallback onerror)
        {
            if (string.IsNullOrEmpty(text))
                throw new ArgumentNullException("text");

            object result;

            using (DbCommand cmd = this.CreateCommand(text, type))
            {
                result = this.DoExecuteScalar(cmd, onerror);
            }

            return result;
        }

        #endregion

        #region public object ExecuteScalar(DbCommand cmd) + 1 overload

        /// <summary>
        /// Executes the DBCommand (which must include the correct connection) returning the scalar result
        /// </summary>
        /// <param name="cmd">The SQL command to execute</param>
        /// <returns>The resultant object from the database execution</returns>
        public object ExecuteScalar(DbCommand cmd)
        {
            return this.ExecuteScalar(cmd, null);
        }

        /// <summary>
        /// Executes the DBCommand (which must include the correct connection) returning the scalar result
        /// </summary>
        /// <param name="cmd">The SQL command to execute</param>
        /// <returns>The resultant object from the database execution</returns>
        public object ExecuteScalar(DbCommand cmd, DBOnErrorCallback onerror)
        {
            if (null == cmd)
                throw new ArgumentNullException("cmd");

            if (null == cmd.Connection)
                throw new ArgumentNullException("cmd.Connection");

            object result = DoExecuteScalar(cmd, onerror);

            return result;
        }

        #endregion

        #region protected virtual object DoExecuteScalar(DbCommand cmd, DBOnErrorCallback onerror)

        /// <summary>
        ///  Virtual (overridable) execute read method accepting a command. All ExecuteScalar methods ultimately call this method
        /// </summary>
        /// <param name="cmd">The command to execute</param>
        /// <returns>The result of the ExecuteScalar method</returns>
        protected virtual object DoExecuteScalar(DbCommand cmd, DBOnErrorCallback onerror)
        {
            object result = null;
            bool opened = false;
            Profile.IDBProfileExecData exec = null;

            try
            {
                if (cmd.Connection.State == ConnectionState.Closed)
                {
                    cmd.Connection.Open();
                    opened = true;
                }

                if (IsProfiling)
                    exec = ProfileBegin(cmd);

                result = cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                ReThrowAction action;
                this.HandleExecutionError(onerror, ref ex, out action);

                if (action == ReThrowAction.Original)
                    throw;
                else if (action == ReThrowAction.Wrapped)
                    throw ex;
                else if (action == ReThrowAction.None)
                    ; //consume it.
                else
                    throw new ArgumentOutOfRangeException("Unknown action to perform", ex);
            }
            finally
            {
                if (IsProfiling)
                    ProfileEnd(exec);

                if (opened)
                    cmd.Connection.Close();
            }
            return result;
        }

        #endregion
        //

        protected virtual System.Data.DataTable DoGetDataTable(DbCommand cmd, DBOnErrorCallback onerror) {
           System.Data.DataSet result = new DataSet();
            bool opened = false;
            Profile.IDBProfileExecData exec = null;

            try
            {
                if (cmd.Connection.State == ConnectionState.Closed)
                {
                    cmd.Connection.Open();
                    opened = true;
                }               
                System.Data.Common.DbDataAdapter da = this.Factory.CreateDataAdapter();
                da.SelectCommand = cmd;                
                da.Fill(result);
                if (IsProfiling)
                    exec = ProfileBegin(cmd);

                
            }
            catch (Exception ex)
            {
                ReThrowAction action;
                this.HandleExecutionError(onerror, ref ex, out action);

                if (action == ReThrowAction.Original)
                    throw;
                else if (action == ReThrowAction.Wrapped)
                    throw ex;
                else if (action == ReThrowAction.None)
                    ; //consume it.
                else
                    throw new ArgumentOutOfRangeException("Unknown action to perform", ex);
            }
            finally
            {
                if (IsProfiling)
                    ProfileEnd(exec);

                if (opened)
                    cmd.Connection.Close();
            }
            if (result.Tables.Count > 0)
            {
                return result.Tables[0];
            }
            else {
                return new DataTable();
            }            
        }

        //
        // ExecuteNonQuery methods
        //

        #region public int ExecuteNonQuery(DBQuery query)

        /// <summary>
        /// Generates a SQL Command from the DBQuery and executes this against this instances database, 
        /// returning the integer result
        /// </summary>
        /// <param name="query">The DBQuery to execute</param>
        /// <returns>The resultant integer from the database query</returns>
        public int ExecuteNonQuery(DBQuery query)
        {
            return ExecuteNonQuery(query, null);
        }

        /// <summary>
        /// Generates a SQL Command from the DBQuery and executes this against this instances database, 
        /// returning the integer result
        /// </summary>
        /// <param name="query">The DBQuery to execute</param>
        /// <param name="onerror">Callback for error handling</param>
        /// <returns>The resultant integer from the database query</returns>
        public int ExecuteNonQuery(DBQuery query, DBOnErrorCallback onerror)
        {
            if (null == query)
                throw new ArgumentNullException("query");

            int returns;

            using (DbCommand cmd = this.CreateCommand(query))
            {
                returns = this.DoExecuteNonQuery(cmd, onerror);
            }

            return returns;

        }

        #endregion

        #region public int ExecuteNonQuery(DbConnection connection, DBQuery query) + 1 overload

        /// <summary>
        /// Generates a SQL Command from the DBQuery and executes this against the specifed connection, 
        /// returning the integer result
        /// </summary>
        /// <param name="query">The DBQuery to execute</param>
        /// <param name="connection">The database connection to use</param>
        /// <returns>The resultant integer from the database query</returns>
        public int ExecuteNonQuery(DbConnection connection, DBQuery query)
        {
            return this.ExecuteNonQuery(connection, query, null);
        }

        /// <summary>
        /// Generates a SQL Command from the DBQuery and executes this against the specifed connection, 
        /// returning the integer result
        /// </summary>
        /// <param name="query">The DBQuery to execute</param>
        /// <param name="connection">The database connection to use</param>
        /// <returns>The resultant integer from the database query</returns>
        public int ExecuteNonQuery(DbConnection connection, DBQuery query, DBOnErrorCallback onerror)
        {
            if (null == connection)
                throw new ArgumentNullException("connection");
            if (null == query)
                throw new ArgumentNullException("query");

            int returns;

            using (DbCommand cmd = this.CreateCommand(connection, query))
            {
                returns = this.DoExecuteNonQuery(cmd, onerror);
            }

            return returns;
        }

        #endregion

        #region public int ExecuteNonQuery(DbTransaction transaction, DBQuery query) + 1 overload

        /// <summary>
        /// Generates a SQL Command from the DBQuery and execute this with the specified transaction, 
        /// returning the integer result.
        /// </summary>
        /// <param name="query">The DBQuery to execute</param>
        /// <param name="transaction">The valid and active transaction to execute under</param>
        /// <returns>The resultant integer from the database query</returns>
        public int ExecuteNonQuery(DbTransaction transaction, DBQuery query)
        {
            return this.ExecuteNonQuery(transaction, query, null);
        }

        /// <summary>
        /// Generates a SQL Command from the DBQuery and execute this with the specified transaction, 
        /// returning the integer result.
        /// </summary>
        /// <param name="query">The DBQuery to execute</param>
        /// <param name="transaction">The valid and active transaction to execute under</param>
        /// <returns>The resultant integer from the database query</returns>
        public int ExecuteNonQuery(DbTransaction transaction, DBQuery query, DBOnErrorCallback onerror)
        {
            if (null == transaction)
                throw new ArgumentNullException("transaction");
            if (null == transaction.Connection)
                throw new ArgumentNullException("transaction.Connection");
            if (null == query)
                throw new ArgumentNullException("query");

            int returns;

            using (DbCommand cmd = this.CreateCommand(transaction, query))
            {
                returns = this.DoExecuteNonQuery(cmd, onerror);
            }

            return returns;
        }

        #endregion

        #region public int ExecuteNonQuery(string text) + 1 overload

        /// <summary>
        /// Executes a SQL string against this instances database, 
        /// returning the integer result
        /// </summary>
        /// <param name="text">The SQL string to execute</param>
        /// <returns>The resultant integer from the database execution</returns>
        public int ExecuteNonQuery(string text)
        {
            if (string.IsNullOrEmpty(text))
                throw new ArgumentNullException("text");

            return ExecuteNonQuery(text, CommandType.Text);
        }

        /// <summary>
        /// Executes a SQL string against this instances database, 
        /// returning the integer result
        /// </summary>
        /// <param name="text">The SQL string to execute</param>
        /// <returns>The resultant integer from the database execution</returns>
        public int ExecuteNonQuery(string text, DBOnErrorCallback onerror)
        {
            if (string.IsNullOrEmpty(text))
                throw new ArgumentNullException("text");

            return ExecuteNonQuery(text, CommandType.Text, onerror);
        }

        #endregion

        #region public int ExecuteNonQuery(string text, CommandType type) + 1 overload

        /// <summary>
        /// Executes a SQL string (of the specified type) against this instances database, 
        /// returning the integer result
        /// </summary>
        /// <param name="text">The SQL string to execute</param>
        /// <param name="type">The type of command the sql text represents</param>
        /// <returns>The resultant integer from the database execution</returns>
        public int ExecuteNonQuery(string text, CommandType type)
        {
            return ExecuteNonQuery(text, type, null);
        }

        /// <summary>
        /// Executes a SQL string (of the specified type) against this instances database, 
        /// returning the integer result
        /// </summary>
        /// <param name="text">The SQL string to execute</param>
        /// <param name="type">The type of command the sql text represents</param>
        /// <returns>The resultant integer from the database execution</returns>
        public int ExecuteNonQuery(string text, CommandType type, DBOnErrorCallback onerror)
        {
            if (string.IsNullOrEmpty(text))
                throw new ArgumentNullException("text");

            int result;

            using (DbCommand cmd = this.CreateCommand(text, type))
            {
                result = this.DoExecuteNonQuery(cmd, onerror);
            }

            return result;
        }

        #endregion

        #region public int ExecuteNonQuery(DbCommand cmd) + 1 overload

        /// <summary>
        /// Executes the DBCommand (which must include the correct connection) returning the integer result
        /// </summary>
        /// <param name="cmd">The SQL command to execute</param>
        /// <returns>The resultant integer from the database execution</returns>
        public int ExecuteNonQuery(DbCommand cmd)
        {
            return ExecuteNonQuery(cmd, null);
        }

        /// <summary>
        /// Executes the DBCommand (which must include the correct connection) returning the integer result
        /// </summary>
        /// <param name="cmd">The SQL command to execute</param>
        /// <param name="onerror">An error callback for any exceptions raised</param>
        /// <returns>The resultant integer from the database execution</returns>
        public int ExecuteNonQuery(DbCommand cmd, DBOnErrorCallback onerror)
        {
            if (null == cmd)
                throw new ArgumentNullException("cmd");
            if (null == cmd.Connection)
                throw new ArgumentNullException("cmd.Connection");

            int result = DoExecuteNonQuery(cmd, onerror);

            return result;
        }

        #endregion

        #region protected virtual int DoExecuteNonQuery(DbCommand cmd)

        /// <summary>
        ///  Virtual (overridable) execute non-query method accepting a command. All ExecuteNonQuery methods ultimately call this method
        /// </summary>
        /// <param name="cmd">The command to execute</param>
        /// <returns>The result of the DbCommand.ExecuteNonQuery method</returns>
        protected virtual int DoExecuteNonQuery(DbCommand cmd, DBOnErrorCallback onerror)
        {
            int result = 0;
            bool opened = false;
            Profile.IDBProfileExecData exec = null;

            try
            {
                if (cmd.Connection.State == ConnectionState.Closed)
                {
                    cmd.Connection.Open();
                    opened = true;
                }
                if (IsProfiling)
                    exec = ProfileBegin(cmd);

                result = cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                ReThrowAction action;
                this.HandleExecutionError(onerror, ref ex, out action);

                if (action == ReThrowAction.Original)
                    throw;
                else if (action == ReThrowAction.Wrapped)
                    throw ex;
                else if (action == ReThrowAction.None)
                    ; //consume it.
                else
                    throw new ArgumentOutOfRangeException("Unknown action to perform", ex);
            }
            finally
            {
                if (IsProfiling)
                    ProfileEnd(exec);

                if (opened)
                    cmd.Connection.Close();
            }
            return result;
        }

        #endregion

        //
        // CreateConnection
        //

        #region public virtual DbConnection CreateConnection()

        /// <summary>
        /// Creates a new DbConnection for the correct provider type with this instances connection string set. 
        /// The DbConnection is not open when returned
        /// </summary>
        /// <returns>A new DbConnection</returns>
        public virtual DbConnection CreateConnection()
        {
            DbConnection con = this.Factory.CreateConnection();
            con.ConnectionString = this.ConnectionString;
            return con;
        }

        #endregion

        //
        // create command methods
        //

        #region public DbCommand CreateCommand()

        /// <summary>
        /// Creates a new empty command with its Connection property set to a new initialiazed, but unopened connection
        /// </summary>
        /// <returns>The new command.</returns>
        /// <remarks>It is the responsibility of the caller to Dispose of the returned Command, however, 
        /// the inner connection will automatically be disposed when thecomand is disposed</remarks>
        public DbCommand CreateCommand()
        {
            DbConnection con = this.CreateConnection();
            DbCommand cmd = this.CreateCommand(con);
            cmd.Disposed += new EventHandler(DisposeCommandOwnedConnection);
            return cmd;
        }

        #endregion

        #region public DbCommand CreateCommand(DbConnection connection)

        /// <summary>
        /// Creates a new command with its connection property set to the specified connection.
        /// </summary>
        /// <param name="connection">The connection the new command should use.</param>
        /// <returns>A new command</returns>
        public DbCommand CreateCommand(DbConnection connection)
        {
            if (null == connection)
                throw new ArgumentNullException("connection");

            DbCommand cmd = this.Factory.CreateCommand();
            cmd.Connection = connection;
            return cmd;
        }

        #endregion

        #region public DbCommand CreateCommand(string text)

        /// <summary>
        /// Creates a new DbCommand of the correct provider type with an unopened DbConnection correctly set 
        /// along with the CommandText and a CommandType of Text
        /// </summary>
        /// <param name="text">The SQL Statement text to use in the command</param>
        /// <returns>The initialized DbCommand</returns>
        /// <remarks>It is the responsibility of the caller to Dispose of the returned Command, however, 
        /// the inner connection will automatically be disposed when thecomand is disposed</remarks>
        public DbCommand CreateCommand(string text)
        {
            return CreateCommand(text, CommandType.Text);
        }

        #endregion

        #region public DbCommand CreateCommand(string text, CommandType type)

        /// <summary>
        /// Creates a new DbCommand for this databases' provider with an (unopened) DbConnection correctly set
        /// along with the CommandText and CommandType.
        /// </summary>
        /// <param name="text">The SQL statement text to use in the command</param>
        /// <param name="type">Specifies the type of command the SQL statement is</param>
        /// <returns>The initialized DbCommand</returns>
        /// <remarks>It is the responsibility of the caller to Dispose of the returned Command, however, 
        /// the inner connection will automatically be disposed when the command is disposed</remarks>
        public DbCommand CreateCommand(string text, CommandType type)
        {
            if (string.IsNullOrEmpty(text))
                throw new ArgumentNullException("text");

            DbConnection con = this.CreateConnection();
            DbCommand cmd = this.CreateCommand(con, text);
            cmd.CommandType = type;
            cmd.Disposed += new EventHandler(DisposeCommandOwnedConnection);

            return cmd;
        }

        #endregion

        #region public DbCommand CreateCommand(DbTransaction transaction, string text)

        /// <summary>
        /// Creates a new DbCommand for this databases' provider with the DbConnection and DbTransaction correctly set
        /// along with the CommandText and a CommandType of Text.
        /// </summary>
        /// <param name="transaction">The transaction the new command should execute under</param>
        /// <param name="text">The SQL Statement text to execute</param>
        /// <returns>The initialized DbCommand</returns>
        /// <remarks>It is the responsibility of the caller to Dispose of the returned Command along with the transaction and its connection</remarks>
        public DbCommand CreateCommand(DbTransaction transaction, string text)
        {
            return CreateCommand(transaction, text, CommandType.Text);
        }

        #endregion

        #region public DbCommand CreateCommand(DbTransaction transaction, string text)

        /// <summary>
        /// Creates a new DbCommand for this databases' provider with the DbConnection and DbTransaction correctly set
        /// along with the CommandText and CommandType.
        /// </summary>
        /// <param name="transaction">The transaction the new command should execute under</param>
        /// <param name="text">The SQL Statement text to execute</param>
        /// <param name="type">Specifies the type of command the SQL statement is</param>
        /// <returns>The initialized DbCommand</returns>
        /// <remarks>It is the responsibility of the caller to Dispose of the returned Command along with the transaction and its connection</remarks>
        public DbCommand CreateCommand(DbTransaction transaction, string text, CommandType type)
        {
            if (null == transaction)
                throw new ArgumentNullException("transaction");
            if (string.IsNullOrEmpty(text))
                throw new ArgumentNullException("text");

            DbCommand cmd = this.Factory.CreateCommand();
            cmd.Connection = transaction.Connection;

            //check for owned transaction (from a call to BeginTransaction)
            if (transaction is DBOwningTransaction)
                cmd.Transaction = ((DBOwningTransaction)transaction).InnerTransaction;
            else
                cmd.Transaction = transaction;
            cmd.CommandText = text;
            cmd.CommandType = type;
            return cmd;
        }

        #endregion

        #region public DbCommand CreateCommand(DbConnection connection, string text)

        /// <summary>
        /// Creates a new DbCommand for this databases' provider with the specified connection correctly set
        /// along with the CommandText and a CommandType of Text.
        /// </summary>
        /// <param name="connection">The connection the new command use</param>
        /// <param name="text">The SQL Statement text to execute</param>
        /// <returns>The initialized DbCommand</returns>
        /// <remarks>It is the responsibility of the caller to Dispose of the returned Command along with the its connection</remarks>
        public DbCommand CreateCommand(DbConnection connection, string text)
        {
            return CreateCommand(connection, text, CommandType.Text);
        }

        #endregion

        #region public virtual DbCommand CreateCommand(DbConnection connection, string text, CommandType type)

        /// <summary>
        /// Creates a new DbCommand for this databases' provider with the specified connection correctly set
        /// along with the CommandText and CommandType.
        /// </summary>
        /// <param name="connection">The connection the new command use</param>
        /// <param name="text">The SQL Statement text to execute</param>
        /// <param name="type">Specifies the type of command the SQL statement is</param>
        /// <returns>The initialized DbCommand</returns>
        /// <remarks>It is the responsibility of the caller to Dispose of the returned Command along with the its connection</remarks>
        public virtual DbCommand CreateCommand(DbConnection connection, string text, CommandType type)
        {
            DbCommand cmd = this.Factory.CreateCommand();
            cmd.Connection = connection;
            cmd.CommandText = text;
            cmd.CommandType = type;
            return cmd;
        }

        #endregion

        #region public DbCommand CreateCommand(DBQuery query)

        /// <summary>
        /// Creates a new DbCommand and fully populates with the (provider specific) generated query statement along with the command type and connection
        /// </summary>
        /// <param name="query">The DBQuery to use for the command</param>
        /// <returns>The fully populated command</returns>
        /// <remarks>It is the responsibility of the caller to Dispose of the returned Command, however, 
        /// the inner connection will automatically be disposed when the command is disposed</remarks>
        public DbCommand CreateCommand(DBQuery query)
        {
            DbConnection con = this.CreateConnection();
            DbCommand cmd = this.CreateCommand(con, query);
            cmd.Disposed += new EventHandler(DisposeCommandOwnedConnection);
            return cmd;
        }

        #endregion

        #region public DbCommand CreateCommand(DbTransaction transaction, DBQuery query)

        /// <summary>
        /// Creates a new DbCommand and fully populates with the (provider specific) generated query statement along with the command type, connection and transaction
        /// </summary>
        /// <param name="transaction">The DbTransaction the cammand should execute under</param>
        /// <param name="query">The DBQuery to use for the command</param>
        /// <returns>The fully populated command</returns>
        /// <remarks>It is the responsibility of the caller to Dispose of the returned Command along with the its connection and transaction</remarks>
        public DbCommand CreateCommand(DbTransaction transaction, DBQuery query)
        {
            if (null == transaction)
                throw new ArgumentNullException("transaction");

            DbCommand cmd = this.CreateCommand(transaction.Connection, query);

            if (transaction is DBOwningTransaction)
                cmd.Transaction = ((DBOwningTransaction)transaction).InnerTransaction;
            else
                cmd.Transaction = transaction;

            return cmd;
        }

        #endregion

        #region public virtual DbCommand CreateCommand(DbConnection connection, DBQuery query)

        /// <summary>
        /// Creates a new DbCommand and fully populates with the (provider specific) generated query statement along with the command type and connection
        /// </summary>
        /// <param name="connection">The DbConnection the command should execute under</param>
        /// <param name="query">The DBQuery to use for the command</param>
        /// <returns>The fully populated command</returns>
        /// <remarks>It is the responsibility of the caller to Dispose of the returned Command along with the its connection</remarks>
        public virtual DbCommand CreateCommand(DbConnection connection, DBQuery query)
        {
            if (null == query)
                throw new ArgumentNullException("query");
            if (null == connection)
                throw new ArgumentNullException("connection");

            DbCommand cmd;

            using (DBStatementBuilder sb = this.CreateStatementBuilder())
            {
                query.BuildStatement(sb);
                string cmdtext = sb.ToString().Trim();
                cmd = this.Factory.CreateCommand();
                cmd.Connection = connection;
                cmd.CommandText = cmdtext;
                cmd.CommandType = query.GetCommandType();

                if (sb.HasParameters)
                {
                    foreach (DBStatementBuilder.StatementParameter sparam in sb.GetPassingParameters())
                    {
                        DbParameter param = sb.CreateCommandParameter(cmd, sparam);
                        cmd.Parameters.Add(param);
                    }
                }
            }

            return cmd;
        }

        #endregion

        //
        // populate command and get command text
        //

        #region public void PopulateCommand(DbCommand cmd, DBQuery query)

        /// <summary>
        /// Populates the specified command with the sql string an parameters generated from the Query.
        /// The connection will not be set, only the CommandText, CommandType and any parameters
        /// </summary>
        /// <param name="cmd">The command to populate</param>
        /// <param name="query">The Query to populate the command with</param>
        public void PopulateCommand(DbCommand cmd, DBQuery query)
        {
            using (DBStatementBuilder sb = this.CreateStatementBuilder())
            {
                query.BuildStatement(sb);
                string text = sb.ToString().Trim();
                cmd.CommandText = text;
                cmd.CommandType = query.GetCommandType();

                if (sb.HasParameters)
                {
                    foreach (DBStatementBuilder.StatementParameter sparam in sb.GetPassingParameters())
                    {
                        DbParameter param = sb.CreateCommandParameter(cmd, sparam);
                        cmd.Parameters.Add(param);
                    }
                }
            }
        }

        #endregion

        #region public string GetCommandText(DBQuery query) + 1 overload

        /// <summary>
        /// Returns the implementation specific SQL text for the specified query
        /// </summary>
        /// <param name="statement">The query to get the text for</param>
        /// <returns>The implementation specific string</returns>
        public string GetCommandText(DBStatement statement)
        {
            if (null == statement)
                throw new ArgumentNullException("query");

            using (DBStatementBuilder sb = this.CreateStatementBuilder())
            {
                statement.BuildStatement(sb);
                string text = sb.ToString().Trim();
                return text;
            }
        }

        /// <summary>
        /// Returns the implementation specific SQL text for the specified query and also populates any parameters in the query
        /// </summary>
        /// <param name="statement">The DBQuery to get the text for</param>
        /// <param name="parameters">Set to a list of any parameters used in teh query</param>
        /// <returns>The implementation specific SQL text</returns>
        public string GetCommandText(DBStatement statement, out List<DbParameter> parameters)
        {
            if (null == statement)
                throw new ArgumentNullException("query");

            parameters = new List<DbParameter>();

            using (DBStatementBuilder sb = this.CreateStatementBuilder())
            {
                statement.BuildStatement(sb);
                string text = sb.ToString().Trim();

                if (sb.HasParameters)
                {
                    foreach (DBStatementBuilder.StatementParameter sparam in sb.GetPassingParameters())
                    {

                        DbParameter param = this.Factory.CreateParameter();
                        sb.PopulateParameter(param, sparam);
                        parameters.Add(param);
                    }
                }

                return text;
            }
        }

        #endregion

        //
        // begin transaction
        //

        #region public DbTransaction BeginTransaction()

        /// <summary>
        /// Creates a new DbTransaction along with a new connection for the transaction.
        /// </summary>
        /// <returns>The newly created DbTransaction</returns>
        /// <remarks>It is the responsibility of the caller to dispose of the transaction, however,
        /// the created transaction will automatically dispose of its owned connection.<br/>
        /// NOTE: The actual transaction will not be directly settable onto any DbCommand. 
        /// Use the DBDatabase.CreateCommand(...) methods to use this transaction in a command.</remarks>
        public virtual DbTransaction BeginTransaction()
        {
            DbConnection con = this.CreateConnection();
            return new DBOwningTransaction(con);
        }

        #endregion


        //
        // add command parameter
        //

        #region public DbParameter AddCommandParameter(DbCommand cmd, string genericParameterName, DbType dbType)
        /// <summary>
        /// Adds and returns a new DbParameter of the correct type with the generic name and type. Set as an input parameter
        /// </summary>
        /// <param name="cmd">The command to add the parameter to</param>
        /// <param name="genericParameterName">The generic name (without an implmentation specific prefix)</param>
        /// <param name="dbType">The DbType of the parameters value.</param>
        /// <returns>The parameter that was added to the command</returns>
        public DbParameter AddCommandParameter(DbCommand cmd, string genericParameterName, DbType dbType)
        {
            return AddCommandParameter(cmd, genericParameterName, dbType, ParameterDirection.Input);
        }

        #endregion

        #region public DbParameter AddCommandParameter(DbCommand cmd, string genericParameterName, DbType dbType, ParameterDirection direction)

        /// <summary>
        /// Adds and returns a new DbParameter of the correct type with the generic name and type.
        /// </summary>
        /// <param name="cmd">The command to add the parameter to</param>
        /// <param name="genericParameterName">The generic name (without an implmentation specific prefix)</param>
        /// <param name="dbType">The DbType of the parameters value.</param>
        /// <param name="direction">The direction of the parameter.</param>
        /// <returns>The parameter that was added to the command</returns>
        public virtual DbParameter AddCommandParameter(DbCommand cmd, string genericParameterName,
                                               DbType dbType, ParameterDirection direction)
        {
            if (null == cmd)
                throw new ArgumentNullException("cmd");
            if (string.IsNullOrEmpty(genericParameterName))
                throw new ArgumentNullException("genericParameterName");

            DbParameter p = cmd.CreateParameter();
            if (cmd.CommandType == CommandType.StoredProcedure
                && String.Equals(this.ProviderName, "System.Data.OracleClient", StringComparison.OrdinalIgnoreCase))
                p.ParameterName = genericParameterName;
            else
                p.ParameterName = this.GetParameterName(genericParameterName);
            p.DbType = dbType;
            p.Direction = direction;
            cmd.Parameters.Add(p);
            return p;
        }

        #endregion

        #region public DbParameter AddCommandParameter(DbCommand cmd, string genericParameterName, DbType dbType, int size)

        /// <summary>
        /// Adds and returns a new DbParameter of the correct type with the generic name and type.
        /// </summary>
        /// <param name="cmd">The command to add the parameter to</param>
        /// <param name="genericParameterName">The generic name (without an implmentation specific prefix)</param>
        /// <param name="dbType">The DbType of the parameters value.</param>
        /// <param name="size">The size / max length of the parameter's value</param>
        /// <returns>The parameter that was added to the command</returns>
        public DbParameter AddCommandParameter(DbCommand cmd, string genericParameterName,
                                               DbType dbType, int size)
        {
            return AddCommandParameter(cmd, genericParameterName, dbType, size, ParameterDirection.Input);
        }

        #endregion

        #region public DbParameter AddCommandParameter(DbCommand cmd, string genericParameterName, DbType dbType, int size, ParameterDirection direction)

        /// <summary>
        /// Adds and returns a new DbParameter of the correct type with the generic name and type.
        /// </summary>
        /// <param name="cmd">The command to add the parameter to</param>
        /// <param name="genericParameterName">The generic name (without an implmentation specific prefix)</param>
        /// <param name="dbType">The DbType of the parameters value.</param>
        /// <param name="size">The size / max length of the parameter's value</param>
        /// <param name="direction">The direction of the parameter.</param>
        /// <returns>The parameter that was added to the command</returns>
        public DbParameter AddCommandParameter(DbCommand cmd, string genericParameterName,
                                               DbType dbType, int size, ParameterDirection direction)
        {
            if (null == cmd)
                throw new ArgumentNullException("cmd");

            if (string.IsNullOrEmpty(genericParameterName))
                throw new ArgumentNullException("genericParameterName");

            DbParameter p = cmd.CreateParameter();
            p.ParameterName = this.GetParameterName(genericParameterName);
            p.DbType = dbType;
            p.Direction = direction;
            p.Size = size;
            cmd.Parameters.Add(p);
            return p;
        }

        #endregion

        //
        // assign parameter value
        //

        #region public void AssignParameterValue(DbParameter param, object value)
        /// <summary>
        /// Assigns the value of the parameter to the specified value
        /// </summary>
        /// <param name="param"></param>
        /// <param name="value"></param>
        public void AssignParameterValue(DbParameter param, object value)
        {
            if (null == param)
                throw new ArgumentNullException("param");

            param.Value = value;

        }

        #endregion

        #region public void AssignParameterValue(DbCommand cmd, string genericParameterName, object value)
        /// <summary>
        /// Assigns the value of a parameter which was created by this DBDatabase with the specified generic name. Throws an exception if a matching parameter does not exist.
        /// </summary>
        /// <param name="cmd">The command that contains the parameter</param>
        /// <param name="genericParameterName">The generic name that was used to create the parameter</param>
        /// <param name="value">The value to assign to the parameter.</param>
        public void AssignParameterValue(DbCommand cmd, string genericParameterName, object value)
        {
            if (null == cmd)
                throw new ArgumentNullException("cmd");

            DbParameter p = cmd.Parameters[GetParameterName(genericParameterName)];

            if (null == p)
                throw new NullReferenceException(string.Format(Errors.NoParameterWithGenericName, genericParameterName));

            p.Value = value;
        }

        #endregion

        //
        // profiler methods
        //

        #region public void AttachProfiler(IDBProfiler profiler, bool start)

        /// <summary>
        /// Attaches a (non null) profiler to this DBDatabase and optionally starts the profiler
        /// </summary>
        /// <param name="profiler">The profiler to attatch</param>
        /// <param name="start">True if profiling should be started on this instance</param>
        public void AttachProfiler(IDBProfiler profiler, bool start)
        {
            if (null == profiler)
                throw new ArgumentNullException("profiler");

            if (this._profiler != null)
                throw new InvalidOperationException(Errors.ProfilerAlreadyAttached);

            this._profiler = profiler;
            this._isprofiling = start;
        }

        #endregion

        #region public IDBProfiler DetatchProfiler()

        /// <summary>
        /// Detatches and returns the current profiler (if any). And stops the profiling
        /// </summary>
        /// <returns></returns>
        public IDBProfiler DetatchProfiler()
        {
            if (_isprofiling)
                _isprofiling = false;
            IDBProfiler prof = this._profiler;
            this._profiler = null;

            return prof;
        }

        #endregion

        #region public void StartProfiling()

        /// <summary>
        /// Starts the profiling of this DBDatabase. NOTE: There must already be a profiler attached
        /// </summary>
        public void StartProfiling()
        {
            if (null == this._profiler)
                throw new NullReferenceException("No profiler to start, set the current profiler with the AttachProfiler method");

            this._isprofiling = true;
        }

        #endregion

        #region public void StopProfiling(bool dump)

        /// <summary>
        /// Stops the profiling of this database, and optionally asks the profiler to dump all execution summaries
        /// </summary>
        /// <param name="dump"></param>
        public void StopProfiling(bool dump)
        {
            if (null != _profiler)
            {
                if (dump)
                    _profiler.DumpExecutionSummary();
            }
            _isprofiling = false;
        }

        #endregion

        #region protected int ProfileBegin(DbCommand cmd)
        /// <summary>
        /// Registers the start of an execution with the profiler
        /// </summary>
        /// <param name="cmd">The command to execute</param>
        /// <returns>The data associated with the registered execution</returns>
        protected Profile.IDBProfileExecData ProfileBegin(DbCommand cmd)
        {
            object[] param = GetParamValues(cmd);
            return ProfileBegin(cmd.CommandText, param);
        }

        #endregion

        #region protected Profile.ProfilerExecData ProfileBegin(string sql, object[] param)
        /// <summary>
        /// Registers the start of an execution with the profiler
        /// </summary>
        /// <param name="sql">The sql text that will be executed</param>
        /// <param name="param">Any parameters in the execution</param>
        /// <returns>The data associated with the registered execution</returns>
        protected Profile.IDBProfileExecData ProfileBegin(string sql, object[] param)
        {
            if (null == _profiler)
                throw new InvalidOperationException("No profiler attached. Only call this method if there is a profiler attached and we are profiling");//should 
            if (!_isprofiling)
                throw new InvalidOperationException("Not profiling. Only call this method if there is a profiler attached and we are profiling");//should 

            return _profiler.BeginExecution(this.Name, sql, param);
        }

        #endregion

        #region protected void ProfileEnd(IDBProfileExecData execid)

        /// <summary>
        /// Registers the end of an execution with the profiler - passing the data returned from the Begin methods
        /// </summary>
        /// <param name="exec">Any data that was returned from ProfileBegin</param>
        protected void ProfileEnd(Profile.IDBProfileExecData exec)
        {
            if (null == _profiler)
                return; //ignore null execs - failure to register at the start. 
            if (!_isprofiling)
                throw new InvalidOperationException("Not profiling. Only call this method if there is a profiler attached and we are profiling");//should 
            _profiler.EndExecution(exec);
        }

        #endregion

        //
        // support methods
        //



        #region protected virtual void HandleExecutionError(Exception ex)

        /// <summary>
        /// Method called if there was an error generated dring the execution of a database command.
        /// </summary>
        /// <param name="ex">The exception that occurred</param>
        /// <param name="onerror">Any error handler</param>
        /// <param name="rethrow">Set to the action to undertake</param>
        /// <remarks>The default behaviour for this instance is to rethrow the exception.
        /// However if the compilation constant 'WRAP_EXCEPTIONS' is defined then all errors that occur 
        /// during execution will be wrapped in a generic exception message.<br/>
        /// Inheritors can also override this method to provide their own error handling</remarks>
        protected virtual void HandleExecutionError(DBOnErrorCallback onerror, ref Exception ex, out ReThrowAction rethrow)
        {
            DBConfigurationSection section = Configuration.DBConfigurationSection.GetSection();

            DBExceptionEventArgs args = new DBExceptionEventArgs(ex);
            if (onerror != null)
                onerror(args);

            //if the onerror callaback has not handled it then check the HandleException event
            if (!args.Handled)
                this.OnHandleException(args);

            if (args.Handled)
            {
                rethrow = ReThrowAction.None;
                ex = null;
            }
            else if (section.WrapExceptions)
            {
                rethrow = ReThrowAction.Wrapped;
                ex = new DataException(args.Message, ex);
            }
            else
                rethrow = ReThrowAction.Original;

        }

        #endregion

        #region protected string[] ExtractTableNames(DataSet ds)
        /// <summary>
        /// Creates an array of all the names of the tables in the dataset in the same order
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        protected string[] ExtractTableNames(DataSet ds)
        {
            if (null == ds)
                throw new ArgumentNullException("ds");

            List<string> all = new List<string>(ds.Tables.Count);
            foreach (DataTable dt in ds.Tables)
            {
                all.Add(dt.TableName);
            }
            return all.ToArray();
        }

        #endregion

        #region private static void DisposeCommandOwnedConnection(object sender, EventArgs e)

        /// <summary>
        /// Static event handler method to dispose of a connection when a DbCommand is disposed of
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void DisposeCommandOwnedConnection(object sender, EventArgs e)
        {
            DbCommand cmd = (DbCommand)sender;
            try
            {
                if (cmd.Connection != null)
                {
                    //System.Diagnostics.Trace.WriteLine("Disposing owned connection for command created by DBDatabase");
                    cmd.Connection.Dispose();

                }
            }
            catch { }
        }

        #endregion

        #region protected virtual DBDatabaseProperties CreateProperties()
        /// <summary>
        /// Creates a new instance of the Database properties
        /// </summary>
        /// <returns></returns>
        protected virtual DBDatabaseProperties CreateProperties()
        {
            DBProviderImplementation factory = DBProviderImplementation.GetImplementation(this.ProviderName);
            DBDatabaseProperties properties = factory.CreateDatabaseProperties(this);

            return properties;
        }

        #endregion

        #region private string GetParameterName(string genericname)

        /// <summary>
        /// Returns an database engine specific parameter for the generic name (e.g. @param1 for MS SQLServer)
        /// </summary>
        /// <param name="genericname">The generic name of the parameter (e.g. param1)</param>
        /// <returns>The specific name</returns>
        private string GetParameterName(string genericname)
        {
            if (string.IsNullOrEmpty(genericname))
                throw new ArgumentNullException("genericname");

            return string.Format(this.GetProperties().ParameterFormat, genericname);
        }

        #endregion

        #region public virtual DBStatementBuilder CreateStatementBuilder()

        /// <summary>
        /// Creates the statement builder for this database connection
        /// </summary>
        /// <returns></returns>
        public virtual DBStatementBuilder CreateStatementBuilder()
        {
            DBProviderImplementation factory = DBProviderImplementation.GetImplementation(this.ProviderName);
            DBStatementBuilder builder = factory.CreateStatementBuilder(this);

            return builder;
        }

        #endregion

        #region protected virtual DBSchemaProvider CreateSchemaProvider()

        /// <summary>
        /// Creates a new DBSchemaProvider for this database conection
        /// </summary>
        /// <returns></returns>
        protected virtual DBSchemaProvider CreateSchemaProvider()
        {
            DBProviderImplementation factory = DBProviderImplementation.GetImplementation(this.ProviderName);
            DBSchemaProvider schema = factory.CreateSchemaProvider(this);

            return schema;
        }

        #endregion

        #region protected void CreateEmptyTables(DataSet ds, string[] tables)

        /// <summary>
        /// Adds the range of empty tables with the correct names as specified to the dataset
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="tables"></param>
        protected void CreateEmptyTables(DataSet ds, string[] tables)
        {
            if (null == tables)
                return;
            for (int i = 0; i < tables.Length; i++)
            {
                string table = tables[i];
                if (string.IsNullOrEmpty(table))
                    throw new ArgumentNullException("tables[" + i.ToString() + "]");
                DataTable dt = new DataTable(table);
                ds.Tables.Add(dt);
            }
        }

        #endregion

        #region private static object[] GetParamValues(DbCommand cmd)

        /// <summary>
        /// Extracts all the parameter values from a command and returns as an object array
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        private static object[] GetParamValues(DbCommand cmd)
        {
            if (cmd.Parameters != null && cmd.Parameters.Count > 0)
            {
                int count = cmd.Parameters.Count;
                object[] all = new object[count];
                for (int i = 0; i < count; i++)
                {
                    DbParameter p = cmd.Parameters[i];
                    all[i] = p.Value;
                }
                return all;
            }
            else
                return new object[] { };
        }

        #endregion

        #region private static IDBProfiler GetConfiguredProfiler(out bool autostart) + 1 overload

        /// <summary>
        /// Loads and returns any configured profilers - from the default config section path, setting autostart from the config section too.
        /// </summary>
        /// <param name="autostart">Set to true if the profiler should be started automatically</param>
        /// <param name="dbName">The name of the database in the connection</param>
        /// <returns>Any IDBProfiler(s) defined in the config section</returns>
        private static IDBProfiler GetConfiguredProfiler(string dbName, out bool autostart)
        {
            string section = DBConfigurationSection.DBConfigSectionElement;

            DBConfigurationSection config = System.Configuration.ConfigurationManager.GetSection(section) as DBConfigurationSection;

            DBProfilerConfigSection sect = null;
            if (null != config)
                sect = config.Profilers;

            autostart = false;

            if (null == sect || !sect.HasProfilers)
            {
                return null;// non defined
            }
            else
            {
                autostart = sect.AutoStart;
                IDBProfilerFactory[] all = sect.GetProfilerFactories();
                if (all.Length == 1)
                    return all[0].GetProfiler(dbName);
                else
                {
                    List<IDBProfiler> profilers = new List<IDBProfiler>(all.Length);
                    foreach (IDBProfilerFactory fact in all)
                    {
                        profilers.Add(fact.GetProfiler(dbName));
                    }
                    return new Profile.DBCollectionProfiler(dbName, profilers);
                }

            }
        }

        #endregion

        //
        // support classes
        //


        #region private class DBOwningTransaction : DbTransaction

        /// <summary>
        /// Transaction wrapper class to close the connection 
        /// when the transaction is disposed.
        /// </summary>
        private class DBOwningTransaction : DbTransaction
        {
            private DbConnection _con;
            private bool _opened;
            private bool _active;
            private DbTransaction _innerTranny;

            public DbTransaction InnerTransaction
            {
                get { return this._innerTranny; }
            }

            public DBOwningTransaction(DbConnection ownedConnection)
            {
                this._con = ownedConnection;
                if (_con == null)
                    throw new ArgumentException("Cannot create an transaction without a valid connection");
                if (_con.State == System.Data.ConnectionState.Closed)
                {
                    _con.Open();
                    _opened = true;
                }
                else
                    _opened = false;

                this._innerTranny = _con.BeginTransaction();
                this._active = true;
            }

            public override void Commit()
            {
                this._innerTranny.Commit();
                this._active = false;
            }

            protected override DbConnection DbConnection
            {
                get { return this._con; }
            }

            public override System.Data.IsolationLevel IsolationLevel
            {
                get { return this._innerTranny.IsolationLevel; }
            }

            public override void Rollback()
            {
                this._innerTranny.Rollback();
                this._active = false;
            }

            protected override void Dispose(bool disposing)
            {
                if (disposing)
                {
                    if (_innerTranny != null)
                    {
                        //this transaction has not been committed
                        //or rolled back so default behaviour is
                        //to rollback, should happen when the
                        //inner transaction is disposed, but just
                        //to make sure do it now
                        if (_active)
                            _innerTranny.Rollback();

                        _innerTranny.Dispose();
                    }
                    if (_opened)
                        _con.Close();
                }

                base.Dispose(disposing);
            }
        }

        #endregion


        //
        // static factory methods
        //

        #region public static DBDataBase Create(string connectionStringName)

        /// <summary>
        /// Creates a new DBDatabase using the ConnectionStrings values set in the configuration file with the name specified
        /// </summary>
        /// <param name="connectionStringName">The name of the connection strings section to get the connection details from</param>
        /// <returns>A new DBDatabase</returns>
        public static DBDatabase Create(string connectionStringName)
        {
            if (string.IsNullOrEmpty(connectionStringName))
                throw new ArgumentNullException("connectionStringName", Errors.NoConnectionStringName);

            System.Configuration.ConnectionStringSettings settings = System.Configuration.ConfigurationManager.ConnectionStrings[connectionStringName];

            if (settings == null)
                throw new ArgumentNullException("connectionStringName", String.Format(Errors.NoConfiguredConnectionWithName, connectionStringName));

            string con = settings.ConnectionString;
            string prov = settings.ProviderName;

            return Create(connectionStringName, con, prov);
        }

        #endregion

        #region public static DBDataBase Create(string connectionString, string providername)

        /// <summary>
        /// Creates a new DBDatabase with the specified connectionString and provider name
        /// </summary>
        /// <param name="connectionString">The connection string to the required SQL Database</param>
        /// <param name="providername">The name of a valid DBProviderFactory to use when creating connections, commands etc.</param>
        /// <returns>A new DBDatabase</returns>
        public static DBDatabase Create(string connectionString, string providername)
        {
            return Create(null, connectionString, providername);
        }

        #endregion

        #region public static DBDatabase Create(string name, string connectionString, string providername)

        /// <summary>
        /// Creates a new DBDatabase with the specified connectionString and provider name
        /// </summary>
        /// <param name="name">An identifiable name for the DBDatabase</param>
        /// <param name="connectionString">The connection string to the required SQL Database</param>
        /// <param name="providername">The name of a valid DBProviderFactory to use when creating connections, commands etc.</param>
        /// <returns>A new DBDatabase</returns>
        public static DBDatabase Create(string name, string connectionString, string providername)
        {
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentNullException("connectionString", String.Format(Errors.ConnectionStringNotSet));

            if (string.IsNullOrEmpty(providername))
                throw new ArgumentNullException("providername", String.Format(Errors.ProviderNameNotSet));

            System.Data.Common.DbProviderFactory fact = System.Data.Common.DbProviderFactories.GetFactory(providername);

            if (null == fact)
                throw new ArgumentNullException("providername", String.Format(Errors.InvalidProviderNameForConnection, providername));

            DBProviderImplementation datafact = DBProviderImplementation.GetImplementation(providername);

            if (null == datafact)
                throw new ArgumentNullException("providername", String.Format(Errors.InvalidProviderNameForDBFactory, providername));

            DBDatabase db = datafact.CreateDatabase(connectionString, providername, fact);

            //set the name of this instance (can be null)
            db.Name = name;

            //check the profilers and and attach any that are defined
            bool autostart;
            IDBProfiler configprofilers = GetConfiguredProfiler(name, out autostart);

            if (null != configprofilers)
                db.AttachProfiler(configprofilers, autostart);

            //finally return the instance
            return db;
        }

        #endregion



    }
}
