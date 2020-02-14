<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AjaxTable.aspx.cs" Inherits="publico_AjaxTable" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
<script src="~/Scripts/jquery-1.10.2.min.js"></script>  
<script>  
    $(document).ready(function () {  
        //Call EmpDetails jsonResult Method  
        $.getJSON("Home/EmpDetails",  
        function (json) {  
        var tr;  
        //Append each row to html table  
        for (var i = 0; i < json.length; i++) {  
                tr = $('<tr/>');  
                tr.append("<td>" + json[i].Id + "</td>");  
                tr.append("<td>" + json[i].Name + "</td>");  
                tr.append("<td>" + json[i].City + "</td>");  
                tr.append("<td>" + json[i].Address + "</td>");  
                $('table').append(tr);  
            }  
        });  
    });  
</script>  
<table class="table table-bordered table-condensed table-hover table-striped">  
        <thead>  
        <tr>  
        <th>Id</th>  
        <th>Name</th>  
        <th>City</th>  
        <th>Address</th>  
        </tr>  
        </thead>  
        <tbody></tbody>  
</table>

</asp:Content>

