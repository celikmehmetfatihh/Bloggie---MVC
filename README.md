# Bloggie-MVC

Bloggie-MVC is an ASP.NET MVC web application for managing and publishing blog posts. Users can create, edit, like, and comment on blog posts, while admins manage posts, tags, and users through a dedicated panel.

## Features
- **Blog Post Management:**
  - Users can create, edit, and list blog posts.
  - Like and comment on blog posts.
- **Tag Management:**
  - Admins can add, edit, and list tags for categorizing posts.
- **User Management:**
  - Admins can manage user accounts.
- **Authentication & Authorization:**
  - Secure login/register with ASP.NET Identity.
  - Role-based access (User/Admin).
  - JWT Token for API authorization.
- **Image Management:** Upload and manage images for blog posts.
- **Responsive Design:** Mobile and desktop compatible.

## Technologies
- **Framework:** ASP.NET MVC (.NET 6/8)
- **Language:** C#
- **Database:** SQL Server 2022, Entity Framework Core
- **Authentication:** ASP.NET Identity
- **Authorization:** JWT Token
- **Design Pattern:** Repository Pattern
- **Frontend:** HTML, CSS, JavaScript, Bootstrap
- **Tools:** Visual Studio 2022, SSMS

## Prerequisites
- .NET SDK (e.g., .NET 6/8)
- Visual Studio 2022 (ASP.NET workload)
- SQL Server 2022
- SSMS

## Installation
1. Clone: `git clone https://github.com/celikmehmetfatihh/Bloggie-MVC.git`
2. Navigate: `cd Bloggie-MVC`
3. Open solution in Visual Studio.
4. Restore NuGet packages.
5. Set up `BloggieAuthDb` and `BloggieDb` in SQL Server (run EF migrations).
6. Update `appsettings.json` with connection strings:
   "ConnectionStrings": {
     "BloggieAuthDb": "Server=FATIh;Database=BloggieAuthDb;Trusted_Connection=True;",
     "BloggieDb": "Server=FATIh;Database=BloggieDb;Trusted_Connection=True;"
   }
