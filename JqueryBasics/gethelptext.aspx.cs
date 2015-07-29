using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace JqueryBasics
{
    public partial class gethelptext : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            divresult.InnerText = gethelptextbykey(Request["helptextkey"]);
        }
        private string gethelptextbykey(string key)
        {
            string helptext = string.Empty;
            string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("spgethelptextbykey", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter parameter = new SqlParameter("@helptextkey", key);
                cmd.Parameters.Add(parameter);
                con.Open();
                helptext = cmd.ExecuteScalar().ToString();
            }
            return helptext;
        }
    }
}