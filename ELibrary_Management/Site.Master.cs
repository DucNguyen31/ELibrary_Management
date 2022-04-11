using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ELibrary_Management
{
    public partial class Site : System.Web.UI.MasterPage
    {
        static string strConn = ConfigurationManager.ConnectionStrings["DbConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if(Session["role"] == null || Session["role"].Equals(""))
                {
                } else
                {
                    SqlConnection conn = new SqlConnection(strConn);
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
                    SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.[User] WHERE UserID = " + Session["userID"].ToString(), conn);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        Label1.Text = dr.GetValue(1).ToString();
                        Image1.ImageUrl = "../img/member_img/" + dr.GetValue(9).ToString();
                    }
                }
            }
            catch (Exception)
            {
            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Successful login!');</script>");
            Response.Redirect("ViewBook.aspx?bookID=" + txtBookName.Text);
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            txtBookName.Text = "";
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Session.Remove("userID");
            Session.Remove("role");
            Response.Redirect("HomePage.aspx");
        }
    }
}