using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReproductorMusicaDecorador
{
    public class DecoradorEfectoVelocidadX2 : DecoradorEfecto
    {
        public DecoradorEfectoVelocidadX2(DecoradorComponenteCancion componente)
            : base(componente, PesoLigeroFabricaTiposEfecto.ObtenerTipo("VelocidadX2")) { }

        public override string Descripcion => _componente.Descripcion + $"\n\t- {_tipoEfecto.Nombre}";
    }
}
