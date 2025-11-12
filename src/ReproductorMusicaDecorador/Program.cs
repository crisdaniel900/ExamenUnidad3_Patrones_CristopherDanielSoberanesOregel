using System;
using System.IO;
using System.Media;
using System.Threading;
using System.Collections.Generic;

namespace ReproductorMusicaDecorador
{
    class ReproductorMusica
    {
        static Dictionary<string, PesoLigeroArtista> artistasPorCancion = new Dictionary<string, PesoLigeroArtista>();
        static Dictionary<string, PesoLigeroAlbum> albumesPorCancion = new Dictionary<string, PesoLigeroAlbum>();
        static SoundPlayer player;
        static string[] archivos;
        static int indiceActual = 0;
        static bool reproduciendo = false;
        static DecoradorComponenteCancion cancionActual;


        static void Main(string[] args)
        {
            Console.Title = "Reproductor de Musica ";
            Console.ForegroundColor = ConsoleColor.Cyan;

            Console.WriteLine("REPRODUCTOR DE MUSICA - CONSOLA");
            Console.ResetColor();

            Console.Write("Ingresa la ruta de la carpeta con archivos .wav: ");
            string carpeta = Console.ReadLine();

            if (!Directory.Exists(carpeta))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error: La carpeta no existe.");
                Console.ResetColor();
                Console.ReadKey();
                return;
            }

            archivos = Directory.GetFiles(carpeta, "*.wav");

            if (archivos.Length == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No se encontraron archivos .wav en la carpeta.");
                Console.ResetColor();
                Console.ReadKey();
                return;
            }

            player = new SoundPlayer();
            cancionActual = new DecoradorCancionBasica(archivos[indiceActual]);
            MostrarMenu();
        }

