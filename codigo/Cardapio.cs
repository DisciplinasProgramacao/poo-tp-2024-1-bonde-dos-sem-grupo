using System;

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
