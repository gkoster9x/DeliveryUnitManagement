using DeliveryUnitManager.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryUnitManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [CustomAuthorize]
    public class LockedController : ControllerBase
    {
        public LockedController()
        {

        }

        [HttpGet("get")]
        public string GetTestApi()
        {
            return "call api get test success";
        }
        [HttpGet("unlocked")]
        [AllowAnonymous]
        public string getUnlocked()
        {
            return "call api get test success";
        }
    }
}
