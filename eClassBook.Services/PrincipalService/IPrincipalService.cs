using eClassBook.Services.DTOs.SchoolUserDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eClassBook.Services.PrincipalService
{
    public interface IPrincipalService
    {
        Task AddPrincipal(PrincipalDTO principal);
    }
}
