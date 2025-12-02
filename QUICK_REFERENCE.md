# ⚡ Guia de Referência Rápida

Comandos e informações essenciais para uso diário do Sistema de Gestão de Ativos.

---

## 🚀 Comandos Rápidos

### Iniciar o Sistema

```bash
# Backend
cd GestaoDeAtivosApi/src/Ativos.Api
dotnet run

# Frontend (em outro terminal)
cd Gestao-de-Ativos-Inpasa
npm run dev
```

### Parar o Sistema

```
Ctrl + C (em ambos os terminais)
```

---

## 🔗 URLs Importantes

| Serviço | URL Local | URL Produção |
|---------|-----------|--------------|
| Frontend | http://localhost:5173 | https://seudominio.com |
| Backend API | http://localhost:5234 | https://api.seudominio.com |
| Swagger (Dev) | http://localhost:5234/swagger | N/A |

---

## 🔑 Endpoints Principais

### Autenticação

```http
POST /api/Login
Body: { "matricula": 1000, "password": "senha" }
```

### Usuários

```http
GET    /api/Usuarios              # Listar todos
GET    /api/Usuarios/{id}         # Buscar por ID
POST   /api/Usuarios/register     # Cadastrar
PUT    /api/Usuarios/{id}         # Atualizar
DELETE /api/Usuarios/{id}         # Excluir
POST   /api/Usuarios/{id}/licencas  # Vincular licenças
GET    /api/Usuarios/{id}/licencas  # Listar licenças
```

### Ativos

```http
GET    /api/Ativos                # Listar todos
GET    /api/Ativos/{id}           # Buscar por ID
POST   /api/Ativos                # Cadastrar
PUT    /api/Ativos/{id}           # Atualizar
DELETE /api/Ativos/{id}           # Excluir
```

### Licenças

```http
GET    /api/Licencas              # Listar todas
POST   /api/Licencas              # Cadastrar
```

### Chamados

```http
GET    /api/Chamados              # Listar todos
POST   /api/Chamados              # Abrir chamado
PUT    /api/Chamados/{id}         # Atualizar
```

---

## 🔍 Filtros de Busca

### Usuários

```
?matricula=1000
?nome=João
?departamento=TI
?cargo=Analista
?role=ADMIN
```

### Ativos

```
?nome=Dell
?modelo=Latitude
?tipo=Notebook
?codInventario=1001
?cidade=São Paulo
?estado=SP
?matriculaUsuario=1000
?nomeUsuario=João
```

---

## 🗄️ Queries SQL Úteis

### Ver todos os usuários com licenças

```sql
SELECT u.P_nome, u.Sobrenome, l.Tipo_Licenca
FROM Usuario u
LEFT JOIN LicencaUsuario lu ON u.id_usuario = lu.UsuariosId_usuario
LEFT JOIN Licencas l ON lu.licencasId_Licenca = l.id_licenca;
```

### Ver ativos por usuário

```sql
SELECT u.P_nome, COUNT(a.id_ativo) as Total_Ativos
FROM Usuario u
LEFT JOIN Ativos a ON u.id_usuario = a.id_usuario
GROUP BY u.id_usuario;
```

### Chamados abertos

```sql
SELECT * FROM Chamados WHERE Status_Chamado = 0;
```

---

## 🔧 Comandos de Manutenção

### Backend

```bash
# Restaurar dependências
dotnet restore

# Compilar
dotnet build

# Executar testes
dotnet test

# Criar migration
dotnet ef migrations add NomeDaMigration

# Aplicar migrations
dotnet ef database update

# Reverter migration
dotnet ef database update PreviousMigration

# Limpar build
dotnet clean
```

### Frontend

```bash
# Instalar dependências
npm install

# Iniciar dev server
npm run dev

# Build de produção
npm run build

# Preview do build
npm run preview

# Limpar cache
npm cache clean --force
```

### Banco de Dados

```bash
# Conectar ao MySQL
mysql -u root -p

# Backup
mysqldump -u root -p projeto_banco > backup.sql

# Restaurar
mysql -u root -p projeto_banco < backup.sql

# Ver tabelas
SHOW TABLES;

# Ver estrutura
DESCRIBE Usuario;
```

