# BestBrightness Store Management System

A web-based application for Best Brightness (cleaning‐products retailer in Ophongolo, KZN) to manage inventory, employees, sales, and reporting. Built with ASP.NET Core Blazor, Entity Framework Core, and SQL Server LocalDB.

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
- [License](#license)  

---

## 📝 Overview

BestBrightness is an ASP.NET Core Blazor application designed to streamline day-to-day operations of a local cleaning-products retailer. It provides:

- Centralized **inventory management** (products, stock levels, categories)  
- **Employee administration** (roles, credentials, activity tracking)  
- **Sales processing** with dynamic **cart**, **payment**, and **change calculation**  
- **Dashboards** for Admin, Sales, and Warehouse views, including real-time metrics and reports  
- Printable **sales slips** for customer records  

---

## ⭐ Key Features

1. **Admin Dashboard**  
   - Manage employees (add/update/remove)  
   - View total sales count and amount  
   - Generate Daily / Weekly / Monthly sales reports  

2. **Sales Dashboard**  
   - Add items to cart by barcode lookup  
   - Select payment method (Cash/Card) with automatic change calculation  
   - Record sales and update product quantities  
   - Real-time clock display  

3. **Warehouse Dashboard**  
   - View products grouped by category  
   - Highlight low-stock items (<15 units)  
   - Toggle category views and see total product count  

4. **Sales Slip Page**  
   - Printable slip showing sale details, items, totals, and cashier info  

---

## 🛠️ Tech Stack

- **Framework:** ASP.NET Core 7.0, Blazor Server  
- **ORM:** Entity Framework Core  
- **Database:** SQL Server LocalDB (with sequences for IDs & barcodes)  
- **UI:** Blazor components, Bootstrap 5  
- **Authentication:** Simple credential-based login (Admin & Employee)  
- **DI & Configuration:** ASP.NET Core Dependency Injection, `appsettings.json`  

---

## 🚀 Getting Started

### Prerequisites

- [.NET 7.0 SDK](https://dotnet.microsoft.com/download)  
- SQL Server LocalDB (installed with Visual Studio or separately)  
- Git (for cloning)  

### Installation

1. **Clone the repo**  
   ```bash
   git clone https://github.com/whyNotDevelop/BestBrightness.git
   cd BestBrightness
