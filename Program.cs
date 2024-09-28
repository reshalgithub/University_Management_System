using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Runtime.InteropServices;

namespace UNIVERSITY_MANAGEMENT
{ 
    class Program
    {
        static void Main(string[] args)
        {
            CreateTables.CreateTable();

            Menu menu = new Menu();
            menu.Display();
        }           
    }  
}
