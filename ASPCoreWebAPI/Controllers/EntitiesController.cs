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
        public ActionResult<IEnumerable<Entity>> GetSearch([FromQuery] string? search)
        {
            var entities = EntityDatabaseMock.GetEntities(search);
            return Ok(entities);
        }

        [HttpPost]
        public ActionResult Post([FromBody] Entity entity)
        {
            EntityDatabaseMock.AddEntity(entity);
            return CreatedAtAction(nameof(Get), new { id = entity.Id }, entity);
        }

        [HttpPut("{id}")]
        public ActionResult Put(string id, [FromBody] Entity entity)
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
