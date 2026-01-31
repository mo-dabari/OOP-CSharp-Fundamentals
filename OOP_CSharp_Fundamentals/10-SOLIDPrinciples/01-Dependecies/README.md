
# Dependencies في البرمجة - دليل شامل

> تلخيص شامل عن مفهوم Dependencies ومشاكلها وحلولها في البرمجة الكائنية

## 📌 جدول المحتويات

- [ما هو Dependencies؟](#ما-هو-dependencies)
- [أدوار Dependencies](#أدوار-dependencies)
- [أنواع Dependencies](#أنواع-dependencies)
- [Dependency Graph](#dependency-graph)
- [Implicit vs Explicit Dependencies](#implicit-vs-explicit-dependencies)
- [مشاكل Dependencies المباشرة](#مشاكل-dependencies-المباشرة)
- [حلول مشكلة Coupling](#حلول-مشكلة-coupling)
- [لماذا تعتبر new من Code Smells؟](#لماذا-تعتبر-new-من-code-smells)
- [المشاكل الناتجة عن سوء استخدام Dependencies](#المشاكل-الناتجة-عن-سوء-استخدام-dependencies)
- [Dependency Injection (DI)](#dependency-injection-di)
- [أنواع DI](#أنواع-di)
- [الخلاصة](#الخلاصة)

---

## ما هو Dependencies؟

ناخد مثال من الحياه الواقعيه لترسيخ المفهوم:

انت عندك **سيارة**  
منطقى السيارة محتاجة **بنزين** عشان تتحرك  
ده بنترجمه: ان **السيارة بتعتمد ع البنزين**

لحد هنا الكلام منطقي

ف البرمجه نفس الفكره:  
**Class A => Depends on => Class B**

---

## أدوار Dependencies

### #Client
- هو ال محتاج الخدمة 
- بيستخدم ال Service عشان يشتغل 
- لو ال Service مش موجوده ال Client مش هيعرف يشتغل 

**مثال:**  
`AccountController` محتاج `securityService` عشان يشتغل

### #Service
- بيقدم وظيفة معينة
- مش بيعرف مين بيستخدمه
- ممكن اكثر من Client يستخدمه

**مثال:**  
ال `securityService` ممكن يستخدمه اكثر من Controller

**المعلومة ال هنطلع بيها:**  
ان ال `securityService` بقي **Reusable**

---

## أنواع Dependencies

### 1- First-Party Dependencies
هو كود انت كاتبه داخل نفس ال Solution  
عندك ال Source Code وتقدر تعدل عليه

**مثال:**  
UI => Services => Data => Domain

**المفيد هنا:**
- تحكم كامل 
- سهوله ف ال Debug
- سرعه ال Build

**عيوبه:**
- ال solution ممكن يكبر وده هيخلى ال Build Time يزيد 
- كمان خطوره ال Circular Dependencies كارثه (هيتم شرحه قدام)

### 2- Framework Dependencies
- جزء من ال .NET Framework
- مكتوبه من Microsoft
- لا يمكن تعديلها 

**أمثله:**
- `System.dll`
- `System.Core.dll`
- `System.Data.dll`

**معلومه جانبيه:**  
ال Framework DLLs بتتحمل تلقائي داخل ال Memory مره واحده فقط وبت Share مع كل ال Projects وبعضها

### 3- Third-Party Dependencies
- هى مكتبات خارجيه 
- ليست جزء من ال .NET
- ليست جزء من اى Solution

**العائد:**
- توفير الوقت وعدم اختراع العجله من جديد مع كود ال Quality ف عاليه بال Test كمان
- بركز اكتر ع ال Business Logic

---

## Dependency Graph

### اى هو ال Dependency Graph؟
هو **تمثيل بصري** يوضح علاقات ال Dependencies داخل النظام

### مكونات ال Graph

#### #Nodes (العقد)
تمثل المكونات المختلفة في النظام:

- **Classes**: علاقة اعتماد كلاس على كلاس أو Interface آخر
- **Assemblies**: علاقة اعتماد DLL على DLL أخرى عبر ال References
- **Subsystems**: علاقة اعتماد بين أجزاء كبيرة من النظام (UI، Business، Data)
- **Methods**: علاقة Method Call

#### #Edges (الحواف)
تمثل **Dependency Relationship** (علاقة الاعتماد)

### اي هو ال Directed Graph
هو عباره عن رسم توضيحي  
وده معناه **A => B**:
- ان ال A depends on B
- ان ال A يعرف عن B وبيستخدمه
- ان ال B لا يعرف عن A

**الاتجاه هنا مهم جدا لأنه يوضح:**
- مين يعتمد ع مين 
- التغيرات مفروض هتبقي فين 

### من الاخر ال Dependency Graph بيساعدك ع:
- اكتشاف Tight Coupling
- تصميم Architecture ابن ناس و Scalability كمان 
- منع ال Cyclic Dependencies

---

## Implicit VS Explicit Dependencies

### Explicit Dependency
هو عباره عن **اعتماد مباشر** A يعتمد ع B

**مثال للتوضيح:**  
لو عندي كلاس `OrderService` بيعتمد جواه ع انترفيس `IOrderRepository`  
هيبقي ف حالتنا هنا:  
ان ال `OrderService` **Explicitly Depends on** `IOrderRepository`

### Implicit Dependency
هو عباره عن **اعتماد غير مباشر** عن طريق Dependency اخري

يعنى الشكل ده بيقول:  
**A => B => C**

- ال A بيعتمد ع ال B هنا **(Explicit)**
- ال B بيعتمد ع ال C هنا **(Explicit)**
- ولكن ال A بيعتمد ع ال C هنا **(Implicit)**

**المعلومه ال نطلع بيها هنا:**  
ان لو ال C مش موجود ال A مش هيشتغل مع انه مش عارف عنه حاجه

---

## مشاكل Dependencies المباشرة

### 1- Circular Dependency

فيما معناه ان ال Path يبدا من عند Node ويرجع الى نفس ال Node مره تاني

**مثال للتوضيح:**  
**A => B => C => A**

وبالتالى:
- A depends on B
- B depends on C
- C depends on A

ويبقي كده ال A بيعتمد ع ال A بشكل Implicit  
**وطبعا دى كارثه**

#### ليه ي عم كارثه؟
عشان المفروض ينور ف دماغك سؤال:  
**طيب ابنى مين الاول؟**

- لو بنيت ال A محتاج ال B (وهو مش موجود)
- لو بنيت ال B محتاج ال C (وهو مش موجود)
- لو بنيت ال C محتاج ال A (وهو مش موجود)

**وهنا هتدخل ف نقطه مين ال جه الاول البيضة ولا الفرخة** 🥚🐔

### 2- Coupling (الارتباط المحكم)

ان لو عندك الشكل ده:  
**A <=> B <=> C**

معناه ان:
- ماينفعش تغير ال A من غير ماتأثر ع ال B, C
- ماينفعش تغير ال B من غير ماتأثر ع ال A, C
- ماينفعش تغير ال C من غير ماتأثر ع ال A, B

**نطلع بمعلومه هنا:**  
ان ال 3 مرتبطين ببعض بشكل متشابك

**والنتيجه كارثه:**
- ال **Reusability** صفر حرفيا 
- ال **Testability** صعب جدا 
- ال **Maintainability** كارثه 

---

## حلول مشكلة Coupling

عشان تكسر موضوع ال Coupling معاك 3 حلول:

### 1- Introduce Interface

الفكره انه بدل ما يخلي ال Client يعتمد ع Class بيخليه يعتمد ع **Abstraction**

**مثال:**  
`OrderService => IOrderRepository`

**بسبب انه بيعالج:**
- ال Tight Coupling To Implementation
- الاعتماد ع Concrete Classes
- صعوبه ف الاستبدال و الاختبار 

**وده نقطه الاساس ف مبدأ ال DIP و مفهوم ال Dependency Injection**

### 2- Reverse Dependency

فكرته انه **عكس اتجاه الاعتماد**

**بدل ما يكون كده:**  
`UI => Data`

**يكون كده:**  
`UI => Abstraction <= Data`

**بسبب انه بيعالج:**
- ال Coupling بين ال Layers 
- وان ال High-level modules تعتمد ع ال Low-level modules

**ودى كمان نقطه اساس ف ال Clean Architecture + SOLID**

### 3- Introduce Mediator

فكرته انه بدل ما كل Object يعرف الباقي  
**كله يتعامل مع Mediator واحد**

**شكله:**  
`A => Mediator <= B`

**بسبب انه بيعالج:**
- ال Coupling بين Objects كتير بتكلم بعض

**خلي بالك:** مش مناسب لكل الحالات

### من الاخر

- **Introduce Interface**: بيقول انا جاى احل ع مستوى ال **Design**
- **Introduce Mediator**: بيقول انا جاى احل ع مستوى ال **Behavioral**
- **Reverse Dependency**: بيقول انا جاى احل ع مستوى ال **Architecture**

---

## لماذا تعتبر `new` من Code Smells؟

### السؤال الاهم: ليه ال `new` تعتبر من ال Code Smell وممكن تبقي سبب ف مشاكل ال Dependencies؟

بما ان كلمه `new` من ضمن وظائفها هى استدعاء ال Constructor  
وال Constructor جزء من ال **Implementation Details**

**نطلع من الكلام ده ان:**  
`new = الاعتماد ع ال Implementation مش ال Abstraction`

### مثال للتوضيح

```csharp
var repo = new SqlRepository();
```

المثال ده بيقولك ي عم العالم انا:
- مرتبط بتنفيذ محدد 
- صعب تغيرى
- صعب اختباري

**وعشان كده:**  
ده ال بيخلي كلمه `new` مش مشكلة في حد ذاته  
**المشكلة إنك بتستخدمها ف المكان الغلط** و تظهر ك Code Smell

---

## المشاكل الناتجة عن سوء استخدام Dependencies

### 1️⃣ Inability to Change Implementation

فيما معناه أن أي تعديل في ال Implementation **بيجبرك تعدل ع كود شغال بالفعل**

وده بيعرض النظام للكسر وبينتج عنه **انتهاك مباشر لمبدأ Open/Closed**

#### نقطه مهمة
المشكله دى مش مقتصره بس ع انك مش عارف تستبدل فقط

دى كمان ممكن تظهر ف حاله انك ضيفت **Behavior جديد** للكلاس

اينعم بيخدم الدومين لكن ممكن يكون ف مخاطرة بكسر حاجات شغالة

#### من الاخر

**التغير مش بيساوى الاستبدال فقط**

كمان لازم يكون يوم لما اجي اضيف سلوك جديد يتم من غير تعديل ع كود قديم او كسره ب اى شكل

#### لو عايز تريح دماغك وتتبع المنطق صح

الاضافات لو معقده وتفاصيل كتيره الاصح انك تعمل **Implementation منفصل**

وتتم التبديل ما بينهم عن طريق ال **Abstraction + Dependency Injection**

**عشان لو معملتش حاجه زى كده:**  
هتلاقي الكلاس ف الاخر بقي كبير جدا و المنطق بتاعه معقد + صعب ف اختباره + لما تفتح الكلاس مبتبقاش عارف مسؤولياته اى بالظبط

### 2️⃣ Chaining of Implicit Dependencies

فيما معناه ان ال Client بيعتمد ع Dependency  
وال Dependency بتعمد ع غيرها

**المشكله هنا:**  
ان اي كلاس ف السلسلة مش موجود او حصل فيه مشكله احب اقولك ان ال App **هينكد عليك ويكراش** 💥

وبكده ال Client بيتأثر فى حاجات هو مش عارف عنها حاجه اصلا

### 3️⃣ Untestability

الاعتماد المباشر ع Concrete Classes بينتج عنه **عدم استخدام Mocks و Stubs**

وبيحول ال **Unit Test** الي **Integration Test** غصب عنك

**بسبب انه:**  
لازم يختبر كل ما هو ال Concrete Classes بيعتمد عليه ك Explicit او Implicit

**وطبعا ده كارثه بينتج عنه:**
- اختبار بطئ بسبب ال I/O
- ولو ال Chain مش شغاله بطريقه طبيعيا هتلاقي ال Test بيطلع نتيجه غريبه ده ف حاله انه متطلعش Error اصلا

#### حلها

انك تستخدم طبعا: **Interface + DI**

الاختبار هيبقي سريع بسبب انك استبدلت ال **(I/O)** بال **(Mock or Stub)**

وده بيشتغل ف ال Memory انه بيعمل Object Fake ب Implement الانترفيس

و بكده تختبر **Behavior فقط**

ويبقي كده **احلى Unit Test ع عيونك** 😉

#### احفظ الجمله دى

**لو مقدرتش تعمل Unit Test يبقي التصميم من البداية غلط 110%**

### 4️⃣ Inappropriate Intimacy

هي حالة ف التصميم لما ال Class يعرف **تفاصيل داخلية** عن Class آخر أكثر من اللازم

#### يعني

Class A يستخدم Class B

**مش بس يستخدمه**، لكنه يقدر يشوف بياناته الداخلية أو يعدل عليها مباشرة

**ده بيخالف ال Encapsulation** اللي مفروض تحمي التفاصيل الداخلية لكل Class

#### حلها

انك تعطي فقط للكلاس ال هو محتاجه فقط وتخفي و تحافظ ع حاله الكلاس الاخر

وكمان يوم لما تباصي ليه حاجه يبقي عن طريق واجهه بشروط برضه

**اوعي تعملها جمعيه خيريه وتفتح الدنيا ع البحري هتندم ي عزيزي** 🫣

---

## Dependency Injection (DI)

### اى هو ال DI؟

هو **Technique** بيقول:  
بدل ما ال Class هو ال يصنع ال Dependencies الخاصه بي بنفسه  
**استقبلها من الخارج وخلصنا**

### فى الواقع

- انت بتطبخ الأكل بنفسك ❌
- انت بتطلب الأكل دليفرى ✅

### ف مجالنا

- كلاس بيعمل `new` لل Dependencies ❌
- كلاس بيستلم Dependencies جاهزة ✅

### عرفت ليه هو عم الناس؟

عشان هو **الأفضل لكل المشاكل ال ذكرنها ف البوستات السابقه**

---

## أنواع DI

### 1️⃣ Constructor Injection

من اسمه انت بتعمل بما يسمى بال Inject ف ال Constructor

#### مزاياه

- ال Dependencies ال الكلاس بيحتاجها من البدايه **بتكون واضحه ف ال Constructor**  
  وبكده اى حد هيعمل Object من الكلاس هيبقي عارف اى ال Dependencies ال محتاجها  
  ومينفعش الكلاس يبقي موجود من غيرها

- بيضمن ان دايما ال Object هيكون **ف حاله امان**

- سهولة ف الأختبار عن طريق تقدر تبعت **Mocks أو Stubs** بسهولة أثناء Unit Testing

- ال Dependencies بتكون **Immutable** ف حاله استخدامك لل `readonly`  
  ده بيخلي الكود أمان اكثر ف انك مش عايز اى تعديل يحصل ع ال Dependencies بعد أنشاء ال Object

### 2️⃣ Property Injection

فيما معنا ان ال Dependency ب Inject **بعد أنشاء ال Object**

الكلاس **مش معتمد عليه بشكل كامل** بيكون ضمنيا ع حسب ممكن ناديت ع ال Property ولا لا

#### المشاكل ال ممكن يعملها النوع ده

- ان ممكن يكون الكلاس ف **حاله غير صحيحه**
- ال Dependency ممكن تكون **Null**
- هبقي مجبر ان اعمل **Null-Check** ف كل استخدام

#### استخدامها بيكون نادر الا لو

- عايز يكون ال Dependency **اختياري**
- و لو ال Framework ال بتستخدمه يكون **فارض عليك** حاجه زى كده

### 3️⃣ Method Injection

من اسمها شارحها نفسها:  
ال Dependency بت Inject مع كل استدعاء لل Method

وبكده الكلاس **مش بيحتفظ بيها ك State** بتنتهي مع انتهاء ال Method

#### مشاكلها

- تعقيد ع ال Client
- ال Parameters كتير مش احسن حاجه ف ال API
- مش مناسبه لمعظم حالات ال Business Logic

#### استخدامها بيكون نادر الا لو

- عايز ال Dependency **تتغير مع كل Call**
- وان ال Dependency **تستخدم ف Method واحده فقط**

---

## واخيرا باين التحيز من الكلام لأنهي نوع هو الافضل

### 🏆 Constructor Injection

- عشان هو **الحل القياسي** لكسر ال Tight Coupling
- اداره و التحكم ف كل ال Dependencies
- معظم ال **Dependency Injection Container** بيتعامل مع النوع ده

**الانواع التانية هي مش خطأ** ولكن **لا تستخدم ك قاعدة عامه**

---

## الخلاصة

### وهنا ينتهي البوست بى هو ليه ال DI الحل الأفضل

#### 1️⃣ بيعالج مشاكل ال Tight Coupling عن طريق

- عن طريق الأعتماد ع ال **Abstraction**
- غير مهتم بى انشاء ال Dependencies

#### 2️⃣ بيحقق مبدأ ال DIP

- ال High-Level Modules **لا تعتمد** ع ال Low-Level Modules
- الاثنين بيعتمده ع ال **Abstraction**
- ال Implementation بيعتمد ع ال Abstraction وليس العكس

#### 3️⃣ بيحل مشاكل

- استخدام ال `new` ف الأماكن الخطأ
- ال Inability to Change Implementation
- ال Chaining of Implicit Dependencies
- ال Untestability
- ال Inappropriate Intimacy

### مع ال DI

الكلاس **مسؤول عن منطق العمل فقط**

انما انشاء وربط ال Dependencies هتكون مهمة ال **DI Container**  
او ممكن تنشئها ف ال **Composition Root**

### متنساش

**DI Container Lifetimes:**
- Singleton
- Scoped
- Transient

---

## 📚 مصادر إضافية

هذا التلخيص مستخرج من قراءات متعددة في كتب الـ Software Design والـ Clean Architecture.

---

## 🤝 المساهمة

هذا الريبو جزء من جهد لنشر العلم وتبسيطه في المجتمع العربي.  
إذا وجدت أي خطأ أو عندك إضافة مفيدة، لا تتردد في فتح Issue أو Pull Request.

---

**صنع بـ ❤️ لنشر العلم في مجتمعنا العربي**
