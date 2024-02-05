using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Persistencia
{
    internal class Conexion
    {   // ===== STRING DE CONEXIÓN ===== //
        private static string str = "Data Source=ALEX\\SQLEXPRESS01; Initial Catalog=AeropuertosAmericanos; Integrated Security=true";

        public static string STR
        {
            get { return str; }
        }
    }
}
