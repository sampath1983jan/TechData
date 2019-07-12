using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Mono.Reflection;

namespace Tz.Data
{
   public static class Shared
    {
        public static string generateID()
        {
            long i = 1;

            foreach (byte b in Guid.NewGuid().ToByteArray())
            {
                i *= ((int)b + 1);
            }

            string number = String.Format("{0:d4}", (DateTime.Now.Ticks / 10) % 1000000000);

            return Guid.NewGuid().ToString("N") + number;
        }
    }


    public static class Common
    {

        public static void Merge<T>(this T target, T source)
        {
            Type t = typeof(T);
            var properties = t.GetProperties().Where(prop => prop.CanRead && prop.CanWrite);
            foreach (var prop in properties)
            {
                var value = prop.GetValue(source, null);
                if (value != null)
                    prop.SetValue(target, value, null);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item1">Actual Object</param>
        /// <param name="item2">Target Object</param>
        public static void Combine<T>(ref T target, T source)
        {
            Type t = typeof(T);
            var properties = t.GetProperties().Where(prop => prop.CanRead && prop.CanWrite);
            foreach (var prop in properties)
            {
                var value = prop.GetValue(source, null);
                if (value != null)
                    prop.SetValue(target, value, null);
            }
        }



        public static List<T> toList<T>(this System.Data.DataTable dt, DataFieldMappings df, Func<T, T> Bind,
            Func<string, string, dynamic> format)
        {
            try
            {
                //  const BindingFlags flags = BindingFlags.Public | BindingFlags.Instance;
                var columnNames = dt.Columns.Cast<DataColumn>()
                    .Select(c => c.ColumnName)
                    .ToList();
                var ints = Activator.CreateInstance<T>();
                var objectProperties = GetAllProperties(typeof(T));
                var targetList = dt.AsEnumerable().Select(dataRow =>
                {
                    var instanceOfT = Activator.CreateInstance<T>();
                    // foreach (var properties in objectProperties.Where(properties => columnNames.Contains(properties.Name) && dataRow[properties.Name] != DBNull.Value))
                    foreach (DataFieldMapping d in df.GetMapping())
                    {
                        if (dt.Columns.Contains(d.DataField))
                        {
                            var property = objectProperties.Where(
                                                    properties => properties.Name == d.MemberField && dataRow[d.DataField] != DBNull.Value
                                                    );
                            if (property.Count() > 0)
                            {
                                var pt = property.FirstOrDefault();
                                if (d.IsKey == false)
                                {
                                    if (pt.PropertyType.Name == "Boolean")
                                    {
                                        if (dataRow[d.DataField] == null)
                                        {
                                            pt.SetValue(instanceOfT, false, null);
                                        }
                                        else
                                        {
                                            pt.SetValue(instanceOfT, Convert.ToBoolean(dataRow[d.DataField]), null);
                                        }
                                    }
                                    else
                                    {
                                        if ((((pt).PropertyType).BaseType).FullName == "System.Enum")
                                        {
                                            pt.SetValue(instanceOfT, format(d.DataField, dataRow[d.DataField].ToString()), null);
                                        }
                                        else if ((((pt).PropertyType).BaseType).FullName == "System.ValueType")
                                        {
                                            pt.SetValue(instanceOfT, Cast(dataRow[d.DataField], (((pt).PropertyType))), null);
                                        }
                                        else
                                        {
                                            pt.SetValue(instanceOfT, dataRow[d.DataField], null);
                                        }
                                    }
                                }
                                else
                                {
                                    FieldInfo nameField = pt.GetBackingField();
                                    nameField.SetValue(instanceOfT, dataRow[d.DataField]);
                                }
                            }
                        }
                    }
                    if (Bind is null)
                    {
                    }
                    else { instanceOfT = Bind(instanceOfT); }
                    return instanceOfT;
                }).ToList();

                return targetList.ToList<T>();
            }
            catch (System.Exception e)
            {
                throw new System.Exception(e.Message, e.InnerException);
            }
        }

        public static dynamic Cast(dynamic obj, Type castTo)
        {
            return Convert.ChangeType(obj, castTo);
        }


        public static List<T> toList<T>(this System.Data.DataTable dt, DataFieldMappings df)
        {
            try
            {
                // const BindingFlags flags = BindingFlags.Public | BindingFlags.Instance;
                var columnNames = dt.Columns.Cast<DataColumn>()
                    .Select(c => c.ColumnName)
                    .ToList();
                var ints = Activator.CreateInstance<T>();
                var objectProperties = GetAllProperties(typeof(T));
                var targetList = dt.AsEnumerable().Select(dataRow =>
                {
                    var instanceOfT = Activator.CreateInstance<T>();
                    // foreach (var properties in objectProperties.Where(properties => columnNames.Contains(properties.Name) && dataRow[properties.Name] != DBNull.Value))
                    foreach (DataFieldMapping d in df.GetMapping())
                    {
                        if (dt.Columns.Contains(d.DataField))
                        {
                            var property = objectProperties.Where(
                                                    properties => properties.Name == d.MemberField && dataRow[d.DataField] != DBNull.Value
                                                    );
                            if (property.Count() > 0)
                            {
                                var pt = property.FirstOrDefault();
                                pt.SetValue(instanceOfT, dataRow[d.DataField], null);
                            }
                        }
                    }
                    return instanceOfT;
                }).ToList();

                return targetList.ToList<T>();
            }
            catch (System.Exception e)
            {
                throw new System.Exception(e.Message, e.InnerException);
            }
        }

        public static IEnumerable<PropertyInfo> GetAllProperties(Type t)
        {
            if (t == null)
                return Enumerable.Empty<PropertyInfo>();

            BindingFlags flags = BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance | BindingFlags.DeclaredOnly;
            return t.GetProperties(flags).Union(GetAllProperties(t.BaseType));
        }


    }


    public class DataFieldMappings
    {
        private List<DataFieldMapping> dfm;
        public DataFieldMappings()
        {
            dfm = new List<DataFieldMapping>();
        }
        public List<DataFieldMapping> GetMapping()
        {
            return dfm;
        }
        public DataFieldMappings Add(string dataField, string memberField, bool isKey = false)
        {
            dfm.Add(new DataFieldMapping(dataField, memberField, isKey));
            return this;
        }
    }

    public class DataFieldMapping
    {
        public string DataField;
        public string MemberField;
        public bool IsKey;
        public DataFieldMapping(string dataField, string memberField, bool isKey)
        {
            DataField = dataField;
            MemberField = memberField;
            IsKey = isKey;
        }
        public DataFieldMapping(string dataField, string memberField)
        {
            DataField = dataField;
            MemberField = memberField;
            IsKey = false;
        }
    }
}
