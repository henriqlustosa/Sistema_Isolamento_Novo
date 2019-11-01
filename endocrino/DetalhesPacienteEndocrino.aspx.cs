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

public partial class endocrino_DetalhesPacienteEndocrino : System.Web.UI.Page
{
    private int _tenta { get; set; }
    private int _id_ativo { get; set; }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            PacienteMailling paciente = new PacienteMailling();

            int prontuario = Convert.ToInt32(Request.QueryString["prontuario"]);
            int tentativa = Convert.ToInt32(Request.QueryString["tentativa"]);
            _tenta = tentativa;


            lbProntuario.Text = prontuario.ToString();

            paciente = PacienteMailingDAO.getDadosPaciente(prontuario);

            txbNomePaciente.Text = paciente.Nome;
            txbNomePacienteModal.Text = paciente.Nome;
            txbTelefone1.Text = paciente.Telefone1;
            txbTelefone2.Text = paciente.Telefone2;
            txbTelefone3.Text = paciente.Telefone3;
            txbTelefone4.Text = paciente.Telefone4;

            BindGrind(prontuario);
        }
    }

    private void BindGrind(int _prontuario)
    {
        GridView1.DataSource = AtivoDAO.ListaConsultasPaciente(_prontuario);
        GridView1.DataBind();
    }


    protected void grdMain_RowCommand(object sender, GridViewCommandEventArgs e)
    {


        int index;

        if (e.CommandName.Equals("editRecord"))
        {
            index = Convert.ToInt32(e.CommandArgument);

            int ID = Convert.ToInt32(GridView1.DataKeys[index].Value.ToString());

            var clickedRecord = from consulta in AtivoDAO.ListaConsultasPaciente(Convert.ToInt32(lbProntuario.Text))
                                where consulta.Id_Consulta == ID
                                select consulta;

            foreach (var d in clickedRecord.ToList())
            {
                txbID.Text = d.Id_Consulta.ToString();
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

        _id_ativo = 0;

        _tenta += 1;

        string mensagem = AtivoDAO.GravaStatusAtivo(_status, _observacao, _usuario, _id_consulta, _tenta, _id_ativo);

        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("$('.modal-backdrop').remove();");
        sb.Append("$(document.body).removeClass('modal-open');");
        ScriptManager.RegisterStartupScript(Page, this.Page.GetType(), "clientscript", sb.ToString(), true);
       
        BindGrind(_prontuario);// recarrega o grid
        ClearInputs(Page.Controls);// limpa os textbox
        UpdatePanel1.Update();
        Response.Redirect("~/endocrino/ListaAtivosEquipe.aspx");
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

    protected void btnVoltar1_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/endocrino/ListaAtivosEquipe.aspx");
    }
}
