using System;

class Program
{
    static void Main(string[] args)
    {
        // Criando instância do restaurante
        Restaurante restaurante = new Restaurante();

        Console.WriteLine("Bem-vindo ao OO Comidinhas Veganas!");

        bool continuar = true;
        Requisicao requisicao = null; // Variável para guardar a requisição atual

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

                    // Verifica se o CPF já existe na base de dados de clientes
                    Cliente clienteExistente = restaurante.BuscarClientePorCpf(cpf);
                    if (clienteExistente != null)
                    {
                        Console.WriteLine($"CPF encontrado na base de dados. Bem-vindo de volta, {clienteExistente.Nome}!");
                    }
                    else
                    {
                        Console.Write("CPF não encontrado na base de dados. Por favor, digite seu nome: ");
                        string nomeCliente = Console.ReadLine();
                        clienteExistente = restaurante.CadastrarNovoCliente(nomeCliente, cpf);
                        Console.WriteLine($"Cliente {clienteExistente.Nome} cadastrado com sucesso!");
                    }

                    // Cria uma requisição para o cliente
                    Console.Write("Digite o número de pessoas para a requisição: ");
                    int numPessoas = int.Parse(Console.ReadLine());
                    requisicao = restaurante.CriarRequisicao(clienteExistente, numPessoas);
                    break;

                case "2":
                    MostrarCardapio(restaurante.Cardapio);
                    break;

                case "3":
                    if (requisicao == null)
                    {
                        Console.WriteLine("Crie uma requisição antes de selecionar produtos.");
                        break;
                    }

                    MostrarCardapio(restaurante.Cardapio);
                    Console.Write("Digite o código do produto: ");
                    int codigoProduto = int.Parse(Console.ReadLine());

                    IProduto produtoSelecionado = SelecionarProduto(restaurante.Cardapio, codigoProduto);
                    if (produtoSelecionado != null)
                    {
                        requisicao.AdicionarProduto(produtoSelecionado);
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

                    requisicao.EncerrarRequisicao();
                    requisicao.MostrarConta();
                    restaurante.LiberarMesa(requisicao);
                    requisicao = null; // Limpa a requisição atual após fechar a conta
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
        // Verifica se o CPF possui 11 dígitos numéricos
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

        // Mostrar comidas
        Console.WriteLine("\nComidas:");
        var comidas = cardapio.ListarComidas();
        for (int i = 0; i < comidas.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {comidas[i].Nome} - R$ {comidas[i].Preco}");
        }

        // Mostrar bebidas
        Console.WriteLine("\nBebidas:");
        var bebidas = cardapio.ListarBebidas();
        for (int i = 0; i < bebidas.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {bebidas[i].Nome} - R$ {bebidas[i].Preco}");
        }
    }

    private static IProduto SelecionarProduto(Cardapio cardapio, int codigo)
    {
        var produtos = cardapio.ListarProdutos();
        if (codigo >= 1 && codigo <= produtos.Count)
        {
            return produtos[codigo - 1];
        }
        else
        {
            return null;
        }
    }
}
