using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using SocketUtil;

namespace EstacionServicioApp.Hilos
{
    public class HiloServer
    {
        private int puerto;
        private ServerSocket server;

        public HiloServer(int puerto)
        {
            this.puerto = puerto;
        }

        public void Ejecutar()
        {
            server = new ServerSocket(puerto);
            Console.WriteLine("Iniciando servidor en el puerto {0}", puerto);

            if (server.Iniciar())
            {
                Console.WriteLine("Servidor iniciado");
                while (true)
                {
                    Console.WriteLine("Esperando clientes...");
                    ClienteSocket clienteSocket = server.ObtenerCliente(); //Aquí el programa queda pausado a la espera que se conecte un cliente al Socket.
                    HiloCliente hiloCliente = new HiloCliente(clienteSocket);
                    Console.WriteLine("Se conecto un cliente");
                    try
                    {
                        Thread t = new Thread(new ThreadStart(hiloCliente.Ejecutar));
                        t.IsBackground = true;
                        t.Start();
                    } catch (Exception ex)
                    {

                    }
                    

                }
            }
        }
    }
}
