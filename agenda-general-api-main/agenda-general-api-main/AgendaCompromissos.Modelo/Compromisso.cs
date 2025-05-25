using System;
using System.Collections.Generic;

namespace AgendaCompromissos.Model;

public class Compromisso
{
    public DateTime DataHora { get; private set; }
    public string Descricao { get; private set; }
    public Usuario Usuario { get; private set; }
    public Local Local { get; private set; }

    private readonly List<Participante> _participantes = new();
    private readonly List<Anotacao> _anotacoes = new();

    public IReadOnlyCollection<Participante> Participantes => _participantes;
    public IReadOnlyCollection<Anotacao> Anotacoes => _anotacoes;

    public List<string> ErrosDeValidacao { get; private set; } = new();

    public Compromisso(DateTime dataHora, string descricao, Usuario usuario, Local local)
    {
        if (dataHora <= DateTime.Now)
        {
            ErrosDeValidacao.Add("A data e hora do compromisso devem ser no futuro.");
        }

        if (string.IsNullOrWhiteSpace(descricao))
        {
            ErrosDeValidacao.Add("A descrição do compromisso é obrigatória.");
        }

        if (ErrosDeValidacao.Count > 0)
        {
            throw new ArgumentException(string.Join("\n", ErrosDeValidacao));
        }

        DataHora = dataHora;
        Descricao = descricao;
        Usuario = usuario;
        Local = local;
    }

    public void AdicionarParticipante(Participante participante)
    {
        if (_participantes.Count >= Local.Capacidade)
        {
            throw new InvalidOperationException("Número de participantes excede a capacidade do local.");
        }
        _participantes.Add(participante);
        participante.AdicionarCompromisso(this);
    }

    public void AdicionarAnotacao(string texto)
    {
        _anotacoes.Add(new Anotacao(texto));
    }

    public override string ToString()
    {
        return $"{DataHora:dd/MM/yyyy HH:mm} - {Descricao} no local {Local.Nome}";
    }
}