cd ../../
docker login registry.pixeldance.at
docker build -t registry.pixeldance.at/pxd-software/pixeldance.recipeapp/recipeapp .
docker push registry.pixeldance.at/pxd-software/pixeldance.recipeapp/recipeapp
cd Docs/Scripts
exit 0