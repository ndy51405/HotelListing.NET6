# HotelListing.NET6
Udemy ASP.NET Core Web API

This is the ASP.NET Core Web API project from Udemy lecture 'Ultimate ASP.NET Core Web API Development Guide' by Trevoir Williams.
Visual Studio with .net 6.0 is used in this project.

In chapter1, basic RESTful API concept is introduced.

In chapter2, we installed VS2022 and postman.

In chapter3, we created the project and did some basic configuration such like:
- Introducing Swagger
- Compare Controllers and Minimal API
- Setup CORS
- Setup Serilog and Seq, and introduce how to read logs with Seq
- Create remote git repository on GitHub and push commits to remote branch

In chapter4, Entity Framework Core is setupped. 
Domain entity is created and migrated to local SQL Server database in code-first way by using `add-migration` and `update-database`.

In chapter5, we created CountriesController by using scaffolding provided by Visual Studio. Then we test GET, POST, PUT and DELETE using Postman.

In chapter6, best practice of Web API is introduced. 
Though the API worked fine just with scaffolding codes, there's something we'd like to improve 
such as overposting attack, too much information exposed in controller, poor maintainability and repeated codes needed to be fixed.
- Overposting attack: DTO(Data Transfer Object) is introduced. Acting as view model, only a few properties are exposed to end user
to prevent some key property being accessed by end user such as id. 
Additional, DTO hide some redundant property like Country in Hotel from returning.
By using AutoMapper the third-party nuget package, we map from DTO to domain entity and in vise versa.
- Too much information exposed in controller, poor maintainability and repeated codes is solved by introducing repository pattern.
IGenericRepository, GenericRepository, CountryRepository and ICountryRepository is created.
