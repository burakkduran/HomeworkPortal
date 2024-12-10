using HomeworkPortal.Models;



namespace HomeworkPortal.Repositories
{
    public class LessonRepository
    {
        private readonly AppDbContext _context;

        public LessonRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<Lesson> GetList()
        {
            var lessons = _context.Lessons.ToList();
            return lessons;
        }

        public Lesson GetById(int id)
        {
            var lesson = _context.Lessons.Where(s => s.Id == id).FirstOrDefault();
            return lesson;
        }

        public void Add(Lesson model)
        {
            _context.Lessons.Add(model);
            _context.SaveChanges();
        }

        public void Update(Lesson model)
        {
            var lesson = GetById(model.Id);
            if (lesson != null)
            {
                lesson.Id = model.Id;
                lesson.Name = model.Name;

                _context.Lessons.Update(lesson);
                _context.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            var lesson = GetById(id);
            if (lesson != null)
            {
                _context.Lessons.Remove(lesson);
                _context.SaveChanges();
            }
        }
    }
}
