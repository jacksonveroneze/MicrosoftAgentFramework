## Why

O Agent de ordens precisa de um primeiro slice vertical funcional que permita ao usuário perguntar, em linguagem natural, se existe ordem aberta para um ativo (ex.: "Tenho ordem para ITUB4?"). Atualmente, a entidade `Order` está vazia, não há use case de consulta por ativo, e o projeto Agent não possui nenhum agente ou tool implementados.

## What Changes

- **Domínio**: finalizar a entidade `Order` com os campos de negócio necessários (18 campos: identificadores, ticker, lado, tipo, status, quantidades, preços e datas). Adicionar enums `OrderSide`, `OrderType` e `OrderStatus`. Criar `OrderErrors` com os erros de domínio relevantes.
- **Infrastructure**: atualizar `OrderMapping` para mapear todos os campos da entidade. Adicionar método `GetByAssetTickerAsync` ao `OrderRepository`.
- **Application**: adicionar `GetByAssetTickerAsync` à interface `IOrderRepository`. Atualizar `OrderResponse` com todos os campos relevantes. Criar novo use case `GetOrdersByAssetUseCase` (separado de `GetByIdOrderUseCase`). Registrar o novo use case no DI.
- **Agent**: criar `OrdersAgent` com instructions e guardrails. Criar `CheckOrdersByAssetTool` que recebe apenas o ticker do modelo e chama exclusivamente `GetOrdersByAssetUseCase`. Registrar no DI.
- **Testes**: criar projeto de testes unitários para Application e Agent. Cobrir os cenários de consulta por ativo com resultado, sem resultado, ticker inválido e pedidos fora de escopo.

## Capabilities

### New Capabilities

- `order-by-asset-agent`: consulta conversacional de ordens por ativo via Agent com guardrails, tool `CheckOrdersByAssetTool` e use case `GetOrdersByAssetUseCase`.

### Modified Capabilities

## Impact

- `Domain/Entities/Order.cs`: entidade será completada com novos campos e enums.
- `Infrastructure/Mappings/OrderMapping.cs`: mapeamento EF Core será atualizado para refletir todos os campos.
- `Infrastructure/Repositories/Order/OrderRepository.cs`: novo método de consulta por ticker.
- `Application/Abstractions/Repositories/IOrderRepository.cs`: novo contrato de consulta por ticker.
- `Application/v1/Orders/Common/Models/OrderResponse.cs`: record atualizado com campos completos.
- `Infrastructure/Extensions/AppServicesExtensions.cs`: registro do novo use case.
- `Agent/` (novos arquivos): `Agents/OrdersAgent.cs`, `Tools/CheckOrdersByAssetTool.cs`, `Instructions/`, `Configuration/`.
- Sem alteração em contratos HTTP, endpoints, API layer ou banco de dados existente.
