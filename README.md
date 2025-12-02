# 🏢 Sistema de Gestão de Ativos - INPASA

Sistema completo para gerenciamento de ativos de TI, incluindo controle de equipamentos, usuários, licenças, chamados e contratos.

## 📋 Índice

- [Sobre o Projeto](#sobre-o-projeto)
- [Tecnologias Utilizadas](#tecnologias-utilizadas)
- [Arquitetura](#arquitetura)
- [Pré-requisitos](#pré-requisitos)
- [Instalação e Configuração](#instalação-e-configuração)
- [Executando o Projeto](#executando-o-projeto)
- [Estrutura do Banco de Dados](#estrutura-do-banco-de-dados)
- [Endpoints da API](#endpoints-da-api)
- [Funcionalidades](#funcionalidades)
- [Documentação Adicional](#documentação-adicional)

> 💡 **Dica:** Consulte o **[Índice Geral da Documentação](./DOCUMENTATION_INDEX.md)** para navegar por toda a documentação disponível.

---

## 🎯 Sobre o Projeto

O Sistema de Gestão de Ativos é uma aplicação web desenvolvida para gerenciar o ciclo de vida completo de ativos de TI em uma organização. O sistema permite:

- Cadastro e controle de ativos (notebooks, desktops, periféricos)
- Gerenciamento de usuários e suas permissões
- Controle de licenças de software (Microsoft 365, Office, etc.)
- Abertura e acompanhamento de chamados técnicos
- Gestão de contratos relacionados aos ativos
- Controle de localização física dos equipamentos

---

## 🚀 Tecnologias Utilizadas

### Backend (.NET 8)
- **ASP.NET Core 8.0** - Framework web
- **Entity Framework Core 8.0** - ORM para acesso ao banco de dados
- **MySQL** - Banco de dados relacional
- **AutoMapper** - Mapeamento de objetos
- **JWT (JSON Web Tokens)** - Autenticação e autorização
- **FluentValidation** - Validação de dados

### Frontend (React)
- **React 19** - Biblioteca JavaScript para UI
- **React Router DOM** - Roteamento
- **Axios** - Cliente HTTP
- **Vite** - Build tool e dev server
- **CSS Modules** - Estilização com escopo local

### Arquitetura
- **Clean Architecture** - Separação em camadas
- **Repository Pattern** - Abstração de acesso a dados
- **Unit of Work** - Gerenciamento de transações
- **Dependency Injection** - Inversão de controle

---

## 🏗️ Arquitetura

O projeto segue os princípios da Clean Architecture, dividido em camadas:

```
GestaoDeAtivosApi/
├── src/
│   ├── Ativos.Api/              # Camada de apresentação (Controllers)
│   ├── Ativos.Application/      # Casos de uso e lógica de aplicação
│   ├── Ativos.Communication/    # DTOs e contratos de comunicação
│   ├── Ativos.Domain/           # Entidades e interfaces de domínio
│   ├── Ativos.Infrastructure/   # Implementação de repositórios e DbContext
│   └── Ativos.Exception/        # Tratamento de exceções customizadas
```

---

## 📦 Pré-requisitos

Antes de começar, certifique-se de ter instalado:

### Backend
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) (versão 8.0 ou superior)
- [MySQL Server](https://dev.mysql.com/downloads/mysql/) (versão 8.0 ou superior)
- [MySQL Workbench](https://dev.mysql.com/downloads/workbench/) (opcional, para gerenciar o banco)

### Frontend
- [Node.js](https://nodejs.org/) (versão 18 ou superior)
- [npm](https://www.npmjs.com/) (geralmente vem com Node.js)

### Ferramentas Recomendadas
- [Visual Studio 2022](https://visualstudio.microsoft.com/) ou [Rider](https://www.jetbrains.com/rider/)
- [Visual Studio Code](https://code.visualstudio.com/)
- [Postman](https://www.postman.com/) ou [Insomnia](https://insomnia.rest/) (para testar a API)

---

## ⚙️ Instalação e Configuração

### 1. Clone o Repositório

```bash
git clone https://github.com/GustavoSozzi/Gestao-de-Ativos
cd GestaoDeAtivosApi
```

### 2. Configuração do Banco de Dados

#### 2.1. Criar o Banco de Dados

Abra o MySQL Workbench ou terminal MySQL e execute:

```sql
CREATE DATABASE projeto_banco CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;
```

#### 2.2. Configurar Connection String

Edite o arquivo `src/Ativos.Api/appsettings.Development.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=projeto_banco;User=root;Password=SUA_SENHA_AQUI;"
  },
  "Jwt": {
    "SecretKey": "sua-chave-secreta-super-segura-aqui-com-pelo-menos-32-caracteres",
    "Issuer": "GestaoAtivosApi",
    "Audience": "GestaoAtivosClient",
    "ExpirationMinutes": 60
  }
}
```

**⚠️ Importante:** Substitua `SUA_SENHA_AQUI` pela senha do seu MySQL.

#### 2.3. Executar Migrations

No diretório raiz do projeto backend:

```bash
cd src/Ativos.Api
dotnet ef database update
```

Isso criará todas as tabelas necessárias no banco de dados.

#### 2.4. Popular Dados Iniciais (Opcional)

Execute os scripts SQL para inserir dados de exemplo:

```bash
# Inserir licenças
mysql -u root -p projeto_banco < scripts/insert_licencas.sql

# Inserir usuários de teste (se disponível)
python gerar_usuarios.py
```

### 3. Configuração do Backend

#### 3.1. Restaurar Dependências

```bash
cd GestaoDeAtivosApi
dotnet restore
```

#### 3.2. Compilar o Projeto

```bash
dotnet build
```

### 4. Configuração do Frontend

#### 4.1. Instalar Dependências

```bash
cd ../Gestao-de-Ativos-Inpasa
npm install
```

#### 4.2. Configurar URL da API

Verifique se o arquivo `src/api/axios.js` está apontando para a URL correta:

```javascript
const API_BASE_URL = 'http://localhost:5234/api';
```

---

## ▶️ Executando o Projeto

### 1. Iniciar o Backend

No diretório `GestaoDeAtivosApi/src/Ativos.Api`:

```bash
dotnet run
```

A API estará disponível em:
- HTTP: `http://localhost:5234`
- HTTPS: `https://localhost:7234`

### 2. Iniciar o Frontend

Em outro terminal, no diretório `Gestao-de-Ativos-Inpasa`:

```bash
npm run dev
```

O frontend estará disponível em: `http://localhost:5173`

### 3. Acessar o Sistema

1. Abra o navegador em `http://localhost:5173`
2. Faça login com as credenciais padrão:
   - **Usuário:** (matrícula de um usuário cadastrado)
   - **Senha:** (senha definida no cadastro)

---

## 🗄️ Estrutura do Banco de Dados

### Principais Tabelas

#### Usuario
```sql
- id_usuario (PK)
- p_nome
- sobrenome
- matricula (UNIQUE)
- departamento
- cargo
- password (hash)
- role (ADMIN | TEAM_MEMBER)
- UserIdentifier (GUID)
```

#### Ativos
```sql
- id_ativo (PK)
- nome
- modelo
- serialNumber
- codInventario
- tipo
- id_usuario (FK)
- id_localizacao (FK)
```

#### Licencas
```sql
- id_licenca (PK)
- Tipo_Licenca (ENUM)
- Data
```

#### LicencaUsuario (Tabela N:N)
```sql
- UsuariosId_usuario (FK)
- licencasId_Licenca (FK)
```

#### Chamados
```sql
- id_chamado (PK)
- titulo
- descricao
- solucao
- Data_Abertura
- Status_Chamado (ENUM)
- id_Ativo (FK)
```

#### Contratos
```sql
- id_contrato (PK)
- tipo
- descricao
- valor
- Id_Ativo (FK)
```

#### Localizacao
```sql
- id_localizacao (PK)
- cidade
- estado
```

### Relacionamentos

- **Usuario** 1:N **Ativos** - Um usuário pode ter vários ativos
- **Usuario** N:N **Licencas** - Usuários podem ter múltiplas licenças
- **Ativo** 1:N **Chamados** - Um ativo pode ter vários chamados
- **Ativo** N:1 **Localizacao** - Vários ativos em uma localização
- **Ativo** 1:N **Contratos** - Um ativo pode ter vários contratos

---

## 🔌 Endpoints da API

### Autenticação

```http
POST /api/Login
Content-Type: application/json

{
  "matricula": 12345,
  "password": "senha123"
}
```

**Resposta:**
```json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "nome": "João Silva"
}
```

### Usuários

| Método | Endpoint | Descrição |
|--------|----------|-----------|
| POST | `/api/Usuarios/register` | Cadastrar novo usuário |
| GET | `/api/Usuarios` | Listar todos os usuários (com filtros) |
| GET | `/api/Usuarios/{id}` | Buscar usuário por ID |
| PUT | `/api/Usuarios/{id}` | Atualizar usuário |
| DELETE | `/api/Usuarios/{id}` | Excluir usuário |
| POST | `/api/Usuarios/{id}/licencas` | Vincular licenças ao usuário |
| GET | `/api/Usuarios/{id}/licencas` | Listar licenças do usuário |

### Ativos

| Método | Endpoint | Descrição |
|--------|----------|-----------|
| POST | `/api/Ativos` | Cadastrar novo ativo |
| GET | `/api/Ativos` | Listar todos os ativos (com filtros) |
| GET | `/api/Ativos/{id}` | Buscar ativo por ID |
| PUT | `/api/Ativos/{id}` | Atualizar ativo |
| DELETE | `/api/Ativos/{id}` | Excluir ativo |

### Licenças

| Método | Endpoint | Descrição |
|--------|----------|-----------|
| POST | `/api/Licencas` | Cadastrar nova licença |
| GET | `/api/Licencas` | Listar todas as licenças |

### Chamados

| Método | Endpoint | Descrição |
|--------|----------|-----------|
| POST | `/api/Chamados` | Abrir novo chamado |
| GET | `/api/Chamados` | Listar todos os chamados |
| PUT | `/api/Chamados/{id}` | Atualizar chamado |

### Contratos

| Método | Endpoint | Descrição |
|--------|----------|-----------|
| POST | `/api/Contratos` | Cadastrar novo contrato |

### Localização

| Método | Endpoint | Descrição |
|--------|----------|-----------|
| POST | `/api/Localizacao` | Cadastrar nova localização |
| GET | `/api/Localizacao` | Listar todas as localizações |

**⚠️ Nota:** Todos os endpoints (exceto `/api/Login`) requerem autenticação via token JWT no header:
```
Authorization: Bearer {seu-token-aqui}
```

---

## ✨ Funcionalidades

### 1. Gestão de Usuários
- ✅ Cadastro de usuários com validação
- ✅ Autenticação JWT
- ✅ Controle de permissões (ADMIN / TEAM_MEMBER)
- ✅ Filtros avançados de busca
- ✅ Edição e exclusão de usuários

### 2. Gestão de Ativos
- ✅ Cadastro completo de ativos
- ✅ Vinculação de ativos a usuários
- ✅ Controle de localização física
- ✅ Histórico de movimentações
- ✅ Busca por múltiplos critérios

### 3. Gestão de Licenças
- ✅ Cadastro de licenças Microsoft 365
- ✅ Vinculação N:N com usuários
- ✅ Interface visual para seleção de licenças
- ✅ Prevenção de duplicação
- ✅ Categorização por tipo (Business, Enterprise, Education)

### 4. Gestão de Chamados
- ✅ Abertura de chamados técnicos
- ✅ Vinculação com ativos
- ✅ Controle de status (Aberto, Em Andamento, Resolvido)
- ✅ Registro de soluções

### 5. Gestão de Contratos
- ✅ Cadastro de contratos
- ✅ Vinculação com ativos
- ✅ Controle de valores

---

## 📚 Documentação Adicional

### 📖 Documentação Técnica
- **[DOCUMENTATION_INDEX.md](./DOCUMENTATION_INDEX.md)** - 📚 Índice geral de toda a documentação
- **[QUICK_REFERENCE.md](./QUICK_REFERENCE.md)** - ⚡ Guia de referência rápida
- **[INSTALL.md](./INSTALL.md)** - Guia de instalação rápida passo a passo
- **[ARCHITECTURE.md](./ARCHITECTURE.md)** - Detalhes da arquitetura do sistema
- **[DEPLOYMENT.md](./DEPLOYMENT.md)** - Guia completo de deploy em produção
- **[API_ATIVOS_DOCUMENTATION.md](./API_ATIVOS_DOCUMENTATION.md)** - Documentação detalhada dos endpoints de Ativos

### 🤝 Contribuição e Desenvolvimento
- **[CONTRIBUTING.md](./CONTRIBUTING.md)** - Guia para contribuidores
- **[CHANGELOG.md](./CHANGELOG.md)** - Histórico de versões e mudanças

### 📊 Gestão
- **[EXECUTIVE_SUMMARY.md](./EXECUTIVE_SUMMARY.md)** - Resumo executivo do projeto
- **[VINCULACAO_USUARIO_ATIVO.md](./VINCULACAO_USUARIO_ATIVO.md)** - Guia de vinculação de usuários e ativos
- **[ATUALIZACAO_FORMULARIO.md](./ATUALIZACAO_FORMULARIO.md)** - Documentação sobre formulários

### 🗄️ Scripts e Utilitários
- **[scripts/insert_licencas.sql](./scripts/insert_licencas.sql)** - Script para inserir licenças no banco
- **[scripts/queries_uteis.sql](./scripts/queries_uteis.sql)** - Queries SQL úteis para consultas

---

## 🔒 Segurança

- Senhas armazenadas com hash BCrypt
- Autenticação via JWT com expiração configurável
- Validação de dados em todas as requisições
- Proteção contra SQL Injection via Entity Framework
- CORS configurado para ambiente de desenvolvimento

---

## 🧪 Testes

### Executar Testes Unitários

```bash
cd tests
dotnet test
```

---

## 🐛 Troubleshooting

### Erro de Conexão com o Banco de Dados

**Problema:** `Unable to connect to any of the specified MySQL hosts`

**Solução:**
1. Verifique se o MySQL está rodando: `sudo systemctl status mysql` (Linux) ou verifique nos serviços (Windows)
2. Confirme a senha no `appsettings.Development.json`
3. Teste a conexão: `mysql -u root -p`

### Erro de Migration

**Problema:** `The entity type 'X' requires a primary key to be defined`

**Solução:**
```bash
dotnet ef migrations remove
dotnet ef migrations add InitialCreate
dotnet ef database update
```

### Frontend não conecta com Backend

**Problema:** `Network Error` ou `CORS policy`

**Solução:**
1. Verifique se o backend está rodando em `http://localhost:5234`
2. Confirme a URL no arquivo `src/api/axios.js`
3. Verifique as configurações de CORS no `Program.cs`

---

## 👥 GUSTAVO SOZZI BOM
---

## 📄 Licença

Este projeto é proprietário e confidencial. Todos os direitos reservados.

---

**Última atualização:** Dezembro 2024
