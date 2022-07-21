using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryUnitManager.Reponsitory.Models.BankingQuestionInterview
{
    public class AnswerInterviews:BaseModel
    {
        [Key]
        public long ID { get; set; }

        public long QuestionID { get; set; }
        public string? Answer { get; set; }

    }
}
