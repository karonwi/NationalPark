using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParkyAPI.Models;
using ParkyAPI.Models.Dtos;
using ParkyAPI.Repository.IRepository;
using System.Collections.Generic;

namespace ParkyAPI.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/v{version:apiVersion}/trails")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    //[ApiExplorerSettings(GroupName = "ParkyOpenAPISpecTrails")]
    public class TrailController : ControllerBase
    {
        private readonly ITrailRepository _trailRepo;
        private readonly IMapper _mapper;

        public TrailController(ITrailRepository trailRepo, IMapper mapper)
        {
            _trailRepo = trailRepo;
            _mapper = mapper;
        }

        /// <summary>
        /// Get list of trails.
        /// </summary>
        /// <returns></returns>

        [HttpGet]//xml comment, gotten by adding three forward slash
        [ProducesResponseType(200, Type =typeof(List<TrailDto>))]
        public IActionResult GetTrails()
        {
            var objList = _trailRepo.GetTrail();
            var objDto = new List<TrailDto>();
            foreach (var obj in objList)
            {
                objDto.Add(_mapper.Map<TrailDto>(obj));
                
            }
            return Ok(objDto);
        }

        /// <summary>
        /// Get individual trails.
        /// </summary>
        /// <param name="nationalParkId"> The Id of the national park</param>
        /// <returns></returns>

        [HttpGet("GetTrailInNationalPark/{nationalParkId:int}")]
        [ProducesResponseType(200, Type = typeof(TrailDto))]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
        public IActionResult GetTrailInNationalPark(int nationalParkId)
        {
            var objList = _trailRepo.GetTrailsInNationalPark(nationalParkId);
            if(objList == null)
            {
                return NotFound();
            }

            var objDto = new List<TrailDto>();
            foreach (var obj in objDto)
            {
                objDto.Add(_mapper.Map<TrailDto>(obj));
                
            }
            //below in the region is the manual mapper that would have being used instead
            //which is converting a national park into a national park dto
            #region Manual Mapper
            /*var objDto = new TrailDto()
            {
                Created = obj.Created,
                Id = obj.Id,
                Name = obj.Name,
                State = obj.State,
            };*/
            #endregion
            return Ok(objDto);
        }


        [HttpPost]
        [ProducesResponseType(201, Type = typeof(TrailDto))]//201 return because that is the one that returns created at.
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public IActionResult CreateTrail([FromBody] TrailCreateDto trailDto)
        {
            if (trailDto == null)
            {
                return BadRequest(ModelState);
            }

            if (_trailRepo.TrailExist(trailDto.Name))
            {
                ModelState.AddModelError("", "The email already exists!");
                return StatusCode(404, ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //convert the dto to a domain model
            var obj = _mapper.Map<Trail>(trailDto);
            if (!_trailRepo.CreateTrail(obj))
            {
                ModelState.AddModelError("", $"Something went wrong when saving the record{obj.Name}");
                return StatusCode(500, ModelState);
            }
            //This returns the value as httpGet  and instead of just returning 200 Ok, it returns 201 Created
            // We are calling the getnationalpark and we are passing the ID
            return CreatedAtRoute("GetTrail",new {trailId = obj.Id },obj);
        }

        [HttpPatch("{trailId:int}", Name = "UpdateTrail")]
        //the Id and the object to be updated
        [ProducesResponseType(204)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public IActionResult UpdateTrail(int trailId, [FromBody] TrailUpdateDto trailDto)
        {
            if (trailDto == null || trailId != trailDto.Id)
            {
                return BadRequest(ModelState);
            }
            var obj = _mapper.Map<Trail>(trailDto);
            if (!_trailRepo.UpdateeTrail(obj))
            {
                ModelState.AddModelError("", $"Something went wrong when updating the record{obj.Name}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpDelete("{trailId:int}", Name = "DeleteTrail")]
        //the Id and the object to be updated
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteTrail(int trailId)
        {
            if (!_trailRepo.TrailExists(trailId))
            {
                return NotFound();
            }
            var obj = _trailRepo.GetTrail(trailId);
            if (!_trailRepo.UpdateeTrail(obj))
            {
                ModelState.AddModelError("", $"Something went wrong when deleting the record{obj.Name}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}
