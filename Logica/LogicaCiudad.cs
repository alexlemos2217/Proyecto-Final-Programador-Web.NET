using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EntidadesCompartidas;
using Persistencia;

namespace Logica
{
    public class LogicaCiudad
    {
        // ===== AGREGAR CIUDAD ===== //
        public static void Agregar(Ciudad cCiudad)
        {
            PersistenciaCiudad.Agregar(cCiudad);
        }

        // ===== MODIFICAR CIUDAD ===== //
        public static void Modificar(Ciudad cCiudad)
        {
            PersistenciaCiudad.Modificar(cCiudad);
        }

        // ===== ELIMINAR CIUDAD ===== //
        public static void Eliminar(Ciudad cCiudad)
        {
            PersistenciaCiudad.Eliminar(cCiudad);
        }

        // ===== BUSCAR CIUDAD ===== //
        public static Ciudad Buscar(string cCiudad)
        {
            Ciudad oCiudad = PersistenciaCiudad.BuscarCiudad(cCiudad);

            return oCiudad;
        }

        // ===== LISTAR CIUDAD ===== //
        public static List<Ciudad> ListarCiudades()
        {
            List<Ciudad> colAuxiliar = new List<Ciudad>();

            colAuxiliar.AddRange(PersistenciaCiudad.ListarCiudades());

            return colAuxiliar;
        }
    }
}
