#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["JoergIsAGeek.Heise.Course.BackendApi/JoergIsAGeek.Heise.Course.BackendApi.csproj", "JoergIsAGeek.Heise.Course.BackendApi/"]
RUN dotnet restore "JoergIsAGeek.Heise.Course.BackendApi/JoergIsAGeek.Heise.Course.BackendApi.csproj"
COPY . .
WORKDIR "/src/JoergIsAGeek.Heise.Course.BackendApi"
RUN dotnet build "JoergIsAGeek.Heise.Course.BackendApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "JoergIsAGeek.Heise.Course.BackendApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "JoergIsAGeek.Heise.Course.BackendApi.dll"]