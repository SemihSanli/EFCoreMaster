# 🚀 EF Core Metotları - Tek Katmanlı Örnek Proje

Bu proje, Entity Framework Core metotlarını tek katmanlı mimari üzerinde **anlamak ve uygulamak** amacıyla hazırlanmıştır.  
Yaklaşık **70 farklı EF Core ve LINQ metodu** kullanılarak gerçek dünya senaryolarına uygun örnekler gösterilmiştir. 🎯

---

## 📦 Kullanılan Entity Sınıfları

Projede aşağıdaki varlıklar (Entity) yer almaktadır:

- 🏃‍♂️ **Activity**  
- 📂 **Category**  
- 🙋‍♂️ **Customer**  
- 💬 **Message**  
- 🛒 **Order**  
- 📦 **Product**  
- 📝 **ToDo**

Her entity, veritabanı tablolarını temsil etmekte ve EF Core aracılığıyla CRUD işlemleri ve sorgular için kullanılmaktadır.

---

## ⚙️ Projede Kullanılan LINQ & EF Core Metotları – Açıklama ve Gerçek Proje Kullanım Senaryoları

### 🔹 Temel CRUD İşlemleri

| Metot Adı            | Açıklama                                           | Gerçek Projede Kullanım Senaryosu                                             |
| -------------------- | -------------------------------------------------- | ----------------------------------------------------------------------------- |
| `Add()`              | DbContext’e yeni bir nesne ekler.                  | Kayıt formundan gelen veriyi veritabanına eklemek: `context.Users.Add(user);` |
| `AddAsync()`         | Yeni bir veriyi async olarak ekler.                | Async form gönderimi: `await context.Users.AddAsync(user);`                   |
| `AddRange()`         | Birden fazla veriyi aynı anda ekler.               | Toplu kullanıcı ekleme: `context.Users.AddRange(userList);`                   |
| `AddRangeAsync()`    | Birden fazla veriyi async olarak ekler.            | Async toplu veri ekleme: `await context.Users.AddRangeAsync(userList);`       |
| `Update()`           | Var olan nesneyi günceller.                        | Profil düzenleme sonrası kullanıcı güncelleme: `context.Users.Update(user);`  |
| `Remove()`           | Mevcut bir veriyi silmek için kullanılır.          | Admin panelinde veri silmek: `context.Users.Remove(user);`                    |
| `SaveChanges()`      | Yapılan tüm değişiklikleri veritabanına kaydeder.  | Ekleme, güncelleme, silme işlemleri sonrası: `context.SaveChanges();`         |
| `SaveChangesAsync()` | Değişiklikleri async olarak veritabanına kaydeder. | `await context.SaveChangesAsync();`                                           |
| `Find()`             | Primary Key’e göre hızlı arama yapar.              | Detay sayfasında veri getirme: `var product = context.Products.Find(id);`     |
| `FindAsync()`        | Async olarak primary key’e göre arama yapar.       | `var user = await context.Users.FindAsync(id);`                               |

### 🔹 Veri Listeleme & Dönüştürme

| Metot Adı        | Açıklama                                                        | Gerçek Projede Kullanım Senaryosu                                                              |
| ---------------- | --------------------------------------------------------------- | ---------------------------------------------------------------------------------------------- |
| `ToList()`       | Sorguyu çalıştırıp sonucu listeye dönüştürür.                   | Tüm kullanıcıları çekmek için: `var users = context.Users.ToList();`                           |
| `ToListAsync()`  | Veriyi listeye async olarak dönüştürür.                         | API'de veriyi async almak: `await context.Users.ToListAsync();`                                |
| `Select()`       | Veri dönüşümü veya seçimi yapmak için kullanılır.               | Sadece kullanıcı adlarını almak: `users.Select(u => u.UserName);`                              |
| `Include()`      | İlişkili verileri dahil etmek için kullanılır.                  | Siparişlerle birlikte müşteri bilgilerini getirmek: `context.Orders.Include(o => o.Customer);` |
| `AsNoTracking()` | Veri değişikliği izleme olmadan performanslı veri okuma sağlar. | Salt-okunur listeler: `context.Users.AsNoTracking().ToList();`                                 |

