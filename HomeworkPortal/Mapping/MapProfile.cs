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
            CreateMap<Grade, GradeModel>().ReverseMap();
            CreateMap<Assignment, AssignmentModel>().ReverseMap();
            CreateMap<AppUser, UserModel>().ReverseMap();
            CreateMap<AppUser, RegisterModel>().ReverseMap();
        }
    }
}
