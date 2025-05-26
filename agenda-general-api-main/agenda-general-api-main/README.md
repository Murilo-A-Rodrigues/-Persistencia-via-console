# Agenda de Compromissos

Este é um projeto desenvolvido na matéria de Programação Orientada a Objetos. Ele utiliza a linguagem de programação C# para criar um sistema de agenda de compromissos com associação entre objetos.

## Funcionalidades

- Registro de compromissos com data, hora, descrição, local e capacidade.
- Adição de participantes aos compromissos, respeitando o limite de capacidade do local.
- Adição de anotações aos compromissos.
- Validação de dados de entrada, como datas, horários e capacidade.
- Exibição de compromissos registrados, incluindo participantes e anotações.

## Como Executar

1. Certifique-se de ter o .NET SDK instalado.
2. Navegue até o diretório do projeto e insira os seguintes comandos caso seja a primeira vez executando o projeto:
```bash
   cd "c:\Users\Muril\Downloads\Trabalho OO\-Persistencia-via-console\agenda-general-api-main\agenda-general-api-main"

   dotnet run
```
3. Após a primeira execução, é possível executar o projeto utilizando apenas o comando:
```bash
   dotnet run
```

## Estrutura do Projeto

- **Program.cs**: Contém o ponto de entrada do programa, responsável por coletar os dados fornecidos pelos usuários e gerenciar o menu principal.

- **Modelos/Local.cs**: Classe responsável por representar o local do compromisso, incluindo validações de nome e capacidade do local.

- **Modelos/Compromisso.cs**: Classe responsável por representar um compromisso, incluindo data, hora, descrição, local, participantes e anotações.

- **Modelos/Participantes.cs**: Classe responsável por representar os participantes de um compromisso.

- **Modelos/Anotacao.cs**: Classe responsável por representar as anotações de um compromisso.

- **Modelos/Usuário.cs**: Classe responsável por representar o usuário que gerencia os compromissos.

- **Persistencia/RepositorioCompromissos**: Classe responsável por criar o arquivo Json.

## Tecnologias Utilizadas

- **C#**: Linguagem de programação principal.
- **.NET SDK**: Framework para desenvolvimento e execução do projeto.

## Exemplos de Uso

### Iniciação do programa

O programa solicitará que selecione ou adicione o usuário e após isso abrirá um menu onde o usuário poderá escolher entre registrar um novo compromisso, visualizar os compromissos existentes, trocar o usuário, excluir um compromisso ou sair do programa.

### Registro de Compromisso

O programa solicitará as seguintes entradas:
- Nome do local: "Sala de Reunião"
- Capacidade máxima do local: 10
- Data do compromisso: 15/05/2025
- Hora do compromisso: 14:00
- Descrição do compromisso: "Reunião de planejamento"

Se os dados forem válidos, o programa permitirá adicionar participantes e anotações.

### Adição de Participantes

O programa solicitará os nomes dos participantes:
- Nome do participante: "João"
- Nome do participante: "Maria"

Se o número de participantes ultrapassar a capacidade do local, o programa exibirá a mensagem:

"Número de participantes excede a capacidade do local."
"Proseguindo sem adição do participante escedente."

### Adição de Anotações 

O programa solicitará as anotações:
- Texto da anotação: "Discutir metas do trimestre"
- Texto da anotação: "Preparar apresentação"

### Encerramento do Registro

Se todos os dados forem válidos, o programa exibirá a mensagem:

"Compromisso registrado com sucesso!"

### Exibição de Compromissos

Ao selecionar a opção 2 no menu, o programa exibirá os compromissos registrados, incluindo participantes e anotações. Exemplo:

"Compromissos registrados:

15/05/2025 14:00 - Reunião de planejamento no local Sala de Reunião
Participantes:
- João
- Maria
Anotações:
- Discutir metas do trimestre (Criada em: 01/05/2025 10:00)
- Preparar apresentação (Criada em: 02/05/2025 15:30)."

E em seguida solicitará:

- Data da reserva: '15/04/2025'
- Hora da reserva: '10:00'
- Descrição da sala: 'Sala de Estudos'
- Capacidade: '20'

### Troca de usuário

O programa retornará para o menu de usuários.

### Exclusão de compromisso

O programa apresentará um menu com os compromissos existentes, com números que representam a ordem em que eles foram criados, o usuário deve selecionar o número referente ao compromisso que deseja excluir.
Caso o usuário selecione um número que não represente nenhum compromisso o programa irá apenas retornar ao menu inicial. 

## Estratégia de Serialização Adotada

O projeto utiliza a biblioteca **System.Text.Json** para serializar e desserializar os dados dos usuários e seus compromissos em um arquivo JSON. Para garantir que listas privadas (como compromissos, participantes e anotações) sejam corretamente persistidas, foi adotado o uso do atributo `[JsonInclude]` nos campos privados dessas listas dentro das classes de modelo.

Dessa forma, mesmo mantendo o encapsulamento das coleções (sem expor setters públicos), o serializador consegue ler e gravar todos os dados necessários. O arquivo `usuarios.json` é salvo sempre na pasta de persistência do projeto, garantindo centralização e fácil acesso aos dados.

**Resumo da estratégia:**
- Uso do `System.Text.Json` para serialização e desserialização.
- Aplicação do atributo `[JsonInclude]` em listas privadas para garantir persistência sem comprometer o encapsulamento.
- O arquivo JSON é salvo em um caminho absoluto, sempre na pasta `AgendaCompromissos.Persistencia`.
- Todo o processo é transparente para o usuário, mantendo a integridade e a organização dos dados.

## Licença

Este projeto está licenciado sob a [MIT License](LICENSE).


## Créditos

Este projeto foi desenvolvido por Murilo Andre Rodrigues como parte da disciplina de Programação Orientada a Objetos.

## Contato

- **Email**: murilorodrigues@alunos.utfpr.edu.br
- **GitHub**: Murilo-A-Rodrigues(https://github.com/Murilo-A-Rodrigues)