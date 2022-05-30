using eClassBook.Models;
using eClassBook.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eClassBook.Services.StatisticsService
{
    public interface IStatisticsService
    {
        /*Statistics for specific school*/
        double SchoolAverageScore(string schoolId);

        ICollection<StatisticInfoDTO> AverageSubjectScores(string schoolId);

        ICollection<StatisticInfoDTO> AverageTeacherScores(string schoolId);

        ICollection<StatisticInfoDTO> SchoolAbsences(string schoolId);

        /*Statistics for all schools in DB*/
        double SchoolAverageScore();

        IDictionary<string, double> AverageSubjectScores();

        IDictionary<string, double> AverageTeacherScores();

        IDictionary<string, ICollection<StatisticInfoDTO>> SchoolAbsences();

        /*Statistics for a single user*/

        double StudentAverageScore(string studentId);

        IDictionary<string, int> StudentAbsences(string studentId);

        double TeacherAverageScore(string teacherId);
    }
}
