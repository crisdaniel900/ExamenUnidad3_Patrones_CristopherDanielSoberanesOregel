using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReproductorMusicaDecorador
{
    public class DecoradorCancionBasica : DecoradorComponenteCancion
    {
        private string _ruta;
        private string _nombre;
        private PesoLigeroArtista _artista;
        private PesoLigeroAlbum _album;

        public DecoradorCancionBasica(string ruta, PesoLigeroArtista artista = null, PesoLigeroAlbum album = null)
        {
            _ruta = ruta;
            _nombre = Path.GetFileName(ruta);
            _artista = artista;
            _album = album;
        }

        public override string Descripcion
        {
            get
            {
                string desc = $" {_nombre}";
                if (_artista != null)
                    desc += $"\n\t Artista: {_artista.Nombre}";
                if (_album != null)
                    desc += $"\n\t Álbum: {_album.Titulo} ({_album.Año})";
                desc += "\n\t- Reproducción Normal";
                return desc;
            }
        }

        public override string RutaArchivo => _ruta;
        public override PesoLigeroArtista Artista => _artista;
        public override PesoLigeroAlbum Album => _album;
    }
}