        static void MostrarMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"  CANCION ACTUAL:");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine(cancionActual.Descripcion);
                Console.ResetColor();

                // Mostrar efectos activos
                string efectosActivos = ObtenerEfectosActivos();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"\n  Estado: {(reproduciendo ? " REPRODUCIENDO" : " PAUSADO")}");
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine($"  Efectos: {efectosActivos}");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"  Pista {indiceActual + 1} de {archivos.Length}");
                Console.WriteLine("------------------------------------------\n");
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("LISTA DE CANCIONES:");
                Console.ResetColor();

                for (int i = 0; i < archivos.Length; i++)
                {
                    if (i == indiceActual)
                        Console.ForegroundColor = ConsoleColor.Cyan;
                    else
                        Console.ForegroundColor = ConsoleColor.White;

                    Console.WriteLine($"  {i + 1}. {Path.GetFileName(archivos[i])}");
                }
                Console.ResetColor();

                Console.WriteLine("\n-------------------------------------------");
                Console.WriteLine("CONTROLES:");
                Console.WriteLine("  [P] Reproducir/Pausar");
                Console.WriteLine("  [N] Siguiente cancion");
                Console.WriteLine("  [A] Canción anterior");
                Console.WriteLine("  [S] Detener");
                Console.WriteLine("  [E] Agregar efectos");
                Console.WriteLine("  [R] Resetear efectos");
                Console.WriteLine("  [M] Editar Informacion (Artista/Álbum)");
                Console.WriteLine("  [F] Ver Flyweight (memoria)");
                Console.WriteLine("  [1-9] Seleccionar canción");
                Console.WriteLine("  [Q] Salir");
                Console.WriteLine("-----------------------------------------------\n");

                Console.Write("Opción: ");
                var tecla = Console.ReadKey(true);

                switch (char.ToUpper(tecla.KeyChar))
                {
                    case 'P':
                        ReproducirPausar();
                        break;
                    case 'N':
                        Siguiente();
                        break;
                    case 'A':
                        Anterior();
                        break;
                    case 'S':
                        Detener();
                        break;
                    case 'E':
                        AgregarEfectos();
                        break;
                    case 'R':
                        ResetearEfectos();
                        break;
                    case 'F':
                        MostrarFlyweight();
                        break;
                    case 'M':
                        EditarMetadatos();
                        break;
                    case 'Q':
                        Detener();
                        return;
                    default:
                        if (char.IsDigit(tecla.KeyChar))
                        {
                            int num = int.Parse(tecla.KeyChar.ToString());
                            if (num > 0 && num <= archivos.Length)
                            {
                                indiceActual = num - 1;
                                CambiarCancion();
                            }
                        }
                        break;
                }

                Thread.Sleep(100);
            }
        }

        static void EditarMetadatos()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(" EDITAR INFO DE CANCION  \n");
            Console.ResetColor();

            string ruta = archivos[indiceActual];
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"Canción: {Path.GetFileName(ruta)}\n");
            Console.ResetColor();

            Console.Write("Nombre del Artista: ");
            string nombreArtista = Console.ReadLine();

            Console.Write("Título del Álbum: ");
            string tituloAlbum = Console.ReadLine();

            Console.Write("Año del Álbum: ");
            string añoAlbum = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(añoAlbum))
                añoAlbum = "Desconocido";

            PesoLigeroArtista artista = null;
            PesoLigeroAlbum album = null;

            if (!string.IsNullOrWhiteSpace(nombreArtista))
            {
                artista = PesoLigeroFabricaArtistas.ObtenerArtista(nombreArtista);
                artistasPorCancion[ruta] = artista;
            }

            if (!string.IsNullOrWhiteSpace(tituloAlbum))
            {
                album = PesoLigeroFabricaAlbumes.ObtenerAlbum(tituloAlbum, añoAlbum);
                albumesPorCancion[ruta] = album;
            }

            
            cancionActual = new DecoradorCancionBasica(ruta, artista, album);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n✓ Metadatos actualizados correctamente");
            Console.ResetColor();
            Thread.Sleep(2000);
        }


        
        static void MostrarFlyweight()
        {
            Console.Clear();

           
            PesoLigeroFabricaTiposEfecto.MostrarTiposCreados();

         
            PesoLigeroFabricaArtistas.MostrarArtistasCreados();

            
            PesoLigeroFabricaAlbumes.MostrarAlbumesCreados();

           
            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.WriteLine("\nPresiona cualquier tecla para continuar...");
            Console.ReadKey();
        }

        static void AgregarEfectos()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Magenta;

            Console.WriteLine("MENÚ DE EFECTOS DE AUDIO       \n");
            Console.ResetColor();

            Console.WriteLine("Selecciona un efecto para aplicar:");
            Console.WriteLine("  1) Alta Resolucion (HD Audio)");
            Console.WriteLine("  2) Velocidad x2");
            Console.WriteLine("  3) Modo Concierto");
            Console.WriteLine("  8) Volver");

            Console.Write("\nOpción: ");
            string opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    cancionActual = new DecoradorEfectoAltaResolucion(cancionActual);
                    MostrarMensajeEfecto(" Alta Resolucion aplicada");
                    break;
                case "2":
                    cancionActual = new DecoradorEfectoVelocidadX2(cancionActual);
                    MostrarMensajeEfecto(" Velocidad x2 aplicada");
                    break;
                case "3":
                    cancionActual = new DecoradorEfectoConcierto(cancionActual);
                    MostrarMensajeEfecto(" Modo Concierto activado");
                    break;
            }
        }

        static void MostrarMensajeEfecto(string mensaje)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n{mensaje}");
            Console.ResetColor();
            Console.WriteLine("\n¿Deseas agregar otro efecto?");
            Console.WriteLine("1) Sí");
            Console.WriteLine("2) No");
            Console.Write("\nOpción: ");
            string respuesta = Console.ReadLine();

            if (respuesta == "1")
            {
                AgregarEfectos();
            }
        }

        static void ResetearEfectos()
        {
           
            PesoLigeroArtista artista = cancionActual.Artista;
            PesoLigeroAlbum album = cancionActual.Album;
            cancionActual = new DecoradorCancionBasica(archivos[indiceActual], artista, album);

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(" Todos los efectos han sido removidos");
            Console.WriteLine("  (Metadatos de artista y álbum conservados)");
            Console.ResetColor();
            Thread.Sleep(2000);
        }

        static void ReproducirPausar()
        {
            if (reproduciendo)
            {
                player.Stop();
                reproduciendo = false;
            }
            else
            {
                ReproducirCancion();
            }
        }

        static void ReproducirCancion()
        {
            try
            {
                player.SoundLocation = cancionActual.RutaArchivo;
                player.Play();
                reproduciendo = true;
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\nError al reproducir: {ex.Message}");
                Console.ResetColor();
                Thread.Sleep(2000);
                reproduciendo = false;
            }
        }

        static void CambiarCancion()
        {
            string ruta = archivos[indiceActual];
            PesoLigeroArtista artista = artistasPorCancion.ContainsKey(ruta) ? artistasPorCancion[ruta] : null;
            PesoLigeroAlbum album = albumesPorCancion.ContainsKey(ruta) ? albumesPorCancion[ruta] : null;

            cancionActual = new DecoradorCancionBasica(ruta, artista, album);

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("---------------------------------------------");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($" Reproduciendo: {Path.GetFileName(ruta)}");
            Console.ResetColor();

            if (artista != null)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($" Artista: {artista.Nombre}");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($" Artista: Desconocido");
            }

            if (album != null)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine($" Álbum: {album.Titulo} ({album.Año})");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine($" Álbum: Desconocido");
            }

            Console.ResetColor();
            Console.WriteLine("---------------------------------------\n");

            string efectos = ObtenerEfectosActivos();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"Efectos: {efectos}");
            Console.ResetColor();

            if (reproduciendo)
                ReproducirCancion();

            Thread.Sleep(2000);
        }



        static void Siguiente()
        {
            indiceActual = (indiceActual + 1) % archivos.Length;
            CambiarCancion();
        }

        static void Anterior()
        {
            indiceActual = (indiceActual - 1 + archivos.Length) % archivos.Length;
            CambiarCancion();
        }

        static void Detener()
        {
            player.Stop();
            reproduciendo = false;
        }

        static string ObtenerEfectosActivos()
        {
            var efectos = new List<string>();
            DecoradorComponenteCancion temp = cancionActual;

            while (temp is DecoradorEfecto decorador)
            {
                efectos.Add(decorador.NombreEfecto);
                var campo = typeof(DecoradorEfecto).GetField("_componente",
                    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                temp = (DecoradorComponenteCancion)campo.GetValue(decorador);
            }

            if (efectos.Count == 0)
                return "Ninguno";

            efectos.Reverse();
            return string.Join(", ", efectos);
        }
    }
}