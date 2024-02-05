using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Persistencia;
using EntidadesCompartidas;

namespace Logica
{
    public class LogicaVuelo
    {
        // ===== AGREGAR VUELO ===== //
        public static void Agregar(Vuelo vVuelo)
        {
            PersistenciaVuelo.Agregar(vVuelo);
        }

        // ===== BUSCAR VUELO ===== //
        public static Vuelo Buscar(string vVuelo)
        {
            Vuelo oVuelo = PersistenciaVuelo.BuscarVuelo(vVuelo);

            return oVuelo;
        }

        // ===== LISTAR PARTIDAS ===== //
        public static List<Vuelo> ListarPartidas(string codigoAeropuerto)
        {
            List<Vuelo> colAuxiliar = new List<Vuelo>();

            colAuxiliar.AddRange(PersistenciaVuelo.ObtenerPartidas(codigoAeropuerto));

            return colAuxiliar;
        }

        // ===== LISTAR ARRIBOS ===== //
        public static List<Vuelo> ListarArribos(string codigoAeropuerto)
        {
            List<Vuelo> colAuxiliar = new List<Vuelo>();

            colAuxiliar.AddRange(PersistenciaVuelo.ObtenerArribos(codigoAeropuerto));

            return colAuxiliar;
        }

        // ===== LISTAR VUELOS ===== //
        public static List<Vuelo> ListarVuelos()
        {
            List<Vuelo> colAuxiliar = new List<Vuelo>();

            colAuxiliar.AddRange(PersistenciaVuelo.ListarVuelos());

            return colAuxiliar;
        }
    }
}
