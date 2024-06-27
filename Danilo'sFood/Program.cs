using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Danilo_sFood
{
    class Program
    {
        static void Main(string[] args)
        {
            Restaurante restaurante = new Restaurante();

            restaurante.AdicionarMesa(new Mesa(1, 4, true));
            restaurante.AdicionarMesa(new Mesa(2, 4, true));
            restaurante.AdicionarMesa(new Mesa(3, 4, true));
            restaurante.AdicionarMesa(new Mesa(4, 4, true));
            restaurante.AdicionarMesa(new Mesa(5, 6, true));
            restaurante.AdicionarMesa(new Mesa(6, 6, true));
            restaurante.AdicionarMesa(new Mesa(7, 6, true));
            restaurante.AdicionarMesa(new Mesa(8, 6, true));
            restaurante.AdicionarMesa(new Mesa(9, 8, true));
            restaurante.AdicionarMesa(new Mesa(10, 8, true));

            Console.WriteLine("Bem-vindo ao OO Comidinhas Veganas!");

            bool continuar = true;
            Requisicao requisicao = null;

            while (continuar)
            {
                Console.WriteLine("\nEscolha uma opção:");
                Console.WriteLine("1. Digitar CPF e criar requisição");
                Console.WriteLine("2. Mostrar cardápio");
                Console.WriteLine("3. Selecionar produto e adicionar à requisição");
                Console.WriteLine("4. Fechar conta e mostrar detalhes");
                Console.WriteLine("5. Sair");

                Console.Write("Opção: ");
                string opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        string cpf;
                        do
                        {
                            Console.Write("Digite seu CPF (apenas números): ");
                            cpf = Console.ReadLine();
                        } while (!ValidaCpf(cpf));

                        Cliente clienteExistente = restaurante.BuscarClientePorCpf(cpf);
                        if (clienteExistente != null)
                        {
                            Console.WriteLine($"CPF encontrado na base de dados. Bem-vindo de volta, {clienteExistente.GetNome()}!");
                        }
                        else
                        {
                            Console.Write("CPF não encontrado na base de dados. Por favor, digite seu nome: ");
                            string nomeCliente = Console.ReadLine();
                            clienteExistente = restaurante.CadastrarNovoCliente(nomeCliente, cpf);
                            Console.WriteLine($"Cliente {clienteExistente.GetNome()} cadastrado com sucesso!");
                        }

                        Console.Write("Digite o número de pessoas para a requisição: ");
                        int numPessoas = int.Parse(Console.ReadLine());
                        requisicao = restaurante.CriarRequisicao(clienteExistente, numPessoas);
                        Console.WriteLine($"Mesa {requisicao.GetMesa()?.GetIdMesa()} definida para a requisição de {requisicao.GetCliente().GetNome()}." +
                                          $" Posição na lista de espera: {requisicao.GetPosicaoListaEspera()}");
                        break;

                    case "2":
                        MostrarCardapio(restaurante.GetCardapio());
                        break;

                    case "3":
                        if (requisicao == null)
                        {
                            Console.WriteLine("Crie uma requisição antes de selecionar produtos.");
                            break;
                        }

                        MostrarCardapio(restaurante.GetCardapio());
                        Console.Write("Digite o código do produto: ");
                        int codigoProduto = int.Parse(Console.ReadLine());

                        IProduto produtoSelecionado = SelecionarProduto(restaurante.GetCardapio(), codigoProduto);
                        if (produtoSelecionado != null)
                        {
                            requisicao.AdicionarProduto(produtoSelecionado);
                            Console.WriteLine($"Produto {produtoSelecionado.GetNome()} adicionado à requisição.");
                        }
                        else
                        {
                            Console.WriteLine("Código de produto inválido.");
                        }
                        break;

                    case "4":
                        if (requisicao == null)
                        {
                            Console.WriteLine("Crie uma requisição antes de fechar a conta.");
                            break;
                        }

                        Console.Write("Digite o número da mesa: ");
                        int numeroMesa = int.Parse(Console.ReadLine());

                        string resultadoConta = restaurante.FecharConta(numeroMesa);
                        Console.WriteLine($"Conta fechada com sucesso.\n{resultadoConta}");
                        requisicao = null;
                        break;

                    case "5":
                        Console.WriteLine("Saindo...");
                        continuar = false;
                        break;

                    default:
                        Console.WriteLine("Opção inválida.");
                        break;
                }
            }
        }

        private static bool ValidaCpf(string cpf)
        {
            if (cpf.Length != 11 || !cpf.All(char.IsDigit))
            {
                Console.WriteLine("CPF inválido. O CPF deve conter exatamente 11 números.");
                return false;
            }
            return true;
        }

        private static void MostrarCardapio(Cardapio cardapio)
        {
            Console.WriteLine("\nCardápio:");
            var produtos = cardapio.ListarProdutos();
            for (int i = 0; i < produtos.Count; i++)
            {
                Console.WriteLine($"{produtos[i].GetCodigo()}. {produtos[i].GetNome()} - R$ {produtos[i].GetPreco()}");
            }
        }

        private static IProduto SelecionarProduto(Cardapio cardapio, int codigo)
        {
            return cardapio.BuscarProdutoPorCodigo(codigo);
        }
    }
}
