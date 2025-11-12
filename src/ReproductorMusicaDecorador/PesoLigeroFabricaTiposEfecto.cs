using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReproductorMusicaDecorador
{
    public static class PesoLigeroFabricaTiposEfecto
    {
        private static Dictionary<string, PesoLigeroTipoEfecto> tipos = new Dictionary<string, PesoLigeroTipoEfecto>();

        public static PesoLigeroTipoEfecto ObtenerTipo(string tipo)
        {
            if (!tipos.ContainsKey(tipo))
            {
                switch (tipo)
                {
                    case "AltaResolucion":
                        tipos[tipo] = new PesoLigeroTipoEfecto("Alta Resolución", "", "HD Audio de alta calidad");
                        break;
                    case "VelocidadX2":
                        tipos[tipo] = new PesoLigeroTipoEfecto("Velocidad x2", "", "Reproducción acelerada");
                        break;
                    case "Concierto":
                        tipos[tipo] = new PesoLigeroTipoEfecto("Modo Concierto", "", "Efecto Reverb ambiental");
                        break;
                    default:
                        throw new ArgumentException($"Tipo de efecto desconocido: {tipo}");
                }
            }
            return tipos[tipo];
        }

        public static void MostrarTiposCreados()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n");
            Console.WriteLine("  TIPOS DE EFECTOS EN MEMORIA (FLYWEIGHT)");
            Console.ResetColor();

            if (tipos.Count == 0)
            {
                Console.WriteLine("  Ningún tipo de efecto creado aún.");
            }
            else
            {
                foreach (var tipo in tipos.Values)
                {
                    Console.Write("  ");
                    tipo.Mostrar();
                }
            }
            Console.WriteLine($"\n  Total de objetos TipoEfecto en memoria: {tipos.Count}");
            Console.WriteLine("═══════════════════════════════════════════\n");
        }
    }
}
