# 📁 Estrutura do Projeto

Visão detalhada da organização de arquivos e pastas do Sistema de Gestão de Ativos.

---

## 🌳 Árvore de Diretórios

```
GestaoDeAtivosApi/
│
├── 📄 README.md                          # Documentação principal
├── 📄 INSTALL.md                         # Guia de instalação
├── 📄 ARCHITECTURE.md                    # Arquitetura do sistema
├── 📄 DEPLOYMENT.md                      # Guia de deploy
├── 📄 CONTRIBUTING.md                    # Guia para contribuidores
├── 📄 CHANGELOG.md                       # Histórico de versões
├── 📄 EXECUTIVE_SUMMARY.md               # Resumo executivo
├── 📄 QUICK_REFERENCE.md                 # Referência rápida
├── 📄 DOCUMENTATION_INDEX.md             # Índice da documentação
├── 📄 PROJECT_STRUCTURE.md               # Este arquivo
├── 📄 API_ATIVOS_DOCUMENTATION.md        # Doc da API de Ativos
├── 📄 VINCULACAO_USUARIO_ATIVO.md        # Guia de vinculação
├── 📄 ATUALIZACAO_FORMULARIO.md          # Doc de formulários
├── 📄 .gitignore                         # Arquivos ignorados pelo Git
├── 📄 GestaoDeAtivosApi.sln              # Solution do Visual Studio
├── 🐍 gerar_usuarios.py                  # Script Python para gerar usuários
│
├── 📁 scripts/                           # Scripts SQL e utilitários
│   ├── insert_licencas.sql              # Inserir licenças iniciais
│   └── queries_uteis.sql                # Queries úteis
│
├── 📁 src/                               # Código fonte do backend
│   │
│   ├── 📁 Ativos.Api/                   # Camada de Apresentação
│   │   ├── 📁 Controllers/              # Controladores da API
│   │   │   ├── AtivosController.cs
│   │   │   ├── UsuariosController.cs
│   │   │   ├── LicencasController.cs
│   │   │   ├── ChamadosController.cs
│   │   │   ├── ContratosController.cs
│   │   │   ├── LocalizacaoController.cs
│   │   │   └── LoginController.cs
│   │   │
│   │   ├── 📁 Filters/                  # Filtros de ação
│   │   ├── 📁 Middleware/               # Middlewares customizados
│   │   ├── 📁 Properties/               # Propriedades do projeto
│   │   │
│   │   ├── 📄 Program.cs                # Ponto de entrada da aplicação
│   │   ├── 📄 Ativos.Api.csproj         # Arquivo de projeto
│   │   ├── 📄 appsettings.json          # Configurações gerais
│   │   ├── 📄 appsettings.Development.json  # Config de desenvolvimento
│   │   └── 📄 appsettings.Development.example.json  # Exemplo de config
│   │
│   ├── 📁 Ativos.Application/           # Camada de Aplicação
│   │   ├── 📁 UseCases/                 # Casos de uso
│   │   │   ├── 📁 Register/             # Casos de uso de criação
│   │   │   │   ├── 📁 Usuarios/
│   │   │   │   │   ├── RegisterUsuariosUseCase.cs
│   │   │   │   │   ├── IRegisterUsuariosUseCase.cs
│   │   │   │   │   └── RegisterUsuariosLicencasUseCase.cs
│   │   │   │   ├── 📁 Ativos/
│   │   │   │   ├── 📁 Licencas/
│   │   │   │   ├── 📁 Chamados/
│   │   │   │   ├── 📁 Contratos/
│   │   │   │   └── 📁 Localizacao/
│   │   │   │
│   │   │   ├── 📁 GetAll/               # Casos de uso de listagem
│   │   │   │   ├── 📁 Usuarios/
│   │   │   │   ├── 📁 Ativos/
│   │   │   │   ├── 📁 Licencas/
│   │   │   │   ├── 📁 Chamados/
│   │   │   │   └── 📁 Localizacao/
│   │   │   │
│   │   │   ├── 📁 GetById/              # Casos de uso de busca
│   │   │   │   ├── GetUsuarioByIdUseCase.cs
│   │   │   │   ├── GetUsuarioLicencasUseCase.cs
│   │   │   │   ├── GetAtivoByIdUseCase.cs
│   │   │   │   └── ...
│   │   │   │
│   │   │   ├── 📁 Update/               # Casos de uso de atualização
│   │   │   │   ├── 📁 Usuarios/
│   │   │   │   ├── 📁 Ativos/
│   │   │   │   └── 📁 Chamados/
│   │   │   │
│   │   │   ├── 📁 Delete/               # Casos de uso de exclusão
│   │   │   │   ├── 📁 Usuarios/
│   │   │   │   └── 📁 Ativos/
│   │   │   │
│   │   │   ├── 📁 Login/                # Autenticação
│   │   │   │   └── 📁 DoLogin/
│   │   │   │
│   │   │   └── 📄 *Validator.cs         # Validadores
│   │   │
│   │   ├── 📁 AutoMapper/               # Perfis de mapeamento
│   │   │   └── AutoMapping.cs
│   │   │
│   │   ├── 📄 DependencyInjectionExtension.cs  # Injeção de dependência
│   │   └── 📄 Ativos.Application.csproj
│   │
│   ├── 📁 Ativos.Communication/         # Camada de Comunicação (DTOs)
│   │   ├── 📁 Requests/                 # DTOs de entrada
│   │   │   ├── RequestUsuariosJson.cs
│   │   │   ├── RequestAtivosJson.cs
│   │   │   ├── RequestLicencasJson.cs
│   │   │   ├── RequestVincularLicencaJson.cs
│   │   │   ├── RequestChamadosJson.cs
│   │   │   ├── RequestContratosJson.cs
│   │   │   ├── RequestLocalizacaoJson.cs
│   │   │   └── RequestLoginJson.cs
│   │   │
│   │   ├── 📁 Responses/                # DTOs de saída
│   │   │   ├── 📁 Usuarios/
│   │   │   ├── 📁 Ativos/
│   │   │   ├── 📁 Register/
│   │   │   └── ResponseErrorJson.cs
│   │   │
│   │   ├── 📁 Enums/                    # Enums para comunicação
│   │   └── 📄 Ativos.Communication.csproj
│   │
│   ├── 📁 Ativos.Domain/                # Camada de Domínio
│   │   ├── 📁 Entities/                 # Entidades de domínio
│   │   │   ├── Usuario.cs
│   │   │   ├── Ativo.cs
│   │   │   ├── Licenca.cs
│   │   │   ├── Chamado.cs
│   │   │   ├── Contrato.cs
│   │   │   └── Localizacao.cs
│   │   │
│   │   ├── 📁 Repositories/             # Interfaces de repositórios
│   │   │   ├── 📁 Usuarios/
│   │   │   │   ├── IUsuariosReadOnlyRepository.cs
│   │   │   │   ├── IUsuariosWriteOnlyRepository.cs
│   │   │   │   └── IUsuariosUpdateOnlyRepository.cs
│   │   │   ├── 📁 Ativos/
│   │   │   ├── 📁 Licencas/
│   │   │   ├── 📁 Chamados/
│   │   │   ├── 📁 Contratos/
│   │   │   └── 📁 Localizacao/
│   │   │
│   │   ├── 📁 Enums/                    # Enumerações de domínio
│   │   │   ├── TipoLicenca.cs
│   │   │   ├── StatusChamado.cs
│   │   │   └── Roles.cs
│   │   │
│   │   ├── 📁 Security/                 # Interfaces de segurança
│   │   ├── 📄 IUnitOfWork.cs            # Interface Unit of Work
│   │   └── 📄 Ativos.Domain.csproj
│   │
│   ├── 📁 Ativos.Infrastructure/        # Camada de Infraestrutura
│   │   ├── 📁 DataAccess/               # Acesso a dados
│   │   │   ├── 📄 AtivosDbContext.cs    # Contexto do EF Core
│   │   │   │
│   │   │   └── 📁 Repositories/         # Implementações
│   │   │       ├── UsuariosRepository.cs
│   │   │       ├── AtivosRepository.cs
│   │   │       ├── LicencasRepository.cs
│   │   │       ├── ChamadosRepository.cs
│   │   │       ├── ContratosRepository.cs
│   │   │       └── LocalizacaoRepository.cs
│   │   │
│   │   ├── 📁 Migrations/               # Migrations do EF Core
│   │   │   ├── 20251102140157_InitialMigration.cs
│   │   │   ├── 20251102214258_CreatedTableLocalizacao.cs
│   │   │   ├── 20251130143319_UpdateLicencas.cs
│   │   │   └── AtivosDbContextModelSnapshot.cs
│   │   │
│   │   ├── 📁 Security/                 # Implementações de segurança
│   │   │   ├── 📁 Cryptography/         # Criptografia
│   │   │   └── 📁 Tokens/               # Geração de tokens JWT
│   │   │
│   │   ├── 📄 UnitOfWork.cs             # Implementação Unit of Work
│   │   ├── 📄 DependencyInjectionExtension.cs
│   │   └── 📄 Ativos.Infrastructure.csproj
│   │
│   └── 📁 Ativos.Exception/             # Camada de Exceções
│       ├── 📁 ExceptionsBase/           # Exceções customizadas
│       │   ├── NotFoundException.cs
│       │   ├── ValidationException.cs
│       │   └── ErrorOnValidationException.cs
│       │
│       ├── 📄 ResourceMessages.cs       # Mensagens de erro
│       └── 📄 Ativos.Exception.csproj
│
└── 📁 tests/                            # Testes (a implementar)
    └── (vazio)
```

