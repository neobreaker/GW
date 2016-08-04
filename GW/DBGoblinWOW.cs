using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using GW.Model;

namespace GW
{
    [Database(Name = "GoblinWOW")]
    public class DBGoblinWOW : DataContext  
    {
        public Table<DBFormula> Formula;
        public Table<DBItem> Item;
        public Table<DBItemInSale> ItemInSale;

        public DBGoblinWOW(string connStr) : base(connStr) { }
    }
}