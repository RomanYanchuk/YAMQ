# docker build -t musical-quiz-api -f api.Dockerfile .
# docker run -d -p 8080:80 --name i-musical-quiz-api  musical-quiz-api 

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
WORKDIR /app

COPY ./Backend/MusicalQuiz/. ./
RUN dotnet publish MusicalQuiz.Main/MusicalQuiz.Main.csproj -c Release -o out


FROM mcr.microsoft.com/dotnet/aspnet:7.0
ENV ASPNETCORE_ENVIRONMENT=Production
WORKDIR /app
COPY --from=build-env /app/out/ .
CMD ASPNETCORE_URLS=http://*:$PORT dotnet MusicalQuiz.Main.dll
