<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AjaxTable.aspx.cs" Inherits="publico_AjaxTable" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
<script src="~/Scripts/jquery-1.10.2.min.js"></script>  
<script>
    $(document).ready(function() {
        //Call EmpDetails jsonResult Method  

        var d = data;

        var result = null;
        $.ajax({
            async: false
                , url: '<%= ResolveUrl("~/publico/webservice.asmx/QuantidadeAtivosStatus") %>'
                , data: '{mes : 01, ano: 2020}'
                , type: 'POST'
                , contentType: 'application/json; charset=utf-8'
                , dataType: 'json'
                , success: function(json) {
                    var tr;
                    //Append each row to html table  
                    for (var i = 0; i < json.length; i++) {
                        tr = $('<tr/>');
                        tr.append("<td>" + json[i].descricao + "</td>");
                        tr.append("<td>" + json[i].quantidade + "</td>");

                        $('table').append(tr);
                    }
                    console.log("teste");
                }
                , error: function(xhr, er) {
                    $("#lbMsg").html('<p> Erro ' + xhr.staus + ' - ' + xhr.statusText + ' - <br />Tipo de erro:  ' + er + '</p>');
                }
        });

    });  
</script>  
<table class="table table-bordered table-condensed table-hover table-striped">  
        <thead>  
        <tr>  
        <th>descricao</th>  
        <th>quantidade</th>  
        </tr>  
        </thead>  
        <tbody></tbody>  
</table>

</asp:Content>

