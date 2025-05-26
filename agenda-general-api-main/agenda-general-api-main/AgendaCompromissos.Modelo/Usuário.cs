using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AgendaCompromissos.Model;

public class Usuario
{
    public string Nome { get; private set; }

    [JsonInclude]
    private List<Compromisso> _compromissos = new();

    public IReadOnlyCollection<Compromisso> Compromissos => _compromissos;

    public Usuario(string nome)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new ArgumentException("O nome do usuário é obrigatório.");
        Nome = nome;
    }

    public void AdicionarCompromisso(Compromisso compromisso)
    {
        if (compromisso == null)
            throw new ArgumentNullException(nameof(compromisso), "Compromisso não pode ser nulo.");
        _compromissos.Add(compromisso);
    }

    public void RemoverCompromisso(Compromisso compromisso)
    {
        _compromissos.Remove(compromisso);
    }

}