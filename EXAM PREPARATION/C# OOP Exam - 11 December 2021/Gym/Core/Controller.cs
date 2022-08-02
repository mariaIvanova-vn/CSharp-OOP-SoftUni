﻿using Gym.Core.Contracts;
using Gym.Models.Athletes;
using Gym.Models.Equipment;
using Gym.Models.Gyms;
using Gym.Models.Gyms.Contracts;
using Gym.Repositories;
using Gym.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gym.Core
{
    public class Controller : IController
    {
        private EquipmentRepository equipment;
        private readonly ICollection<Gym.Models.Gyms.Gym> gyms;
        public Controller()
        {
            gyms = new List<Gym.Models.Gyms.Gym>();
            equipment = new EquipmentRepository();
        }
        public string AddAthlete(string gymName, string athleteType, string athleteName, string motivation, int numberOfMedals)
        {
            var gym = gyms.FirstOrDefault(g => g.Name == gymName);
            bool isAdded = false;
            if (athleteType == "Boxer")
            {
                if (gym.GetType().Name == nameof(BoxingGym))
                {
                    isAdded = true;
                    Boxer boxer = new Boxer(athleteName,motivation,numberOfMedals);
                    gym.AddAthlete(boxer);
                }
            }
            else if (athleteType == "Weightlifter")
            {
                if (gym.GetType().Name == nameof(WeightliftingGym))
                {
                    isAdded = true;
                    Weightlifter weightlifter = new Weightlifter(athleteName, motivation, numberOfMedals);
                    gym.AddAthlete(weightlifter);
                }
            }
            else
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidAthleteType);
            }
            if (isAdded)
            {
                return String.Format(OutputMessages.EntityAddedToGym, athleteType, gymName);
            }
            return OutputMessages.InappropriateGym;
        }

        public string AddEquipment(string equipmentType)
        {
            if (equipmentType == "BoxingGloves")
            {
                equipment.Add(new BoxingGloves());
            }
            else if (equipmentType == "Kettlebell")
            {
                equipment.Add(new Kettlebell());
            }
            else
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidEquipmentType);
            }
            return $"Successfully added {equipmentType}.";
        }

        public string AddGym(string gymType, string gymName)
        {
            if (gymType == "BoxingGym")
            {
                gyms.Add(new BoxingGym(gymName));
            }
            else if (gymType == "WeightliftingGym")
            {
                gyms.Add(new WeightliftingGym(gymName));
            }
            else
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidGymType);
            }
            return $"Successfully added {gymType}.";
        }

        public string EquipmentWeight(string gymName)
        {
            var gym = gyms.FirstOrDefault(g => g.Name == gymName);
            return String.Format(OutputMessages.EquipmentTotalWeight,gymName, gym.EquipmentWeight);
        }

        public string InsertEquipment(string gymName, string equipmentType)
        {
            Equipment eq = (Equipment)equipment.FindByType(equipmentType);
            if (eq == null)
            {
                throw new InvalidOperationException(String.Format(ExceptionMessages.InexistentEquipment, equipmentType));
            }
            var gym = gyms.FirstOrDefault(g=>g.Name == gymName);    
            gym.AddEquipment(eq);
            equipment.Remove(eq);
            return String.Format(OutputMessages.EntityAddedToGym, equipmentType, gymName);
        }

        public string Report()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in gyms)
            {
                sb.AppendLine(item.GymInfo());
            }
            return sb.ToString().Trim();
        }

        public string TrainAthletes(string gymName)
        {
            var gym = gyms.FirstOrDefault(g => g.Name == gymName);
            gym.Exercise();
            return String.Format(OutputMessages.AthleteExercise, gym.Athletes.Count);
        }
    }
}
