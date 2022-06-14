using eClassBook.Models;
using eClassBook.Models.SchoolUserEntities;

namespace eClassBook.Data.Repositories
{
    public class Repositories : IRepositories
    {
        private readonly eClassBookDbContext _context;
        private readonly IDictionary<Type, object> _repositories;

        public Repositories(eClassBookDbContext context)
        {
            this._context = context;
            this._repositories = new Dictionary<Type, object>();
        }

        private IGeneralRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            var type = typeof(TEntity);

            if (!this._repositories.ContainsKey(type))
            {
                var typeOfRepository = typeof(GeneralRepository<TEntity>);
                var repository = Activator.CreateInstance(typeOfRepository, this._context);

                this._repositories.Add(type, repository);
            }

            return (IGeneralRepository<TEntity>)this._repositories[type];
        }

        public IGeneralRepository<Absence> Absences => this.GetRepository<Absence>();

        public IGeneralRepository<Class> Classes => this.GetRepository<Class>();

        public IGeneralRepository<Grade> Grades => this.GetRepository<Grade>();

        public IGeneralRepository<Parent> Parents => this.GetRepository<Parent>();

        public IGeneralRepository<Principal> Principals => this.GetRepository<Principal>();

        public IGeneralRepository<School> Schools => this.GetRepository<School>();

        public IGeneralRepository<Student> Students => this.GetRepository<Student>();

        public IGeneralRepository<Subject> Subjects => this.GetRepository<Subject>();

        public IGeneralRepository<Teacher> Teachers => this.GetRepository<Teacher>();

        public IGeneralRepository<SchoolAdmin> SchoolAdmins => this.GetRepository<SchoolAdmin>();

        public IGeneralRepository<ApplicationUser> Users => this.GetRepository<ApplicationUser>();

        public IGeneralRepository<SchoolUser> SchoolUsers => this.GetRepository<SchoolUser>();

        public IGeneralRepository<ClassToSubject> ClassToSubject => this.GetRepository<ClassToSubject>();

        public IGeneralRepository<StudentToGrade> StudentsToGrades => this.GetRepository<StudentToGrade>();

        public IGeneralRepository<TeacherToSubject> TeacherToSubject => this.GetRepository<TeacherToSubject>();

        public int SaveChanges()
        {
            return this._context.SaveChanges();
        }
    }
}
