<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DesbloqueioUser.aspx.cs" Inherits="publico_DesbloqueioUser" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
   <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="x_title">
        <h2>
            Desbloquear Usuário</h2>
        <div class="clearfix">
        </div>
    </div>
    
    
    <div>
        <asp:GridView ID="GridView1" runat="server">
        </asp:GridView>
    
    </div>
    
    
    
    <div class="x_content">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div id="demo-form2" data-parsley-validate class="form-horizontal form-label-left input_mask">
                    <div class="row">
                        <div class="form-group">
                            <asp:Label ID="Msg" runat="server" ForeColor="maroon" class="control-label col-md-12" /><br />
                        </div>
                    </div>
                    <div class="row">
                        <div class="form-group">
                            <label class="control-label col-md-3" for="UsernameTextBox">
                                Usuário: <span class="required">*</span>
                            </label>
                            <div class="col-md-6">
                                <asp:TextBox ID="txbUser" class="form-control" runat="server" AutoPostBack="true" />
                                <asp:RequiredFieldValidator ID="UsernameRequiredValidator" runat="server" ControlToValidate="txbUser"
                                    ForeColor="red" Display="Static" ErrorMessage="Required" /><br />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="form-group">
                            <div class="col-md-6 col-sm-6 col-xs-12 col-md-offset-3">
                                <asp:Button ID="UnlockButton" Text="Desbloquear" OnClick="UnlockUser_OnClick" 
                                    runat="server" Enabled="true" class="btn btn-primary" />
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
