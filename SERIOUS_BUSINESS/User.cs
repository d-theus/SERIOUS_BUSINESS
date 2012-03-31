using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SERIOUS_BUSINESS
{
    enum Access_Modifiers { acc_none, acc_director, acc_administrator, acc_storewk, acc_mgr }; 
    class User : res.Employee
    {
        public int accessModifier;
        public string login;
        User(string _login, int _accessModifier)
        {
            login = _login;
            accessModifier = _accessModifier;
        }
    }
}
