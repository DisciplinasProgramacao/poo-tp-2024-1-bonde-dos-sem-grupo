using System;

public class Mesa
{
    private int _idMesa;
    private int _lugaresMesa;
    private bool _status;

    public Mesa(int idMesa, int lugaresMesa, bool status)
    {
        _idMesa = idMesa;
        _lugaresMesa = lugaresMesa;
        _status = status;
    }

    public bool VerificaDisponibilidade()
    {
        return Status;
    }
}