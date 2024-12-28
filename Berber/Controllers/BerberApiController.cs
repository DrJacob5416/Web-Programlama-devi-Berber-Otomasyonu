using Berber.Models.DatabaseOperations.Operations;
using Berber.Models.ServiceModels;
using Berber.Models.Tables;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Emit;

namespace Berber.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BerberApiController : ControllerBase
    {
        private readonly IYapayZekaOp yapayZekaOp;

        public BerberApiController(IYapayZekaOp yapayZekaOp)
        {
            this.yapayZekaOp = yapayZekaOp;
        }
        [Route("CreateYapayZeka")]
        [HttpPost]
        public IActionResult CreateYapayZeka([FromBody] YapayZekaModel yapayZekaModel)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            yapayZekaOp.Add(
                new YapayZeka()
                {
                    ApplicationUserId = yapayZekaModel.ApplicationUserId,
                    Request = yapayZekaModel.Request,
                    Response = yapayZekaModel.Response
                });
            yapayZekaOp.Save();

            return Ok();
        }
        [HttpGet("AllAnswers/{applicationUserId}")]
        [Route("AllAnswers")]
        public IActionResult AllAnswers(string applicationUserId)
        {
            var yapayZekaModel = yapayZekaOp.GetAllByCondition(x => x.ApplicationUserId == applicationUserId);
            if (yapayZekaModel == null || !yapayZekaModel.Any())
                return BadRequest("Kullanıcıya ait yapay zeka sorgusu bulunamadı");
            return Ok(yapayZekaModel);
        }
    }
}
