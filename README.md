## What this is about?

This project help you to monitor your websites and send and alert to you if something goes wrong.

## Docker

If you want to run it on Docker. Here you go;

First create postgres;

```sh
docker run -d --name postgres -e POSTGRES_PASSWORD=123456789 postgres
```

Next run the app;

```sh
docker run -d --name monova-console -p 8080:80 -e MONOVA_CONNECTIONSTRING="Server=postgres;Port=5432;Database=monova;User Id=postgres;Password=123456789;" -e STRIPE_API_KEY=TYPE-STRIPE-API-KEY -e STRIPE_PUBLIC_KEY=TYPE-STRIPE-PUBLIC-KEY --link postgres:postgres monova/console
```

## Test

## Production
