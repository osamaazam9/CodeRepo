CREATE TABLE [data].[SurveyAnswer] (
    [surveyAnswerId] UNIQUEIDENTIFIER NOT NULL,
    [referralId]     INT              NOT NULL,
    [questionTypeId] INT              NOT NULL,
    [answer]         INT              NULL,
    [created]        DATETIME         CONSTRAINT [DF_SurveyAnswer_created] DEFAULT (getdate()) NOT NULL,
    [modified]       DATETIME         CONSTRAINT [DF_SurveyAnswer_modified] DEFAULT (getdate()) NOT NULL,
    [createdBy]      INT              NULL,
    [modifiedBy]     INT              NULL,
    [isActive]       BIT              CONSTRAINT [DF_SurveyAnswer_isActive] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_SurveyAnswer] PRIMARY KEY CLUSTERED ([surveyAnswerId] ASC),
    CONSTRAINT [FK_SurveyAnswers_Questions] FOREIGN KEY ([questionTypeId]) REFERENCES [app].[QuestionType] ([questionTypeId]),
    CONSTRAINT [FK_SurveyAnswers_Referrals] FOREIGN KEY ([referralId]) REFERENCES [data].[Referral] ([referralId]) ON DELETE CASCADE ON UPDATE CASCADE
);





