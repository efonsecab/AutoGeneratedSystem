# AutoGeneratedSystem
A sample app using C# Incremental Source Generators
C# Source Generatos allow you to inspect and enrich your code while compiling.
In this specific sample, we are using Incremental Generators to generate the code for Services, API Endpoints, and API Clients.
You will see some partial classes decorated with attributes such as
* `[ServiceOfEntity("{entityName}")]`
* `[ControllerOfEntity("{entityName}")]`
* `[ClientServiceOfEntity("{entityName}")]`

These attribute are being used in the Incremental Generators code to identify which classes the developer wants to automatically create the code for.

For the auto generation of the Blazor pages, currently, the process is a little bit different.
You need to create a file "AutoGeneratePages.txt" in the location
"/Pages/{entityName}"

The file need to have all of each CRUD action you want to autogenerated
A CRUD action would be any of these
* Create
* List
* Edit
* Delete

**Note**: Currently, only the Create functionality is implemented

After you have created the file, change the file's Build Action to "C# analyzer additional file", and compile your application.
The generator will create the .razor and .razor.cs files for each of the CRUD actions specified in the text file.
