using AdvertApi.Models;
using AdvertApi.Repository.Interfaces;
using AdvertApi.Repository.Response;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAdvert.Advert.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]/v1")]
    public class AdvertController : ControllerBase
    {
        private readonly IAdvertStorageRepository _repository;

        public AdvertController(IAdvertStorageRepository repository)
        {
            this._repository = repository;
        }

        [HttpPost("[action]")]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [ProducesResponseType(201, Type = typeof(CreateAdvertResponse))]
        public async Task<IActionResult> Create(AdvertModel model)
        {
            string recordId;
            try
            {
                recordId = await _repository.Add(model);
            }
            catch (KeyNotFoundException)
            {
                return new NotFoundResult();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
            return StatusCode(201, new CreateAdvertResponse { ID = recordId });
        }


        [HttpPut("[action]")]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Confirm(ConfirmAdvertModel model)
        {
            try
            {
                await _repository.Confirm(model);
            }
            catch (KeyNotFoundException)
            {
                return new NotFoundResult();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
            return StatusCode(200);
        }


        [HttpGet("[action]")]
        public async Task<IActionResult> Get()
        {
            bool result = false;
            try
            {
                 result = await _repository.CheckHealthAsync();
            }
            catch (Exception e)
            {
              return  StatusCode(500, e.Message);
            }
            return StatusCode(200, result ? "Healthy" : "Unhealthy");
        }
    }
}
