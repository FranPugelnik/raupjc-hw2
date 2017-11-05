using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Class1
{
    public class Student
    {
        public string Name { get; set; }
        public string Jmbag { get; set; }
        public Gender Gender { get; set; }
        public Student(string name, string jmbag)
        {
            Name = name;
            Jmbag = jmbag;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Student Stud))
                return false;
            return Jmbag.Equals(Stud.Jmbag);
        }

        public override int GetHashCode()
        {
            return Jmbag.GetHashCode();
        }

        public static bool operator == (Student s1, Student s2)
        {

            return s1.Jmbag.Equals(s2.Jmbag);
        }


        public static bool operator !=(Student s1, Student s2)
        {
            if (s1 == null) return true;
            if (s2 == null) return true;
            return !(s1 == s2);
        }


    }

    public enum Gender
    {
        Male, Female
    }
}
