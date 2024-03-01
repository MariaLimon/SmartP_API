using Microsoft.AspNetCore.Mvc;
using SmartPlant.Api.Models;
using SmartPlant.Api.Services;
using SmartPlant.Api.Configurations;
using MongoDB.Bson;

namespace SmartPlant.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlantController : ControllerBase
{
    private readonly ILogger<PlantController> _logger;
    private readonly PlantServices _plantServices;

    public PlantController(ILogger<PlantController> logger, PlantServices plantServices)
    {
        _logger = logger;
        _plantServices = plantServices;
    }

    [HttpGet]
    public async Task<IActionResult> GetPlants(){
        var plants = await _plantServices.GetAsync();
        return Ok(plants);
    }

    [HttpPost]
    public async Task<IActionResult> InsertPlant([FromBody] Plant plantToInsert)
    {
        if(plantToInsert == null)
            return BadRequest();
        if(plantToInsert.NamePlant == string.Empty)
            ModelState.AddModelError("Name","La planta no debe estar vacio");
        if(plantToInsert.TypePlant == string.Empty)
            ModelState.AddModelError("Type","La planta no debe estar vacio");
        if(plantToInsert.UserId == ObjectId.Empty)
            ModelState.AddModelError("User","La planta no debe estar vacio");



        await _plantServices.InsertPlant(plantToInsert);

        return Created("Created",true);
    }

    [HttpDelete("ID")]
    public async Task<IActionResult> DeletePlant(string id)
    {
        if(id == null)
            return BadRequest();
        if(id == string.Empty)
            ModelState.AddModelError("Id","No debe dejar el id vacio");

        await _plantServices.DeletePlant(id);

        return Ok();
    }

    [HttpPut("PlantToUpdate")]
    public async Task<IActionResult> UpdatePlant(Plant plant)
    {
        if(plant == null)
            return BadRequest();
        if(plant.Id == string.Empty)
            ModelState.AddModelError("Id","No debe dejar el id vacio");
        if(plant.NamePlant == string.Empty)
            ModelState.AddModelError("Name","No debe dejar el nombre vacio");
        if(plant.TypePlant == string.Empty )
            ModelState.AddModelError("Tipo","No debe dejar el tipo vacio");
        if(plant.UserId == ObjectId.Empty)
            ModelState.AddModelError("User","No debe dejar el user vacio");

        await _plantServices.UpdatePlant(plant);

        return Ok();
    }
}
