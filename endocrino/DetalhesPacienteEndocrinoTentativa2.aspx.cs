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

public partial class endocrino_DetalhesPacienteEndocrinoTentativa2 : System.Web.UI.Page
{
    public int tenta { get; set; }
    public int id_ativo { get; set; }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int prontuario = Convert.ToInt32(Request.QueryString["prontuario"]);
            int tentativa = 1;

            tenta = tentativa;

            lbProntuario.Text = prontuario.ToString();


            PacienteMailling paciente = new PacienteMailling();
            paciente = PacienteMailingDAO.getDadosPaciente(prontuario);

            txbNomePaciente.Text = paciente.Nome;
            txbNomePacienteModal.Text = paciente.Nome;
            txbTelefone1.Text = paciente.Telefone1;
            txbTelefone2.Text = paciente.Telefone2;
            txbTelefone3.Text = paciente.Telefone3;
            txbTelefone4.Text = paciente.Telefone4;

            BindGrind1Tentativas(prontuario, tentativa);

        }
    }

    private void BindGrind1Tentativas(int _prontuario, int _tentativa)
    {
        string _realizada = "N";
        GridView1.DataSource = AtivoDAO.ListaTentativaContatoPaciente(_prontuario, _tentativa, _realizada);
        GridView1.DataBind();
    }

    protected void grdMain_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index;

        if (e.CommandName.Equals("editRecord"))
        {
            index = Convert.ToInt32(e.CommandArgument);
            id_ativo = Convert.ToInt32(GridView1.DataKeys[index].Value.ToString());


            string _realizada = "N";
            var clickedRecord = from consulta in AtivoDAO.ListaTentativaContatoPaciente(Convert.ToInt32(lbProntuario.Text), 1, _realizada)
                                where consulta.Id_Ativo == id_ativo
                                select consulta;


            foreach (var d in clickedRecord.ToList())
            {
                txbID.Text = d.Id_Ativo.ToString();
                txbdtConsulta.Text = d.Dt_Consulta.ToString();
                txbEquipe.Text = d.Equipe;
                txbProfissional.Text = d.Nome_Profissional;
                txbCodConsulta.Text = d.Codigo_Consulta.ToString();
            }

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none",
                    "<script>$('#editModal').modal('show');</script>", false);
        }
    }

    protected void btnAtualizaTelefones_Click(object sender, EventArgs e)
    {
        PacienteMailling paciente = new PacienteMailling();

        paciente.Prontuario = Convert.ToInt32(lbProntuario.Text);
        paciente.Telefone1 = txbTelefone1.Text;
        paciente.Telefone2 = txbTelefone2.Text;
        paciente.Telefone3 = txbTelefone3.Text;
        paciente.Telefone4 = txbTelefone4.Text;

        string mensagem = PacienteMailingDAO.AtualizaTelefones(paciente.Prontuario, paciente.Telefone1, paciente.Telefone2, paciente.Telefone3, paciente.Telefone4);
        ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + mensagem + "');", true);
    }


    protected void btnGravar_Click(object sender, EventArgs e)
    {
        int _prontuario = Convert.ToInt32(lbProntuario.Text);
        int _status = Convert.ToInt32(ddlStatus.SelectedValue);
        string _observacao = txbObservacao.Text.ToUpper();
        string _usuario = System.Web.HttpContext.Current.User.Identity.Name;
        int _id_consulta = Convert.ToInt32(txbID.Text);
        id_ativo = Convert.ToInt32(txbID.Text);

        int tentativa = 2;

        // update realizado

        string mensagem = AtivoDAO.GravaStatusAtivo(_status, _observacao, _usuario, _id_consulta, tentativa, id_ativo);

        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("$('.modal-backdrop').remove();");
        sb.Append("$(document.body).removeClass('modal-open');");
        ScriptManager.RegisterStartupScript(Page, this.Page.GetType(), "clientscript", sb.ToString(), true);

        //BindGrind1Tentativas(_prontuario, 1);// recarrega o grid        
        ClearInputs(Page.Controls);// limpa os textbox
        Response.Redirect("~/endocrino/ListaAtivosEquipe2.aspx");
    }


    void ClearInputs(ControlCollection ctrls)
    {
        foreach (Control ctrl in ctrls)
        {
            if (ctrl is TextBox)
                ((TextBox)ctrl).Text = string.Empty;
            ClearInputs(ctrl.Controls);
        }
    }

    protected void btnVoltar2_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/endocrino/ListaAtivosEquipe2.aspx");
    }
}
