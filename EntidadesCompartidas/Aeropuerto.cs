using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace EntidadesCompartidas
{
    public class Aeropuerto
    {
        // ===== ATRIBUTOS ===== //
        private string codigo;
        private string nombre;
        private string direccion;
        private double impuestoPartida;
        private double impuestoLlegada;

        private Ciudad ciudad;

        // ===== PROPIEDADES ===== //
        public string Codigo
        {
            get { return codigo; }
            set
            {
                if (value == "")
                {
                    throw new Exception("El código no puede estar vacío");
                }
                if (!Regex.IsMatch(value, "^[a-zA-Z]{3}$"))
                {
                    throw new Exception("El código debe ser de 3 letras");
                }

                codigo = value;
            }
        }

        public string Nombre
        {
            get { return nombre; }
            set 
            {
                if (value == "")
                {
                    throw new Exception("El nombre no puede estar vacío");
                }
                if (value.Length > 30)
                {
                    throw new Exception("El nombre no puede superar los 30 caracteres");
                }
                nombre = value;
            }
        }

        public string Direccion
        {
            get { return direccion; }
            set
            {
                if (value == "")
                {
                    throw new Exception("La dirección no puede estar vacía");
                }
                if (value.Length > 30)
                {
                    throw new Exception("La dirección no puede superar los 30 caracteres");
                }
                direccion = value;
            }
        }

        public double ImpuestoPartida
        {
            get { return impuestoPartida; }
            set
            {
                if (value < 0)
                {
                    throw new Exception("El impuesto de partida no puede ser negativo");
                }
                impuestoPartida = value;
            }
        }

        public double ImpuestoLlegada
        {
            get { return impuestoLlegada; }
            set
            {
                if (value < 0)
                {
                    throw new Exception("El impuesto de llegada no puede ser negativo");
                }
                impuestoLlegada = value;
            }
        }

        public Ciudad Ciudad
        {
            get { return ciudad; }
            set 
            {
                if (value != null)
                    ciudad = value;
                else
                    throw new Exception("Debe saberse cual es la ciudad");
            }
        }

        // ===== CONSTRUCTOR ===== //
        public Aeropuerto(string pCodigo, string pNombre, string pDireccion, Ciudad pCiudad,double pImpuestoPartida, double pImpuestoLlegada)
        {
            Codigo = pCodigo;
            Nombre = pNombre;
            Direccion = pDireccion;
            Ciudad = pCiudad;
            ImpuestoPartida = pImpuestoPartida;
            ImpuestoLlegada = pImpuestoLlegada;
        }

        // ===== TO STRING ===== //
        public override string ToString()
        {
            return ("Código, " + codigo + ", nombre: " + nombre + ", direccion: " + direccion + ", ciudad: " + ciudad.NombreCoudad + ", impuesto de partida: " + impuestoPartida + ", impuesto de llegada: " + impuestoLlegada);
        }
    }
}
