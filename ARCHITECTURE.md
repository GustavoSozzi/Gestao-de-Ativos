# 🏗️ Arquitetura do Sistema

## Visão Geral

O Sistema de Gestão de Ativos segue os princípios da **Clean Architecture**, garantindo separação de responsabilidades, testabilidade e manutenibilidade.

---

## 📐 Diagrama de Camadas

```
┌─────────────────────────────────────────────────────────┐
│                    Presentation Layer                    │
│                   (Ativos.Api - Controllers)             │
│  - Recebe requisições HTTP                               │
│  - Valida entrada                                        │
│  - Retorna respostas HTTP                                │
└────────────────────┬────────────────────────────────────┘
                     │
                     ▼
┌─────────────────────────────────────────────────────────┐
│                   Application Layer                      │
│              (Ativos.Application - UseCases)             │
│  - Lógica de negócio                                     │
│  - Orquestração de operações                             │
│  - Validações de regras de negócio                       │
└────────────────────┬────────────────────────────────────┘
                     │
                     ▼
┌─────────────────────────────────────────────────────────┐
│                     Domain Layer                         │
│           (Ativos.Domain - Entities/Interfaces)          │
│  - Entidades de domínio                                  │
│  - Interfaces de repositórios                            │
│  - Regras de negócio core                                │
└────────────────────┬────────────────────────────────────┘
                     │
                     ▼
┌─────────────────────────────────────────────────────────┐
│                 Infrastructure Layer                     │
│        (Ativos.Infrastructure - Repositories/DB)         │
│  - Implementação de repositórios                         │
│  - Acesso ao banco de dados                              │
│  - Serviços externos                                     │
└─────────────────────────────────────────────────────────┘
```

---

## 📦 Estrutura de Projetos

### 1. Ativos.Api (Presentation)
**Responsabilidade:** Interface com o mundo externo

```
Ativos.Api/
├── Controllers/          # Endpoints da API
│   ├── UsuariosController.cs
│   ├── AtivosController.cs
│   ├── LicencasController.cs
│   └── ...
├── Middleware/          # Middlewares customizados
├── Filters/             # Filtros de ação
├── Program.cs           # Configuração da aplicação
└── appsettings.json     # Configurações
```

**Dependências:**
- Ativos.Application
- Ativos.Communication

### 2. Ativos.Application (Business Logic)
**Responsabilidade:** Casos de uso e lógica de aplicação

```
Ativos.Application/
├── UseCases/
│   ├── Register/        # Casos de uso de criação
│   │   ├── Usuarios/
│   │   ├── Ativos/
│   │   └── ...
│   ├── GetAll/          # Casos de uso de listagem
│   ├── GetById/         # Casos de uso de busca
│   ├── Update/          # Casos de uso de atualização
│   └── Delete/          # Casos de uso de exclusão
├── AutoMapper/          # Perfis de mapeamento
└── Validators/          # Validadores FluentValidation
```

**Dependências:**
- Ativos.Domain
- Ativos.Communication

### 3. Ativos.Domain (Core)
**Responsabilidade:** Entidades e regras de negócio

```
Ativos.Domain/
├── Entities/            # Entidades de domínio
│   ├── Usuario.cs
│   ├── Ativo.cs
│   ├── Licenca.cs
│   └── ...
├── Repositories/        # Interfaces de repositórios
│   ├── Usuarios/
│   ├── Ativos/
│   └── ...
├── Enums/              # Enumerações
│   ├── TipoLicenca.cs
│   ├── StatusChamado.cs
│   └── Roles.cs
└── IUnitOfWork.cs      # Interface Unit of Work
```

**Dependências:** Nenhuma (camada mais interna)

### 4. Ativos.Infrastructure (Data Access)
**Responsabilidade:** Implementação de acesso a dados

```
Ativos.Infrastructure/
├── DataAccess/
│   ├── AtivosDbContext.cs      # Contexto do EF Core
│   └── Repositories/           # Implementações
│       ├── UsuariosRepository.cs
│       ├── AtivosRepository.cs
│       └── ...
├── Migrations/                 # Migrations do EF Core
├── Security/                   # Criptografia e tokens
│   ├── Cryptography/
│   └── Tokens/
└── UnitOfWork.cs              # Implementação Unit of Work
```

**Dependências:**
- Ativos.Domain

### 5. Ativos.Communication (DTOs)
**Responsabilidade:** Contratos de comunicação

```
Ativos.Communication/
├── Requests/           # DTOs de entrada
│   ├── RequestUsuariosJson.cs
│   ├── RequestAtivosJson.cs
│   └── ...
├── Responses/          # DTOs de saída
│   ├── ResponseUsuarioJson.cs
│   ├── ResponseAtivoJson.cs
│   └── ...
└── Enums/             # Enums para comunicação
```

**Dependências:** Nenhuma

### 6. Ativos.Exception (Error Handling)
**Responsabilidade:** Tratamento de exceções

```
Ativos.Exception/
├── ExceptionsBase/     # Exceções customizadas
│   ├── NotFoundException.cs
│   ├── ValidationException.cs
│   └── ...
└── ResourceMessages.cs # Mensagens de erro
```

**Dependências:** Nenhuma

---

## 🔄 Fluxo de Requisição

### Exemplo: Criar um Usuário

```
1. Cliente HTTP
   │
   ▼
2. UsuariosController.RegisterUsuarios()
   │ - Recebe RequestUsuariosJson
   │ - Valida entrada básica
   │
   ▼
3. RegisterUsuariosUseCase.Execute()
   │ - Valida regras de negócio
   │ - Verifica se matrícula já existe
   │ - Criptografa senha
   │
   ▼
4. IUsuariosWriteOnlyRepository.Add()
   │ - Adiciona usuário ao contexto
   │
   ▼
5. IUnitOfWork.Commit()
   │ - Salva mudanças no banco
   │
   ▼
6. AutoMapper
   │ - Mapeia Usuario → ResponseUsuarioJson
   │
   ▼
7. Controller retorna 201 Created
```

