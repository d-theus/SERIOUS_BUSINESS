using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;

namespace SERIOUS_BUSINESS
{
    static class ReportGenerator
    {
        static private StreamWriter writer;
        static ReportGenerator()
        {
        }

        static public void GenerateFromDataTable (DataTable _srcDataTable,string filename, string reportCaption = "" )
        {
            writer = new StreamWriter(filename, false,Encoding.UTF8);
            writer.WriteLine("<html>");
            writer.WriteLine("<body>");
            writer.WriteLine(string.Format("<h1>{0}</h1>", reportCaption));
            CreateTable(ref writer, _srcDataTable);
            writer.WriteLine("</body>");
            writer.WriteLine("</html>");
            writer.Close();
            writer.Dispose();
        }

        static private void CreateTable(ref StreamWriter wr, DataTable Tbl)
        {
            if (wr == null)
                throw new ArgumentNullException("stream writer");
            if (Tbl == null)
                throw new ArgumentNullException("DataTable");

            #region Table
            wr.WriteLine("<table border=\"1\"");

            #region Col headers
            wr.WriteLine("<tr>");
            foreach (DataColumn col in Tbl.Columns)
            {
                wr.WriteLine(string.Format("<th>{0}</th>", col.Caption));
            }
            wr.WriteLine("</tr>"); 
            #endregion
            #region Filling rows
            foreach (DataRow row in Tbl.Rows)
            {
                wr.WriteLine("<tr>");
                foreach (var val in row.ItemArray)
                {
                    wr.WriteLine(string.Format("<td>{0}</td>", val.ToString()));
                }
                wr.WriteLine("</tr>");
            } 
            #endregion


            wr.WriteLine("</table>"); 
            #endregion
        }
    }
}

 //           <html>
 //<body>

 //<h1>My First Heading</h1>

 //<p>My first paragraph.</p>

 //</body>
 //</html>
