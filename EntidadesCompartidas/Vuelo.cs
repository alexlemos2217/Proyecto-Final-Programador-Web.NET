using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntidadesCompartidas
{
    public class Vuelo
    {
        // ===== ATRIBUTOS ===== //
        private string codigo;
        private DateTime fecHorSalida;
        private DateTime fecHorLlegada;
        private double precio;
        private int asientos;

        private Aeropuerto aeropuertoSalida;
        private Aeropuerto aeropuertoLlegada;

        // ===== PROPIEDADES ===== //
        public string Codigo
        {
            get { return codigo; }
            set 
            {
                codigo = value;
            }
        }

        public DateTime FecHorSalida
        {
            get { return fecHorSalida; }
            set { fecHorSalida = value; }
        }

        public DateTime FecHorLlegada
        {
            get { return fecHorLlegada; }
            set {

                if (value > FecHorSalida)
                {
                    fecHorLlegada = value; 
                }
                else
                    throw new Exception("La fecha y hora de salida debe ser menor a la de llegada");            
            }
        }

        public double Precio
        {
            get { return precio; }
            set 
            {
                if (value < 0)
                {
                    throw new Exception("El precio no puede ser negativo");
                }
                if (value > 99999999.99)
                {
                    throw new Exception("El precio no puede superar los $99999999.99");
                }
                precio = value;
            }
        }

        public int Asientos
        {
            get { return asientos; }
            set
            {
                if (value < 100 || value > 300)
                {
                    throw new Exception("La cantidad de asientos debe estar en un rango de 100 a 300");
                }
                asientos = value;
            }
        }

        public Aeropuerto AeropuertoSalida
        {
            get { return aeropuertoSalida; }
            set
            {
                aeropuertoSalida = value;
            }
        }

        public Aeropuerto AeropuertoLlegada
        {
            get { return aeropuertoLlegada; }
            set
            {
                if (value != null)
                    aeropuertoLlegada = value;
                else
                    throw new Exception("Debe saberse cual es el aeropuerto");
            }
        }

        // ===== CONSTRUCTOR ===== //
        public Vuelo(string pCodigo, DateTime pFecHorSalida, DateTime pFecHorLlegada, double pPrecio, int pAsientos, Aeropuerto pAeropuertoSalida, Aeropuerto pAeropuertoLlegada)
        {
            Codigo = pCodigo;
            FecHorSalida = pFecHorSalida;
            FecHorLlegada = pFecHorLlegada;
            Precio = pPrecio;
            Asientos = pAsientos;
            AeropuertoSalida = pAeropuertoSalida;
            AeropuertoLlegada = pAeropuertoLlegada;
        }

        // ====== TO STRING ===== //
        public override string ToString()
        {
            return ("Codigo: " + codigo + ", fecha salida: " + fecHorSalida + ", fecha llegada: " + fecHorLlegada + ", precio: " + precio + ", asientos: " + asientos + ", salida: " + aeropuertoSalida.Nombre + ", llegada: " + aeropuertoLlegada.Nombre);
        }
    }
}
