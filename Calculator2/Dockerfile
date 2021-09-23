#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build

#Installing nodejs & others

RUN apt-get update -yq \
    && apt-get install curl gnupg -yq \
    && curl -sL https://deb.nodesource.com/setup_16.x | bash \
    && apt-get install nodejs -yq \
    && npm install -g @angular/cli@12.2.7 -yq

WORKDIR /src
COPY ["Calculator2/Calculator.csproj", "Calculator2/"]
RUN dotnet restore "Calculator2/Calculator.csproj"
COPY "Calculator2/**" "Calculator2"
WORKDIR "/src/Calculator2"
RUN dotnet build "Calculator.csproj" -c Release -o /app/build

WORKDIR /src
COPY "Calculator-Ang/" "Calculator-Ang/"

WORKDIR "/src/Calculator-Ang"
RUN npm install \
    && ng build -c production

WORKDIR "/src/Calculator2"

FROM build AS publish
RUN dotnet publish "Calculator.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY --from=build /src/Calculator-Ang/dist ClientApp/.

ENTRYPOINT ["dotnet", "Calculator.dll"]