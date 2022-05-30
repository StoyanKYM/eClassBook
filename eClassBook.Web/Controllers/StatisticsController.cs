using eClassBook.Services.DTOs;
using eClassBook.Services.StatisticsService;
using Microsoft.AspNetCore.Mvc;

namespace eClassBook.Web.Controllers
{
    [Route("Statistics/")]
    public class StatisticsController : Controller
    {
        private readonly IStatisticsService statisticsService;

        public StatisticsController(IStatisticsService statisticsService)
        {
            this.statisticsService = statisticsService;
        }

        [HttpGet("GetSchoolScore/{schoolId}")]
        //[Authorize(Roles = "SuperAdmin, SchoolAdmin, Principal")]
        public double GetSchoolScore([FromRoute] string schoolId)
        {
            return statisticsService.SchoolAverageScore(schoolId);
        }

        [HttpGet("AverageSubjectScoresBySchool/{schoolId}")]
        //[Authorize(Roles = "SuperAdmin, SchoolAdmin, Principal")]
        public ICollection<StatisticInfoDTO> AverageSubjectScores([FromRoute] string schoolId)
        {
            return statisticsService.AverageSubjectScores(schoolId);
        }

        [HttpGet("AverageTeacherScoresBySchool/{schoolId}")]
        //[Authorize(Roles = "SuperAdmin, SchoolAdmin, Principal")]
        public ICollection<StatisticInfoDTO> AverageTeacherScores([FromRoute] string schoolId)
        {
            return statisticsService.AverageTeacherScores(schoolId);
        }

        [HttpGet("GetSchoolAbsences/{schoolId}")]
        //[Authorize(Roles = "SuperAdmin, SchoolAdmin, Principal")]
        public ICollection<StatisticInfoDTO> SchoolAbsences([FromRoute] string schoolId)
        {
            return statisticsService.SchoolAbsences(schoolId);
        }

        /*All schools statistics (Admin panel)*/
        [HttpGet("GetSchoolScore")]
        //[Authorize(Roles = "SuperAdmin")]
        public double GetSchoolScore()
        {
            return statisticsService.SchoolAverageScore();
        }

        // fix later?
        [HttpGet("GetAverageTeacherScores")]
        //[Authorize(Roles = "SuperAdmin")]
        public IDictionary<string, double> AverageTeacherScores()
        {
            return statisticsService.AverageTeacherScores();
        }

        // fix later?
        [HttpGet("GetAverageSubjectScores")]
        //[Authorize(Roles = "SuperAdmin")]
        public IDictionary<string, double> AverageSubjectScores()
        {
            return statisticsService.AverageSubjectScores();
        }

        //[HttpGet("best/{n}")]
        //[Authorize(Roles = "SuperAdmin")]
        //public ICollection GetBestSchools(int n)
        //{
        //    return statisticsService.BestNSchools(n);
        //}

        // fix later?
        [HttpGet("GetSchoolAbsences")]
        //[Authorize(Roles = "SuperAdmin")]
        public IDictionary<string, ICollection<StatisticInfoDTO>> SchoolAbsences()
        {
            return statisticsService.SchoolAbsences();
        }
    }
}
