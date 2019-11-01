<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Chart2.aspx.cs" Inherits="publico_Chart2" Title="Untitled Page" %>

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
            <div class="well" style="overflow: auto">
                <div class="col-md-6">
                    <asp:DropDownList ID="ddl" runat="server" 
                        OnSelectedIndexChanged="ddl_SelectedIndexChanged" AutoPostBack="True">
                    </asp:DropDownList>
                    <asp:TextBox ID="txbData" runat="server"></asp:TextBox>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    
    
    <asp:Label ID="lbMsg" runat="server" Text=""></asp:Label>
    <div>
       <input id="1" type="button" onclick="gerarGrafico()" value="Executar" />
        
    </div>
    
    
    <div class="x_content">
        <div>
            <canvas id="myChart" style="height: 50px; width: 150px"></canvas>
        </div>
        
        <div>
            <canvas id="myChart2" style="height: 50px; width: 150px"></canvas>
        </div>
    </div>

    <script type="text/javascript">
        $(document).ready(function() {
            $("#myChart").hide();
        //var dataChart = GetData(12);

        });


        var ctx = document.getElementById('myChart').getContext('2d');
        //var tData = GetData(10);
        var myChart = new Chart(ctx, {
            type: 'line',
            data: []
        });


       
        
        var ctx = document.getElementById('myChart2').getContext('2d');
        //var tData = GetData(10);
        var myChart = new Chart(ctx, {
            type: 'line',
            data: JSON.parse(GetData(JSON.stringify('22/10/2019')))
        });
        
        function gerarGrafico() {
            
           var dia = document.getElementById('<%=txbData.ClientID %>');

           //alert("Valor: " + JSON.stringify(dia.value));
           updateChart(JSON.stringify(dia.value))
        }

        function updateChart(data) {
            $("#myChart").show();
            myChart.data = JSON.parse(GetData(data));
            myChart.update();
        };
        
        function GetData(data) {
            var d = data;
            
            var result = null;
            $.ajax({
                async: false
                , url: '<%= ResolveUrl("~/publico/webservice.asmx/ChamadasDia") %>'
                , data: '{data : '+ d +'}'
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

           
    </script>
</asp:Content>
