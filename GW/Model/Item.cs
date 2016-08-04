using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;
using GW;

namespace GW.Model
{
    [Table(Name = "Item")]
    public class DBItem
    {
        [Column(IsPrimaryKey = true, CanBeNull = false, DbType = "nchar(10)", IsDbGenerated = true, AutoSync = AutoSync.OnInsert)]
        public string ID { get; set; }

        [Column(CanBeNull = true, DbType = "nchar(10)", UpdateCheck = UpdateCheck.Never)]
        public string name { get; set; }

        [Column(CanBeNull = true, DbType = "float", UpdateCheck = UpdateCheck.Never)]
        public float avgprice { get; set; }

        [Column(CanBeNull = true, DbType = "float", UpdateCheck = UpdateCheck.Never)]
        public float refprice { get; set; }

        [Column(CanBeNull = true, DbType = "float", UpdateCheck = UpdateCheck.Never)]
        public float lowprice { get; set; }

        [Column(CanBeNull = true, DbType = "float", UpdateCheck = UpdateCheck.Never)]
        public float highprice { get; set; }

        [Column(CanBeNull = true, DbType = "datetime", UpdateCheck = UpdateCheck.Never)]
        public DateTime updatetime { get; set; }

        [Column(CanBeNull = true, DbType = "nchar(30)", UpdateCheck = UpdateCheck.Never)]
        public string FWQ { get; set; }

        [Column(CanBeNull = true, DbType = "int", UpdateCheck = UpdateCheck.Never)]
        public int formula { get; set; }

        public static DBItem GetItemByName(string name, string FWQ)
        {
            DBItem ret = new DBItem() ;

            using (DBGoblinWOW dc = new DBGoblinWOW(Constant.CONNSTR))
            {
                try
                {
                    var rst = from data in dc.Item
                              where data.name == name && data.FWQ == FWQ
                              select data;
                    foreach (DBItem cur in rst)
                    {
                        ret = cur;
                        break;
                    }
                }
                catch (Exception ex)
                {

                }

            }
            return ret;
        }

        public static void UpdateItem(string name, float avgprice, float refprice, float lowprice, float highprice, DateTime updatetime, string FWQ, int formula)
        {
            using (DBGoblinWOW dc = new DBGoblinWOW(Constant.CONNSTR))
            {
                try
                {
                    DBItem tar = dc.Item.Single(d => d.name == name && d.FWQ == FWQ);
                    if (tar != null)
                    {
                        tar.avgprice = avgprice;
                        tar.refprice = refprice;
                        tar.lowprice = lowprice;
                        tar.highprice = highprice;
                        tar.formula = formula;
                    }
                    else
                    {
                        tar.avgprice = 0;
                        tar.refprice = 0;
                        tar.lowprice = 0;
                        tar.highprice = 0;
                        tar.formula = Constant.FORMULAUNKNOWN;
                    }
                    tar.updatetime = updatetime;
                    dc.SubmitChanges();
                }
                catch (Exception ex)
                {

                }

            }
        }

        public static float GetAvgPrice(string name, string FWQ)
        {
            float avg = 0;

            using (DBGoblinWOW dc = new DBGoblinWOW(Constant.CONNSTR))
            {
                try
                {
                    var rst = from data in dc.Item
                              where data.name == name && data.FWQ ==FWQ
                              select data;

                    foreach (DBItem cur in rst)
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
        
    }

}