using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using System.Reflection.Emit;
using System.Text.RegularExpressions;

namespace SERIOUS_BUSINESS
{
    class TableOperator
    {
        static public void SetNewContentUnique(IEnumerable<StockForManager> src, ref DataTable des)
        {
            if (src != null)
            {
                #region Init
                if (des == null)
                {
                    des = new DataTable();
                }
                else
                {
                    des.Dispose();
                    des = new DataTable();
                }
                #endregion
                #region Columns
                if (src.Any())
                {
                    des.Columns.Add("id", 1.GetType());
                    foreach (var par in src.First().Parameters.OrderBy(par => par.id).AsEnumerable())
                    {
                        des.Columns.Add(new DataColumn(par.name, par.GetTypedValue().GetType()));
                    }
                    des.Columns.Add(new DataColumn("Остаток", 1.GetType()));
                #endregion
                #region Fill
                    foreach (var item in src)
                    {
                        DataRow nrow = des.NewRow();
                        int i = 0;
                        nrow[i++] = item.id;
                        item.Parameters.ForEach(par =>
                        {
                            nrow[i] = par.GetTypedValue();
                            i++;
                        });
                        nrow[i] = item.stockResidue;
                        des.Rows.Add(nrow);
                    }
                }
                    #endregion
            }
            else
            {
                throw new ArgumentNullException("Null src enumerable collection");
            }
        }

        static public void SetNewContentCommon(Object[] src, ref DataTable des)
        {
            #region Init
            if (des == null)
            {
                des = new DataTable();
            }
            else
            {
                des.Dispose();
                des = new DataTable();
            }
            #endregion
            des = ConvertToDataTable(src);
            FormatHeaders(ref des);
        }

        static public void SetNewContentCommon(Object[] src, ref DataTable des, string title)
        {
            #region Init
            if (des == null)
            {
                des = new DataTable();
            }
            else
            {
                des.Dispose();
                des = new DataTable();
            }
            #endregion
            des = ConvertToDataTable(src);
            FormatHeaders(ref des);
            des.TableName = title;
        }

        static public DataTable Where(DataTable tbl, Func<DataRow, bool> selector)
        {
            var queryRes = tbl.AsEnumerable().Where(selector);
            if (queryRes.Any())
            {
                return queryRes.CopyToDataTable();
            }
            else
                System.Windows.Forms.MessageBox.Show("Поиск не дал результатов");
            return tbl;
        }

        static public DataTable Like(DataTable tbl, string column, string substring)
        {
            var queryRes = tbl.AsEnumerable().Where(row => row[column].ToString().Contains(substring));
            if (queryRes.Any())
            {
                return queryRes.CopyToDataTable(); 
            }
            else
                System.Windows.Forms.MessageBox.Show("Поиск не дал результатов");
            return tbl;
        }

        private static DataTable CreateDataTable(PropertyInfo[] properties)
        {

            DataTable dt = new DataTable();
            DataColumn dc = null;
            foreach (PropertyInfo pi in properties)
            {
                dc = new DataColumn();
                dc.ColumnName = pi.Name;
                dc.DataType = pi.PropertyType;
                dt.Columns.Add(dc);
            }
            return dt;
        }

        private static void FillData(PropertyInfo[] properties, DataTable dt, Object o)
        {
            DataRow dr = dt.NewRow();
            foreach (PropertyInfo pi in properties)
                dr[pi.Name] = pi.GetValue(o, null);
            dt.Rows.Add(dr);
        }

        public static DataTable ConvertToDataTable(Object[] array)
        {
            PropertyInfo[] properties = array.GetType().GetElementType().GetProperties();
            DataTable dt = CreateDataTable(properties);
            if (array.Length != 0)
            {
                foreach (object o in array)
                    FillData(properties, dt, o);
            }
            return dt;
        }

        public static void FormatHeaders(ref DataTable dt, string pattern = "_")
        {
            Regex regex = new Regex(pattern);
            foreach(DataColumn dc in dt.Columns)
            {
                dt.Columns[dc.Ordinal].Caption = regex.Replace(dc.Caption, " ");
            }
        }
    }
}
