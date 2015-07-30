using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Script.Serialization;

namespace JqueryBasics
{
    /// <summary>
    /// Summary description for MenuHandler
    /// </summary>
    public class MenuHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            List<Menu> listmenu = new List<Menu>();

            string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("spgetmenudata", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Menu menu = new Menu();
                    menu.Id = Convert.ToInt32(rdr["Id"]);
                    menu.menutext = rdr["menutext"].ToString();
                    menu.parentid = rdr["parentid"] != DBNull.Value ? Convert.ToInt32(rdr["parentid"]) : (int?)null;
                    menu.active = Convert.ToBoolean(rdr["active"]);
                    listmenu.Add(menu);

                }
            }

            List<Menu> menutree = getmenutree(listmenu,null);
            JavaScriptSerializer js = new JavaScriptSerializer();
            context.Response.Write(js.Serialize(menutree));

        }


        //build a hierarchy here e.g parent item is india and all its children 
        //and one child has further children
        private List<Menu> getmenutree(List<Menu> list,int? parentid)
        {
            return list.Where(x => x.parentid == parentid).Select(x => new Menu()
            {
                Id = x.Id,
                menutext = x.menutext,
                parentid = x.parentid,
                active = x.active,   
                List = getmenutree(list,x.Id)
            }).ToList();
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}