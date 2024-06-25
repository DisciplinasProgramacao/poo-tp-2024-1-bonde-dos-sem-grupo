using System;

class Program
{
    static void Main(string[] args)
    {
        // Código principal que gerencia a interação com o usuário.
        // Cria instâncias e chama métodos para realizar operações com o restaurante.

        // Criando instância do restaurante
        Restaurante restaurante = new Restaurante();
        // Adicionando mesas conforme especificado
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
        Requisicao requisicao = null; // Variável para guardar a requisição atual

        while (continuar)
        {
            Console.WriteLine("\nEscolha uma opção:");
            Console.WriteLine("1. Digitar CPF e criar requisição");
            Console.WriteLine("2. Mostrar cardápio restaurante");
            Console.WriteLine("3. Selecionar produto e adicionar à requisição restaurante");
            Console.WriteLine("4. Fechar conta e mostrar detalhes restaurante");
            Console.WriteLine("5. Mostrar cardápio café OOCV");
            Console.WriteLine("6. Selecionar produto e fazer pedido no café OOCV");
            Console.WriteLine("7. Sair");

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
                        Console.WriteLine($"CPF encontrado na base de dados. Bem-vindo de volta, {clienteExistente.GetNome()}!");
                    }
                    else
                    {
                        Console.Write("CPF não encontrado na base de dados. Por favor, digite seu nome: ");
                        string nomeCliente = Console.ReadLine();
                        clienteExistente = restaurante.CadastrarNovoCliente(nomeCliente, cpf);
                        Console.WriteLine($"Cliente {clienteExistente.GetNome()} cadastrado com sucesso!");
                    }

                    // Cria uma requisição para o cliente
                    Console.Write("Digite o número de pessoas para a requisição: ");
                    int numPessoas = int.Parse(Console.ReadLine());
                    requisicao = restaurante.CriarRequisicao(clienteExistente, numPessoas);
                    break;

                case "2":
                    MostrarCardapioRestaurante(restaurante.GetCardapio());
                    break;

                case "3":
                    if (requisicao == null)
                    {
                        Console.WriteLine("Crie uma requisição antes de selecionar produtos.");
                        break;
                    }

                    MostrarCardapioRestaurante(restaurante.GetCardapio());
                    Console.Write("Digite o código do produto: ");
                    int codigoProdutoRestaurante = int.Parse(Console.ReadLine());

                    IProduto produtoSelecionadoRestaurante = SelecionarProduto(restaurante.GetCardapio(), codigoProdutoRestaurante);
                    if (produtoSelecionadoRestaurante != null)
                    {
                        requisicao.AdicionarProduto(produtoSelecionadoRestaurante);
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
                    Console.WriteLine(resultadoConta);
                    requisicao = null; // Limpa a requisição atual após fechar a conta
                    break;

                case "5":
                    MostrarCardapioCafe(restaurante.GetCardapio());
                    break;

                case "6":
                    MostrarCardapioCafe(restaurante.GetCardapio());
                    Console.Write("Digite o código do produto: ");
                    int codigoProdutoCafe = int.Parse(Console.ReadLine());

                    IProduto produtoSelecionadoCafe = SelecionarProdutoCafe(restaurante.GetCardapio(), codigoProdutoCafe);
                    if (produtoSelecionadoCafe != null)
                    {
                        Console.WriteLine($"Pedido realizado no café OOCV: {produtoSelecionadoCafe.GetNome()}.");
                    }
                    else
                    {
                        Console.WriteLine("Código de produto inválido.");
                    }
                    break;

                case "7":
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

    private static void MostrarCardapioRestaurante(Cardapio cardapio)
    {
        Console.WriteLine("\nCardápio Restaurante:");

        var produtos = cardapio.ListarProdutos().Where(p => p.GetType() != typeof(CafeComida) && p.GetType() != typeof(CafeBebida)).ToList();
        for (int i = 0; i < produtos.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {produtos[i].GetNome()} - R$ {produtos[i].GetPreco()}");
        }
    }

    private static void MostrarCardapioCafe(Cardapio cardapio)
    {
        Console.WriteLine("\nCardápio Café OOCV:");

        var produtosCafe = cardapio.ListarProdutos().Where(p => p.GetType() == typeof(CafeComida) || p.GetType() == typeof(CafeBebida)).ToList();
        for (int i = 0; i < produtosCafe.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {produtosCafe[i].GetNome()} - R$ {produtosCafe[i].GetPreco()}");
        }
    }

    private static IProduto SelecionarProduto(Cardapio cardapio, int codigo)
    {
        var produtos = cardapio.ListarProdutos().Where(p => !(p.GetType() == typeof(CafeComida) || p.GetType() == typeof(CafeBebida))).ToList();
        if (codigo >= 1 && codigo <= produtos.Count)
        {
            return produtos[codigo - 1];
        }
        else
        {
            return null;
        }
    }

    private static IProduto SelecionarProdutoCafe(Cardapio cardapio, int codigo)
    {
        var produtosCafe = cardapio.ListarProdutos().Where(p => p.GetType() == typeof(CafeComida) || p.GetType() == typeof(CafeBebida)).ToList();
        if (codigo >= 1 && codigo <= produtosCafe.Count)
        {
            return produtosCafe[codigo - 1];
        }
        else
        {
            return null;
        }
    }
}
