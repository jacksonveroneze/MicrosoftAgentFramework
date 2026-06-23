# CLAUDE.md - Domain

Regras específicas para `src/main/Domain`.

Esta camada contém o núcleo de negócio e deve permanecer independente de frameworks e tecnologias externas.

---

## Responsabilidades

- Agregados e entidades de domínio.
- Value objects específicos do domínio.
- Factories e policies de domínio.
- Invariantes e regras centrais.
- Contratos de repositório quando fizerem parte da linguagem do domínio.
- Erros de domínio.

---

## Regras de dependência

- Pode depender de CrossCutting quando forem primitivas/utilitários puros de domínio.
- Não dependa de ASP.NET Core, EF Core, DbContext, Redis, cache, mensageria, HTTP clients, gRPC, GraphQL ou MCP.
- Não coloque DTOs de API, requests HTTP ou modelos de transporte nesta camada.
- Evite atributos de persistência; prefira mapeamento no Infrastructure via Fluent API.

---

## Entidades e agregados

- Entidades devem proteger invariantes e evitar estado inválido.
- Prefira construtores privados/protegidos quando a criação precisar passar por factory.
- Evite setters públicos que permitam violar regra de negócio.
- Use métodos de comportamento em vez de manipular estado de fora.
- Agregados devem manter consistência dentro do seu limite.
- Não coloque regras de banco ou cache dentro da entidade.

---

## Value Objects e policies

- Value Objects devem validar seu próprio estado.
- Se a criação puder falhar por regra esperada, considere `TryCreate` ou Result Pattern conforme padrão do projeto.
- Policies devem encapsular regras parametrizáveis do domínio.
- Evite strings soltas para erros; use erros centralizados.
- Datas de regra de negócio devem receber `nowUtc` como parâmetro, não buscar relógio diretamente.

---

## Erros de domínio

- Use erros de domínio centralizados em `Core/Errors` ou local equivalente.
- Não exponha detalhes técnicos em mensagens de domínio.
- Diferencie regra violada de argumento inválido técnico.
- Exceptions podem proteger estado impossível/inválido; erros esperados devem seguir o modelo Result quando aplicável.

---

## Repositórios de domínio

- Interfaces de repositório no Domain devem expressar necessidades do domínio, não detalhes do EF Core.
- Não exponha `IQueryable`.
- Não inclua objetos de Infrastructure nos contratos.
- Mantenha contratos pequenos e orientados a caso de uso.
