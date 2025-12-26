    /*
    المتطلبات:
    ──────────
    أنشئ نظام حيوانات يحتوي على:
    
    1. فئة أب Animal مع:
       - خصائص: name, age
       - دوال: virtual MakeSound(), virtual Move(), virtual Eat()
    
    2. فئات وارثة:
       - Dog: يرث من Animal
         * Override: MakeSound("واف واف"), Move("يركض")
         * دالة جديدة: Fetch()
       
       - Cat: يرث من Animal
         * Override: MakeSound("مياو"), Move("يمشي بخفة")
         * دالة جديدة: Scratch()
       
       - Bird: يرث من Animal
         * Override: MakeSound("تيوت"), Move("يطير")
         * دالة جديدة: BuildNest()
    
    3. AnimalSanctuary يدير قائمة الحيوانات
       - AddAnimal()
       - MakeAllSounds()
       - MoveAll()
       - PrintAnimalInfo()
    */