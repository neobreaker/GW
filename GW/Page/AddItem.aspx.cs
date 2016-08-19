using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GW.Ctrl;

namespace GW.Page
{
    public partial class AddItem : System.Web.UI.Page
    {
        private bool m_isbinding = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }


        protected void CheckBoxBind_CheckedChanged(object sender, EventArgs e)
        {
            if (this.CheckBoxBind.Checked)
            {
                m_isbinding = true;
            }
            else
            {
                m_isbinding = false;
            }
        }

        protected void ButtonAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string item_name = this.TextBoxItem.Text.Trim();
                if (item_name.Equals(""))
                {
                    this.LabelHint.Text = "物品名不能为空";
                    this.LabelHint.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    string FWQ = this.Label1.Text.Trim();
                    Core.AddItem(item_name, FWQ);
                    if (!m_isbinding)
                    {
                        Core.AddItemInSale(item_name, FWQ);
                    }
                    this.LabelHint.ForeColor = System.Drawing.Color.Green;
                    this.LabelHint.Text = "添加成功";
                }
                
            }
            catch(Exception ex)
            {
                this.LabelHint.Text = ex.Message;
                this.LabelHint.ForeColor = System.Drawing.Color.Red;
            }
            
        }
    }
}