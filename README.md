# Barber Style — Sistema MVC de Barbearia

Projeto didático em **C# / ASP.NET Core MVC (.NET 8)**, sem banco de dados.

## Requisitos
- .NET 8 SDK instalado
- Visual Studio 2022 (carga de trabalho ASP.NET e desenvolvimento Web) ou VS Code

## Como executar
1. Extraia o arquivo ZIP.
2. Abra a pasta `BarbeariaMVC` no Visual Studio ou abra o terminal nessa pasta.
3. Execute:
   ```bash
   dotnet restore
   dotnet run
   ```
4. Abra no navegador o endereço HTTPS/HTTP informado pelo terminal.

## Funcionalidades
- Painel inicial com indicadores e próximos horários.
- Cadastro, edição, listagem e exclusão de clientes.
- Cadastro, edição, listagem e exclusão de serviços.
- Cadastro, edição, listagem e exclusão de barbeiros.
- Agendamentos com seleção de cliente, serviço e barbeiro.
- Validação de campos, verificação de horário passado e conflito de horário do barbeiro.
- Marcar atendimento como concluído.

## Organização MVC

### Models
`Models/` contém as classes que representam os dados e as regras de validação:
- `Cliente`: nome, telefone e e-mail.
- `Servico`: nome, preço e duração.
- `Barbeiro`: nome, telefone e especialidade.
- `Agendamento`: cliente, serviço, barbeiro, data/hora, observação e status.
- `HomeViewModel`: dados reunidos para o painel.

### Views
`Views/` contém as páginas Razor (`.cshtml`) exibidas no navegador. Cada pasta corresponde a um controller. Os formulários enviam dados ao controller usando `asp-for` e `asp-action`.

### Controllers
`Controllers/` recebe as requisições do navegador, valida os dados, chama o repositório e escolhe qual View será apresentada.

### Data
`Data/BarbeariaRepository.cs` guarda os dados em listas na memória e oferece métodos para adicionar, consultar, atualizar e remover registros. Não é um banco de dados.

### wwwroot
Arquivos estáticos: CSS, JavaScript e imagens.

### Program.cs
Configura MVC, registra o repositório e define a rota padrão.

## Atenção sobre os dados
Este projeto foi solicitado sem banco de dados. Por isso, os registros são mantidos apenas na memória do processo. Eles somem quando a aplicação é encerrada ou reiniciada. Para uso real, o próximo passo seria conectar um banco de dados.
