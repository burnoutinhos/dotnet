# 🔥 Burnoutinhos Project

Projeto desenvolvido para a Global Solution FIAP - Uma aplicação .NET focada em gerenciamento de tarefas, sugestões e blocos de tempo para ajudar usuários a evitar burnout através de organização e planejamento eficiente.
O sistema se baseia na organização pessoa das pessoas, principalmente as pessoas que trabalham home office, que faz com que as divisões de espaço e tempo sejam dificultadas, porém com o nosso aplicativo, é possível a criação de Tarefas e blocos de tempo para organizar o seu dia, também consta com um sistema de sugestões para as suas tarefas, assim fazendo com que a pessoa não precise pensar tanto em como realizar elas, assim deixando o dia mais leve, afastando cada vez mais o nosso usuário do burnout.


## 👥 Equipe Burnoutinhos

Desenvolvido pela equipe Burnoutinhos
- Gustavo Dias da Silva Cruz - RM556448

- Júlia Medeiros Angelozi - RM556364

- Felipe Ribeiro Tardochi da Silva - RM555100


## 📋 Sobre o Projeto

O Burnoutinhos é uma API RESTful que oferece funcionalidades para:
- Gerenciamento de usuários com autenticação JWT
- Sistema de tarefas (Todos) com paginação
- Sugestões baseadas em tarefas
- Blocos de tempo para organização
- Notificações para usuários
- Telemetria e monitoramento com OpenTelemetry
- Integração com Machine Learning (ML.NET)

## 🛠️ Tecnologias Utilizadas

- **.NET 9.0**
- **Entity Framework Core 9.0.4**
- **Oracle Database** (Oracle.EntityFrameworkCore 9.23.80)
- **JWT Authentication**
- **Swagger/OpenAPI**
- **OpenTelemetry** (Observabilidade)
- **ML.NET** (Machine Learning)
- **xUnit** (Testes)
- **FluentAssertions** (Asserções nos testes)
- **Moq** (Mocking)

## 📦 Pré-requisitos

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- Oracle Database (configurado conforme connection string)
- Visual Studio 2022 / VS Code / Rider

## 🚀 Como Executar o Projeto

### 1. Clone o repositório

```bash
git clone https://github.com/burnoutinhos/dotnet.git
cd dotnet/BurnoutinhosProject
```

### 2. Configure a Connection String

Edite o arquivo [`appsettings.json`](BurnoutinhosProject/appsettings.json):

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=seu_host:1521/orcl; User Id=seu_usuario;Password=sua_senha;"
  },
  "Jwt": {
    "Key": "sua_chave_secreta_aqui",
    "Issuer": "https://seu-dominio.com",
    "Audience": "https://sua-audiencia.com"
  }
}
```

### 3. Execute as Migrations

```bash
dotnet ef database update
```

### 4. Execute o projeto

```bash
dotnet run
```

A aplicação estará disponível em:
- **HTTPS**: https://localhost:7191
- **HTTP**: http://localhost:5023
- **Swagger UI**: https://localhost:7191/swagger

## 🧪 Como Executar os Testes

### Navegar para o projeto de testes

```bash
cd BurnoutinhosProject.Tests
```

### Executar todos os testes

```bash
dotnet test
```

### Executar com cobertura de código

```bash
dotnet test --collect:"XPlat Code Coverage"
```

### Ver resultados detalhados

```bash
dotnet test --logger "console;verbosity=detailed"
```

## 📍 Principais Endpoints

### 🔐 Autenticação

#### Login
```http
POST /auth/login
Content-Type: application/json

{
  "email": "usuario@exemplo.com",
  "password": "senha123"
}
```

**Resposta**: Token JWT para autenticação

---

### 👤 Usuários

#### Listar todos os usuários
```http
GET /user
Authorization: Bearer {token}
```

#### Buscar usuário por ID
```http
GET /user/{id}
Authorization: Bearer {token}
```

#### Criar novo usuário
```http
POST /user
Content-Type: application/json

{
  "name": "João Silva",
  "email": "joao@exemplo.com",
  "password": "senha123",
  "preferredLanguage": "pt_BR",
  "profileImage": "url_da_imagem"
}
```

#### Atualizar usuário
```http
PUT /user/{id}
Authorization: Bearer {token}
Content-Type: application/json
```

#### Deletar usuário
```http
DELETE /user/{id}
Authorization: Bearer {token}
```

---

### ✅ Tarefas (Todos)

#### Listar todas as tarefas
```http
GET /todo
Authorization: Bearer {token}
```

#### Listar tarefas com paginação
```http
GET /todo/paged?pageNumber=1&pageSize=10
Authorization: Bearer {token}
```

#### Buscar tarefa por ID
```http
GET /todo/{id}
Authorization: Bearer {token}
```

#### Listar tarefas por usuário
```http
GET /todo/user/{userId}
Authorization: Bearer {token}
```

#### Criar nova tarefa
```http
POST /todo
Authorization: Bearer {token}
Content-Type: application/json

