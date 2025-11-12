using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReproductorMusicaDecorador
{
    public class PesoLigeroArtista
    {
        public string Nombre { get; private set; }

        public PesoLigeroArtista(string nombre)
        {
            Nombre = nombre;
        }


        public override string ToString() => Nombre;
    }
}
