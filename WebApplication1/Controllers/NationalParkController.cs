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
    [Route("api/v{version:apiVersion}/nationalparks")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    //[ApiExplorerSettings(GroupName = "ParkyOpenAPISpecNP")]
    public class NationalParkController : ControllerBase
    {
        private readonly INationalParkRepository _npRepo;
        private readonly IMapper _mapper;

        public NationalParkController(INationalParkRepository npRepo, IMapper mapper)
        {
            _npRepo = npRepo;
            _mapper = mapper;
        }

        /// <summary>
        /// Get list of national parks.
        /// </summary>
        /// <returns></returns>

        [HttpGet]//xml comment, gotten by adding three forward slash
        [ProducesResponseType(200, Type =typeof(List<NationalParkDto>))]
        public IActionResult GetNationalParks()
        {
            var objList = _npRepo.GetNationalPark();
            var objDto = new List<NationalParkDto>();
            foreach (var obj in objList)
            {
                objDto.Add(_mapper.Map<NationalParkDto>(obj));
                
            }
            return Ok(objDto);
        }

        /// <summary>
        /// Get individual national parks.
        /// </summary>
        /// <param name="nationalParkId"> The Id of the national park</param>
        /// <returns></returns>

        [HttpGet("{nationalParkId:int}", Name = "GetNationalPark")]
        [ProducesResponseType(200, Type = typeof(NationalParkDto))]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
        public IActionResult GetNationalPark(int nationalParkId)
        {
            var obj = _npRepo.GetNationalPark(nationalParkId);
            if(obj == null)
            {
                return NotFound();
            }
            var objDto = _mapper.Map<NationalParkDto>(obj);
            //below in the region is the manual mapper that would have being used instead
            //which is converting a national park into a national park dto
            #region Manual Mapper
            /*var objDto = new NationalParkDto()
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
        [ProducesResponseType(201, Type = typeof(NationalParkDto))]//201 return because that is the one that returns created at.
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
       // [ProducesDefaultResponseType]
        public IActionResult CreateNationalPark([FromBody] NationalParkDto nationalParkDto)
        {
            if (nationalParkDto == null)
            {
                return BadRequest(ModelState);
            }

            if (_npRepo.NationalParkExist(nationalParkDto.Name))
            {
                ModelState.AddModelError("", "The email already exists!");
                return StatusCode(404, ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //convert the dto to a domain model
            var obj = _mapper.Map<NationalPark>(nationalParkDto);
            if (!_npRepo.CreateNationalPark(obj))
            {
                ModelState.AddModelError("", $"Something went wrong when saving the record{obj.Name}");
                return StatusCode(500, ModelState);
            }
            //This returns the value as httpGet  and instead of just returning 200 Ok, it returns 201 Created
            // We are calling the getnationalpark and we are passing the ID
            return CreatedAtRoute("GetNationalPark",new { version =HttpContext.GetRequestedApiVersion().ToString(),nationalParkId = obj.Id },obj);
        }

        [HttpPatch("{nationalParkId:int}", Name = "UpdateNationalPark")]
        //the Id and the object to be updated
        [ProducesResponseType(204)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public IActionResult UpdateNationalPark(int nationalParkId, [FromBody] NationalParkDto nationalParkDto)
        {
            if (nationalParkDto == null || nationalParkId != nationalParkDto.Id)
            {
                return BadRequest(ModelState);
            }
            var obj = _mapper.Map<NationalPark>(nationalParkDto);
            if (!_npRepo.UpdateeNationalPark(obj))
            {
                ModelState.AddModelError("", $"Something went wrong when updating the record{obj.Name}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpDelete("{nationalParkId:int}", Name = "DeleteNationalPark")]
        //the Id and the object to be updated
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteNationalPark(int nationalParkId)
        {
            if (!_npRepo.NationalParkExists(nationalParkId))
            {
                return NotFound();
            }
            var obj = _npRepo.GetNationalPark(nationalParkId);
            if (!_npRepo.UpdateeNationalPark(obj))
            {
                ModelState.AddModelError("", $"Something went wrong when deleting the record{obj.Name}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}
