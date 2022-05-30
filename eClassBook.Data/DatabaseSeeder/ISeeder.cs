using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eClassBook.Data.DatabaseSeeder
{
    public interface ISeeder
    {
        public Task InitializeSeed();
    }
}
