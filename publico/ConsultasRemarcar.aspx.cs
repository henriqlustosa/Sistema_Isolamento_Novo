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
        gvCarregaConsultasRemarcar();
    }

    private void gvCarregaConsultasRemarcar()
    {
        GridView1.DataSource = ListaConsultasRemarcar();
        GridView1.DataBind();
    }

    public static List<ConsultasRemarcar> ListaConsultasRemarcar()
    {
        // colocar regra para listar consultas do paciente com tentativas abaixo de 3 (terceira tentativa)

        var lista = new List<ConsultasRemarcar>();
        
        using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["gtaConnectionString"].ToString()))
        {
            SqlCommand cmm = cnn.CreateCommand();

            cmm.CommandText = "SELECT [data_ligacao] " +
                                  ",[nome_paciente] "+
                                  ",[telefone1] "+
                                  ",[telefone2] "+
                                  ",[telefone3] "+
                                  ",[telefone4] "+
                                  ",[status] "+
                                  ",[usuario] "+
                                  ",[prontuario] "+
                                  ",[grade] "+
                                  ",[equipe] "+
                                  ",[dt_consulta] "+
                                  ",[profissional] "+
                                  ",[codigo_consulta] "+
                                  ",[observacao] "+
                              "FROM [hspmCall].[dbo].[vw_relatorio_ativos] "+
                              "WHERE status = 'CANCELAR E REMARCAR' "+
                              "AND data_ligacao >= GETDATE() - 10 "+
                              "order by data_ligacao";
            try
            {
                cnn.Open();
                SqlDataReader dr1 = cmm.ExecuteReader();

                //char[] ponto = { '.', ' ' };
                while (dr1.Read())
                {
                    ConsultasRemarcar consulta = new ConsultasRemarcar();

                    consulta.Data_Contato = dr1.GetDateTime(0);
                    consulta.Nome = dr1.GetString(1);
                    consulta.Telefone1 = dr1.GetString(2);
                    consulta.Telefone2 = dr1.GetString(3);
                    consulta.Telefone3 = dr1.GetString(4);
                    consulta.Telefone4 = dr1.GetString(5);
                    consulta.Status = dr1.GetString(6);
                    consulta.Usuario_Contato = dr1.GetString(7);
                    consulta.Prontuario = dr1.GetInt32(8);
                    consulta.Grade = dr1.GetInt32(9);
                    consulta.Equipe = dr1.GetString(10);
                    consulta.Dt_Consulta = dr1.GetDateTime(11).ToString();
                    consulta.Nome_Profissional = dr1.GetString(12);
                    consulta.Codigo_Consulta = dr1.GetInt32(13);
                    consulta.Observacao = dr1.GetString(14);
                    
                    lista.Add(consulta);
                }
            }
            catch (Exception ex)
            {
                string error = ex.Message;
            }
        }
        return lista;
    }

}
