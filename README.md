# Ambev.DeveloperEvaluation

## Descrição
O projeto `Ambev.DeveloperEvaluation` é uma aplicação .NET 8 que gerencia vendas, incluindo a criação, atualização e fechamento de vendas. Ele segue os princípios de design orientado a domínio (DDD) e inclui validação de regras de negócios.

## Pré-requisitos
- .NET 8 SDK
- Visual Studio 2022 (ou outra IDE compatível)

## Configuração do Projeto

### Clonando o Repositório
Clone o repositório para sua máquina local usando o seguinte comando:

### Restaurando Dependências
Navegue até o diretório do projeto e restaure as dependências do NuGet:

## Executando a Aplicação

### Usando Visual Studio 2022
1. Abra o Visual Studio 2022.
2. Selecione `File > Open > Project/Solution` e abra o arquivo `Ambev.DeveloperEvaluation.sln`.
3. Defina o projeto de inicialização (startup project) clicando com o botão direito no projeto principal e selecionando `Set as Startup Project`.
4. Pressione `F5` para iniciar a aplicação.

### Usando CLI do .NET
1. Navegue até o diretório do projeto principal:

2. Execute a aplicação:

## Executando Testes

### Usando Visual Studio 2022
1. Abra o Test Explorer (`Test > Test Explorer`).
2. Clique em `Run All` para executar todos os testes.

### Usando CLI do .NET
1. Navegue até o diretório dos testes:
## Estrutura do Projeto
- `src/Ambev.DeveloperEvaluation.Application`: Contém a lógica de aplicação, incluindo handlers para comandos e consultas.
- `src/Ambev.DeveloperEvaluation.Domain`: Contém as entidades de domínio, eventos e interfaces de repositório.
- `src/Ambev.DeveloperEvaluation.ORM`: Contém a implementação dos repositórios usando Entity Framework.
- `tests/Ambev.DeveloperEvaluation.Unit`: Contém os testes unitários para a aplicação.

## Contribuição
1. Faça um fork do repositório.
2. Crie uma nova branch (`git checkout -b feature/nova-feature`).
3. Commit suas mudanças (`git commit -am 'Adiciona nova feature'`).
4. Faça o push para a branch (`git push origin feature/nova-feature`).
5. Abra um Pull Request.

## Licença
Este projeto está licenciado sob a licença MIT. Veja o arquivo `LICENSE` para mais detalhes.

