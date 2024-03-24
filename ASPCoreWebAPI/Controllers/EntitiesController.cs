using Microsoft.AspNetCore.Mvc;
using ASPCoreWebAPI.Models;
using ASPCoreWebAPI.Data;
using System.Collections.Generic;

namespace ASPCoreWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EntitiesController : ControllerBase
    {
        
        [HttpGet("{id}")]
        public ActionResult<Entity> Get(string id)
        {
            var entity = EntityDatabaseMock.GetEntityById(id);
            if (entity == null)
            {
                return NotFound();
            }
            return Ok(entity);
        }

        [HttpGet]
        public ActionResult<IEnumerable<Entity>> GetSearch(
       [FromQuery] string? search,
       [FromQuery] string? gender,
       [FromQuery] DateTime? startDate,
       [FromQuery] DateTime? endDate,
       [FromQuery] List<string>? countries,
       [FromQuery] int pageNumber = 1, // Default to the first page
       [FromQuery] int pageSize = 10, // Default page size
       [FromQuery] string sortBy = "Name", // Default sort by Name
       [FromQuery] string sortOrder = "asc" // Default sort order is ascending
   )
        {
            // Adjust the call to GetEntities to pass the parameters for filter,pagination and sorting along with the filters
            var entities = EntityDatabaseMock.GetEntities(search, gender, startDate, endDate, countries, pageNumber, pageSize, sortBy, sortOrder);
            return Ok(entities);
        }




       
        [HttpPost]

        public ActionResult Post(Entity entity)
        {
            EntityDatabaseMock.AddEntity(entity);
            return Ok(entity);
        }

        [HttpPut("{id}")]
        public ActionResult Put(string id,Entity entity)
        {
            var existingEntity = EntityDatabaseMock.GetEntityById(id);
            if (existingEntity == null)
            {
                return NotFound();
            }
            EntityDatabaseMock.UpdateEntity(id, entity);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            var existingEntity = EntityDatabaseMock.GetEntityById(id);
            if (existingEntity == null)
            {
                return NotFound();
            }
            EntityDatabaseMock.DeleteEntity(id);
            return NoContent();
        }
    }
}
