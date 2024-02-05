using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EntidadesCompartidas;
using System.Data.SqlClient;
using System.Data;

namespace Persistencia
{
    public class PersistenciaVuelo
    {
        // ===== AGREGAR VUELO ===== //
        public static int Agregar(Vuelo vVuelo)
        {
            SqlConnection oConexion = new SqlConnection(Conexion.STR);
            SqlCommand oComando = new SqlCommand("AltaVuelo", oConexion);
            oComando.CommandType = CommandType.StoredProcedure;

            oComando.Parameters.AddWithValue("@fecHorSalida", vVuelo.FecHorSalida);
            oComando.Parameters.AddWithValue("@fecHorLlegada", vVuelo.FecHorLlegada);
            oComando.Parameters.AddWithValue("@idAeropuertoSalida", vVuelo.AeropuertoSalida.Codigo);
            oComando.Parameters.AddWithValue("@idAeropuertoLlegada", vVuelo.AeropuertoLlegada.Codigo);
            oComando.Parameters.AddWithValue("@precio", vVuelo.Precio);
            oComando.Parameters.AddWithValue("@asientos", vVuelo.Asientos);

            SqlParameter oRetorno = new SqlParameter("@Retorno", SqlDbType.Int);
            oRetorno.Direction = ParameterDirection.ReturnValue;

            SqlParameter oCodigoVuelo = new SqlParameter("@codigoVuelo", SqlDbType.VarChar, 15);
            oCodigoVuelo.Direction = ParameterDirection.Output;

            oComando.Parameters.Add(oRetorno);
            oComando.Parameters.Add(oCodigoVuelo);

            try
            {
                oConexion.Open();
                oComando.ExecuteNonQuery();

                int resultado = (int)oRetorno.Value;

                if (resultado == 1)
                {
                    vVuelo.Codigo = oCodigoVuelo.Value.ToString();
                }
                else if (resultado == -1)
                {
                    throw new Exception("Ocurrió un error al agregar el vuelo.");
                }
                else if (resultado == -2)
                {
                    throw new Exception("Los asientos no están dentro del rango especificado (100-300).");
                }

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar el vuelo: " + ex.Message);
            }
            finally
            {
                oConexion.Close();
            }
        }


        // ===== BUSCAR VUELO ===== //
        public static Vuelo BuscarVuelo(string codigoVuelo)
        {
            SqlConnection oConexion = new SqlConnection(Conexion.STR);
            SqlCommand oComando = new SqlCommand("BuscarVueloPorCodigo", oConexion);
            oComando.CommandType = CommandType.StoredProcedure;

            Vuelo vuelo = null;
            oComando.Parameters.AddWithValue("@codigo", codigoVuelo);

            try
            {
                oConexion.Open();
                SqlDataReader oReader = oComando.ExecuteReader();

                if (oReader.HasRows)
                {
                    oReader.Read();

                    // Aeropuerto de salida
                    Aeropuerto aeropuertoSalida = PersistenciaAeropuerto.BuscarAeropuerto(oReader["idAeropuertoSalida"].ToString());

                    // Aeropuerto de llegada
                    Aeropuerto aeropuertoLlegada = PersistenciaAeropuerto.BuscarAeropuerto(oReader["idAeropuertoLlegada"].ToString());

                    vuelo = new Vuelo(oReader["codigo"].ToString(), Convert.ToDateTime(oReader["fecHorSalida"]), Convert.ToDateTime(oReader["fecHorLlegada"]), Convert.ToDouble(oReader["precio"]), Convert.ToInt32(oReader["asientos"]), aeropuertoSalida, aeropuertoLlegada);
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

            return vuelo;
        }

        // ===== OBTENER PARTIDAS ===== //
        public static List<Vuelo> ObtenerPartidas(string codigoAeropuerto)
        {
            List<Vuelo> partidas = new List<Vuelo>();

            SqlConnection oConexion = new SqlConnection(Conexion.STR);

            try
            {
                SqlCommand oComando = new SqlCommand("ObtenerPartidas", oConexion);
                oComando.CommandType = CommandType.StoredProcedure;

                oComando.Parameters.AddWithValue("@codigoAeropuerto", codigoAeropuerto);

                oConexion.Open();
                SqlDataReader oReader = oComando.ExecuteReader();

                while (oReader.Read())
                {
                    try
                    {
                        string codigoVuelo = oReader["CodigoVuelo"].ToString();
                        Vuelo vuelo = BuscarVuelo(codigoVuelo);

                        if (vuelo != null)
                        {
                            partidas.Add(vuelo);
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
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

            return partidas;
        }

        // ===== OBTENER ARRIBOS ===== //
        public static List<Vuelo> ObtenerArribos(string codigoAeropuerto)
        {
            List<Vuelo> arribos = new List<Vuelo>();

            SqlConnection oConexion = new SqlConnection(Conexion.STR);

            try
            {
                SqlCommand oComando = new SqlCommand("ObtenerArribos", oConexion); 
                oComando.CommandType = CommandType.StoredProcedure;

                oComando.Parameters.AddWithValue("@codigoAeropuerto", codigoAeropuerto);

                oConexion.Open();
                SqlDataReader oReader = oComando.ExecuteReader();

                while (oReader.Read())
                {
                    try
                    {
                        string codigoVuelo = oReader["CodigoVuelo"].ToString();
                        Vuelo vuelo = BuscarVuelo(codigoVuelo);

                        if (vuelo != null) // Asegurarse de que se haya encontrado un vuelo
                        {
                            arribos.Add(vuelo);
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
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

            return arribos;
        }

        // ===== LISTAR VUELOS ===== //
        public static List<Vuelo> ListarVuelos()
        {
            List<Vuelo> colVuelos = new List<Vuelo>();

            SqlConnection oConexion = new SqlConnection(Conexion.STR);
            SqlCommand oComando = new SqlCommand("ListarVuelos", oConexion);
            oComando.CommandType = CommandType.StoredProcedure;

            try
            {
                oConexion.Open();
                SqlDataReader oReader = oComando.ExecuteReader();

                if (oReader.HasRows)
                {
                    while (oReader.Read())
                    {
                        Aeropuerto salida = PersistenciaAeropuerto.BuscarAeropuerto(oReader["idAeropuertoSalida"].ToString());
                        Aeropuerto llegada = PersistenciaAeropuerto.BuscarAeropuerto(oReader["idAeropuertoLlegada"].ToString());

                        Vuelo unVuelo = new Vuelo(oReader["codigo"].ToString(), Convert.ToDateTime(oReader["fecHorSalida"]), Convert.ToDateTime(oReader["fecHorLlegada"]), Convert.ToDouble(oReader["precio"]), Convert.ToInt32(oReader["asientos"]), salida, llegada);

                        colVuelos.Add(unVuelo);
                    }
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
            return colVuelos;
        }
    }
}
