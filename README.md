# ORBITA SOA API – Futuro do Trabalho 👩‍💻🚀

> API Web **ASP.NET Core** com **SOA & WebServices**, **JWT**, **autorização por perfis** e **camadas bem definidas** para orquestrar trilhas de carreira, missões e progresso de aprendizagem focados no **Futuro do Trabalho**.

Global Solution – 2º Semestre  
Disciplina: **SOA & WebServices**

---

## 👥 Integrantes

| Nome | RM |
|------|----|
| **Kaue Pastori Teixeira** | **RM98501** |
| **Felipe Bressane** | **RM97688** |
| **Nicolas Boni**       | **RM551965** |

---

## 🎯 Visão Geral do Projeto

A **ORBITA SOA API** é um conjunto de **serviços RESTful** pensados como *building blocks* reutilizáveis para plataformas de desenvolvimento de carreira.

O objetivo é apoiar empresas e pessoas em desafios ligados ao **Futuro do Trabalho**, permitindo:

- Cadastrar **usuários** (alunos, mentores, admins);
- Modelar **rotas de carreira** (career paths) por área e nível;
- Criar **missões** práticas (tarefas de estudo, desafios, atividades);
- Acompanhar o **progresso** do usuário em cada missão;
- Garantir **segurança**, **autenticação JWT** e **autorização por perfis** (Student, Mentor, Admin);
- Expor tudo isso via **API SOA**, organizada em **serviços mínimos, independentes e reutilizáveis**.

Essa API pode ser consumida por:

- Portais web de carreira;
- Aplicativos mobile;
- Bots de chat/IA;
- Sistemas internos de RH/L&D que desejem integrar trilhas de upskilling e reskilling.

---

## 🧩 Como o projeto conversa com o tema “Futuro do Trabalho”

O futuro do trabalho exige:

- **Preparar pessoas para carreiras que ainda nem existem**;
- **Conectar dados** de aprendizado, progresso, engajamento;
- **Criar experiências flexíveis, remotas, híbridas e digitais**;
- **Automatizar** parte da jornada, sem perder personalização.

A ORBITA SOA API endereça isso ao:

- Modelar **rotas de carreira** e **missões** como entidades de negócio reutilizáveis;
- Permitir que diversos canais (web, mobile, bot, IA) consumam os mesmos serviços;
- Oferecer mecanismos de **segurança corporativa** (JWT, roles, stateless);
- Tornar fácil acompanhar o **progresso de aprendizagem** de cada colaborador/aluno.

---

## ⚙️ Stack Técnica

- **Linguagem:** C# / .NET
- **Framework Web:** ASP.NET Core Web API
- **Arquitetura:** SOA / RESTful + camadas (Domain, Application, Infrastructure, Api)
- **Persistência:** Entity Framework Core + SQL Server
- **Autenticação:** JWT Bearer (stateless)
- **Autorização:** Roles (`Student`, `Mentor`, `Admin`)
- **Documentação:** Swagger / OpenAPI
- **Tratamento de Erros:** Middleware global de exceções
- **Testes manuais:** Postman (collection incluída no repositório)

---

## 🏗 Arquitetura e Organização de Pastas

Estrutura geral da solução:

