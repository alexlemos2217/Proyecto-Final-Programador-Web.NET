using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.SqlClient;
using EntidadesCompartidas;

namespace Persistencia
{
    public class PersistenciaCliente
    {
        // ===== LOGUEO CLIENTE ===== //
        public static Cliente Logueo(string pUsu, string pPass)
        {
            Cliente oCliente = null;

            SqlConnection oConexion = new SqlConnection(Conexion.STR);
            SqlCommand oComando = new SqlCommand("LogueoCliente", oConexion);
            oComando.CommandType = CommandType.StoredProcedure;

            oComando.Parameters.AddWithValue("@numPasaporte", pUsu);
            oComando.Parameters.AddWithValue("@contrasenia", pPass);

            try
            {
                oConexion.Open();
                SqlDataReader oLector = oComando.ExecuteReader();

                if (oLector.HasRows)
                {
                    if (oLector.Read())
                    {
                        oCliente = new Cliente(
                                Convert.ToString(oLector["numeroPasaporte"]),
                                Convert.ToString(oLector["nombre"]),
                                Convert.ToString(oLector["numeroTarjeta"]),
                                Convert.ToString(oLector["contrasenia"])
                            );
                    }
                }
                oLector.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                oConexion.Close();
            }
            return oCliente;
        }

        // ===== AGREGAR CLIENTE ===== //
        public static void Agregar(Cliente cCliente)
        {
            SqlConnection oConexion = new SqlConnection(Conexion.STR);
            SqlCommand oComando = new SqlCommand("AltaCliente", oConexion);
            oComando.CommandType = CommandType.StoredProcedure;

            oComando.Parameters.AddWithValue("@pasaporte", cCliente.NumPasaporte);
            oComando.Parameters.AddWithValue("@nombre", cCliente.Nombre);
            oComando.Parameters.AddWithValue("@tarjeta", cCliente.NumTarCre);
            oComando.Parameters.AddWithValue("@contrasenia", cCliente.Contrasenia);

            SqlParameter oRetorno = new SqlParameter("@Retorno", SqlDbType.Int);
            oRetorno.Direction = ParameterDirection.ReturnValue;
            oComando.Parameters.Add(oRetorno);

            try
            {
                oConexion.Open();
                oComando.ExecuteNonQuery();

                int resultado = (int)oRetorno.Value;

                if (resultado == -1)
                    throw new Exception("Ya existe un cliente con ese código");
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

        // ===== MODIFICAR CLIENTE ===== //
        public static void Modificar(Cliente cCliente)
        {
            SqlConnection oConexion = new SqlConnection(Conexion.STR);
            SqlCommand oComando = new SqlCommand("ModificarCliente", oConexion);
            oComando.CommandType = CommandType.StoredProcedure;

            oComando.Parameters.AddWithValue("@pasaporte", cCliente.NumPasaporte);
            oComando.Parameters.AddWithValue("@nombre", cCliente.Nombre);
            oComando.Parameters.AddWithValue("@tarjeta", cCliente.NumTarCre);
            oComando.Parameters.AddWithValue("@contrasenia", cCliente.Contrasenia);

            SqlParameter oRetorno = new SqlParameter("@Retorno", SqlDbType.Int);
            oRetorno.Direction = ParameterDirection.ReturnValue;
            oComando.Parameters.Add(oRetorno);

            try
            {
                oConexion.Open();
                oComando.ExecuteNonQuery();

                int resultado = (int)oRetorno.Value;

                if (resultado == -1)
                    throw new Exception("No existe cliente con ese nombre, no se modifica");
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

        // ===== ELIMINAR CLIENTE ===== //
        public static void Eliminar(Cliente cCliente)
        {
            SqlConnection oConexion = new SqlConnection(Conexion.STR);
            SqlCommand oComando = new SqlCommand("BajaCliente", oConexion);
            oComando.CommandType = CommandType.StoredProcedure;


            SqlParameter oCodigo = new SqlParameter("@numeroPasaporte", cCliente.NumPasaporte);
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
                    throw new Exception("No existe una cliente con ese pasaporte");

                else if (resultado == -2)
                    throw new Exception("Ocurrió un error inesperado");
                else if (resultado == -3)
                    throw new Exception("Hay pasajes asociados - No se elimina");
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

        // ===== BUSCAR CLIENTE ===== //
        public static Cliente Buscar(string pasaporteCliente)
        {
            SqlConnection oConexion = new SqlConnection(Conexion.STR);
            SqlCommand oComando = new SqlCommand("BuscarCliente", oConexion);
            oComando.CommandType = CommandType.StoredProcedure;

            Cliente cliente = null;
            oComando.Parameters.AddWithValue("@pasaporte", pasaporteCliente);

            try
            {
                oConexion.Open();
                SqlDataReader oReader = oComando.ExecuteReader();

                if (oReader.HasRows)
                {
                    oReader.Read();

                    cliente = new Cliente(oReader["numeroPasaporte"].ToString(), oReader["nombre"].ToString(), oReader["numeroTarjeta"].ToString(), oReader["contrasenia"].ToString());
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

            return cliente;
        }

        // ===== LISTADO DE CLIENTES ===== //
        public static List<Cliente> ListarClientes()
        {

            List<Cliente> colClientes = new List<Cliente>();

            SqlConnection oConexion = new SqlConnection(Conexion.STR);
            SqlCommand oComando = new SqlCommand("ListarCliente", oConexion);
            oComando.CommandType = CommandType.StoredProcedure;

            try
            {
                oConexion.Open();
                SqlDataReader oReader = oComando.ExecuteReader();

                if (oReader.HasRows)
                {
                    while (oReader.Read())
                    {
                        Cliente unCliente = new Cliente(oReader["numeroPasaporte"].ToString(), oReader["nombre"].ToString(), oReader["numeroTarjeta"].ToString(), oReader["contrasenia"].ToString());
                        colClientes.Add(unCliente);
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
            return colClientes;
        }
    }
}
