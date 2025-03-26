# Challenge Application

## Descrição

Este projeto é uma aplicação de exemplo que demonstra a implementação de um serviço de contas utilizando C# e .NET 8. A aplicação inclui a criação de contas, gerenciamento de transações e integração com um banco de dados.

## Estrutura do Projeto

- **Challenge.API**: Contém os controladores e a configuração da API.
- **Challenge.Application**: Contém os serviços e a lógica de negócios.
- **Challenge.Domain**: Contém as entidades, interfaces e DTOs.
- **Challenge.Infrastructure**: Contém as implementações de repositórios e bibliotecas externas.
- **Challenge.DI**: Contém as extensões para injeção de dependências.
- **Challenge.Database**: Contém os scripts SQL para criação de tabelas e procedimentos armazenados.

## Principais caracteristicas

✅ **Arquitetura Limpa (Clean Architecture)**: A API foi projetada com base em princípios sólidos de arquitetura, garantindo modularidade, escalabilidade e fácil manutenção.

✅ **Validação de Dados de Entrada com FluentValidation**: Implementação de validações rigorosas e mensagens de erro claras para garantir que as entradas estejam sempre corretas e bem formatadas.

✅ **Autenticação de Rotas com JWT**: Proteção das rotas sensíveis utilizando JSON Web Token, garantindo que apenas usuários autenticados possam acessar determinadas funcionalidades.

✅ **Global Error Handler**: Middleware centralizado para capturar e tratar erros de maneira padronizada, com respostas claras e códigos de status apropriados.

✅ **Testes Unitários com NUnit, Moq e Fluent Assertions**: Adoção de boas práticas de testes automatizados, utilizando NUnit para os testes, Moq para mocks de dependências e Fluent Assertions para uma sintaxe mais fluente e expressiva nos asserts.

✅ **Docker para Execução do Banco de Dados (SQL Server)**: Utilização do Docker para orquestrar o SQL Server, garantindo um ambiente de desenvolvimento consistente, fácil de configurar e pronto para testes.

✅ **Padrão Unit of Work**: Implementação do Unit of Work para gerenciar transações de forma eficiente, garantindo consistência nos dados e melhor controle sobre as operações no banco.

✅ **Mapeamento de dados com Mapster**: Utilização do Mapster para realizar o mapeamento de dados entre modelos de forma eficiente, garantindo que as transformações de objetos sejam feitas de maneira limpa e performática.

✅ **Envio de E-mail para o Recebedor na Transferência**: Implementado um sistema de envio de e-mails notificando o recebedor sempre que uma transferência for concluída com sucesso, garantindo melhor comunicação e transparência.

✅ **Salvamento de Exceções no MongoDB**: Qualquer erro inesperado durante o processamento da API é capturado e salvo no MongoDB, permitindo melhor rastreamento e análise de falhas.

✅ **Cache com Redis**: Implementado Redis para armazenar informações de transferências e consultas frequentemente acessadas, otimizando a performance da API e reduzindo a carga no banco de dados.

## Endpoints

### **1. Criação de Usuário**

**POST** `/api/v{version}/user`\

#### **Request**

**Body (JSON)**
```json
{
  "name": "string",
  "email": "string",
  "password": "string"
}
```

#### **Response**

**Código 200** – Sucesso

```json
{
  "userId": "uuid",
  "name": "string"
}
```

### **2. Geração de Token**

**POST** `/api/v{version}/auth`\
Gera um novo token JWT para acesso à rotas seguras.

#### **Request**

**Body (JSON)**

```json
{
  "userId": "string",
  "password": "string"
}
```

#### **Response**

**Código 200** – Sucesso

```json
{
  "Token": "string",
  "Expiration": "2025-03-25T12:00:00Z"
}
```

### **3. Criação de Pessoa Física**

**POST** `/api/v{version}/individual`\
Cria uma nova Individual Person.

#### **Request**

**Body (JSON)**

```json
{
  "name": "string",
  "email": "string",
  "phone": "string",
  "cpf": "string",
  "birthDate": "string"
}
```

#### **Response**

**Código 200** – Sucesso

```json
{
  "personId": "uuid",
  "name": "string",
  "cpf": "string"
}
```

### **4. Criação de Pessoa Logista**

**PUT** `/api/accounts/{id}`\
Cria um novo Merchant Person.

#### **Request**

- **Body (JSON)**

```json
{
  "name": "string",
  "email": "string",
  "phone": "string",
  "cnpj": "string",
  "merchantName": "string",
  "merchantAddress": "string",
  "merchantContact": "string"
}
```

#### **Response**

**Código 200** – Sucesso

```json
{
  "personId": "uuid",
  "name": "string",
  "cnpj": "string",
  "merchantName": "string"
}
```

### **5. Criação de Conta**

**POST** `/api/v{version}/account`\
Criação de conta bancária.

#### **Request**

- **Body (JSON)**

```json
{
  "personId": "string",
  "accountNumber": "string",
  "balance": 0
}
```

#### **Response**

**Código 200** – Sucesso

```json
{
  "personId": "string",
  "accountNumber": "string",
  "balance": 0
}
```

### **6. Transferência**

**POST** `/api/v{version}/account/transfer`\
Transferência de valores entre contas.

#### **Request**

- **Body (JSON)**

```json
{
  "value": 0,
  "payerId": "uuid",
  "payeeId": "uuid"
}
```

#### **Response**

**Código 200** – Sucesso

```json
{
  "transferId": "uuid",
  "value": 0,
  "payerId": "uuid",
  "payeeId": "uuid",
  "payeeName": "string",
  "accountNumber": "string",
  "createdAt": "2025-03-25T12:00:00Z",
}
```

### **7. Depósito**

**POST** `/api/v{version}/account/{accountNumber}/deposit`\
Adiciona saldo à conta.

#### **Request**

- **Parâmetro na URL**: `accountNumber` (Número da conta da Person)
- **Body (JSON)**

```json
{
  "value": 0,
  "personId": "uuid"
}
```

#### **Response**

**Código 200** – Sucesso

```json
{
  "depositId": "uuid",
  "value": 0,
  "personId": "uuid",
  "accountNumber": "string",
  "createdAt": "2025-03-25T12:00:00Z",
}
```

---