---

## 🗄️ Modelo de Dados

### Entidades Principais

#### Usuario
```csharp
public class Usuario
{
    public long Id_usuario { get; set; }
    public string P_nome { get; set; }
    public string Sobrenome { get; set; }
    public long Matricula { get; set; }
    public string Departamento { get; set; }
    public string Cargo { get; set; }
    public Guid UserIdentifier { get; set; }
    public string Role { get; set; }
    public string Password { get; set; }
    
    // Navegação
    public ICollection<Ativo> Ativos { get; }
    public List<Licenca> licencas { get; }
}
```

#### Ativo
```csharp
public class Ativo
{
    public long Id_ativo { get; set; }
    public string Nome { get; set; }
    public string Modelo { get; set; }
    public string SerialNumber { get; set; }
    public long CodInventario { get; set; }
    public string Tipo { get; set; }
    
    // Chaves estrangeiras
    public long? id_usuario { get; set; }
    public long id_localizacao { get; set; }
    
    // Navegação
    public Usuario Usuario { get; set; }
    public Localizacao localizacao { get; set; }
    public List<Chamado> Chamados { get; }
}
```

#### Licenca
```csharp
public class Licenca
{
    public long Id_Licenca { get; set; }
    public TipoLicenca Tipo_Licenca { get; set; }
    public DateTime Data { get; set; }
    
    // Navegação N:N
    public List<Usuario> Usuarios { get; }
}
```

### Relacionamentos

```
Usuario 1───N Ativo
Usuario N───N Licenca (via LicencaUsuario)
Ativo N───1 Localizacao
Ativo 1───N Chamado
Ativo 1───N Contrato
```

---

## 🔐 Segurança

### Autenticação JWT

```csharp
// Geração do Token
var token = new JwtSecurityToken(
    issuer: _configuration["Jwt:Issuer"],
    audience: _configuration["Jwt:Audience"],
    claims: claims,
    expires: DateTime.UtcNow.AddMinutes(60),
    signingCredentials: credentials
);
```

### Autorização

```csharp
[Authorize]  // Requer autenticação
[Authorize(Roles = "ADMIN")]  // Requer role específica
```

### Criptografia de Senhas

```csharp
// BCrypt para hash de senhas
var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
var isValid = BCrypt.Net.BCrypt.Verify(password, hashedPassword);
```

---

## 🎯 Padrões de Design Utilizados

### 1. Repository Pattern
Abstração do acesso a dados

```csharp
public interface IUsuariosReadOnlyRepository
{
    Task<Usuario?> GetById(long id);
    Task<List<Usuario>> GetAll();
}
```

### 2. Unit of Work
Gerenciamento de transações

```csharp
public interface IUnitOfWork
{
    Task Commit();
}
```

### 3. Dependency Injection
Inversão de controle

```csharp
services.AddScoped<IRegisterUsuariosUseCase, RegisterUsuariosUseCase>();
```

### 4. DTO (Data Transfer Object)
Separação entre entidades e contratos

```csharp
public class RequestUsuariosJson { }
public class ResponseUsuarioJson { }
```

### 5. Mapper Pattern
Conversão entre objetos

```csharp
var response = _mapper.Map<ResponseUsuarioJson>(usuario);
```

---

## 🧪 Testabilidade

A arquitetura facilita testes em todas as camadas:

### Testes Unitários
```csharp
[Fact]
public async Task Execute_ValidUser_ReturnsSuccess()
{
    // Arrange
    var mockRepository = new Mock<IUsuariosRepository>();
    var useCase = new RegisterUsuariosUseCase(mockRepository.Object);
    
    // Act
    var result = await useCase.Execute(request);
    
    // Assert
    Assert.NotNull(result);
}
```

### Testes de Integração
```csharp
[Fact]
public async Task RegisterUsuario_ValidData_Returns201()
{
    // Arrange
    var client = _factory.CreateClient();
    
    // Act
    var response = await client.PostAsJsonAsync("/api/Usuarios/register", request);
    
    // Assert
    Assert.Equal(HttpStatusCode.Created, response.StatusCode);
}
```

---

## 📊 Performance

### Otimizações Implementadas

1. **Eager Loading**
```csharp
.Include(u => u.licencas)
.Include(a => a.Usuario)
```

2. **AsNoTracking para Leitura**
```csharp
.AsNoTracking()  // Quando não precisa rastrear mudanças
```

3. **Paginação** (a implementar)
```csharp
.Skip((page - 1) * pageSize)
.Take(pageSize)
```

4. **Índices no Banco**
```csharp
builder.HasIndex(u => u.Matricula).IsUnique();
```

---

## 🔄 Extensibilidade

### Adicionar Nova Entidade

1. Criar entidade em `Ativos.Domain/Entities`
2. Criar repositório em `Ativos.Domain/Repositories`
3. Implementar repositório em `Ativos.Infrastructure`
4. Criar DTOs em `Ativos.Communication`
5. Criar UseCases em `Ativos.Application`
6. Criar Controller em `Ativos.Api`
7. Registrar dependências em `DependencyInjectionExtension`

---

## 📚 Referências

- [Clean Architecture - Robert C. Martin](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html)
- [Entity Framework Core Documentation](https://docs.microsoft.com/ef/core/)
- [ASP.NET Core Documentation](https://docs.microsoft.com/aspnet/core/)

---

**Última atualização:** Dezembro 2024
