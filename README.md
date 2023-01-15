# Cancun Hotel
## _Welcome to Cancun Hotel API !_

This api is responsable to make reservations for customers and provide available dates

## Dependencies 

| Package | README |
| ------ | ------ |
| Swashbuckle.AspNetCore | [https://learn.microsoft.com...](https://learn.microsoft.com/en-us/aspnet/core/tutorials/getting-started-with-swashbuckle?view=aspnetcore-7.0&tabs=visual-studio) |
| FluentValidation | [https://docs.fluentvalidation.net...](https://docs.fluentvalidation.net/en/latest/) |

## Getting Started

Cancun Hotel requires [.NET](https://dotnet.microsoft.com/en-us/download/dotnet/7.0) 7.0.102 to run. (Latest release date. January 10, 2023)

Install and test the dependencies. Run the commands on the project root path

```sh
dotnet build
dotnet test
```

After the step above without errors. Just running the API...

```sh
dotnet run --project .\CancunHotel\CancunHotel.Api.csproj
```

## Swagger and Endpoints

This API is based on OpenAPI and provides a documentation/playground. To use this you just need to open in or browser the link bellow:

http://localhost:3000/swagger/index.html

If you want try yourself without swagger you can run: 
### POST Example

```sh
curl --location --request POST 'http://localhost:3000/api/v1/Reservation' \
--header 'Content-Type: application/json' \
--data-raw '
{
  "guest": {
    "name": "Matheus",
    "email": "matheus@hotmail.com",
    "checkIn": true
  },
  "startDate": "2023-01-16",
  "endDate": "2023-01-19"
}
'
```

### PUT Example

```sh
curl --location --request PUT 'http://localhost:3000/api/v1/Reservation' \
--header 'Content-Type: application/json' \
--data-raw '{
    "id": "f35242cd-9fcd-4e79-91e7-a9475e57337f",
    "guest": {
        "checkIn": true,
        "name": "Matheus",
        "email": "matheus@hotmail.com"
    },
    "startDate": "2023-01-17",
    "endDate": "2023-01-19"
}
'
```

## License

MIT

**Free Software**
