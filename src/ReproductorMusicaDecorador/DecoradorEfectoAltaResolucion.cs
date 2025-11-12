using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReproductorMusicaDecorador
{
    public class DecoradorEfectoAltaResolucion : DecoradorEfecto
    {
        public DecoradorEfectoAltaResolucion(DecoradorComponenteCancion componente)
            : base(componente, PesoLigeroFabricaTiposEfecto.ObtenerTipo("AltaResolucion")) { }

        public override string Descripcion => _componente.Descripcion + $"\n\t-  {_tipoEfecto.Nombre}";
    }
}
