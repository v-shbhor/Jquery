using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.ServiceModel.Web;
using System.Text;

namespace JqueryBasics
{
    [ServiceContract(Namespace = "")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class EmployeeService
    {
        
        [OperationContract]
        public employe4 getemployeebyid(int employeeid)
        {
            employe4 employee = new employe4();

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
            return employee;
        }

        // Add more operations here and mark them with [OperationContract]
    }
}
