using System;
using System.Text;
using System.Data;
using System.Linq;
using System.Collections.Generic;

namespace SERIOUS_BUSINESS
{
    enum PType { pt_txt = 1, pt_dbl = 2, pt_bool = 3 };
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
    class AssociatedPC
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

    class TableWithAccess
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
        public TableWithAccess(string _name, int _AM)
        {
            this.name = _name;
            this.accessMod = _AM;
        }
    }
    class StockForStock
    {
        public string Категория { get; set; }
        public string Наименование { get; set; }
        public int Спрос { get; set; }
        public int Остаток { get; set; }

        public StockForStock() { }

    }
    class StockForManager
    {
        public int id {get; set;}
        public int stockResidue { get; set; }
        public List<NamedParameter> Parameters;

        public StockForManager()
        {
            Parameters = new List<NamedParameter>();
        }
    }

    class OrdersForManager
    {
        public int Номер { get; set; }
        public DateTime Дата { get; set; }
        public string Заказчик { get; set; }
        public string Статус { get; set; }
        public OrdersForManager() { }
    }

    class AllOrders : OrdersForManager
    {
        public string Сотрудник { get; set; }
        public AllOrders () {}
    }

}