#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Src/Bootstrapper/BitShifter.Bootstrapper/BitShifter.Bootstrapper.csproj", "Src/Bootstrapper/BitShifter.Bootstrapper/"]
COPY ["Src/Modules/Identity/BitShifter.Modules.Identity.Api/BitShifter.Modules.Identity.Api.csproj", "Src/Modules/Identity/BitShifter.Modules.Identity.Api/"]
COPY ["Src/Modules/Identity/BitShifter.Modules.Identity.Core/BitShifter.Modules.Identity.Core.csproj", "Src/Modules/Identity/BitShifter.Modules.Identity.Core/"]
COPY ["Src/Shared/BitShifter.Shared.Abstractions/BitShifter.Shared.Abstractions.csproj", "Src/Shared/BitShifter.Shared.Abstractions/"]
COPY ["Src/Shared/BitShifter.Shared.ROP/BitShifter.Shared.ROP.csproj", "Src/Shared/BitShifter.Shared.ROP/"]
COPY ["Src/Shared/BitShifter.Shared.Kernel/BitShifter.Shared.Kernel.csproj", "Src/Shared/BitShifter.Shared.Kernel/"]
COPY ["Src/Modules/Identity/BitShifter.Modules.Identity.Domain/BitShifter.Modules.Identity.Domain.csproj", "Src/Modules/Identity/BitShifter.Modules.Identity.Domain/"]
COPY ["Src/Shared/BitShifter.Shared.Infrastructure/BitShifter.Shared.Infrastructure.csproj", "Src/Shared/BitShifter.Shared.Infrastructure/"]
COPY ["Src/Modules/Recipes/BitShifter.RecipeApp.Modules.Recipes.WebApi/BitShifter.Modules.Recipes.Api.csproj", "Src/Modules/Recipes/BitShifter.RecipeApp.Modules.Recipes.WebApi/"]
COPY ["Src/Modules/Recipes/BitShifter.RecipeApp.Modules.Recipes.Application/BitShifter.Modules.Recipes.Application.csproj", "Src/Modules/Recipes/BitShifter.RecipeApp.Modules.Recipes.Application/"]
COPY ["Src/Modules/Recipes/BitShifter.RecipeApp.Modules.Recipes.Domain/BitShifter.Modules.Recipes.Domain.csproj", "Src/Modules/Recipes/BitShifter.RecipeApp.Modules.Recipes.Domain/"]
COPY ["Src/Modules/Recipes/BitShifter.RecipeApp.Modules.Recipes.Infrastructure/BitShifter.Modules.Recipes.Infrastructure.csproj", "Src/Modules/Recipes/BitShifter.RecipeApp.Modules.Recipes.Infrastructure/"]
RUN dotnet restore "Src/Bootstrapper/BitShifter.Bootstrapper/BitShifter.Bootstrapper.csproj"
COPY . .
WORKDIR "/src/Src/Bootstrapper/BitShifter.Bootstrapper"
RUN dotnet build "BitShifter.Bootstrapper.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BitShifter.Bootstrapper.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BitShifter.Bootstrapper.dll"]