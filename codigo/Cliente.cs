using System;

public class Cliente
{
    private string _nome;
    private string _cpf;

    public string Nome
    {
        get { return _nome; }
        set { _nome = value; }
    }

    public string CPF
    {
        get { return _cpf; }
        set { _cpf = value; }
    }

    public Cliente(string nome, string cpf)
    {
        _nome = nome;
        _cpf = cpf;
    }
}
