using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Danilo_sFood
{
    public abstract class Produto : IProduto
    {
        private string nome;
        private double preco;
        private int codigo;

        protected Produto(string nome, double preco, int codigo)
        {
            this.nome = nome;
            this.preco = preco;
            this.codigo = codigo;
        }

        public string GetNome()
        {
            return nome;
        }

        public double GetPreco()
        {
            return preco;
        }

        public int GetCodigo()
        {
            return codigo;
        }
    }
}
