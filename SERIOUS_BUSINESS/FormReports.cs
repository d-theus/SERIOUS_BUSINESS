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
            database = new res.Model1Container(RegistryInteractor.GetFromReg("Connection String"));
            curEmpl = _curEmpl;
            InitTypes();
        }

        private void InitTypes()
        {
            types = new List<ItemWithAccess>();

            types.AddRange(new ItemWithAccess[] { 
            new ItemWithAccess("Выручка", (int)accessModifiers.acc_stock),
            new ItemWithAccess("Склад", (int)accessModifiers.acc_stock),
            new ItemWithAccess("Сотрудники", (int)accessModifiers.acc_adm)
            });

            cb_type.DataSource = types.Where(tbl => tbl.accessMod == curEmpl.Appointment.accessModifier || curEmpl.Appointment.accessModifier == (int)accessModifiers.acc_adm).ToList();
            cb_type.ValueMember = "accessMod";
            cb_type.DisplayMember = "name";
        }

        private void GenerateTable(DateTime initialDate)
        {
            string title = string.Format("С {0} по {1}", initialDate.Date.ToShortDateString(), DateTime.Now.Date.ToShortDateString());
            switch (cb_type.Text)
            {
                case "Выручка":
                    #region generate report
                    var viewIncome = from cat in database.ItemCategorySet
                               select new Report_Income
                               {
                                   Категория = cat.name
                               };
                    double overallIncome = 0;
                    Report_Income[] Report = viewIncome.ToArray();
                    #region category income
                    foreach (var ent in viewIncome)
                    {
                        var refEnt = Report.Single(v => v.Категория == ent.Категория);
                        IQueryable<res.Item> cItems = (from items in database.ItemSet where items.ItemCategory.name == refEnt.Категория select items);
                        #region item income
                        foreach (var item in cItems)
                        {
                            double curItemBuyP = 0;
                            double curItemSellP = 0;
                            try
                            {
                                curItemSellP = (double)item.ItemParameter.Single(par => par.ParameterCategory.name == "Цена продажи").valueDbl;
                                curItemBuyP = (double)item.ItemParameter.Single(par => par.ParameterCategory.name == "Цена закупки").valueDbl;
                            }
                            catch (InvalidOperationException)
                            {
                                curItemBuyP = 0;
                                curItemSellP = 0;
                            }
                            if ((from pos in database.PositionSet where pos.itemID == item.id select pos).Any())
                            {
                                int ICount = 0;
                                var positions = (from pos in database.PositionSet where pos.itemID == item.id select pos).Where(pos => pos.Order.date >= initialDate);
                                if (positions.Any())
                                {
                                    ICount = positions.Sum(p => p.count);
                                }
                                else
                                {
                                    ICount = 0;
                                }
                                refEnt.Прибыль = ICount * (curItemSellP - curItemBuyP);
                                if (refEnt.Прибыль >= 0)
                                {
                                    overallIncome += refEnt.Прибыль;
                                }
                            }
                        }
                        #endregion
                    }
                    #endregion
                    #region ratio
                    foreach (var ent in Report)
                    {
                        var refEnt = Report.Single(v => v.Категория == ent.Категория);

                        if (overallIncome != 0 && refEnt.Прибыль >= 0)
                        {
                            refEnt.От_Общей_прибыли = Decimal.Round(new Decimal(refEnt.Прибыль * 100 / overallIncome), 2);
                        }
                        else
                        {
                            refEnt.От_Общей_прибыли = 0;
                        }
                    }
                    #endregion
                    TableOperator.SetNewContentCommon(Report, ref table, title);
                    #endregion
                    break;
                case "Склад":
                    #region generate report
                    IQueryable<StockForStock> viewStock = from item in database.ItemSet
                                                      select new StockForStock
                                                      {
                                                          id = item.id,
                                                          Категория = item.ItemCategory.name,
                                                          Остаток = item.storeResidue,
                                                      };
                    List<StockForStock> Stock = new List<StockForStock>();
                    foreach (var ent in viewStock)
                    {
                        int demand = 0;
                        var counts = from pos in database.PositionSet where pos.itemID == ent.id && pos.Order.date >= initialDate.Date select pos.count;
                        if (counts.Any())
                        {
                            demand = counts.Sum();
                        }
                        Stock.Add(new StockForStock 
                        {
                            id = ent.id,
                            Категория = ent.Категория,
                            Наименование = (from par in database.ItemParameterSet where par.itemID == ent.id && par.ParameterCategory.name == "Наименование" select par).FirstOrDefault().valueTxt,
                            Остаток = ent.Остаток,
                            Спрос = demand
                        });
                    }
                    TableOperator.SetNewContentCommon(Stock.ToArray(), ref table, title);
                    #endregion
                    break;
                case "Сотрудники":
                    #region generate report
		                    var emp_set = from emp in database.EmployeeSet
                                   select new Report_Employees
                                   {
                                       Номер = emp.id,
                                       Полное_имя = emp.name
                                   };
                    List<Report_Employees> emp_view = new List<Report_Employees>();
                    foreach (var ent in emp_set)
                    {
                        int ord_count = 0;
                        var db_ord = (from ord in database.OrderSet where ord.date >= initialDate.Date select ord.id);
                        if (db_ord.Any())
                        {
                            ord_count = db_ord.Count();
                        }
                        emp_view.Add(new Report_Employees
                        {
                            Номер = ent.Номер,
                            Полное_имя = ent.Полное_имя,
                            Количество_заказов = ord_count
                        });
                    } 
	#endregion
                    TableOperator.SetNewContentCommon(emp_view.ToArray(), ref table, title);
                    break;
            }
        }

        private void btn_generate_Click(object sender, EventArgs e)
        {
            string rootD = RegistryInteractor.GetFromReg("Root Directory");
            GenerateTable(mc_initialDate.SelectionStart);
            string report_filename = string.Format("{0}reports/{1} - {2}.html", rootD, cb_type.Text, DateTime.Now.ToShortDateString());
            if (!Directory.EnumerateDirectories(rootD, "reports").Any())
            {
                Directory.CreateDirectory(rootD + "reports");
            }
            string report_url = string.Format("file://localhost/{0}", report_filename);
            ReportGenerator.GenerateFromDataTable(table, report_filename, cb_type.Text);
            webBrowser.Navigate(report_url);
        }

        private void mc_initialDate_DateChanged(object sender, DateRangeEventArgs e)
        {
            if (mc_initialDate.SelectionRange.End.Date <= DateTime.Now.Date)
            {
                dateCriteria = mc_initialDate.SelectionStart.Date.Date;
            }
            else
            {
                dateCriteria = DateTime.Now.Date;
                mc_initialDate.SelectionEnd = DateTime.Now.Date;
                MessageBox.Show("Нельзя генерировать отчеты из будущего");
            }
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
            wr.WriteLine(string.Format("<h3>{0}</h3>", Tbl.TableName));
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
