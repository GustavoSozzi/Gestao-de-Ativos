# Documentação da API de Ativos

## Endpoint GET /api/Ativos

Este endpoint retorna todos os ativos com seus dados relacionados de **Usuário** e **Localização** através de JOINs.

### Query SQL Equivalente
```sql
SELECT * FROM ativos 
INNER JOIN usuario ON ativos.id_usuario = usuario.id_usuario 
INNER JOIN localizacao ON ativos.id_localizacao = localizacao.id_localizacao;
```

### Estrutura de Resposta

```json
{
  "ativos": [
    {
      "id_ativo": 1,
      "id_localizacao": 1,
      "nome": "Notebook Dell",
      "modelo": "Latitude 5420",
      "serialNumber": "ABC123",
      "codInventario": 1001,
      "tipo": "Notebook",
      "id_usuario": 1,
      "localizacao": {
        "id_localizacao": 1,
        "cidade": "São Paulo",
        "estado": "SP"
      },
      "usuario": {
        "id_usuario": 1,
        "p_nome": "João",
        "sobrenome": "Silva",
        "matricula": 12345
      }
    }
  ]
}
```

### Parâmetros de Busca (Query Parameters)

Você pode filtrar os resultados usando os seguintes parâmetros na URL:

| Parâmetro | Tipo | Descrição | Exemplo |
|-----------|------|-----------|---------|
| `nome` | string | Busca por nome do ativo (contém) | `?nome=Dell` |
| `modelo` | string | Busca por modelo do ativo (contém) | `?modelo=Latitude` |
| `tipo` | string | Busca por tipo do ativo (contém) | `?tipo=Notebook` |
| `codInventario` | long | Busca por código de inventário exato | `?codInventario=1001` |
| `cidade` | string | Busca por cidade da localização (contém) | `?cidade=São Paulo` |
| `estado` | string | Busca por estado da localização (contém) | `?estado=SP` |
| `matriculaUsuario` | long | Busca por matrícula do usuário exata | `?matriculaUsuario=12345` |
| `nomeUsuario` | string | Busca por nome ou sobrenome do usuário (contém) | `?nomeUsuario=João` |

### Exemplos de Uso

#### 1. Buscar todos os ativos
```
GET /api/Ativos
```

#### 2. Buscar ativos por nome
```
GET /api/Ativos?nome=Dell
```

#### 3. Buscar ativos por cidade
```
GET /api/Ativos?cidade=São Paulo
```

#### 4. Buscar ativos por matrícula do usuário
```
GET /api/Ativos?matriculaUsuario=12345
```

#### 5. Buscar com múltiplos filtros
```
GET /api/Ativos?tipo=Notebook&cidade=São Paulo&estado=SP
```

#### 6. Buscar por nome do usuário
```
GET /api/Ativos?nomeUsuario=João
```

### Notas Importantes

- Todos os parâmetros são opcionais
- As buscas por texto (nome, modelo, tipo, cidade, estado, nomeUsuario) são case-sensitive e usam `Contains`
- Múltiplos filtros podem ser combinados
- Se nenhum ativo for encontrado, retorna status `204 No Content`
- Requer autenticação (token JWT)
