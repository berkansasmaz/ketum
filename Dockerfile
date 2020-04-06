FROM microsoft/dotnet:2.2-sdk-alpine3.8 as builder
WORKDIR /source
RUN apk add --update nodejs nodejs-npm yarn
COPY . .
WORKDIR /source/src/Ketum.Web
RUN dotnet restore
RUN yarn install
RUN dotnet publish -c Release -o /app/

FROM microsoft/dotnet:2.2-aspnetcore-runtime-alpine3.8 as baseimage
RUN apk add --update nodejs nodejs-npm
WORKDIR /app
COPY --from=builder /app .
ENV ASPNETCORE_ENVIRONMENT=Production

EXPOSE 80
CMD [ "dotnet", "Ketum.Web.dll" ]