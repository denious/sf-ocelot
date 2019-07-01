using System;
using System.ComponentModel.DataAnnotations;

namespace Phx.People.Data
{
    [Serializable]
    public class Person
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }

        public Person()
        {
        }

        protected Person(int id, string name, int age)
        {
            Id = id;
            Name = name;
            Age = age;
        }
    }
}