---

## 📊 Estatísticas do Projeto

### Backend (.NET)

| Camada | Arquivos | Linhas de Código (aprox.) |
|--------|----------|---------------------------|
| Api | 15 | 800 |
| Application | 45 | 2,500 |
| Domain | 25 | 1,200 |
| Infrastructure | 30 | 1,800 |
| Communication | 20 | 600 |
| Exception | 5 | 200 |
| **Total** | **140** | **~7,100** |

### Frontend (React)

```
Gestao-de-Ativos-Inpasa/
│
├── 📄 package.json                      # Dependências do projeto
├── 📄 vite.config.js                    # Configuração do Vite
├── 📄 index.html                        # HTML principal
├── 📄 README.md                         # Documentação do frontend
│
├── 📁 src/                              # Código fonte
│   ├── 📄 main.jsx                      # Ponto de entrada
│   ├── 📄 App.jsx                       # Componente principal
│   │
│   └── 📁 api/                          # Configuração da API
│       ├── axios.js                     # Cliente HTTP
│       └── README.md
│
├── 📁 Components/                       # Componentes React
│   ├── 📁 Header/                       # Cabeçalho
│   │   ├── Header.jsx
│   │   └── Header.module.css
│   │
│   ├── 📁 Sidebar/                      # Menu lateral
│   │   ├── Sidebar.jsx
│   │   └── Sidebar.module.css
│   │
│   ├── 📁 Layout/                       # Layout principal
│   │   ├── Layout.jsx
│   │   └── Layout.module.css
│   │
│   └── 📁 Pages/                        # Páginas
│       ├── 📁 Login/
│       │   ├── LoginPage.jsx
│       │   └── LoginPage.module.css
│       │
│       ├── 📁 Usuarios/
│       │   ├── UsuariosPage.jsx
│       │   ├── UsuariosPage.module.css
│       │   ├── UsuariosList.jsx
│       │   ├── UsuariosList.module.css
│       │   ├── UsuariosFilter.jsx
│       │   ├── UsuariosFilter.module.css
│       │   ├── UsuarioForm.jsx
│       │   ├── UsuarioForm.module.css
│       │   ├── VincularLicencasModal.jsx
│       │   └── VincularLicencasModal.module.css
│       │
│       ├── 📁 Ativos/
│       │   └── ...
│       │
│       └── 📁 Chamados/
│           └── ...
│
├── 📁 Hooks/                            # Custom Hooks
│   └── LayoutContext.jsx
│
├── 📁 Helper/                           # Funções auxiliares
│
└── 📁 assets/                           # Recursos estáticos
    └── (imagens, ícones, etc.)
```

