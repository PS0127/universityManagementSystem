namespace universityManagementSystem
{
    public class Persons
    {
        public int Id { get; set; }
        public string firstName { get; set; }
        public string lastName{ get; set; }
    }

    public class Student : Persons
    {
        public string course { get; set; }
    }

    public class Teacher : Persons
    {
        public string department { get; set; }
    }
