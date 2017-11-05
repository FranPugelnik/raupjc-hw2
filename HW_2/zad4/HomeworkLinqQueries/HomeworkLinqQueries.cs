using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Class1
{ 
    public class HomeworkLinqQueries
    {
        public static string[] Linq1(int[] intArray)
        {
            return intArray.GroupBy(x => x)
                           .Select(s => $"Broj {s.Key} ponavlja se {s.Count()} puta")
                           .OrderBy(x => x)
                           .ToArray();
        }
        public static University[] Linq2_1(University[] universityArray)
        {
            return universityArray.Where(x => x.Students.All(y => y.Gender.Equals(Gender.Male))).ToArray(); // ili y.Gender == gender.Male
        }
        public static University[] Linq2_2(University[] universityArray)
        {
            double average = universityArray.Average(x => x.Students.Length);
            return universityArray.Where(x => x.Students.Length < average).ToArray();
        }
        public static Student[] Linq2_3(University[] universityArray)
        {
            return universityArray.SelectMany(x => x.Students).Distinct().ToArray();
        }
        public static Student[] Linq2_4(University[] universityArray)
        {
            return universityArray.Where(x => x.Students.All(y => y.Gender == Gender.Male) || x.Students.All(y => y.Gender == Gender.Female))
                                  .SelectMany(x => x.Students)
                                  .Distinct()
                                  .ToArray();
        }
        public static Student[] Linq2_5(University[] universityArray)
        {
            return universityArray.SelectMany(x => x.Students)
                                  .GroupBy(y => y)
                                  .Where(z => z.Count() > 1)
                                  .Select(h => h.Key)
                                  .ToArray();
        }

    }
}
