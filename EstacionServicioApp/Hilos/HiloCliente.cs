using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EstacionServicioModel.DAL;
using EstacionServicioModel.DTO;
using SocketUtil;
using ValidacionUtil;

namespace EstacionServicioApp.Hilos
{
    public class HiloCliente
    {
        private ClienteSocket clienteSocket;
        private static ILecturaDAL dalILectura = LecturaDALFactory.CreateDal();
        private IMedidorConsumoDAL dalIMedidorConsumo = MedidorConsumoDALFactory.CreateDal();
        private IMedidorTraficoDAL dalIMedidorTrafico = MedidorTraficoDALFactory.CreateDal();
        private ValidarTuberias vt = new ValidarTuberias();
        private SepararMensaje sm = new SepararMensaje();
        private ValidarFecha vf = new ValidarFecha();
        private ValidarNumero vn = new ValidarNumero();
        private ValidarTipoMedidor vtm = new ValidarTipoMedidor();
        private ValidarValor vv = new ValidarValor();
        private ValidarEstado ve = new ValidarEstado();
        private ConvertirEstado ce = new ConvertirEstado();
        private ValidarMensajeUPDATE vmu = new ValidarMensajeUPDATE();

        public HiloCliente(ClienteSocket clienteSocket)
        {
            this.clienteSocket = clienteSocket;
        }

