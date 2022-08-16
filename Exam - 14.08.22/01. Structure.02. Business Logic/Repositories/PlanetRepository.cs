using PlanetWars.Models.Planets.Contracts;
using PlanetWars.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlanetWars.Repositories
{
    public class PlanetRepository : IRepository<IPlanet>
    {
        private readonly List<IPlanet> models;

        public PlanetRepository()
        {
            models = new List<IPlanet>();
        }
        public IReadOnlyCollection<IPlanet> Models => models;

        public void AddItem(IPlanet model)
        {
            models.Add(model);
        }

        public IPlanet FindByName(string name)
        {
            return models.FirstOrDefault(w => w.Name == name);
        }

        public bool RemoveItem(string name)
        {
            return models.Remove(models.FirstOrDefault(w => w.Name == name));
        }
    }
}
