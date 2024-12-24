using HomeworkPortal.Models;
using HomeworkPortal.ViewModels;



namespace HomeworkPortal.Repositories
{
    public class LessonRepository
    {
        private readonly AppDbContext _context;

        public LessonRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<LessonModel> GetList()
        {
            var lessons = _context.Lessons.Select(x => new LessonModel()
            {
                Id = x.Id,
                Name = x.Name,
                IsActive = x.IsActive
            }).ToList();
            return lessons;
        }

        public LessonModel GetById(int id)
        {
            var lesson = _context.Lessons.Where(s => s.Id == id).Select(x => new LessonModel()
            {
                Id = x.Id,
                Name = x.Name,
                IsActive = x.IsActive
            }).FirstOrDefault(); return lesson;
        }

        public void Add(LessonModel model)
        {
            var lesson = new Lesson()
            {
                Name = model.Name,
                IsActive = model.IsActive,
            };
            _context.Lessons.Add(lesson); _context.SaveChanges();
        }

        public void Update(LessonModel model)
        {
            var lesson  = _context.Lessons.Where(s => s.Id == model.Id).FirstOrDefault();
            if (lesson != null)
            {
                lesson.Id = model.Id;
                lesson.Name = model.Name;
                lesson.IsActive = model.IsActive;

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
