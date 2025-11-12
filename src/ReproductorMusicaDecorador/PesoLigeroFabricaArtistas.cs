using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReproductorMusicaDecorador
{
    public static class PesoLigeroFabricaArtistas
    {
        private static Dictionary<string, PesoLigeroArtista> artistas = new Dictionary<string, PesoLigeroArtista>();

        public static PesoLigeroArtista ObtenerArtista(string nombre)
        {
            string clave = nombre.ToLower().Trim();
            if (!artistas.ContainsKey(clave))
            {
                artistas[clave] = new PesoLigeroArtista(nombre);
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine($"  [Flyweight] Nuevo artista creado: {nombre}");
                Console.ResetColor();
            }
            return artistas[clave];
        }

        public static void MostrarArtistasCreados()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n-----------------------------------------");
            Console.WriteLine("  ARTISTAS EN MEMORIA (FLYWEIGHT)");
            Console.WriteLine("--------------------------------------------");
            Console.ResetColor();

            if (artistas.Count == 0)
            {
                Console.WriteLine("  Ningún artista registrado aún.");
            }
            else
            {
                int i = 1;
                foreach (var artista in artistas.Values)
                {
                    Console.WriteLine($"  {i}.  {artista.Nombre}");
                    i++;
                }
            }
            Console.WriteLine($"\n  Total de objetos Artista en memoria: {artistas.Count}");
        }

        public static int CantidadArtistas() => artistas.Count;
    }
}
