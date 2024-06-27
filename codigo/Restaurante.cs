using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Danilo_sFood
{
    public class Restaurante
    {
        private List<Cliente> clientes;
        private List<Mesa> mesas;
        private List<Requisicao> requisicoesEmAndamento;
        private List<Requisicao> requisicoesEmEspera;
        private Cardapio cardapio;

        public Restaurante()
        {
            clientes = new List<Cliente>();
            mesas = new List<Mesa>();
            requisicoesEmAndamento = new List<Requisicao>();
            requisicoesEmEspera = new List<Requisicao>();
            cardapio = new Cardapio();
        }

        public Cliente BuscarClientePorCpf(string cpf)
        {
            return clientes.Find(c => c.GetCpf() == cpf);
        }

        public Cliente CadastrarNovoCliente(string nome, string cpf)
        {
            Cliente novoCliente = new Cliente(nome, cpf);
            clientes.Add(novoCliente);
            return novoCliente;
        }

        public void AdicionarMesa(Mesa mesa)
        {
            mesas.Add(mesa);
        }

        public Requisicao CriarRequisicao(Cliente cliente, int numPessoas)
        {
            var requisicao = new Requisicao(cliente, numPessoas);
            var resultadoMesa = DefinirMesa(requisicao);
            requisicao.SetPosicaoListaEspera(resultadoMesa.Item2);
            requisicoesEmAndamento.Add(requisicao);
            return requisicao;
        }

        public Tuple<Mesa, int> DefinirMesa(Requisicao requisicao)
        {
            var mesasDisponiveis = mesas.Where(m => m.VerificaDisponibilidade())
                                         .OrderBy(m => m.GetLugaresMesa())
                                         .ToList();

            var mesaEscolhida = mesasDisponiveis.FirstOrDefault(m => m.GetLugaresMesa() >= requisicao.GetNumClientes());

            if (mesaEscolhida != null)
            {
                mesaEscolhida.SetStatus(false);
                requisicao.SetMesa(mesaEscolhida);
                return Tuple.Create(mesaEscolhida, -1);
            }
            else
            {
                AdicionarNaListaEspera(requisicao);
                int posicao = requisicoesEmEspera.IndexOf(requisicao) + 1;
                return Tuple.Create<Mesa, int>(null, posicao);
            }
        }

        public void LiberarMesa(Requisicao requisicao)
        {
            if (requisicao.GetMesa() != null)
            {
                requisicao.GetMesa().SetStatus(true);
            }
        }

        public void AdicionarNaListaEspera(Requisicao requisicao)
        {
            requisicoesEmEspera.Add(requisicao);
        }

        public void AtenderRequisicao()
        {
            if (requisicoesEmEspera.Count > 0)
            {
                var requisicaoMaisAntiga = requisicoesEmEspera[0];
                requisicoesEmEspera.RemoveAt(0);
                DefinirMesa(requisicaoMaisAntiga);
            }
        }

        public string FecharConta(int numeroMesa)
        {
            var requisicaoParaFechar = requisicoesEmAndamento.FirstOrDefault(r => r.GetMesa().GetIdMesa() == numeroMesa);

            if (requisicaoParaFechar != null)
            {
                string conta = requisicaoParaFechar.MostrarConta();
                requisicoesEmEspera.Add(requisicaoParaFechar);
                LiberarMesa(requisicaoParaFechar);
                requisicoesEmAndamento.Remove(requisicaoParaFechar);
                return conta;
            }
            else
            {
                return null;
            }
        }

        public Cardapio GetCardapio()
        {
            return cardapio;
        }
    }




}
