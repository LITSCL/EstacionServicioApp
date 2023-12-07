using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

using EstacionServicioModel.DTO;

namespace EstacionServicioModel.DAL
{
    public class LecturaDALArchivo : ILecturaDAL
    {
        private LecturaDALArchivo()
        {

        }

        private static ILecturaDAL instancia;

        public static ILecturaDAL GetInstancia()
        {
            if (instancia == null)
            {
                instancia = new LecturaDALArchivo();
            }
            return instancia;
        }

        public string archivoConsumo = Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + "consumos.txt";
        public string archivoTrafico = Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + "traficos.txt";
        List<Lectura> lecturasConsumo = new List<Lectura>();
        List<Lectura> lecturasTrafico = new List<Lectura>();

        public List<Lectura> ObtenerLecturasConsumo()
        {  
            try
            {
                string archivo = File.ReadAllText(archivoConsumo);
                lecturasConsumo = JsonConvert.DeserializeObject<List<Lectura>>(archivo);
            } catch (Exception ex)
            {
  
            }
            return lecturasConsumo;
        }

        public List<Lectura> ObtenerLecturasTrafico()
        {  
            try
            {
                string archivo = File.ReadAllText(archivoTrafico);
                lecturasTrafico = JsonConvert.DeserializeObject<List<Lectura>>(archivo);
            }
            catch (Exception ex)
            {

            }
            return lecturasTrafico;
        }

        public bool RegistrarLectura(Lectura lectura)
        {
            string tipoLectura = lectura.Tipo.ToLower();
            if (tipoLectura == "consumo")
            {
                try
                {
                    lecturasConsumo = ObtenerLecturasConsumo();
                    lecturasConsumo.Add(lectura);
                }
                catch (Exception ex)
                {
                    lecturasConsumo.Add(lectura);
                    return false;
                }
                string texto = JsonConvert.SerializeObject(lecturasConsumo);
                File.WriteAllText(archivoConsumo, texto);
                return true;
            }
            else if (tipoLectura == "trafico")
            {  
                try
                {
                    lecturasTrafico = ObtenerLecturasTrafico();
                    lecturasTrafico.Add(lectura);
                }
                catch (Exception ex)
                {
                    lecturasTrafico.Add(lectura);
                    return false;
                }
                string texto = JsonConvert.SerializeObject(lecturasTrafico);
                File.WriteAllText(archivoTrafico, texto);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
