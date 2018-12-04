USE [SatisfactionInfo]
GO
/****** Object:  Table [dbo].[UserQuestionnarieAnswers]    Script Date: 04.12.2018 23:49:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserQuestionnarieAnswers](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](5) NULL,
	[UserQuestionnarieID] [int] NULL,
	[QuestionNomber] [int] NULL,
	[Question] [nvarchar](250) NULL,
	[AvailableAnswers] [nvarchar](max) NULL,
	[Answered] [nvarchar](max) NULL,
	[AnswerType] [nvarchar](20) NULL,
	[AddWhyName] [nvarchar](250) NULL,
	[AddWhy] [bit] NULL,
	[AddWhyBody] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserQuestionnaries]    Script Date: 04.12.2018 23:49:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserQuestionnaries](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Date] [datetime] NULL,
	[Code] [nvarchar](5) NULL,
 CONSTRAINT [PK_UserAnswers] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[VAnsweredQuestionnaries]    Script Date: 04.12.2018 23:49:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[VAnsweredQuestionnaries]
AS
SELECT        dbo.UserQuestionnaries.Name, dbo.UserQuestionnaries.Date, dbo.UserQuestionnaries.Code, dbo.UserQuestionnaries.ID, dbo.UserQuestionnarieAnswers.QuestionNomber, dbo.UserQuestionnarieAnswers.Question, 
                         dbo.UserQuestionnarieAnswers.AvailableAnswers, dbo.UserQuestionnarieAnswers.Answered, dbo.UserQuestionnarieAnswers.AnswerType, dbo.UserQuestionnarieAnswers.AddWhyName, 
                         dbo.UserQuestionnarieAnswers.AddWhy, dbo.UserQuestionnarieAnswers.AddWhyBody
FROM            dbo.UserQuestionnarieAnswers INNER JOIN
                         dbo.UserQuestionnaries ON dbo.UserQuestionnarieAnswers.UserQuestionnarieID = dbo.UserQuestionnaries.ID
GO
/****** Object:  Table [dbo].[Answers]    Script Date: 04.12.2018 23:49:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Answers](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Answer] [nvarchar](250) NOT NULL,
	[Weight] [decimal](18, 2) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[QuestionsAnswer]    Script Date: 04.12.2018 23:49:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QuestionsAnswer](
	[QuestionID] [int] NOT NULL,
	[AnswerID] [int] NOT NULL,
 CONSTRAINT [PK_QuestionAnswer] PRIMARY KEY CLUSTERED 
(
	[AnswerID] ASC,
	[QuestionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Questionnaries]    Script Date: 04.12.2018 23:49:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Questionnaries](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Code] [nvarchar](5) NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_Questionaries] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[QuestionnariesQuestion]    Script Date: 04.12.2018 23:49:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QuestionnariesQuestion](
	[QuestionnarieID] [int] NOT NULL,
	[QuestionID] [int] NOT NULL,
 CONSTRAINT [PK_QuestionnariesQuestion] PRIMARY KEY CLUSTERED 
(
	[QuestionID] ASC,
	[QuestionnarieID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Questions]    Script Date: 04.12.2018 23:49:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Questions](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Question] [nvarchar](250) NOT NULL,
	[AnswerType] [nvarchar](20) NULL,
	[AddWhyName] [nvarchar](250) NULL,
	[AddWhy] [bit] NULL,
 CONSTRAINT [PK_Questions] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[VUserQuestionnarie]    Script Date: 04.12.2018 23:49:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO







CREATE VIEW [dbo].[VUserQuestionnarie]
AS

SELECT ISNULL(CONVERT(int, row_number() OVER(order by dbo.Questionnaries.Name)),0) as ID,       
dbo.Questionnaries.Name, 
dbo.Questions.Question, 
dbo.Questions.AddWhy,
dbo.Answers.Answer, 
dbo.Answers.Weight, 
dbo.Questions.AnswerType, 
dbo.Questionnaries.Code
FROM     dbo.Questionnaries INNER JOIN
         dbo.QuestionnariesQuestion ON dbo.Questionnaries.ID = dbo.QuestionnariesQuestion.QuestionnarieID 
		 left JOIN dbo.Questions ON dbo.QuestionnariesQuestion.QuestionID = dbo.Questions.ID 
		 left JOIN dbo.QuestionsAnswer ON dbo.Questions.ID = dbo.QuestionsAnswer.QuestionID 
		 left JOIN dbo.Answers ON dbo.QuestionsAnswer.AnswerID = dbo.Answers.ID
						 
GO
/****** Object:  Table [dbo].[AnswerTypes]    Script Date: 04.12.2018 23:49:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AnswerTypes](
	[AnswerType] [nvarchar](20) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[AnswerType] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 04.12.2018 23:49:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 04.12.2018 23:49:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](450) NOT NULL,
	[UserName] [nvarchar](256) NULL,
	[NormalizedUserName] [nvarchar](256) NULL,
	[Email] [nvarchar](256) NULL,
	[NormalizedEmail] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEnd] [datetimeoffset](7) NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
 CONSTRAINT [PK_AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[QuestionnariesQuestion]  WITH CHECK ADD  CONSTRAINT [FK_QuestionID] FOREIGN KEY([QuestionID])
REFERENCES [dbo].[Questions] ([ID])
GO
ALTER TABLE [dbo].[QuestionnariesQuestion] CHECK CONSTRAINT [FK_QuestionID]
GO
ALTER TABLE [dbo].[QuestionnariesQuestion]  WITH CHECK ADD  CONSTRAINT [FK_QuestionnarieID] FOREIGN KEY([QuestionnarieID])
REFERENCES [dbo].[Questionnaries] ([ID])
GO
ALTER TABLE [dbo].[QuestionnariesQuestion] CHECK CONSTRAINT [FK_QuestionnarieID]
GO
ALTER TABLE [dbo].[Questions]  WITH CHECK ADD  CONSTRAINT [FK_Questions_AnswerTypes] FOREIGN KEY([AnswerType])
REFERENCES [dbo].[AnswerTypes] ([AnswerType])
GO
ALTER TABLE [dbo].[Questions] CHECK CONSTRAINT [FK_Questions_AnswerTypes]
GO
ALTER TABLE [dbo].[QuestionsAnswer]  WITH CHECK ADD  CONSTRAINT [FK_AnswerIDQuestion] FOREIGN KEY([QuestionID])
REFERENCES [dbo].[Questions] ([ID])
GO
ALTER TABLE [dbo].[QuestionsAnswer] CHECK CONSTRAINT [FK_AnswerIDQuestion]
GO
ALTER TABLE [dbo].[QuestionsAnswer]  WITH CHECK ADD  CONSTRAINT [FK_QuestionsIDAnswer] FOREIGN KEY([AnswerID])
REFERENCES [dbo].[Answers] ([ID])
GO
ALTER TABLE [dbo].[QuestionsAnswer] CHECK CONSTRAINT [FK_QuestionsIDAnswer]
GO
ALTER TABLE [dbo].[UserQuestionnarieAnswers]  WITH CHECK ADD  CONSTRAINT [FK_UserQuestionnarieAnswers_UserQuestionnaries] FOREIGN KEY([UserQuestionnarieID])
REFERENCES [dbo].[UserQuestionnaries] ([ID])
GO
ALTER TABLE [dbo].[UserQuestionnarieAnswers] CHECK CONSTRAINT [FK_UserQuestionnarieAnswers_UserQuestionnaries]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[48] 4[14] 2[14] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "UserQuestionnarieAnswers"
            Begin Extent = 
               Top = 13
               Left = 268
               Bottom = 235
               Right = 465
            End
            DisplayFlags = 280
            TopColumn = 2
         End
         Begin Table = "UserQuestionnaries"
            Begin Extent = 
               Top = 7
               Left = 30
               Bottom = 137
               Right = 200
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 13
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'VAnsweredQuestionnaries'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'VAnsweredQuestionnaries'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[46] 4[17] 2[3] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = -96
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Questionaries"
            Begin Extent = 
               Top = 6
               Left = 8
               Bottom = 102
               Right = 178
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "QuestionariesQuestion"
            Begin Extent = 
               Top = 117
               Left = 222
               Bottom = 213
               Right = 392
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Questions"
            Begin Extent = 
               Top = 4
               Left = 412
               Bottom = 169
               Right = 582
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "QuestionsAnswer"
            Begin Extent = 
               Top = 23
               Left = 624
               Bottom = 119
               Right = 794
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Answers"
            Begin Extent = 
               Top = 185
               Left = 410
               Bottom = 330
               Right = 575
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 2010
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'VUserQuestionnarie'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'VUserQuestionnarie'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'VUserQuestionnarie'
GO
