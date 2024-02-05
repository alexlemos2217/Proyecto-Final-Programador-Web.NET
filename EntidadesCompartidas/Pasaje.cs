using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntidadesCompartidas
{
    public class Pasaje
    {
        // ===== ATRIBUTOS ===== //
        private Vuelo vuelo;
        private Cliente cliente;

        private int numVenta;
        private DateTime fecha;
        private double precio;

        // ===== PROPIEDADES ===== //
        public Vuelo oVuelo
        {
            get { return vuelo; }
            set
            {
                if (value != null)
                    vuelo = value;
                else
                    throw new Exception("Debe saberse cual es el vuelo");
            }
        }

        public Cliente oCliente
        {
            get { return cliente; }
            set
            {
                if (value != null)
                    cliente = value;
                else
                    throw new Exception("Debe saberse cual es el cliente");
            }
        }

        public int NumVenta
        {
            get { return numVenta; }
            set
            { numVenta = value; }
        }

        public DateTime Fecha
        {
            get { return fecha; }
            set { fecha = value; }
        }

        public double Precio
        {
            get { return precio; }
            set { precio = value; }
        }

        public Pasaje(Vuelo pVuelo, Cliente pCliente, int pNumVenta, DateTime pFecha, double pPrecio)
        {
            oVuelo = pVuelo;
            oCliente = pCliente;
            NumVenta = pNumVenta;
            Fecha = pFecha;
            Precio = pPrecio;
        }

        // ===== TO STRING ===== //
        public override string ToString()
        {
            return ("Codigo: " + numVenta + ", Fecha: " + fecha +  ", Precio: " + precio + " , Vuelo: " + vuelo.Codigo + " , Cliente " + cliente.Nombre);
        }
    }
}
