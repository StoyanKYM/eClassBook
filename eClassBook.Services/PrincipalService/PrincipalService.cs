using AutoMapper;
using eClassBook.Data.Repositories;
using eClassBook.Models.SchoolUserEntities;
using eClassBook.Services.AccountServices;
using eClassBook.Services.DTOs.SchoolUserDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace eClassBook.Services.PrincipalService
{
    public class PrincipalService : IPrincipalService
    {
        private readonly IAccountService _accountService;
        private readonly IRepositories repo;
        private readonly IMapper mapper;

        public PrincipalService(
            IAccountService accountService,
            IRepositories repo,
            IMapper mapper)
        {
            _accountService = accountService;
            this.repo = repo;
            this.mapper = mapper;
        }
        public async Task AddPrincipal(PrincipalDTO principalModel)
        {
            var principal = mapper.Map<PrincipalDTO, Principal>(principalModel);
            var school = repo.Schools.GetById(principalModel.SchoolId);

            if (school == null)
            {
                throw new TargetException("School not found");
            }
            principal.School = school;

            var account = await _accountService.RegisterSchoolUser(principal);
            if (account == null)
            {
                return;
            }
            principal.User = account;
            principal.Id = account.Id;
            repo.Principals.Create(principal);
        }
    }
}
