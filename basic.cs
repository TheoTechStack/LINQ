using System;
using System.Collections;

public class Program
{
    delegate bool IsTeenAger(Student stud);

    public static void Main()
    {
        IsTeenAger isTeenAger = delegate (Student s)
        {
            return s.Age > 12
            && s.Age < 20;
        };

        //The Lambda expression evolves from anonymous method by first removing the delegate keyword
        //and parameter type and adding a lambda operator =>.
        Func<Student, bool> isTeenAger2 = s => s.Age > 12 && s.Age < 20;

        Student stud = new Student() { Age = 25 };

        Console.WriteLine(isTeenAger(stud));
        Console.WriteLine(isTeenAger2(stud));

        IList<Student> studentList = new List<Student>() {
        new Student() { Id = 1, Name = "John", Age = 13} ,
        new Student() { Id = 2, Name = "Moin",  Age = 21 } ,
        new Student() { Id = 3, Name = "Bill",  Age = 18 } ,
        new Student() { Id = 4, Name = "Ram" , Age = 20} ,
        new Student() { Id = 5, Name = "Ron" , Age = 15 }
    };

       var filteredResult = from s in studentList
                             where s.Age > 12 && s.Age < 20
                             select s.Name;

        var filterOutOddAge = studentList.Where((student, age) =>
        {
            return (age % 2 == 0 ? true : false);
        });

        Console.WriteLine("\nStudent List ");
        foreach (var student in studentList)
        {
            Console.WriteLine(student.Name);
        }


        //getting types of values instead on unboxing
        IList mixedList = new ArrayList
        {
            0,
            "One",
            "Two",
            3,
            new Student() { Id = 1, Name = "Bill" }
        };

        var stringResult = from s in mixedList.OfType<string>()
                           select s;


        var intResult = from s in mixedList.OfType<int>()
                        select s;
     

        foreach (var str in stringResult)
            Console.WriteLine($"StringTpe {str} \n");

        foreach (var integer in intResult)
            Console.WriteLine($"IntType {integer} \n");

        IList<Student> unOrderedStudentList = new List<Student>() {
            new Student() { Id = 1, Name = "John", BirthDate = new DateTime(2009, 4, 15) },
            new Student() { Id = 2, Name = "Moin", BirthDate = new DateTime(2003, 11, 20) },
            new Student() { Id = 3, Name = "Bill", BirthDate = new DateTime(2006, 8, 5) },
            new Student() { Id = 4, Name = "Ram", BirthDate = new DateTime(2004, 2, 25) },
            new Student() { Id = 4, Name = "Ram", BirthDate = new DateTime(2000, 9, 30) }, 
            new Student() { Id = 5, Name = "Ron", BirthDate = new DateTime(2009, 10, 10) }
        };

        //now since we have two Ram we want to take the first student enrolled

        var thenByList = unOrderedStudentList
                         .OrderBy(s => s.Name)
                         .ThenBy(d => d.BirthDate);

        Console.WriteLine("\nThenBy:");

        foreach (var std in thenByList)
            Console.WriteLine("Name: {0}, BirthDate:{1}", std.Name, std.BirthDate);




        //Join
        IList<Student> studentListWithStandard = new List<Student>() {
        new Student() { Id = 1, Name = "John", StandardID =1 },
        new Student() { Id = 2, Name = "Moin", StandardID =1 },
        new Student() { Id = 4, Name = "Ram" , StandardID =2 },
        new Student() { Id = 5, Name = "Ron"  }
        };

        IList<Standard> standardList = new List<Standard>() {
        new Standard(){ StandardID = 1, StandardName="Standard 1"},
        new Standard(){ StandardID = 2, StandardName="Standard 2"},
        new Standard(){ StandardID = 3, StandardName="Standard 3"}
        };

        var innerJoin = studentListWithStandard.Join(// outer sequence 
                              standardList,  // inner sequence 
                              student => student.StandardID,    // outerKeySelector
                              standard => standard.StandardID,  // innerKeySelector
                              (student, standard) => new  // result selector
                              {
                                  student.Name,
                                  standard.StandardName
                              });

        Console.WriteLine("Joined Results By the Ids: \n");
        foreach (var obj in innerJoin)
        {

            Console.WriteLine("{0} - {1}", obj.Name, obj.StandardName);
        }
    }
}

public class Student
{
    public DateTime BirthDate { get; set; }
    public int Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public int ? StandardID { get; set; }
}

public class Standard
{
    public int StandardID { get; set; }
    public string StandardName { get; set; }
}
