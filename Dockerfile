#pobranie obrazu .net sdk 6.0 
FROM mcr.microsoft.com/dotnet/sdk:6.0
#skopiowanie plików i folderów z obecnej ścieżki do folderu /app w powstającym obrazie
COPY . /app
#ustawienie folderu /app w powstającym obrazie jako ścieżki roboczej
WORKDIR /app
#pobranie brakujących paczek Nugget
RUN dotnet restore
#wykonanie publisha do folderu /publish ustawiając projekt movieRental.Api jako projekt startowy apliakcji  
RUN dotnet publish ./movieRental.Api/movieRental.Api.csproj -o /publish/
#ustawienie folderu /publish jako ścieżki roboczej
WORKDIR /publish
#ustawienie komendy uruchamiającej aplikację .Net wraz z ustawieniem zmiennej środowiskowej PORT ($ oznacza początek nazwy zmiennej środowiskowej)
CMD ASPNETCORE_URLS=http://*:$PORT dotnet movieRental.Api.dll
