<p align="center">
  <h1 align="center">🌍 Tourism-API</h1>
  <p align="center">Tourism & Booking Management System</p>
</p>

<p align="center">
  <!-- Core -->
  <img src="https://img.shields.io/badge/.NET-9-512BD4?logoColor=white" />
  <img src="https://img.shields.io/badge/C%23-Modern-blue?logo=csharp&logoColor=white" />
  <img src="https://img.shields.io/badge/Architecture-Clean-success" />

  <!-- Data -->
  <img src="https://img.shields.io/badge/ORM-Entity%20Framework%20Core-6DB33F" />
  <img src="https://img.shields.io/badge/EF-Fluent%20API-important" />
  <img src="https://img.shields.io/badge/Database-Azure%20SQL-0078D4?logo=microsoftsqlserver&logoColor=white" />

  <!-- Cloud -->
  <img src="https://img.shields.io/badge/Azure-App%20Service-0078D4?logo=microsoftazure&logoColor=white" />
  <img src="https://img.shields.io/badge/CI%2FCD-GitHub%20Actions-2088FF?logo=githubactions&logoColor=white" />

  <!-- Security & Docs -->
  <img src="https://img.shields.io/badge/Auth-JWT-orange?logo=jsonwebtokens&logoColor=white" />
  <img src="https://img.shields.io/badge/API-Swagger-85EA2D?logo=swagger&logoColor=black" />
</p>

---

## ✨ Overview

**Tourist** is an enterprise-grade **.NET 9 RESTful API** built using **Clean Architecture**, **Entity Framework Core**, and **Fluent API** for precise database modeling and configuration.

The system allows users to:
- 🌆 Discover cities and tourist destinations
- ✈️ Book trips and experiences
- 🏨 Manage hotel reservations
- 💳 Process secure payments
- ⭐ Leave reviews and ratings

Built with scalability, maintainability, and real-world production readiness in mind.

---

## 🏗️ Architecture & Data Modeling

<p>
  <img src="https://img.shields.io/badge/Pattern-Repository-blue" />
  <img src="https://img.shields.io/badge/Pattern-Unit%20of%20Work-purple" />
  <img src="https://img.shields.io/badge/Pattern-Dependency%20Injection-green" />
</p>

### Clean Architecture Layers

- **API (Presentation Layer)**  
  Controllers, JWT authentication, Swagger documentation.

- **Application Layer**  
  Business rules, services, DTOs, and interfaces.

- **Domain Layer**  
  Core entities and domain logic.

- **Persistence Layer**  
  Entity Framework Core, DbContext, Migrations, and **Fluent API configurations**.

### 🧩 Entity Framework & Fluent API

- Entity relationships configured using **Fluent API**
- Explicit control over:
  - Table mappings
  - Relationships & constraints
  - Indexes
  - Cascade behaviors
- No reliance on Data Annotations for complex rules

---

## 🚀 Features

<p>
  <img src="https://img.shields.io/badge/Feature-City%20Discovery-brightgreen" />
  <img src="https://img.shields.io/badge/Feature-Trip%20Booking-blue" />
  <img src="https://img.shields.io/badge/Feature-Hotel%20Management-orange" />
  <img src="https://img.shields.io/badge/Feature-Payments-purple" />
  <img src="https://img.shields.io/badge/Feature-Reviews-yellow" />
</p>

- City & trip discovery
- Secure multi-step booking
- Real-time hotel availability
- Payment gateway ready
- Review & rating system

---

## 📂 Project Structure
```
Tourist/
├── Tourist.API
├── Tourist.Application
├── Tourist.Models
└── Tourist.Persistence
```
⚙️ Getting Started
bash
Copy code
git clone https://github.com/Omar-Suliman-T/Tourism-API.git
dotnet ef database update --project Tourist.Persistence --startup-project Tourist.API
dotnet run --project Tourist.API
☁️ Deployment
<p> <img src="https://img.shields.io/badge/Hosted%20On-Azure-0078D4?logo=microsoftazure&logoColor=white" /> </p>
