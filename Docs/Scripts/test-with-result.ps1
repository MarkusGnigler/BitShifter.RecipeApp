cd ../..

# dotnet test --logger "trx;LogFileName=TestResults.trx"
# dotnet test --logger "trx;LogFileName=TestResults.trx" /p:CollectCoverage=true /p:CoverletOutputFormat=cobertura
# dotnet test --logger "cobertura;LogFileName=coverage.cobertura.xml" --collect:"XPlat Code Coverage" /p:CollectCoverage=true /p:CoverletOutputFormat=cobertura
dotnet test --collect:"XPlat Code Coverage"

reportgenerator `
    -reports:"**\coverage.cobertura.xml" `
    -targetdir:"coveragereport" `
    -reporttypes:Html

cd Docs/Scripts

exit 0