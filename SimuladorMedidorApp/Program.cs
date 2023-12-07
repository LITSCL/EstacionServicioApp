
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

using SocketUtil;
using ValidacionUtil;

namespace SimuladorMedidorApp
{
    class Program
    {
        static void Main(string[] args)
        {
            SepararMensaje sm = new SepararMensaje();
            string eleccion = "";
            string fecha = "";
            string numero = "";
            string tipo = "";
            string valor = "";
            string estado = "";
            string estructuraMensajeNuestro;
            string mensajeRecibido;
            string[] mensajeSeparado;

            string ip = ConfigurationManager.AppSettings["ip"];
            int puerto = Convert.ToInt32(ConfigurationManager.AppSettings["puerto"]);
            ClienteSocket clienteSocket = new ClienteSocket(ip, puerto);
        
            do
            {
                Console.WriteLine("Preparando primer mensaje...");
                Console.WriteLine("1. Medidor de tráfico");
                Console.WriteLine("2. Medidor de consumo");

                eleccion = Console.ReadLine();

                switch (eleccion)
                {
                    case "1":
                        tipo = "trafico";
                        break;
                    case "2":
                        tipo = "consumo";
                        break;
                    default:
                        eleccion = "";
                        break;
                } 
            } while (eleccion == "");

            if (clienteSocket.Conectar())
            {
                Console.WriteLine("Preparando primer mensaje...");
                Console.WriteLine("Ingrese el numero del medidor (1)");
                numero = Console.ReadLine();

                Console.WriteLine("Preparando primer mensaje...");
                Console.WriteLine("Ingrese la fecha actual (2021-06-05-12-40-15)");
                fecha = Console.ReadLine();

                estructuraMensajeNuestro = fecha + "|" + numero + "|" + tipo;

                clienteSocket.Escribir(estructuraMensajeNuestro);

                mensajeRecibido = clienteSocket.Leer();
                Console.WriteLine("Servidor dice: {0}", mensajeRecibido);

                mensajeSeparado = sm.Separar(mensajeRecibido);
                if (mensajeSeparado[1] == "WAIT")
                {
                    Console.WriteLine("Preparando segundo mensaje...");
                    Console.WriteLine("Ingrese el numero del medidor (1)");
                    numero = Console.ReadLine();

                    Console.WriteLine("Preparando segundo mensaje...");
                    Console.WriteLine("Ingrese la fecha actual (2011-11-29-12-40-15)");
                    fecha = Console.ReadLine();

                    Console.WriteLine("Preparando segundo mensaje...");
                    Console.WriteLine("Ingrese el tipo de medidor (Trafico; Consumo)");
                    tipo = Console.ReadLine();

                    Console.WriteLine("Preparando segundo mensaje...");
                    Console.WriteLine("Ingrese el valor de la medición (500)");
                    valor = Console.ReadLine();

                    eleccion = "";
                    do
                    {
                        Console.WriteLine("Preparando segundo mensaje...");
                        Console.WriteLine("¿Desea envíar un estado?");
                        Console.WriteLine("1. Si");
                        Console.WriteLine("2. No");

                        eleccion = Console.ReadLine();

                        switch (eleccion)
                        {
                            case "1":
                                eleccion = "Si";
                                break;
                            case "2":
                                eleccion = "No";
                                break;
                            default:
                                eleccion = "";
                                break;
                        }
                    } while (eleccion == "");

                    if (eleccion == "Si")
                    {
                        eleccion = "";
                        do
                        {
                            Console.WriteLine("Preparando segundo mensaje...");
                            Console.WriteLine("Seleccione el código de estado");
                            Console.WriteLine("1. -1");
                            Console.WriteLine("2.  0");
                            Console.WriteLine("3.  1");
                            Console.WriteLine("4.  2");

                            eleccion = Console.ReadLine();

                            switch (eleccion)
                            {
                                case "1":
                                    eleccion = "-1";
                                    estado = eleccion;
                                    break;
                                case "2":
                                    eleccion = "0";
                                    estado = eleccion;
                                    break;
                                case "3":
                                    eleccion = "1";
                                    estado = eleccion;
                                    break;
                                case "4":
                                    eleccion = "2";
                                    estado = eleccion;
                                    break;
                                default:
                                    eleccion = "";
                                    break;
                            }
                        } while (eleccion == "");

                        estructuraMensajeNuestro = numero + "|" + fecha + "|" + tipo + "|" + valor + "|" + estado + "|" + "UPDATE";
                        clienteSocket.Escribir(estructuraMensajeNuestro);

                        mensajeRecibido = clienteSocket.Leer();
                        Console.WriteLine("Servidor dice: {0}", mensajeRecibido);

                        Console.WriteLine("Fuiste desconectado del servidor, presiona una tecla para salir...");
                    }
                    else
                    {
                        estructuraMensajeNuestro = numero + "|" + fecha + "|" + tipo + "|" + valor + "|" + "UPDATE";
                        clienteSocket.Escribir(estructuraMensajeNuestro);

                        mensajeRecibido = clienteSocket.Leer();
                        Console.WriteLine("Servidor dice: {0}", mensajeRecibido);

                        Console.WriteLine("Fuiste desconectado del servidor, presiona una tecla para salir...");
                    }
                }
                else
                {
                    mensajeRecibido = clienteSocket.Leer();
                    Console.WriteLine("Fuiste desconectado del servidor, presiona una tecla para salir...");
                }
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("El servidor no se encuentra disponible");
                Console.ReadKey();
            }
        }
    }
}
