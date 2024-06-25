# Código do Projeto

Mantenha neste diretório todo o código fonte do projeto. 

Se necessário, descreva neste arquivo aspectos relevantes da estrutura de diretórios criada para organização do código.

using system;

class Program
{
    static void Main(string[] args)
    {
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

public class Restaurante
{
    private List<Cliente> clientes;
    private List<Mesa> mesas;
    private List<Requisicao> requisicoesEmAndamento; // Lista de requisições em andamento
    private List<Requisicao> requisicoesEmEspera; // Lista de requisições em espera
    private Cardapio cardapio;

    public Restaurante()
    {
        clientes = new List<Cliente>();
        mesas = new List<Mesa>();
        requisicoesEmAndamento = new List<Requisicao>(); // Inicialização da lista de requisições em andamento
        requisicoesEmEspera = new List<Requisicao>(); // Inicialização da lista de requisições em espera
        cardapio = new Cardapio(); // Inicialização do Cardápio
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
        Console.WriteLine($"Mesa {mesa.GetIdMesa()} com {mesa.GetLugaresMesa()} lugares adicionada.");
    }

    public Requisicao CriarRequisicao(Cliente cliente, int numPessoas)
    {
        var requisicao = new Requisicao(cliente, numPessoas);
        Console.WriteLine($"Requisição criada para o cliente {cliente.GetNome()}.");
        string resultadoMesa = DefinirMesa(requisicao); // Aloca uma mesa para a nova requisição
        Console.WriteLine(resultadoMesa);

        // Adiciona a requisição à lista de requisições em andamento
        requisicoesEmAndamento.Add(requisicao);

        return requisicao;
    }

    public string DefinirMesa(Requisicao requisicao)
    {
        // Ordena as mesas por capacidade em ordem crescente
        var mesasDisponiveis = mesas.Where(m => m.VerificaDisponibilidade())
                                     .OrderBy(m => m.GetLugaresMesa())
                                     .ToList();

        // Busca a primeira mesa com capacidade maior ou igual ao número de pessoas na requisição
        var mesaEscolhida = mesasDisponiveis.FirstOrDefault(m => m.GetLugaresMesa() >= requisicao.GetNumClientes());

        if (mesaEscolhida != null)
        {
            mesaEscolhida.SetStatus(false);
            requisicao.SetMesa(mesaEscolhida);
            Console.WriteLine($"Mesa {mesaEscolhida.GetIdMesa()} definida para a requisição de {requisicao.GetCliente().GetNome()}.");
            return $"Mesa {mesaEscolhida.GetIdMesa()} definida para a requisição de {requisicao.GetCliente().GetNome()}.";
        }
        else
        {
            AdicionarNaListaEspera(requisicao);
            int posicao = requisicoesEmEspera.IndexOf(requisicao) + 1;
            Console.WriteLine($"Mesa não disponível. Requisição adicionada à lista de espera na posição {posicao}.");
            return $"Mesa não disponível. Requisição adicionada à lista de espera na posição {posicao}.";
        }
    }

    public string LiberarMesa(Requisicao requisicao)
    {
        if (requisicao.GetMesa() != null)
        {
            requisicao.GetMesa().SetStatus(true);
            Console.WriteLine($"Mesa {requisicao.GetMesa().GetIdMesa()} liberada.");
            return $"Mesa {requisicao.GetMesa().GetIdMesa()} liberada.";
        }
        else
        {
            Console.WriteLine("Mesa não encontrada.");
            return "Mesa não encontrada.";
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
            Console.WriteLine($"Requisição de {requisicaoMaisAntiga.GetNumClientes()} clientes atendida.");
        }
        else
        {
            Console.WriteLine("Não há requisições na lista de espera.");
        }
    }

    public string FecharConta(int numeroMesa)
    {
        // Busca a requisição associada à mesa informada na lista de requisições em andamento
        var requisicaoParaFechar = requisicoesEmAndamento.FirstOrDefault(r => r.GetMesa().GetIdMesa() == numeroMesa);

        if (requisicaoParaFechar != null)
        {
            requisicaoParaFechar.MostrarConta();
            // Adiciona a requisição à lista de requisições encerradas
            requisicoesEmEspera.Add(requisicaoParaFechar);
            LiberarMesa(requisicaoParaFechar); // Liberar a mesa associada à requisição
                                               // Remove a requisição da lista de requisições em andamento
            requisicoesEmAndamento.Remove(requisicaoParaFechar);
            return "Conta fechada com sucesso.";
        }
        else
        {
            return "Não foi encontrada nenhuma requisição em andamento para a mesa informada.";
        }
    }

    public Cardapio GetCardapio()
    {
        return cardapio;
    }
}

public class Mesa
{
    private int idMesa;
    private int lugaresMesa;
    private bool status;

    public Mesa(int idMesa, int lugaresMesa, bool status)
    {
        this.idMesa = idMesa;
        this.lugaresMesa = lugaresMesa;
        this.status = status;
    }

    public int GetIdMesa()
    {
        return idMesa;
    }

    public int GetLugaresMesa()
    {
        return lugaresMesa;
    }

    public bool GetStatus()
    {
        return status;
    }

    public void SetStatus(bool status)
    {
        this.status = status;
    }

    public bool VerificaDisponibilidade()
    {
        return status;
    }
}

public class Cliente
{
    private string nome;
    private string cpf;

    public Cliente(string nome, string cpf)
    {
        this.nome = nome;
        this.cpf = cpf;
    }

    public string GetNome()
    {
        return nome;
    }

    public string GetCpf()
    {
        return cpf;
    }
}

public class Requisicao
{
    private int id;
    private Cliente cliente;
    private int numClientes;
    private List<IProduto> produtos;
    private Mesa mesa;
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

    public void AdicionarProduto(IProduto produto)
    {
        produtos.Add(produto);
        Console.WriteLine($"Produto {produto.GetNome()} adicionado à requisição.");
    }

    public void EncerrarRequisicao()
    {
        Console.WriteLine($"Requisição {id} encerrada.");
    }

    public void MostrarConta()
    {
        Console.WriteLine("\nConta:");

        foreach (var produto in produtos)
        {
            Console.WriteLine($"{produto.GetNome()} - R$ {produto.GetPreco()}");
        }

        double total = produtos.Sum(p => p.GetPreco());
        Console.WriteLine($"\nTotal: R$ {total}");
    }
}

public class Cardapio
{
    private List<IProduto> produtos;
    private int ultimoCodigoConsultado;

    public Cardapio()
    {
        produtos = new List<IProduto>
    {
        // Produtos do restaurante original
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
        new Bebida("Taça de vinho vegano", 18, GetProximoCodigo()),

        // Produtos do café OOCV
        new CafeComida("Não de queijo", 5, GetProximoCodigo()),
        new CafeComida("Bolinha de cogumelo", 7, GetProximoCodigo()),
        new CafeComida("Rissole de palmito", 7, GetProximoCodigo()),
        new CafeComida("Coxinha de carne de jaca", 8, GetProximoCodigo()),
        new CafeComida("Fatia de queijo de caju", 9, GetProximoCodigo()),
        new CafeComida("Biscoito amanteigado", 3, GetProximoCodigo()),
        new CafeComida("Cheesecake de frutas vermelhas", 15, GetProximoCodigo()),
        new CafeBebida("Água", 3, GetProximoCodigo()),
        new CafeBebida("Copo de suco", 7, GetProximoCodigo()),
        new CafeBebida("Café espresso orgânico", 6, GetProximoCodigo())
    };
    }

    public List<IProduto> ListarProdutos()
    {
        return produtos;
    }

    private int GetProximoCodigo()
    {
        ultimoCodigoConsultado++;
        return ultimoCodigoConsultado;
    }
}

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

public interface IProduto
{
    string GetNome();
    double GetPreco();
    int GetCodigo();
}

public class CafeBebida : Produto
{
    public CafeBebida(string nome, double preco, int codigo) : base(nome, preco, codigo)
    {
    }
}

public class CafeComida : Produto
{
    public CafeComida(string nome, double preco, int codigo) : base(nome, preco, codigo)
    {
    }
}

public class Bebida : Produto
{
    public Bebida(string nome, double preco, int codigo) : base(nome, preco, codigo)
    {
    }
}

public class Comida : Produto
{
    public Comida(string nome, double preco, int codigo) : base(nome, preco, codigo)
    {
    }
}
