# GoCartWorld E-Commerce System

GoCartWorld is a comprehensive e-commerce platform designed to provide a seamless online shopping experience for both customers and administrators. Built primarily with C#, this project showcases a robust backend, dynamic frontend, and easy-to-use management features.

## Features

- **User Authentication**: Secure login and registration system for customers and admins.
- **Product Catalog**: Browse products with detailed descriptions, images, and categories.
- **Shopping Cart**: Add, remove, and update products in the cart.
- **Order Processing**: Place orders and track status.
- **Admin Dashboard**: Manage products, categories, orders, and customers.
- **Responsive Design**: Frontend built with HTML, CSS, and JavaScript for a smooth experience on all devices.

## Technologies Used

- **C#**: Backend logic, models, controllers, and business operations.
- **HTML & CSS**: Layout design and styling.
- **JavaScript**: Interactive features and client-side validation.
- **Database**: (Specify if using SQL Server, SQLite, or other)

## Getting Started

### Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download) installed
- IDE such as [Visual Studio](https://visualstudio.microsoft.com/) or [VS Code](https://code.visualstudio.com/)
- Database server (if applicable)

### Installation

1. **Clone the repository**
   ```bash
   git clone https://github.com/Nourhan-Ziada/GoCartWorldE-CommerceSystem.git
   ```

2. **Set up the database**
   - Update the connection string in `appsettings.json` (or relevant config file).
   - Run migrations or seed data as necessary.

3. **Run the project**
   ```bash
   dotnet run
   ```
   Or use your IDE's run/build features.

### Usage

- Access the application via `http://localhost:5000` (or the configured port).
- Register as a user or log in as admin to manage the system.

## Project Structure

- `Controllers/` - Handles HTTP requests and routes.
- `Models/` - Entity definitions.
- `Views/` - Frontend pages (HTML, CSS, JS).
- `Services/` - Business logic and data access.
- `wwwroot/` - Static assets.

## Contributing

Contributions are welcome! Please open issues or submit pull requests for improvements or fixes.

## Contact

For questions or feedback, contact [Nourhan-Ziada](https://github.com/Nourhan-Ziada).