```text
Orbita.SoaApi/
├─ Api/
│  ├─ Controllers/
│  │  ├─ AuthController.cs
│  │  ├─ UsersController.cs
│  │  ├─ CareerPathsController.cs
│  │  ├─ MissionsController.cs
│  │  ├─ ProgressController.cs
│  │  └─ CareerPathsV2Controller.cs   // Exemplo de versionamento v2
│  ├─ Middlewares/
│  │  └─ ExceptionHandlingMiddleware.cs
│  └─ Program.cs                       // DI, Auth, Swagger, Versionamento
│
├─ Application/
│  ├─ DTOs/
│  │  ├─ Auth/
│  │  ├─ Users/
│  │  ├─ CareerPaths/
│  │  ├─ Missions/
│  │  └─ Progress/
│  ├─ Interfaces/
│  │  ├─ IAuthService.cs
│  │  ├─ IUserService.cs
│  │  ├─ ICareerPathService.cs
│  │  ├─ IMissionService.cs
│  │  └─ IProgressService.cs
│  └─ Services/
│     ├─ AuthService.cs
│     ├─ UserService.cs
│     ├─ CareerPathService.cs
│     ├─ MissionService.cs
│     └─ ProgressService.cs
│
├─ Domain/
│  ├─ Entities/
│  │  ├─ User.cs
│  │  ├─ CareerPath.cs
│  │  ├─ Mission.cs
│  │  └─ UserMissionProgress.cs
│  ├─ Enums/
│  │  ├─ UserRole.cs
│  │  └─ MissionStatus.cs
│  ├─ ValueObjects/
│  │  └─ Email.cs
│  ├─ Exceptions/
│  │  ├─ NotFoundException.cs
│  │  ├─ ValidationException.cs
│  │  ├─ UnauthorizedException.cs
│  │  ├─ ForbiddenException.cs
│  │  └─ ConflictException.cs
│  └─ Responses/
│     └─ ApiResponse.cs
│
├─ Infrastructure/
│  ├─ Persistence/
│  │  └─ OrbitaContext.cs
│  ├─ Security/
│  │  ├─ JwtOptions.cs
│  │  ├─ JwtTokenGenerator.cs
│  │  ├─ IPasswordHasher.cs
│  │  └─ PasswordHasher.cs
│  └─ Config/
│     └─ DependencyInjection.cs
│
├─ appsettings.json
└─ Orbita.SoaApi.csproj
```

**Pontos-chave de SOA:**

- Cada controller expõe **serviços de negócio** específicos (Auth, Users, CareerPaths, Missions, Progress).
- As regras de negócio estão em **services** (Application), não nos controllers.
- Camada Domain é **agnóstica de infraestrutura**, focada em entidades, VOs, enums e exceções.
- Tudo é consumível via **WebServices REST** (HTTP/JSON), adequado ao escopo da disciplina.

---

## 📦 Modelagem de Domínio

### Entidades principais

- **User**
  - `Id`, `Name`, `Email` (VO), `PasswordHash`, `Role` (enum), `WeeklyAvailableHours`, `CreatedAt`
- **CareerPath**
  - `Id`, `Name`, `Area`, `Description`, `Level`
  - Navegação: coleção de `Missions`
- **Mission**
  - `Id`, `CareerPathId`, `Title`, `Description`, `Difficulty`, `EstimatedMinutes`, `XpReward`
- **UserMissionProgress**
  - `Id`, `UserId`, `MissionId`, `Status` (enum), `StartedAt`, `CompletedAt`

### Value Object (VO)

- **Email**
  - Garante formato válido na criação (`Email.Create(...)`).
  - Se inválido, lança `ValidationException`.

### Enums

- `UserRole` → `Student`, `Mentor`, `Admin`
- `MissionStatus` → `Pendente`, `EmAndamento`, `Concluida`

---

## 🔐 Segurança, Autenticação e Autorização

### JWT – Autenticação Stateless

- Autenticação configurada com **JWT Bearer**.
- Tokens contém:
  - `sub` → Id do usuário
  - `email`
  - `role` (Student/Mentor/Admin)
- Não há `Session` em servidor → política **STATELESS**.

### Perfis e Autorização

- Controllers usam `[Authorize]` com **roles**:
  - Ex.: `[Authorize(Roles = "Admin,Mentor")]` em criação de CareerPaths e Missions.
  - `[Authorize(Roles = "Admin")]` em endpoints administrativos.
  - `[Authorize(Roles = "Student")]` em operações de progresso do aluno.

### Registro e Login

- `POST /api/v1/Auth/register` → cria novo usuário com papel default `Student`.  
  - Regra especial configurada no código: e-mails específicos podem ser promovidos para Admin de forma automática (facilitando testes sem mexer no banco).
- `POST /api/v1/Auth/login` → retorna `token`, `expiresAt`, `name`, `email`, `role`.

---

## 🧪 Tratamento Global de Exceções (Advice)

A classe `ExceptionHandlingMiddleware` centraliza o tratamento de erros:

