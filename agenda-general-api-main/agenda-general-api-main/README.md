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
   cd "c:\Users\Muril\Downloads\Trabalho OO\agenda-general-api"

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

## Tecnologias Utilizadas

- **C#**: Linguagem de programação principal.
- **.NET SDK**: Framework para desenvolvimento e execução do projeto.

## Exemplos de Uso

### Iniciação do programa

O programa solicitará o nome completo do usuário e abrirá um menu onde o usuário poderá escolher entre registrar um novo compromisso, visualizar os compromissos existentes ou sair do programa.

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

## Licença

Este projeto está licenciado sob a [MIT License](LICENSE).


## Créditos

Este projeto foi desenvolvido por Murilo Andre Rodrigues e por Victor Luiz De Oliveira Paes como parte da disciplina de Programação Orientada a Objetos.

## Contato

- **Email**: murilorodrigues@alunos.utfpr.edu.br
- **GitHub**: Murilo-A-Rodrigues(https://github.com/Murilo-A-Rodrigues)

- **Email**: victorpaes@alunos.utfpr.edu.br
- **GitHub**: VctPaes(https://github.com/VctPaes)