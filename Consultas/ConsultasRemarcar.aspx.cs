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

    protected void GridView1_PreRender(object sender, EventArgs e)
    {
        
        // colocar no grid OnPreRender="GridView1_PreRender"

        int _stat = Convert.ToInt32(ddlStatus.SelectedValue.ToString());
        // You only need the following 2 lines of code if you are not 
        // using an ObjectDataSource of SqlDataSource
        GridView1.DataSource = ConsultasDAO.ListaConsultasCancelar(_stat);
        GridView1.DataBind();

        if (GridView1.Rows.Count > 0)
        {
            //This replaces <td> with <th> and adds the scope attribute
            GridView1.UseAccessibleHeader = true;

            //This will add the <thead> and <tbody> elements
            GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;

            //This adds the <tfoot> element. 
            //Remove if you don't have a footer row
            GridView1.FooterRow.TableSection = TableRowSection.TableFooter;

        }
    }
}