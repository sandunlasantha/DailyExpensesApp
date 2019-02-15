using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using SQLite;

namespace DailyExpensesApp.DBConnection
{
    [Table("Users")]
    public  class Users
    {
        [PrimaryKey,AutoIncrement]
        public int Id
        {
            get;
            set;
        }
                       
        public string Name {
            get;
            set;
        }

      
        [NotNull, Unique]
        public string Email {
            get;
            set;
        }
       
        [NotNull]
        public string Password
        {
            get;
            set;
        }
     
    }
}
