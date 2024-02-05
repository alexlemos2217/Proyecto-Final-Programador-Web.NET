using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntidadesCompartidas;
using Persistencia;

namespace Logica
{
    public class LogicaCliente
    {
        // ===== LOGUEO CLIENTE ===== //
        public static Cliente Logueo(string pUsu, string pPass)
        {
            Cliente oCliente = null;

            oCliente = PersistenciaCliente.Logueo(pUsu, pPass);

            return oCliente;
        }

        // ===== AGREGAR CLIENTE ===== //
        public static void Agregar(Cliente cCliente)
        {
            PersistenciaCliente.Agregar(cCliente);
        }

        // ===== MODIFICAR CLIENTE ===== //
        public static void Modificar(Cliente cCliente)
        {
            PersistenciaCliente.Modificar(cCliente);
        }

        // ===== ELIMINAR CLIENTE ===== //
        public static void Eliminar(Cliente cCliente)
        {
            PersistenciaCliente.Eliminar(cCliente);
        }

        // ===== BUSCAR CLIENTE ===== //
        public static Cliente Buscar(string cCliente)
        {
            Cliente oCliente = PersistenciaCliente.Buscar(cCliente);

            return oCliente;
        }

        // ===== LISTAR CLIENTES ===== //
        public static List<Cliente> ListarClientes()
        {
            List<Cliente> colAuxiliar = new List<Cliente>();

            colAuxiliar.AddRange(PersistenciaCliente.ListarClientes());

            return colAuxiliar;
        }
    }
}
