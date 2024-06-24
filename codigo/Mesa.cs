using System;

public class Mesa
{
    public int IdMesa { get; }
    public int LugaresMesa { get; }
    public bool Status { get; set; }

    public Mesa(int idMesa, int lugaresMesa, bool status)
    {
        IdMesa = idMesa;
        LugaresMesa = lugaresMesa;
        Status = status;
    }

    public bool VerificaDisponibilidade()
    {
        return Status;
    }
}
