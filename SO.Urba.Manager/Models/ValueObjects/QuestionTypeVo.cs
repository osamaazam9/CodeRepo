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

namespace SO.Urba.Models.ValueObjects
{
    [Table("QuestionType", Schema = "app" )]
    [Serializable]
    public  class QuestionTypeVo
    {
    
          
    	[DisplayName("question Type Id")]
    	[Required]
    	[Key]
        public int questionTypeId { get; set; }
    
    	[DisplayName("question Text")]
        public string questionText { get; set; }
    
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
    
    
        [Association("QuestionType_SurveyAnswer", "questionTypeId", "questionTypeId")]
        public List<SurveyAnswerVo> surveyAnswerses { get; set; }
       
      public QuestionTypeVo()
            {
    				this.isActive = true;
            }
    
    }
    
}
