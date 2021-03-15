CREATE TABLE [app].[QuestionType] (
    [questionTypeId] INT      IDENTITY (1, 1) NOT NULL,
    [questionText]   TEXT     NULL,
    [created]        DATETIME CONSTRAINT [DF_QuestionType_created] DEFAULT (getdate()) NOT NULL,
    [modified]       DATETIME CONSTRAINT [DF_QuestionType_modified] DEFAULT (getdate()) NOT NULL,
    [createdBy]      INT      NULL,
    [modifiedBy]     INT      NULL,
    [isActive]       BIT      CONSTRAINT [DF_QuestionType_isActive] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_Questions] PRIMARY KEY CLUSTERED ([questionTypeId] ASC)
);

