using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Script.Serialization;
using System.Xml.Serialization;



namespace JqueryBasics
{
    /// <summary>
    /// Summary description for emplyeeservice
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class emplyeeservice : System.Web.Services.WebService
    {

        [WebMethod]
        public void getemployeebyid(int employeeid)
        //use public EmployeeAJAX getemployeebyid(int employeeid) definition if we need to 
        //return an xml i.e return EmployeeAJAX object
        {
            EmployeeAJAX employee = new EmployeeAJAX();

            string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("spgetemployeebyid", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter parameter = new SqlParameter();
                parameter.ParameterName = "@Id";
                parameter.Value = employeeid;
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
            // This code returns the object in XML format if we want to return a JSO then make the following changes
            /* we will not return from the function and hence set the return type to VOID */
            JavaScriptSerializer js = new JavaScriptSerializer();
            Context.Response.Write(js.Serialize(employee)); //and delete the return employee

            // return employee; use this code in case u would like to send a xml object from teh webservice
        }

        [WebMethod]
        public void addemployees(EmployeeAJAX emp)
        //use public EmployeeAJAX getemployeebyid(int employeeid) definition if we need to 
        //return an xml i.e return EmployeeAJAX object
        {

            string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("spinsertemployees", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter()
                {
                    ParameterName = "@name",
                    Value = emp.name
                });
                cmd.Parameters.Add(new SqlParameter()
                {
                    ParameterName = "@gender",
                    Value = emp.gender
                });
                cmd.Parameters.Add(new SqlParameter()
                {
                    ParameterName = "@salary",
                    Value = emp.salary
                });

                con.Open();
                cmd.ExecuteNonQuery();


            }
        }


        [WebMethod]
        public void getallemployee()
        //use public EmployeeAJAX getemployeebyid(int employeeid) definition if we need to 
        //return an xml i.e return EmployeeAJAX object
        {
            List<EmployeeAJAX> listemployees = new List<EmployeeAJAX>();

            string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("select * from tblemployee", con);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    EmployeeAJAX employee = new EmployeeAJAX();
                    employee.ID = Convert.ToInt32(rdr["Id"]);
                    employee.name = rdr["name"].ToString();
                    employee.gender = rdr["gender"].ToString();
                    employee.salary = Convert.ToInt32(rdr["salary"]);
                    listemployees.Add(employee);

                }
            }
            // This code returns the object in XML format if we want to return a JSO then make the following changes
            /* we will not return from the function and hence set the return type to VOID */
            JavaScriptSerializer js = new JavaScriptSerializer();
            Context.Response.Write(js.Serialize(listemployees)); //and delete the return employee

            // return employee; use this code in case u would like to send a xml object from teh webservice
        }

        [WebMethod]
        public void usernameexists(string name)
        {
            bool nameinuse = false;
            string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("spuserexists", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter parameter = new SqlParameter();
                parameter.ParameterName = "@name";
                parameter.Value = name;
                cmd.Parameters.Add(parameter);
                con.Open();
                nameinuse = Convert.ToBoolean(cmd.ExecuteScalar());
            }
            Registration reg = new Registration();
            reg.username = name;
            reg.nameinuse = nameinuse;
            JavaScriptSerializer js = new JavaScriptSerializer();
            Context.Response.Write(js.Serialize(reg));
        }

        
    }
}
