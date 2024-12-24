using AutoMapper;
using HomeworkPortal.Models;
using HomeworkPortal.ViewModels;

namespace HomeworkPortal.Mapping
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<Lesson, LessonModel>().ReverseMap();
            CreateMap<Category, CategoryModel>().ReverseMap();
        }
    }
}
