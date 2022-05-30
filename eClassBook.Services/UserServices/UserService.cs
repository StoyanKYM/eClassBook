using eClassBook.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eClassBook.Services.UserServices
{
    public class UserService : IUserService
    {
        private readonly eClassBookDbContext context;

        public UserService(eClassBookDbContext _context)
        {
            context = _context;
        }
        public IEnumerable<KeyValuePair<string, string>> GetAllKeyValuePairs()
        {
            return this.context.Schools.Select(x => new
            {
                x.Id, 
                x.Name,
            })
                .OrderBy(x => x.Name)
                .ToList()
                .Select(x => new KeyValuePair<string, string>(x.Id.ToString(), x.Name.ToString()));
        }
    }
}
