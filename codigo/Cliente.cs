using System;

public class Cliente
{
    private string nome;
    private string cpf;

    public Cliente(string nome, string cpf)
    {
        this.nome = nome;
        this.cpf = cpf;
    }

    public string GetNome()
    {
        return nome;
    }

    public string GetCpf()
    {
        return cpf;
    }
}
