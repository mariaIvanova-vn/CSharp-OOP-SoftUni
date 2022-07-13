using System;
using System.Collections.Generic;
using System.Text;

namespace FoodShortage
{
    public class Citizen : IIdentifiable, IBirthable, IBuyer
    {
        private string name;
        private int age;
        private string id;
        private string birthday;
        public string Id { get { return id; } set { id = value; } }
        public string Birthday { get { return birthday; } set { birthday = value; } }
        public Citizen(string name, int age, string id, string birthday)
        {
            Name = name;
            Age = age;
            Id = id;
            Birthday = birthday;
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public int Age
        {
            get { return age; }
            set { age = value; }
        }

        public int Food { get; private set; }

        public void BuyFood()
        {
            this.Food += 10;
        }
    }
}
