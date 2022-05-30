
namespace eClassBook.ViewModels.Class
{
    public class ClassViewModel
    {
        public string Id { get; set; }

        public int StartYear { get; set; }

        public int Grade { get; set; }

        public char GradeLetter { get; set; }

        public eClassBook.Models.SchoolUserEntities.Teacher ClassTeacher { get; set; }
    }
}
