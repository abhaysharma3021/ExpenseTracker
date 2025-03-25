# ExpenseTracker

## Overview
This is an Expense Tracker API for Roadmap.sh [challenge](https://roadmap.sh/projects/expense-tracker-api), made using ASP.NET Core 9. It allows users to track their expenses efficiently with various features like categorization and budget management.

This project follows the Domain-Driven Design (DDD) pattern with a structured architecture that includes Repository and Services layers. Additionally, it implements the CQRS pattern using MediatR for handling commands and queries efficiently.
This is an Expense Tracker API, made using ASP.NET Core 9. It allows users to track their expenses efficiently with various features like categorization, reporting, and budget management.

## Project URL
[Project Page](https://github.com/abhaysharma3021/ExpenseTracker.git)

## Installation

### Prerequisites
- .NET 9 SDK
- Docker Desktop

### Steps to Run the Project
1. Clone the repository:
   ```sh
   git clone https://github.com/abhaysharma3021/ExpenseTracker.git
   ```
2. Navigate to the project directory:
   ```sh
   cd ExpenseTracker
   ```
3. Install dependencies:
   ```sh
   dotnet restore
   ```
4. Setup Database in Docker:
   ```sh
   docker compose up -d
   ```
5. Apply Migrations
   ```sh
   dotnet ef database update --context ApplicationDbContext --project ExpenseTracker.Infrastructure
   ``` 
6. Build the application
   ```sh
   dotnet build
   ```
8. Run the application
   ```sh
   dotnet run
   ```

## API Endpoints

### Authentication
- **POST /api/Auth/register** - Register a new user.
- **POST /api/Auth/login** - Authenticate a user and retrieve a token.

### Categories
- **GET /api/Categories** - Retrieve all categories.
- **POST /api/Categories** - Create a new category.
- **GET /api/Categories/{id}** - Get category by ID.

### Expenses
- **GET /api/Expenses** - Retrieve all expenses.
- **POST /api/Expenses** - Create a new expense.
- **GET /api/Expenses/{id}** - Retrieve a specific expense by ID.
- **PUT /api/Expenses/{id}** - Update an existing expense.
- **DELETE /api/Expenses/{id}** - Delete an expense by ID.
- **GET /api/Expenses/{userid}/filtered** - Retrieve filtered expenses for a user with optional date range.

### Users
- **GET /api/Users** - Retrieve all users.
- **POST /api/Users** - Create a new user.
- **GET /api/Users/{id}** - Retrieve user details by ID.
- **PUT /api/Users/{id}** - Update user information.
- **DELETE /api/Users/{id}** - Delete a user by ID.


## Usage
Use any API client (like Postman) to interact with the API endpoints.
OR
API will be available at [https://localhost:7278/scalar/v1](https://localhost:7278/scalar/v1)

## Technologies Used
- ASP.NET Core 9
- Entity Framework Core
- SQL Server
- MediatR (CQRS Pattern)
- Repository and Service Pattern (DDD)

## Contributing
If you'd like to contribute, please fork the repository and submit a pull request with your improvements.

## License
MIT License

## Feedback & Support
If you have any issues or suggestions, feel free to open an issue on the GitHub repository or contact me directly.

---
Made with ❤️ by Abhay Sharma

