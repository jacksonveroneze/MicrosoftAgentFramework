# =====================================================
# Build + Publish
# =====================================================
FROM mcr.microsoft.com/dotnet/sdk:10.0-noble AS publish

WORKDIR /src

COPY ./main/Api/Api.csproj ./main/Api/
COPY ./main/Application/Application.csproj ./main/Application/
COPY ./main/Domain/Domain.csproj ./main/Domain/
COPY ./main/Infrastructure/Infrastructure.csproj ./main/Infrastructure/
COPY *.sln ./
COPY nuget.config ./
COPY Directory.Build.props ./
COPY Directory.Packages.props ./

RUN dotnet restore ./main/Api/Api.csproj \
    -r linux-x64 \
    -p:PublishReadyToRun=true

COPY . .

RUN dotnet publish ./main/Api/Api.csproj \
    -c Release \
    -r linux-x64 \
    --self-contained false \
    --no-restore \
    -p:PublishReadyToRun=true \
    -p:PublishTrimmed=false \
    -p:DebugType=None \
    -p:DebugSymbols=false \
    --output /publish

# =====================================================
# Etapa Final: Imagem de Runtime
# =====================================================
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime

ENV DOTNET_GCDynamicAdaptationMode=1 \
    DOTNET_GCHeapHardLimitPercent=0x4B \
    DOTNET_TieredCompilation=1 \
    DOTNET_TC_QuickJitForLoops=1 \
    DOTNET_EnableDiagnostics=1
    # DOTNET_GCConserveMemory=5 \
    # DOTNET_ThreadPool_UnfairSemaphoreSpinLimit=70

COPY --from=publish /publish /home/app

WORKDIR /home/app

ENTRYPOINT ["dotnet", "/home/app/Api.dll"]
