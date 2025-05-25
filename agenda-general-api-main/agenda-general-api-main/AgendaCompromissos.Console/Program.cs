using AgendaCompromissos.Model;
using System.Globalization;

CultureInfo culturaBrasileira = new("pt-BR");

Console.WriteLine("Bem-vindo ao Sistema de Agenda de Compromissos!");

Console.Write("Digite o seu nome completo: ");
string nomeUsuario = Console.ReadLine();
Usuario usuario = new(nomeUsuario);

while (true)
{
    Console.WriteLine("\nMenu:");
    Console.WriteLine("1. Registrar novo compromisso");
    Console.WriteLine("2. Exibir compromissos");
    Console.WriteLine("3. Sair");
    Console.Write("Escolha uma opção: ");
    string opcao = Console.ReadLine();

    switch (opcao)
    {
        case "1":
            RegistrarCompromisso(usuario);
            break;
        case "2":
            ExibirCompromissos(usuario);
            break;
        case "3":
            Console.WriteLine("Encerrando o programa...");
            return;
        default:
            Console.WriteLine("Opção inválida. Tente novamente.");
            break;
    }
}

static void RegistrarCompromisso(Usuario usuario)
{
    DateTime? data = null;
    TimeSpan? hora = null;
    string descricao = null;
    string nomeLocal = null;
    int? capacidade = null;

    Console.WriteLine("\nVamos registrar um novo compromisso.\n");

    while( string.IsNullOrWhiteSpace(nomeLocal) || capacidade == null)
    {

    
    Console.Write("Digite o nome do local: ");
    nomeLocal = Console.ReadLine();
    

    while (capacidade == null)
    {
        Console.Write("Digite a capacidade máxima do local: ");
        var capacidadeDigitada = Console.ReadLine();
        try
        {
            capacidade = int.Parse(capacidadeDigitada);
        }
        catch (FormatException)
        {
            Console.WriteLine($"{capacidadeDigitada} não é um número válido. Tente novamente.");
        }
    }
    try{
        Local local = new(nomeLocal, capacidade.Value);
    }
    catch (ArgumentException ex)
    {
        Console.WriteLine($"Erro ao criar o local: {ex.Message}");
        nomeLocal = null;
        capacidade = null;
    }
    }
    
    while(data == null || hora == null || string.IsNullOrWhiteSpace(descricao)){
    while (data == null)
    {
        Console.Write("Digite a data do compromisso (dd/MM/yyyy): ");
        var dataDigitada = Console.ReadLine();
        try
        {
            data = DateTime.ParseExact(dataDigitada, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        }
        catch (FormatException )
        {
            Console.WriteLine($"{dataDigitada} não é uma data válida. Tente novamente.");
        }
    }

    while (hora == null)
    {
        Console.Write("Digite a hora do compromisso (HH:mm): ");
        var horaDigitada = Console.ReadLine();
        try
        {
            hora = TimeSpan.ParseExact(horaDigitada, "hh\\:mm", CultureInfo.InvariantCulture);
        }
        catch (FormatException)
        {
            Console.WriteLine($"{horaDigitada} não é uma hora válida(utilize o modelo HH:MM).");
        }
         catch (OverflowException)
    {
        Console.WriteLine($"{horaDigitada} não é uma hora válida(Tente um horário que esteja entre 00:00 e 23:59).");
    }
    }

    DateTime dataHora = data.Value.Add(hora.Value);

    
    Console.Write("Digite a descrição do compromisso: ");
    descricao = Console.ReadLine();

    try
    {
        Local local = new(nomeLocal, capacidade.Value);
        Compromisso compromisso = new(dataHora, descricao, usuario, local);

        Console.Write("Deseja adicionar participantes? (s/n): ");
        if (Console.ReadLine()?.ToLower() == "s")
        {
        try{
            while (true)
            {
                Console.Write("Digite o nome do participante (ou deixe em branco para finalizar): ");
                string nomeParticipante = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(nomeParticipante)) break;

                Participante participante = new(nomeParticipante);
                compromisso.AdicionarParticipante(participante);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Message}");
            Console.WriteLine($"Proseguindo sem adição do participante escedente.");
        }
        }

        Console.Write("Deseja adicionar anotações? (s/n): ");
        if (Console.ReadLine()?.ToLower() == "s")
        {
            while (true)
            {
                Console.Write("Digite o texto da anotação (ou deixe em branco para finalizar): ");
                string textoAnotacao = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(textoAnotacao)) break;

                compromisso.AdicionarAnotacao(textoAnotacao);
            }
        }

        usuario.AdicionarCompromisso(compromisso);
        Console.WriteLine("\nCompromisso registrado com sucesso!");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Erro ao registrar compromisso:\n {ex.Message}");
        data = null;
        hora = null;
        descricao = null;
    }
    }
}

static void ExibirCompromissos(Usuario usuario)
{
    Console.WriteLine("\nCompromissos registrados:");
    if (usuario.Compromissos.Count == 0)
    {
        Console.WriteLine("Nenhum compromisso registrado.");
        return;
    }

    foreach (var compromisso in usuario.Compromissos)
    {
        Console.WriteLine($"\n{compromisso}");
        
        // Exibir participantes
        if (compromisso.Participantes.Count > 0)
        {
            Console.WriteLine("Participantes:");
            foreach (var participante in compromisso.Participantes)
            {
                Console.WriteLine($"- {participante.Nome}");
            }
        }
        else
        {
            Console.WriteLine("Nenhum participante registrado.");
        }

        // Exibir anotações
        if (compromisso.Anotacoes.Count > 0)
        {
            Console.WriteLine("Anotações:");
            foreach (var anotacao in compromisso.Anotacoes)
            {
                Console.WriteLine($"- {anotacao.Texto} (Criada em: {anotacao.DataCriacao:dd/MM/yyyy HH:mm})");
            }
        }
        else
        {
            Console.WriteLine("Nenhuma anotação registrada.");
        }
    }
}