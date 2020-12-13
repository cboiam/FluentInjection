# FluentInjection

Fluent Injection is a helper library that extends the options for Microsoft.Extensions.DependencyInjection with a method to discover the implementations of your service and inject it.

## Example with simple interface

```c#
public void ConfigureServices(IServiceCollection services)
{
   //... other services
   service.RegisterImplementationsOf<IExampleInterface>().AsScoped();
}
```

## Example with generic interface

```c#
public void ConfigureServices(IServiceCollection services)
{
   //... other services
   service.RegisterImplementationsOf(typeof(IExampleInterface<,>)).AsTransient();
}
```