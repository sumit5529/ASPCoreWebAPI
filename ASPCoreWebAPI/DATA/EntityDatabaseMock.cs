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
                    new Address { AddressLine = "123 Pithla", City = "Ayodhya", Country = "India" }
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



        public static List<Entity> GetEntities(
        string? search = null,
        string? gender = null,
        DateTime? startDate = null,
        DateTime? endDate = null,
        List<string>? countries = null,
        int pageNumber = 1,
        int pageSize = 10,
        string sortBy = "Name", // Default sorting by Name
        string sortOrder = "asc" // Default sort order
 )
        {
            var query = _entities.AsQueryable();

            // Search by text in names, address line, or country
            if (!string.IsNullOrWhiteSpace(search))
            {
                var lowerCaseSearch = search.ToLower();
                query = query.Where(entity =>
                    entity.Addresses != null && entity.Addresses.Any(address =>
                        (!string.IsNullOrWhiteSpace(address.Country) && address.Country.ToLower().Contains(lowerCaseSearch)) ||
                        (!string.IsNullOrWhiteSpace(address.AddressLine) && address.AddressLine.ToLower().Contains(lowerCaseSearch))) ||
                    entity.Names.Any(name =>
                        (!string.IsNullOrWhiteSpace(name.FirstName) && name.FirstName.ToLower().Contains(lowerCaseSearch)) ||
                        (!string.IsNullOrWhiteSpace(name.MiddleName) && name.MiddleName.ToLower().Contains(lowerCaseSearch)) ||
                        (!string.IsNullOrWhiteSpace(name.Surname) && name.Surname.ToLower().Contains(lowerCaseSearch))));
            }

            // Filter by gender, handling null
            if (!string.IsNullOrWhiteSpace(gender))
            {
                query = query.Where(entity => entity.Gender != null && entity.Gender.Equals(gender, StringComparison.OrdinalIgnoreCase));
            }
           

            // Filter by start date and end date, handling null dates
            if (startDate.HasValue)
            {
                query = query.Where(entity => entity.Dates != null && entity.Dates.Any(d => d.Dates.HasValue && d.Dates.Value.Date >= startDate.Value.Date));
            }

            if (endDate.HasValue)
            {
                query = query.Where(entity => entity.Dates != null && entity.Dates.Any(d => d.Dates.HasValue && d.Dates.Value.Date <= endDate.Value.Date));
            }

            // Filter by countries
            if (countries != null && countries.Count > 0)
            {
                query = query.Where(entity =>
                    entity.Addresses != null && entity.Addresses.Any(address =>
                        address.Country != null && countries.Contains(address.Country)));
            }

            // Sorting (example based on Name property in asc order)
            var filteredEntities = query.ToList(); // Materialize query to apply in-memory operations

            IEnumerable<Entity> sortedEntities;
            if (sortOrder.ToLower() == "asc")
            {
                sortedEntities = filteredEntities.OrderBy(e => e.Names.FirstOrDefault()?.FirstName);
            }
            else
            {
                sortedEntities = filteredEntities.OrderByDescending(e => e.Names.FirstOrDefault()?.FirstName);
            }

            // Apply pagination to the in-memory sorted list
            var paginatedEntities = sortedEntities.Skip((pageNumber - 1) * pageSize).Take(pageSize);

            return paginatedEntities.ToList();

           

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


