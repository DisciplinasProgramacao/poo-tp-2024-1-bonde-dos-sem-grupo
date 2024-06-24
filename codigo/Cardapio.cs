using System.Collections.Generic;

// Classe para representar o cardápio
public class Cardapio
{
    private List<IProduto> produtos;
    private int ultimoCodigoConsultado; // Atributo para armazenar o último código consultado

    public Cardapio()
    {
        produtos = new List<IProduto>
    {
        new Comida("Moqueca de Palmito", 32, 1),
        new Comida("Falafel Assado", 20, 2),
        new Comida("Salada Primavera com Macarrão Konjac", 25, 3),
        new Comida("Escondidinho de Inhame", 18, 4),
        new Comida("Strogonoff de Cogumelos", 35, 5),
        new Comida("Caçarola de legumes", 22, 6),
        new Bebida("Água", 3, 7),
        new Bebida("Copo de suco", 7, 8),
        new Bebida("Refrigerante orgânico", 7, 9),
        new Bebida("Cerveja vegana", 9, 10),
        new Bebida("Taça de vinho vegano", 18, 11)
    };
    }

    public List<IProduto> ListarProdutos()
    {
        return produtos;
    }

    public List<Comida> ListarComidas()
    {
        return produtos.OfType<Comida>().ToList();
    }

    public List<Bebida> ListarBebidas()
    {
        return produtos.OfType<Bebida>().ToList();
    }

    private void DefinirUltimoCodigoConsultado(int codigo)
    {
        ultimoCodigoConsultado = codigo;
    }

    public IProduto BuscarProdutoPorCodigo()
    {
        foreach (var produto in produtos)
        {
            if (produto.Codigo == ultimoCodigoConsultado)
            {
                return produto;
            }
        }
        return null;
    }
}
