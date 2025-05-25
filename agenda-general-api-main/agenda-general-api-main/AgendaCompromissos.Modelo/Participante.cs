using System.Collections.Generic;

namespace AgendaCompromissos.Model;

public class Participante
{
    public string Nome { get; private set; }
    private readonly List<Compromisso> _compromissos = new();

    public IReadOnlyCollection<Compromisso> Compromissos => _compromissos;

    public Participante(string nome)
    {
        Nome = nome;
    }

    public void AdicionarCompromisso(Compromisso compromisso)
    {
        if (!_compromissos.Contains(compromisso))
        {
            _compromissos.Add(compromisso);
        }
    }
}