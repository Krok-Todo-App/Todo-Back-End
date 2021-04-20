using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;

namespace taskAPI.Model
{
    public class ToDoContext : IdentityDbContext
    {
        public const string SchemaName = "public";
        public ToDoContext(DbContextOptions<ToDoContext> options): base(options){}
        public DbSet<ToDoTask> Tasks { get; set; }
    }
    public class ToDoTask {

        [Key]
        public int Id {get;init;}
        public bool completed {get;set;} = false;

        [Required, MaxLength(300)]
        public string TaskName{get;set;}
        public string TaskDescription{get;set;}
        
        public DateTime? StartDate {get;set;}
        public DateTime? DueDate {get;set;}
    }
}