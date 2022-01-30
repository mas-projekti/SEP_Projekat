using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Infrastructure;
using WebShop.Service.Contract.Dto;
using WebShop.Service.Contract.Services;

namespace WebShop.Service.Services
{
    public class CourseService :ICourseService
    {
        private readonly WebShopDbContext _dbContext;
        private readonly IMapper _mapper;

        public CourseService(WebShopDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public CourseDto GetCourse(int id)
        {
            return _mapper.Map<CourseDto>(_dbContext.Courses.Find(id));
        }

        public List<CourseDto> GetCourses()
        {
            return _mapper.Map<List<CourseDto>>(_dbContext.Courses.ToList());
        }
    }
}
