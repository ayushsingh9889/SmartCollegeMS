# 🎓 Smart College Management System

A comprehensive web application built using **ASP.NET Core 8 MVC** and **MySQL** to manage student records, faculty details, and daily attendance. Designed to solve real-world college administration problems with a centralized, secure dashboard.

---

## 📋 Table of Contents
- [Features](#-features)
- [Technology Stack](#-technology-stack)
- [Architecture](#-architecture)
- [Database Schema](#-database-schema)
- [Setup Instructions](#-setup-instructions)
- [Screenshots](#-screenshots)
- [Viva Q&A Preparation](#-viva-qa-preparation)
- [Author](#-author)

---

## ✨ Features

### 🔐 Secure Authentication
- Admin registration and login using ASP.NET Core Identity.
- Password hashing (no plain-text passwords).
- Protected routes using `[Authorize]` attribute.

### 📊 Admin Dashboard
- Real-time statistics: Total Students, Total Faculty, Present/Absent today.
- Interactive **Chart.js** bar chart showing last 7 days of attendance.
- Recently added Students and Faculty lists.

### 👨‍🎓 Student Management
- Complete **CRUD** operations (Create, Read, Update, Delete).
- **Search** by Name or Student ID.
- **Filter** by Department.
- Server-side validation using Data Annotations.
- Prevents duplicate Student IDs (Unique Index).

### 👨‍🏫 Faculty Management
- Complete CRUD operations.
- Validated email and phone fields.

### 📅 Attendance Management
- Date picker to select attendance day.
- Bulk toggle Present/Absent for all students.
- Prevents duplicate attendance (1 student = 1 record per day).
- Edit saved attendance anytime.

### 📈 Reports & Export
- Filter attendance by date range.
- View detailed attendance reports.
- **Download as CSV** (opens in Excel).

---

## 🛠️ Technology Stack

| Layer | Technology |
|---|---|
| **Backend** | C# (.NET 8), ASP.NET Core MVC |
| **Database** | MySQL 8.0 |
| **ORM** | Entity Framework Core + Pomelo.EntityFrameworkCore.MySql |
| **Authentication** | ASP.NET Core Identity |
| **Frontend** | HTML5, CSS3, Razor Views, Bootstrap 5 |
| **Charts** | Chart.js |
| **Icons** | Bootstrap Icons |
| **IDE** | Visual Studio Code |
| **Version Control** | Git & GitHub |

---

## 🏗️ Architecture

This project follows the **MVC (Model-View-Controller)** design pattern:
