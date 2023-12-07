using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EstacionServicioModel.DTO;

namespace EstacionServicioModel.DAL
{
    public interface IMedidorConsumoDAL
    {
        List<MedidorConsumo> ObtenerMedidores();
    }
}
