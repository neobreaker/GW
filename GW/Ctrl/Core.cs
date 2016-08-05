using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GW.Model;
using System.Data;
using GW;

namespace GW.Ctrl
{
    public class Core
    {
        private static int m_manufacturingcycle = 0;
        public static int GetCostByName(string name, string FWQ)
        {
            int cnt = 0;
            int i = 0, t = 0;
            float min_avgprice = 0;
            float min_refprice = 0;
            float min_lowprice = 0;
            float min_highprice = 0;
            DBItem total_item = new DBItem();
            DBItem cur_item = new DBItem();
            List<DBFormula> lst_formula = DBFormula.GetFormulaByOutput(name.Trim());
            cnt = lst_formula.Count;
            if (cnt > 0)
            {
                for (i = 0; i < cnt; i++)
                {
                    GetCostByName(lst_formula[i].input, FWQ);
                }
                min_avgprice = UpdateItemFromInSale(name.Trim(), FWQ);     //首先参考拍卖行的价格

                for (t = 0; t < Constant.MAXFORMULATYPE; t++)
                {
                    total_item.avgprice = 0;
                    total_item.refprice = 0;
                    total_item.lowprice = 0;
                    total_item.highprice = 0;
                    for (i = 0; i < cnt; i++)
                    {
                        if (lst_formula[i].type == t)  //统计配方中所有需要物品的最低参考价格
                        {
                            cur_item = DBItem.GetItemByName(lst_formula[i].input, FWQ);
                            total_item.avgprice += cur_item.avgprice * lst_formula[i].inputnum / lst_formula[i].outputnum;
                            total_item.refprice += cur_item.refprice * lst_formula[i].inputnum / lst_formula[i].outputnum;
                            total_item.lowprice += cur_item.lowprice * lst_formula[i].inputnum / lst_formula[i].outputnum;
                            total_item.highprice += cur_item.highprice * lst_formula[i].inputnum / lst_formula[i].outputnum;
                        }
                    }
                    if (min_avgprice > total_item.avgprice && total_item.avgprice != 0)     //从所有配方和拍卖行的价格比较出 最低价格
                    {
                        min_avgprice = total_item.avgprice;
                        min_refprice = total_item.refprice;
                        min_lowprice = total_item.lowprice;
                        min_highprice = total_item.highprice;
                        
                        DBItem.UpdateItem(name, min_avgprice, min_refprice, min_lowprice, min_highprice, System.DateTime.Now, FWQ, t);     //存最低价格
                    }
                }
                

            }
            else
            {   
                //各种原材料直接将参考拍卖行的价格
                UpdateItemFromInSale(name.Trim(), FWQ);
            }

            return cnt;
        }

        public static void CompareCost(DBFormula formula)
        {

        }

        public static float UpdateItemFromInSale(string name, string FWQ)
        {
            float avgprice = float.MaxValue;
            List<DBItemInSale> lst_iteminsale = DBItemInSale.GetItemInSaleByName(name.Trim(), FWQ);
            if (lst_iteminsale.Count > 0)
            {
                DBItemInSale iteminsale = lst_iteminsale[0];
                avgprice = iteminsale.avgprice;
                DBItem.UpdateItem(iteminsale.name, iteminsale.avgprice, iteminsale.avgprice, iteminsale.lowprice, iteminsale.highprice, System.DateTime.Now, iteminsale.FWQ, Constant.FORMULAAH);
            }
            return avgprice;
        }

