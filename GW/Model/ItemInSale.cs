using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;
using GW;

namespace GW.Model
{
    [Table(Name = "ItemInSale")]
    public class DBItemInSale
    {
        [Column(IsPrimaryKey = true, CanBeNull = false, DbType = "int", IsDbGenerated = true, AutoSync = AutoSync.OnInsert)]
        public int ID { get; set; }

        [Column(CanBeNull = false, DbType = "nchar(10)")]
        public string name { get; set; }

        [Column(CanBeNull = true, DbType = "float")]
        public float lowprice { get; set; }

        [Column(CanBeNull = true, DbType = "float")]
        public float highprice { get; set; }

        [Column(CanBeNull = true, DbType = "float")]
        public float avgprice { get; set; }

        [Column(CanBeNull = true, DbType = "int")]
        public int avgnum { get; set; }

        [Column(CanBeNull = true, DbType = "float")]
        public float recentprice { get; set; }

        [Column(CanBeNull = false, DbType = "nchar(30)")]
        public string FWQ { get; set; }

        public static List<DBItemInSale> GetItemInSaleByName(string name, string FWQ)
        {
            List<DBItemInSale> ret = new List<DBItemInSale>();

            using (DBGoblinWOW dc = new DBGoblinWOW(Constant.CONNSTR))
            {
                try
                {
                    var rst = from data in dc.ItemInSale
                              where data.name == name && data.FWQ == FWQ
                              select data;

                    foreach (DBItemInSale cur in rst)
                    {
                        ret.Add(cur);
                    }
                }
                catch (Exception ex)
                {

                }

            }
            return ret;
        }

        public static float GetAvgPrice(string name, string FWQ)
        {
            float avg = 0;

            using (DBGoblinWOW dc = new DBGoblinWOW(Constant.CONNSTR))
            {
                try
                {
                    var rst = from data in dc.ItemInSale
                              where data.name == name && data.FWQ == FWQ
                              select data;

                    foreach (DBItemInSale cur in rst)
                    {
                        avg = cur.avgprice;
                        break;
                    }
                }
                catch (Exception ex)
                {

                }

            }

            return avg;
        }

        public static void Add(string name, string FWQ)
        {
            using (DBGoblinWOW dc = new DBGoblinWOW(Constant.CONNSTR))
            {
                try
                {
                    DBItemInSale item = new DBItemInSale();
                    item.name = name;
                    item.FWQ = FWQ;
                    dc.ItemInSale.InsertOnSubmit(item);
                    dc.SubmitChanges();
                }
                catch (Exception ex)
                {
                    throw (ex);
                }
            }
         
        }

    }
}