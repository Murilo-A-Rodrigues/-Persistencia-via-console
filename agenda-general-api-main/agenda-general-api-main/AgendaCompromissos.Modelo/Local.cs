namespace AgendaCompromissos.Model;

public class Local
{
    public string Nome { get; private set; }
    public int Capacidade { get; private set; }

    public List<string> ErrosDeValidacao { get; private set; } = new();

    public Local(string nome, int capacidade)
    {
        if (string.IsNullOrWhiteSpace(nome))
        {
            ErrosDeValidacao.Add("O nome do local é obrigatório.");
        }

        if (capacidade <= 0)
        {
            ErrosDeValidacao.Add("A capacidade do local deve ser maior que zero.");
        }

        if (ErrosDeValidacao.Count > 0)
        {
            throw new ArgumentException(string.Join("\n", ErrosDeValidacao));
        }

        Nome = nome;
        Capacidade = capacidade;
    }

    public void ValidarCapacidade(int quantidade)
    {
        if (quantidade > Capacidade)
        {
            throw new InvalidOperationException("Quantidade de pessoas excede a capacidade do local.");
        }
    }
}