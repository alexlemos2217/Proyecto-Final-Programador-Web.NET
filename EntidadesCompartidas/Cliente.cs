using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;


namespace EntidadesCompartidas
{
    public class Cliente
    {
        // ===== ATRIBUTOS ===== //
        string numPasaporte;
        string nombre;
        string numTarCre;
        string contrasenia;

        // ===== PROPIEDADES ===== //
        public string NumPasaporte
        {
            get { return numPasaporte; }
            set
            {
                if (value == "")
                {
                    throw new Exception("El número de pasaporte no puede estar vacío");
                }
                if (!value.All(char.IsDigit))
                {
                    throw new Exception("Los dígitos del pasaporte deben ser números");
                }
                if (value.Length > 15)
                {
                    throw new Exception("El número de pasaporte debe contener hasta 15 números");
                }
                numPasaporte = value;
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
                    throw new Exception("El nombre debe contener hasta 30 caracteres");
                }
                nombre = value;
            }
        }

        public string NumTarCre
        {
            get { return numTarCre; }
            set
            {
                if (value == "")
                {
                    throw new Exception("El número de la tarjeta de crédito no puede estar vacío");
                }
                if (!value.All(char.IsDigit))
	            {
		            throw new Exception("Los dígitos de la tarjeta deben ser números");
	            }
                if (value.Length > 15)
                {
                    throw new Exception("El número de la tarjeta no puede superar los 15 dígitos");
                }
                numTarCre = value;
            }
        }

        public string Contrasenia
        {
            get { return contrasenia; }
            set
            {
                if (value == "")
                {
                    throw new Exception("La contraseña no puede estar vacía");
                }
                if (value.Length > 15)
                {
                    throw new Exception("La contraseña no puede superar los 15 dígitos");
                }
                contrasenia = value;
            }
        }

        // ===== CONSTRUCTOR ===== //
        public Cliente(string pNumPasaporte, string pNombre, string pNumTarCre, string pContrasenia)
        {
            NumPasaporte = pNumPasaporte;
            Nombre = pNombre;
            NumTarCre = pNumTarCre;
            Contrasenia = pContrasenia;
        }

        // ===== TO STRING ===== //
        public override string ToString()
        {
            return ("Número de pasaporte: " + numPasaporte + ", nombre: " + nombre + ", número de tarjeta de crédito: " + numTarCre + ", contraseña: " + contrasenia);
        }
    }
}
