using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstacionServicioModel.DTO
{
    public class Medidor
    {
        int id;
        string fechaInstalacion;

        public int Id { get => id; set => id = value; }
        public string FechaInstalacion { get => fechaInstalacion; set => fechaInstalacion = value; }

        public bool EnviarLectura(Lectura lectura)
        {
            return true;
        }
    }
}
