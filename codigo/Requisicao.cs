using System;

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
