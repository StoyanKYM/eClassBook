using AutoMapper;
using eClassBook.Data;
using eClassBook.Data.Repositories;
using eClassBook.Models;
using eClassBook.ViewModels.Class;
using eClassBook.ViewModels.School;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace eClassBook.Services.SchoolServices
{
    public class SchoolService : ISchoolService
    {
        private readonly eClassBookDbContext dbContext;
        private readonly IMapper _mapper;
        private readonly IRepositories repo;

        public SchoolService(eClassBookDbContext dbContext, IRepositories repo, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.repo = repo;
            this._mapper = mapper;
        }
        public void CreateSchool(SchoolInputModel inputModel)
        {
            School school = new School()
            {
                Name = inputModel.Name,
                Address = inputModel.Address,
                Number = inputModel.Number,
                Id = Guid.NewGuid().ToString(),
            };

            dbContext.Schools.Add(school);
            dbContext.SaveChanges();

        }

        public void CreateSchoolSecond(SchoolInputModel inputModel)
        {
            School school = new School()
            {
                Name = inputModel.Name,
                Address = inputModel.Address,
                Number = inputModel.Number,
                Id = Guid.NewGuid().ToString(),
            };

            repo.Schools.Create(school);
            repo.SaveChanges();

        }

        public ICollection<SchoolViewModel> GetAll()
        {
            List<SchoolViewModel> schoolList = new List<SchoolViewModel>();
            var schools = repo.Schools.Query().ToList();

            foreach (var school in schools)
            {
                var currentSchool = new SchoolViewModel
                {
                    Address = school.Address,
                    Name = school.Name,
                    Number = school.Number,
                };

                schoolList.Add(currentSchool);
            }

            return schoolList;

        }

        public void EditSchool(string schoolId, SchoolInputModel inputModel)
        {
            var newData = _mapper.Map<SchoolInputModel, School>(inputModel);
            newData.Id = schoolId;

            repo.Schools.Update(newData);
        }

        public void DeleteSchool(string schoolId)
        {
            var school = repo.Schools.GetById(schoolId);

            if (school is null)
            {
                throw new TargetException("School not found");
            }

            repo.Schools.Delete(school);
            repo.Schools.SaveChanges();
        }

        public SchoolViewModel GetSchoolById(string schoolId)
        {
            var school = repo.Schools.GetById(schoolId);

            if (school is null)
            {
                throw new TargetException("School not found");
            }

            return _mapper.Map<School, SchoolViewModel>(school);
        }
    }
}
