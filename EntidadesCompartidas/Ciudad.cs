using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace EntidadesCompartidas
{
    public class Ciudad
    {
        // ===== ATRIBUTOS ===== //
        private string codigo;
        private string nombreCiudad;
        private string pais;

        // ===== PROPIEDADES ===== //
        public string Codigo
        {
            get { return codigo; }
            set
            {
                if (value.Trim() == "")
                {
                    throw new Exception("El código no puede estar vacío");
                }
                if (!Regex.IsMatch(value, "^[a-zA-Z]{6}$"))
                {
                    throw new Exception("El código debe tener 6 letras, nombre de la ciudad y país");
                }
                codigo = value;
            }
        }

        public string NombreCoudad
        {
            get {  return nombreCiudad; }
            set
            {
                if (value.Trim() == "")
                {
                    throw new Exception("El nombre de la ciudad no puede estar vacío");
                }
                if (value.Length < 0 || value.Length > 30)
                {
                    throw new Exception("El nombre de la ciudad no puede superar los 30 caracteres");
                }
                nombreCiudad = value;
            }
        }

        public string Pais
        {
            get { return pais; }
            set
            {
                if (value.Trim() == "")
                {
                    throw new Exception("El nombre del país no puede estar vacío");
                }
                if (value.Length < 0 || value.Length > 30)
                {
                    throw new Exception("El nombre del país no puede superar los 30 caracteres");
                }
                pais = value;
            }
        }

        // ===== CONSTRUCTOR ===== //
        public Ciudad(string pCodigo, string pNombreCiudad, string pPais)
        {
            Codigo = pCodigo;
            NombreCoudad = pNombreCiudad;
            Pais = pPais;
        }

        // ===== TO STRING ===== //
        public override string ToString()
        {
            return ("Codigo: " + codigo + ", nombre de la ciudad: " + nombreCiudad + ", país: " + pais);
        }
    }
}
