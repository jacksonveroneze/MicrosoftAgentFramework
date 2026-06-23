# CLAUDE.md - Infrastructure

Regras específicas para `src/main/Infrastructure`.

Esta camada implementa detalhes técnicos e integra a aplicação com banco, cache, observabilidade, autenticação técnica e providers.

---

## Responsabilidades

- EF Core, DbContexts, migrations e mappings.
- Implementações de repositórios.
- Unit of Work concreto.
- Redis, FusionCache e distributed counters.
- Providers concretos: clock, mapper, gerador de código, sanitização, clients externos.
- Configuração de autenticação/autorização técnica.
- Logging, métricas, tracing, correlation id e health checks.
- Registro de DI para implementações concretas.

---

## Regras de dependência

- Pode depender de Application e Domain para implementar suas abstrações.
- Não coloque regra de negócio em repositories, DbContexts, mappings ou extensions.
- Não altere contratos da Application apenas para facilitar EF/cache.
- Não exponha tipos concretos de Infrastructure para Application ou Domain.

---

## EF Core e banco

- DbContexts ficam nesta camada.
- Prefira Fluent API para mapeamentos.
- Use snake_case conforme padrão do projeto.
- Use `DefaultWriteDbContext` para escrita e `DefaultReadDbContext` com no-tracking para leitura quando aplicável.
- Use `AsNoTracking` em consultas somente leitura quando não estiver configurado globalmente.
- Não exponha `IQueryable` fora da persistência.
- Evite N+1 queries.
- Evite carregar mais dados que o necessário.
- Use paginação para listas.
- Passe `CancellationToken` em operações async.
- Use transação/UoW quando a operação precisar ser atômica.
- Trate unique constraint como conflito esperado quando aplicável.
- Não habilite sensitive data logging em produção.

---

## Repositories e UoW

- Repositories implementam interfaces de Application/Domain.
- Mantenha repositories focados em persistência, sem regra de negócio.
- Não crie generic repository novo sem valor claro.
- Use UoW concreto apenas para coordenar persistência/commit.
- Não esconda chamadas caras em propriedades ou métodos com nome inocente.

---

## Cache e Redis

- Use cache somente quando houver benefício claro de leitura, resiliência ou performance.
- Defina TTL, fail-safe, timeouts e jitter de forma intencional.
- Não cacheie dados sensíveis sem decisão explícita.
- Chaves de cache devem ter prefixo/nome consistente.
- Propague `CancellationToken`.
- Mantenha fallback para repositório/origem quando o cache falhar, se o caso permitir.

---

## Observabilidade e configuração

- Use extension methods para registrar configurações de infraestrutura.
- Mantenha configurações tipadas em classes de configuration/options.
- Use Serilog, correlation id, OpenTelemetry e Prometheus conforme padrão existente.
- Não logue secrets, tokens, connection strings ou dados pessoais.
- Instrumente dependências relevantes, mas evite ruído.
- Health checks devem refletir dependências importantes.

---

## Segurança

- Configure JWT Bearer e validação de token de forma restritiva.
- Use `ClockSkew = TimeSpan.Zero` quando o projeto exigir validação estrita.
- Não hardcode secrets ou endpoints sensíveis.
- Leia configurações de providers seguros.
- Evite configurações inseguras em produção.
