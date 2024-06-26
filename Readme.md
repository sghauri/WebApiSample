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

##### Pending Items
* [27-June-2024] Use 'AutoMapper' to map Dto class to entity class for scenarios such as PUT endpoint.
	* see: https://code-maze.com/net-core-web-development-part5/

* Use Request body (json) for POST endpoint
* Tryout various dependency injection methodologies
* Inject configuration (from appsettings.json) to a client class (controller or service) 