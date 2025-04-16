# CDR API
## Technology choices
1. I have built the API using ASP.NET Core. The project is structured into a clean architectural design.
2. For ORM I used Entity framework since I'm already familiar with it and it's wildly used among all .NET projects.
3. For the implementation of endpoints, I chose to use a new library I hadn't used before but really wanted to try FastEndpoints.
4. As DB I used Microsoft SQL Server.
5. For testing, I went for XUnit, Shouldy, and NSubstitue since I was already familiar with all of these technologies and had used them before, and they are also very popular.

## Considerations / future enhancements
1. Make a database table for currency, not have it as enum.
2. Add proper logging (using for example serilog).
3. Have functional tests that test the endpoint calls.
4. More complex data validation during upload.
5. Have a CI/CD Pipeline.

## Assumptions
// - //