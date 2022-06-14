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


        // TODO: Finish Services

        [HttpGet("GetSchoolScore/{schoolId}")]
        public double GetSchoolScore([FromRoute] string schoolId)
        {
            return statisticsService.SchoolAverageScore(schoolId);
        }

        [HttpGet("AverageSubjectScoresBySchool/{schoolId}")]
        public ICollection<StatisticInfoDTO> AverageSubjectScores([FromRoute] string schoolId)
        {
            return statisticsService.AverageSubjectScores(schoolId);
        }

        [HttpGet("AverageTeacherScoresBySchool/{schoolId}")]
        public ICollection<StatisticInfoDTO> AverageTeacherScores([FromRoute] string schoolId)
        {
            return statisticsService.AverageTeacherScores(schoolId);
        }

        [HttpGet("GetSchoolAbsences/{schoolId}")]
        public ICollection<StatisticInfoDTO> SchoolAbsences([FromRoute] string schoolId)
        {
            return statisticsService.SchoolAbsences(schoolId);
        }

        /*All schools statistics (Admin panel)*/
        [HttpGet("GetSchoolScore")]
        public double GetSchoolScore()
        {
            return statisticsService.SchoolAverageScore();
        }

        // fix later
        [HttpGet("GetAverageTeacherScores")]
        public IDictionary<string, double> AverageTeacherScores()
        {
            return statisticsService.AverageTeacherScores();
        }

        // fix later
        [HttpGet("GetAverageSubjectScores")]
        public IDictionary<string, double> AverageSubjectScores()
        {
            return statisticsService.AverageSubjectScores();
        }

        // fix later
        [HttpGet("GetSchoolAbsences")]
        public IDictionary<string, ICollection<StatisticInfoDTO>> SchoolAbsences()
        {
            return statisticsService.SchoolAbsences();
        }
    }
}
