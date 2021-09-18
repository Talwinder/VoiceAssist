FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["src/CreditCardService/CreditCardService.csproj", "src/CreditCardService/"]
RUN dotnet restore "src\CreditCardService\CreditCardService.csproj"
COPY . .
WORKDIR "/src/src/CreditCardService" 
RUN dotnet build "CreditCardService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CreditCardService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CreditCardService.dll"]
