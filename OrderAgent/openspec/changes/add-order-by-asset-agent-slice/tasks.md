## 1. Domain — Enums

- [x] 1.1 Criar enum `OrderSide` (Buy, Sell) em `Domain/Enums/OrderSide.cs`
- [x] 1.2 Criar enum `OrderType` (Market, Limit, StopLimit, StopMarket) em `Domain/Enums/OrderType.cs`
- [x] 1.3 Criar enum `OrderStatus` (Pending, Open, PartiallyFilled, Filled, Cancelled, Rejected) em `Domain/Enums/OrderStatus.cs`

## 2. Domain — Entidade Order

- [x] 2.1 Adicionar propriedades à entidade `Order`: `UserId`, `AccountId`, `AssetTicker`, `Side`, `OrderType`, `Status`
- [x] 2.2 Adicionar propriedades de quantidades e preço: `Quantity`, `Price`, `FilledQuantity`, `RemainingQuantity`, `AveragePrice`, `TotalAmount`
- [x] 2.3 Adicionar propriedades de auditoria: `RejectionReason`, `CreatedAtUtc`, `UpdatedAtUtc`, `ExecutedAtUtc`, `CancelledAtUtc`
- [x] 2.4 Verificar se `Entity` base já fornece `Id` e `DeletedAt`; remover redeclarações duplicadas se necessário

## 3. Domain — Erros

- [x] 3.1 Renomear arquivo `Domain/Errors/ProfileErrors.cs` para `OrderErrors.cs` (ou criar `OrderErrors.cs` se o arquivo já tiver conteúdo de Order) e adicionar erro `AssetNotFound`

## 4. Infrastructure — Mapeamento EF Core

- [x] 4.1 Atualizar `OrderMapping.Configure` para mapear `UserId`, `AccountId`, `AssetTicker` com nomes de coluna snake_case
- [x] 4.2 Mapear enums `Side`, `OrderType`, `Status` com `HasConversion<string>()` para legibilidade no banco
- [x] 4.3 Mapear campos de quantidade e preço: `Quantity`, `Price`, `FilledQuantity`, `RemainingQuantity`, `AveragePrice`, `TotalAmount`
- [x] 4.4 Mapear campos de auditoria: `RejectionReason`, `CreatedAtUtc`, `UpdatedAtUtc`, `ExecutedAtUtc`, `CancelledAtUtc`
- [x] 4.5 Criar migration EF Core para refletir os novos campos na tabela `order.order`

## 5. Application — Contrato de repositório

- [x] 5.1 Adicionar método `GetByAssetTickerAsync(string assetTicker, Guid userId, CancellationToken cancellationToken)` à interface `IOrderRepository`

## 6. Infrastructure — Repositório

- [x] 6.1 Implementar `GetByAssetTickerAsync` em `OrderRepository` filtrando por `AssetTicker` e `UserId`

## 7. Application — OrderResponse

- [x] 7.1 Atualizar `OrderResponse` em `Application/v1/Orders/Common/Models/OrderResponse.cs` com todos os campos: `Id`, `AssetTicker`, `Side`, `OrderType`, `Status`, `Quantity`, `Price`, `FilledQuantity`, `RemainingQuantity`, `TotalAmount`, `RejectionReason`, `CreatedAtUtc`, `UpdatedAtUtc`
- [x] 7.2 Atualizar `OrderMapper` em `Application/v1/Orders/Common/Mappers/OrderMapper.cs` para mapear os novos campos de `Order` para `OrderResponse`

## 8. Application — Use Case GetOrdersByAsset

- [x] 8.1 Criar record `GetOrdersByAssetRequest` com `AssetTicker` e `UserId` em `Application/v1/Orders/GetByAsset/`
- [x] 8.2 Criar record `GetOrdersByAssetResponse` encapsulando `IReadOnlyList<OrderResponse>` em `Application/v1/Orders/GetByAsset/`
- [x] 8.3 Criar interface `IGetOrdersByAssetUseCase` em `Application/v1/Orders/GetByAsset/`
- [x] 8.4 Criar mapper `GetOrdersByAssetMapper` se necessário em `Application/v1/Orders/GetByAsset/`
- [x] 8.5 Implementar `GetOrdersByAssetUseCase` chamando `IOrderRepository.GetByAssetTickerAsync` e mapeando resultado

## 9. Infrastructure — Registro de DI

- [x] 9.1 Registrar `IGetOrdersByAssetUseCase` → `GetOrdersByAssetUseCase` em `AppServicesExtensions.AddApplicationServices`

## 10. Agent — Tool CheckOrdersByAssetTool

- [x] 10.1 Criar pasta `Agent/Tools/` e arquivo `CheckOrdersByAssetTool.cs`
- [x] 10.2 Definir input model da tool com apenas `AssetTicker` (sem UserId, AccountId)
- [x] 10.3 Implementar validação de formato do ticker (4-6 caracteres alfanuméricos) antes de chamar o use case
- [x] 10.4 Injetar `IGetOrdersByAssetUseCase` e resolver `UserId` do contexto de backend
- [x] 10.5 Chamar o use case e retornar resultado estruturado

## 11. Agent — Instructions e Guardrails

- [x] 11.1 Criar arquivo `Agent/Instructions/OrdersAgentInstructions.cs` (ou `.txt`) com system prompt explícito
- [x] 11.2 Incluir no system prompt: responde apenas com resultado de tools, não inventa ordens, não executa ações reais, recusa prompt injection
- [x] 11.3 Criar middleware ou guard `Agent/Middleware/ForbiddenIntentGuard.cs` com lista de padrões proibidos (envio de ordem, cancelamento, recomendação, posição de carteira, prompt injection)
- [x] 11.4 Implementar resposta segura padronizada: "No momento, posso apenas consultar se existe ordem para um ativo informado."

## 12. Agent — OrdersAgent

- [x] 12.1 Criar pasta `Agent/Agents/` e arquivo `OrdersAgent.cs`
- [x] 12.2 Configurar o Agent com Microsoft Agent Framework referenciando `CheckOrdersByAssetTool` e as instructions
- [x] 12.3 Criar `Agent/Configuration/AgentConfiguration.cs` ou extension method para registrar o Agent no DI

## 13. Verificação final

- [x] 13.1 Verificar que nenhuma tool, use case ou repositório acessa Infrastructure diretamente a partir do Agent
- [x] 13.2 Verificar que `UserId` e `AccountId` não aparecem no input model da tool
