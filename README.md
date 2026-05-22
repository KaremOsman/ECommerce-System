# 🚀 E-Commerce System (.NET 8 | N-Tier Architecture | Enterprise-Ready)

![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![C#](https://img.shields.io/badge/C%23-11-239120?style=for-the-badge&logo=c-sharp&logoColor=white)
![SQL Server](https://img.shields.io/badge/SQL_Server-CC2927?style=for-the-badge&logo=microsoft-sql-server&logoColor=white)
![Redis](https://img.shields.io/badge/Redis-DD0031?style=for-the-badge&logo=redis&logoColor=white)
![Bootstrap](https://img.shields.io/badge/Bootstrap-5-563D7C?style=for-the-badge&logo=bootstrap&logoColor=white)

---

# 📌 Overview

A scalable E-Commerce system built using ASP.NET Core MVC and RESTful Web API following N-Tier Architecture principles.

The project includes:

- Admin Dashboard
- Authentication & Authorization
- Product & Order Management
- Stripe Payment Integration
- Redis Caching
- RESTful APIs

---

# 🏗️ Architecture

```bash
E-Commerce Solution (8 Projects)

├── Core
│   ├── Domain
│   ├── Service
│   └── Services.Abstractions
│
├── Infrastructure
│   ├── Persistence
│   └── Presentation
│
├── AdminDashboard
│   ├── Controllers
│   ├── Models
│   ├── Views
│   ├── Services
│   ├── Helpers
│   ├── wwwroot
│   ├── appsettings.json
│   └── Program.cs
│
├── E-CommerceApp
│   ├── Extensions
│   ├── Factories
│   ├── wwwroot
│   ├── Properties
│   └── Dependencies
```

---

# ✨ Features

## 🔐 Authentication & Security
- JWT Authentication
- Authorization & Protected Endpoints
- Secure API Communication

## 🛒 E-Commerce Features
- Product Management
- Orders Management
- Shopping Cart
- Checkout System
- Stripe Payment Gateway

## ⚡ Performance
- Redis Cache Integration
- Optimized Database Queries

## 📡 API Features
- RESTful APIs
- Swagger Documentation
- Validation & Error Handling

---

# 🧰 Tech Stack

- ASP.NET Core MVC
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- Redis
- JWT
- Stripe
- Bootstrap
- LINQ

---

# ⚙️ Installation & Setup

## 1️⃣ Clone Repository

```bash
git clone https://github.com/YOUR_USERNAME/E-Commerce-System.git
```

---

## 2️⃣ Configure Database

Update connection strings inside:

```bash
appsettings.json
```

---

## 3️⃣ Apply Migrations

```bash
dotnet ef database update
```

---

## 4️⃣ Run The Project

```bash
dotnet run
```

---

# 📡 API Base URL

```bash
https://localhost:7026/api/
```

---

# 📸 Project Preview

## 🖥️ Admin Dashboard

| Home Page | Login | Products Management |
| :---: | :---: | :---: |
| ![Home](ScreenShots/admin-dashboard-home-page.jpg) | ![Login](ScreenShots/admin-dashboard-login-page.jpg) | ![Products](ScreenShots/products-management-home-page.jpg) |

---

## 🛠️ API & Swagger

| Swagger UI | Authentication | Products API |
| :---: | :---: | :---: |
| ![Swagger](ScreenShots/swagger-page.jpg) | ![Auth](ScreenShots/authentication-endpoints.jpg) | ![Products API](ScreenShots/products-endpoints.jpg) |

---

# 🚀 Future Improvements

- Docker Support
- CI/CD Pipeline
- Unit Testing
- Azure Deployment
- Role-Based Dashboard
- Email Notifications

---

# 👨‍💻 Author

## Mohamed Osman Mohamed

ASP.NET Core Developer

- Passionate about Backend Development & Scalable Systems
- Interested in Enterprise Application Architecture

---

# ⭐ Support

If you like this project, don't forget to give it a ⭐ on GitHub.
