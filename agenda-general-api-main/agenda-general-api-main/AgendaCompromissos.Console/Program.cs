using AgendaCompromissos.Model;
using AgendaCompromissos.Persistencial;
using System.Globalization;

CultureInfo culturaBrasileira = new("pt-BR");

Console.WriteLine("Bem-vindo ao Sistema de Agenda de Compromissos!");

List<Usuario> usuarios = RepositorioCompromissos.Carregar();
Usuario usuario = null;

while (usuario == null) 
{
    Console.WriteLine("\nUsuários cadastrados:");
    if (usuarios.Count == 0)
        Console.WriteLine("Nenhum usuário cadastrado.");
    else
        for (int i = 0; i < usuarios.Count; i++)
            Console.WriteLine($"{i + 1}. {usuarios[i].Nome}");

    Console.Write("Digite o número do usuário para entrar ou pressione ENTER para criar novo: ");
    var entrada = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(entrada))
    {
        Console.Write("Digite o nome completo do novo usuário: ");
        string nomeUsuario = Console.ReadLine();

        var existente = usuarios.Find(u => u.Nome.Equals(nomeUsuario, StringComparison.OrdinalIgnoreCase));
        if (existente != null)
        {
            Console.WriteLine("Usuário já existe! Selecionando usuário existente.");
            usuario = existente;
        }
        else
        {
            usuario = new Usuario(nomeUsuario);
            usuarios.Add(usuario);
            RepositorioCompromissos.Salvar(usuarios);
        }
    }
    else if (int.TryParse(entrada, out int indice) && indice > 0 && indice <= usuarios.Count)
    {
        usuario = usuarios[indice - 1];
    }
    else
    {
        Console.WriteLine("Opção inválida.");
    }
}

while (true)
{
    Console.WriteLine($"\nUsuário atual: {usuario.Nome}");
    Console.WriteLine("1. Registrar novo compromisso");
    Console.WriteLine("2. Exibir compromissos");
    Console.WriteLine("3. Trocar de usuário");
    Console.WriteLine("4. Excluir compromisso");
    Console.WriteLine("0. Sair");
    Console.Write("Escolha uma opção: ");
    string opcao = Console.ReadLine();

    switch (opcao)
    {
        case "1":
            var usuarioAtual = usuarios.Find(u => u.Nome.Equals(usuario.Nome, StringComparison.OrdinalIgnoreCase));
            RegistrarCompromisso(usuarioAtual);
            RepositorioCompromissos.Salvar(usuarios);
            break;
        case "2":
            ExibirCompromissos(usuario);
            break;
        case "3":
            usuario = null;
            while (usuario == null)
            {
                Console.WriteLine("\nUsuários cadastrados:");
                for (int i = 0; i < usuarios.Count; i++)
                    Console.WriteLine($"{i + 1}. {usuarios[i].Nome}");
                Console.Write("Digite o número do usuário para entrar: ");
                var entradaTroca = Console.ReadLine();
                if (int.TryParse(entradaTroca, out int idx) && idx > 0 && idx <= usuarios.Count)
                    usuario = usuarios[idx - 1];
                else
                    Console.WriteLine("Opção inválida.");
            }
            break;
        
        case "4":
            ExcluirCompromisso(usuario, usuarios);
             break;
             
        case "0":
            RepositorioCompromissos.Salvar(usuarios);
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

static void ExcluirCompromisso(Usuario usuario, List<Usuario> usuarios)
{
    if (usuario.Compromissos.Count == 0)
    {
        Console.WriteLine("Nenhum compromisso para excluir.");
        return;
    }

    Console.WriteLine("\nCompromissos registrados:");
    int i = 1;
    foreach (var compromisso in usuario.Compromissos)
    {
        Console.WriteLine($"{i}. {compromisso}");
        i++;
    }

    Console.Write("Digite o número do compromisso que deseja excluir: ");
    if (int.TryParse(Console.ReadLine(), out int escolha) && escolha > 0 && escolha <= usuario.Compromissos.Count)
    {
        var lista = usuario.Compromissos.ToList();
        var compromissoRemover = lista[escolha - 1];

        // Remover do campo privado (precisa de um método na classe Usuario)
        usuario.RemoverCompromisso(compromissoRemover);

        RepositorioCompromissos.Salvar(usuarios);
        Console.WriteLine("Compromisso excluído com sucesso!");
    }
    else
    {
        Console.WriteLine("Opção inválida.");
    }
}