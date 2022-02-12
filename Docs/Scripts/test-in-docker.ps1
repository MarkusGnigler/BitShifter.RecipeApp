cd ../..
docker build --tag recipetests -f Dockerfile.Test .
docker run --rm -v $PWD/coveragereport:/source/coveragereport recipetests
cd Docs/Scripts
exit 0