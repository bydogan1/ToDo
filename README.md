# ToDo
ToDo List
# 📝 Todo Yönetim API

Modern bir Todo yönetim uygulaması. .NET 10, Entity Framework Core ve SQLite kullanılarak geliştirilmiştir.

## ✨ Özellikler

- ✅ **CRUD İşlemleri**: Todo ekleme, görüntüleme, düzenleme ve silme
- 📅 **Tarih/Saat Yönetimi**: Bitiş tarihi ve saati belirleme
- 🏷️ **Kategori Sistemi**: 8 farklı kategori ile görevleri organize etme
  - 📋 Görevler
  - 📝 Notlar
  - 🚀 Projeler
  - 👤 Kişisel
  - 💼 İş
  - 🛒 Alışveriş
  - 🏥 Sağlık
  - 📚 Eğitim
- 🔍 **Arama ve Filtreleme**: 
  - Başlık ve açıklamada arama
  - Duruma göre filtreleme (Tümü, Bekleyen, Tamamlanan)
  - Kategoriye göre filtreleme
- 🎨 **Modern Karanlık Tema**: Göz yormayan karanlık mod arayüz
- 📊 **İstatistikler**: Toplam, tamamlanan ve bekleyen todo sayıları KPI
- ⚠️ **Gecikmiş Görev Uyarısı**: Geçmiş tarihli görevler için görsel uyarı

## 🛠️ Teknolojiler

- **.NET 10.0**: Minimal API yaklaşımı
- **Entity Framework Core**: Veritabanı ORM
- **SQLite**: Hafif ve taşınabilir veritabanı
- **Repository Pattern**: Veri erişim katmanı
- **DTOs**: Veri transfer nesneleri
- **Validation**: Veri doğrulama
- **CORS**: Cross-Origin Resource Sharing desteği
- **OpenAPI**: API dokümantasyonu

## 📋 Gereksinimler

- .NET 10.0 SDK veya üzeri
- Modern bir web tarayıcısı

## 🚀 Kurulum

1. Projeyi klonlayın veya indirin:
```bash
cd src/MyApi
```

2. Bağımlılıkları yükleyin:
```bash
dotnet restore
```

3. Projeyi çalıştırın:
```bash
dotnet run
```

4. Tarayıcınızda açın:
```
http://localhost:5117
```

## 📁 Proje Yapısı

```
MyApi/
├── Data/
│   └── ApplicationDbContext.cs      # Entity Framework DbContext
├── DTOs/
│   └── TodoDto.cs                   # Veri transfer nesneleri
├── Models/
│   └── Todo.cs                      # Todo entity modeli
├── Repositories/
│   ├── ITodoRepository.cs           # Repository interface
│   └── TodoRepository.cs            # Repository implementasyonu
├── Validators/
│   └── CreateTodoDtoValidator.cs    # Validasyon kuralları
├── wwwroot/
│   └── index.html                   # Web arayüzü
├── Program.cs                        # Ana uygulama dosyası
└── MyApi.csproj                      # Proje dosyası
```

## 🔌 API Endpoints

### GET `/api/todos`
Tüm todoları listeler.

**Query Parametreleri:**
- `completed` (bool, opsiyonel): Tamamlanma durumuna göre filtreleme
- `search` (string, opsiyonel): Başlık veya açıklamada arama
- `category` (string, opsiyonel): Kategoriye göre filtreleme

**Örnek:**
```
GET /api/todos?completed=false&category=Görevler&search=test
```

### GET `/api/todos/{id}`
Belirli bir todo'yu getirir.

### POST `/api/todos`
Yeni todo oluşturur.

**Request Body:**
```json
{
  "title": "Todo başlığı",
  "description": "Todo açıklaması",
  "category": "Görevler",
  "dueDate": "2024-01-15T14:30:00Z"
}
```

### PUT `/api/todos/{id}`
Todo'yu günceller.

**Request Body:**
```json
{
  "title": "Güncellenmiş başlık",
  "description": "Güncellenmiş açıklama",
  "category": "Notlar",
  "isCompleted": true,
  "dueDate": "2024-01-20T10:00:00Z"
}
```

### DELETE `/api/todos/{id}`
Todo'yu siler.

## 📖 Kullanım

### Todo Ekleme
1. Ana sayfada "Yeni Todo Ekle" formunu doldurun
2. Başlık (zorunlu), açıklama, kategori ve bitiş tarihi girin
3. "Todo Ekle" butonuna tıklayın

### Todo Düzenleme
1. Todo listesinde "✏️ Düzenle" butonuna tıklayın
2. Modal pencerede değişiklikleri yapın
3. "Güncelle" butonuna tıklayın

### Todo Tamamlama
1. Todo listesinde "✓ Tamamla" butonuna tıklayın
2. Veya düzenleme modal'ında "Tamamlandı" checkbox'ını işaretleyin

### Filtreleme
- **Durum Filtreleri**: Tümü, Bekleyen, Tamamlanan
- **Kategori Filtreleri**: Her kategori için ayrı buton
- **Arama**: Başlık ve açıklamada arama yapabilirsiniz

## 🗄️ Veritabanı

Uygulama SQLite veritabanı kullanır. Veritabanı dosyası (`todos.db`) proje kök dizininde otomatik olarak oluşturulur.

**Veritabanı Şeması:**
- `Id` (int, Primary Key)
- `Title` (string, max 200 karakter, zorunlu)
- `Description` (string, max 1000 karakter)
- `Category` (string, max 50 karakter, varsayılan: "Görevler")
- `IsCompleted` (bool, varsayılan: false)
- `DueDate` (DateTime?, opsiyonel)
- `CreatedAt` (DateTime, otomatik)
- `UpdatedAt` (DateTime?, opsiyonel)

## 🔧 Geliştirme

### Projeyi Geliştirme Modunda Çalıştırma
```bash
dotnet run --environment Development
```

### Veritabanını Sıfırlama
Veritabanını sıfırlamak için `todos.db` dosyasını silin ve projeyi yeniden çalıştırın:
```bash
rm todos.db
dotnet run
```

### OpenAPI Dokümantasyonu
API dokümantasyonu için:
```
http://localhost:5117/openapi/v1.json
```

## 📝 Lisans

Bu proje eğitim amaçlı geliştirilmiştir.

## 👨‍💻 Geliştirici

.NET 10 Minimal API ile geliştirilmiştir.

## 🎯 Gelecek Özellikler

- [ ] Kullanıcı kimlik doğrulama
- [ ] Çoklu kullanıcı desteği
- [ ] Todo paylaşımı
- [ ] Dosya ekleme
- [ ] Bildirimler
- [ ] Export/Import özelliği
- [ ] Mobil uygulama