- Captura exceções de domínio (`NotFoundException`, `ValidationException`, `ConflictException`, etc.).
- Mapeia para códigos HTTP apropriados:
  - 400 – validação
  - 401 – não autenticado
  - 403 – não autorizado
  - 404 – não encontrado
  - 409 – conflito (ex.: e-mail já existente)
  - 500 – erro interno inesperado
- Sempre retorna no formato de `ApiResponse`:

```json
{
  "success": false,
  "message": "Mensagem explicativa",
  "errors": null
}
```

Isso equivale ao padrão de **`@ControllerAdvice`** em Spring, porém usando o padrão de middlewares do ASP.NET Core.

---

## 🌐 Endpoints Principais (Visão Geral)

Base da API (exemplo):  
`https://localhost:65148`

Documentação:  
`/swagger`

### Autenticação

| Verbo | Rota                    | Descrição                     |
|-------|-------------------------|-------------------------------|
| POST  | `/api/v1/Auth/register` | Registra novo usuário         |
| POST  | `/api/v1/Auth/login`    | Autentica e gera token JWT    |

### Usuário (perfil e contexto)

| Verbo | Rota                        | Descrição                                 |
|-------|-----------------------------|-------------------------------------------|
| GET   | `/api/v1/Users/me`          | Retorna dados do usuário autenticado      |
| GET   | `/api/v1/Users`             | Lista usuários (somente Admin)            |
| PUT   | `/api/v1/Users/{id}/role`   | Atualiza role (Admin → Student/Mentor/Admin) |

### Career Paths

| Verbo | Rota                       | Descrição                                   |
|-------|----------------------------|---------------------------------------------|
| GET   | `/api/v1/CareerPaths`      | Lista todas as rotas de carreira            |
| GET   | `/api/v1/CareerPaths/{id}` | Detalha uma rota                            |
| POST  | `/api/v1/CareerPaths`      | Cria nova rota (Admin/Mentor)               |
| PUT   | `/api/v1/CareerPaths/{id}` | Atualiza rota (Admin/Mentor)                |
| DELETE| `/api/v1/CareerPaths/{id}` | Remove rota (somente Admin)                 |

### Missões

| Verbo | Rota                        | Descrição                                  |
|-------|-----------------------------|--------------------------------------------|
| GET   | `/api/v1/Missions`          | Lista missões                              |
| GET   | `/api/v1/Missions/{id}`     | Detalha missão                             |
| POST  | `/api/v1/Missions`          | Cria missão (Admin/Mentor)                 |
| PUT   | `/api/v1/Missions/{id}`     | Atualiza missão                            |
| DELETE| `/api/v1/Missions/{id}`     | Remove missão                              |

### Progresso do Usuário

| Verbo | Rota                                         | Descrição                                         |
|-------|----------------------------------------------|---------------------------------------------------|
| GET   | `/api/v1/Progress`                           | Lista progresso do usuário autenticado (Student)  |
| POST  | `/api/v1/Progress`                           | Cria/começa progresso em missão                   |
| PUT   | `/api/v1/Progress/{progressId}/status`       | Atualiza status (`EmAndamento` / `Concluida`)     |

---

## 📡 Versionamento da API

A solução inclui exemplo de versionamento:

- **v1** – API principal:
  - `/api/v1/...`
- **v2** – endpoint de demonstração:
  - `/api/v2/CareerPaths`

O controller `CareerPathsV2Controller` retorna uma payload simples indicando a versão, apenas para mostrar ao avaliador a estrutura de rotas versionadas.

---

## ▶️ Como Executar o Projeto

### 1. Pré-requisitos

- **.NET SDK 8.0+** (ou a versão configurada no `.csproj`)
- **SQL Server** ou **LocalDB** (por padrão, `(localdb)\\MSSQLLocalDB`)

### 2. Configurar conexão com banco

No arquivo `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\MSSqlLocalDB;Database=OrbitaSoaDb;Trusted_Connection=True;TrustServerCertificate=True;"
  },
  "Jwt": {
    "Issuer": "Orbita.SoaApi",
    "Audience": "Orbita.SoaApi",
    "SecretKey": "chave-super-secreta-para-dev"
  }
}
```

