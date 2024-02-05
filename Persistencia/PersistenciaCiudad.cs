using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EntidadesCompartidas;
using System.Data.SqlClient;
using System.Data;

namespace Persistencia
{
    public class PersistenciaCiudad
    {
        // ===== AGREGAR CIUDAD ===== //
        public static void Agregar(Ciudad cCiudad)
        {
            SqlConnection oConexion = new SqlConnection(Conexion.STR);
            SqlCommand oComando = new SqlCommand("AltaCiudad", oConexion);
            oComando.CommandType = CommandType.StoredProcedure;

            oComando.Parameters.AddWithValue("@codigo", cCiudad.Codigo);
            oComando.Parameters.AddWithValue("@nombre", cCiudad.NombreCoudad);
            oComando.Parameters.AddWithValue("@pais", cCiudad.Pais);

            SqlParameter oRetorno = new SqlParameter("@Retorno", SqlDbType.Int);
            oRetorno.Direction = ParameterDirection.ReturnValue;
            oComando.Parameters.Add(oRetorno);

            try
            {
                oConexion.Open();
                oComando.ExecuteNonQuery();

                int resultado = (int)oRetorno.Value;

                if (resultado == -1)
                    throw new Exception("La ciuadd ya existe");
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

        // ===== MODIFICAR CIUDAD ===== //
        public static void Modificar(Ciudad cCiudad)
        {
            SqlConnection oConexion = new SqlConnection(Conexion.STR);
            SqlCommand oComando = new SqlCommand("ModificarCiudad", oConexion);
            oComando.CommandType = CommandType.StoredProcedure;

            oComando.Parameters.AddWithValue("@codigo", cCiudad.Codigo);
            oComando.Parameters.AddWithValue("@nombre", cCiudad.NombreCoudad);
            oComando.Parameters.AddWithValue("@pais", cCiudad.Pais);

            SqlParameter oRetorno = new SqlParameter("@Retorno", SqlDbType.Int);
            oRetorno.Direction = ParameterDirection.ReturnValue;
            oComando.Parameters.Add(oRetorno);

            try
            {
                oConexion.Open();
                oComando.ExecuteNonQuery();

                int resultado = (int)oRetorno.Value;

                if (resultado == -1)
                    throw new Exception("No existe una ciudad con ese nombre, no se modifica");
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

        // ===== ELIMINAR CIUDAD ===== //
        public static void Eliminar(Ciudad cCiudad)
        {
            SqlConnection oConexion = new SqlConnection(Conexion.STR);
            SqlCommand oComando = new SqlCommand("BajaCiudad", oConexion);
            oComando.CommandType = CommandType.StoredProcedure;


            SqlParameter oCodigo = new SqlParameter("@codigo", cCiudad.Codigo);
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
                    throw new Exception("No existe una ciudad con ese nombre");

                else if (resultado == -2)
                    throw new Exception("Ocurrió un error inesperado");

                else if (resultado == -3)
                    throw new Exception("Hay aeropuertos asociados - No se elimina");
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

        // ===== BUSCAR CIUDAD ===== //
        public static Ciudad BuscarCiudad(string codigoCiudad)
        {
            SqlConnection oConexion = new SqlConnection(Conexion.STR);
            SqlCommand oComando = new SqlCommand("BuscarCiudadPorCodigo", oConexion);
            oComando.CommandType = CommandType.StoredProcedure;

            Ciudad ciudad = null;
            oComando.Parameters.AddWithValue("@codigo", codigoCiudad);

            try
            {
                oConexion.Open();
                SqlDataReader oReader = oComando.ExecuteReader();

                if (oReader.HasRows)
	            {
		            oReader.Read();
                    ciudad = new Ciudad(oReader["codigo"].ToString(), oReader["nombre"].ToString(), oReader["pais"].ToString());

                    oReader.Close();
	            }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                oConexion.Close();
            }

            return ciudad;
        }

        // ===== LISTAR CIUDADES ===== //
        public static List<Ciudad> ListarCiudades()
        {

            List<Ciudad> colCiudades = new List<Ciudad>();

            SqlConnection oConexion = new SqlConnection(Conexion.STR);
            SqlCommand oComando = new SqlCommand("ListarCiudad", oConexion);
            oComando.CommandType = CommandType.StoredProcedure;

            try
            {
                oConexion.Open();
                SqlDataReader oReader = oComando.ExecuteReader();

                if (oReader.HasRows)
                {
                    while (oReader.Read())
                    {
                        Ciudad unaCiudad = new Ciudad(oReader["codigo"].ToString(), oReader["nombre"].ToString(), oReader["pais"].ToString());
                        colCiudades.Add(unaCiudad);
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
            return colCiudades;
        }
    }
}