### 🔹 Filtreleme & Sorgulama

| Metot Adı          | Açıklama                                                             | Gerçek Projede Kullanım Senaryosu                                                      |
| ------------------ | -------------------------------------------------------------------- | -------------------------------------------------------------------------------------- |
| `Where()`          | Şartlara göre filtreleme yapar.                                      | Belirli şehirdeki kullanıcıları listelemek: `users.Where(u => u.City == "İzmir");`     |
| `FirstOrDefault()` | Şarta uyan ilk değeri döner, yoksa varsayılan değeri verir.          | Giriş yapan kullanıcıyı bulmak: `context.Users.FirstOrDefault(u => u.Email == email);` |
| `All()`            | Tüm elemanların şartı sağlayıp sağlamadığını kontrol eder.           | Tüm kullanıcılar aktif mi: `users.All(u => u.IsActive);`                               |
| `Any()`            | Koleksiyonda şartı sağlayan bir eleman olup olmadığını kontrol eder. | Belirli e-posta var mı kontrolü: `users.Any(u => u.Email == email);`                   |
| `AnyAsync()`       | Async versiyonu, aynı işlevi arka planda yapar.                      | `await context.Users.AnyAsync(u => u.IsActive);`                                       |
| `AllAsync()`       | Async olarak tüm elemanları kontrol eder.                            | `await context.Users.AllAsync(u => u.IsVerified);`                                     |
| `Contains()`       | Koleksiyon ya da string içinde bir değerin varlığını kontrol eder.   | İsim "Ali" içeriyor mu: `name.Contains("Ali");`                                        |
| `StartsWith()`     | Belirli bir ifadeyle başlayıp başlamadığını kontrol eder.            | Telefon numarası "+90" ile mi başlıyor: `phone.StartsWith("+90");`                     |
| `EndsWith()`       | Belirli bir ifadeyle bitip bitmediğini kontrol eder.                 | Dosya uzantısı ".jpg" ile mi bitiyor: `file.EndsWith(".jpg");`                         |
| `DefaultIfEmpty()` | Boş koleksiyonlarda varsayılan değer döner.                          | Boş koleksiyona karşı fallback sağlamak: `query.DefaultIfEmpty(new Product());`        |

### 🔹 Sıralama & Sayfalama

| Metot Adı             | Açıklama                              | Gerçek Projede Kullanım Senaryosu                                        |
| --------------------- | ------------------------------------- | ------------------------------------------------------------------------ |
| `Take()`              | İlk n elemanı alır.                   | Sayfalama için ilk 10 kaydı almak: `products.Take(10);`                  |
| `Skip()`              | İlk n elemanı atlayıp kalanları alır. | Sayfalamada 2. sayfaya geçmek: `products.Skip(10).Take(10);`             |
| `OrderBy()`           | Artan şekilde sıralar.                | Kullanıcıları ada göre sırala: `users.OrderBy(u => u.Name);`             |
| `OrderByDescending()` | Azalan şekilde sıralar.               | Siparişleri tarihe göre sırala: `orders.OrderByDescending(o => o.Date);` |
| `SkipLast()`          | Son n elemanı çıkarır.                | Son 3 kaydı hariç tut: `list.SkipLast(3);`                               |
| `TakeLast()`          | Sondan n elemanı alır.                | Son 5 işlemi getir: `logs.TakeLast(5);`                                  |
| `Reverse()`           | Koleksiyonu tersine çevirir.          | Sıralı bir listeyi ters göstermek: `items.Reverse();`                    |

### 🔹 Set & Koleksiyon Operasyonları

