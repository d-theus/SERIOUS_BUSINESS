using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Stimulsoft.Report;
using Stimulsoft.Report.Components;
using Stimulsoft.Controls;
using Stimulsoft.Base;
using Stimulsoft.Base.Drawing;

namespace SERIOUS_BUSINESS
{
    public partial class FormReports : Form
    {
        private res.Model1Container database;

        private DataTable table;
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

        private void GenerateTable(DateTime initialDate, DateTime _endDate)
        {
            string title = string.Format("С {0} по {1}", initialDate.Date.ToShortDateString(), _endDate.Date.ToShortDateString());
            DateTime endDate = _endDate.AddDays(1);
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
                                var positions = (from pos in database.PositionSet where pos.itemID == item.id select pos).Where(pos => 
                                    pos.Order.date >= initialDate.Date
                                    &&
                                    pos.Order.date <= endDate.Date);
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
                            refEnt.Процент_от_общей_прибыли = Decimal.Round(new Decimal(refEnt.Прибыль * 100 / overallIncome), 2);
                        }
                        else
                        {
                            refEnt.Процент_от_общей_прибыли = 0;
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
                        var counts = from pos in database.PositionSet where pos.itemID == ent.id && pos.Order.date >= initialDate.Date && pos.Order.date <= endDate.Date select pos.count;
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
                        var db_ord = (from ord in database.OrderSet where ord.date >= initialDate.Date && ord.date <= endDate.Date select ord.id);
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
            GenerateTable(dtp_begin.Value, dtp_end.Value);
            string report_filename = string.Format("{0}reports/{1} - {2}.xls", rootD, cb_type.Text, DateTime.Now.ToShortDateString());
            if (!Directory.EnumerateDirectories(rootD, "reports").Any())
            {
                Directory.CreateDirectory(rootD + "reports");
            }
            string report_url = string.Format("file://localhost/{0}", report_filename);
            //ReportGenerator.GenerateFromDataTable(table, report_filename, cb_type.Text);
            ReportGenerator.GenerateNewReport(table, report_filename, cb_type.Text);
            //webBrowser.Navigate(report_url);
        }
    }
    static class ReportGenerator
    {
        static ReportGenerator()
        {
        }

        static public void GenerateNewReport(DataTable tbl, string path, string caption)
        {

			StiReport report = new StiReport();

			//Add tbl to datastore
			report.RegData("tbl", tbl);

			//Fill dictionary
			report.Dictionary.Synchronize();
			report.Dictionary.DataSources[0].Name = "tbl";
			report.Dictionary.DataSources[0].Alias = "tbl";

			//StiPage page = report.Pages[0];
            foreach (StiPage page in report.Pages)
            {

                //Create header
                if (report.Pages.IndexOf(page) == 0)
                {
                    StiText header = new StiText(new RectangleD(0, 0, page.Width, page.Height / 20), caption);
                    StiText period = new StiText(new RectangleD(0, page.Height / 20, page.Width, page.Height / 20), tbl.TableName);
                    header.HorAlignment = StiTextHorAlignment.Center;
                    header.Font = new Font("Arial", 22.5f);
                    period.HorAlignment = StiTextHorAlignment.Center;
                    header.Font = new Font("Arial", 12f);
                    page.Components.Add(header);
                    page.Components.Add(period);
                    header.Render();
                    period.Render(); 
                }

                //Create HeaderBand
                StiHeaderBand headerBand = new StiHeaderBand();
                headerBand.Height = 0.5;
                headerBand.Name = "HeaderBand";
                page.Components.Add(headerBand);

                //Create Databand
                StiDataBand dataBand = new StiDataBand();
                dataBand.DataSourceName = "tbl";
                dataBand.Height = 0.5;
                dataBand.Name = "DataBand";
                page.Components.Add(dataBand);

                //Create texts
                double pos = 0;

                double columnWidth = StiAlignValue.AlignToMinGrid(page.Width / tbl.Columns.Count, 0.1, true);

                int nameIndex = 1;

                foreach (DataColumn dataColumn in tbl.Columns)
                {

                    //Create text on header
                    StiText headerText = new StiText(new RectangleD(pos, page.Height / 10, columnWidth, 0.5));
                    headerText.Text.Value = dataColumn.Caption;
                    headerText.HorAlignment = StiTextHorAlignment.Center;
                    headerText.Name = "HeaderText" + nameIndex.ToString();
                    headerText.Brush = new StiSolidBrush(Color.LightGray);
                    headerText.Border.Side = StiBorderSides.All;
                    headerBand.Components.Add(headerText);

                    //Create text on Data Band
                    StiText dataText = new StiText(new RectangleD(pos, 0, columnWidth, 0.5));
                    dataText.Text.Value = "{tbl." + Stimulsoft.Report.CodeDom.StiCodeDomSerializator.ReplaceSymbols(dataColumn.ColumnName) + "}";
                    dataText.Name = "DataText" + nameIndex.ToString();
                    dataText.Border.Side = StiBorderSides.All;

                    //Add highlight
                    StiCondition condition = new StiCondition();
                    condition.BackColor = Color.DarkGray;
                    condition.TextColor = Color.Black;
                    condition.Expression = "(Line & 1) == 1";
                    condition.Item = StiFilterItem.Expression;
                    dataText.Conditions.Add(condition);

                    dataBand.Components.Add(dataText);

                    pos = pos + columnWidth;

                    nameIndex++;
                }

                //Create FooterBand
                StiFooterBand footerBand = new StiFooterBand();
                footerBand.Height = 0.5;
                footerBand.Name = "FooterBand";
                page.Components.Add(footerBand);

                //Create text on footer
                StiText footerText = new StiText(new RectangleD(0, 0, page.Width, 0.5));
                footerText.Text.Value = "Всего записей - {Count()}";
                footerText.HorAlignment = StiTextHorAlignment.Right;
                footerText.Name = "FooterText";
                footerText.Brush = new StiSolidBrush(Color.Gray);
                footerBand.Components.Add(footerText);

                report.Show();
            } 
        }
    }
}
