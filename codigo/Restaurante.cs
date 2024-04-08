using System;
using System.Collections.Generic;

public class Restaurante
{
    private List<Mesa> mesas;
    private List<Requisicao> filaEspera;

    // Construtor
    public Restaurante()
    {
        // Inicializa
        this.mesas = new List<Mesa>();
        this.filaEspera = new List<Requisicao>();
    }

    /// <summary>
    /// Define uma mesa.
    /// </summary>
    /// <param name="requisicao"></param>
    /// <returns>Mensagem de sucesso.</returns>
    public string DefinirMesa(Requisicao requisicao)
    {
        // Percorre todas as mesas
        for (int i = 0; i < mesas.Count; i++)
        {
            // Verifica se a mesa está disponível
            if (mesas[i].verificaDisponibilidade())
            {
                // Define a mesa para a requisição
                mesas[i].status = false;
                mesas[i].lugaresMesa = requisicao.numClientes;
                mesas[i].idMesa = mesas.Count + 1;
                // Mensagem de sucesso
                return $"Mesa {mesas[i].idMesa} definida para a requisição de {requisicao.cliente.nome}.";
            }
        }
        // Mensagem de falha
        return "Mesa não disponível.";
    }

    /// <summary>
    /// Libera uma mesa.
    /// </summary>
    /// <param name="requisicao"></param>
    /// <returns>Mensagem de sucesso.</returns>
    public string LiberarMesa(Requisicao requisicao)
    {
        // Percorre todas as mesas
        for (int i = 0; i < mesas.Count; i++)
        {
            // Verifica se é a mesa da requisição
            if (mesas[i].idMesa == requisicao.mesa.idMesa)
            {
                // Libera a mesa
                mesas[i].status = true;
                // Mensagem de sucesso
                return $"Mesa {mesas[i].idMesa} liberada.";
            }
        }
        // Mensagem de falha
        return "Mesa não encontrada.";
    }

    /// <summary>
    /// Adiciona uma requisição para a lista de espera.
    /// </summary>
    /// <param name="requisicao"></param>
    public void AdicionarNaListaEspera(Requisicao requisicao)
    {
        filaEspera.Add(requisicao);
    }

    /// <summary>
    /// Atende uma requisição da lista de espera.
    /// </summary>
    /// <param name="requisicao"></param>
    public void AtenderRequisicao(Requisicao requisicao)
    {
        // Verifica se a requisição está na lista de espera
        if (filaEspera.Contains(requisicao))
        {
            // Remove a requisição da lista de espera
            filaEspera.Remove(requisicao);
            // Mensagem de falha
            Console.WriteLine($"Requisição de {requisicao.numClientes} clientes atendida.");
        }
        else
        {
            Console.WriteLine("Requisição não encontrada na lista de espera.");
        }
    }
}
