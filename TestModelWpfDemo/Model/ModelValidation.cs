using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestModelWpfDemo.Model
{
    public class ModelValidation
    {
        private static bool AgeValidation(int age,int MinAge,int MaxAge)
        {
            if (age>=MinAge && age<=MaxAge)
            {
                return true;
            }
            else return false;
        }

        private static bool NameContainNonDigits(string name)
        {
            if (!name.All(char.IsLetter))
            {
                return true;
            }
            return false;
        }
        private static bool NameIsEmpty(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return true;
            }
            return false;
        }

        public static List<string>  Validate(Student student)
        {
            int minAge = 16;
            int MaxAge = 100;

            List<string> Alltests = new List<string>();

            if (!AgeValidation(student.Age, minAge, MaxAge))
            {
                Alltests.Add($"Age must be betwen {minAge} and {MaxAge}");
            }
           

            if (NameIsEmpty(student.FirstName))
            {
                Alltests.Add($"First name field is mandatory.");
            }
            
            if (NameIsEmpty(student.LastName))
            {
                Alltests.Add($"Last name field is mandatory.");
            }

            if (NameContainNonDigits(student.LastName))
            {
                Alltests.Add($"Last name field must contain only letters.");
            }
            if (NameContainNonDigits(student.FirstName))
            {
                Alltests.Add($"First name field must contain only letters.");
            }

            return Alltests;
        }
    }
}
