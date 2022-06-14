using eClassBook.Data;
using eClassBook.Services.DTOs;
using eClassBook.ViewModels.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eClassBook.Services.HomeServices
{
    public class GetCountService : IGetCountService
    {
        private readonly eClassBookDbContext _dbContext;

        public GetCountService(eClassBookDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public GetCountDTO GetCount()
        {
            var viewModel = new GetCountDTO() 
            { 
                SubjectCount = this._dbContext.Subjects.Count(),
                SchoolCount = this._dbContext.Schools.Count(),
                StudentCount = this._dbContext.SchoolUsers.Count(),
            };

            return viewModel;

        }
    }
}
