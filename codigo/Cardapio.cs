using System.Collections.Generic;

// Classe para representar o cardápio do café
public class Cardapio
{
    private List<IProduto> produtos;

    public Cardapio()
    {
        produtos = new List<IProduto>();
    }

    // Método para adicionar produto ao cardápio
    public void AdicionarProduto(IProduto produto)
    {
        produtos.Add(produto);
    }

    // Método para listar todos os produtos do cardápio
    public List<IProduto> ListarProdutos()
    {
        return produtos;
    }

    // Método para buscar um produto pelo nome
    public IProduto BuscarProduto(string nome)
    {
        return produtos.Find(p => p.Nome == nome);
    }
}
