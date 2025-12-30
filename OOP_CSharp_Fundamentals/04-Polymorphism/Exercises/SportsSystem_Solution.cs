
namespace Polymorphism.Exercises
{

    public interface ISport
    {
        string GetName();
        double GetCaloriesBurnedPerHour();
        int GetDifficulty();  // 1-10
    }


    public class Running : ISport
    {
        private double speed;  // كم/س

        public Running(double speed)
        {
            this.speed = speed;
        }

        public string GetName() => $"الجري ({speed} كم/س)";
        public double GetCaloriesBurnedPerHour() => 600 + (speed * 50);
        public int GetDifficulty() => 7;
    }

    public class Swimming : ISport
    {
        public string GetName() => "السباحة";
        public double GetCaloriesBurnedPerHour() => 800;
        public int GetDifficulty() => 8;
    }

    public class Cycling : ISport
    {
        private double distance;  // كم

        public Cycling(double distance)
        {
            this.distance = distance;
        }

        public string GetName() => $"الدراجة ({distance} كم)";
        public double GetCaloriesBurnedPerHour() => 500 + (distance * 30);
        public int GetDifficulty() => 6;
    }

    public class FitnessTracker
    {
        private List<ISport> activities = new();
        private double duration;  // بالساعات

        public void AddActivity(ISport sport, double hours)
        {
            activities.Add(sport);
            duration += hours;
            Console.WriteLine($"✅ تم إضافة {sport.GetName()} لمدة {hours} ساعات");
        }

        public void PrintFitnessSummary()
        {
            Console.WriteLine("\n💪 ملخص اللياقة البدنية:");
            double totalCalories = 0;
            int avgDifficulty = 0;

            foreach (var sport in activities)
            {
                double calories = sport.GetCaloriesBurnedPerHour();
                totalCalories += calories;
                avgDifficulty += sport.GetDifficulty();
            }

            avgDifficulty = activities.Count > 0 ? avgDifficulty / activities.Count : 0;

            Console.WriteLine($"  عدد الأنشطة: {activities.Count}");
            Console.WriteLine($"  إجمالي الساعات: {duration}");
            Console.WriteLine($"  السعرات المحروقة: {totalCalories * duration:F1}");
            Console.WriteLine($"  متوسط الصعوبة: {avgDifficulty}/10");
        }

        public void PrintMostEffective()
        {
            var mostEffective = activities.OrderByDescending(s => s.GetCaloriesBurnedPerHour()).FirstOrDefault();
            if (mostEffective != null)
            {
                Console.WriteLine($"\n🏆 أكثر نشاط فعالية: {mostEffective.GetName()}");
                Console.WriteLine($"   يحرق: {mostEffective.GetCaloriesBurnedPerHour():F1} سعرة/ساعة");
            }
        }
    }
}
