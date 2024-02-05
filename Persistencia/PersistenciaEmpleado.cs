using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.SqlClient;
using EntidadesCompartidas;

namespace Persistencia
{
    public class PersistenciaEmpleado
    {
        // ===== LOGUEO EMPLEADO ===== //
        public static Empleado Logueo(string pUsu, string pPass)
        {
            Empleado oEmpleado = null;

            SqlConnection oConexion = new SqlConnection(Conexion.STR);
            SqlCommand oComando = new SqlCommand("LogueoEmpleado", oConexion);
            oComando.CommandType = CommandType.StoredProcedure;

            oComando.Parameters.AddWithValue("@usuario", pUsu);
            oComando.Parameters.AddWithValue("@contrasenia", pPass);

            try
            {
                oConexion.Open();
                SqlDataReader oLector = oComando.ExecuteReader();

                if (oLector.HasRows)
                {
                    if (oLector.Read())
                    {
                        oEmpleado = new Empleado(
                            Convert.ToString(oLector["usuario"]),
                            Convert.ToString(oLector["nombreCompleto"]),
                            Convert.ToString(oLector["contrasenia"]),
                            Convert.ToString(oLector["labor"])
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

            return oEmpleado;
        }
    }
}
