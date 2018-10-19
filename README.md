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
2. Create an ApiSettings class that contains your authorization details.
3. Instantiate a new client.
4. Create a RestRequest.
5. Execute the RestRequest.

## Examples:

### Creating the RestRequest manually:

```csharp
var settings = new ApiSettings("host","accountsid","authtoken");
var client = new TempWorksClient(settings);
var request = new RestRequest("users", Method.GET);
var result = client.Execute<User>(request);
```
