using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReproductorMusicaDecorador
{
    public abstract class DecoradorEfecto : DecoradorComponenteCancion
    {
        protected DecoradorComponenteCancion _componente;
        protected PesoLigeroTipoEfecto _tipoEfecto; 

        public DecoradorEfecto(DecoradorComponenteCancion componente, PesoLigeroTipoEfecto tipoEfecto)
        {
            _componente = componente;
            _tipoEfecto = tipoEfecto;
        }

        public override string RutaArchivo => _componente.RutaArchivo;
        public override PesoLigeroArtista Artista => _componente.Artista;
        public override PesoLigeroAlbum Album => _componente.Album;
        public string NombreEfecto => _tipoEfecto.Nombre;
    }
}
