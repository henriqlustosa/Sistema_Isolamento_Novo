using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Data.SqlClient;

public partial class publico_ConsultasRemarcar : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
      
    }

    protected void btnListar_Click(object sender, EventArgs e)
    {
         int _stat = Convert.ToInt32(ddlStatus.SelectedValue.ToString());
                
         GridView1.DataSource = ConsultasDAO.ListaConsultasCancelar(_stat);
         GridView1.DataBind();
    }
}