        private static void GetAllMaterial(string output, string FWQ, int num = 1)
        {
            DBItem item = DBItem.GetItemByName(output, FWQ);
            
            try
            {
                if (item.formula < Constant.FORMULAAH)
                {
                    List<DBFormula> lst_formula = DBFormula.GetFormulaByOutputAndType(output, item.formula);
                    foreach (DBFormula formula in lst_formula)
                    {
                        if (m_manufacturingcycle < formula.CDminite * num)
                        {
                            m_manufacturingcycle = formula.CDminite * num;
                        }

                        GetAllMaterial(formula.input, FWQ, formula.inputnum * num);
                    }
                }
                else if (item.formula == Constant.FORMULAAH)
                {

                    DataRow dr = m_formuladt.NewRow();
                    dr["name"] = item.name;
                    dr["avgprice"] = item.avgprice;
                    dr["lowprice"] = item.lowprice;
                    dr["highprice"] = item.highprice;
                    dr["updatetime"] = item.updatetime;
                    dr["FWQ"] = item.FWQ;
                    dr["num"] = num;
                    dr["total"] = item.avgprice * num;
                    m_formuladt.Rows.Add(dr);
                }
            }
            catch (Exception ex)
            {

            }
        }

        public static DataTable m_formuladt = null;
        public static DataTable m_outputdt = null;

        public static void GetAllMaterialByOutput(string output, string FWQ, int num = 1)
        {
            try
            {
                if (m_formuladt == null)
                {
                    m_formuladt = new DataTable("TABLE_ITEMINPUT");
                    m_formuladt.Columns.Add("name", typeof(string));
                    m_formuladt.Columns.Add("avgprice", typeof(float));
                    m_formuladt.Columns.Add("lowprice", typeof(float));
                    m_formuladt.Columns.Add("highprice", typeof(float));
                    m_formuladt.Columns.Add("updatetime", typeof(DateTime));
                    m_formuladt.Columns.Add("FWQ", typeof(string));
                    m_formuladt.Columns.Add("num", typeof(int));
                    m_formuladt.Columns.Add("total", typeof(float));
                }
                if (m_outputdt == null)
                {
                    m_outputdt = new DataTable("TABLE_ITEMOUTPUT");
                    m_outputdt.Columns.Add("name", typeof(string));
                    m_outputdt.Columns.Add("avgprice", typeof(float));
                    m_outputdt.Columns.Add("lowprice", typeof(float));
                    m_outputdt.Columns.Add("highprice", typeof(float));
                    m_outputdt.Columns.Add("updatetime", typeof(DateTime));
                    m_outputdt.Columns.Add("FWQ", typeof(string));
                    m_outputdt.Columns.Add("num", typeof(int));
                    m_outputdt.Columns.Add("avgprofit", typeof(float));
                    m_outputdt.Columns.Add("avgprofitrate", typeof(string));
                    m_outputdt.Columns.Add("manufacturingcycle", typeof(string));
                    m_outputdt.Columns.Add("avgsaleprice", typeof(float));
                }
                m_manufacturingcycle = 0;
                m_formuladt.Clear();
                m_outputdt.Clear();

                GetAllMaterial(output, FWQ, num);
                CalcOutput(output, FWQ);
            }
            catch (Exception ex)
            {

            }
            
        }

        public static void CalcOutput(string output, string FWQ)
        {
            DataRow dr = m_outputdt.NewRow();
            try
            {
                List<DBItemInSale> lst_iteminsale = DBItemInSale.GetItemInSaleByName(output.Trim(), FWQ);
                if (lst_iteminsale.Count > 0)
                {
                    DBItemInSale iteminsale = lst_iteminsale[0];
                    DBItem item = DBItem.GetItemByName(output, FWQ);
                    float profit = iteminsale.avgprice - item.avgprice;
                    dr["name"] = output;
                    dr["avgprice"] = item.avgprice;
                    dr["lowprice"] = item.lowprice;
                    dr["highprice"] = item.highprice;
                    dr["updatetime"] = item.updatetime;
                    dr["FWQ"] = item.FWQ;
                    dr["num"] = iteminsale.avgnum;
                    dr["avgsaleprice"] = iteminsale.avgprice;
                    dr["avgprofit"] = profit;
                    dr["avgprofitrate"] = profit / item.avgprice;
                    TimeSpan ts = new TimeSpan(0, m_manufacturingcycle, 0);
                    dr["manufacturingcycle"] = String.Format("{0}天", ts.Days);
                    m_outputdt.Rows.Add(dr);
                }
            }
            catch (Exception ex)
            {

            }
            
        }

    }
}