| Categoria | Arquivos | Linhas de Código (aprox.) |
|-----------|----------|---------------------------|
| Componentes | 25 | 2,000 |
| Páginas | 15 | 1,500 |
| Hooks | 3 | 150 |
| API | 2 | 100 |
| **Total** | **45** | **~3,750** |

---

## 📝 Documentação

| Documento | Linhas | Páginas (aprox.) |
|-----------|--------|------------------|
| README.md | 450 | 12 |
| INSTALL.md | 350 | 10 |
| ARCHITECTURE.md | 600 | 18 |
| DEPLOYMENT.md | 700 | 22 |
| CONTRIBUTING.md | 500 | 15 |
| CHANGELOG.md | 200 | 6 |
| EXECUTIVE_SUMMARY.md | 550 | 16 |
| QUICK_REFERENCE.md | 400 | 12 |
| DOCUMENTATION_INDEX.md | 350 | 10 |
| PROJECT_STRUCTURE.md | 300 | 9 |
| Outros | 300 | 10 |
| **Total** | **~4,700** | **~140** |

---

## 🗄️ Banco de Dados

### Tabelas

| Tabela | Colunas | Relacionamentos |
|--------|---------|-----------------|
| Usuario | 9 | 1:N Ativos, N:N Licencas |
| Ativos | 8 | N:1 Usuario, N:1 Localizacao, 1:N Chamados |
| Licencas | 3 | N:N Usuario |
| LicencaUsuario | 2 | Tabela intermediária |
| Chamados | 7 | N:1 Ativo |
| Contratos | 5 | N:1 Ativo |
| Localizacao | 3 | 1:N Ativos |

