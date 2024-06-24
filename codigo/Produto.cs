// Implementação básica da interface IProduto
public abstract class Produto : IProduto
{
    public string Nome { get; set; }
    public double Preco { get; set; }
    public int Codigo { get; set; }

    protected Produto(string nome, double preco, int codigo)
    {
        Nome = nome;
        Preco = preco;
        Codigo = codigo;
    }
}
