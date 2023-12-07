using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EstacionServicioModel.DTO;

namespace EstacionServicioModel.DAL
{
    public interface IMedidorTraficoDAL
    {
        List<MedidorTrafico> ObtenerMedidores();
    }
}
