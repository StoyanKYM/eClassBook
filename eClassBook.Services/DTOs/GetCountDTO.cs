using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eClassBook.Services.DTOs
{
    public class GetCountDTO
    {
        public int SubjectCount { get; set; }
        public int StudentCount { get; set; }
        public int SchoolCount { get; set; }
    }
}
