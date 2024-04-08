using System;

public class Requisicao
{
    Cliente cliente;
    int numClientes;
    Mesa mesa;
    DateTime dataReq;
    DateTime horaEntrada;
    DateTime horaSaida;

    public void criarRequisicao(Cliente cliente, int numClientes, Mesa mesa)
    {
        this.cliente = cliente;
        this.numClientes = numClientes;
        this.mesa = mesa;
        this.dataReq = DateTime.Now;
        this.horaEntrada = DateTime.Now;
        // Pode ser aprimorado
    }

    public void encerrarRequisicao()
    {
        this.horaSaida = DateTime.Now;
        TimeSpan duracao = horaSaida - horaEntrada;
        Console.WriteLine($"Requisicao encerrada para {cliente.Nome} na mesa {mesa.Numero}.");
        Console.WriteLine($"Numero de clientes: {numClientes}");
        Console.WriteLine($"Hora de entrada: {horaEntrada}");
        Console.WriteLine($"Hora de saida: {horaSaida}");
        Console.WriteLine($"Duracaoo da requisicao: {duracao}");
        // Pode ser aprimorado
    }
}
