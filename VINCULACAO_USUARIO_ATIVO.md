# Vinculação de Usuário ao Ativo

## Funcionalidade Implementada

Agora é possível vincular um usuário a um ativo durante o cadastro, buscando o usuário pela matrícula.

## Como Funciona

### No Frontend (Formulário de Cadastro de Ativo)

1. **Campo de Matrícula**: Um novo campo foi adicionado ao formulário de cadastro de ativos
2. **Busca Automática**: Ao digitar a matrícula e sair do campo (onBlur), o sistema busca automaticamente o usuário
3. **Feedback Visual**:
   - ✓ **Usuário encontrado**: Exibe nome completo e departamento em um card verde
   - ✗ **Usuário não encontrado**: Exibe mensagem de erro em vermelho
   - 🔍 **Buscando**: Mostra indicador de carregamento
4. **Opcional**: O campo de matrícula é opcional - você pode cadastrar um ativo sem usuário
5. **Limpar**: Botão "✕" para remover o usuário selecionado

### No Backend

#### Endpoint Atualizado: GET /api/Usuarios

Agora aceita um parâmetro de query opcional:

```
GET /api/Usuarios?matricula=10001
```

**Resposta:**
```json
{
  "usuarios": [
    {
      "id_usuario": 1,
      "p_nome": "João",
      "sobrenome": "Silva",
      "matricula": 10001
    }
  ]
}
```

#### Endpoint: POST /api/Ativos/register

Agora aceita o campo `id_usuario` (opcional):

```json
{
  "nome": "Notebook Dell",
  "modelo": "Latitude 5420",
  "serialNumber": "DLL-001",
  "codInventario": 1001,
  "tipo": "Notebook",
  "id_localizacao": 1,
  "id_usuario": 1
}
```

#### Endpoint: GET /api/Ativos

Retorna os ativos com os dados do usuário vinculado:

```json
{
  "ativos": [
    {
      "id_ativo": 1,
      "nome": "Notebook Dell",
      "modelo": "Latitude 5420",
      "id_usuario": 1,
      "usuario": {
        "id_usuario": 1,
        "p_nome": "João",
        "sobrenome": "Silva",
        "matricula": 10001
      },
      "localizacao": {
        "id_localizacao": 1,
        "cidade": "São Paulo",
        "estado": "SP"
      }
    }
  ]
}
```

## Fluxo de Uso

### 1. Cadastrar um Usuário (se ainda não existir)

Primeiro, certifique-se de que o usuário está cadastrado no sistema.

### 2. Cadastrar um Ativo com Usuário

1. Clique em "Novo Ativo"
2. Preencha os dados do ativo (nome, modelo, serial, etc.)
3. Selecione a localização
4. **Digite a matrícula do usuário** no campo "Matrícula do Usuário"
5. Aguarde a busca automática
6. Verifique se o usuário correto foi encontrado (nome aparecerá em verde)
7. Clique em "Cadastrar Ativo"

### 3. Cadastrar um Ativo sem Usuário

1. Clique em "Novo Ativo"
2. Preencha os dados do ativo
3. **Deixe o campo de matrícula vazio**
4. Clique em "Cadastrar Ativo"

### 4. Editar um Ativo e Vincular/Desvincular Usuário

1. Clique no botão de editar (✏️) do ativo
2. Para vincular: Digite a matrícula do usuário
3. Para desvincular: Clique no botão "✕" ao lado do campo de matrícula
4. Salve as alterações

## Validações

- ✅ A matrícula deve existir no banco de dados
- ✅ O usuário deve estar cadastrado antes de vincular ao ativo
- ✅ Um ativo pode existir sem usuário vinculado
- ✅ A localização é obrigatória
- ✅ O usuário é opcional

## Filtros de Busca

Você também pode filtrar ativos por usuário usando os filtros:

- **Matrícula do Usuário**: Busca exata por matrícula
- **Nome do Usuário**: Busca por nome ou sobrenome (contém)

Exemplo:
```
GET /api/Ativos?matriculaUsuario=10001
GET /api/Ativos?nomeUsuario=João
```

## Relacionamentos no Banco de Dados

```
┌─────────────┐         ┌─────────────┐         ┌──────────────┐
│  Localizacao│◄────────│   Ativos    │────────►│   Usuario    │
│             │         │             │         │              │
│ id_localizacao       │ id_ativo    │         │ id_usuario   │
│ cidade      │         │ nome        │         │ p_nome       │
│ estado      │         │ modelo      │         │ sobrenome    │
└─────────────┘         │ id_localizacao       │ matricula    │
                        │ id_usuario  │         │ departamento │
                        └─────────────┘         └──────────────┘
```

- **Ativo → Localização**: Obrigatório (INNER JOIN)
- **Ativo → Usuário**: Opcional (LEFT JOIN)
