EF Core Code First Example
This project demonstrates the basic operations of generating a database using EF Core Code First approach.

Technologies Used
- .NET 8
- Entity Framework Core - latest version, not specified
- Code First approach
- SQL Server database

Code Overview
The main code includes:

- BloggingContext - Defines DbContext, specifies entities to be mapped
- User, Blog, Post - Entity classes
- Program.cs - Main operation demo code
  - Add user
  - Query user
  - Update user
  - Delete user

Code First Steps
The Code First approach follows these main steps:

1. Define entity classes (POCO classes)
2. Create DbContext class
3. Specify connection string
4. Enable migrations and update database on application startup:
   - Add-Migration InitialCreate
   - Update-Database

This will create the database and tables based on the entity classes.

Usage
1. Update connection string in BloggingContext
2. Run the project, it will create the database and tables automatically
3. CRUD operations are demoed in Program.cs

This demo shows basic usage of EF Core Code First and can serve as a starting point for .NET projects using EF Core.

Please let me know if any section needs improvement or additions.

