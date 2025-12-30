using System;
using System.Collections.Generic;
using System.Linq;

namespace Encapsulation.Exercises
{
    public class User
    {
        private string username;
        private string passwordHash;  // لا نحتفظ بالكلمة الأصلية!
        private List<string> loginHistory;
        private int failedAttempts;
        private bool isLocked;

        public User(string username, string password)
        {
            Username = username;
            SetPassword(password);
            loginHistory = new List<string>();
            failedAttempts = 0;
            isLocked = false;
        }

        public string Username
        {
            get { return username; }
            private set
            {
                if (!string.IsNullOrWhiteSpace(value) && value.Length >= 4)
                    username = value;
                else
                    throw new ArgumentException("اسم المستخدم يجب أن يكون 4 أحرف على الأقل");
            }
        }

        public bool IsLocked => isLocked;

        private string HashPassword(string password)
        {
            // في الواقع نستخدم SHA256 أو Bcrypt
            // هنا نستخدم طريقة بسيطة للتوضيح
            return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(password));
        }

        private bool IsPasswordValid(string password)
        {
            // Validation قوي
            if (string.IsNullOrWhiteSpace(password) || password.Length < 8)
                return false;

            bool hasUpper = password.Any(char.IsUpper);
            bool hasLower = password.Any(char.IsLower);
            bool hasDigit = password.Any(char.IsDigit);
            bool hasSpecial = password.Any(c => !char.IsLetterOrDigit(c));

            return hasUpper && hasLower && hasDigit && hasSpecial;
        }

        public void SetPassword(string newPassword)
        {
            if (!IsPasswordValid(newPassword))
            {
                Console.WriteLine("❌ كلمة المرور ضعيفة!");
                Console.WriteLine("   يجب أن تحتوي على:");
                Console.WriteLine("   • 8 أحرف على الأقل");
                Console.WriteLine("   • حروف كبيرة");
                Console.WriteLine("   • حروف صغيرة");
                Console.WriteLine("   • أرقام");
                Console.WriteLine("   • رموز خاصة");
                throw new ArgumentException("كلمة المرور ضعيفة");
            }

            passwordHash = HashPassword(newPassword);
            Console.WriteLine("✅ تم تعيين كلمة المرور");
        }

        public bool VerifyPassword(string password)
        {
            if (isLocked)
            {
                Console.WriteLine("❌ الحساب مقفول!");
                return false;
            }

            string hash = HashPassword(password);
            if (hash == passwordHash)
            {
                failedAttempts = 0;
                loginHistory.Add($"{DateTime.Now:yyyy-MM-dd HH:mm} - نجح");
                Console.WriteLine("✅ كلمة المرور صحيحة");
                return true;
            }
            else
            {
                failedAttempts++;
                loginHistory.Add($"{DateTime.Now:yyyy-MM-dd HH:mm} - فشل");

                if (failedAttempts >= 3)
                {
                    isLocked = true;
                    Console.WriteLine("❌ الحساب مقفول بسبب محاولات فاشلة");
                }
                else
                {
                    Console.WriteLine($"❌ كلمة المرور خاطئة ({failedAttempts}/3)");
                }
                return false;
            }
        }

        public void UnlockAccount()
        {
            isLocked = false;
            failedAttempts = 0;
            Console.WriteLine("✅ تم فتح الحساب");
        }

        public void PrintLoginHistory()
        {
            Console.WriteLine($"\n📋 سجل الدخول ({username}):");
            foreach (var log in loginHistory.TakeLast(5))
            {
                Console.WriteLine($"  {log}");
            }
        }
    }
}
