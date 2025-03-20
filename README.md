# Challenge Application

## Descrição

Este projeto é uma aplicação de exemplo que demonstra a implementação de um serviço de contas utilizando C# e .NET 8. A aplicação inclui a criação de contas, gerenciamento de transações e integração com um banco de dados.

## Estrutura do Projeto

- **Challenge.Application**: Contém os serviços e a lógica de negócios.
- **Challenge.Domain**: Contém as entidades, interfaces e DTOs.
- **Challenge.Infrastructure**: Contém as implementações de repositórios e configurações.
- **Challenge.DI**: Contém as extensões para injeção de dependências.
- **Challenge.Database**: Contém os scripts SQL para criação de tabelas e procedimentos armazenados.

## Instalação

1. Clone o repositório:
    
```shell
    git clone https://github.com/seu-usuario/challenge-application.git
    cd challenge-application
    
```

2. Restaure os pacotes NuGet:
    
```shell
    dotnet restore
    
```

3. Configure a string de conexão no arquivo `appsettings.json`:
    
```json
    {
      "ConnectionStrings": {
        "DefaultConnection": "SuaStringDeConexaoAqui"
      }
    }
    
```

4. Execute as migrações do banco de dados:
    
```shell
    dotnet ef database update
    
```

