using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Serialization;

namespace JqueryBasics
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //code to generate a .NET Object to JSON STRING
            /* employee emp1 = new employee
             {
                 fname = "shailesh",
                 lname = "bhor",
                 salary = 50000
             };
             employee emp2 = new employee
             {
                 fname = "vasudha",
                 lname = "bhor",
                 salary = 50000
             };

             List <employee> listemp = new List<employee>();
             listemp.Add(emp1);
             listemp.Add(emp2);
             JavaScriptSerializer javascriptserializer = new JavaScriptSerializer();
             string jsonstring = javascriptserializer.Serialize(listemp);
             Response.Write(jsonstring); */



            //CODE FOR JSON STRING TO .Net Object
            //We are escaping the " with \
            string jsonstring = "[{\"fname\":\"todd\",\"lname\":\"modd\",\"gender\":\"male\"},{\"fname\":\"mary\",\"lname\":\"modd\",\"gender\":\"female\"}]";
            JavaScriptSerializer javascriptserializer = new JavaScriptSerializer();
            List<employee> listemployee = (List<employee>)javascriptserializer.Deserialize(jsonstring, typeof(List<employee>));

            foreach(employee employee in listemployee)
            {
                Response.Write("First Name = " + employee.fname + "<br/>");
                Response.Write("Last Name = " + employee.lname + "<br/>");
                Response.Write("Salary = " + employee.salary + "<br/>");
            }


            
        }
    }
}