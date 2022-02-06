﻿using System.Reflection;

namespace DataLibrary.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public Gender Gender { get; set; }
    }
    public enum Gender
    {
        Male, Female, Other
    }
}
