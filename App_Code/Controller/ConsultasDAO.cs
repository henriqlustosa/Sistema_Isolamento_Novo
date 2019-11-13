using System;
using System.Data;
using System.Configuration;
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

/// <summary>
/// Summary description for ConsultasDAO
/// </summary>
public class ConsultasDAO
{
	public ConsultasDAO()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public static List<ConsultasRemarcar> ListaConsultasCancelar(int _status)
    {
        // colocar regra para listar consultas do paciente com tentativas abaixo de 3 (terceira tentativa)

        var lista = new List<ConsultasRemarcar>();

        using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["gtaConnectionString"].ToString()))
        {
            SqlCommand cmm = cnn.CreateCommand();

            cmm.CommandText = "SELECT [id_consulta]" +
                              ",[prontuario]" +
                              ",[nome_paciente]" +
                              ",[equipe]" +
                              ",[dt_consulta]" +
                              ",[codigo_consulta]" +
                              ",[desc_status]" +
                              ",[observacao]" +
                              ",[data_ligacao]" +
                              ",[stat]" +
                              ",[status]" +
                              " FROM [vw_cancelar_consultas] " +
                              " WHERE stat = 0 " +
                              " AND status = " + _status;
            try
            {
                cnn.Open();
                SqlDataReader dr1 = cmm.ExecuteReader();

                //char[] ponto = { '.', ' ' };
                while (dr1.Read())
                {
                    ConsultasRemarcar consulta = new ConsultasRemarcar();
                    consulta.Id_Consulta = dr1.GetInt32(0);
                    consulta.Prontuario = dr1.GetInt32(1);
                    consulta.Nome = dr1.GetString(2);
                    consulta.Equipe = dr1.GetString(3);
                    consulta.Dt_Consulta = dr1.GetDateTime(4).ToString();
                    consulta.Codigo_Consulta = dr1.GetInt32(5);
                    consulta.Status = dr1.GetString(6);
                    consulta.Observacao = dr1.GetString(7);
                    consulta.Data_Contato = dr1.GetDateTime(8);

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
