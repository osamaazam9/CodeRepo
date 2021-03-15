using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using SO.Urba.Managers;

namespace SO.Urba.Models.ValueObjects
{
    
    [Table("SurveyAnswer", Schema = "data" )]
    [Serializable]
    public  class SurveyAnswerVo
    {
        private QuestionTypeManager questionTypeManager = new QuestionTypeManager();
          
    	[DisplayName("survey Answer Id")]
    	[Required]
    	[Key]
        public Guid surveyAnswerId { get; set; }
    
    	[DisplayName("referral Id")]
    	[Required]
        public int referralId { get; set; }
    
    	[DisplayName("question Id")]
    	[Required]
        public int questionTypeId { get; set; }
    
    	[DisplayName("answer")]
        public Nullable<int> answer { get; set; }
    
    	[DisplayName("created")]
    	[Required]
        public DateTime created { get; set; }
    
    	[DisplayName("modified")]
    	[Required]
        public DateTime modified { get; set; }
    
    	[DisplayName("created By")]
        public Nullable<int> createdBy { get; set; }
    
    	[DisplayName("modified By")]
        public Nullable<int> modifiedBy { get; set; }
    
    	[DisplayName("is Active")]
    	[Required]
        public bool isActive { get; set; }
    
    
        [ForeignKey("questionTypeId")]
        public QuestionTypeVo questionType { get; set; }
       
        [ForeignKey("referralId")]
        public ReferralVo referral { get; set; }

        public SurveyAnswerVo()
        {
            this.surveyAnswerId = Guid.NewGuid();
            this.isActive = true;
        }

        public SurveyAnswerVo(int questionId, int? refId = null )
        {
            this.questionTypeId = questionId;
            this.questionType = questionTypeManager.get(questionId);
            this.referralId = refId?? 0;
            this.surveyAnswerId = Guid.NewGuid();
            this.isActive = true;
        }
   
    }
    
}
