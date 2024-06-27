using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Danilo_sFood
{
    public class Comida : Produto
    {
        public Comida(string nome, double preco, int codigo) : base(nome, preco, codigo)
        {
        }
    }
}
