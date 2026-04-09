#  Sole Inventory — Shoe Business Inventory Management System

![C#](https://img.shields.io/badge/Language-C%23-blue)
![.NET](https://img.shields.io/badge/Framework-.NET%208-purple)
![Status](https://img.shields.io/badge/Status-Completed-brightgreen)
![GitHub](https://img.shields.io/badge/GitHub-Markyute-black)

---

##  Developer Information

| Field | Details |
|---|---|
| **Full Name** | John Mark C. Idanan |
| **GitHub Username** | [@Markyute](https://github.com/Markyute) |
| **Year & Section** | 3rd Year — BSIT-3A |
| **School** | Bicol University Polangui |
| **Project Type** | Midterm Exam — C# IT Elective 2 |
| **Date Submitted** | April 9, 2026 |

---

##  Project Description

**Sole Inventory** is a CLI-based Inventory Management System built with **C# (.NET 8)** as my Midterm Exam output for our It Elective 2. I chose a **shoe business** as the theme because it is a realistic and practical business scenario that allowed me to fully demonstrate all OOP principles and inventory features required by the exam.

The system manages everything a shoe store would need — from tracking products by brand, size, and color, to managing suppliers and categories, monitoring stock levels, and computing the total value of inventory.

---

##  Features Implemented

| # | Feature | Status |
|---|---|---|
| 1 | Add Category | ✅ Done |
| 2 | Add Supplier | ✅ Done |
| 3 | Add Product | ✅ Done |
| 4 | View All Products | ✅ Done |
| 5 | Search Product (by Name, Brand, or ID) | ✅ Done |
| 6 | Update Product | ✅ Done |
| 7 | Delete Product | ✅ Done |
| 8 | Restock Product | ✅ Done |
| 9 | Deduct Product Stock | ✅ Done |
| 10 | View Transaction History | ✅ Done |
| 11 | Show Low Stock Products | ✅ Done |
| 12 | Compute Total Inventory Value | ✅ Done |
|  | User Login System (Bonus) | ✅ Done |
|  | Sample Data Initialization (Bonus) | ✅ Done |

---

##  Project Structure

```
ShoeInventory/
│
├── Models/
│   ├── Product.cs           → Shoe product with brand, size, color, price, qty
│   ├── Category.cs          → Shoe categories (Sneakers, Boots, Sandals, etc.)
│   ├── Supplier.cs          → Supplier details with contact information
│   ├── User.cs              → User model for login and authentication
│   └── TransactionRecord.cs → Logs every restock and deduct transaction
│
├── Services/
│   ├── InventoryService.cs  → Core inventory logic and operations
│   ├── AuthService.cs       → User authentication and session management
│   └── ConsoleHelper.cs     → Console UI formatting and input utilities
│
├── Program.cs               → Main entry point and menu-driven interface
├── ShoeInventory.csproj     → .NET 8 project configuration file
├── .gitignore               → Git ignore rules
└── README.md                → Project documentation (this file)
```

---

##  OOP Concepts Applied

| Concept | Where Applied |
|---|---|
| **Classes & Objects** | All 5 models and service classes |
| **Encapsulation** | Private fields with public properties in every model |
| **Constructors** | Parameterized constructors in all 5 models |
| **Access Modifiers** | `private`, `public`, `static` used appropriately throughout |
| **Properties** | Get/set with validation logic in every model |
| **Methods** | Separated into service classes for clean, reusable logic |
| **Exception Handling** | `try-catch` blocks in all menu operations |
| **Collections** | `List<T>` used exclusively — no database |

---

##  How to Run

### Requirements
- [.NET SDK 8.0 or higher](https://dotnet.microsoft.com/download)
- Visual Studio 2022/2026 or VS Code

### Steps
```bash
# 1. Clone the repository
git clone https://github.com/Markyute/ShoeInventory.git

# 2. Navigate into the project folder
cd ShoeInventory

# 3. Run the application
dotnet run
```

Or open `ShoeInventory.csproj` in Visual Studio and press **F5**.

---

##  Default Login Accounts

| Role | Username | Password |
|------|----------|----------|
| Admin | `admin` | `admin123` |
| Staff | `staff1` | `staff123` |

---

##  Sample Data

The system loads sample shoe data automatically on first login to demonstrate all features:

- **5 Categories** — Sneakers, Boots, Sandals, Formal, Kids
- **3 Suppliers** — Nike Philippines, Adidas Distrib. PH, Local Crafts Co.
- **6 Products** — Nike Air Max, Adidas Ultraboost, Chelsea Boot, and more

---

##  Personal Note

This project was built entirely by me, **John Mark C. Idanan**, as part of our Midterm Examination in IT Elective 2. 

Building this project helped me better understand how encapsulation, constructors, and access modifiers work together to create clean and maintainable code. The part I found most challenging was designing the `TransactionRecord` model to properly log every stock movement, and making sure the `InventoryService` correctly handles edge cases like insufficient stock or duplicate entries.

I am proud of the login system, the colorful console UI, and the fact that all 12 required features are fully functional.

---

##  Tech Stack

- **Language:** C# (.NET 8)
- **Storage:** In-memory `List<T>` — no database used
- **IDE:** Visual Studio 2026
- **Version Control:** Git & GitHub

---

##  License

This project was created for academic purposes as a Midterm Exam output at **Bicol University Polangui**, BSIT-3A, Academic Year 2025–2026.

---

*Made with dedication by **John Mark C. Idanan** — BSIT-3A, Bicol University Polangui* 