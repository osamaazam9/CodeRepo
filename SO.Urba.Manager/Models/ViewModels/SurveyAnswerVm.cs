using SO.Urba.Managers;
using SO.Urba.Models.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SO.Urba.Models.ViewModels
{
    public class SurveyAnswerVm
    {
        private SurveyAnswerManager surveyAnswerManager = new SurveyAnswerManager();
        private QuestionTypeManager questionTypeManager = new QuestionTypeManager();

        public List<SurveyAnswerVo> voSlist { get; set; }
        public string comment { get; set; }
        public int referralID { get; set; }

        public SurveyAnswerVm()
        {
            voSlist = new List<SurveyAnswerVo>();
        }
        public SurveyAnswerVm(List<SurveyAnswerVo> list,string _comment= "")
        {
            comment = _comment;
            voSlist = list;
            if(list.Count > 0)
            referralID = voSlist[0].referralId;
        }
        public SurveyAnswerVm(int referralId)
        {
            voSlist = new List<SurveyAnswerVo>();
            List<QuestionTypeVo> questions = questionTypeManager.getAll(true);

            foreach (var q in questions)
            {
                voSlist.Add(new SurveyAnswerVo(q.questionTypeId, referralId));
            }
            referralID = referralId; 
        }

    }
}
