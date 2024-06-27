using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Danilo_sFood
{
    public class Requisicao
    {
        private int id;
        private Cliente cliente;
        private int numClientes;
        private List<IProduto> produtos;
        private Mesa mesa;
        private int posicaoListaEspera;
        private static int idCounter = 1;

        public Requisicao(Cliente cliente, int numClientes)
        {
            this.id = idCounter++;
            this.cliente = cliente;
            this.numClientes = numClientes;
            produtos = new List<IProduto>();
        }

        public int GetNumClientes()
        {
            return numClientes;
        }

        public Cliente GetCliente()
        {
            return cliente;
        }

        public Mesa GetMesa()
        {
            return mesa;
        }

        public void SetMesa(Mesa mesa)
        {
            this.mesa = mesa;
        }

        public int GetPosicaoListaEspera()
        {
            return posicaoListaEspera;
        }

        public void SetPosicaoListaEspera(int posicao)
        {
            this.posicaoListaEspera = posicao;
        }

        public void AdicionarProduto(IProduto produto)
        {
            produtos.Add(produto);
        }

        public double CalcularTotal()
        {
            return produtos.Sum(p => p.GetPreco());
        }

        public string MostrarConta()
        {
            StringBuilder conta = new StringBuilder();

            foreach (var produto in produtos)
            {
                conta.AppendLine($"{produto.GetNome()} - R$ {produto.GetPreco()}");
            }

            conta.AppendLine($"Total: R$ {CalcularTotal()}");

            return conta.ToString();
        }
    }

}
