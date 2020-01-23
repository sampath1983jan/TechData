using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
using System.Data;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Microsoft.CSharp;
using System.Reflection;
using Microsoft.VisualBasic;

namespace System.Data
{
  public class DataModel
    {
        private List<ModelRelation> Relations;
        private DataSet mSchema;
        public List<SchemaError> SchemaErrors;
        public List<ModelField> Fields;
        public List<string> Tables;
        private DataTable BaseTable;
        public DataModel(DataTable dt)
        {
            BaseTable = new DataTable();
            BaseTable = dt;
            Relations = new List<ModelRelation>();
            mSchema = new DataSet();
            Fields = new List<ModelField>();
            SchemaErrors = new List<SchemaError>();
            this.mSchema.Tables.Add(dt);
            this.addRelation(new ModelRelation(BaseTable.TableName, "", "", ""));
        }
        public DataModel Select(params ModelField[] fields) {
            this.addField(fields);
            return this;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="fieldName"></param>
        /// <param name="joinFieldName"></param>
        /// <returns></returns>
        public   DataModel Join(DataTable dt, string fieldName,string joinFieldName) {
            this.addRelation(new ModelRelation(BaseTable.TableName, dt.TableName, fieldName, joinFieldName));
            BaseTable = dt;
            this.mSchema.Tables.Add(dt);
            return this;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="baseDT"></param>
        /// <param name="JoinData"></param>
        /// <param name="fieldName"></param>
        /// <param name="joinFieldName"></param>
        /// <returns></returns>
        public DataModel Join(DataTable baseDT, DataTable JoinData, string fieldName, string joinFieldName) {
            this.addRelation(new ModelRelation(baseDT.TableName, JoinData.TableName, fieldName, joinFieldName));
            BaseTable = baseDT;
            this.mSchema.Tables.Add(baseDT);
            this.mSchema.Tables.Add(JoinData);
            return this;
        }
        private DataModel addRelation(ModelRelation relation)
        {
            Relations.Add(relation);         
            return this;
        }

        private DataModel addField(params ModelField[] fields)
        {
            for (int ifel = 0; ifel < fields.Length; ifel++)
            {
                Fields.Add(fields[ifel]);
            }
            return this;
        }
        private bool isTableExist(string tb)
        {
            if (tb == "")
            {
                return true;
            }
            for (int j = 0; j < this.Tables.Count; j++)
            {
          
                if (this.Tables[j] == tb)
                {
                    return true;
                }
            }
            return false;
        }
        public object getResult()
        {
            //DataSet ds;
            //List<SchemaRelation> rela;
            //DynamicSchema dy = new DynamicSchema(ds,rela);
            //dy.addRelation(new SchemaRelation("", "", "", ""), new SchemaRelation("", "", "", ""));
            this.Tables = new List<string>();
            for (int i = 0; i < this.Relations.Count; i++)
            {
                ModelRelation sr = new ModelRelation();
                sr = this.Relations[i];
                if (!isTableExist(sr.BaseSchema))
                {
                    this.Tables.Add(sr.BaseSchema);
                }
                if (!isTableExist(sr.RelatedSchema))
                {
                    this.Tables.Add(sr.RelatedSchema);
                }
            }
            CSharpCodeProvider c = new CSharpCodeProvider();
            ICodeCompiler icc = c.CreateCompiler();
            CompilerParameters cp = new CompilerParameters();
             


            cp.ReferencedAssemblies.Add("system.dll");
            cp.ReferencedAssemblies.Add("system.xml.dll");
            cp.ReferencedAssemblies.Add("system.data.dll");
            cp.ReferencedAssemblies.Add("system.windows.forms.dll");
            cp.ReferencedAssemblies.Add("system.drawing.dll");
            cp.ReferencedAssemblies.Add("System.Collections.dll");
            cp.ReferencedAssemblies.Add("System.ComponentModel.dll");
            cp.ReferencedAssemblies.Add("System.Data.DataSetExtensions.dll");
            cp.ReferencedAssemblies.Add("System.Linq.dll");
            cp.ReferencedAssemblies.Add("mscorlib.dll");
            cp.ReferencedAssemblies.Add("System.Threading.Tasks.dll");
            cp.ReferencedAssemblies.Add("System.Reflection.dll");
            cp.ReferencedAssemblies.Add("Microsoft.CSharp.dll");
            cp.ReferencedAssemblies.Add("System.Core.dll");
            cp.ReferencedAssemblies.Add(this.AssemblyPath() + "Mysql.Data.dll");
            // cp.ReferencedAssemblies.Add("ForziaUtilities.dll");

            cp.CompilerOptions = "/t:library";
            cp.GenerateInMemory = true;
            StringBuilder sb = new StringBuilder("");

            sb.Append("using System;\n");
            sb.Append("using System.Xml;\n");
            sb.Append("using System.Data.SqlClient;\n");
            sb.Append("using System.Windows.Forms;\n");
            sb.Append("using System.Drawing;\n");

            sb.Append("using System.Collections.Generic;\n");
            sb.Append("using System.ComponentModel;\n");
            sb.Append("using System.Data;\n");
            sb.Append("using System.Linq;\n");
            sb.Append("using System.Text;\n");
            sb.Append("using System.Threading.Tasks;\n");
            sb.Append("using System.Reflection;\n");
            sb.Append("using System.CodeDom.Compiler;\n");
            sb.Append("using System.Diagnostics;\n");
            sb.Append("using Microsoft.CSharp;\n");
            sb.Append("using MySql.Data.MySqlClient;\n");
            sb.Append("using MySql.Data;\n");


            sb.Append("namespace CodeEvalution{ \n");
            sb.Append("public class CodeEvalutor{ \n");
            sb.Append("public object EvalCode(DataSet ds){ \n");
            // sb.Append(sbCode + " \n");
            for (int itbl = 0; itbl < this.Tables.Count; itbl++)
            {
                sb.AppendFormat("DataTable dt_{0} = new DataTable();\n", this.Tables[itbl]);
                sb.AppendFormat("dt_{0} = ds.Tables[{1}] ;\n", this.Tables[itbl], "\"" + this.Tables[itbl] + "\"");
            }

            for (int itbl = 0; itbl < this.Tables.Count; itbl++)
            {
                if (itbl == 0)
                {
                    sb.AppendFormat("var query = from {0} in dt_{0}.AsEnumerable()\n", Tables[itbl]);
                }
                else
                {
                    sb.AppendFormat(" from {0} in dt_{0}.AsEnumerable()\n", Tables[itbl]);
                }
            }

            for (int i = 0; i < this.Relations.Count; i++)
            {
                ModelRelation sr = new ModelRelation();
                sr = this.Relations[i];
                if (i == 0)
                {
                    sb.Append(" .Where (mapping => ");
                    continue;
                }
                else
                {
                    sb.AppendFormat("mapping.Field<{3}>({4})  == {0}.Field<{1}>({2})", sr.BaseSchema, getColumnFieldType(this.mSchema.Tables[sr.BaseSchema].Columns[sr.BaseSchemaField]), "\"" + sr.BaseSchemaField + "\"", getColumnFieldType(this.mSchema.Tables[sr.RelatedSchema].Columns[sr.RelatedSchemaField]), "\"" + sr.RelatedSchemaField + "\"");
                }
                if (i != this.Relations.Count - 1)
                {
                    sb.Append(" && \n");
                }
                else
                {
                    sb.Append(" ).DefaultIfEmpty() \n");
                }

                ////////////////////////////////////////// direct relationship commented due to left join missing /sampathkumar.A
                //SchemaRelation sr= new SchemaRelation() ;
                //sr=this.Relations[i];
                //if (i == 0) { 
                //sb.Append(" where ");
                //}

                //sb.AppendFormat("{0}.Field<{1}>({2}) == {3}.Field<{4}>({5})\n", this.Relations[i].BaseSchema, getColumnFieldType(this.mSchema.Tables[sr.BaseSchema].Columns[sr.BaseSchemaField]), "\"" + sr.BaseSchemaField + "\"", sr.RelatedSchema, getColumnFieldType(this.mSchema.Tables[sr.RelatedSchema].Columns[sr.RelatedSchemaField]), "\"" + sr.RelatedSchemaField + "\"");

                //if (i != this.Relations.Count -1)
                //{
                //    sb.Append(" && \n");
                //}
                //////////////////////////////////////////////////////

                // Unable to solve multiple field join in relation so that it is commented /sampathkumar.a

                //if (i == 0) {
                //    sb.AppendFormat("var query = from {0} in dt_{0}.AsEnumerable()\n",this.Relations[i].BaseSchema);
                //}


                //if (sr.IsRelated == false) {
                //    sb.AppendFormat("join {0} in dt_{0}.AsEnumerable() on {1}.Field<{4}>({2}) equals {0}.Field<{5}>({3})\n", sr.RelatedSchema,
                //       this.Relations[i].BaseSchema, "\"" + sr.BaseSchemaField + "\"", "\"" + sr.RelatedSchemaField + "\"", getColumnFieldType(this.mSchema.Tables[sr.BaseSchema].Columns[sr.BaseSchemaField]),
                //       getColumnFieldType(this.mSchema.Tables[sr.RelatedSchema].Columns[sr.RelatedSchemaField]));
                //    sr.IsRelated = true;
                //}
                //sb.Append(this.getMultiCondition(sr));
                //sb.Append (this.getChildRelation(sr));

            }
            sb.Append("select new {");
            for (int ifld = 0; ifld < this.Fields.Count; ifld++)
            {
                sb.AppendFormat("  {0} = ({3} == null)? {4} : {3}.Field<{1}>({2})", this.Fields[ifld].FieldName, getFieldType(this.Fields[ifld]),
                    "\"" + this.Fields[ifld].FieldName + "\"", this.Fields[ifld].Schema, getNullValue(this.Fields[ifld]));
                if (ifld != (this.Fields.Count - 1))
                {
                    sb.AppendFormat(",");
                }
            }

            sb.Append("};\n");
            sb.Append(" DataTable rst = new DataTable();\n");
            sb.Append(" rst = ConvertToDataTable(query);\n");
            sb.Append("return rst; \n");
            sb.Append("} \n");
            sb.Append("public DataTable ConvertToDataTable<T>(IEnumerable<T> varlist) \n");
            sb.Append("{ \n");
            sb.Append("DataTable dtReturn = new DataTable(); \n");
            // column names   
            sb.Append("PropertyInfo[] oProps = null; \n");
            sb.Append("if (varlist == null) return dtReturn; \n");
            sb.Append("foreach (T rec in varlist) \n");
            sb.Append("{ \n");
            sb.Append("if (oProps == null) \n");
            sb.Append("{ \n");
            sb.Append("oProps = ((Type)rec.GetType()).GetProperties(); \n");
            sb.Append("foreach (PropertyInfo pi in oProps) \n");
            sb.Append("{ \n");
            sb.Append("Type colType = pi.PropertyType; \n");

            sb.Append("if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition() == typeof(Nullable<>))) \n");
            sb.Append("{ \n");
            sb.Append("colType = colType.GetGenericArguments()[0]; \n");
            sb.Append("} \n");
            sb.Append("dtReturn.Columns.Add(new DataColumn(pi.Name, colType)); \n");
            sb.Append("} \n");
            sb.Append("} \n");
            sb.Append("DataRow dr = dtReturn.NewRow(); \n");
            sb.Append("foreach (PropertyInfo pi in oProps) \n");
            sb.Append("{ \n");
            sb.Append("dr[pi.Name] = pi.GetValue(rec, null) == null ? DBNull.Value : pi.GetValue \n");
            sb.Append("(rec, null); \n");
            sb.Append("} \n");
            sb.Append("dtReturn.Rows.Add(dr); \n");
            sb.Append("} \n");
            sb.Append("return dtReturn; \n");
            sb.Append("} \n");
            sb.Append("} \n");
            sb.Append("}\n");

            CompilerResults cr = icc.CompileAssemblyFromSource(cp, sb.ToString());
            if (cr.Errors.Count > 0)
            {
                for (int i = 0; i < cr.Errors.Count; i++)
                {
                    SchemaErrors.Add(new SchemaError(cr.Errors[i].ErrorText, cr.Errors[i].Line));
                    return null;
                }
            }

            System.Reflection.Assembly a = cr.CompiledAssembly;
            object o = a.CreateInstance("CodeEvalution.CodeEvalutor");
            Type t = o.GetType();
            MethodInfo mi = t.GetMethod("EvalCode");
            object[] arguments = new object[1];
            arguments[0] = this.mSchema;
            object s = mi.Invoke(o, arguments);
            return s;
        }
        private string getColumnFieldType(DataColumn dc)
        {
            string dtype = dc.DataType.ToString();
            dtype = dtype.Replace("System.", "");
            return dtype;
        }
        private string getNullValue(ModelField fld)
        {

            string flType = getFieldType(fld);
            if (flType.ToLower() == "string")
            {
                return "\"" + "" + "\"";
            }
            else if (flType.ToLower().IndexOf("int") >= 0 || flType.ToLower().IndexOf("double") >= 0 || flType.ToLower().IndexOf("decimal") >= 0 || flType.ToLower().IndexOf("float") >= 0 || flType.ToLower().IndexOf("currency") >= 0 || flType.ToLower().IndexOf("int") >= 0 || flType.ToLower().IndexOf("int") >= 0)
            {
                return "0";
            }
            else if (flType.ToLower().IndexOf("date") >= 0)
            {
                return DateTime.MinValue.ToString();
            }
            else if (flType.ToLower().IndexOf("bool") >= 0)
            {
                return "false";
            }
            else
            {
                return "\"" + "" + "\"";
            }
        }
        private string getFieldType(ModelField fld)
        {
            string dtype = this.mSchema.Tables[fld.Schema].Columns[fld.FieldName].DataType.ToString();
            dtype = dtype.Replace("System.", "");
            return dtype;
        }
        private StringBuilder getMultiCondition(ModelRelation sr)
        {
            StringBuilder sc = new StringBuilder();
            for (int j = 0; j < this.Relations.Count; j++)
            {
                ModelRelation tempsr = this.Relations[j];
                if (tempsr.IsRelated == false && ((tempsr.BaseSchema == sr.BaseSchema && tempsr.RelatedSchema == sr.RelatedSchema) || (tempsr.BaseSchema == sr.RelatedSchema && tempsr.RelatedSchema == sr.BaseSchema)))
                {
                    sc.AppendFormat(" and {0}.{1} equals {2}.{3} ", sr.BaseSchema, sr.BaseSchemaField, sr.RelatedSchema, sr.RelatedSchemaField);
                }
                sr.IsRelated = true;
            }
            return sc;
        }
        private StringBuilder getChildRelation(ModelRelation sr)
        {
            StringBuilder sc = new StringBuilder();

            for (int j = 0; j < this.Relations.Count; j++)
            {
                ModelRelation tempsr = this.Relations[j];
                if (tempsr.IsRelated == false && tempsr.BaseSchema == sr.RelatedSchema)
                {

                    sc.AppendFormat("join {0} in dt_{0}.AsEnumerable() on {1}.Field<{4}>({2}) equals {0}.Field<{5}>({3})\n", tempsr.RelatedSchema,
                      this.Relations[j].BaseSchema, "\"" + tempsr.BaseSchemaField + "\"", "\"" + tempsr.RelatedSchemaField + "\"",
                      getColumnFieldType(this.mSchema.Tables[tempsr.BaseSchema].Columns[tempsr.BaseSchemaField]),
                      getColumnFieldType(this.mSchema.Tables[tempsr.RelatedSchema].Columns[tempsr.RelatedSchemaField]));
                    tempsr.IsRelated = true;
                }
                //else if (tempsr.IsRelated == false && tempsr.BaseSchema == sr.BaseSchema)
                //{
                //    sc.AppendFormat("join {0} in dt_{0}.AsEnumerable() on {1}.Field<{4}>({2}) equals {0}.Field<{5}>({3})\n", tempsr.RelatedSchema,
                //         this.Relations[j].BaseSchema, tempsr.BaseSchemaField, tempsr.RelatedSchemaField, getColumnFieldType(this.mSchema.Tables[tempsr.BaseSchema].Columns[tempsr.BaseSchemaField]),
                //         getColumnFieldType(this.mSchema.Tables[tempsr.RelatedSchema].Columns[tempsr.RelatedSchemaField]));
                //    tempsr.IsRelated = true;
                //}
                //else if (tempsr.IsRelated == false && tempsr.RelatedSchema == sr.RelatedSchema)
                //{
                //    sc.AppendFormat("join {0} in dt_{0}.AsEnumerable() on {1}.Field<{4}>({2}) equals {0}.Field<{5}>({3})\n", tempsr.RelatedSchema,
                //         this.Relations[j].BaseSchema, tempsr.BaseSchemaField, tempsr.RelatedSchemaField,
                //         getColumnFieldType(this.mSchema.Tables[tempsr.BaseSchema].Columns[tempsr.BaseSchemaField]),
                //         getColumnFieldType(this.mSchema.Tables[tempsr.RelatedSchema].Columns[tempsr.RelatedSchemaField]));
                //    tempsr.IsRelated = true;
                //}
                //else if (tempsr.IsRelated == false && tempsr.RelatedSchema == sr.BaseSchema)
                //{
                //    sc.AppendFormat("join {0} in dt_{0}.AsEnumerable() on {1}.Field<{4}>({2}) equals {0}.Field<{5}>({3})\n", tempsr.RelatedSchema,
                //         this.Relations[j].BaseSchema, tempsr.BaseSchemaField, tempsr.RelatedSchemaField, getColumnFieldType(this.mSchema.Tables[tempsr.BaseSchema].Columns[tempsr.BaseSchemaField]),
                //         getColumnFieldType(this.mSchema.Tables[tempsr.RelatedSchema].Columns[tempsr.RelatedSchemaField]));
                //    tempsr.IsRelated = true;
                //}
            }

            return sc;
        }
        private string AssemblyPath()
        {
            string strAssemblyLocation = "";
            strAssemblyLocation = Assembly.GetExecutingAssembly().CodeBase;
            strAssemblyLocation = strAssemblyLocation.Replace("file:///", "");
            strAssemblyLocation = strAssemblyLocation.Replace("/", @"\");
            strAssemblyLocation = Strings.Left(strAssemblyLocation, strAssemblyLocation.LastIndexOf(@"\") + 1);
            if (strAssemblyLocation.IndexOf("Debug") > 0)
            {

            }
            else {
                strAssemblyLocation = strAssemblyLocation.Replace(@"\bin", "");
            }
           
            return strAssemblyLocation;
        }
    }

    


    public class ModelRelation
    {
        private string mBaseSchema;
        private string mRelatedSchema;
        private string mBaseSchemaField;
        private string mRelatedSchemaField;
        private bool mIsRelated;
        public string BaseSchema
        {
            get
            {
                return mBaseSchema;
            }
            set
            {
                mBaseSchema = value;
            }
        }
        public string RelatedSchema
        {
            get
            {
                return mRelatedSchema;
            }
            set
            {
                mRelatedSchema = value;
            }
        }

        public string BaseSchemaField
        {
            get
            {
                return mBaseSchemaField;
            }
            set
            {
                mBaseSchemaField = value;
            }
        }

        public string RelatedSchemaField
        {
            get
            {
                return mRelatedSchemaField;
            }
            set
            {
                mRelatedSchemaField = value;
            }
        }
        public bool IsRelated
        {
            set
            {
                mIsRelated = value;
            }
            get
            {
                return mIsRelated;
            }
        }

        public ModelRelation()
        {
            mBaseSchema = "";
            mBaseSchemaField = "";
            mRelatedSchema = "";
            mRelatedSchemaField = "";
            IsRelated = false;
        }
        public ModelRelation(string pBaseSchema, string pRelatedSchema, string pBaseSchemaField, string pRelatedSchemaField)
        {
            this.BaseSchema = pBaseSchema;
            this.BaseSchemaField = pBaseSchemaField;
            this.RelatedSchema = pRelatedSchema;
            this.RelatedSchemaField = pRelatedSchemaField;
            IsRelated = false;


            //DataTable dt_avg_Manpower_by_month = new DataTable();
            //            DataSet ds = new DataSet();

            //dt_avg_Manpower_by_month = ds.Tables["avg_Manpower_by_month"] ;
            //            DataTable dt_Monthly_Production = new DataTable();
            //dt_Monthly_Production = ds.Tables["Monthly_Production"] ;

            //var query = from avg_Manpower_by_month in dt_avg_Manpower_by_month.AsEnumerable()
            //             from Monthly_Production in dt_Monthly_Production.AsEnumerable()
            //            where Monthly_Production.Field<Int32>("CalenderMonth") == avg_Manpower_by_month.Field<Int32>("Month") && Monthly_Production.Field<Int32>("CalenderYear") == avg_Manpower_by_month.Field<Int32>("Year")
            //            select new {  Pcs = Monthly_Production.Field<Decimal>("Pcs"),  RoughCarats = Monthly_Production.Field<Decimal>("RoughCarats")};


        }
     


    }

    public class ModelField
    {
        private string mFieldName = "";
        private string mSchema = "";
        public string FieldName
        {

            get
            {
                return mFieldName;
            }
            set
            {
                mFieldName = value;
            }
        }

        public string Schema
        {

            get
            {
                return mSchema;
            }
            set
            {
                mSchema = value;
            }
        }
        public ModelField(string pFieldName, string pSchemaName)
        {
            this.FieldName = pFieldName;
            this.Schema = pSchemaName;
        }
    }

    public class SchemaError
    {

        private string mError;
        private int mLineNumber;
        public string Error
        {
            get
            {
                return mError;
            }
            set
            {
                mError = value;
            }
        }

        public int LineNumber
        {
            get
            {
                return mLineNumber;
            }
            set
            {
                mLineNumber = value;
            }
        }

        public SchemaError(string err, int lineNumber)
        {
            mError = err;
            mLineNumber = lineNumber;



        }

    }

}

 