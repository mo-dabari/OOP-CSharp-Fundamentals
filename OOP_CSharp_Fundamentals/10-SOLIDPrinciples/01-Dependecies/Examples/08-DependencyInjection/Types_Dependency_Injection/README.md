# Dependency Injection – مقارنة مختصرة وواضحة

في Dependency Injection في أكتر من طريقة لتمرير الـ Dependencies داخل الكلاس،
لكن مش كل الطرق متساوية، وكل واحدة ليها استخدامات محددة.

---

## 🏆 Constructor Injection (الطريقة المفضلة)

### المميزات

- Dependencies واضحة وصريحة
- الكائن دائمًا في حالة صحيحة (Valid State)
- يمنع إنشاء Object ناقص
- Dependencies ثابتة (Immutable)
- الأفضل للاختبار (Testing)
- مدعوم افتراضيًا في أغلب DI Containers

### الاستخدام

- Dependencies الأساسية والمطلوبة دائمًا

---

## ⚠️ Property Injection

### العيوب

- الكائن ممكن يتستخدم قبل ما الـ Dependency تتحدد
- يحتاج Null Checks كتير
- Dependencies قابلة للتغيير بعد الإنشاء
- يقلل من وضوح الـ Contract

### الاستخدام

- Dependencies اختيارية
- أو في حالات Frameworks تفرضه

---

## ⚠️ Method Injection

### العيوب

- يزيد تعقيد الكود على الـ Client
- توقيع الميثود (Method Signature) يكبر
- يصعب القراءة والصيانة

### الاستخدام

- Dependency بتتغير مع كل Call
- أو مرتبطة بسيناريو تنفيذ محدد

---

## 🧠 مقارنة سريعة

| النوع                 | الوضوح    | الأمان | سهولة الاختبار | متى يُستخدم    |
| --------------------- | --------- | ------ | -------------- | -------------- |
| Constructor Injection | عالي جدًا | عالي   | ممتاز          | القاعدة العامة |
| Property Injection    | متوسط     | ضعيف   | متوسط          | حالات خاصة     |
| Method Injection      | ضعيف      | متوسط  | ضعيف           | نادرًا         |

---

## ✅ القاعدة الذهبية

- 🏆 استخدم **Constructor Injection** كخيار افتراضي
- ⚠️ استخدم **Property** أو **Method Injection** فقط عند وجود سبب معماري واضح
- أي اختيار غير Constructor Injection يحتاج مبرر حقيقي
