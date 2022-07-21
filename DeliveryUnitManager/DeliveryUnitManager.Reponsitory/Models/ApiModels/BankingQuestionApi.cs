using DeliveryUnitManager.Reponsitory.Models.BankingQuestionInterview;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryUnitManager.Reponsitory.Models.ApiModels
{
    public class BankingQuestionApi
    {
        public long Id { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public string PlatformType { get; set; }
        public string Level { get; set; }
        public string Projects { get; set; }

        public BankingQuestionApi(QuestionInterviews question)
        {
            Id=question.ID;
            Question = question.Question;
            Answer = question.Answer;
            PlatformType    = question.PlatformType;
            Level = question.Level;
            Projects = question.Projects;
        }
      
    }
}
