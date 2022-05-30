using AutoMapper;
using eClassBook.Data.Repositories;
using eClassBook.Models;
using eClassBook.Models.SchoolUserEntities;
using eClassBook.ViewModels.User;
using System.Reflection;

namespace eClassBook.Services.SchoolUserService
{
    public class SchoolUserService : ISchoolUserService
    {
        private readonly IRepositories _repositories;
        private readonly IMapper _mapper;

        public SchoolUserService(IRepositories repositories, IMapper mapper)
        {
            _repositories = repositories;
            _mapper = mapper;
        }

        public UserViewModel GetSchoolUserById(string id)
        {
            var user = _repositories.SchoolUsers.Query()
                .SingleOrDefault(x => x.Id == id);
            return _mapper.Map<SchoolUser, UserViewModel>(user);
        }

        public IEnumerable<UserViewModel> GetAllSchoolUsers()
        {
            var users = _repositories.SchoolUsers.Query();
            return _mapper
                .Map<IEnumerable<SchoolUser>, IEnumerable<UserViewModel>
                >(users);
        }

        /*========================= Private methods =========================*/
        
        public School GetSchool(string schoolId)
        {
            var school =
                _repositories.Schools.GetById(schoolId);
            if (school == null)
            {
                throw new TargetException("School Id is invalid");
            }

            return school;
        }
    }
}
