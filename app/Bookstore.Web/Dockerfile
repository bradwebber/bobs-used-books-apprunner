#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/sdk:6.0
COPY . /app
WORKDIR /app
RUN dotnet publish -c Release -o out
EXPOSE 80
Expose 443
ENTRYPOINT ["dotnet", "out/Bookstore.Web.dll"]


#FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
#WORKDIR /app
#EXPOSE 80
#EXPOSE 443
#
#FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
#WORKDIR /src
#COPY ["app/Bookstore.Web/Bookstore.Web.csproj", "app/Bookstore.Web/"]
#COPY ["app/Bookstore.Data/Bookstore.Data.csproj", "app/Bookstore.Data/"]
#COPY ["app/Bookstore.Domain/Bookstore.Domain.csproj", "app/Bookstore.Domain/"]
#RUN dotnet restore "app/Bookstore.Web/Bookstore.Web.csproj"
#COPY . .
#WORKDIR "/src/app/Bookstore.Web"
#RUN dotnet build "Bookstore.Web.csproj" -c Release -o /app/build
#
#FROM build AS publish
#RUN dotnet publish "Bookstore.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false
#
#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "Bookstore.Web.dll"]