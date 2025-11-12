using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReproductorMusicaDecorador
{
    public class PesoLigeroAlbum
    {
        public string Titulo { get; private set; }
        public string Año { get; private set; }

        public PesoLigeroAlbum(string titulo, string año)
        {
            Titulo = titulo;
            Año = año;
        }

        public override string ToString() => $"{Titulo} ({Año})";
    }
}
