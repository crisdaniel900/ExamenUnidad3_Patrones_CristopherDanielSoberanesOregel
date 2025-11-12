using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReproductorMusicaDecorador
{
    public class DecoradorEfectoConcierto : DecoradorEfecto
    {
        public DecoradorEfectoConcierto(DecoradorComponenteCancion componente)
            : base(componente, PesoLigeroFabricaTiposEfecto.ObtenerTipo("Concierto")) { }

        public override string Descripcion => _componente.Descripcion + $"\n\t- {_tipoEfecto.Nombre}";
    }
}
