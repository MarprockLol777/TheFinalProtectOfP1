using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace TheFinalProtectOfP1
{
    public class MyDataContex : DbContext
    {
        public DbSet<Student>  Students { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-TDQPUV8\\SQLEXPRESS;Database=My_Data_Base_About_Strudent_Agent;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=true;");
        }
    }
}
