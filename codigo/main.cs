using System;

class Program
{
    static void Main()
    {
        // Criando instância do restaurante
        Restaurante restaurante = new Restaurante();

        // Cadastrando cliente
        Cliente cliente1 = new Cliente(1, "João");
        restaurante.CadastrarCliente(cliente1);

        // Criando mesa
        Mesa mesa1 = new Mesa(1);
        restaurante.Mesas.Add(mesa1);

        // Criando requisição
        Requisicao requisicao1 = restaurante.CriarRequisicao(1, cliente1);

        // Alocando requisição à mesa
        mesa1.AlocarRequisicao(requisicao1);

        // Encerrando requisição
        requisicao1.Encerrar();
    }
}