- Caso não use LocalDB, alterar para:
  - `Server=localhost;Database=OrbitaSoaDb;User Id=...;Password=...;TrustServerCertificate=True;`

### 3. Rodar migrations/criação do banco

O projeto está configurado para executar `EnsureCreated()` na inicialização, criando o banco e as tabelas automaticamente.

### 4. Rodar a API

No diretório do projeto:

```bash
dotnet restore
dotnet build
dotnet run
```

O console exibirá algo como:

```text
Now listening on: https://localhost:65148
```

Acesse o **Swagger** em:  
`https://localhost:65148/swagger`

(Porta pode variar conforme sua máquina.)

---

## 🧪 Roteiro de Testes Sugerido (Swagger ou Postman)

### Fluxo recomendado para demonstração em vídeo / avaliação

1. **Registrar Student**
   - `POST /api/v1/Auth/register`  
   - Body com nome, e-mail e senha de um aluno.

2. **Registrar Admin**
   - `POST /api/v1/Auth/register` com um e-mail que o código promove a Admin (por exemplo `admin@orbita.admin`, conforme implementado).
   - Mostrar que a API não permite duplicar e-mails (se tentar cadastrar de novo, retorna 409).

3. **Login Student**
   - `POST /api/v1/Auth/login` → copiar token.

4. **Login Admin**
   - `POST /api/v1/Auth/login` → copiar token de Admin.

5. **Testar Autorização**
   - Com token de Student, tentar criar CareerPath → 403 Forbidden.
   - Com token de Admin, criar CareerPath → 201 Created.

6. **Criar Missão**
   - `POST /api/v1/Missions` autenticado como Admin → 201.

7. **Criar Progresso (Student)**
   - Logar como Student e chamar `POST /api/v1/Progress` → 201.

8. **Atualizar Status do Progresso**
   - `PUT /api/v1/Progress/{id}/status` com `"Concluida"` → 200, `CompletedAt` preenchido.

9. **Listar progresso do usuário logado**
   - `GET /api/v1/Progress` → mostra as missões e status do aluno.

Esse roteiro demonstra na prática:

- CRUD básico;
- Regras de negócio;
- JWT (login e uso de token);
- Autorização por perfil (403 quando Student tenta ação de Admin);
- Tratamento global de erros (mensagens claras e padronizadas).

---

## 📄 Informações Adicionais Importantes

- **Stateless**: não há controle de sessão em memória/servidor; toda autenticação é via token.
- **SOA / Reutilização**: serviços podem ser consumidos de portais, apps mobile, bots, etc., sem acoplamento à UI.
- **Padronização de Respostas**:
  - Sucesso:
    ```json
    {
      "success": true,
      "message": "Operação realizada com sucesso.",
      "data": { ... }
    }
    ```
  - Erro:
    ```json
    {
      "success": false,
      "message": "Descrição do erro.",
      "errors": { ... }
    }
    ```
- **Coleção Postman**:
  - O repositório inclui uma collection Postman com o fluxo completo de:
    - registro de usuários,
    - login,
    - criação de rota/missão,
    - progresso de aluno.

---

## ✅ Checklist de Atendimento aos Critérios da Disciplina

- ✅ **Criação de Entities, VO, Enums, Controllers, DTOs**
- ✅ **Padronização de respostas com Response Entity (`ApiResponse<T>`)**
- ✅ **Tratamento global de exceções (middleware → equivalente a Advice)**
- ✅ **Autenticação de usuário com JWT**
- ✅ **Autorização de requisições com perfis (Student, Mentor, Admin)**
- ✅ **Política STATELESS (nenhuma sessão de servidor, apenas tokens JWT)**
- ✅ **Casos de uso e regras de negócio implementados como serviços**
- ✅ **Organização modular em camadas e serviços reutilizáveis**

---

## 👋 Contato / Observações Finais

Este projeto integra a **Global Solution – Futuro do Trabalho**, servindo como núcleo de serviços para experiências mais amplas de carreira (como a ORBITA Career Platform).

Em caso de dúvidas sobre o código, arquitetura ou decisões de design, os autores estão disponíveis para esclarecimentos durante a apresentação/avaliação.