| Metot Adı     | Açıklama                                           | Gerçek Projede Kullanım Senaryosu                                              |
| ------------- | -------------------------------------------------- | ------------------------------------------------------------------------------ |
| `Concat()`    | İki koleksiyonu ardışık birleştirir.               | Kullanıcı ve admin listelerini birleştirmek: `users.Concat(admins);`           |
| `Union()`     | Tekrarsız birleşim yapar.                          | İki şehirden gelen kullanıcıları birleştirme: `users1.Union(users2);`          |
| `UnionBy()`   | Belirli bir özelliğe göre birleşim yapar.          | E-postaya göre benzersiz kullanıcılar: `users1.UnionBy(users2, u => u.Email);` |
| `Except()`    | Bir koleksiyonda olup diğerinde olmayanları döner. | Yasaklı kullanıcıları çıkarma: `allUsers.Except(bannedUsers);`                 |
| `ExceptBy()`  | Belirli özelliğe göre hariç tutar.                 | `ExceptBy(admins, u => u.Email);`                                              |
| `Intersect()` | Ortak olan öğeleri döner.                          | Ortak müşterileri bulma: `list1.Intersect(list2);`                             |
| `Distinct()`  | Tekrarlayanları kaldırır.                          | Aynı isimleri filtrelemek: `names.Distinct();`                                 |
| `Append()`    | Koleksiyonun sonuna öğe ekler.                     | Listeye ekleme: `list.Append(newItem);`                                        |
| `Prepend()`   | Koleksiyonun başına öğe ekler.                     | Menüye ana başlık eklemek: `menu.Prepend("Ana Sayfa");`                        |
| `Chunk()`     | Koleksiyonu parçalara böler.                       | Sayfalamada kolaylık: `list.Chunk(10);`                                        |

### 🔹 İleri Seviye & Diğer

| Metot Adı           | Açıklama                                                                 | Gerçek Projede Kullanım Senaryosu                                               |
| ------------------- | ------------------------------------------------------------------------ | ------------------------------------------------------------------------------- |
| `Aggregate()`       | Koleksiyon üzerinde birleştirici işlemler yapar.                         | Toplam uzunluk hesaplama: `list.Aggregate((a, b) => a + b);`                    |
| `GroupBy()`         | Elemanları belirli bir kritere göre gruplayarak döner.                   | Şehre göre kullanıcıları gruplama: `users.GroupBy(u => u.City);`                |
| `Join()`            | İki koleksiyonu ortak alan üzerinden birleştirir.                        | Kullanıcı ve adres bilgilerini birleştirme.                                     |
| `GroupJoin()`       | Gruplu birleştirme işlemi yapar.                                         | Kategoriler ve alt ürünlerini getirme.                                          |
| `AsQueryable()`     | IEnumerable'ı IQueryable'a dönüştürür.                                   | Dinamik sorgu oluşturmak için: `list.AsQueryable();`                            |
| `Entry()`           | Entity'nin EF üzerindeki izlenme durumuna erişim sağlar.                 | Nesne durumu kontrolü: `context.Entry(user).State;`                             |
| `Cast<T>()`         | Koleksiyonu belirli bir tipe dönüştürür.                                 | Object listesini int listesine çevirme: `list.Cast<int>();`                     |
| `OfType<T>()`       | Belirli tipteki elemanları filtreler.                                    | Sadece string değerleri almak: `list.OfType<string>();`                         |
| `Attach()`          | Veritabanında var olan ama takip edilmeyen nesneyi izlemeye alır.        | Dışarıdan gelen nesneyi güncellemek için bağlama: `context.Attach(entity);`     |
| `AttachRange()`     | Birden fazla nesneyi izlemeye alır.                                      | Toplu güncelleme senaryosu.                                                     |
| `Index()`           | Belirli pozisyondaki öğeyi elde eder.                                    | `list.ElementAt(index);` yerine özel senaryolarda kullanılır.                   |
| `CountBy()`         | Elemanları gruplayıp, her grubun sayısını verir (LinqKit veya MoreLinq). | Şehre göre kullanıcı sayısı: `users.CountBy(u => u.City);`                      |
| `LongCount()`       | Çok büyük koleksiyonlarda eleman sayısını döner.                         | Büyük datasetlerde veri sayımı: `users.LongCount();`                            |
| `SingleOrDefault()` | Koleksiyonda tek bir eleman varsa döner, yoksa varsayılan döner.         | Benzersiz kullanıcı kontrolü: `context.Users.SingleOrDefault(u => u.Id == id);` |
| `First()`           | Koleksiyondaki ilk elemanı döner, yoksa hata verir.                      | Son eklenen kullanıcıyı alma: `users.OrderByDescending(u => u.Id).First();`     |
| `Last()`            | Koleksiyondaki son elemanı döner, yoksa hata verir.                      | Liste sonunda işlem yapmak: `list.Last();`                                      |


