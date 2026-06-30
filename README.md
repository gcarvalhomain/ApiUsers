# ApiUsers

## Português

API simples para gerenciar usuários.

ASP.NET Core Minimal API, Entity Framework Core e SQL Server. A API salva usuários no banco e permite criar, listar, buscar, atualizar e remover registros.

Cada usuário tem:

- `id`
- `name`
- `email`
- `age`
- `createdAt`
- `updatedAt`

O email não pode se repetir.

### Como rodar o projeto

Você precisa ter instalado:

- .NET 10 SDK
- SQL Server ou SQL Server Express
- Entity Framework Core CLI

Instale a ferramenta do Entity Framework se ainda não tiver: 

```bash
dotnet tool install --global dotnet-ef
```

Veja a conexão do banco em `appsettings.json`:

```json
"DefaultConnection": "Server=localhost\\SQLEXPRESS;Database=ApiUsersDb;Trusted_Connection=True;TrustServerCertificate=True"
```

Depois rode:

```bash
dotnet restore
dotnet ef database update
dotnet run
```

O projeto abre nestes endereços:

```text
http://localhost:5203
https://localhost:7272
```

### Rotas

```text
GET    /api/users
GET    /api/users/{id}
POST   /api/users
PUT    /api/users/{id}
DELETE /api/users/{id}
```

### Criar usuário

```json
{
  "name": "Maria Silva",
  "email": "maria@email.com",
  "age": 25
}
```

### Listar usuários

A rota `GET /api/users` aceita filtros:

```text
page=1
pageSize=10
search=maria
sortyBy=name
SortedDirection=desc
```

Exemplo:

```text
/api/users?page=1&pageSize=10&search=maria&sortyBy=name&SortedDirection=desc
```

Observação: no código o parâmetro está como `sortyBy`, então use esse nome mesmo.

### Pastas principais

```text
Data        Configuração do banco
Dtos        Dados de entrada e saída
Endpoints   Rotas da API
Migrations  Migrações do banco
Models      Modelos do projeto
Responses   Respostas padrão
Service     Regras dos usuários
```

### Status codes
```text
    200 OK
    201 Created
    204 No Content
    400 Bad Request
    404 Not Found
    409 Conflict
```

---

## English

Simple API to manage users.

ASP.NET Core Minimal API, Entity Framework Core, and SQL Server. The API stores users in the database and can create, list, search, update, and delete records.

Each user has:

- `id`
- `name`
- `email`
- `age`
- `createdAt`
- `updatedAt`

The email must be unique.

### How to run

You need:

- .NET 10 SDK
- SQL Server or SQL Server Express
- Entity Framework Core CLI

Install the Entity Framework tool if you do not have it:

```bash
dotnet tool install --global dotnet-ef
```

Check the database connection in `appsettings.json`:

```json
"DefaultConnection": "Server=localhost\\SQLEXPRESS;Database=ApiUsersDb;Trusted_Connection=True;TrustServerCertificate=True"
```

Then run:

```bash
dotnet restore
dotnet ef database update
dotnet run
```

The project runs on:

```text
http://localhost:5203
https://localhost:7272
```

### Routes

```text
GET    /api/users
GET    /api/users/{id}
POST   /api/users
PUT    /api/users/{id}
DELETE /api/users/{id}
```

### Create user

```json
{
  "name": "Maria Silva",
  "email": "maria@email.com",
  "age": 25
}
```

### List users

The `GET /api/users` route accepts filters:

```text
page=1
pageSize=10
search=maria
sortyBy=name
SortedDirection=desc
```

Example:

```text
/api/users?page=1&pageSize=10&search=maria&sortyBy=name&SortedDirection=desc
```

Note: the parameter in the code is `sortyBy`, so use that name.

### Main folders

```text
Data        Database setup
Dtos        Request and response data
Endpoints   API routes
Migrations  Database migrations
Models      Project models
Responses   Standard responses
Service     User rules
```
