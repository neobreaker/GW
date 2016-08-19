using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;
using System.ComponentModel;

using GW;

namespace GW.Model
{
    [Table(Name = "formula")]
    public class DBFormula
    {
        [Column(IsPrimaryKey = true, CanBeNull = false, DbType = "int", IsDbGenerated = true, AutoSync = AutoSync.OnInsert)]
        public int ID { get; set; }

        [Column(CanBeNull = false, DbType = "nchar(10)")]
        public string output { get; set; }

        [Column(CanBeNull = false, DbType = "nchar(10)")]
        public string input { get; set; }

        [Column(CanBeNull = false, DbType = "int")]
        public int outputnum { get; set; }

        [Column(CanBeNull = false, DbType = "int")]
        public int inputnum { get; set; }

        [Column(CanBeNull = false, DbType = "int")]
        public int type { get; set; }

        [Column(CanBeNull = false, DbType = "int")]
        public int CDminite { get; set; }

        [Column(CanBeNull = false, DbType = "int")]
        public int isvalid { get; set; }

        public static List<DBFormula> GetFormulaByOutput(string output)
        {
            List<DBFormula> ret = new List<DBFormula>();

            using (DBGoblinWOW dc = new DBGoblinWOW(Constant.CONNSTR))
            {
                try
                {
                    var rst = from data in dc.Formula
                              where data.output == output
                              select data;
                    foreach (DBFormula cur in rst)
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

        public static List<DBFormula> GetFormulaByOutputAndType(string output, int type)
        {
            List<DBFormula> ret = new List<DBFormula>();

            using (DBGoblinWOW dc = new DBGoblinWOW(Constant.CONNSTR))
            {
                try
                {
                    var rst = from data in dc.Formula
                              where data.output == output && data.type == type
                              select data;
                    foreach (DBFormula cur in rst)
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
        
    }
}
