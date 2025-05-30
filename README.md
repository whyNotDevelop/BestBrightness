# BestBrightness Store Management System

[![.NET](https://img.shields.io/badge/.NET-7.0-blue)](https://dotnet.microsoft.com/)  
[![Blazor Server](https://img.shields.io/badge/Blazor-Server-blueviolet)](https://dotnet.microsoft.com/apps/aspnet/web-apps/blazor)  
[![EF Core](https://img.shields.io/badge/EF%20Core-7.0-green)](https://docs.microsoft.com/ef/core/)  
[![SQL Server](https://img.shields.io/badge/SQL%20Server-LocalDB-red)](https://docs.microsoft.com/sql/)  
[![GitHub Repo](https://img.shields.io/badge/GitHub%20Repo-whyNotDevelop/BestBrightness-lightgrey)](https://github.com/whyNotDevelop/BestBrightness)  

A web-based ASP.NET Core Blazor application for Best Brightness (cleaning-products retailer in Ophongolo, KZN) to manage inventory, employees, sales, and reporting.

---

## 📖 Table of Contents

- [Overview](#overview)  
- [Key Features](#key-features)  
- [Tech Stack](#tech-stack)  
- [Getting Started](#getting-started)  
  - [Prerequisites](#prerequisites)  
  - [Installation](#installation)  
  - [Database Setup](#database-setup)  
  - [Running the App](#running-the-app)  
- [Project Structure](#project-structure)  
- [Usage](#usage)  
- [Contributing](#contributing)  

---

## 📝 Overview

BestBrightness provides a centralized, real-time platform for:

1. **Inventory Management** (Products, stock levels, categories)  
2. **Employee Administration** (Admin configures Employees/Warehouse Managers; CRUD operations for Employee & Warehouse Manager accounts)  
3. **Sales Processing** (Cart, payment methods, change calculation)  
4. **Dashboards** (Admin, Sales, Warehouse) with metrics & reports  
5. **Printable Sales Slips** for customer records  

---

## ⭐ Key Features

- **Admin Dashboard**  
  - Add/edit/remove **Employees** & **Warehouse Managers**  
  - View total sales count & amount  
  - Generate Daily / Weekly / Monthly sales reports  

- **Sales Dashboard**  
  - Barcode lookup & add items to cart  
  - Choose payment method (Cash/Card) with automatic change calculation  
  - Record sales & auto-update inventory  
  - Real-time clock display  

- **Warehouse Dashboard**  
  - View products grouped by category  
  - Highlight low-stock items (<15 units)  
  - Toggle category views & see total inventory count  

- **Sales Slip**  
  - Printable receipt detailing transaction, items, totals, and cashier  

---

## 🛠️ Tech Stack

- **Framework:** ASP.NET Core 7.0, Blazor Server  
- **ORM:** Entity Framework Core  
- **Database:** SQL Server LocalDB (with sequences for Admin, Employee, and Product barcodes)  
- **UI:** Blazor components, Bootstrap 5  
- **DI & Config:** ASP.NET Core Dependency Injection, `appsettings.json`  

---

## 🚀 Getting Started

### Prerequisites

- [.NET 7.0 SDK](https://dotnet.microsoft.com/download)  
- SQL Server LocalDB  
- Git  

### Installation

1. **Clone the repo**  
   ```bash
   git clone https://github.com/whyNotDevelop/BestBrightness.git
   cd BestBrightness
