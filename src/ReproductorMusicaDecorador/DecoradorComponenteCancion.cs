using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReproductorMusicaDecorador
{
    public abstract class DecoradorComponenteCancion
    {
        public abstract string Descripcion { get; }
        public abstract string RutaArchivo { get; }
        public abstract PesoLigeroArtista Artista { get; }
        public abstract PesoLigeroAlbum Album { get; }
    }
}
