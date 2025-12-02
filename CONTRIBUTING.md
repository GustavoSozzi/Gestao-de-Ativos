# 🤝 Guia de Contribuição

Obrigado por considerar contribuir com o Sistema de Gestão de Ativos! Este documento fornece diretrizes para contribuir com o projeto.

---

## 📋 Índice

- [Código de Conduta](#código-de-conduta)
- [Como Contribuir](#como-contribuir)
- [Padrões de Código](#padrões-de-código)
- [Processo de Pull Request](#processo-de-pull-request)
- [Reportar Bugs](#reportar-bugs)
- [Sugerir Melhorias](#sugerir-melhorias)

---

## 📜 Código de Conduta

Este projeto adere a um código de conduta. Ao participar, espera-se que você mantenha este código.

### Nossos Padrões

- Usar linguagem acolhedora e inclusiva
- Respeitar pontos de vista e experiências diferentes
- Aceitar críticas construtivas
- Focar no que é melhor para a comunidade
- Mostrar empatia com outros membros

---

## 🚀 Como Contribuir

### 1. Fork o Projeto

```bash
# Clone seu fork
git clone https://github.com/seu-usuario/GestaoDeAtivosApi.git
cd GestaoDeAtivosApi

# Adicione o repositório original como upstream
git remote add upstream https://github.com/original/GestaoDeAtivosApi.git
```

### 2. Crie uma Branch

```bash
# Atualize sua main
git checkout main
git pull upstream main

# Crie uma branch para sua feature/fix
git checkout -b feature/nome-da-feature
# ou
git checkout -b fix/nome-do-bug
```

### 3. Faça suas Alterações

- Escreva código limpo e bem documentado
- Siga os padrões de código do projeto
- Adicione testes quando aplicável
- Atualize a documentação se necessário

### 4. Commit suas Mudanças

```bash
git add .
git commit -m "feat: adiciona nova funcionalidade X"
```

### 5. Push para seu Fork

```bash
git push origin feature/nome-da-feature
```

### 6. Abra um Pull Request

- Vá para o repositório original no GitHub
- Clique em "New Pull Request"
- Selecione sua branch
- Preencha o template de PR

---

## 💻 Padrões de Código

### Backend (C#)

#### Convenções de Nomenclatura

```csharp
// Classes: PascalCase
public class UsuarioRepository { }

// Interfaces: I + PascalCase
public interface IUsuarioRepository { }

// Métodos: PascalCase
public async Task<Usuario> GetById(long id) { }

// Variáveis locais: camelCase
var usuarioAtual = await GetById(1);

// Constantes: PascalCase
public const string DefaultRole = "TEAM_MEMBER";

// Propriedades: PascalCase
public string Nome { get; set; }
```

#### Estrutura de Classes

```csharp
public class ExemploClasse
{
    // 1. Campos privados
    private readonly IRepository _repository;
    
    // 2. Construtor
    public ExemploClasse(IRepository repository)
    {
        _repository = repository;
    }
    
    // 3. Propriedades públicas
    public string Nome { get; set; }
    
    // 4. Métodos públicos
    public async Task<Result> Execute()
    {
        // implementação
    }
    
    // 5. Métodos privados
    private void ValidateData()
    {
        // implementação
    }
}
```

#### Async/Await

```csharp
// ✅ Correto
public async Task<Usuario> GetUsuarioAsync(long id)
{
    return await _repository.GetByIdAsync(id);
}

// ❌ Incorreto
public Usuario GetUsuario(long id)
{
    return _repository.GetByIdAsync(id).Result; // Evite .Result
}
```

#### Tratamento de Exceções

```csharp
// ✅ Correto
public async Task<Usuario> GetUsuario(long id)
{
    var usuario = await _repository.GetById(id);
    
    if (usuario is null)
        throw new NotFoundException("Usuário não encontrado");
    
    return usuario;
}

// ❌ Incorreto
public async Task<Usuario> GetUsuario(long id)
{
    try
    {
        return await _repository.GetById(id);
    }
    catch (Exception ex)
    {
        // Não engolir exceções sem tratamento
        return null;
    }
}
```

### Frontend (React)

#### Componentes Funcionais

```javascript
// ✅ Correto
const UsuarioForm = ({ onSubmit, onCancel, usuarioData }) => {
  const [formData, setFormData] = React.useState({});
  
  const handleSubmit = (e) => {
    e.preventDefault();
    onSubmit(formData);
  };
  
  return (
    <form onSubmit={handleSubmit}>
      {/* JSX */}
    </form>
  );
};

export default UsuarioForm;
```

#### Hooks

```javascript
// ✅ Correto - Hooks no topo do componente
const MeuComponente = () => {
  const [data, setData] = React.useState([]);
  const [loading, setLoading] = React.useState(false);
  
  React.useEffect(() => {
    fetchData();
  }, []);
  
  // resto do componente
};
```

#### Nomenclatura

```javascript
// Componentes: PascalCase
const UsuariosList = () => { };

// Funções: camelCase
const handleSubmit = () => { };
const fetchUsuarios = async () => { };

// Constantes: UPPER_SNAKE_CASE
const API_BASE_URL = 'http://localhost:5234';

// CSS Modules
import styles from './Component.module.css';
<div className={styles.container}>
```

---

## 🔄 Processo de Pull Request

### Checklist antes de Submeter

- [ ] Código compila sem erros
- [ ] Testes passam (se aplicável)
- [ ] Código segue os padrões do projeto
- [ ] Documentação atualizada
- [ ] Commit messages seguem o padrão
- [ ] Branch está atualizada com main

### Template de Pull Request

```markdown
## Descrição
Breve descrição das mudanças

## Tipo de Mudança
- [ ] Bug fix
- [ ] Nova funcionalidade
- [ ] Breaking change
- [ ] Documentação

## Como Testar
1. Passo 1
2. Passo 2
3. Passo 3

## Screenshots (se aplicável)
Cole aqui

## Checklist
- [ ] Código testado localmente
- [ ] Documentação atualizada
- [ ] Sem warnings de compilação
```

### Padrão de Commit Messages

Seguimos o [Conventional Commits](https://www.conventionalcommits.org/):

```
tipo(escopo): descrição curta

Descrição mais detalhada (opcional)

Closes #123
```

**Tipos:**
- `feat`: Nova funcionalidade
- `fix`: Correção de bug
- `docs`: Documentação
- `style`: Formatação (não afeta código)
- `refactor`: Refatoração
- `test`: Testes
- `chore`: Manutenção

**Exemplos:**
```bash
feat(usuarios): adiciona filtro por departamento
fix(ativos): corrige bug na listagem de ativos
docs(readme): atualiza instruções de instalação
refactor(repositories): simplifica query de usuários
```

---

## 🐛 Reportar Bugs

### Antes de Reportar

1. Verifique se o bug já foi reportado
2. Certifique-se de estar usando a versão mais recente
3. Tente reproduzir o bug

### Template de Bug Report

```markdown
**Descrição do Bug**
Descrição clara e concisa do bug

**Como Reproduzir**
1. Vá para '...'
2. Clique em '...'
3. Role até '...'
4. Veja o erro

**Comportamento Esperado**
O que deveria acontecer

**Screenshots**
Se aplicável, adicione screenshots

**Ambiente:**
- OS: [ex: Windows 10]
- Browser: [ex: Chrome 96]
- Versão: [ex: 1.0.0]

**Informações Adicionais**
Qualquer outra informação relevante
```

---

## 💡 Sugerir Melhorias

### Template de Feature Request

```markdown
**A funcionalidade está relacionada a um problema?**
Descrição clara do problema

**Descreva a solução que você gostaria**
Descrição clara da solução proposta

**Descreva alternativas consideradas**
Outras soluções que você considerou

**Contexto Adicional**
Qualquer outro contexto ou screenshots
```

---

## 🧪 Testes

### Backend

```csharp
[Fact]
public async Task GetById_ExistingUser_ReturnsUser()
{
    // Arrange
    var mockRepo = new Mock<IUsuariosRepository>();
    mockRepo.Setup(r => r.GetById(1))
        .ReturnsAsync(new Usuario { Id_usuario = 1 });
    
    var useCase = new GetUsuarioByIdUseCase(mockRepo.Object);
    
    // Act
    var result = await useCase.Execute(1);
    
    // Assert
    Assert.NotNull(result);
    Assert.Equal(1, result.Id_usuario);
}
```

### Frontend

```javascript
describe('UsuarioForm', () => {
  it('should submit form with valid data', () => {
    const onSubmit = jest.fn();
    const { getByLabelText, getByText } = render(
      <UsuarioForm onSubmit={onSubmit} />
    );
    
    fireEvent.change(getByLabelText('Nome'), {
      target: { value: 'João' }
    });
    
    fireEvent.click(getByText('Cadastrar'));
    
    expect(onSubmit).toHaveBeenCalled();
  });
});
```

---

## 📚 Recursos Úteis

- [Clean Architecture](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html)
- [C# Coding Conventions](https://docs.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions)
- [React Best Practices](https://react.dev/learn)
- [Conventional Commits](https://www.conventionalcommits.org/)

---

## 📞 Dúvidas?

Se tiver dúvidas sobre como contribuir:

- Abra uma issue com a tag `question`
- Entre em contato com a equipe
- Consulte a documentação

---

**Obrigado por contribuir! 🎉**
