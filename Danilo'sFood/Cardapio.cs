using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Danilo_sFood
{
    public class Cardapio
    {
        private List<IProduto> produtos;
        private int ultimoCodigoConsultado;

        public Cardapio()
        {
            produtos = new List<IProduto>
        {
            new Comida("Moqueca de Palmito", 32, GetProximoCodigo()),
            new Comida("Falafel Assado", 20, GetProximoCodigo()),
            new Comida("Salada Primavera com Macarrão Konjac", 25, GetProximoCodigo()),
            new Comida("Escondidinho de Inhame", 18, GetProximoCodigo()),
            new Comida("Strogonoff de Cogumelos", 35, GetProximoCodigo()),
            new Comida("Caçarola de legumes", 22, GetProximoCodigo()),
            new Bebida("Água", 3, GetProximoCodigo()),
            new Bebida("Copo de suco", 7, GetProximoCodigo()),
            new Bebida("Refrigerante orgânico", 7, GetProximoCodigo()),
            new Bebida("Cerveja vegana", 9, GetProximoCodigo()),
            new Bebida("Taça de vinho vegano", 18, GetProximoCodigo())
        };
        }

        public List<IProduto> ListarProdutos()
        {
            return produtos;
        }

        public IProduto BuscarProdutoPorCodigo(int codigo)
        {
            return produtos.FirstOrDefault(p => p.GetCodigo() == codigo);
        }

        private int GetProximoCodigo()
        {
            return ++ultimoCodigoConsultado;
        }
    }
}