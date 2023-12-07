using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstacionServicioModel.DTO
{
    public class Lectura
    {
        int medidor;
        string fecha;
        string valor;
        string tipo;
        string estado;
        //string unidadMedida;

        public int Medidor { get => medidor; set => medidor = value; }
        public string Fecha { get => fecha; set => fecha = value; }
        public string Valor { get => valor; set => valor = value; }
        public string Tipo { get => tipo; set => tipo = value; }
        public string Estado { get => estado; set => estado = value; }
        //public string UnidadMedida { get => unidadMedida; set => unidadMedida = value; }
    }
}
