// See https://aka.ms/new-console-template for more information
using LinqPractices;
using LinqPractices.DbOperations;


Console.WriteLine("Hello, World!");
DataGenerator.Initialize();
var _context = new LinqDbContext();
var students = _context.Students.ToList<Student>();

Console.WriteLine("***** Find *****"); 
var student = _context.Students.Where(student => student.StudentId == 3).FirstOrDefault();
Console.WriteLine(student.Name); 

student = _context.Students.Find(2);
Console.WriteLine(student.Name); 

Console.WriteLine("***** FirstOrDefault *****");
student = _context.Students.Where(student=> student.Surname == "Arda").FirstOrDefault();
Console.WriteLine(student.Name);  

student = _context.Students.FirstOrDefault(x => x.Surname == "Arda");
Console.WriteLine(student.Name); 

Console.WriteLine("***** SingleOrDefault *****"); 
student = _context.Students.SingleOrDefault(student => student.Name == "Deniz");
Console.WriteLine(student.Surname); 

Console.WriteLine("***** ToList *****");
var studentlist = _context.Students.Where(student=> student.ClassId == 2).ToList(); 
Console.WriteLine(studentlist.Count);

Console.WriteLine("***** OrderBy *****");
students = _context.Students.OrderBy(x => x.StudentId).ToList();
foreach (var st in students)
{
    Console.WriteLine(st.StudentId + " - " + st.Name + "" + st.Surname);
} 

Console.WriteLine("***** OrderByDescending *****");
students = _context.Students.OrderByDescending(x => x.StudentId).ToList();
foreach (var st in students)
{
    Console.WriteLine(st.StudentId + " - " + st.Name + "" + st.Surname);
} 

var anonymousObjResult = _context.Students
                                .Where(b => b.ClassId== 2)
                                .Select(b => new {
                                            Id = b.StudentId,
                                            fullName = b.Name+ " "+ b.Surname });

    foreach (var obj in anonymousObjResult)
    {
        Console.WriteLine(obj.Id+ " - " + obj.fullName);
    }    