using Microsoft.AspNetCore.Mvc;
using MySpot.Core.Entities;
using MySpot.Core.ValueObjects;
using MySpot.Application.Services;
using MySpot.Application.DTO;
using MySpot.Application.Commands;

namespace MySpot.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReservationsController : ControllerBase
    {

        private readonly IReservationsService _reservationsService;

        public ReservationsController(IReservationsService reservationsService)
        {
            _reservationsService = reservationsService;
        }

        [HttpGet]
        public ActionResult<ReservationDto[]> Get()
            => Ok(_reservationsService.GetAllWeekly());

        [HttpGet("{id:guid}")]
        public ActionResult<ReservationDto> Get(Guid id)
        {
            var reservation = _reservationsService.Get(id);
            if(reservation is null)
                return NotFound();

            return Ok(reservation);
        }

        [HttpPost]
        public ActionResult Post(CreateReservation command)
        {
            var id = _reservationsService.Create(command with {ReservationId = Guid.NewGuid()});
            if(id is null)
                return BadRequest();

            return CreatedAtAction(nameof(Get), new {id}, default);
        }

        [HttpPut("{id:guid}")]
        public ActionResult Put(Guid id, ChangeReservationLicensePlate command)
        {
            if(_reservationsService.Update(command with {ReservationId = id}))
                return NoContent();

            return NotFound();
        }

        [HttpDelete("{id:guid}")]
        public ActionResult Delete(Guid id)
        {
            if(_reservationsService.Delete(new DeleteReservation(id)))
                return NoContent();

            return NotFound();
        }
    }
}