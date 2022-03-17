cd ../..

# docker login

docker buildx build `
    --platform linux/amd64,linux/arm/v7,linux/arm64 `
    --tag registry.pixeldance.at/pxd-software/pixeldance.recipeapp/recipeapp `
    --push `
    .

exit 0