## 📸 Ekran Görüntüleri 

![ImageAlt](https://github.com/SemihSanli/EFCoreMaster/blob/00600379569529b5c6f6d9736aebaf04f251b6bd/Images/Ekran%20g%C3%B6r%C3%BCnt%C3%BCs%C3%BC%202025-06-18%20150257.png)
![ImageAlt](https://github.com/SemihSanli/EFCoreMaster/blob/00600379569529b5c6f6d9736aebaf04f251b6bd/Images/Ekran%20g%C3%B6r%C3%BCnt%C3%BCs%C3%BC%202025-06-18%20150308.png)
![ImageAlt](https://github.com/SemihSanli/EFCoreMaster/blob/00600379569529b5c6f6d9736aebaf04f251b6bd/Images/Ekran%20g%C3%B6r%C3%BCnt%C3%BCs%C3%BC%202025-06-18%20150318.png)
![ImageAlt](https://github.com/SemihSanli/EFCoreMaster/blob/00600379569529b5c6f6d9736aebaf04f251b6bd/Images/Ekran%20g%C3%B6r%C3%BCnt%C3%BCs%C3%BC%202025-06-18%20150533.png)
![ImageAlt](https://github.com/SemihSanli/EFCoreMaster/blob/00600379569529b5c6f6d9736aebaf04f251b6bd/Images/Ekran%20g%C3%B6r%C3%BCnt%C3%BCs%C3%BC%202025-06-18%20150552.png)
![ImageAlt](https://github.com/SemihSanli/EFCoreMaster/blob/00600379569529b5c6f6d9736aebaf04f251b6bd/Images/Ekran%20g%C3%B6r%C3%BCnt%C3%BCs%C3%BC%202025-06-18%20150601.png)
![ImageAlt](https://github.com/SemihSanli/EFCoreMaster/blob/00600379569529b5c6f6d9736aebaf04f251b6bd/Images/Ekran%20g%C3%B6r%C3%BCnt%C3%BCs%C3%BC%202025-06-18%20150613.png)
![ImageAlt](https://github.com/SemihSanli/EFCoreMaster/blob/00600379569529b5c6f6d9736aebaf04f251b6bd/Images/Ekran%20g%C3%B6r%C3%BCnt%C3%BCs%C3%BC%202025-06-18%20150626.png)
![ImageAlt](https://github.com/SemihSanli/EFCoreMaster/blob/00600379569529b5c6f6d9736aebaf04f251b6bd/Images/Ekran%20g%C3%B6r%C3%BCnt%C3%BCs%C3%BC%202025-06-18%20150635.png)
![ImageAlt](https://github.com/SemihSanli/EFCoreMaster/blob/00600379569529b5c6f6d9736aebaf04f251b6bd/Images/Ekran%20g%C3%B6r%C3%BCnt%C3%BCs%C3%BC%202025-06-18%20150647.png)
![ImageAlt](https://github.com/SemihSanli/EFCoreMaster/blob/00600379569529b5c6f6d9736aebaf04f251b6bd/Images/Ekran%20g%C3%B6r%C3%BCnt%C3%BCs%C3%BC%202025-06-18%20150647.png)
![ImageAlt](https://github.com/SemihSanli/EFCoreMaster/blob/00600379569529b5c6f6d9736aebaf04f251b6bd/Images/Ekran%20g%C3%B6r%C3%BCnt%C3%BCs%C3%BC%202025-06-18%20150711.png)
![ImageAlt](https://github.com/SemihSanli/EFCoreMaster/blob/00600379569529b5c6f6d9736aebaf04f251b6bd/Images/Ekran%20g%C3%B6r%C3%BCnt%C3%BCs%C3%BC%202025-06-18%20150729.png)
![ImageAlt](https://github.com/SemihSanli/EFCoreMaster/blob/00600379569529b5c6f6d9736aebaf04f251b6bd/Images/Ekran%20g%C3%B6r%C3%BCnt%C3%BCs%C3%BC%202025-06-18%20150754.png)
![ImageAlt](https://github.com/SemihSanli/EFCoreMaster/blob/00600379569529b5c6f6d9736aebaf04f251b6bd/Images/Ekran%20g%C3%B6r%C3%BCnt%C3%BCs%C3%BC%202025-06-18%20150812.png)
![ImageAlt](https://github.com/SemihSanli/EFCoreMaster/blob/00600379569529b5c6f6d9736aebaf04f251b6bd/Images/Ekran%20g%C3%B6r%C3%BCnt%C3%BCs%C3%BC%202025-06-18%20150819.png)
![ImageAlt](https://github.com/SemihSanli/EFCoreMaster/blob/00600379569529b5c6f6d9736aebaf04f251b6bd/Images/Ekran%20g%C3%B6r%C3%BCnt%C3%BCs%C3%BC%202025-06-18%20151052.png)

![ImageAlt](https://github.com/SemihSanli/EFCoreMaster/blob/00600379569529b5c6f6d9736aebaf04f251b6bd/Images/Ekran%20g%C3%B6r%C3%BCnt%C3%BCs%C3%BC%202025-06-18%20151156.png)
![ImageAlt](https://github.com/SemihSanli/EFCoreMaster/blob/00600379569529b5c6f6d9736aebaf04f251b6bd/Images/Ekran%20g%C3%B6r%C3%BCnt%C3%BCs%C3%BC%202025-06-18%20151231.png)
![ImageAlt](https://github.com/SemihSanli/EFCoreMaster/blob/00600379569529b5c6f6d9736aebaf04f251b6bd/Images/Ekran%20g%C3%B6r%C3%BCnt%C3%BCs%C3%BC%202025-06-18%20151523.png)
![ImageAlt](https://github.com/SemihSanli/EFCoreMaster/blob/00600379569529b5c6f6d9736aebaf04f251b6bd/Images/Ekran%20g%C3%B6r%C3%BCnt%C3%BCs%C3%BC%202025-06-18%20151551.png)
![ImageAlt](https://github.com/SemihSanli/EFCoreMaster/blob/00600379569529b5c6f6d9736aebaf04f251b6bd/Images/Ekran%20g%C3%B6r%C3%BCnt%C3%BCs%C3%BC%202025-06-18%20151606.png)


![ImageAlt](https://github.com/SemihSanli/EFCoreMaster/blob/00600379569529b5c6f6d9736aebaf04f251b6bd/Images/Ekran%20g%C3%B6r%C3%BCnt%C3%BCs%C3%BC%202025-06-18%20151231.png)
![ImageAlt](https://github.com/SemihSanli/EFCoreMaster/blob/00600379569529b5c6f6d9736aebaf04f251b6bd/Images/Ekran%20g%C3%B6r%C3%BCnt%C3%BCs%C3%BC%202025-06-18%20151523.png)
![ImageAlt](https://github.com/SemihSanli/EFCoreMaster/blob/00600379569529b5c6f6d9736aebaf04f251b6bd/Images/Ekran%20g%C3%B6r%C3%BCnt%C3%BCs%C3%BC%202025-06-18%20151551.png)
![ImageAlt](https://github.com/SemihSanli/EFCoreMaster/blob/00600379569529b5c6f6d9736aebaf04f251b6bd/Images/Ekran%20g%C3%B6r%C3%BCnt%C3%BCs%C3%BC%202025-06-18%20151606.png)

