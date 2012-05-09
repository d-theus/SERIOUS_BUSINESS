using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace SERIOUS_BUSINESS
{
    public partial class FormReports : Form
    {
        private res.Model1Container database;

        private DataTable table;
        private DateTime dateCriteria;
        private List<ItemWithAccess> types;
        private res.Employee curEmpl;
        public FormReports(res.Employee _curEmpl)
        {
            InitializeComponent();
            database = new res.Model1Container();
            curEmpl = _curEmpl;
            InitTypes();
        }

        private void InitTypes()
        {
            types = new List<ItemWithAccess>();

            types.AddRange(new ItemWithAccess[] { 
            new ItemWithAccess("Выручка\\Расходы", (int)accessModifiers.acc_stock),
            new ItemWithAccess("Категории товаров", (int)accessModifiers.acc_stock),
            new ItemWithAccess("Товары конкретных категорий", (int)accessModifiers.acc_stock),
            new ItemWithAccess("Заказы по сотрудникам", (int)accessModifiers.acc_adm)
            });

            cb_type.DataSource = types.Where(tbl => tbl.accessMod == curEmpl.Appointment.accessModifier || curEmpl.Appointment.accessModifier == (int)accessModifiers.acc_adm).ToList();
            cb_type.ValueMember = "accessMod";
            cb_type.DisplayMember = "name";
        }

        private void GenerateTable(DateTime initialDate)
        {
            switch (cb_type.Text)
            {
                case "Выручка\\Расходы":
                    var view = from cat in database.ItemCategorySet
                               select new Report_Income
                               {
                                   Категория = cat.name
                               };
                    double overallIncome = 0;
                    #region category income
                    foreach (var ent in view)
                    {
                        Report_Income refEnt = view.Single(v => v.Категория == ent.Категория);
                        IQueryable<res.Item> cItems = (from items in database.ItemSet where items.ItemCategory.name == refEnt.Категория select items);
                        #region item income
                        foreach (var item in cItems)
                        {
                            refEnt.Прибыль +=
                                (from pos in database.PositionSet where pos.Order.date >= initialDate && pos.itemID == item.id select pos.count).Sum()
                                *
                                (double)item.ItemParameter.Single(par => par.ParameterCategory.name == "Цена продажи").valueDbl;
                        }
                        #endregion
                        overallIncome += refEnt.Прибыль;
                    }
                    #endregion
                    #region ratio
                    foreach (var ent in view)
                    {
                        Report_Income refEnt = view.Single(v => v.Категория == ent.Категория);

                        if (overallIncome != 0)
                        {
                            refEnt.От_Общей_прибыли = refEnt.Прибыль / overallIncome;
                        }
                        else
                        {
                            refEnt.От_Общей_прибыли = 0;
                        }
                    }
                    #endregion
                    TableOperator.SetNewContentCommon(view.ToArray(), ref table);
                    break;
            }
        }

        private void btn_generate_Click(object sender, EventArgs e)
        {
            GenerateTable(mc_initialDate.SelectionStart);
            string report_filename = string.Format("{0} - {1}.html", cb_type.Text, DateTime.Now.ToShortDateString());
            ReportGenerator.GenerateFromDataTable(table, report_filename, cb_type.Text);
            this.webBrowser.Url = ;
        }
    }
    static class ReportGenerator
    {
        static private StreamWriter writer;
        static ReportGenerator()
        {
        }

        static public void GenerateFromDataTable(DataTable _srcDataTable, string filename, string reportCaption = "")
        {
            writer = new StreamWriter(filename, false, Encoding.UTF8);
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
