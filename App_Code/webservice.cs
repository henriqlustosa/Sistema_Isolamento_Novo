using System;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics;
using System.Web.Script.Services;
using System.Web.Script.Serialization;
using System.Collections.Generic;
using System.Configuration;

[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class webservice : System.Web.Services.WebService
{


    //Retorna o formato JSON
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    [WebMethod()]
    public string RetornaChamadasDia()
    {
        //Lista
        var lista = new List<CallHoras>();

        using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["gtaConnectionString"].ToString()))
        {
            SqlCommand cmm = cnn.CreateCommand();

            cmm.CommandText = "SELECT DATEPART(HOUR, data_ligacao) AS [HourofDay], COALESCE(COUNT(id_consulta), 0) AS [Total Calls] "+
                            " FROM [hspmCall].[dbo].[ativo_ligacao] "+
                            " WHERE day(data_ligacao) = 11 "+
                            " GROUP BY DATEPART(HOUR, data_ligacao) "+
                            " ORDER BY DATEPART(HOUR, data_ligacao)";
            try
            {
                cnn.Open();
                SqlDataReader dr1 = cmm.ExecuteReader();

                //char[] ponto = { '.', ' ' };
                while (dr1.Read())
                {
                    CallHoras call = new CallHoras();

                    call.Horadodia = dr1.GetInt32(0);
                    call.TotalCall = dr1.GetInt32(1);

                    lista.Add(call);
                }
            }
            catch (Exception ex)
            {
                string error = ex.Message;
            }
        }

        //O JavaScriptSerializer vai fazer o web service retornar JSON
        JavaScriptSerializer js = new JavaScriptSerializer();
        return js.Serialize(lista);
    }

    //Retorna o formato JSON
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    [WebMethod()]
    public string ChamadasDia(string data)
    {
        string _dia = data;
        //string d = "18/10/2019";

        var horas = new List<string>();
        var total = new List<int>();

        using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["gtaConnectionString"].ToString()))
        {
            SqlCommand cmm = cnn.CreateCommand();

            cmm.CommandText = "SELECT DATEPART(HOUR, data_ligacao) AS [HourofDay], COALESCE(COUNT(id_consulta), 0) AS [Total Calls] " +
                            " FROM [hspmCall].[dbo].[ativo_ligacao] " +
                            " WHERE CONVERT(varchar(12),data_ligacao, 103) = '" + _dia + "' GROUP BY DATEPART(HOUR, data_ligacao) " +
                            " ORDER BY DATEPART(HOUR, data_ligacao)";
            try
            {
                cnn.Open();
                SqlDataReader dr1 = cmm.ExecuteReader();

                while (dr1.Read())
                {
                    CallHoras call = new CallHoras();
                    call.Horadodia = dr1.GetInt32(0);
                    call.TotalCall = dr1.GetInt32(1);

                    horas.Add(call.Horadodia.ToString() + ":00");

                    total.Add(call.TotalCall);

                }
                
            }
            catch (Exception ex)
            {
                string error = ex.Message;
            }
        }

        Chart _chart = new Chart();
        _chart.labels =   horas.ToArray();
        _chart.datasets = new List<Datasets>();

        List<Datasets> _dataSet = new List<Datasets>();
        _dataSet.Add(new Datasets()
        {
            label = "Ativos de Hoje",
            data = total.ToArray(),
            backgroundColor = new string[] { "rgba(38, 185, 154, 0.31)" },
            borderColor = new string[] { "rgba(38, 185, 154, 0.7)" },

            pointBorderColor = new string[] {"rgba(38, 185, 154, 0.7)"},
			pointBackgroundColor = new string[] { "rgba(38, 185, 154, 0.7)"},
			pointHoverBackgroundColor = new string[] { "#fff"},
			pointHoverBorderColor = new string[] { "rgba(220,220,220,1)"},
			pointBorderWidth = "1",
            borderWidth = "1"
        });

        _chart.datasets = _dataSet;


        //O JavaScriptSerializer vai fazer o web service retornar JSON
        JavaScriptSerializer js = new JavaScriptSerializer();
        return js.Serialize(_chart);
    }


    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    [WebMethod()]
    public string ChamadasOntem()
    {
        var horas = new List<string>();
        var total = new List<int>();

        using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["gtaConnectionString"].ToString()))
        {
            SqlCommand cmm = cnn.CreateCommand();

            cmm.CommandText = "SELECT DATEPART(HOUR, data_ligacao) AS [HourofDay], COALESCE(COUNT(id_consulta), 0) AS [Total Calls] " +
                            " FROM [hspmCall].[dbo].[ativo_ligacao] " +
                            " WHERE DATEDIFF(day, GETDATE() , data_ligacao) = - 1 "+
                            " GROUP BY DATEPART(HOUR, data_ligacao) " +
                            " ORDER BY DATEPART(HOUR, data_ligacao)";
            try
            {
                cnn.Open();
                SqlDataReader dr1 = cmm.ExecuteReader();

                while (dr1.Read())
                {
                    CallHoras call = new CallHoras();
                    call.Horadodia = dr1.GetInt32(0);
                    call.TotalCall = dr1.GetInt32(1);

                    horas.Add(call.Horadodia.ToString() + ":00");
                    total.Add(call.TotalCall);

                }

            }
            catch (Exception ex)
            {
                string error = ex.Message;
            }
        }

        Chart _chart = new Chart();
        _chart.labels = horas.ToArray();
        _chart.datasets = new List<Datasets>();

        List<Datasets> _dataSet = new List<Datasets>();
        _dataSet.Add(new Datasets()
        {
            label = "Ativos de Ontem",
            data = total.ToArray(),
            backgroundColor = new string[] { "rgba(38, 185, 154, 0.31)" },
            borderColor = new string[] { "rgba(38, 185, 154, 0.7)" },

            pointBorderColor = new string[] { "rgba(38, 185, 154, 0.7)" },
            pointBackgroundColor = new string[] { "rgba(38, 185, 154, 0.7)" },
            pointHoverBackgroundColor = new string[] { "#fff" },
            pointHoverBorderColor = new string[] { "rgba(220,220,220,1)" },
            pointBorderWidth = "1",
            borderWidth = "1"
        });

        _chart.datasets = _dataSet;

        //O JavaScriptSerializer vai fazer o web service retornar JSON
        JavaScriptSerializer js = new JavaScriptSerializer();
        return js.Serialize(_chart);
    }

    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    [WebMethod()]
    public string ChamadasSeteDias()
    {
        var horas = new List<string>();
        var total = new List<int>();

        using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["gtaConnectionString"].ToString()))
        {
            SqlCommand cmm = cnn.CreateCommand();

            cmm.CommandText = "SELECT DATEPART(DAY, data_ligacao) AS [Day], COALESCE(COUNT(id_consulta), 0) AS [Total Calls] " +
                            " FROM [hspmCall].[dbo].[ativo_ligacao] " +
                            " WHERE data_ligacao between (CONVERT(datetime, GETDATE() - 6, 103)) AND (CONVERT(datetime, GETDATE(), 103)) " +
                            " GROUP BY DATEPART(DAY, data_ligacao), DATEPART(MONTH, data_ligacao) " +
                            " ORDER BY DATEPART(MONTH, data_ligacao) asc ";
            try
            {
                cnn.Open();
                SqlDataReader dr1 = cmm.ExecuteReader();

                while (dr1.Read())
                {
                    CallHoras call = new CallHoras();
                    call.Horadodia = dr1.GetInt32(0);
                    call.TotalCall = dr1.GetInt32(1);

                    horas.Add(call.Horadodia.ToString());
                    total.Add(call.TotalCall);

                }

            }
            catch (Exception ex)
            {
                string error = ex.Message;
            }
        }

        Chart _chart = new Chart();
        _chart.labels = horas.ToArray();
        _chart.datasets = new List<Datasets>();

        List<Datasets> _dataSet = new List<Datasets>();
        _dataSet.Add(new Datasets()
        {
            label = "Ativos Últimos 7 Dias",
            data = total.ToArray(),
            backgroundColor = new string[] { "rgba(38, 185, 154, 0.31)" },
            borderColor = new string[] { "rgba(38, 185, 154, 0.7)" },

            pointBorderColor = new string[] { "rgba(38, 185, 154, 0.7)" },
            pointBackgroundColor = new string[] { "rgba(38, 185, 154, 0.7)" },
            pointHoverBackgroundColor = new string[] { "#fff" },
            pointHoverBorderColor = new string[] { "rgba(220,220,220,1)" },
            pointBorderWidth = "1",
            borderWidth = "1"
        });

        _chart.datasets = _dataSet;

        //O JavaScriptSerializer vai fazer o web service retornar JSON
        JavaScriptSerializer js = new JavaScriptSerializer();
        return js.Serialize(_chart);
    }

    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    [WebMethod()]
    public string ChamadasEsteMes()
    {
        var horas = new List<string>();
        var total = new List<int>();

        using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["gtaConnectionString"].ToString()))
        {
            SqlCommand cmm = cnn.CreateCommand();

            cmm.CommandText = "SELECT DATEPART(DAY, data_ligacao) AS [Day], COALESCE(COUNT(id_consulta), 0) AS [Total Calls] " +
                            " FROM [hspmCall].[dbo].[ativo_ligacao] " +
                            " WHERE data_ligacao between (CONVERT(datetime, (DATEADD(mm, DATEDIFF(mm, 0, GETDATE()), 0)) - 0, 103)) AND (CONVERT(datetime, GETDATE(), 103)) " +
                            " GROUP BY DATEPART(DAY, data_ligacao) " +
                            " ORDER BY DATEPART(DAY, data_ligacao)";
            try
            {
                cnn.Open();
                SqlDataReader dr1 = cmm.ExecuteReader();

                while (dr1.Read())
                {
                    CallHoras call = new CallHoras();
                    call.Horadodia = dr1.GetInt32(0);
                    call.TotalCall = dr1.GetInt32(1);

                    horas.Add(call.Horadodia.ToString());
                    total.Add(call.TotalCall);

                }

            }
            catch (Exception ex)
            {
                string error = ex.Message;
            }
        }

        Chart _chart = new Chart();
        _chart.labels = horas.ToArray();
        _chart.datasets = new List<Datasets>();

        List<Datasets> _dataSet = new List<Datasets>();
        _dataSet.Add(new Datasets()
        {
            label = "Ativos Este Mês",
            data = total.ToArray(),
            backgroundColor = new string[] { "rgba(38, 185, 154, 0.31)" },
            borderColor = new string[] { "rgba(38, 185, 154, 0.7)" },

            pointBorderColor = new string[] {"rgba(38, 185, 154, 0.7)"},
			pointBackgroundColor = new string[] { "rgba(38, 185, 154, 0.7)"},
			pointHoverBackgroundColor = new string[] { "#fff"},
			pointHoverBorderColor = new string[] { "rgba(220,220,220,1)"},
			pointBorderWidth = "1",
            borderWidth = "1"
        });

        _chart.datasets = _dataSet;

        //O JavaScriptSerializer vai fazer o web service retornar JSON
        JavaScriptSerializer js = new JavaScriptSerializer();
        return js.Serialize(_chart);
    }

}
