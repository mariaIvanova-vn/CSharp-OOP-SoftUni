using System;
using System.Collections.Generic;
using System.Text;

namespace FoodShortage
{
    public class Robot : IIdentifiable
    {
        private string model;
        private string id;
        public string Id { get { return id; } set { id = value; } }
        public Robot(string model, string id)
        {
            Model = model;
            Id = id;
        }
        public string Model
        {
            get { return model; }
            set { model = value; }
        }
    }
}
