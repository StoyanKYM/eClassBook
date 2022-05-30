using AutoMapper;
using AutoMapper.QueryableExtensions;
using eClassBook.Data.Repositories;
using eClassBook.Models.SchoolUserEntities;
using eClassBook.Services.AccountServices;
using eClassBook.Services.DTOs.SchoolUserDTOs;
using eClassBook.Services.StudentService;
using eClassBook.ViewModels.Parent;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace eClassBook.Services.ParentService
{
    public class ParentService : IParentService
    {
        private IStudentService StudentService;
        private readonly IRepositories repo;
        private readonly IMapper mapper;
        private readonly IAccountService _accountService;

        public ParentService(
            IAccountService accountService,
            IStudentService studentService,
            IRepositories repositories,
            IMapper mapper)
        {
            _accountService = accountService;
            StudentService = studentService;
            this.repo = repositories;
            this.mapper = mapper;
        }

        public IEnumerable<ParentViewModel> GetAllParentsFromSchool(string schoolId)
        {
            var parents = repo.Parents.Query()
                .AsNoTracking()
                .Include(p => p.User)
                .Include(p => p.School)
                .Include(p => p.Children)
                .Where(p => p.School.Id == schoolId)
                .ProjectTo<ParentViewModel>(mapper.ConfigurationProvider)
                .ToList();

            return parents;
        }

        public ParentViewModel GetParentDialogData(string parentId)
        {
            var children = repo.Students.Query()
                .AsNoTracking()
                .Include(o => o.User)
                .Include(o => o.Parent)
                .Where(s => s.Parent.Id == parentId)
                .ToList();

            var parent = repo.Parents.Query()
                .AsNoTracking()
                .Include(p => p.Children)
                .Include(p => p.User)
                .ProjectTo<ParentViewModel>(mapper.ConfigurationProvider)
                .FirstOrDefault(p => p.SchoolUserId == parentId);

            foreach (var ch in children)
            {
                parent.ChildrenData.Add(StudentService.GetStudentDialogData(ch.Id));
            }

            return parent;
        }

        public async Task AddParent(ParentDTO parentModel)
        {
            var parent = mapper.Map<ParentDTO, Parent>(parentModel);
            var school = repo.Schools.GetById(parentModel.SchoolId);
            if (school == null)
            {
                throw new TargetException("School not found");
            }
            parent.School = school;

            foreach (var childId in parentModel.ChildrenId)
            {
                var child = repo.Students.GetById(childId);
                if (child == null)
                {
                    throw new TargetException("Student not found");
                }
                parent.Children.Add(child);
            }

            var account = await _accountService.RegisterSchoolUser(parent);
            if (account == null)
            {
                return;
            }
            parent.User = account;
            parent.Id = account.Id;
            repo.Parents.Create(parent);
            repo.SaveChanges();
        }

    }
}
