using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntidadesCompartidas;
using Persistencia;

namespace Logica
{
    public class LogicaEmpleado
    {
        // ===== LOGUEO EMPLEADO ===== //
        public static Empleado Logueo(string pUsu, string pPass)
        {
            Empleado oEmpleado = null;

            oEmpleado = PersistenciaEmpleado.Logueo(pUsu, pPass);

            return oEmpleado;
            
        }
    }
}
