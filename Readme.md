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
* Use Request body (json) for POST endpoint
* Tryout various dependency injection methodologies
* Inject configuration (from appsettings.json) to a client class (controller or service) 