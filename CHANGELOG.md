# 📝 Changelog

Todas as mudanças notáveis neste projeto serão documentadas neste arquivo.

O formato é baseado em [Keep a Changelog](https://keepachangelog.com/pt-BR/1.0.0/),
e este projeto adere ao [Semantic Versioning](https://semver.org/lang/pt-BR/).

---

## [1.0.0] - 2024-12-02

### 🎉 Lançamento Inicial

#### ✨ Adicionado

**Backend:**
- Sistema de autenticação JWT completo
- CRUD completo de Usuários
- CRUD completo de Ativos
- CRUD de Licenças Microsoft 365
- Sistema de Chamados técnicos
- Gestão de Contratos
- Controle de Localizações
- Vinculação N:N entre Usuários e Licenças
- Endpoint para listar licenças de um usuário
- Validação de dados com FluentValidation
- Tratamento de exceções customizado
- AutoMapper para mapeamento de objetos
- Repository Pattern e Unit of Work
- Clean Architecture com separação em camadas
- Migrations do Entity Framework Core
- Suporte a filtros avançados de busca

**Frontend:**
- Interface de login com autenticação
- Dashboard principal
- Página de gestão de usuários com filtros
- Página de gestão de ativos
- Modal para vincular licenças aos usuários
- Pré-seleção de licenças já atribuídas
- Organização de licenças por categoria
- Design responsivo
- Integração completa com API
- Tratamento de erros
- Loading states

**Banco de Dados:**
- Estrutura completa de tabelas
- Relacionamentos N:N configurados
- Índices para performance
- Scripts de inserção de dados iniciais

**Documentação:**
- README.md completo
- INSTALL.md com guia de instalação passo a passo
- ARCHITECTURE.md com detalhes da arquitetura
- DEPLOYMENT.md com guia de deploy
- API_ATIVOS_DOCUMENTATION.md
- Scripts SQL úteis
- Exemplos de configuração

#### 🔒 Segurança
- Criptografia de senhas com BCrypt
- Autenticação via JWT
- Autorização baseada em roles (ADMIN/TEAM_MEMBER)
- Validação de entrada em todos os endpoints
- Proteção contra SQL Injection via EF Core
- CORS configurado

#### 📊 Funcionalidades Principais

**Gestão de Usuários:**
- Cadastro com validação de matrícula única
- Edição de dados
- Exclusão de usuários
- Busca com múltiplos filtros
- Controle de permissões

**Gestão de Ativos:**
- Cadastro completo de equipamentos
- Vinculação com usuários
- Controle de localização
- Busca avançada
- Histórico de movimentações

**Gestão de Licenças:**
- 19 tipos de licenças Microsoft 365
- Vinculação múltipla com usuários
- Interface visual para seleção
- Prevenção de duplicação
- Categorização (Business, Enterprise, Education, etc.)

**Gestão de Chamados:**
- Abertura de chamados
- Vinculação com ativos
- Controle de status
- Registro de soluções

**Gestão de Contratos:**
- Cadastro de contratos
- Vinculação com ativos
- Controle de valores

---

## [Roadmap] - Próximas Versões

### 🔮 Planejado para v1.1.0

#### Backend
- [ ] Paginação em endpoints de listagem
- [ ] Filtros mais avançados
- [ ] Exportação de relatórios (PDF/Excel)
- [ ] Logs de auditoria
- [ ] Notificações por email
- [ ] API de dashboard com estatísticas

#### Frontend
- [ ] Dashboard com gráficos e estatísticas
- [ ] Página de relatórios
- [ ] Histórico de movimentações de ativos
- [ ] Notificações em tempo real
- [ ] Modo escuro
- [ ] Exportação de dados

#### Funcionalidades
- [ ] Sistema de notificações
- [ ] Histórico de alterações
- [ ] Relatórios gerenciais
- [ ] Gestão de fornecedores
- [ ] Controle de garantias
- [ ] Agendamento de manutenções

### 🚀 Planejado para v2.0.0

- [ ] Aplicativo mobile (React Native)
- [ ] QR Code para ativos
- [ ] Integração com Active Directory
- [ ] API para integração com outros sistemas
- [ ] Sistema de workflows
- [ ] Aprovações de chamados
- [ ] SLA para chamados
- [ ] Gestão de inventário
- [ ] Controle de estoque de peças

---

## Tipos de Mudanças

- `✨ Adicionado` - para novas funcionalidades
- `🔄 Modificado` - para mudanças em funcionalidades existentes
- `🗑️ Depreciado` - para funcionalidades que serão removidas
- `🐛 Corrigido` - para correção de bugs
- `🔒 Segurança` - para correções de vulnerabilidades
- `📚 Documentação` - para mudanças na documentação
- `⚡ Performance` - para melhorias de performance

---

## Versionamento

Este projeto usa [Semantic Versioning](https://semver.org/):

- **MAJOR** (X.0.0): Mudanças incompatíveis na API
- **MINOR** (0.X.0): Novas funcionalidades compatíveis
- **PATCH** (0.0.X): Correções de bugs compatíveis

---

**Última atualização:** Dezembro 2024
