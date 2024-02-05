using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Persistencia;
using EntidadesCompartidas;

namespace Logica
{
    public class LogicaPasaje
    {
        // ===== AGREGAR PASAJE ===== //
        public static void Agregar(Pasaje pPasaje)
        {
            PersistenciaPasaje.Agregar(pPasaje);
        }

        // ===== LISTAR PASAJE ===== //
        public static List<Pasaje> ListarPasajes()
        {
            return PersistenciaPasaje.ListarPasajes();
        }

        // ===== LISTAR PASAJE POR USUARIO ===== //
        public static List<Pasaje> ListarPasajesUsuario(string pPasaje)
        {
            return PersistenciaPasaje.ListarPasajesPorUsuario(pPasaje);
        }
    }
}
