using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SO.Utility.Classes;
using SO.Urba.Models.ValueObjects;

namespace SO.Urba.Manager.Models.ViewModels
{
    public class ReferralRecordVm
    {
        public int contactInfoId { get; set; } //

        public int companyCategoryTypeId { get; set; }

        public int clientId { get; set; }
        // list of services
        public List<CompanyCategoryTypeVo> companyCategoryType { get; set; } // 

        //public string keyword { get; set; }
        //public string submitButton { get; set; }
        //public Paging paging;

        //[DisplayName("Is Active: ")]
        //public bool? filterIsActive { get; set; }


        public ReferralRecordVm()
        {
            this.companyCategoryType = new List<CompanyCategoryTypeVo>();

            //if (paging == null)
            //    paging = new Paging();
        }
    }

    public enum ProviderReferralHistoryFilter
    {
        All,
        Accepted,
    }

    /// aaaaaaaaaaaa
    //  a row in Referral History
    public class ProviderReferralHistoryInfo
    {
        public ProviderReferralHistoryInfo()
        {
            referralDate = new DateTime();
            finishDate = new DateTime();
        }
        public string type { get; set; }
        public string name { get; set; }
        public DateTime referralDate { get; set; }
        public decimal quote { get; set; }
        public bool accepted { get; set; }
        public decimal finalCharge { get; set; }
        public DateTime finishDate { get; set; }
    }

    public class CompanyCategoryClass
    {
        #region -- properties --

        private string _name;
        public string name
        {
            get { return _name; }
        }

        private int _companyCategoryTypeId;
        public int companyCategoryTypeId
        {
            get { return _companyCategoryTypeId; }
        }

        private int _companyCategoryCount;
        public int companyCategoryCount
        {
            get { return _companyCategoryCount; }
        }


        #endregion

        #region -- constructors --

        public CompanyCategoryClass()
        {
            FillupData(
                0,
                "",
                0
            );
        }
        public CompanyCategoryClass(string inName)
        {
            FillupData(
                0,
                inName,
                0
            );
        }
        public CompanyCategoryClass(
                int inCompanyCategoryTypeId,
                string inName,
                int inContractorCount
            )
        {
            FillupData(
                inCompanyCategoryTypeId,
                inName,
                inContractorCount
            );
        }

        #endregion

        private void FillupData(
                int inCompanyCategoryTypeId,
                string inName,
                int inCompanyCategoryCount
            )
        {
            _companyCategoryTypeId = inCompanyCategoryTypeId;
            _name = inName;
            _companyCategoryCount = inCompanyCategoryCount;
        }

        public override string ToString()
        {
            return
                _name + ", " +
                _companyCategoryCount + " provider" + ((_companyCategoryCount > 1) ? "s" : "")
                ;
        }

        public string label { get { return ToString(); } }
    }

