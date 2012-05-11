using System;
using System.Windows.Forms;
using System.Text;
using System.Data;
using System.Linq;
using System.Collections.Generic;

namespace SERIOUS_BUSINESS
{
    enum PType { pt_txt = 1, pt_dbl = 2, pt_bool = 3 };
    class ParameterCategoryV : Object
    {
        public string Название { get; set; }
        public bool По_умолчанию { get; set; }
        public ParameterCategoryV() { }
    }
    class NamedItem
    {
        public int id
        {
            get;
            set;
        }
        public string name
        {
            get;
            set;
        }
        public NamedItem() { }
    }
    partial class AssociatedPC : Object
    {
        public int id { get; set; }
        public string name { get; set; }
        public short type { get; set; }
        public bool associated { get; set; }
        public AssociatedPC() { }
    }
    class NamedParameter
    {
        public int id { get; set; }
        public short type { get; set; }
        public string name { get; set; }
        public string valueTxt { get; set; }
        public double? valueDbl { get; set; }
        public bool? valueBool { get; set; }

        public NamedParameter() { }

        public string GetValue()
        {
            switch (type)
            {
                case ((short)PType.pt_txt):
                    return valueTxt;
                case ((short)PType.pt_dbl):
                    return valueDbl.ToString();
                case ((short)PType.pt_bool):
                    return valueBool.ToString();
                default:
                    return null;
            }

        }

        public dynamic GetTypedValue()
        {
            switch (type)
            {
                case (short)PType.pt_txt:
                    if (String.IsNullOrEmpty(valueTxt))
                        return "";
                    else
                        return valueTxt;
                case (short)PType.pt_dbl:
                    if (valueDbl.HasValue)
                        return valueDbl;
                    else return 0.0f;
                case (short)PType.pt_bool:
                    if (valueBool.HasValue)
                        return valueBool;
                    else
                        return false;
                default:
                    return "";
            }
        }

        static public bool? GetTypedBValue(string _value, short _type)
        {
            if (_type == (short)PType.pt_bool)

                try
                {
                    return bool.Parse(_value);
                }
                catch (FormatException)
                {
                    throw new FormatException("Не удалось распознать значение " + _value + "\nПроверьте правильность ввода:\n Входная строка должна иметь формат {True, False}");
                }

            else return null;
        }

        static public double? GetTypedDValue(string _value, short _type)
        {
            if (_type == (short)PType.pt_dbl)

                try
                {
                    return double.Parse(_value);
                }
                catch (FormatException)
                {
                    throw new FormatException("Не удалось распознать значение " + _value + "\nПроверьте правильность ввода:\n Входная строка должна иметь формат '1234,1234'");
                }

            else return null;

        }

        static public string GetTypedSValue(string _value, short _type)
        {
            if (_type == (short)PType.pt_txt)

                return _value;
            else return null;

        }

        public static List<NamedParameter> CastToNamed(IQueryable<res.ItemParameter> src)
        {
            List<NamedParameter> res = new List<NamedParameter>();
            src.ToList().ForEach(par =>
            {
                res.Add(new NamedParameter
                {
                    id = par.id,
                    name = par.ParameterCategory.name,
                    type = par.ParameterCategory.type,
                    valueBool = par.valueBool,
                    valueDbl = par.valueDbl,
                    valueTxt = par.valueTxt
                });
            });
            return res;
        }

    }
    class ItemWithAccess
    {
        public string name
        {
            get;
            set;
        }
        public int accessMod
        {
            get;
            set;
        }
        public ItemWithAccess(string _name, int _AM)
        {
            this.name = _name;
            this.accessMod = _AM;
        }
    }
    class TableWithAccessAndCMS : ItemWithAccess
    {
        public ContextMenuStrip mstrip;
        public TableWithAccessAndCMS(string _name, int _AM, ContextMenuStrip _mstrip)
            : base(_name, _AM)
        {
            mstrip = _mstrip;
        }
    }
    class StockForStock : Object
    {
        public int id { get; set; }
        public string Категория { get; set; }
        public string Наименование { get; set; }
        public int Спрос { get;set;}
        public int Остаток { get; set; }

        public StockForStock() { }

    }
    class StockForManager
    {
        public int id { get; set; }
        public int stockResidue { get; set; }
        public List<NamedParameter> Parameters;

        public StockForManager()
        {
            Parameters = new List<NamedParameter>();
        }
    }
    class OrdersForManager : Object
    {
        public int Номер { get; set; }
        public DateTime Дата { get; set; }
        public string Заказчик { get; set; }
        public string Статус { get; set; }
        public OrdersForManager() { }
    }
    class Employees : Object
    {
        public int Номер { get; set; }
        public string Имя { get; set; }
        public string Логин { get; set; }
        public string Доступ { get; set; }
        public Employees() { }
    }
    class AllOrders : OrdersForManager
    {
        public string Сотрудник { get; set; }
        public AllOrders() { }
    }
    class PositionForOrder : Object
    {
        public int id { get; set; }
        public string Наименование { get; set; }
        public int Количество { get; set; }
        public PositionForOrder() { }
    }
    class PositionDelta : Object
    {
        public int id { get; set; }
        public int delta { get; set; }
        public PositionDelta(res.Position _old, PositionForOrder _new)
        {
            id = _old.id;
            delta = Calculate(_old, _new);
        }
        public static int Calculate(res.Position _old, PositionForOrder _new)
        {
            return _new.Количество - _old.count;
        }
        public static int Calculate(res.Position _old, int _newCount)
        {
            return _newCount - _old.count;
        }
        public static void ApplyPosAndItem(ref res.Position _pos, PositionDelta _delta)
        {
            _pos.count += _delta.delta;
            _pos.Item.storeResidue -= _delta.delta;
        }
    }
    class Report_Income : Object
    {
        public string Категория { get; set;}
        public double Прибыль { get; set;}
        public Decimal От_Общей_прибыли { get; set;}

        public Report_Income() { }
    }

}