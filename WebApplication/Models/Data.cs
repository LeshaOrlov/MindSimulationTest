using System;
using System.Collections.Generic;
using System.Text;

namespace WebApplication.Models
{
    public class Data
    {
        public string FileName { get; set; }
        public string Discription { get; set; }
        public float Version { get; set; }

        public List<DataElement> ElementsList { get; set;}
       
    }

}