---

## 🐛 Troubleshooting Rápido

### Backend não inicia

```bash
# Verificar porta em uso
lsof -i :5234  # Mac/Linux
netstat -ano | findstr :5234  # Windows

# Verificar logs
dotnet run --verbosity detailed
```

### Erro de conexão com banco

```bash
# Testar conexão
mysql -u root -p -h localhost

# Verificar serviço
sudo systemctl status mysql  # Linux
net start MySQL80  # Windows
```

### Frontend não carrega

```bash
# Limpar e reinstalar
rm -rf node_modules package-lock.json
npm install

# Verificar porta
lsof -i :5173  # Mac/Linux
```

### Erro de CORS

Verifique `Program.cs`:
```csharp
builder.Services.AddCors(options => {
    options.AddPolicy("AllowFrontend", policy => {
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});
```

---

## 📊 Tipos de Licenças

| ID | Nome | Categoria |
|----|------|-----------|
| 1 | Business Basic | Business |
| 2 | Business Standard | Business |
| 3 | Business Premium | Business |
| 4 | Apps for Business | Business |
| 10 | Enterprise E1 | Enterprise |
| 11 | Enterprise E3 | Enterprise |
| 12 | Enterprise E5 | Enterprise |
| 13 | Enterprise F3 | Enterprise |
| 14 | Apps for Enterprise | Enterprise |
| 20 | Education A1 | Education |
| 21 | Education A3 | Education |
| 22 | Education A5 | Education |
| 30 | Home and Business 2021 | Perpétuas |
| 31 | Professional Plus 2021 | Perpétuas |
| 32 | Standard 2019 | Perpétuas |
| 40 | Project Plan 3 | Serviços |
| 41 | Visio Plan 2 | Serviços |
| 42 | Exchange Online | Serviços |
| 43 | Power BI Pro | Serviços |

---

## 🔐 Roles e Permissões

| Role | Descrição | Permissões |
|------|-----------|------------|
| ADMIN | Administrador | Acesso total ao sistema |
| TEAM_MEMBER | Membro da equipe | Acesso limitado |

---

## 📝 Status de Chamados

| Valor | Status | Descrição |
|-------|--------|-----------|
| 0 | Aberto | Chamado recém criado |
| 1 | Em Andamento | Sendo trabalhado |
| 2 | Resolvido | Finalizado |

---

## 🔑 Variáveis de Ambiente

### Backend (appsettings.json)

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=projeto_banco;User=root;Password=senha;"
  },
  "Jwt": {
    "SecretKey": "chave-secreta-aqui",
    "Issuer": "GestaoAtivosApi",
    "Audience": "GestaoAtivosClient",
    "ExpirationMinutes": 60
  }
}
```

### Frontend (axios.js)

```javascript
const API_BASE_URL = 'http://localhost:5234/api';
```

---

## 📞 Contatos Úteis

| Área | Contato |
|------|---------|
| Suporte Técnico | suporte@empresa.com |
| Desenvolvimento | dev@empresa.com |
| Infraestrutura | infra@empresa.com |

---

## 🔗 Links Úteis

- [Documentação Completa](./README.md)
- [Guia de Instalação](./INSTALL.md)
- [Arquitetura](./ARCHITECTURE.md)
- [Deploy](./DEPLOYMENT.md)
- [Contribuir](./CONTRIBUTING.md)

---

## 💡 Dicas Rápidas

### Desenvolvimento

```bash
# Assistir mudanças no backend
dotnet watch run

# Hot reload no frontend (já habilitado por padrão)
npm run dev
```

### Produtividade

- Use Postman Collections para testar API
- Configure snippets no VS Code
- Use extensões: C# Dev Kit, ESLint, Prettier
- Atalhos: F5 (debug), Ctrl+Shift+B (build)

### Segurança

- Nunca commite `appsettings.Development.json`
- Use senhas fortes (mínimo 8 caracteres)
- Troque JWT SecretKey em produção
- Mantenha dependências atualizadas

---

**Última atualização:** Dezembro 2024
