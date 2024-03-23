using ASPCoreWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ASPCoreWebAPI.Data
{
    public static class EntityDatabaseMock
    {
        private static List<Entity> _entities = new List<Entity>
        {
            // Initialize with a sample entity for demonstration
            new Entity
            {
                Id = "1",
                Deceased = false,
                Gender = "Male",
                Addresses = new List<Address>
                {
                    new Address { AddressLine = "123 Main St", City = "Ayodhya", Country = "India" }
                },
                Dates = new List<Date>
                {
                    new Date { DateType = "Birth", Dates = new DateTime(2003, 07, 07) }
                },
                Names = new List<Name>
                {
                    new Name { FirstName = "Sumit", MiddleName = "Kumar", Surname = "Mishra" }
                }
            }
        };


        public static List<Entity> GetAllEntities() => _entities;

        public static List<Entity> GetEntities(string? search = null)
             {
            if (string.IsNullOrWhiteSpace(search))
            {
                return _entities;
            }

            var lowerCaseSearch = search.ToLower();
            return _entities.Where(entity =>
                entity.Addresses != null && entity.Addresses.Any(address =>
                    (!string.IsNullOrWhiteSpace(address.Country) && address.Country.ToLower().Contains(lowerCaseSearch)) ||
                    (!string.IsNullOrWhiteSpace(address.AddressLine) && address.AddressLine.ToLower().Contains(lowerCaseSearch))) ||
                entity.Names.Any(name =>
                    (!string.IsNullOrWhiteSpace(name.FirstName) && name.FirstName.ToLower().Contains(lowerCaseSearch)) ||
                    (!string.IsNullOrWhiteSpace(name.MiddleName) && name.MiddleName.ToLower().Contains(lowerCaseSearch)) ||
                    (!string.IsNullOrWhiteSpace(name.Surname) && name.Surname.ToLower().Contains(lowerCaseSearch))
                )
            ).ToList();
        }


        public static Entity GetEntityById(string id)
        {
          Entity entity = _entities.First(e => e.Id == id);
            return entity;
        }

        public static void AddEntity(Entity entity)
        {
            _entities.Add(entity);
        }

        public static void UpdateEntity(string id, Entity updatedEntity)
        {
            var index = _entities.FindIndex(e => e.Id == id);
            if (index != -1)
            {
                _entities[index] = updatedEntity;
            }
        }

        public static void DeleteEntity(string id)
        {
            _entities.RemoveAll(e => e.Id == id);
        }
    }
}


