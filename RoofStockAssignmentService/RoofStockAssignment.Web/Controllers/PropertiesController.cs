using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RoofStockAssignent.Domain;
using RoofStockAssignment.BL;

namespace RoofStockAssignment.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertiesController : ControllerBase
    {
        BusinessLogic businessLogic = new BusinessLogic();
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<Property>> Get()
        {
            try
            {
                return Ok(businessLogic.GetSavedProperties());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody] Property property)
        {
            try
            {
                businessLogic.SaveProperty(property);
                return Ok(true);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}
