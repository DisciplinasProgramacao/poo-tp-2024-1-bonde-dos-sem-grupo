using System;

public class Restaurante
{
    private List <Mesa> mesas;
    private List <Requisicao> filaEspera;

    //Construtor 
    public Restaurante()
    {
        //Inicializa
        this.mesas = new List<Mesa>();
        this.filaEspera = new List<Requisicao>();
    }

    /// <summary>
    /// Definir uma mesa para uma requisição
    /// </summary>
    /// <param name="requisicao"></param>
    /// <returns></returns>
    public string DefinirMesa(Requisicao requisicao)
    {
        //Percorre todas as mesas
        foreach (var mesa in this.mesas)
        {
            //Verifica se a mesa está disponível
            if (mesa.verificaDisponibilidade())
            {
                //Define a mesa
                mesa.status = false;
                mesa.lugaresMesa = requisicao.numClientes;
                mesa.idMesa = this.mesas.Count + 1;
                //Retorna mensagem de sucesso
                return $"Mesa {mesa.idMesa} definida para a requisição de {requisicao.cliente.nome}.";
            }
        }
        //Retorna mensagem de falha
        return "Mesa não disponível.";
    }

    /// <summary>
    /// Liberar mesa para o cliente
    /// </summary>
    /// <param name="requisicao"></param>
    /// <returns></returns>
    public string LiberarMesa(Requisicao requisicao)
    {
        //Percorre todas as mesas
        foreach (var mesa in this.mesas)
        {
            //Verifica se a mesa corresponde à mesa da requisição
            if (mesa.idMesa == requisicao.mesa.idMesa)
            {
                //Libera a mesa
                mesa.status = true;
                //Retorna mensagem de sucesso
                return $"Mesa {mesa.idMesa} liberada.";
            }
        }
        //Retorna mensagem de falha
        return "Mesa não encontrada.";
    }

    /// <summary>
    /// Adiciona uma requisição à fila de espera
    /// </summary>
    /// <param name="requisicao"></param>
    public void AdicionarNaFilaEspera(Requisicao requisicao)
    {
        //Adiciona a requisição à fila de espera
        this.filaEspera.Add(requisicao);
    }

    /// <summary>
    /// Atender um cliente da fila de espera
    /// </summary>
    public void AtenderCliente()
    {
        // Verifica se há clientes na fila de espera
        if (this.filaEspera.Count > 0)
        {
            //Obtém a primeira requisição da fila de espera
            var requisicao = this.filaEspera[0];
            //Remove a requisição da fila de espera
            this.filaEspera.RemoveAt(0);
            //Mensagem de atendimento com sucesso
            Console.WriteLine($"Atendendo o cliente {requisicao.cliente.nome} na mesa {requisicao.mesa.idMesa}.");
        }
        else
        {
            //Mensagem se não tiver clientes esperando
            Console.WriteLine("Não há clientes na fila de espera.");
        }
    }
}