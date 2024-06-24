using System;
using System.Collections.Generic;

public class Restaurante
{
    public List<Cliente> Clientes { get; private set; }
    public List<Mesa> Mesas { get; private set; }
    private List<Requisicao> filaEspera;
    public Cardapio Cardapio { get; private set; }

    public Restaurante()
    {
        Clientes = new List<Cliente>();
        Mesas = new List<Mesa>();
        filaEspera = new List<Requisicao>();
        Cardapio = new Cardapio(); // Inicialização do Cardápio
    }

    public Cliente BuscarClientePorCpf(string cpf)
    {
        return Clientes.Find(c => c.CPF == cpf);
    }

    public Cliente CadastrarNovoCliente(string nome, string cpf)
    {
        Cliente novoCliente = new Cliente(nome, cpf);
        Clientes.Add(novoCliente);
        return novoCliente;
    }

    public void AdicionarMesa(Mesa mesa)
    {
        Mesas.Add(mesa);
        Console.WriteLine($"Mesa {mesa.IdMesa} com {mesa.LugaresMesa} lugares adicionada.");
    }

    public void CadastrarCliente(Cliente cliente)
    {
        Console.WriteLine($"Cliente {cliente.Nome} cadastrado.");
    }

    public Requisicao CriarRequisicao(Cliente cliente, int numPessoas)
    {
        var requisicao = new Requisicao(cliente, numPessoas);
        Console.WriteLine($"Requisição criada para o cliente {cliente.Nome}.");
        return requisicao;
    }

    public string DefinirMesa(Requisicao requisicao)
    {
        foreach (var mesa in Mesas)
        {
            if (mesa.VerificaDisponibilidade())
            {
                mesa.Status = false;
                requisicao.Mesa = mesa;
                Console.WriteLine($"Mesa {mesa.IdMesa} definida para a requisição de {requisicao.Cliente.Nome}.");
                return $"Mesa {mesa.IdMesa} definida para a requisição de {requisicao.Cliente.Nome}.";
            }
        }
        Console.WriteLine("Mesa não disponível.");
        return "Mesa não disponível.";
    }

    public string LiberarMesa(Requisicao requisicao)
    {
        if (requisicao.Mesa != null)
        {
            requisicao.Mesa.Status = true;
            Console.WriteLine($"Mesa {requisicao.Mesa.IdMesa} liberada.");
            return $"Mesa {requisicao.Mesa.IdMesa} liberada.";
        }
        else
        {
            Console.WriteLine("Mesa não encontrada.");
            return "Mesa não encontrada.";
        }
    }

    public void AdicionarNaListaEspera(Requisicao requisicao)
    {
        filaEspera.Add(requisicao);
    }

    public void AtenderRequisicao()
    {
        if (filaEspera.Count > 0)
        {
            var requisicaoMaisAntiga = filaEspera[0];
            filaEspera.RemoveAt(0);
            Console.WriteLine($"Requisição de {requisicaoMaisAntiga.NumClientes} clientes atendida.");
        }
        else
        {
            Console.WriteLine("Não há requisições na lista de espera.");
        }
    }
}
