using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;

namespace taskAPI.Model
{
    public class ToDoContext : DbContext
    {
        public ToDoContext(DbContextOptions<ToDoContext> options): base(options){}
        public DbSet<ToDoTask> Tasks { get; set; }
    }
    public class ToDoTask {

        [Key]
        public int Id {get;init;}

        [MaxLength(100)]
        public string Status {get;set;}
        [Required, MaxLength(300)]
        public string TaskName{get;set;}
        public string TaskDescription{get;set;}
        [Required]
        public DateTime CreateDate {get;set;}
        public DateTime? DueDate {get;set;}
    }
}