    /*
    public class ReferralClass
    {
        #region -- properties --

        private int _id;
        public int id
        {
            get { return _id; }
        }

        private int _memberId;
        public int MemberID
        {
            get { return _memberID; }
        }

        private int _contractorID;
        public int ContractorID
        {
            get { return _contractorID; }
        }

        private string _contractorCompanyName;
        public string contractorCompanyName
        {
            get { return _contractorCompanyName; }
        }
//
        private int _companyCategoryId;
        public int companyCategoryId
        {
            get { return _companyCategoryId ; }
        }

        private string _contractorCategoryName;
        public string ContractorCategoryName
        {
            get { return _contractorCategoryName; }
        }

        private string _referralDate;
        public string ReferralDate
        {
            get { return _referralDate; }
            set { _referralDate = value; }
        }

        private string _quote;
        public string Quote
        {
            get { return _quote; }
            set { _quote = value; }
        }

        private string _accepted;
        public string Accepted
        {
            get { return _accepted; }
            set { _accepted = value; }
        }

        private string _finalQuote;
        public string FinalQuote
        {
            get { return _finalQuote; }
            set { _finalQuote = value; }
        }

        private string _finishDate;
        public string FinishDate
        {
            get { return _finishDate; }
            set { _finishDate = value; }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        private string _surveyComment;
        public string SurveyComment
        {
            get { return _surveyComment; }
            set { _surveyComment = value; }
        }

        private Hashtable _questionAnswerPairs;
        public Hashtable QuestionAnswerPairs
        {
            get
            {
                return _questionAnswerPairs;
            }
            set
            {
                _questionAnswerPairs = value;
            }
        }

        /// <summary>
        /// <remarks>Answer a question on this referral. The answers are reserved until ApplySurveyAnswers() is called to apply to database.</remarks>
        /// </summary>
        /// <param name="inQuestionID">Answering a question overwrites the last answer.</param>
        /// <param name="inAnswer"></param>
        public void AnswerQuestion(int inQuestionID, int inAnswer)
        {
            if (_questionAnswerPairs == null)
                _questionAnswerPairs = new Hashtable();

            // add question and answer, or edit existing question
            _questionAnswerPairs[inQuestionID] = inAnswer;
        }

        public int GetAnswer(int inQuestionID)
        {
            if (_questionAnswerPairs == null || _questionAnswerPairs[inQuestionID] == null)
                return 0;
            return (int)_questionAnswerPairs[inQuestionID];
        }


        #endregion

        #region -- constructors --

        public ReferralClass(
            int inMemberID,
            int inContractorID,
            int inContractorCategoryID
            )
        {
            FillupData(
                0,
                inMemberID,
                inContractorID,
                "",
                inContractorCategoryID,
                "",
                "",
                "",
                "",
                "",
                "",
                ""
            );
        }

        public ReferralClass(
            int inID,
            int inMemberID,
            int inContractorID,
            string inContractorCompanyName,
            int inContractorCategoryID,
            string inContractorCategoryName,
            string inReferralDate,
            string inQuote,
            string inAccepted,
            string inFinalQuote,
            string inFinishDate,
            string inDescription
            )
        {
            FillupData(
                inID,
                inMemberID,
                inContractorID,
                inContractorCompanyName,
                inContractorCategoryID,
                inContractorCategoryName,
                inReferralDate,
                inQuote,
                inAccepted,
                inFinalQuote,
                inFinishDate,
                inDescription
            );
        }

        public ReferralClass(
            int inID,
            ReferralClass inReferral
            )
        {
            FillupData(
                inID,
                inReferral.MemberID,
                inReferral.ContractorID,
                inReferral.ContractorCompanyName,
                inReferral.ContractorCategoryID,
                inReferral.ContractorCategoryName,
                inReferral.ReferralDate,
                inReferral.Quote,
                inReferral.Accepted,
                inReferral.FinalQuote,
                inReferral.FinishDate,
                inReferral.Description
            );
        }


        #endregion

        private void FillupData(
                int inID,
                int inMemberID,
                int inContractorID,
                string inContractorCompanyName,
                int inContractorCategoryID,
                string inContractorCategoryName,
                string inReferralDate,
                string inQuote,
                string inAccepted,
                string inFinalQuote,
                string inFinishDate,
                string inDescription
            )
        {
            _id = inID;
            _memberID = inMemberID;
            _contractorID = inContractorID;
            _contractorCompanyName = inContractorCompanyName;
            _contractorCategoryID = inContractorCategoryID;
            _contractorCategoryName = inContractorCategoryName;
            _referralDate = inReferralDate;
            _quote = inQuote;
            _accepted = inAccepted;
            _finalQuote = inFinalQuote;
            _finishDate = inFinishDate;
            _description = inDescription;
        }


        public override string ToString()
        {
            return
                _id.ToString().Trim() + "," +
                _memberID.ToString().Trim() + "," +
                _contractorCompanyName + "," +
                _contractorCategoryName.ToString().Trim() + "," +
                _referralDate
                ;
        }
    }
     * */

}
