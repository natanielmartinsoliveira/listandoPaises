using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using ListandoPaises.ViewModels;


namespace ListandoPaises.Model
{
    class Country
    {
        //Classe do pais
        public string name { get; set; }
        public string capital { get; set; }
        public List<string> altSpellings { get; set; }
        public string region { get; set; }
        public string population { get; set; }
        public string nativeName { get; set; }
        public string flag { get; set; }
    }
}
