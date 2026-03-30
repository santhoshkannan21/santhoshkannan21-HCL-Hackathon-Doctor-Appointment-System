# DoctorAppointmentSystem
# 🏢 Workspace Allocation System | HCL Hackathon 2026

## 🚀 Overview
This project is a high-performance Workspace Allocation and Booking System built during the HCL Hackathon (Mar 30, 2026). It ensures strict concurrency control to prevent double-booking of resources, providing a seamless experience for both Employees and Administrators.

## 🛠️ Technology Stack
* **Backend:** .NET 10 Web API, C#
* **Frontend:** Blazor Web App (Interactive Server Mode)
* **Database:** Entity Framework Core (InMemory Provider for rapid prototyping)
* **Security:** JWT (JSON Web Tokens) & BCrypt Password Hashing
* **Testing:** xUnit & FluentAssertions, Postman API Validation

## 🏗️ Architectural Highlights
* **Clean Architecture:** Strict separation of concerns (Controllers -> Services -> Repositories/DbContext).
* **Concurrency Shield:** Database-level composite constraints and Service Layer overlap validation using Enum TimeSlots.
* **Global Exception Handling:** Centralized `IExceptionHandler` returning RFC 7807 standard `ProblemDetails`.
* **Rate Limiting:** Fixed window limiter configured to protect endpoints from spam/DDoS attacks.

## 👥 Squad Members & Roles
* **[Santhosh Kannan M]:** Backend Architect (API, EF Core, Auth, Business Logic)
* **[Prasath G]:** Blazor Frontend Developer (UI Components, State Management)
* **[Yamuna E]:** UI/UX Stylist (CSS, Bootstrap, Layouts)
* **[Kajitha A]:** Data Engineer & QA (Seed Data generation, Postman Testing, Git Management)

## ⚙️ How to Run Locally
1. Clone the repository: `git clone <repository-url>`
2. Navigate to the API folder and run: `dotnet run`
3. Navigate to the Blazor folder and run: `dotnet run`
4. Use the provided Seed Data credentials to log in:
   * **Admin:** `admin@hcl.com` | Password: `Admin@123`
   * **Patient:** `rahul@test.com` | Password: `Pass@123`


