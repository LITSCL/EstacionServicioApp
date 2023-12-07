using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EstacionServicioModel.DTO;

namespace EstacionServicioModel.DAL
{
    public interface ILecturaDAL
    {
        bool RegistrarLectura(Lectura lectura);
        List<Lectura> ObtenerLecturasTrafico();
        List<Lectura> ObtenerLecturasConsumo();
    }
}
