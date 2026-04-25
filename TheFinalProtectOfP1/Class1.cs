using System.ComponentModel.DataAnnotations;

public class Student
{
    [Key]
    public int Id { get; set; }

    [StringLength(100)]
    public string Name { get; set; }

    [StringLength(100)]
    public string LastName { get; set; }

    public int Age { get; set; }
    public int Sex { get; set; }

    [StringLength(250)]
    public string Address { get; set; }

    [StringLength(500)]          
    public string Scores { get; set; }

    public int Overall_score { get; set; }

    public Student() { }

    public Student(int id, string name, string lastname, int age, int sex,
                   string address, string scores, int overall_score)
    {
        Id = id;
        Name = name;
        LastName = lastname;
        Age = age;
        Sex = sex;
        Address = address;
        Scores = scores;
        Overall_score = overall_score;
    }
}