using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReproductorMusicaDecorador
{
    public static class PesoLigeroFabricaAlbumes
    {
        private static Dictionary<string, PesoLigeroAlbum> albumes = new Dictionary<string, PesoLigeroAlbum>();

        public static PesoLigeroAlbum ObtenerAlbum(string titulo, string año)
        {
            string clave = $"{titulo.ToLower().Trim()}_{año}";
            if (!albumes.ContainsKey(clave))
            {
                albumes[clave] = new PesoLigeroAlbum(titulo, año);
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine($"  [Peso Pluma] Nuevo álbum creado: {titulo} ({año})");
                Console.ResetColor();
            }
            return albumes[clave];
        }

        public static void MostrarAlbumesCreados()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  ÁLBUMES EN MEMORIA (Peso liguerito)");

            Console.ResetColor();

            if (albumes.Count == 0)
            {
                Console.WriteLine("  Ningún álbum registrado aún.");
            }
            else
            {
                int i = 1;
                foreach (var album in albumes.Values)
                {
                    Console.WriteLine($"  {i}.  {album.Titulo} ({album.Año})");
                    i++;
                }
            }
            Console.WriteLine($"\n  Total de objetos Album en memoria: {albumes.Count}");
        }

        public static int CantidadAlbumes() => albumes.Count;
    }
}
