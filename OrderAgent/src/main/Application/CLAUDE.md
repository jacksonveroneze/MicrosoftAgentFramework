# CLAUDE.md - Application

Regras específicas para `src/main/Application`.

Esta camada orquestra casos de uso e permanece independente de transporte e infraestrutura concreta.

---

## Responsabilidades

- Definir use cases.
- Definir inputs e outputs internos da aplicação.
- Definir abstrações para repositórios, UoW, cache, clock, mapper, geradores, clients e providers.
- Executar validações de entrada da operação.
- Orquestrar Domain e abstrações externas.
- Retornar Result Pattern para erros esperados.

---

## Regras de dependência

- Pode depender de Domain e bibliotecas de aplicação aprovadas, como FluentValidation, Mapster abstractions e logging abstractions.
- Não referencie ASP.NET Core, HttpContext, endpoints, controllers, gRPC generated types, GraphQL resolvers ou MCP tools.
- Não referencie EF Core, DbContext, Redis, providers concretos, HTTP clients concretos ou mensageria concreta.
- Toda dependência externa deve entrar por interface/abstração.

---

## Use cases

- Use cases devem representar operações de negócio: `CreateOrder`, `GetByIdOrder`, `ActivateOrder`.
- Não nomeie use cases com termos de transporte: `PostShortUrl`, `GrpcCreateOrder`, `HttpGetOrder`.
- Implementações devem ser `sealed` quando não houver necessidade de herança.
- Use cases devem expor `ExecuteAsync(request, cancellationToken)` quando seguirem o padrão do projeto.
- Sempre valide `request` com guard clause ou validator antes de usar seus dados.
- Propague `CancellationToken` para todas as chamadas async.
- Não use `.Result`, `.Wait()` ou sync-over-async.

---

## Result Pattern e validação

- Retorne `Result<T>` para sucesso ou erro esperado.
- Use `Result.FromNotFound`, `FromInvalid`, `FromConflict` ou equivalentes do projeto para erros esperados.
- Não use exception para fluxo normal de negócio.
- Use FluentValidation para regras de formato/input.
- Invariantes fortes pertencem ao Domain.
- Erros devem usar objetos/padrões centralizados do projeto, não strings soltas espalhadas.

---

## Inputs, outputs e mapeamentos

- Inputs implementam `IBaseRequest` quando participarem do padrão de use case.
- Outputs devem ser simples, imutáveis quando possível e sem dependência de transporte.
- Use records para modelos de dados simples.
- Não coloque atributos HTTP, gRPC, GraphQL ou MCP em modelos de Application.
- Use Mapster/registers quando houver mapeamentos repetidos ou não triviais.
- Evite expor entidades de Domain diretamente para Api.

---

## Logs e clock

- Use `ILogger` abstractions apenas quando o log agregar valor real.
- Não logue dados sensíveis.
- Para data/hora de negócio, prefira `IDateTimeProvider` em vez de acesso direto ao relógio do sistema.
- Use UTC para datas técnicas.

---

## Ao adicionar novo caso de uso

1. Crie input, output e interface do use case.
2. Crie validator quando houver validação de entrada.
3. Use abstrações para repositórios e serviços externos.
4. Retorne Result para erros esperados.
5. Propague `CancellationToken`.
6. Registre o use case na composição/DI conforme padrão do projeto.
7. Adicione testes unitários quando existirem.
