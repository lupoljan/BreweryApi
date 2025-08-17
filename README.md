# 🍺 Brewery API

This is a sample enterprise-style API built with **ASP.NET Core**, **Entity Framework Core**, and **SQLite**.  
It integrates with the [Open Brewery DB](https://www.openbrewerydb.org/documentation) to fetch brewery data and provides search, sorting, autocomplete, and caching capabilities.

## 🚀 Features

- **RESTful Endpoints** for breweries
- **Data persistence** using SQLite + EF Core / in-memory storage
- **AutoMapper** for DTO <-> Entity mapping
- **Dependency Injection** throughout services and repositories
- **Sorting** strategies (Name, City, Distance – via Strategy Pattern)
- **Search + Autocomplete** functionality
- **Caching** results for 10 minutes
- **Global Exception Handling** with error code mapping
- **Logging** (built-in + custom middleware)
- **API Versioning** (`v1`) <!-- - **Authentication & Security** via JWT (Bearer tokens) -->
- **SOLID Principles** applied for clean architecture


## Install Dependencies
```
dotnet restore
```
## Run the Application
```
dotnet run
```

## Example Requests
### Get all breweries

```
GET /api/v1/breweries
```
### Search with autocomplete
```
GET /api/v1/breweries?search=dog
```
### Sort by city
```
GET /api/v1/breweries?sort=city
```
###  Sort by distance (with user location)
```
GET /api/v1/breweries?sort=distance&latitude=40.71&longitude=-74.25
```