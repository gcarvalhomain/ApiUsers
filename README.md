# ApiUsers

API de gerenciamento de usuários desenvolvida com ASP.NET Core Minimal API.

Criei esse projeto para praticar os principais conceitos de uma Web API com C#, incluindo CRUD, banco de dados, autenticação JWT, autorização por roles, validações, paginação, busca e ordenação.

## Tecnologias usadas

* C#
* ASP.NET Core Minimal API
* Entity Framework Core
* SQL Server
* JWT
* Scalar
* Migrations

## O que a API faz

A API permite:

* cadastrar usuários
* fazer login
* gerar token JWT
* listar usuários
* buscar usuário por ID
* atualizar usuário
* remover usuário
* proteger rotas com autenticação
* limitar ações por role
* paginar resultados
* buscar por nome ou e-mail
* ordenar os resultados

## Autenticação

A autenticação é feita com JWT.

Rotas principais:

```text
POST /api/auth/register
POST /api/auth/login
GET  /api/auth/me
```

Exemplo de cadastro:

```json
{
  "name": "Maria Silva",
  "email": "maria@email.com",
  "age": 25,
  "password": "123456"
}
```

Exemplo de login:

```json
{
  "email": "maria@email.com",
  "password": "123456"
}
```

O login retorna um token JWT, que deve ser enviado nas rotas protegidas:

```text
Authorization: Bearer seu_token_aqui
```

## Roles

A API usa dois tipos de usuário:

```text
User
Admin
```

Por padrão, novos usuários são criados como `User`.

A role `Admin` é necessária para remover usuários.

## Rotas de usuários

```text
GET    /api/users
GET    /api/users/{id}
POST   /api/users
PUT    /api/users/{id}
DELETE /api/users/{id}
```

## Permissões

```text
GET    /api/users        Público
GET    /api/users/{id}   Público
POST   /api/users        Requer autenticação
PUT    /api/users/{id}   Requer autenticação
DELETE /api/users/{id}   Requer autenticação e role Admin
GET    /api/auth/me      Requer autenticação
```

## Listagem com filtros

A rota `GET /api/users` aceita alguns parâmetros opcionais:

```text
page
pageSize
search
sortBy
sortDirection
```

Exemplo:

```text
/api/users?page=1&pageSize=10&search=maria&sortBy=name&sortDirection=asc
```

Campos disponíveis para ordenação:

```text
id
name
email
age
createdAt
```

## Banco de dados

O projeto usa SQL Server com Entity Framework Core.

A connection string fica no `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost\\SQLEXPRESS;Database=ApiUsersDb;Trusted_Connection=True;TrustServerCertificate=True"
  }
}
```

Também é necessário configurar os dados do JWT:

```json
{
  "Jwt": {
    "Key": "sua-chave-secreta-com-tamanho-suficiente",
    "Issuer": "ApiUsers",
    "Audience": "ApiUsersClient",
    "ExpirationInMinutes": 60
  }
}
```

## Como rodar o projeto

Requisitos:

* .NET 10 SDK
* SQL Server ou SQL Server Express
* Entity Framework Core CLI

Instalar a ferramenta do Entity Framework, caso ainda não tenha:

```bash
dotnet tool install --global dotnet-ef
```

Rodar o projeto:

```bash
dotnet restore
dotnet ef database update
dotnet run
```

A API pode ser testada pelo Scalar:

```text
http://localhost:5203/scalar/v1
```

A porta pode mudar dependendo da configuração local.

## Status codes usados

```text
200 OK
201 Created
204 No Content
400 Bad Request
401 Unauthorized
403 Forbidden
404 Not Found
409 Conflict
```

## Estrutura do projeto

```text
Data        Configuração do banco
Dtos        Objetos de entrada e saída
Endpoints   Rotas da API
Migrations  Migrações do banco
Models      Modelos do projeto
Responses   Respostas padronizadas
Service     Regras de negócio
```

## Observação

Esse projeto foi desenvolvido como prática de backend com ASP.NET Core.

A ideia foi começar com um CRUD simples e evoluir aos poucos, adicionando banco de dados, autenticação, autorização, validações e recursos comuns em APIs reais.

Próximos pontos que ainda podem ser adicionados:

* refresh token
* testes automatizados
* deploy
* frontend consumindo a API
* melhorias na arquitetura
