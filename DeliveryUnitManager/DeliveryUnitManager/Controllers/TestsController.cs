﻿using DeliveryUnitManager.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryUnitManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestsController : ControllerBase
    {
        public TestsController()
        {

        }

        [HttpGet("get")]
        public string GetTestApi()
        {
            return "call api get test success";
        }

        [HttpGet("getAuthorize")]
        [CustomAuthorize(Role ="test")]
        public string GetAuthorizeTestApi()
        {
            return "call api getAuthorize test success";
        }
    }  
}
