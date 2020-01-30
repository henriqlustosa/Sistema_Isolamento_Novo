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

public partial class Paciente_historico : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Msg.Text = "";

        if (!IsPostBack)
        {
            Msg.Text = "Por favor, forneça um número de prontuário.";
        }
    }

    public void SearchHistorico_OnClick(object sender, EventArgs e)
    {
        int _prontuario = Convert.ToInt32(txbProntuario.Text);

        PacienteMailling paciente = new PacienteMailling();
        paciente = PacienteMailingDAO.getDadosPaciente(_prontuario);

        lbNomePaciente.Text = paciente.Nome;
        lbTel1.Text = paciente.Telefone1;
        lbTel2.Text = paciente.Telefone2;
        lbTel3.Text = paciente.Telefone3;
        lbTel4.Text = paciente.Telefone4;

        GridView1.DataSource = ConsultasDAO.ListaConsultasPaciente(_prontuario);
        GridView1.DataBind();
    }
}