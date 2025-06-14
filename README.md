# 📚 Library Management Application (WPF + DI)

<div align="center">
  <img src="https://img.shields.io/badge/.NET-8.0-blue?logo=dotnet" />
  <img src="https://img.shields.io/badge/WPF-UI-green?logo=windows" />
  <img src="https://img.shields.io/badge/EF%20Core-ORM-blueviolet?logo=entity-framework" />
  <img src="https://img.shields.io/badge/DI-Enabled-yellow" />
</div>

<div>
  <img src="https://github.com/StarredNaga/Architecture-training-Library-Application-/blob/master/AppImage.bmp" />
</div>
---

## 🧾 Описание (Русский)

**Library Management Application** — это настольное WPF-приложение для управления коллекцией книг, реализованное с применением архитектурных принципов чистого кода и Dependency Injection.

### 💡 Возможности:
- 📗 Добавление, редактирование и удаление книг
- 🔍 Поиск по автору или названию
- 💾 Поддержка двух видов хранилищ:
  - **Файловая система** (сериализация в JSON)
  - **База данных** (через Entity Framework Core)
- 🧱 Чёткое разделение на слои:
  - `Domain`, `DataAccess`, `UI (WPF)`
- 🧪 Расширяемость через интерфейсы (`IBookService`, `IFileReader`, `IBookFormatter`, ...)

### 🚀 Как запустить:
```bash
git clone https://github.com/StarredNaga/Architecture-training-Library-Application-.git
```

## 🧾 Description (English)

**Library Management Application** is a desktop WPF application for managing book collections, implemented using clean code architectural principles and Dependency Injection.

### 💡 Features:
- 📗 Add, edit, and delete books
- 🔍 Search by author or title
- 💾 Support for two storage types:
  - **File system** (JSON serialization)
  - **Database** (via Entity Framework Core)
- 🧱 Clear layer separation:
  - `Domain`, `DataAccess`, `UI (WPF)`
- 🧪 Extensibility through interfaces (`IBookService`, `IFileReader`, `IBookFormatter`, ...)
- 🔄 Dynamic storage switching via Dependency Injection

### 🏗️ Architecture Highlights:
- **MVVM pattern** with ViewModel layer
- **Dependency Injection** (Microsoft.Extensions.DependencyInjection)
- **Repository pattern** for data access
- **Configuration-free EF Core** setup (code-first)
- **Validation** for book entities

### 🚀 How to Run:
```bash
git clone https://github.com/StarredNaga/Architecture-training-Library-Application-.git
```
