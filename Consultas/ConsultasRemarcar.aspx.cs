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


    protected void grdMain_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index;
        
        if (e.CommandName.Equals("editRecord"))
        {
            index = Convert.ToInt32(e.CommandArgument);
           
            int _id_consulta = Convert.ToInt32(GridView1.DataKeys[index].Value.ToString()); //id da consulta

            GridViewRow row = GridView1.Rows[index];
            string _status = row.Cells[7].Text;

            Response.Redirect("~/Consultas/InformaCancelamento.aspx?idconsulta=" + _id_consulta + "&status=" + _status + "");
        }
    }
}