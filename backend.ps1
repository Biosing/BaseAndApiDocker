dotnet dev-certs https -ep %USERPROFILE%\.aspnet\https\aspnetapp.pfx -p password
dotnet dev-certs https --trust

docker-compose -f "docker-compose.yml" stop
docker-compose -f "docker-compose.yml" rm --force
docker-compose -f "docker-compose.yml" up --build doc-web-api db