        public void Ejecutar()
        {
            bool primerMensaje = false;
            bool segundoMensajeHabilitado = false;
            bool segundoMensaje = false;
            bool existeMedidor1 = false;
            bool existeMedidor2 = false;

            //1. Crear los auxiliares.
            string fechaAuxiliar1 = "";
            string numeroAuxiliar1 = "";
            string tipoAuxiliar1 = "";

            string numeroAuxiliar2 = "";
            string fechaAuxiliar2 = "";
            string tipoAuxiliar2 = "";
            string valorAuxiliar = "";
            string estadoAuxiliar = "";
            string final = "";

            try
            {
                //2. Recibir el mensaje.
                string mensaje1 = clienteSocket.Leer().Trim(); //Aquí debe llegar FECHA|NUMERO|TIPO.    

                //3. Validar si vienen la cantidad de tuberias correctas.
                if (vt.Validar(mensaje1, 2) == true)
                {
                    //4. Separar el mensaje (Cantidad de tuberias + 1).
                    string[] mensajeSeparado = sm.Separar(mensaje1);
                    fechaAuxiliar1 = mensajeSeparado[0];
                    numeroAuxiliar1 = mensajeSeparado[1];
                    tipoAuxiliar1 = mensajeSeparado[2];

                    //5. Validar la fecha.
                    if (vf.Validar(fechaAuxiliar1) == true)
                    {
                        //6. Validar el número de serie.
                        if (vn.Validar(numeroAuxiliar1) == true)
                        {
                            //7. Validar el tipo de medidor.
                            if (vtm.Validar(tipoAuxiliar1) == true)
                            {
                                primerMensaje = true;
                            }
                        }
                    }
                }

                //8. Comprobar si el mensaje tenía la estructura correcta.
                if (primerMensaje == true)
                {
                    //9. Covertir los auxiliares en el tipo de dato del modelo.
                    string fecha1 = fechaAuxiliar1;
                    int numero1 = Convert.ToInt32(numeroAuxiliar1);
                    string tipo1 = tipoAuxiliar1;

                    //10. Obtener fecha del servidor.
                    DateTime ahora = DateTime.Now;
                    string fechaServidor = ahora.ToString("yyyy-MM-dd-hh-mm-ss");

                    //11. Verificar que tipo de medidor es el que envió el mensaje, verificar si existe ese medidor y comparar si las fechas cumplen con la diferencia de tiempo (Fecha cliente-servidor).
                    if (tipo1.ToLower() == "consumo")
                    {
                        List<MedidorConsumo> medidoresConsumo = dalIMedidorConsumo.ObtenerMedidores();
                        foreach (MedidorConsumo mc in medidoresConsumo)
                        {
                            if (mc.Id == numero1)
                            {
                                existeMedidor1 = true;
                                break;
                            }
                        }
                        if (existeMedidor1 == true)
                        {
                            //TODO: Comparar fecha server-cliente.
                            clienteSocket.Escribir(fechaServidor + "|" + "WAIT");
                            segundoMensajeHabilitado = true;
                        }
                        else
                        {
                            clienteSocket.Escribir(fechaAuxiliar1 + "|" + numeroAuxiliar1 + "|" + "ERROR");
                            clienteSocket.CerrarConexion();
                        }
                    }
                    else if (tipo1.ToLower() == "trafico")
                    {
                        List<MedidorTrafico> medidoresTrafico = dalIMedidorTrafico.ObtenerMedidores();
                        foreach (MedidorTrafico mt in medidoresTrafico)
                        {
                            if (mt.Id == numero1)
                            {
                                existeMedidor1 = true;
                                break;
                            }
                        }
                        if (existeMedidor1 == true)
                        {
                            //TODO: Comparar fecha server-cliente.
                            clienteSocket.Escribir(fechaServidor + "|" + "WAIT");
                            segundoMensajeHabilitado = true;
                        }
                        else
                        {
                            clienteSocket.Escribir(fechaAuxiliar1 + "|" + numeroAuxiliar1 + "|" + "ERROR");
                            clienteSocket.CerrarConexion();
                        }
                    }
                    else
                    {
                        clienteSocket.Escribir(fechaAuxiliar1 + "|" + numeroAuxiliar1 + "|" + "ERROR");
                        clienteSocket.CerrarConexion();
                    }
                }
                else
                {
                    clienteSocket.Escribir(fechaAuxiliar1 + "|" + numeroAuxiliar1 + "|" + "ERROR");
                    clienteSocket.CerrarConexion();
                }

                if (segundoMensajeHabilitado == true)
                {
                    //1. Recibir el mensaje.
                    string mensaje2 = clienteSocket.Leer().Trim(); //Aquí debe llegar NUMERO|FECHA|TIPO|VALOR|UPDATE ó NUMERO|FECHA|TIPO|VALOR|ESTADO|UPDATE.

                    //2. Validar si vienen la cantidad de tuberias correctas.
                    if (vt.Validar(mensaje2, 4) == true)
                    {
                        //3. Separar el mensaje (Cantidad de tuberias + 1).
                        string[] mensajeSeparado = sm.Separar(mensaje2);
                        numeroAuxiliar2 = mensajeSeparado[0];
                        fechaAuxiliar2 = mensajeSeparado[1];
                        tipoAuxiliar2 = mensajeSeparado[2];
                        valorAuxiliar = mensajeSeparado[3];
                        final = mensajeSeparado[4];

                        //4. Validar el número de serie.
                        if (vn.Validar(numeroAuxiliar2) == true)
                        {
                            //5. Validar la fecha.
                            if (vf.Validar(fechaAuxiliar2) == true)
                            {
                                //6. Validar el tipo de medidor.
                                if (vtm.Validar(tipoAuxiliar2) == true)
                                {
                                    //7. Validar valor de la medición.
                                    if (vv.Validar(valorAuxiliar) == true)
                                    {
                                        //8. Comprobar que el mensaje final sea UPDATE.
                                        if (vmu.Validar(final) == true)
                                        {
                                            segundoMensaje = true;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else if (vt.Validar(mensaje2, 5) == true)
                    {
                        //3. Separar el mensaje (Cantidad de tuberias + 1).
                        string[] mensajeSeparado = sm.Separar(mensaje2);
                        numeroAuxiliar2 = mensajeSeparado[0];
                        fechaAuxiliar2 = mensajeSeparado[1];
                        tipoAuxiliar2 = mensajeSeparado[2];
                        valorAuxiliar = mensajeSeparado[3];
                        estadoAuxiliar = mensajeSeparado[4];
                        final = mensajeSeparado[5];

                        //4. Validar el número de serie.
                        if (vn.Validar(numeroAuxiliar2) == true)
                        {
                            //5. Validar la fecha.
                            if (vf.Validar(fechaAuxiliar2) == true)
                            {
                                //6. Validar el tipo de medidor.
                                if (vtm.Validar(tipoAuxiliar2) == true)
                                {
                                    //7. Validar valor de la medición.
                                    if (vv.Validar(valorAuxiliar) == true)
                                    {
                                        //8. Validar el estado del medidor y convertirlo a lenguaje humano.
                                        if (ve.Validar(estadoAuxiliar) == true)
                                        {
                                            estadoAuxiliar = ce.Convertir(estadoAuxiliar);
                                            //9. Comprobar que el mensaje final sea UPDATE.
                                            if (vmu.Validar(final) == true)
                                            {
                                                segundoMensaje = true;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                //9-10. Comprobar si el mensaje tenía la estructura correcta.
                if (segundoMensaje == true)
                {
                    //11. Covertir los auxiliares en el tipo de dato del modelo.
                    int numero2 = Convert.ToInt32(numeroAuxiliar2);
                    string fecha2 = fechaAuxiliar2;
                    string tipo2 = tipoAuxiliar2;
                    string valor = valorAuxiliar;
                    string estado = estadoAuxiliar;
                    if (estado == "")
                    {
                        estado = "null";
                    }
                    string finish = final;

                    //12. Verificar que el numero y tipo de medidor corresponden a los enviados en el primer mensaje.
                    if (numeroAuxiliar1 == numeroAuxiliar2 && tipoAuxiliar1 == tipoAuxiliar2)
                    {
                        //13. Verificar que tipo de medidor es el que envió el mensaje, verificar si existe ese medidor y guardar la lectura.
                        if (tipo2.ToLower() == "consumo")
                        {
                            List<MedidorConsumo> medidoresConsumo = dalIMedidorConsumo.ObtenerMedidores();
                            foreach (MedidorConsumo mc in medidoresConsumo)
                            {
                                if (mc.Id == numero2)
                                {
                                    existeMedidor2 = true;
                                    break;
                                }
                            }
                            if (existeMedidor2 == true)
                            {
                                //14. Crear la instancia de tipo Lectura.
                                Lectura lectura = new Lectura()
                                {
                                    Medidor = numero2,
                                    Fecha = fecha2,
                                    Tipo = tipo2,
                                    Valor = valor,
                                    Estado = estado
                                };

                                //15. Guardar lectura en el documento.
                                lock (dalILectura)
                                {
                                    if (dalILectura.RegistrarLectura(lectura) == true)
                                    {
                                        clienteSocket.Escribir(numero2 + "|" + "OK");
                                        Console.WriteLine("Cliente desconectado");
                                        clienteSocket.CerrarConexion();
                                    }
                                    else
                                    {
                                        clienteSocket.Escribir(fechaAuxiliar1 + "|" + numeroAuxiliar1 + "|" + "ERROR");
                                        Console.WriteLine("Cliente desconectado");
                                        clienteSocket.CerrarConexion();
                                    }
                                }
                            }
                            else
                            {
                                clienteSocket.Escribir(fechaAuxiliar1 + "|" + numeroAuxiliar1 + "|" + "ERROR");
                                Console.WriteLine("Cliente desconectado");
                                clienteSocket.CerrarConexion();
                            }
                        }
                        else if (tipo2.ToLower() == "trafico")
                        {
                            List<MedidorTrafico> medidoresTrafico = dalIMedidorTrafico.ObtenerMedidores();
                            foreach (MedidorTrafico mt in medidoresTrafico)
                            {
                                if (mt.Id == numero2)
                                {
                                    existeMedidor2 = true;
                                    break;
                                }
                            }
                            if (existeMedidor2 == true)
                            {
                                //14. Crear la instancia de tipo Lectura.
                                Lectura lectura = new Lectura()
                                {
                                    Medidor = numero2,
                                    Fecha = fecha2,
                                    Tipo = tipo2,
                                    Valor = valor,
                                    Estado = estado
                                };

                                //15. Guardar lectura en el documento.
                                lock (dalILectura)
                                {
                                    if (dalILectura.RegistrarLectura(lectura) == true)
                                    {
                                        clienteSocket.Escribir(numero2 + "|" + "OK");
                                        Console.WriteLine("Cliente desconectado");
                                        clienteSocket.CerrarConexion();
                                    }
                                    else
                                    {
                                        clienteSocket.Escribir(fechaAuxiliar1 + "|" + numeroAuxiliar1 + "|" + "ERROR");
                                        Console.WriteLine("Cliente desconectado");
                                        clienteSocket.CerrarConexion();
                                    }
                                }
                            }
                            else
                            {
                                clienteSocket.Escribir(fechaAuxiliar1 + "|" + numeroAuxiliar1 + "|" + "ERROR");
                                Console.WriteLine("Cliente desconectado");
                                clienteSocket.CerrarConexion();
                            }
                        }
                        else
                        {
                            clienteSocket.Escribir(fechaAuxiliar1 + "|" + numeroAuxiliar1 + "|" + "ERROR");
                            Console.WriteLine("Cliente desconectado");
                            clienteSocket.CerrarConexion();
                        }
                    }
                    else
                    {
                        clienteSocket.Escribir(fechaAuxiliar1 + "|" + numeroAuxiliar1 + "|" + "ERROR");
                        Console.WriteLine("Cliente desconectado");
                        clienteSocket.CerrarConexion();
                    }
                }
                else
                {
                    clienteSocket.Escribir(fechaAuxiliar1 + "|" + numeroAuxiliar1 + "|" + "ERROR");
                    Console.WriteLine("Cliente desconectado");
                    clienteSocket.CerrarConexion();
                }
            } catch (Exception ex)
            {
                Console.WriteLine("Cliente desconectado"); //Se muestra si el cliente se desconecta del servidor a proposito.
            }
        }
    }
}
