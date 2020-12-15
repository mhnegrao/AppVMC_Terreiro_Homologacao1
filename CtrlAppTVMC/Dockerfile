#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src


#RUN dotnet restore "AppVMC/AppVMC.csproj"
#COPY . .
#WORKDIR "/src/AppVMC"
#RUN dotnet build "AppVMC.csproj" -c Release -o /app/build

#FROM build AS publish
#RUN dotnet publish "AppVMC.csproj" -c Release -o /app/publish

#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "AppVMC.dll"]

COPY ["AppVMC.csproj","."]
#COPY ["AppVMC/Data/BaseDados.db", "AppVMC/Dados"]
RUN dotnet restore "AppVMC.csproj"
COPY . .
RUN dotnet build "AppVMC.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AppVMC.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app

COPY --from=publish /app/publish .
ENV ASPNETCORE_URLS http://*:$PORT
CMD ["dotnet","AppVMC.dll"]