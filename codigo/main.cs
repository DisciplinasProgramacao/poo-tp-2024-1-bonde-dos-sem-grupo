using System;

class Program
{
    static void Main()
    {
        // Criando o restaurante
        var restaurante = new Restaurante(new List<Mesa>(), new List<Requisicao>());

        // Cadastrar cliente
        Console.WriteLine("Cadastro de Cliente");
        Console.Write("Nome do cliente: ");
        string nomeCliente = Console.ReadLine();
        Console.Write("CPF do cliente: ");
        string cpfCliente = Console.ReadLine();

        var cliente = new Cliente(nomeCliente, cpfCliente);

        // Criar uma requisição
        Console.WriteLine("\nAtendimento de Requisição");
        Console.Write("Número de clientes: ");
        int numClientes = Convert.ToInt32(Console.ReadLine());
        Console.Write("Id da mesa: ");
        int idMesa = Convert.ToInt32(Console.ReadLine());
        Console.Write("Quantidade de lugares na mesa: ");
        int lugaresMesa = Convert.ToInt32(Console.ReadLine());
        Console.Write("Data da requisição (DD/MM/AAAA): ");
        DateTime dataReq = DateTime.ParseExact(Console.ReadLine(), "dd/MM/yyyy", null);
        Console.Write("Hora de entrada (HH:MM): ");
        DateTime horaEntrada = DateTime.ParseExact(Console.ReadLine(), "HH:mm", null);
        Console.Write("Hora de saída (HH:MM): ");
        DateTime horaSaida = DateTime.ParseExact(Console.ReadLine(), "HH:mm", null);

        var mesa = new Mesa(idMesa, lugaresMesa);
        var requisicao = new Requisicao(cliente, numClientes, mesa, dataReq, horaEntrada, horaSaida);

        // Atender a requisição
        restaurante.AtenderRequisicao(requisicao);

        Console.WriteLine("\nRequisição atendida com sucesso!");
    }
}
