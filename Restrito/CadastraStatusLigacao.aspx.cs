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

public partial class Restrito_CadastraStatusLigacao : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindRepeator();
        }
    }
    protected void btnCadastrar_Click(object sender, EventArgs e)
    {
        string _descricaoStatus = Request.Form["descricaoStatus"].ToString();
        string _tentativa = Request.Form["tenta"].ToString();
        string _ativo = Request.Form["ativo"].ToString();

        string msg = StatusConsultaDAO.GravaStatus(_descricaoStatus, _tentativa, _ativo);

        lbMensagem.Text = msg;

        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none",
                    "<script>$('#modalMsg').modal('show');</script>", false);
        BindRepeator();
    }

    private void BindRepeator()
    {
        rpt.DataSource = StatusConsultaDAO.listaStatusConsulta();
        rpt.DataBind();
    }
}
