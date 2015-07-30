using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JqueryBasics
{
    public class Menu
    {
        public int Id { get; set; }
        public int? parentid { get; set; } //here ? suggest its a nullable integer
        public string menutext { get; set; }
        public bool active { get; set; }
        public List<Menu> List { get; set; } //this will store the children of the parent
    }
}