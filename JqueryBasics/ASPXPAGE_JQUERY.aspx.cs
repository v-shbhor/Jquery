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
    public partial class ASPXPAGE_JQUERY : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        //this function is server side function as it is code behind file
        //to call this using jquery using AJAX we need to convert this to a static function
        //and decorate it with webmethod attrib
        [System.Web.Services.WebMethod]
        public static employe4 getemployeebyid(int empid)
        {
            employe4 employee = new employe4();

            string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("spgetemployeebyid", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter parameter = new SqlParameter();
                parameter.ParameterName = "@Id";
                parameter.Value = empid;
                cmd.Parameters.Add(parameter);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    employee.ID = Convert.ToInt32(rdr["Id"]);
                    employee.name = rdr["name"].ToString();
                    employee.gender = rdr["gender"].ToString();
                    employee.salary = Convert.ToInt32(rdr["salary"]);

                }
            }
            return employee;
        }
    }
}