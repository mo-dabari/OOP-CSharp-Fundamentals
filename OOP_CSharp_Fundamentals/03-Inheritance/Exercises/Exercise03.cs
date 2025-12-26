    // ═══════════════════════════════════════════════════════════
    // التمرين 3: نظام المركبات المتقدم
    // ═══════════════════════════════════════════════════════════
    
    /*
    المتطلبات:
    ──────────
    أنشئ نظام مركبات متقدم:
    
    1. Vehicle (الأب):
       - خصائص: brand, color, speed
       - virtual: Start(), Stop(), GetSpeed()
    
    2. Car: يرث من Vehicle
       - خصائص إضافية: numberOfDoors
       - override Start/Stop
    
    3. Motorcycle: يرث من Vehicle
       - خصائص إضافية: hasStorage
       - override Start/Stop
    
    4. Truck: يرث من Vehicle
       - خصائص إضافية: loadCapacity
       - override Start/Stop
       - دالة: LoadCargo()
    
    5. ElectricCar: يرث من Car
       - خصائص إضافية: batteryCapacity
       - override Start
       - دالة: Charge()
    
    6. Fleet Manager:
       - إدارة أسطول المركبات
       - بدء الجميع
       - إيقاف الجميع
       - تقرير السرعات
    */