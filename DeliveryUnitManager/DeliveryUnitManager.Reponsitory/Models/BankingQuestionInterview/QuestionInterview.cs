using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryUnitManager.Reponsitory.Models.BankingQuestionInterview
{
    public class QuestionInterviews:BaseModel
    {
        [Key]
        public long ID { get; set; }
        public string Question { get; set; }
        public  string Answer { get; set; }
        public string? PlatformType { get; set; }// Web, Mobile, Desktop, DB, 

        public string Level { get; set; }
        public string Projects { get; set; }

    }
}
