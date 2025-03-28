# 📘 GHBT.Solution

Bu repository, .NET 9 ile geliştirilen iki ayrı projeyi içermektedir:

- **EventManagement**: Etkinlik oluşturma ve katılımcı yönetimi
- **GoRestUserAdmin**: GoRest API ile entegre AJAX tabanlı kullanıcı yönetim paneli

---

## 📂 Proje Yapısı
```
GHBT.Solution
│
├── EventManagement
│   ├── src
│   │   ├── EventManagement.API
│   │   ├── EventManagement.UI
│   │   ├── EventManagement.Application
│   │   ├── EventManagement.Domain
│   │   └── EventManagement.Infrastructure
│   └── test
│       └── EventManagement.Tests
│
├── GoRestUserAdmin
│   ├── src
│   │   └── GoRestUserAdmin.UI
│   └── test
│       └── GoRestUserAdmin.Tests
│
└── GHBT.Solution.sln
```

---

## 🧪 Gereksinimler
- .NET 9 SDK
- PostgreSQL (EventManagement için)
- Visual Studio 2022+ veya Rider / VS Code

---

## 🚀 EventManagement Projesi Nasıl Çalıştırılır?

### 1. PostgreSQL Ayarları
`appsettings.json` içerisinde bağlantı string'inizi belirtin:

```json
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Port=5432;Database=EventDb;Username=postgres;Password=yourpassword"
}
```

### 2. Migration ve Veritabanı Kurulumu
```bash
cd EventManagement/src/EventManagement.API

# Migration oluştur
dotnet ef migrations add InitialCreate -p ../EventManagement.Infrastructure -s ./

# DB güncelle
dotnet ef database update -p ../EventManagement.Infrastructure -s ./
```

### 3. Uygulamaları Başlat
EventManagement projesi **API** ve **UI** olmak üzere iki ayrı sunucu içerir. Birlikte çalışmaları gerekir:

```bash
# API başlatılır
cd EventManagement/src/EventManagement.API

dotnet run
```

```bash
# UI ayrı terminalde başlatılır
cd EventManagement/src/EventManagement.UI

dotnet run
```

> `EventManagement.UI`, `EventManagement.API` ile haberleşerek veri işlemlerini gerçekleştirir.

API: [https://localhost:5001/swagger](https://localhost:5001/swagger)  
UI: [https://localhost:5002](https://localhost:5002) (örnek)


---

## 🌐 GoRestUserAdmin Projesi Nasıl Çalıştırılır?

### 1. `appsettings.json` Yapılandırması
```json
"GoRest": {
  "BaseUrl": "https://gorest.co.in/public/v2/",
  "Token": "YOUR_ACCESS_TOKEN"
}
```

### 2. Projeyi Başlat
```bash
cd GoRestUserAdmin/src/GoRestUserAdmin.UI

dotnet run
```

> Uygulama açıldığında otomatik olarak `/Users` sayfasına yönlenir.

- Yeni kullanıcı ekleme
- AJAX ile güncelleme/silme
- Modal form ile kullanıcı yönetimi

---

## 👨‍💻 Geliştirici Notları
- Clean Architecture prensipleri EventManagement projesinde uygulanmıştır
- GoRestUserAdmin monolitik, sade, AJAX tabanlı bir CRUD yönetim panelidir


---

Her iki proje de gerçek dünya kullanımına uygun şekilde ayrıştırılmış ve profesyonel yapıya sahiptir. İyi çalışmalar! 🚀
