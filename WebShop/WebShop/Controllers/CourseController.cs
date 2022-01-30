using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShop.Service.Contract.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebShop.Controllers
{
    [Route("api/courses")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _courseService;

        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [HttpGet("all")]
        public IActionResult GetCourses()
        {
            return Ok(_courseService.GetCourses());
        }

        [HttpGet("{id}")]
        public IActionResult GetCourse(int id)
        {
            return Ok(_courseService.GetCourse(id));
        }

    }
}
