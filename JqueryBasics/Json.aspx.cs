using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace JqueryBasics
{
    public class helptext
    {
        public string key { get; set; }
        public string Text { get; set; }
    }
    public partial class Json : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.ContentType = "text/xml";
            XmlSerializer xmlserializer = new XmlSerializer(typeof(helptext));
            xmlserializer.Serialize(Response.OutputStream, gethelptextbykey(Request["helptextkey"]));

            /*
            JavaScriptSerializer js = new JavaScriptSerializer();
            string jsonstring = js.Serialize(gethelptextbykey(Request["helptextkey"]));
            Response.Write(jsonstring);
            */
        }
        private helptext gethelptextbykey(string key)
        {
            helptext helptext = new helptext();

            string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("spgethelptextbykey", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter parameter = new SqlParameter("@helptextkey", key);
                cmd.Parameters.Add(parameter);
                con.Open();
                helptext.Text = cmd.ExecuteScalar().ToString();
                helptext.key = key;
            }
            return helptext;
        }
    }
}