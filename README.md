نظام متكامل لإدارة العمليات الأكاديمية في الجامعات، تم بناؤه باستخدام تقنية ASP.NET Core MVC. يركز المشروع على ربط الكيانات الثلاثة الأساسية: الطلاب، الكورسات، والمعيدين، مع نظام تسجيل (Enrollment) ذكي.

🚀 الميزات الجديدة (Latest Updates)
تم تحديث النظام ليدعم الربط الديناميكي بين الجداول:

Student Dashboard: إمكانية عرض تفاصيل الطالب وإضافة كورسات له مباشرة من داخل ملفه الشخصي.

Instructor Management: ربط كل كورس بالمعيد المسؤول عنه وعرض قائمة الكورسات الخاصة بكل معيد.

Advanced Enrollment: نظام تسجيل يعتمد على معرفات (IDs) الطلاب والكورسات مع تسجيل تلقائي لتوقيت التسجيل.

Enhanced Navigation: توفير أزرار تنقل سريعة (Navigation Links) بين قوائم الطلاب، الكورسات، والانرولمنت لضمان تجربة مستخدم سلسة.

Course Insights: صفحة معلومات الكورس تعرض الآن (المعيد المسؤول، قائمة الطلاب المسجلين، وتواريخ الإضافة).

✨ الميزات الأساسية (Core Features)
Full CRUD Operations: إدارة شاملة (إضافة، عرض، تعديل، حذف) لجميع البيانات.

Relational Database: تنفيذ علاقات معقدة (One-to-Many & Many-to-Many) بين الطلاب والمعيدين والكورسات.

Server-Side Validation: التأكد من صحة البيانات المدخلة قبل حفظها في قاعدة البيانات.

Responsive UI: واجهة مستخدم متجاوبة تماماً باستخدام Bootstrap و Razor Tag Helpers.

🛠 التقنيات المستخدمة (Tech Stack)
Framework: ASP.NET Core 8.0 (MVC)

ORM: Entity Framework Core

Database: SQL Server

Frontend: Razor Views, HTML5, CSS3, Bootstrap 5

Architecture: Repository Pattern & Service Layer (لضمان تنظيم الكود وسهولة صيانته).

📊 هيكل البيانات (Database Schema)
يعتمد المشروع على نظام علاقات قوي:

Students ↔ Courses: علاقة (Many-to-Many) من خلال جدول الـ Enrollment.

Instructors ↔ Courses: علاقة (One-to-Many) حيث يتبع الكورس معيداً واحداً.

Departments: لتنظيم الطلاب والكورسات برمجياً.

⚙️ كيفية التشغيل (Setup)
قم بعمل Clone للمستودع.

قم بتعديل Connection String في ملف appsettings.json ليتوافق مع جهازك.

افتح الـ Package Manager Console وقم بتنفيذ الأوامر التالية:

Update-Database

قم بتشغيل المشروع (F5).
