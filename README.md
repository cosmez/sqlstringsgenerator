# SqlStrings Source Generator

C# Source Generator that transforms .sql files in your project into statically-typed 
C# classes and fields.

## Installation

```bash
dotnet add package Emm.SqlStringGenerator 
```

### Example

create sql file

```sql 
--namespace: Org.MyAppNamespace
--class: Queries
--classModifier: public

--name: CountTable
SELECT COUNT(*) FROM Table

--name: DeleteTable
DELETE FROM Table WHERE Id = @Id
```

include in your .csproj
```xml
<ItemGroup>
    <AdditionalFiles Include="Queries.sql" /> 
</ItemGroup>
```

use in your code
```csharp
Console.WriteLine(Org.MyAppNamespace.Queries.CountTable);
//or 
Connection.Execute(Org.MyAppNamespace.Queries.DeleteTable, new { Id = 5 });
```
