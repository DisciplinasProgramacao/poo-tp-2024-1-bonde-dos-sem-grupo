using System;

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
