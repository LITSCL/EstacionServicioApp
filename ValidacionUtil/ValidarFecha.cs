using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValidacionUtil
{
    public class ValidarFecha
    {
        public bool Validar(string fecha)
        {
            string[] fechaArray = fecha.Split('-');
            int contadorCaracteres = 0;
            int contadorValidaciones = 0;

            //1. Comprobar que vienen la cantidad de datos necesarios.
            if (fechaArray.Length == 6)
            {
                if (fechaArray[0].Length == 4)
                {
                    contadorCaracteres++;
                }
                if (fechaArray[1].Length == 2)
                {
                    contadorCaracteres++;
                }
                if (fechaArray[2].Length == 2)
                {
                    contadorCaracteres++;
                }
                if (fechaArray[3].Length == 2)
                {
                    contadorCaracteres++;
                }
                if (fechaArray[4].Length == 2)
                {
                    contadorCaracteres++;
                }
                if (fechaArray[5].Length == 2)
                {
                    contadorCaracteres++;
                }
                
                if (contadorCaracteres == 6)
                {
                    try
                    {
                        //2. Comprobar que todo lo que viene son número enteros.
                        int yearInt = Convert.ToInt32(fechaArray[0]);
                        int mesInt = Convert.ToInt32(fechaArray[1]);
                        int diaInt = Convert.ToInt32(fechaArray[2]);
                        int horaInt = Convert.ToInt32(fechaArray[3]);
                        int minutoInt = Convert.ToInt32(fechaArray[4]);
                        int segundoInt = Convert.ToInt32(fechaArray[5]);

                        //3. Comprobar que el año es valido.
                        if (yearInt > 999 && yearInt < 10000)
                        {
                            contadorValidaciones++;
                        }
                        //4. Comprobar que el mes es valido.
                        if (mesInt > 0 && mesInt < 13)
                        {
                            contadorValidaciones++;
                        }
                        //5. Comprobar que el dia es valido.
                        if (diaInt > 0 && diaInt < 32)
                        {
                            contadorValidaciones++;
                        }
                        //6. Comprobar que la hora es valida.
                        if (horaInt > -1 && horaInt < 25)
                        {
                            contadorValidaciones++;
                        }
                        //7. Comprobar que los minutos son validos.
                        if (minutoInt > -1 && minutoInt < 61)
                        {
                            contadorValidaciones++;
                        }
                        //8. Comprobar que los segundos son validos.
                        if (segundoInt > -1 && segundoInt < 61)
                        {
                            contadorValidaciones++;
                        }

                        if (contadorValidaciones == 6)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    catch (Exception ex)
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
