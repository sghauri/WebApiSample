   # <div align="center">&#xFDFD;</div>
#### <div align="center">BISMILLAH AR-RAHMAN AR-RAHEEM</div>
#### <div align="center">In the Name of ALLAH Most Gracious, Most Merciful</div>

## WebApiSample

This project is intended to try out .NET Core features and also implement few scenarios related to Web API.

#### 1. CreatedAtAction and CreatedAtRoute Examples

* See _Add_ endpoint in _TodoItemController_ for examples of _CreatedAtAction_ and _CreatedAtRoute_ action result.

#### 2. Middlewares

* See _Middlewares_ folder for different implementations of middleware.
* _SecurityMiddlewareFilter_ is an example of middleware filter.
 
#### 3. Injecting multiple implmentations of same interface using DI container
* See _Services/CustomerLogger_ file for multiple classes that implement same interface.
* In _Program.cs_, _builder.Services.TryAddEnumerable()_ method is used to inject all classes implmenting the same interface.
* In _TodoItemController_ constructor _IEnumerable_ parameter is used to receive all injected implementations.

#### 4. Entity Framework Core (SQLite)
* Entities defined in the project _Domain_.
* Entity Framework Core (using SQLite file database, code first approach) data context, migrations and repositories defined in the project _DAL_.

#### 5. OData (branch: feature/odata)
* _TodoItemV2Controller__ is intended for OData.
* All projects updated to use 'net8.0' SDK.
* Resources:
	* https://www.mongodb.com/developer/languages/csharp/query-with-odata-mongodb-efcore-provider-dotnet/
	* https://code-maze.com/aspnetcore-webapi-using-odata/	

##### Pending Items
* [branch: feature/db-context] Refractor code:
	1. ~~Separate Project for domain entities.~~
	2. ~~Separate Project for data (Repositories, Unit of Work, Db Context).~~
	3. Implement service to run migrations at application startup.
	4. Add index for 'Status' column in TodoItem table.