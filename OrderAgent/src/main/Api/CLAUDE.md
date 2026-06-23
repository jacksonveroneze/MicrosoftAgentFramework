# CLAUDE.md - Api

Regras específicas para `src/main/Api`.

Este projeto é a camada de entrada/composição da aplicação.

---

## Responsabilidades

- Expor Minimal APIs e futuros transportes como REST, gRPC, GraphQL ou MCP.
- Configurar pipeline ASP.NET Core.
- Configurar middleware, health checks, métricas, OpenAPI/Scalar e versionamento.
- Aplicar autenticação e autorização na borda.
- Mapear entrada externa para Application.
- Mapear Result/Application output para resposta do transporte.
- Configurar DI chamando extensões de Infrastructure/Application.

---

## Regras

- Mantenha `Program.cs` pequeno e delegue configuração para extension methods.
- Endpoints devem ser finos: receber input, chamar use case e traduzir resultado.
- Não coloque regra de negócio em endpoints, middlewares ou extensões de API.
- Não acesse DbContext, Redis, cache ou repositories diretamente em endpoints.
- Não retorne entidades de Domain/EF diretamente para clientes.
- Use `CancellationToken` nos endpoints e propague para os use cases.
- Use `ResultTranslator` ou padrão equivalente para converter Result em resposta HTTP.
- Use `ProblemDetails` para erros HTTP estruturados.
- Não exponha stack trace, exception message sensível ou detalhes internos para clientes.
- Mantenha rotas agrupadas por versão e recurso.
- Use constantes para nomes de rotas quando houver geração de location/header.

---

## Contratos de entrada e saída

- Pode usar inputs/outputs da Application diretamente em Minimal APIs somente quando eles forem contratos estáveis, livres de atributos HTTP e sem dependência de transporte.
- Se o contrato externo divergir da operação de aplicação, crie DTO de Api e faça mapping para o input da Application.
- Para gRPC, GraphQL ou MCP, não passe tipos gerados/específicos do transporte para Application.
- Mantenha detalhes como `HttpContext`, `LinkGenerator`, status code e headers apenas na Api.

---

## Pipeline ASP.NET Core

- Preserve correlation id antes de logs e endpoints.
- Use exception handler global.
- Use health checks e métricas em endpoints dedicados.
- OpenAPI/Scalar deve ser exposto apenas em ambiente apropriado.
- Autenticação deve vir antes de autorização.
- Evite middlewares sem necessidade clara.

---

## Segurança

- Autenticação e autorização devem ser explícitas.
- Não confie em dados enviados pelo cliente para autorização.
- Não logue tokens, secrets, payloads sensíveis ou dados pessoais.
- Validar entrada antes de executar operações sensíveis.
- Retornar erros seguros e consistentes.

---

## Ao adicionar novo endpoint

1. Crie/identifique o use case na Application.
2. Crie input/output na Application ou DTO na Api quando necessário.
3. Adicione endpoint fino no grupo de rota correto.
4. Propague `CancellationToken`.
5. Traduza Result usando o padrão existente.
6. Adicione `Produces`/documentação quando aplicável.
7. Atualize testes quando existirem.