{
  "name": "Estudar .NET",
  "description": "Revisar conceitos de Entity Framework",
  "isCompleted": 0,
  "userId": 1,
  "createdAt": "2025-01-20T10:00:00"
}
```

#### Atualizar tarefa
```http
PUT /todo/{id}
Authorization: Bearer {token}
Content-Type: application/json
```

#### Deletar tarefa
```http
DELETE /todo/{id}
Authorization: Bearer {token}
```

---

### 💡 Sugestões

#### Listar todas as sugestões
```http
GET /suggestion
Authorization: Bearer {token}
```

#### Buscar sugestão por ID
```http
GET /suggestion/{id}
Authorization: Bearer {token}
```

#### Criar nova sugestão
```http
POST /suggestion
Authorization: Bearer {token}
Content-Type: application/json

{
  "suggestionDescription": "Adicionar mais detalhes ao planejamento",
  "userId": 1,
  "createdAt": "2025-01-20T10:00:00"
}
```

#### Atualizar sugestão
```http
PUT /suggestion/{id}
Authorization: Bearer {token}
Content-Type: application/json
```

#### Deletar sugestão
```http
DELETE /suggestion/{id}
Authorization: Bearer {token}
```

---

### ⏰ Blocos de Tempo

#### Listar todos os blocos de tempo
```http
GET /timeblock
Authorization: Bearer {token}
```

#### Buscar bloco de tempo por ID
```http
GET /timeblock/{id}
Authorization: Bearer {token}
```

#### Criar novo bloco de tempo
```http
POST /timeblock
Authorization: Bearer {token}
Content-Type: application/json

{
  "start": 10.00,
  "end": 12.00,
  "name": "Estudar",
  "userId": 1
}
```

#### Atualizar bloco de tempo
```http
PUT /timeblock/{id}
Authorization: Bearer {token}
Content-Type: application/json
```

#### Deletar bloco de tempo
```http
DELETE /timeblock/{id}
Authorization: Bearer {token}
```

---

### 🔔 Notificações

#### Listar todas as notificações
```http
GET /notification
Authorization: Bearer {token}
```

#### Buscar notificação por ID
```http
GET /notification/{id}
Authorization: Bearer {token}
```

#### Criar nova notificação
```http
POST /notification
Authorization: Bearer {token}
Content-Type: application/json

{
  "message": "Nova tarefa disponível",
  "userId": 1,
  "createdAt": "2025-01-20T10:00:00"
}
```

---

### 📊 Telemetria

#### Health Check
```http
GET /api/telemetry/health
```

#### Métricas
```http
GET /api/telemetry/metrics
```

#### Teste de Trace
```http
POST /api/telemetry/trace-test
Content-Type: application/json

{
  "testData": "Exemplo de dados para teste"
}
```

---

## 🏗️ Estrutura do Projeto

```
BurnoutinhosProject/
├── Connection/          # Configuração do DbContext
├── Controllers/         # Controllers da API
├── DTO/                # Data Transfer Objects
├── Enums/              # Enumerações
├── Mappings/           # Entity Framework Mappings
├── Migrations/         # Migrations do EF Core
├── Models/             # Entidades do domínio
├── Repository/         # Camada de repositório
├── Service/            # Camada de serviço/negócio
└── Program.cs          # Ponto de entrada da aplicação

BurnoutinhosProject.Tests/
└── UnitTest1.cs        # Testes unitários
```

## 🔑 Autenticação JWT

O projeto utiliza JWT Bearer Token para autenticação. Após fazer login, inclua o token no header de todas as requisições:

```
Authorization: Bearer {seu_token_aqui}
```

## 📈 Observabilidade

O projeto está configurado com OpenTelemetry para:
- Traces distribuídos
- Métricas de performance
- Logs estruturados

Configure o endpoint no [`appsettings.json`](BurnoutinhosProject/appsettings.json):

```json
{
  "OpenTelemetry": {
    "Endpoint": "http://localhost:4317",
    "ServiceName": "Burnoutinhos",
    "ServiceVersion": "1.0.0"
  }
}
```

## 🧪 Testes Disponíveis

O projeto inclui testes para:
- ✅ Criação de notificações
- ✅ Exclusão de sugestões
- ✅ Criação de blocos de tempo
- ✅ Listagem de sugestões
- ✅ Criação de tarefas

Todos os testes usam banco de dados em memória (InMemory) para isolamento.

---

**Nota**: Lembre-se de atualizar as configurações de segurança (JWT Key, Connection String) antes de implantar em produção.