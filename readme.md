
# Clean Architecture with Dapper

I absolutely love the [Clean Architecture](https://www.youtube.com/watch?v=dK4Yb6-LxAk&t=851s) in .NET. However, it seems like most of the [templates available](https://github.com/jasontaylordev/CleanArchitecture) are tied to [Entity Framework](https://github.com/jasontaylordev/CleanArchitecture). Personally for most use cases, I prefer using [Dapper](https://dapper-tutorial.net).  This is therefore a Clean Architecture templae that uses Dapper as the Databse Layer, and a few other opinions.

## Features

### Health Checks

This template is built for a Cloud Native experience. And in the cloud, having the ability to determine the [health of your application](https://dev.to/schmittfelipe/cloud-native-monitoring-at-scale-application-s-health-17n7) is key.  The template gives you the health of your main application and database to start with, with a blueprint of how to extend this to other infrastructure you will be integrating with.

### Integration Tests

I wanted to make it dead easy to write and run integration tests

### Fluent Validation

### API Exception Filters

### Dapper ORM

### Fluent Migrations

## Installation

The solution depends on [DotNet 5](https://dotnet.microsoft.com/download/dotnet/5.0) so make sure you have it installd before running the following conversation:

```bash
  dotnet restore
  dotnet build
  cd src/api
  dotnet run
```

## Environment Variables

To run this project, you will need to add the following environment variables to your .env file

`API_KEY`

`ANOTHER_API_KEY`
