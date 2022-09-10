using Microsoft.AspNetCore.Mvc;
using MySpot.Api.Entities;
using MySpot.Api.Services;

namespace MySpot.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReservationsController : ControllerBase
    {
        private readonly ReservationsService _service = new();

        [HttpGet]
        public ActionResult<Reservation[]> Get()
            => Ok(_service.GetAllWeekly());

        [HttpGet("{id:guid}")]
        public ActionResult<Reservation> Get(Guid id)
        {
            var reservation = _service.Get(id);
            if(reservation is null)
                return NotFound();

            return Ok(reservation);
        }

        [HttpPost]
        public ActionResult Post(Reservation reservation)
        {
            var id = _service.Create(reservation);
            if(id is null)
                return BadRequest();

            return CreatedAtAction(nameof(Get), new {Id = id}, default);
        }

        [HttpPut("{id:guid}")]
        public ActionResult Put(Guid id, Reservation reservation)
        {
            reservation.Id = id;
            
            var succeeded = _service.Update(reservation);
            if(!succeeded)
                return BadRequest();

            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public ActionResult Delete(Guid id)
        {
            var succeeded = _service.Delete(id);
            if(!succeeded)
                return BadRequest();

            return NoContent();
        }
    }
}