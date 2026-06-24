## Context

O projeto possui Clean Architecture com camadas Domain, Application, Infrastructure, Api e Agent. A entidade `Order` existe mas está vazia. O repositório `IOrderRepository` expõe apenas `GetByIdAsync`. O Agent é um projeto vazio que referencia exclusivamente Application. A stack é .NET 10, PostgreSQL via EF Core com Npgsql, Redis via StackExchangeRedis, Mapster para mapeamento, FluentValidation para validação de input e Result Pattern (`JacksonVeroneze.NET.Result`) para erros esperados.

Esta change implementa o primeiro slice vertical: o usuário pergunta em linguagem natural se existe ordem para um ativo, o Agent identifica o ticker, chama `CheckOrdersByAssetTool`, que chama `GetOrdersByAssetUseCase`, que consulta o banco via `IOrderRepository`.

## Goals / Non-Goals

**Goals:**
- Completar a entidade `Order` com campos de domínio completos.
- Atualizar `OrderMapping` (EF Core Fluent API) para refletir todos os campos.
- Criar `GetOrdersByAssetUseCase` com repositório atualizado.
- Criar `CheckOrdersByAssetTool` e `OrdersAgent` com guardrails.
- Garantir que a tool nunca receba dados sensíveis do modelo (userId, accountId).
- Bloquear qualquer pedido fora do escopo com resposta segura.

**Non-Goals:**
- Alteração de contratos HTTP, endpoints ou API layer.
- Criação de endpoint novo ou versão nova de API.
- Envio, cancelamento ou alteração de ordens reais.
- Recomendação de compra, venda ou manutenção.
- Consulta de posição de carteira ou proventos.
- Multi-agent, workflow ou MCP.
- Cache de ordens (reservado para change futura).

## Decisions

### 1. Novo use case `GetOrdersByAssetUseCase` em vez de adaptar `GetByIdOrderUseCase`

`GetByIdOrderUseCase` consulta por Id e retorna no máximo um registro. "Tenho ordem para ITUB4?" é semanticamente diferente — requer filtro por ticker e pode retornar múltiplos resultados. Adaptar o use case existente violaria a responsabilidade única e quebraria o contrato existente. **Decisão**: criar `GetOrdersByAssetUseCase` com request/response próprios.

### 2. Enums como tipos C# no Domain, sem tabela auxiliar

`OrderSide`, `OrderType` e `OrderStatus` são listas fechadas de domínio que não evoluem em tempo de execução. Tabelas auxiliares adicionariam joins sem valor. O EF Core mapeia enums para integer por padrão. **Decisão**: enums C# mapeados para `integer` no banco, com `HasConversion<string>()` para legibilidade no banco.

### 3. `UserId` e `AccountId` vêm do contexto de backend, nunca do prompt

A tool recebe do modelo apenas o ticker. `UserId` e `AccountId` são resolvidos de `IHttpContextAccessor` ou contexto de sessão fora do fluxo do LLM. **Decisão**: `GetOrdersByAssetInput` tem apenas `AssetTicker`; o use case recebe o contexto do usuário por parâmetro separado, injetado pelo Agent via backend context — não pelo modelo.

> **Nota de implementação**: na V1, como não há autenticação ainda no Agent, `UserId` será um placeholder fixo passado pelo Agent via código. A validação real de identidade será adicionada em change futura dedicada a autenticação do Agent.

### 4. Guardrails em dois níveis: system prompt e código

Guardrails apenas em system prompt são insuficientes — podem ser contornados por prompt injection. **Decisão**: guardrails em dois níveis: (a) system instructions explícitas no Agent; (b) validação em código na tool que bloqueia tickers inválidos e em middleware que detecta intents proibidos via lista de padrões conhecidos.

### 5. `CheckOrdersByAssetTool` retorna `IReadOnlyList<OrderSummary>` para o Agent

O Agent precisa de dados estruturados para formular a resposta. Retornar texto livre da tool dificultaria testes e adicionaria lógica de formatação à tool. **Decisão**: a tool retorna `IReadOnlyList<OrderSummary>` com os campos necessários; o Agent formata a resposta em linguagem natural.

## Risks / Trade-offs

- **[Risco] Entidade Order sem migration inicial** → Ao completar os campos, será necessário criar migration EF Core. A migration deve ser criada com cuidado para não dropar a tabela existente caso ela já tenha dados. Mitigação: criar migration incremental e revisar o script gerado antes de aplicar.
- **[Risco] Guardrail de prompt injection baseado em padrões** → Uma lista de padrões pode ter falsos negativos para injeções criativas. Mitigação: escopo fechado (somente ticker aceito) reduz a superfície de ataque; padrões cobrem os casos mais comuns.
- **[Risco] `UserId` fixo na V1** → Sem autenticação real no Agent, qualquer usuário veria ordens pelo ticker sem segmentação. Mitigação: aceitável para validação técnica inicial; change futura de autenticação é pré-requisito para produção.
- **[Trade-off] Sem cache de ordens na V1** → Cada pergunta vai ao banco. Aceitável para primeiro slice; cache pode ser adicionado em change específica quando a frequência de consulta justificar.

## Open Questions

- Qual o valor de `UserId` placeholder para a V1 enquanto não há autenticação no Agent?
- A tabela `order` no banco já existe com schema `order`? É necessário verificar antes de criar a migration.
- Qual o modelo Claude preferido para o `OrdersAgent` em produção? (Agent CLAUDE.md sugere Sonnet para implementação.)
