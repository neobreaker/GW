using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GW.Ctrl;
using System.Data;

namespace GW.Page
{
    public partial class Test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }

        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            if (this.TextBoxSearch.Text.Trim() != "")
            {
                Core.GetCostByName(this.TextBoxSearch.Text.Trim(), "克尔苏加德");
                Core.GetAllMaterialByOutput(this.TextBoxSearch.Text.Trim(), "克尔苏加德");
                this.GridViewOutputItem.DataSource = Core.m_outputdt;
                this.GridViewOutputItem.DataBind();
                this.GridViewSearch.DataSource = Core.m_formuladt;
                this.GridViewSearch.DataBind();
            }
            
        }

    }
}