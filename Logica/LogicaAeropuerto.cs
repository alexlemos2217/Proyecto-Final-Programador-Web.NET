using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EntidadesCompartidas;
using Persistencia;

namespace Logica
{
    public class LogicaAeropuerto
    {
        // ===== AGREGAR AEROPUERTO ===== //
        public static void Agregar(Aeropuerto aAeropuerto)
        {
            PersistenciaAeropuerto.Agregar(aAeropuerto);
        }

        // ===== MODIFICAR AEROPUERTO ===== //
        public static void Modificar(Aeropuerto aAeropuerto)
        {
            PersistenciaAeropuerto.Modificar(aAeropuerto);
        }

        // ===== ELIMINAR AEROPUERTO ===== //
        public static void Eliminar(Aeropuerto aAeropuerto)
        {
            PersistenciaAeropuerto.Eliminar(aAeropuerto);
        }

        // ===== BUSCAR AEROPUERTO ===== //
        public static Aeropuerto Buscar(string aAeropuerto)
        {
            Aeropuerto oAeropuerto = PersistenciaAeropuerto.BuscarAeropuerto(aAeropuerto);

            return oAeropuerto;
        }

        // ===== LISTAR AEROPUERTOS ===== //
        public static List<Aeropuerto> ListarAeropuertos()
        {
            List<Aeropuerto> colAuxiliar = new List<Aeropuerto>();

            colAuxiliar.AddRange(PersistenciaAeropuerto.ListarAeropuertos());

            return colAuxiliar;
        }
    }
}
