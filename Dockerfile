FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base

WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Src/Bootstrapper/PixelDance.Bootstrapper/PixelDance.Bootstrapper.csproj", "Src/Bootstrapper/PixelDance.Bootstrapper/"]
COPY ["Src/Modules/Identity/PixelDance.Modules.Identity.Api/PixelDance.Modules.Identity.Api.csproj", "Src/Modules/Identity/PixelDance.Modules.Identity.Api/"]
COPY ["Src/Modules/Identity/PixelDance.Modules.Identity.Core/PixelDance.Modules.Identity.Core.csproj", "Src/Modules/Identity/PixelDance.Modules.Identity.Core/"]
COPY ["Src/Modules/Identity/PixelDance.Modules.Identity.Domain/PixelDance.Modules.Identity.Domain.csproj", "Src/Modules/Identity/PixelDance.Modules.Identity.Domain/"]
COPY ["Src/Shared/PixelDance.Shared.Infrastructure/PixelDance.Shared.Infrastructure.csproj", "Src/Shared/PixelDance.Shared.Infrastructure/"]
COPY ["Src/Shared/PixelDance.Shared.Abstractions/PixelDance.Shared.Abstractions.csproj", "Src/Shared/PixelDance.Shared.Abstractions/"]
COPY ["Src/Shared/PixelDance.Shared.ROP/PixelDance.Shared.ROP.csproj", "Src/Shared/PixelDance.Shared.ROP/"]
COPY ["Src/Shared/PixelDance.Shared.Kernel/PixelDance.Shared.Kernel.csproj", "Src/Shared/PixelDance.Shared.Kernel/"]
COPY ["Src/Modules/Recipes/PixelDance.RecipeApp.Modules.Recipes.WebApi/PixelDance.Modules.Recipes.Api.csproj", "Src/Modules/Recipes/PixelDance.RecipeApp.Modules.Recipes.WebApi/"]
COPY ["Src/Modules/Recipes/PixelDance.RecipeApp.Modules.Recipes.Application/PixelDance.Modules.Recipes.Application.csproj", "Src/Modules/Recipes/PixelDance.RecipeApp.Modules.Recipes.Application/"]
COPY ["Src/Modules/Recipes/PixelDance.RecipeApp.Modules.Recipes.Domain/PixelDance.Modules.Recipes.Domain.csproj", "Src/Modules/Recipes/PixelDance.RecipeApp.Modules.Recipes.Domain/"]
COPY ["Src/Modules/Recipes/PixelDance.RecipeApp.Modules.Recipes.Infrastructure/PixelDance.Modules.Recipes.Infrastructure.csproj", "Src/Modules/Recipes/PixelDance.RecipeApp.Modules.Recipes.Infrastructure/"]
RUN dotnet restore "Src/Bootstrapper/PixelDance.Bootstrapper/PixelDance.Bootstrapper.csproj"
COPY . .
WORKDIR "/src/Src/Bootstrapper/PixelDance.Bootstrapper"
RUN dotnet build "PixelDance.Bootstrapper.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PixelDance.Bootstrapper.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PixelDance.Bootstrapper.dll"]