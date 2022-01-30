using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Service.Contract.Dto;

namespace WebShop.Service.Contract.Services
{
    public interface ICourseService
    {
        List<CourseDto> GetCourses();
        CourseDto GetCourse(int id);
    }
}
