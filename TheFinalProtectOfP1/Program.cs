using Azure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;
using System.Security.AccessControl;
using System.Text;
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
    public int Scores { get; set; }
    public int Overall_score { get; set; }



    public Student()  // constructor vacio XD 
    {
        
    }


    public Student(int id, string name, string Lastname, int age, int sex, string address, int score, int overall_score)
    {
        Id = id;
        Name = name;
        LastName = Lastname;
        Age = age;
        Sex = sex;
        Address = address;
        Scores = score;
        Overall_score = overall_score;
    }


}



