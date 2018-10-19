# Specifications.Core
This is a specification pattern library.

## Installation:

You can install the Specifications.Core NuGet package from the .NET Core CLI using:

```
dotnet add package Specifications.Core
```

or from the NuGet package manager:

```
Install-Package Specifications.Core
```

## Usage:

1. Install package from Nuget.
2. Create a specification around your Use Case.

## Examples:

### Creating the RestRequest manually:

```csharp
var specification = new Specification<User>(x => x.Active);
```
