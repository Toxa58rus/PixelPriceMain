#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ApiGateways/ApiGateways/ApiGateways.csproj", "ApiGateways/ApiGateways/"]
COPY ["ApiGateways/ApiGateways.Dommani/ApiGateways.Domain.csproj", "ApiGateways/ApiGateways.Dommani/"]
COPY ["Contracts/Contracts.csproj", "Contracts/"]
COPY ["Common/Common/Common.csproj", "Common/Common/"]
COPY ["ApiGateways/ApiGateways.Context/ApiGateways.Context.csproj", "ApiGateways/ApiGateways.Context/"]
COPY ["ApiGateways/ApiGateways.Service/ApiGateways.Service.csproj", "ApiGateways/ApiGateways.Service/"]
COPY ["Chat/Chat.Context/Chat.Context/ChatService.Context.csproj", "Chat/Chat.Context/Chat.Context/"]
RUN dotnet restore "ApiGateways/ApiGateways/ApiGateways.csproj"
COPY . .
WORKDIR "/src/ApiGateways/ApiGateways"
RUN dotnet build "ApiGateways.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ApiGateways.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ApiGateways.dll"]