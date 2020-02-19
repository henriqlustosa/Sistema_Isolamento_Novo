<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="dadosMes.aspx.cs" Inherits="Grafico_dadosMes" Title="Call HSPM" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script src='<%= ResolveUrl("https://cdnjs.cloudflare.com/ajax/libs/jquery/2.2.4/jquery.min.js") %>'
        type="text/javascript"></script>

    <script src='<%= ResolveUrl("https://cdn.jsdelivr.net/npm/chart.js@2.8.0") %>' type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Label ID="lbMsg" runat="server" Text=""></asp:Label>
            <div class="row">
                <label class="control-label">
                    <h6>
                        Informe mês e ano:</h6>
                </label>
                <div class="col-md-4 col-sm-12 col-xs-12">
                    <div class="col-md-4">
                        <asp:TextBox ID="txbData" runat="server" class="form-control" data-inputmask="'mask': '99/9999'"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredValidator" runat="server" ControlToValidate="txbData"
                            ForeColor="red" Display="Static" ErrorMessage="Required" /><br />
                    </div>
                    <div class="col-md-2">
                        <input runat="server" type="button" onclick="gerarGrafico()" value="Gráfico" class="btn btn-success" />
                    </div>
                    <div class="col-md-2">
                        <input id="Button1" runat="server" type="button" value="Limpar" onclick="reloadPage()" class="btn btn-primary" />
                    </div>
                </div>
            </div>
            <div id="graficos">
                <div class="row">
                    <div class="col-md-6 col-sm-12 col-xs-12">
                        <div class="x_panel">
                            <div class="x_title">
                                <h2>
                                    Gráfico Status de Ativos</h2>
                                <div class="clearfix">
                                </div>
                            </div>
                            <div class="x_content">
                                <div>
                                    <canvas id="myChartPie" style="height: 116px; width: 150px"></canvas>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!-- novo grafico -->
                    <div class="col-md-6 col-sm-12 col-xs-12">
                        <div class="x_panel tile ">
                            <div class="x_title">
                                <h2>
                                    Quantitativo Status de Ativos</h2>
                                <div class="clearfix">
                                    <table class="table">
                                        <thead class="thead-dark">
                                            <tr>
                                                <th>
                                                    Descricao
                                                </th>
                                                <th>
                                                    Quantidade
                                                </th>
                                            </tr>
                                        </thead>
                                        <tbody id="tdata">
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                            <div class="x_content">
                                <canvas id="myChart" style="height: 116px; width: 150px"></canvas>
                            </div>
                        </div>
                    </div>
                    <!-- -->
                </div>
                <div class="row">
                    <!-- novo grafico -->
                    <div class="col-md-12 col-sm-12 col-xs-12">
                        <div class="x_panel tile ">
                            <div class="x_title">
                                <h2>
                                    Ativos Mensal</h2>
                                <div class="clearfix">
                                </div>
                            </div>
                            <div class="x_content">
                                <canvas id="myChartLine" style="height: 50px; width: 200px"></canvas>
                            </div>
                        </div>
                    </div>
                    <!-- -->
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <script type="text/javascript">
        $(document).ready(function() {
            $("#myChartPie").hide();
            $("#myChart").hide();
            $("#myChartLine").hide();
            $("#graficos").hide();
        });


        function gerarGrafico() {
            var dia = document.getElementById('<%=txbData.ClientID %>');
            
            $("#graficos").show();
            updateChartPie(dia.value);
            updateChartLine(dia.value);
            dadosMesStatus(dia.value);
        }

        var ctx = document.getElementById('myChartPie').getContext('2d');
        var myChartPie = new Chart(ctx, {
            type: 'pie',
            data: []
        });

        function updateChartPie(data) {
            $("#myChartPie").show();
            myChartPie.data = JSON.parse(GetDataPie(JSON.stringify(data)));
            myChartPie.update();
        };

        function GetDataPie(data) {
            var d = data;

            var result = null;
            $.ajax({
                async: false
                , url: '<%= ResolveUrl("~/publico/webservice.asmx/QuantidadeAtivosStatusGrafico") %>'
                , data: '{mesAno : ' + d + '}'
                , type: 'POST'
                , contentType: 'application/json; charset=utf-8'
                , dataType: 'json'
                , success: function(data) {
                    result = data.d;
                }
                , error: function(xhr, er) {
                    $("#lbMsg").html('<p> Erro ' + xhr.staus + ' - ' + xhr.statusText + ' - <br />Tipo de erro:  ' + er + '</p>');
                }
            });
            return result;
        }

        var ctx = document.getElementById('myChartLine').getContext('2d');
        var myChartLine = new Chart(ctx, {
            type: 'line',
            data: []
        });

        function updateChartLine(data) {
            $("#myChartLine").show();
            myChartLine.data = JSON.parse(GetDataLine(JSON.stringify(data)));
            myChartLine.update();
        };

        function GetDataLine(data) {
            var d = data;

            var result = null;
            $.ajax({
                async: false
                , url: '<%= ResolveUrl("~/publico/webservice.asmx/ChamadasMes") %>'
                , data: '{mesAno : ' + d + '}'
                , type: 'POST'
                , contentType: 'application/json; charset=utf-8'
                , dataType: 'json'
                , success: function(data) {
                    result = data.d;
                }
                , error: function(xhr, er) {
                    $("#lbMsg").html('<p> Erro ' + xhr.staus + ' - ' + xhr.statusText + ' - <br />Tipo de erro:  ' + er + '</p>');
                }
            });
            return result;
        }

        function dadosMesStatus(data) {
            var d = JSON.stringify(data);
            $.ajax({
                async: false
                                , url: '<%= ResolveUrl("~/publico/webservice.asmx/QuantidadeAtivosStatus") %>'
                                , data: '{mesAno : ' + d + '}'
                                , type: 'POST'
                                , contentType: 'application/json; charset=utf-8'
                                , dataType: 'json'
                                , success: function(data) {
                                    var data = JSON.parse(data.d);
                                    data.forEach(function(dt) {
                                        $("#tdata").append("<tr>" +
                                        "<td>" + dt.descricao + "</td>" +
                                        "<td>" + dt.quantidade + "</td>"
                                        + "</tr>"
                                        );
                                    });

                                }
                                , error: function(xhr, er) {
                                    $("#lbMsg").html('<p> Erro ' + xhr.staus + ' - ' + xhr.statusText + ' - <br />Tipo de erro:  ' + er + '</p>');
                                }
            });
        }

        function reloadPage() {
            window.location.reload()
        }
        
    </script>

</asp:Content>
