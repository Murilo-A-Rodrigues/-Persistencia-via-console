using System;

namespace AgendaCompromissos.Model;

public class Anotacao
{
    public string Texto { get; private set; }
    public DateTime DataCriacao { get; private set; }

    public Anotacao(string texto)
    {
        Texto = texto;
        DataCriacao = DateTime.Now;
    }
}