using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SERIOUS_BUSINESS
{
    enum Access_Modifiers : int  { acc_none = 0, acc_director, acc_administrator, acc_storewk, acc_mgr }; 
    public class User
    {
        public int accessModifier;
        public string login;
        public User(string _login, int _accessModifier)
        {
            login = _login;
            accessModifier = _accessModifier;
        }
    }
}
