# Yet another shocase szenario

Yes, in fact it's nothing more then a ordinary CRUD base mini application and <br>
sure it's way too much abstractions with uneseccary details in it, <br>
but the only purpose of this repo is to show my technical knowledge about state of the art, "Modern" Webdevelopment and make my girlfriend happy with a resilient, beautiful and selfhosted application to store her favorite recipes.

> Note: This project was always developed with CleanCode and CleanArchitecture in mind.

## Short project description

The project contains 2 modules.

1. RecipeModule<br>
   The most important module is the recipe module.
   It implements all operations with CQRS pattern but not down to the database (e.g. ReadSide=NoSql & WriteSide=Sql). Because it is not yet necessary.
2. IdentityModule<br>
   In the identity module i implement a functional approach with my custom Result class in PixelDance.Shared.ROP. It simplifies my error handling with RailwayOrientedProgramming. If you question what it is, it's a simple Monad with usefull operations on it and it should eliminate null handling. For further reading on the RailwayOrientedProgramming concept, see [Scott Wlaschin](https://fsharpforfunandprofit.com/rop/)

## General

To "DRY" my query logic and push it down to the domain layer, i use a pretty awesome project from Steve Smith named [Ardalis.Specification](https://github.com/ardalis/Specification). It's uses the well known specification pattern and comes with a generic ef-core repository.

## Folder and Module structure

```
PixelDance.RecipeApp/
|
|– Bootstrapper/
|   |- WebApi
|
|– Frontend/
|   |– pixeldance-materialize // Angular frontend application
|
|- Modules
|   |- Identity
|   |- Recipes
|
|- Shared
|   |- Abstractions
|   |- Infrastructure
|   |- Kernel
|   |- ROP
```

## Next steps

To reach a real-worl scenario messaging infrastructure would be required to decouple my recipe and category "AggregateRoot", but for the time being, it's nothing more then a violation from KISS principle.

My first thoughts would be either a Reactive approach with a self-made MessageBus that can be found on my Github profile [PixelDance.ReactiveBus](https://github.com/MarkusGnigler/PixelDance.ReactiveBus) or use IMHO a overkill solution like RabbitMq or somthing similar.

## A short breav about my used technologies and libraries

1. Backend

   - ASP.Core
   - EfCore
   - SeriLog
   - MediatR
   - AutoMapper
   - Ardalis.Specification
   - FluentAssertions

2. Frontend

   - @angular
   - @nrwl/nx
   - @ngrx
   - rxjs
   - @angular/material
   - @angular/flex-layout
   - ngx-editor

## Build and run with docker

1. Preparte database
   To run this application the connectionstring in **appsetting.json/EfCore:ConnectionString** must be eqal to the db.env credentials.

2. Preparte identity
   To generate a secure JWT token add a secret token to **appsetting.json/TokenKey**

3. Start infrastructure with docker-compose<br>
   Run on root folder the following command to host a local instance of the application with all neseccary infrastructure

   ```
   $ docker-compose up --build
   ```
