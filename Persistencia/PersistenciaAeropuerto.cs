using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EntidadesCompartidas;
using System.Data.SqlClient;
using System.Data;

namespace Persistencia
{
    public class PersistenciaAeropuerto
    {
        // ===== AGREGAR AEROPUERTO ===== //
        public static void Agregar(Aeropuerto aAeropuerto)
        {
            SqlConnection oConexion = new SqlConnection(Conexion.STR);
            SqlCommand oComando = new SqlCommand("AltaAeropuerto", oConexion);
            oComando.CommandType = CommandType.StoredProcedure;

            oComando.Parameters.AddWithValue("@codigo", aAeropuerto.Codigo);
            oComando.Parameters.AddWithValue("@nombre", aAeropuerto.Nombre);
            oComando.Parameters.AddWithValue("@direccion", aAeropuerto.Direccion);
            oComando.Parameters.AddWithValue("@idCiudad", aAeropuerto.Ciudad.Codigo);
            oComando.Parameters.AddWithValue("@impuestoPartida", aAeropuerto.ImpuestoPartida);
            oComando.Parameters.AddWithValue("@impuestoLlegada", aAeropuerto.ImpuestoLlegada);

            SqlParameter oRetorno = new SqlParameter("@Retorno", SqlDbType.Int);
            oRetorno.Direction = ParameterDirection.ReturnValue;
            oComando.Parameters.Add(oRetorno);

            try
            {
                oConexion.Open();
                oComando.ExecuteNonQuery();

                int resultado = (int)oRetorno.Value;

                if (resultado == -1)
                    throw new Exception("Ya existe un aeropuerto con ese código");
                else if (resultado == -2)
                    throw new Exception("Ocurrió un error inesperado");

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

        // ===== MODIFICAR AEROPUERTO ===== //
        public static void Modificar(Aeropuerto aAeropuerto)
        {
            SqlConnection oConexion = new SqlConnection(Conexion.STR);
            SqlCommand oComando = new SqlCommand("ModificarAeropuerto", oConexion);
            oComando.CommandType = CommandType.StoredProcedure;

            oComando.Parameters.AddWithValue("@codigo", aAeropuerto.Codigo);
            oComando.Parameters.AddWithValue("@nombre", aAeropuerto.Nombre);
            oComando.Parameters.AddWithValue("@direccion", aAeropuerto.Direccion);
            oComando.Parameters.AddWithValue("@idCiudad", aAeropuerto.Ciudad.Codigo);
            oComando.Parameters.AddWithValue("@impuestoPartida", aAeropuerto.ImpuestoPartida);
            oComando.Parameters.AddWithValue("@impuestoLlegada", aAeropuerto.ImpuestoLlegada);

            SqlParameter oRetorno = new SqlParameter("@Retorno", SqlDbType.Int);
            oRetorno.Direction = ParameterDirection.ReturnValue;
            oComando.Parameters.Add(oRetorno);
            try
            {
                oConexion.Open();
                oComando.ExecuteNonQuery();

                int resultado = (int)oRetorno.Value;

                if (resultado == -1)
                    throw new Exception("Aeropuerto no encontrado");
                else if (resultado == -2)
                    throw new Exception("Ocurrió un error inesperado");
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

        // ===== ELIMINAR AEROPUERTO ===== //
        public static void Eliminar(Aeropuerto aAeropuerto)
        {
            SqlConnection oConexion = new SqlConnection(Conexion.STR);
            SqlCommand oComando = new SqlCommand("BajaAeropuerto", oConexion);
            oComando.CommandType = CommandType.StoredProcedure;

            SqlParameter oCodigo = new SqlParameter("@codigo", aAeropuerto.Codigo);
            oCodigo.Direction = ParameterDirection.Input;

            SqlParameter oRetorno = new SqlParameter("@Retorno", SqlDbType.Int);
            oRetorno.Direction = ParameterDirection.ReturnValue;

            oComando.Parameters.Add(oCodigo);
            oComando.Parameters.Add(oRetorno);

            try 
	        {	        
		        oConexion.Open();
                oComando.ExecuteNonQuery();

                int resultado = (int)oRetorno.Value;

                if (resultado == -1)
                    throw new Exception("Aeropuerto no encontrado");
                else if (resultado == -2)
                    throw new Exception("Ocurrió un error inesperado");
                else if (resultado == -3)
                    throw new Exception("Aeropuerto con vuelos asociados, no se elimina");
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

        // ===== BUSCAR AEROPUERTO ===== //
        public static Aeropuerto BuscarAeropuerto(string codigoAero)
        {
            SqlConnection oConexion = new SqlConnection(Conexion.STR);
            SqlCommand oComando = new SqlCommand("BuscarAeropuertoPorCodigo", oConexion);
            oComando.CommandType = CommandType.StoredProcedure;

            Aeropuerto aeropuerto = null;
            oComando.Parameters.AddWithValue("@codigo", codigoAero);

            try
            {
                oConexion.Open();
                SqlDataReader oReader = oComando.ExecuteReader();

                if (oReader.HasRows)
                {
                    oReader.Read();

                    Ciudad ciudad = PersistenciaCiudad.BuscarCiudad(oReader["idCiudad"].ToString());

                    aeropuerto = new Aeropuerto(oReader["codigo"].ToString(), oReader["nombre"].ToString(), oReader["direccion"].ToString(), ciudad, Convert.ToDouble(oReader["impuestoPartida"]), Convert.ToDouble(oReader["impuestoLlegada"]));
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

            return aeropuerto;
        }

        // ===== LISTAR AEROPUERTOS CON VUELOS ===== //
        public static List<Aeropuerto> ListarAeropuertosConVuelos(Vuelo unVuelo)
        {
            SqlConnection oConexion = new SqlConnection(Conexion.STR);
            SqlCommand oComando = new SqlCommand("ListarAeropuertosConVuelos", oConexion);
            oComando.CommandType = CommandType.StoredProcedure;
            oComando.Parameters.AddWithValue("@idCiudad", unVuelo.Codigo);

            List<Aeropuerto> colAeropuertos = new List<Aeropuerto>();

            try
            {
                oConexion.Open();
                SqlDataReader oReader = oComando.ExecuteReader();

                while (oReader.Read())
                {
                    Ciudad ciudad = PersistenciaCiudad.BuscarCiudad(oReader["idCiudad"].ToString());

                    Aeropuerto unAeropuerto = new Aeropuerto(oReader["codigo"].ToString(), oReader["nombre"].ToString(), oReader["direccion"].ToString(), ciudad, Convert.ToDouble(oReader["impuestoPartida"]), Convert.ToDouble(oReader["impuestoLlegada"]));

                    colAeropuertos.Add(unAeropuerto);
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

            return colAeropuertos;
        }

        // ===== LISTAR AEROPUERTOS ===== //
        public static List<Aeropuerto> ListarAeropuertos()
        {
            List<Aeropuerto> colAeropuertos = new List<Aeropuerto>();

            SqlConnection oConexion = new SqlConnection(Conexion.STR);
            SqlCommand oComando = new SqlCommand("ListarAeropuertos", oConexion);
            oComando.CommandType = CommandType.StoredProcedure;

            try
            {
                oConexion.Open();
                SqlDataReader oReader = oComando.ExecuteReader();

                if (oReader.HasRows)
                {
                    while (oReader.Read())
                    {
                        Ciudad ciudad = PersistenciaCiudad.BuscarCiudad(oReader["idCiudad"].ToString());

                        Aeropuerto unAeropuerto = new Aeropuerto(oReader["codigo"].ToString(), oReader["nombre"].ToString(), oReader["direccion"].ToString(), ciudad, Convert.ToDouble(oReader["impuestoPartida"]), Convert.ToDouble(oReader["impuestoLlegada"]));

                        colAeropuertos.Add(unAeropuerto);
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
            return colAeropuertos;
        }
    }
}
