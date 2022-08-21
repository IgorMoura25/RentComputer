FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["./services/RC.Customer.API/RC.Customer.API.csproj", "services/RC.Customer.API/"] 
COPY ["./building-blocks/RC.Core/RC.Core.csproj", "building-blocks/RC.Core/"] 
COPY ["./building-blocks/RC.MessageBus/RC.MessageBus.csproj", "building-blocks/RC.MessageBus/"] 
COPY ["./building-blocks/RC.WebAPI.Core/RC.WebAPI.Core.csproj", "building-blocks/RC.WebAPI.Core/"] 
RUN dotnet restore "services/RC.Customer.API/RC.Customer.API.csproj" 
COPY . . 
WORKDIR "services/RC.Customer.API" 
RUN dotnet build "RC.Customer.API.csproj" -c Release -o /app/build 


FROM build AS publish
RUN dotnet publish "RC.Customer.API.csproj" -c Release -o /app/publish


FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS final
WORKDIR /app
EXPOSE 80
EXPOSE 443
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RC.Customer.API.dll"]