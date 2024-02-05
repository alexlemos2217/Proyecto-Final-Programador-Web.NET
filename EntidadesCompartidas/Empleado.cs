using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntidadesCompartidas
{
    public class Empleado
    {
        // ===== ATRIBUTOS ===== //
        private string nombreUsuario;
        private string contrasenia;
        private string nombreCompleto;
        private string labor;

        // ===== PROPIEDADES ===== //
        public string NombreUsuario
        {
            get { return nombreUsuario; }
            set
            {
                if (value == "")
                    throw new Exception("El nombre de usuario no puede estar vacío");
                if (value.Length > 15)
                    throw new Exception("El nombre de usuario no puede superar los 15 caracteres");

                nombreUsuario = value;
            }
        }

        public string Contrasenia
        {
            get { return contrasenia; }
            set
            {
                if (value == "")
                    throw new Exception("La contraseña no puede estar vacío");
                if (value.Length > 30)
                    throw new Exception("La contraseña no puede superar los 15 caracteres");
                contrasenia = value;
            }
        }

        public string NombreCompleto
        {
            get { return nombreCompleto; }
            set
            {
                if (value == "")
                    throw new Exception("El nombre no puede estar vacío");
                if (value.Length > 30)
                    throw new Exception("El nombre no puede superar los 30 caracteres");
                nombreCompleto = value;
            }
        }

        public string Labor
        {
            get { return labor; }
            set
            {
                if (value.Trim().ToUpper() != "GERENTE" && value.Trim().ToUpper() != "VENDEDOR" && value.Trim().ToUpper() != "ADMIN")
                    throw new Exception("Labor deberá ser: GERENTE, VENDEDOR o ADMIN");

                labor = value;
            }
        }

        // ===== CONSTRUCTOR ===== //
        public Empleado(string pNomUsu, string pNomComp, string pContrasenia, string pLabor)
        {
            NombreUsuario = pNomUsu;
            NombreCompleto = pNomComp;
            Contrasenia = pContrasenia;
            Labor = pLabor;
        }

        // ===== TO STRING ===== //
        public override string ToString()
        {
            return "Nombre de usuario: " + nombreUsuario + "<br>Nombre completo: " + nombreCompleto + "<br>Contraseña: " + contrasenia + "<br>Labor: " + labor;
        }

    }
}
