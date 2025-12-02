# 🚀 Guia de Instalação Rápida

Este guia fornece instruções passo a passo para configurar e executar o Sistema de Gestão de Ativos.

## ⏱️ Tempo Estimado: 15-20 minutos

---

## 📋 Checklist de Pré-requisitos

Antes de começar, certifique-se de ter instalado:

- [ ] .NET 8 SDK
- [ ] MySQL Server 8.0+
- [ ] Node.js 18+
- [ ] Git

### Verificar Instalações

```bash
# Verificar .NET
dotnet --version
# Deve retornar: 8.0.x ou superior

# Verificar Node.js
node --version
# Deve retornar: v18.x.x ou superior

# Verificar MySQL
mysql --version
# Deve retornar: mysql Ver 8.0.x ou superior
```

---

## 🔧 Passo 1: Clonar o Repositório

```bash
git clone <url-do-repositorio>
cd GestaoDeAtivosApi
```

---

## 🗄️ Passo 2: Configurar o Banco de Dados

### 2.1. Criar o Banco de Dados

```bash
# Conectar ao MySQL
mysql -u root -p

# No prompt do MySQL, execute:
CREATE DATABASE projeto_banco CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;
EXIT;
```

### 2.2. Configurar Connection String

Edite o arquivo `src/Ativos.Api/appsettings.Development.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=projeto_banco;User=root;Password=SUA_SENHA;"
  },
  "Jwt": {
    "SecretKey": "MinhaChaveSuperSecretaComPeloMenos32Caracteres123456",
    "Issuer": "GestaoAtivosApi",
    "Audience": "GestaoAtivosClient",
    "ExpirationMinutes": 60
  }
}
```

**⚠️ IMPORTANTE:** Substitua `SUA_SENHA` pela sua senha do MySQL.

### 2.3. Executar Migrations

```bash
cd src/Ativos.Api
dotnet ef database update
```

**Saída esperada:**
```
Build started...
Build succeeded.
Applying migration '20251102140157_InitialMigration'.
Done.
```

### 2.4. Inserir Dados Iniciais

```bash
# Voltar para a raiz do projeto
cd ../..

# Inserir licenças
mysql -u root -p projeto_banco < scripts/insert_licencas.sql
```

---

## ⚙️ Passo 3: Configurar o Backend

### 3.1. Restaurar Dependências

```bash
dotnet restore
```

### 3.2. Compilar

```bash
dotnet build
```

**Saída esperada:**
```
Build succeeded.
    0 Warning(s)
    0 Error(s)
```

---

## 🎨 Passo 4: Configurar o Frontend

### 4.1. Navegar para o diretório do frontend

```bash
cd ../Gestao-de-Ativos-Inpasa
```

### 4.2. Instalar Dependências

```bash
npm install
```

**Aguarde a instalação (pode levar alguns minutos).**

### 4.3. Verificar Configuração da API

Abra `src/api/axios.js` e confirme:

```javascript
const API_BASE_URL = 'http://localhost:5234/api';
```

---

## ▶️ Passo 5: Executar o Sistema

### 5.1. Iniciar o Backend

Em um terminal, no diretório `GestaoDeAtivosApi/src/Ativos.Api`:

```bash
cd ../../GestaoDeAtivosApi/src/Ativos.Api
dotnet run
```

**Aguarde até ver:**
```
Now listening on: http://localhost:5234
Now listening on: https://localhost:7234
Application started. Press Ctrl+C to shut down.
```

### 5.2. Iniciar o Frontend

Em **outro terminal**, no diretório `Gestao-de-Ativos-Inpasa`:

```bash
cd ../Gestao-de-Ativos-Inpasa
npm run dev
```

**Aguarde até ver:**
```
  VITE v5.x.x  ready in xxx ms

  ➜  Local:   http://localhost:5173/
  ➜  Network: use --host to expose
```

---

## 🎉 Passo 6: Acessar o Sistema

1. Abra seu navegador
2. Acesse: `http://localhost:5173`
3. Você verá a tela de login

---

## 👤 Passo 7: Criar Primeiro Usuário

### Opção A: Via API (Postman/Insomnia)

```http
POST http://localhost:5234/api/Usuarios/register
Content-Type: application/json

{
  "p_nome": "Admin",
  "sobrenome": "Sistema",
  "matricula": 1000,
  "departamento": "TI",
  "cargo": "Administrador",
  "password": "admin123"
}
```

### Opção B: Via MySQL

```sql
USE projeto_banco;

INSERT INTO Usuario (P_nome, Sobrenome, Matricula, Departamento, Cargo, Password, Role, UserIdentifier)
VALUES (
  'Admin',
  'Sistema',
  1000,
  'TI',
  'Administrador',
  '$2a$11$YourHashedPasswordHere',  -- Use BCrypt para gerar o hash
  'ADMIN',
  UUID()
);
```

### Fazer Login

- **Matrícula:** 1000
- **Senha:** admin123

---

## ✅ Verificação da Instalação

### Checklist Final

- [ ] Backend rodando em `http://localhost:5234`
- [ ] Frontend rodando em `http://localhost:5173`
- [ ] Consegue acessar a tela de login
- [ ] Consegue fazer login com sucesso
- [ ] Consegue navegar pelas páginas do sistema

---

## 🐛 Problemas Comuns

### Erro: "Unable to connect to MySQL"

**Solução:**
```bash
# Verificar se MySQL está rodando
sudo systemctl status mysql  # Linux
# ou
net start MySQL80  # Windows

# Testar conexão
mysql -u root -p
```

### Erro: "Port 5234 already in use"

**Solução:**
```bash
# Encontrar processo usando a porta
lsof -i :5234  # Mac/Linux
netstat -ano | findstr :5234  # Windows

# Matar o processo ou mudar a porta em launchSettings.json
```

### Erro: "npm ERR! code ENOENT"

**Solução:**
```bash
# Limpar cache do npm
npm cache clean --force

# Reinstalar dependências
rm -rf node_modules package-lock.json
npm install
```

### Erro: "Migration already applied"

**Solução:**
```bash
# Resetar banco de dados
dotnet ef database drop
dotnet ef database update
```

---

## 📞 Precisa de Ajuda?

Se encontrar problemas:

1. Verifique os logs do backend no terminal
2. Verifique o console do navegador (F12)
3. Consulte o [README.md](./README.md) completo
4. Entre em contato com o suporte

---

## 🎯 Próximos Passos

Após a instalação bem-sucedida:

1. ✅ Criar usuários adicionais
2. ✅ Cadastrar localizações
3. ✅ Cadastrar ativos
4. ✅ Vincular licenças aos usuários
5. ✅ Explorar todas as funcionalidades

---

**Instalação concluída com sucesso! 🎉**

Agora você está pronto para usar o Sistema de Gestão de Ativos.
