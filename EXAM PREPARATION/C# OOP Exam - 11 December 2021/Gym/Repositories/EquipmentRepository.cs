using Gym.Models.Equipment;
using Gym.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Gym.Models.Equipment.Contracts;

namespace Gym.Repositories
{
    public class EquipmentRepository : IRepository<IEquipment>
    {
        private List<IEquipment> models;
        public IReadOnlyCollection<IEquipment> Models { get; }

        public void Add(IEquipment model)
        {
           models.Add(model);
        }

        public IEquipment FindByType(string type)
        {
            return models.FirstOrDefault(e => e.GetType().Name == type);
        }

        public bool Remove(IEquipment model)
        {
           return models.Remove(model);    
        }
    }
}
