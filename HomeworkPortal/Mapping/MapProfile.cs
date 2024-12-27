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
            CreateMap<StudentSubmission, StudentSubmissionModel>()
                .ForMember(dest => dest.AssignmentName, opt => opt.MapFrom(src => src.Assignment.Name))
                .ForMember(dest => dest.StudentName, opt => opt.MapFrom(src => src.Student.FullName))
                .ReverseMap();
        }
    }
}