### Scripts

- `insert_licencas.sql` - 19 licenças
- `queries_uteis.sql` - 25+ queries úteis

---

## 📦 Dependências Principais

### Backend

```xml
<PackageReference Include="Microsoft.EntityFrameworkCore" Version="8.0.21" />
<PackageReference Include="Pomelo.EntityFrameworkCore.MySql" Version="8.0.2" />
<PackageReference Include="AutoMapper" Version="13.0.1" />
<PackageReference Include="BCrypt.Net-Next" Version="4.0.3" />
<PackageReference Include="System.IdentityModel.Tokens.Jwt" Version="8.2.1" />
```

### Frontend

```json
{
  "react": "^19.1.1",
  "react-dom": "^19.1.1",
  "react-router-dom": "^6.30.1",
  "axios": "^1.13.2",
  "react-icons": "^5.5.0"
}
```

---

## 🎯 Convenções de Nomenclatura

### Backend

- **Namespaces**: `Ativos.{Camada}.{Funcionalidade}`
- **Classes**: `PascalCase`
- **Interfaces**: `I{Nome}` (ex: `IUsuariosRepository`)
- **Métodos**: `PascalCase`
- **Variáveis**: `camelCase`
- **Constantes**: `PascalCase`

### Frontend

- **Componentes**: `PascalCase` (ex: `UsuarioForm.jsx`)
- **Funções**: `camelCase` (ex: `handleSubmit`)
- **CSS Modules**: `{Component}.module.css`
- **Constantes**: `UPPER_SNAKE_CASE`

### Banco de Dados

- **Tabelas**: `PascalCase` (ex: `Usuario`, `Ativos`)
- **Colunas**: `Snake_Case` com primeira letra maiúscula (ex: `Id_usuario`, `P_nome`)
- **Chaves primárias**: `id_{tabela}` (ex: `id_usuario`)
- **Chaves estrangeiras**: `id_{tabela_referenciada}` (ex: `id_usuario`)

---

## 🔍 Localização de Funcionalidades

### Adicionar Novo Endpoint

1. **Controller**: `src/Ativos.Api/Controllers/`
2. **UseCase**: `src/Ativos.Application/UseCases/`
3. **Request/Response**: `src/Ativos.Communication/`
4. **Registrar DI**: `src/Ativos.Application/DependencyInjectionExtension.cs`

### Adicionar Nova Entidade

1. **Entidade**: `src/Ativos.Domain/Entities/`
2. **Repository Interface**: `src/Ativos.Domain/Repositories/`
3. **Repository Impl**: `src/Ativos.Infrastructure/DataAccess/Repositories/`
4. **DbContext**: Adicionar `DbSet` em `AtivosDbContext.cs`
5. **Migration**: `dotnet ef migrations add NomeDaMigration`

### Adicionar Nova Página (Frontend)

1. **Componente**: `Components/Pages/{Nome}/`
2. **Rota**: Adicionar em `App.jsx`
3. **Menu**: Adicionar em `Sidebar.jsx`

---

## 📈 Crescimento do Projeto

### Versão 1.0.0 (Atual)
- 140 arquivos backend
- 45 arquivos frontend
- 12 documentos
- 7 tabelas no banco
- ~10,850 linhas de código

### Projeção v2.0.0
- ~200 arquivos backend
- ~70 arquivos frontend
- ~15 documentos
- ~12 tabelas no banco
- ~18,000 linhas de código

---

**Última atualização:** Dezembro 2024
