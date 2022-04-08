﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoGeneratedSystem.Common;
using AutoGeneratedSystem.Common.CustomAttributes;
using AutoMapper;
using AutoGeneratedSystem.Models.Company;
using AutoGeneratedSystem.Services;
using AutoGeneratedSystem.DataAccess.Models;

namespace AutoGeneratedSystem.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ControllerOfEntity(Constants.EntityNames.Company, typeof(long))]
    public partial class CompanyController : ControllerBase
    {
    }
}
