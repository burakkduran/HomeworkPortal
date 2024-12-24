using AutoMapper;
using HomeworkPortal.Models;
using HomeworkPortal.ViewModels;



namespace HomeworkPortal.Repositories
{
    public class LessonRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;


        public LessonRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<LessonModel> GetList()
        {
            var lessons = _context.Lessons.ToList();
            var lessonModels = _mapper.Map<List<LessonModel>>(lessons);
            return lessonModels;
        }

        public LessonModel GetById(int id)
        {
            var lesson = _context.Lessons.Where(s => s.Id == id).FirstOrDefault();
            var lessonModel = _mapper.Map<LessonModel>(lesson);
            return lessonModel;
        }

        public void Add(LessonModel model)
        {
            var lesson = _mapper.Map<Lesson>(model);
            _context.Lessons.Add(lesson); 
            _context.SaveChanges();
        }

        public void Update(LessonModel model)
        {
            var lesson  = _context.Lessons.Where(s => s.Id == model.Id).FirstOrDefault();
            if (lesson != null)
            {
                lesson.Id = model.Id;
                lesson.Name = model.Name;
                lesson.IsActive = model.IsActive;
                lesson.CategoryId = model.CategoryId;

                _context.Lessons.Update(lesson);
                _context.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            var lesson = _context.Lessons.Where(s => s.Id == id).FirstOrDefault();
            if (lesson != null)
            {
                _context.Lessons.Remove(lesson);
                _context.SaveChanges();
            }
        }
    }
}
