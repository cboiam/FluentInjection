# ImplementationDiscovery

[![Build](https://github.com/cboiam/ImplementationDiscovery/workflows/Build/badge.svg?branch=main)](https://github.com/cboiam/ImplementationDiscovery/actions?query=workflow%3ABuild)
[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=cboiam_ImplementationDiscovery&metric=alert_status)](https://sonarcloud.io/dashboard?id=cboiam_ImplementationDiscovery)

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
