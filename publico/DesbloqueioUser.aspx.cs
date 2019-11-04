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

public partial class publico_DesbloqueioUser : System.Web.UI.Page
{
    MembershipUser u;

    protected void Page_Load(object sender, EventArgs e)
    {
        Msg.Text = "";

        if (!IsPostBack)
        {
            Msg.Text = "Por favor, forneça um nome de usuário.";
        }
        else
        {
            VerifyUsername();
        }

        getUsersLocked();

    }


    /// <summary>
    ///  criar metodo datatable
    /// </summary>

    protected void getUsersLocked()
    {
        //DataTable dt = new DataTable();

        MembershipUserCollection filteredUsers = new MembershipUserCollection();

        try
        {
            var users = Membership.GetAllUsers();

            foreach (MembershipUser user in users)
            {
                // if user is currently online, add to gridview list
                if (user.IsLockedOut == true)
                {
                    filteredUsers.Add(user);
                }
            }
            
        }
        catch (Exception ex)
        {
            string error = ex.Message;
        }

        GridView1.DataSource = filteredUsers;
        GridView1.DataBind();
    }


    public void VerifyUsername()
    {
        u = Membership.GetUser(txbUser.Text, false);

        if (u == null)
        {
            Msg.Text = "Usuário " + Server.HtmlEncode(txbUser.Text) + " não encontrado. Por favor, verifique os dados e tente novamente.";

            UnlockButton.Enabled = false;
        }
        else
        {
            UnlockButton.Enabled = true;
        }
    }


    public void UnlockUser_OnClick(object sender, EventArgs args)
    {
        u = Membership.GetUser(txbUser.Text, false);

        if (u == null)
        {
            Msg.Text = "Usuário " + Server.HtmlEncode(txbUser.Text) + " não encontrado. Por favor, verifique os dados e tente novamente.";
            return;
        }

        try
        {
            u.UnlockUser();
            Msg.Text = "Usuário " + Server.HtmlEncode(txbUser.Text) + " desbloqueado.";
        }
        catch (MembershipPasswordException e)
        {
            Msg.Text = "Não foi possivel desbloquear. Tente novamente.";
            return;
        }
        catch (Exception e)
        {
            Msg.Text = e.Message;
            return;
        }

    }

}
