using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReproductorMusicaDecorador
{
    public class PesoLigeroTipoEfecto
    {
        public string Nombre { get; private set; }
        
        public string Descripcion { get; private set; }

        public PesoLigeroTipoEfecto(string nombre, string icono, string descripcion)
        {
            Nombre = nombre;
            
            Descripcion = descripcion;
        }

        public void Mostrar()
        {
            Console.WriteLine($" {Nombre}: {Descripcion}");
        }
    }
}
