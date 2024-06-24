using System;

public class Requisicao
{
    public int Id { get; private set; }
    public Cliente Cliente { get; private set; }
    public int NumClientes { get; private set; }
    public Mesa Mesa { get; set; }
    public DateTime DataReq { get; private set; }
    public DateTime HoraEntrada { get; private set; }
    public DateTime HoraSaida { get; private set; }
    private List<IProduto> produtosConsumidos;

    public Requisicao(Cliente cliente, int numClientes)
    {
        Cliente = cliente;
        NumClientes = numClientes;
        DataReq = DateTime.Now;
        HoraEntrada = DateTime.Now;
        produtosConsumidos = new List<IProduto>();
    }

    public void AdicionarProduto(IProduto produto)
    {
        produtosConsumidos.Add(produto);
        Console.WriteLine($"Produto {produto.Nome} adicionado à requisição.");
    }

    public void EncerrarRequisicao()
    {
        HoraSaida = DateTime.Now;
        Console.WriteLine($"Requisição encerrada.");
    }

    public void MostrarConta()
    {
        decimal total = 0;
        Console.WriteLine($"Conta da Requisição:");

        foreach (var produto in produtosConsumidos)
        {
            Console.WriteLine($"{produto.Nome} - R$ {produto.Preco}");
            total += (decimal)produto.Preco; // Conversão explícita para decimal
        }

        Console.WriteLine($"Total: R$ {total}");
    }
}
