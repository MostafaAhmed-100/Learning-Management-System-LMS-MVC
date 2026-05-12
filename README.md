<div align="center">

# 🎓 Learning Management System (LMS)

![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-8.0-512BD4?style=for-the-badge&logo=dotnet)
![EF Core](https://img.shields.io/badge/EF%20Core-Latest-512BD4?style=for-the-badge&logo=dotnet)
![SQL Server](https://img.shields.io/badge/SQL%20Server-Latest-CC2927?style=for-the-badge&logo=microsoftsqlserver)
![Bootstrap](https://img.shields.io/badge/Bootstrap-5.3-7952B3?style=for-the-badge&logo=bootstrap)
![Identity](https://img.shields.io/badge/ASP.NET%20Identity-✓-green?style=for-the-badge)

نظام متكامل لإدارة العمليات الأكاديمية، مبني بـ ASP.NET Core MVC

</div>

---

## 📌 نظرة عامة

نظام إدارة تعليمي يربط بين الطلاب، الكورسات، والمعيدين مع نظام تسجيل (Enrollment) ذكي. تم بناؤه بمعمارية احترافية تعتمد على Repository Pattern و Service Layer مع نظام مصادقة وصلاحيات كامل.

---

## ✨ الميزات

- ✅ Full CRUD لجميع الكيانات (Students, Courses, Instructors, Enrollments)
- ✅ Repository Pattern & Service Layer
- ✅ Server-Side Validation بـ Data Annotations
- ✅ Many-to-Many Enrollment System مع منع التكرار
- ✅ ASP.NET Core Identity (Authentication & Authorization)
- ✅ Role-Based Access Control (Admin, Instructor, Student)
- ✅ Password Hashing بـ ASP.NET Identity
- ✅ Custom Request Logging Middleware
- ✅ Custom Exception Filter
- ✅ Responsive UI بـ Bootstrap 5 RTL

---

## 👥 الأدوار والصلاحيات

| الدور | الصلاحيات |
|-------|-----------|
| Admin | كامل الصلاحيات — إضافة، تعديل، حذف، إدارة الأدوار |
| Instructor | عرض الطلاب والكورسات فقط |
| Student | عرض الكورسات وتسجيل نفسه في كورس |

---

## 🛠 التقنيات المستخدمة

| التقنية | الاستخدام |
|--------|-----------|
| ASP.NET Core 8.0 MVC | Framework الأساسي |
| Entity Framework Core | ORM & Database Management |
| ASP.NET Core Identity | Authentication & Authorization |
| SQL Server | قاعدة البيانات |
| Razor Views | Frontend Templating |
| Bootstrap 5 RTL | UI & Styling |
| Repository Pattern | Data Access Layer |
| Service Layer | Business Logic Layer |

---

## 📊 هيكل قاعدة البيانات
Students ──────────────────── Enrollments ──────────────────── Courses
(PK: StudentId)        (FK: StudentId, CourseId)         (PK: CourseId)
(FK: IdentityUserId)                                      (FK: InstructorId)
│
Instructors
(FK: IdentityUserId)

- **Students ↔ Courses**: Many-to-Many عن طريق جدول Enrollment
- **Instructors ↔ Courses**: One-to-Many
- **ApplicationUser ↔ Student/Instructor**: One-to-One عن طريق IdentityUserId

---

## ⚙️ كيفية التشغيل

1. Clone المشروع
```bash
git clone https://github.com/MostafaAhmed-100/Learning-Management-System-LMS-MVC.git
```

2. عدّل الـ Connection String في `appsettings.json`
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER;Database=LMS_Project;Trusted_Connection=True;"
}
```

3. طبّق الـ Migrations
```bash
Update-Database
```

4. شغّل المشروع بـ `F5`

5. أنشئ الأدوار الأولى عن طريق `/Role/Index` ثم سجّل أول Admin عن طريق `/Account/RegisterAdmin`

---

## 📁 هيكل المشروع
LMS-MVC/
├── Controllers/
│   ├── AccountController.cs
│   ├── StudentController.cs
│   ├── CourseController.cs
│   ├── InstructorController.cs
│   ├── EnrollmentController.cs
│   └── RoleController.cs
├── Entities/
├── DTOs/
│   ├── Request DTOs/
│   └── Response DTOs/
├── Repositories/
│   ├── Interface/
│   └── Implementation/
├── Service/
│   ├── Interfaces/
│   └── Implementation/
├── Models/
│   └── ApplicationUser.cs
├── Filter/
│   └── MyCustomExceptionFilterAttribute.cs
└── Views/

---

<div align="center">
Developed by Mostafa Ahmed Soudi © 2026
</div>
