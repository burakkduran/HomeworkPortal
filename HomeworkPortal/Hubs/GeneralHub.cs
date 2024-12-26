using Microsoft.AspNetCore.SignalR;
using HomeworkPortal.Repositories;

namespace HomeworkPortal.Hubs
{
    public class GeneralHub : Hub
    {
        private readonly AssignmentRepository _assignmentRepository;
        private readonly LessonRepository _lessonRepository;
        private readonly GradeRepository _gradeRepository;

        public GeneralHub(
            AssignmentRepository assignmentRepository,
            LessonRepository lessonRepository,
            GradeRepository gradeRepository)
        {
            _assignmentRepository = assignmentRepository;
            _lessonRepository = lessonRepository;
            _gradeRepository = gradeRepository;
        }

        public int GetAssignmentCount()
        {
            return _assignmentRepository.Where(x => x.IsActive).Count();
        }

        public int GetLessonCount()
        {
            return _lessonRepository.Where(x => x.IsActive).Count();
        }

        public int GetGradeCount()
        {
            return _gradeRepository.Where(x => x.IsActive).Count();
        }
    }
}