using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using EntidadesCompartidas;

namespace Persistencia
{
    public class PersistenciaPasaje
    {

        // ===== AGREGAR PASAJE ===== //
        public static void Agregar(Pasaje pPasaje)
        {
            SqlConnection oConexion = new SqlConnection(Conexion.STR);
            SqlCommand oComando = new SqlCommand("AltaPasaje", oConexion);
            oComando.CommandType = CommandType.StoredProcedure;

            double impuestoSalida = pPasaje.oVuelo.AeropuertoSalida.ImpuestoPartida;
            double impuestoLlegada = pPasaje.oVuelo.AeropuertoLlegada.ImpuestoLlegada;
            double precioVuelo = pPasaje.oVuelo.Precio;
            double totalPasaje = (precioVuelo + (impuestoSalida + impuestoLlegada));

            oComando.Parameters.AddWithValue("@codigoVuelo", pPasaje.oVuelo.Codigo);
            oComando.Parameters.AddWithValue("@numeroPasaporteCliente", pPasaje.oCliente.NumPasaporte);
            oComando.Parameters.AddWithValue("@precioTotal", totalPasaje);

            // Retorno
            oComando.Parameters.Add("@numeroPasaje", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;

            try
            {
                oConexion.Open();
                oComando.ExecuteNonQuery();

                // Obtener el valor del retorno
                int numeroPasaje = Convert.ToInt32(oComando.Parameters["@numeroPasaje"].Value);

                if (numeroPasaje > 0)
                {
                    pPasaje.NumVenta = numeroPasaje;
                    pPasaje.Fecha = DateTime.Now;
                    pPasaje.Precio = totalPasaje;
                }
                else if (numeroPasaje == -1)
                {
                    throw new Exception("El vuelo no existe");
                }
                else if (numeroPasaje == -2)
                {
                    throw new Exception("El cliente no existe");
                }
                else if (numeroPasaje == -3)
                {
                    throw new Exception("No hay asientos disponibles");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                oConexion.Close();
            }
        }

        // ===== PASAJES POR USUARIO ===== //
        public static List<Pasaje> ListarPasajesPorUsuario(string numPasaporte)
        {
            List<Pasaje> historicoPasajes = new List<Pasaje>();

            SqlConnection oConexion = new SqlConnection(Conexion.STR);
            SqlCommand oComando = new SqlCommand("HistoricoCliente", oConexion);
            oComando.CommandType = CommandType.StoredProcedure;

            oComando.Parameters.AddWithValue("@pasaporteCliente", numPasaporte);

            try
            {
                oConexion.Open();
                SqlDataReader oReader = oComando.ExecuteReader();

                while (oReader.Read())
                {
                    Vuelo vuelo = PersistenciaVuelo.BuscarVuelo(oReader["CodigoVuelo"].ToString());
                    Cliente cliente = PersistenciaCliente.Buscar(oReader["NumeroPasaporte"].ToString());
                    int numeroPasaje = Convert.ToInt32(oReader["NumeroPasaje"]);
                    DateTime fechaCompra = Convert.ToDateTime(oReader["FechaCompra"]);

                    // Calcular el precio del vuelo
                    double impuestoSalida = vuelo.AeropuertoSalida.ImpuestoPartida;
                    double impuestoLlegada = vuelo.AeropuertoLlegada.ImpuestoLlegada;
                    double precioVuelo = vuelo.Precio;
                    double precioPasaje = precioVuelo + (impuestoSalida + impuestoLlegada);

                    Pasaje pasaje = new Pasaje(vuelo, cliente, numeroPasaje, fechaCompra, precioPasaje);
                    historicoPasajes.Add(pasaje);
                }
                oReader.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                oConexion.Close();
            }

            if (historicoPasajes.Count == 0)
            {
                throw new Exception("El cliente no tiene historial de pasajes.");
            }

            return historicoPasajes;
        }


        // ===== LISTAR TODOS LOS PASAJES ===== //
        public static List<Pasaje> ListarPasajes()
        {
            SqlConnection oConexion = new SqlConnection(Conexion.STR);
            SqlCommand oComando = new SqlCommand("ListarPasaje", oConexion);
            oComando.CommandType = CommandType.StoredProcedure;

            List<Pasaje> listaPasaje = new List<Pasaje>();

            try
            {
                oConexion.Open();

                SqlDataReader oReader = oComando.ExecuteReader();

                if (oReader.HasRows)
                {
                    while (oReader.Read())
                    {
                        Vuelo vuelo = PersistenciaVuelo.BuscarVuelo(oReader["idVuelo"].ToString());
                        Cliente cliente = PersistenciaCliente.Buscar(oReader["idCliente"].ToString());
                        int numeroPasaje = Convert.ToInt32(oReader["numeroPasaje"]);
                        DateTime fechaCompra = Convert.ToDateTime(oReader["fechaCompra"]);

                        // Calcular el precio del vuelo
                        double impuestoSalida = vuelo.AeropuertoSalida.ImpuestoPartida;
                        double impuestoLlegada = vuelo.AeropuertoLlegada.ImpuestoLlegada;
                        double precioVuelo = vuelo.Precio;
                        double precioPasaje = precioVuelo + (impuestoSalida + impuestoLlegada);

                        Pasaje unPasaje = new Pasaje(vuelo, cliente, numeroPasaje, fechaCompra, precioPasaje);
                        listaPasaje.Add(unPasaje);
                    }
                }

                oReader.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oConexion.Close();
            }

            return listaPasaje;
        }

    }
}
