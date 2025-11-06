# 🧠 Yazlab-TPS — Yapay Zekâ Destekli TPS (Zombi)

### 🎯 Amaç  
Bu proje, **üçüncü şahıs nişancı (TPS)** türünde, zombilere karşı hayatta kalmaya dayalı bir oyun prototipidir.  
Oyuncu, haritada devriye gezen zombiler tarafından fark edildiğinde kovalanır ve hayatta kalmak için nişan alıp ateş eder.  
Proje, YAZILIM GELİŞTİRME LABORATUVARI I dersi kapsamında Unity 6 (6000.2.8f1) kullanılarak geliştirilmiştir.

---

## 👥 Üyeler:
• Eren Dağlı | 231307033 – Player Mekanikleri: Hareket, kamera, nişan, ateş etme, animasyonlar
• Yusuf Can Müştekin | 231307082 – Yapay Zekâ & Level: Zombi FSM, NavMesh, çevre tasarımı
• Beyza Yıldırım | 241307135 – Sistem & UI: Health sistemi, GameManager, UI/menüler, sahne yönetimi

---

## ⚙️ Oyun Mekanikleri

- **Hareket:** Oyuncu klavye yön tuşları (WASD) ile hareket eder.  
- **Kamera ve Nişan:** Cinemachine v3 kullanılarak TPS kamera yapısı kurulmuştur. Sağ tık (RMB) ile nişan (ADS) modu devreye girer.  
- **Ateş Etme:** Sol tık (LMB) ile raycast tabanlı ateş edilir. Mermiler, hedefteki objenin `TakeDamage()` fonksiyonunu çağırır.  
- **Sağlık Sistemi:** Hem oyuncu hem zombiler aynı `Health` script’ini kullanır. Hasar aldıklarında `OnHealthChanged` ve `OnDied` event’leri tetiklenir.  
- **Yapay Zekâ (Zombi FSM):**  
  Zombiler, `Idle → Patrol → Chase → Attack` şeklinde durum geçişlerine sahip FSM yapısıyla hareket eder.  
  Oyuncu menziline girdiğinde `NavMeshAgent` ile kovalamaya başlar, yeterince yaklaşınca animasyon event’iyle hasar verir.  
- **UI ve Oyun Durumu:**  
  Can barı, mermi sayacı, pause menüsü ve kazanç/kayıp ekranları GameManager tarafından yönetilir.  
  Oyun durumu `Playing`, `Paused`, `Win` ve `Lose` olmak üzere dört durumla kontrol edilir.  
- **Debug:** Geliştirme sürecinde test amaçlı olarak `M` tuşu 10 hasar verir, `N` tuşu 10 iyileştirir.

---

## 🛠️ Kurulum

### 🔹 Gereksinimler
- Unity **6.0.0 (6000.2.8f1)** veya üzeri  
- Gerekli paketler:
  - `com.unity.cinemachine`
  - `com.unity.inputsystem`
  - `com.unity.ai.navigation`

---

### 🔹 Kurulum Adımları

1. Repoyu klonlayın:
   ```bash
   git clone https://github.com/kullaniciadi/yazlab-tps.git
2. Unity Hub'ı açın.
3. Add diyerek klonladığınız klasörü seçin.
4. Unity sürümünü 6000.2.8f1 olarak ayarlayın.
5. Proje açıldığında paketleri otomatik olarak import edin (gerekirse Window → Package Manager → Resolve).


## 🧱 Klasör Yapısı

```
Assets/
  Shared/     # IDamageable, Health (ortak sistem)
  Systems/    # GameManager, Scene akışı
  Player/     # PlayerController, kamera, ateş
  AI/         # ZombieAI, FSM, NavMesh
  UI/         # Canvas, menüler, HP/Ammo barları
  Prefabs/    # Player, Zombie, UI_Canvas
  Scenes/     # MainMenu, MainScene, Win/Lose
  Art/, Materials/, Audio/
```

---

## 🧩 Sistem Akış Şeması

```
Input → PlayerController → Fire → Raycast
           │
           ▼
       IDamageable.TakeDamage()
           │
           ▼
         Health (event)
           │
           ├──► UI (Can barı)
           └──► GameManager (Win/Lose)
```

FSM Akışı:
```
Idle → Patrol → Chase → Attack
↑                     │
└──────Lost Sight─────┘
```

---

## 🧰 Kullanılan Araçlar ve Teknikler

- Unity **6.0.0 (6000.2.8f1)**
- **Cinemachine v3**
- **Input System (Both Mode)**
- **AI Navigation (NavMesh)**
- **Event-driven mimari** (Health → UI/GameManager)
- **OOP ve Interface kullanımı** (`IDamageable`)
- **Prefab tabanlı modüler yapı**
- **Scene yönetimi** (`DontDestroyOnLoad`)

---

## ⚠️ Karşılaşılan Zorluklar ve Çözümler

| Zorluk | Çözüm |
|--------|-------|
| Farklı Unity sürümleri (2022 ↔ 6000) | Proje Unity 6’ya yükseltildi, paket uyumsuzlukları giderildi |
| Eksik .meta dosyaları (web upload) | `.unitypackage (Include Dependencies)` ile yeniden import |
| Çakışan `Health` script’leri | Tek `Shared/Health.cs` altında birleştirildi |
| Boş görünen sahne | Prefab referansları yeniden bağlandı |
| NavMesh sorunları | `AI Navigation` paketiyle yeniden bake edildi |

---

## 📚 Literatür Taraması (Kısa Karşılaştırma)

| Örnek Çalışma | İçerik | Bizim Projemiz |
|----------------|--------|----------------|
| Unity Learn TPS (2022) | Temel TPS kontrolü, Cinemachine v2 | Cinemachine v3 ile ADS geçişi eklendi |
| Brackeys Zombie AI (2021) | FSM + NavMesh | Aynı FSM, ancak animasyon event’leriyle hasar sistemi |
| Mixamo Low-Poly Template (2020) | Hazır modeller/sahneler | Mekanikler sıfırdan kodlandı |

---

## 🧠 Öğrenilenler ve Katkılar

- Git versiyon kontrolü  
- OOP ve event-driven yapının uygulanması  
- Cinemachine, Input System ve AI Navigation kullanım deneyimi  
- Prefab, sahne ve UI entegrasyonu  
- Hata ayıklama, merge çatışmaları, optimizasyon deneyimi  

---

## 🏁 Sonuç

Bu proje, Unity 6 üzerinde **Player**, **AI** ve **UI** sistemlerinin modüler olarak tasarlandığı,  
yapay zekâ destekli bir TPS prototipidir.  
Oyun, istenen tüm temel mekanikleri (hareket, nişan, ateş, FSM tabanlı düşman davranışı ve oyun durumu yönetimi)  
başarıyla içermektedir.
