# ImplementationDiscovery

[!Build(https://github.com/cboiam/ImplementationDiscovery/workflows/Build/badge.svg?branch=main)]
[![Quality gate](https://sonarcloud.io/api/project_badges/quality_gate?project=cboiam_ImplementationDiscovery)](https://sonarcloud.io/dashboard?id=cboiam_ImplementationDiscovery)

Implementation Discovery is a helper library that extends the options of Microsoft.Extensions.DependencyInjection with functions for discovering the implementations of your service and injecting it.

## Example with simple interface

```c#
public void ConfigureServices(IServiceCollection services)
{
   //... other services
   service.MapImplementationsOf<IExampleInterface>().AsScoped();
}
```

## Example with generic interface

```c#
public void ConfigureServices(IServiceCollection services)
{
   //... other services
   service.MapImplementationsOf(typeof(IExampleInterface<,>)).AsTransient();
}
```

You can inject them with `AsTransient()`, `AsScoped()` or `AsSingleton()`.
