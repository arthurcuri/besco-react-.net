# Configuração do Backend

Este frontend pode ser usado com ambos os backends (Java e .NET).

## Configuração Atual
- **Backend**: .NET (ASP.NET Core)
- **Porta**: 5000
- **Status**: ✅ Configurado

## Como Alternar Entre Backends

### Para usar o Backend .NET (Atual):
1. **Arquivo**: `src/services/api.js`
   ```javascript
   const API_BASE_URL = 'http://localhost:5000/api';
   ```

2. **Arquivo**: `package.json`
   ```json
   "proxy": "http://localhost:5000"
   ```

### Para usar o Backend Java:
1. **Arquivo**: `src/services/api.js`
   ```javascript
   const API_BASE_URL = 'http://localhost:8080/api';
   ```

2. **Arquivo**: `package.json`
   ```json
   "proxy": "http://localhost:8080"
   ```

## Executar o Frontend

```bash
cd frontend
npm start
```

O frontend estará disponível em: http://localhost:3000

## Portas dos Serviços

| Serviço | Tecnologia | Porta | Status |
|---------|------------|-------|--------|
| Frontend | React | 3000 | ✅ |
| Backend Java | Spring Boot | 8080 | ✅ |
| Backend .NET | ASP.NET Core | 5000 | ✅ |

## API Endpoints (Ambos os backends)

- `GET /api/tasks` - Listar todas as tarefas
- `POST /api/tasks` - Criar nova tarefa
- `PUT /api/tasks/{id}` - Atualizar tarefa
- `DELETE /api/tasks/{id}` - Deletar tarefa
- `GET /api/tasks/stats` - Estatísticas
- `GET /api/tasks/pending` - Tarefas pendentes
- `GET /api/tasks/in-progress` - Tarefas em andamento
- `GET /api/tasks/completed` - Tarefas concluídas 