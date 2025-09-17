# Interview Assignment – .NET Core + PostgreSQL

## 🚀 Setup Instructions

### Prerequisites
- [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download)
- [PostgreSQL 14+](https://www.postgresql.org/download/)

### Database Setup
1. Create PostgreSQL database:
   ```sql
   CREATE DATABASE "TemplatesDb";
   ```
2. Update the connection string in `src/Templates.Api/appsettings.json` if needed.
3. Apply migrations:
   ```bash
   dotnet ef database update --project src/Templates.Api
   ```

### Run the API
```bash
dotnet run --project src/Templates.Api
```

API will be available at:
- Swagger UI (development): https://localhost:5000/swagger

## ✅ Existing Functionality
- **Users**
  - Entity: `User` (`Id`, `FirstName`, `LastName`, `Email`)
  - CRUD endpoints at `/api/users`
  - Unique index on `Email` is configured in `AppDbContext`

## 📝 What the Candidate Needs to Add
The candidate should implement the Template Management feature.

### Requirements (to be implemented by the candidate)
1. Move business logic from `Controllers/UsersController` to `Services/UsersService`:

2. Add `Template` entity:
   - `Id` (int)
   - `Value` (string, required - valid scriban template)

3. Add `TemplatesController` with endpoints:
   - `PUT /api/templates` → Create a template
   - `Post /api/templates` → Updates a template
   - `GET /api/templates/{id}` → Get a template
   - `DELETE /api/templates/{id}` → Delete a template
   - `GET /api/templates/{id}/compile/{userId}` → Compiles a template for a user

4. Implementation:
   - Expand User entity with more information (for example: Street, City, Country, HouseNumber...)
   - Template Value field must be a valid scriban template, for example: "User {{ user.name }} lives in {{ user.city }}, {{ user.country }}"
   - Ensure that compile endpoint compiles given template using data of the specified user.
   - Implement `GET /api/templates/{id}/compile/{userId}/html` → Compiles the selected template for a specific user and generates HTML document using default styles indicated below.
   - Each user must be able to customize the styles of their HTML documents.
  
```css
h1 {
  font-family: "Arial";
  font-size: 16pt;
  font-weight: 700;
  text-decoration: underline;
  line-height: 1.15em;
}
h2 {
  font-family: "Arial";
  font-size: 12pt;
  font-weight: 700;
  line-height: 1.15em;
}

h3 {
  font-size: 10pt;
  font-weight: 700;
  line-height: 1.15em;
}

p {
  font-size: 10pt;
  font-weight: 400;
  line-height: 1.5em;
}
```

4. Deliverable:
   - Candidate should provide a link **GitHub Repo** with changes implemented

---

# Scriban Template API

A .NET 9 Web API project that allows dynamic HTML generation using [Scriban](https://github.com/scriban/scriban) templating, with support for user-specific styling.

## Features

- CRUD operations for **Users** and **Templates**
- Compile Scriban templates with user data (`/compile/{userId}`)
- Generate dynamic HTML documents (`/compile/{userId}/html`)
- Each user can customize their HTML **CSS styles**
- Input validation and exception handling
- AutoMapper and DTOs for clean mapping
- Swagger/OpenAPI support

## Technologies Used

- ASP.NET Core 9
- PostgreSQL
- Entity Framework Core
- Scriban (templating)
- AutoMapper
- Swagger (Swashbuckle)
- Visual Studio 2022

## Getting Started

### Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/)
- [PostgreSQL](https://www.postgresql.org/)
- [Visual Studio 2022](https://visualstudio.microsoft.com/)

### Setup

1. **Configure the connection string** in `appsettings.json`:

    ```json
    "ConnectionStrings": {
      "DefaultConnection": "Host=localhost;Port=yourport;Database=template_db;Username=postgres;Password=yourpassword"
    }
    ```

2. **Run the database migration**:

    ```powershell
    Update-Database
    ```

3. **Start the project**:

    ```bash
    dotnet run
    ```

    Or via Visual Studio (press `F5`).

## API Endpoints

### Users

- `GET /api/users`
- `GET /api/users/{id}`
- `POST /api/users`
- `PUT /api/users/{id}`
- `DELETE /api/users/{id}`

### Templates

- `GET /api/templates/{id}`
- `POST /api/templates`
- `PUT /api/templates/{id}`
- `DELETE /api/templates/{id}`
- `GET /api/templates/{id}/compile/{userId}`
- `GET /api/templates/{id}/compile/{userId}/html`

## Example Template

```text
<h1>User: {{ user.first_name }}</h1>
<p>Address: {{ user.street }}, {{ user.city }}, {{ user.country }}</p>
```

## Default Style

The default CSS style is embedded and used if a user has not specified their custom style.

```css
h1 {
  font-family: 'Arial';
  font-size: 16pt;
  font-weight: 700;
  text-decoration: underline;
  line-height: 1.15em;
}
```

## Project Structure

```
Templates.Api/
│
├── Controllers/
├── DTOs/
├── Entities/
├── Services/
├── Profiles/         # AutoMapper profiles
├── Utils/            # Utility classes (Scriban rendering, defaults)
├── Configuration/    # Dependency Injection setup
└── Data/             # DbContext and EF configuration
```

