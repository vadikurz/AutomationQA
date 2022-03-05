﻿using System.Collections.Generic;
using System.Linq;
using FifthTask.Exceptions;
using FifthTask.Vehicles;

namespace FifthTask
{
    public class CarPark
    {
        public Dictionary<string, Vehicle> Vehicles { get; set; }

        public CarPark()
        {
            Vehicles = new Dictionary<string, Vehicle>();
        }

        public void Add(Vehicle vehicle, string id)
        {
            if (Vehicles.ContainsKey(id))
            {
                throw new AddException("Vehicle with this ID already exists");
            }

            Vehicles.Add(id, vehicle);
        }

        public Vehicle GetAutoByParameter(string parameter, string value)
        {
            var vehicles = GetAutosByParameter(parameter, value);
            if (vehicles.Count != 1)
            {
                throw new AmbiguousMatchException("More than one match found");
            }

            return vehicles.First();
        }

        public List<Vehicle> GetAutosByParameter(string parameter, string value)
        {
            if (!IsParameterExists(parameter))
            {
                throw new NotFoundParameterException("Parameter not found");
            }

            if (!IsValueByParameterExists(parameter, value))
            {
                throw new NotFoundValueByParameterException("No matches found");
            }

            return Vehicles.Values.Where(vehicle => vehicle.GetType().GetProperties().Any(property =>
                    property.Name.ToLower() == parameter.ToLower() && property.GetValue(vehicle)?.ToString()?.ToLower() == value.ToLower())).ToList();
        }

        public void UpdateAuto(string id, Vehicle vehicle)
        {
            if (!Vehicles.ContainsKey(id))
            {
                throw new UpdateAutoException("No such identifier exists");
            }

            Vehicles[id] = vehicle;
        }

        public void RemoveAuto(string id)
        {
            if (!Vehicles.ContainsKey(id))
            {
                throw new RemoveAutoException("No such identifier exists");
            }

            Vehicles.Remove(id);
        }
        
        private bool IsParameterExists(string parameter) =>
            Vehicles.Values.Any(vehicle => vehicle.GetType()
                .GetProperties()
                .Any(property => property.Name.ToLower() == parameter.ToLower()));

        private bool IsValueByParameterExists(string parameter, string value) =>
            Vehicles.Values.Any(vehicle => vehicle.GetType()
                .GetProperties()
                .Any(property =>
                    property.Name.ToLower() == parameter.ToLower() && property.GetValue(vehicle)?.ToString()?.ToLower() == value.ToLower()));
    }
}
