USE [SatisfactionInfo]
GO
/****** Object:  Table [dbo].[UserQuestionnarieAnswers]    Script Date: 18.12.2018 01:57:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserQuestionnarieAnswers](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](5) NULL,
	[UserQuestionnarieID] [int] NULL,
	[QuestionNumber] [int] NULL,
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
/****** Object:  Table [dbo].[UserQuestionnaries]    Script Date: 18.12.2018 01:57:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserQuestionnaries](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Date] [datetime] NULL,
	[Code] [nvarchar](5) NULL,
	[Description] [nvarchar](1000) NULL,
 CONSTRAINT [PK_UserAnswers] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[VAnsweredQuestionnaries]    Script Date: 18.12.2018 01:57:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW [dbo].[VAnsweredQuestionnaries]
AS
SELECT          dbo.UserQuestionnaries.ID, dbo.UserQuestionnaries.Name, dbo.UserQuestionnaries.Date, dbo.UserQuestionnaries.Code, dbo.UserQuestionnarieAnswers.QuestionNumber, dbo.UserQuestionnarieAnswers.Question, 
                         dbo.UserQuestionnarieAnswers.AvailableAnswers, dbo.UserQuestionnarieAnswers.Answered, dbo.UserQuestionnarieAnswers.AnswerType, dbo.UserQuestionnarieAnswers.AddWhyName, 
                         dbo.UserQuestionnarieAnswers.AddWhy, dbo.UserQuestionnarieAnswers.AddWhyBody
FROM            dbo.UserQuestionnarieAnswers INNER JOIN
                         dbo.UserQuestionnaries ON dbo.UserQuestionnarieAnswers.UserQuestionnarieID = dbo.UserQuestionnaries.ID


GO
/****** Object:  Table [dbo].[Answers]    Script Date: 18.12.2018 01:57:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Answers](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Answer] [nvarchar](250) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Questionnaries]    Script Date: 18.12.2018 01:57:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Questionnaries](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Code] [nvarchar](5) NULL,
	[Active] [bit] NOT NULL,
	[MaxAnswers] [int] NOT NULL,
	[Description] [nvarchar](1000) NULL,
 CONSTRAINT [PK_Questionaries] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[QuestionnariesQuestion]    Script Date: 18.12.2018 01:57:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QuestionnariesQuestion](
	[QuestionnarieID] [int] NOT NULL,
	[QuestionID] [int] NOT NULL,
	[QuestionNumber] [int] NULL,
 CONSTRAINT [PK_QuestionnariesQuestion] PRIMARY KEY CLUSTERED 
(
	[QuestionID] ASC,
	[QuestionnarieID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Questions]    Script Date: 18.12.2018 01:57:15 ******/
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
/****** Object:  Table [dbo].[QuestionsAnswer]    Script Date: 18.12.2018 01:57:15 ******/
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
/****** Object:  View [dbo].[VUserQuestionnarie]    Script Date: 18.12.2018 01:57:15 ******/
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
dbo.Questions.AnswerType, 
dbo.Questionnaries.Code
FROM     dbo.Questionnaries INNER JOIN
         dbo.QuestionnariesQuestion ON dbo.Questionnaries.ID = dbo.QuestionnariesQuestion.QuestionnarieID 
		 left JOIN dbo.Questions ON dbo.QuestionnariesQuestion.QuestionID = dbo.Questions.ID 
		 left JOIN dbo.QuestionsAnswer ON dbo.Questions.ID = dbo.QuestionsAnswer.QuestionID 
		 left JOIN dbo.Answers ON dbo.QuestionsAnswer.AnswerID = dbo.Answers.ID
						 

GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 18.12.2018 01:57:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AnswerTypes]    Script Date: 18.12.2018 01:57:15 ******/
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
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 18.12.2018 01:57:15 ******/
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
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 18.12.2018 01:57:15 ******/
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
SET IDENTITY_INSERT [dbo].[Answers] ON 
GO
INSERT [dbo].[Answers] ([ID], [Answer]) VALUES (1, N'Tak')
GO
INSERT [dbo].[Answers] ([ID], [Answer]) VALUES (2, N'Nie')
GO
INSERT [dbo].[Answers] ([ID], [Answer]) VALUES (3, N'1')
GO
INSERT [dbo].[Answers] ([ID], [Answer]) VALUES (4, N'2')
GO
INSERT [dbo].[Answers] ([ID], [Answer]) VALUES (5, N'3')
GO
INSERT [dbo].[Answers] ([ID], [Answer]) VALUES (6, N'4')
GO
INSERT [dbo].[Answers] ([ID], [Answer]) VALUES (7, N'5')
GO
INSERT [dbo].[Answers] ([ID], [Answer]) VALUES (8, N'Może')
GO
INSERT [dbo].[Answers] ([ID], [Answer]) VALUES (9, N'lepsza niż się spodziewałem/am')
GO
INSERT [dbo].[Answers] ([ID], [Answer]) VALUES (10, N'gorsza niż się spodziewałem/am')
GO
INSERT [dbo].[Answers] ([ID], [Answer]) VALUES (11, N'Fanpage Szkoły Programowania WSEI')
GO
INSERT [dbo].[Answers] ([ID], [Answer]) VALUES (12, N'Fanpage innego podmiotu/organizacji (jaki?)')
GO
INSERT [dbo].[Answers] ([ID], [Answer]) VALUES (13, N'Reklama w sieci (google)')
GO
INSERT [dbo].[Answers] ([ID], [Answer]) VALUES (14, N'Serwis enevea.pl')
GO
INSERT [dbo].[Answers] ([ID], [Answer]) VALUES (15, N'Linkedin Szkoły Programowania WSEI')
GO
INSERT [dbo].[Answers] ([ID], [Answer]) VALUES (16, N'Polecenie znajomego/znajomej')
GO
INSERT [dbo].[Answers] ([ID], [Answer]) VALUES (17, N'Inne  (jakie źródło?)')
GO
INSERT [dbo].[Answers] ([ID], [Answer]) VALUES (19, N'PONIEDZIAŁEK')
GO
INSERT [dbo].[Answers] ([ID], [Answer]) VALUES (20, N'WTOREK')
GO
INSERT [dbo].[Answers] ([ID], [Answer]) VALUES (21, N'ŚRODA')
GO
INSERT [dbo].[Answers] ([ID], [Answer]) VALUES (22, N'CZWARTEK ')
GO
INSERT [dbo].[Answers] ([ID], [Answer]) VALUES (23, N'PIĄTEK')
GO
INSERT [dbo].[Answers] ([ID], [Answer]) VALUES (24, N'SOBOTA')
GO
INSERT [dbo].[Answers] ([ID], [Answer]) VALUES (25, N'do południa (8:00-12:00)')
GO
INSERT [dbo].[Answers] ([ID], [Answer]) VALUES (26, N'po południu (12:00-16:00)	')
GO
INSERT [dbo].[Answers] ([ID], [Answer]) VALUES (27, N'wieczorem (16:00-20:00)')
GO
INSERT [dbo].[Answers] ([ID], [Answer]) VALUES (28, N'maturzysta')
GO
INSERT [dbo].[Answers] ([ID], [Answer]) VALUES (29, N'student')
GO
INSERT [dbo].[Answers] ([ID], [Answer]) VALUES (30, N'osoba pracująca ( określ branżę/stanowisko pracy?)')
GO
INSERT [dbo].[Answers] ([ID], [Answer]) VALUES (31, N'osoba bezrobotna')
GO
INSERT [dbo].[Answers] ([ID], [Answer]) VALUES (32, N'urlop wychowawczy')
GO
INSERT [dbo].[Answers] ([ID], [Answer]) VALUES (33, N'KOBIETA')
GO
INSERT [dbo].[Answers] ([ID], [Answer]) VALUES (34, N'MĘŻCZYZNA')
GO
INSERT [dbo].[Answers] ([ID], [Answer]) VALUES (35, N'18-29')
GO
INSERT [dbo].[Answers] ([ID], [Answer]) VALUES (36, N'30-39')
GO
INSERT [dbo].[Answers] ([ID], [Answer]) VALUES (37, N'40-49')
GO
INSERT [dbo].[Answers] ([ID], [Answer]) VALUES (38, N'powyżej 49 lat')
GO
INSERT [dbo].[Answers] ([ID], [Answer]) VALUES (39, N'taka, jaką się spodziewałem/am')
GO
INSERT [dbo].[Answers] ([ID], [Answer]) VALUES (40, N'Jestem kursantem szkoły')
GO
SET IDENTITY_INSERT [dbo].[Answers] OFF
GO
INSERT [dbo].[AnswerTypes] ([AnswerType]) VALUES (N'Jednokrotny')
GO
INSERT [dbo].[AnswerTypes] ([AnswerType]) VALUES (N'Numeryczny')
GO
INSERT [dbo].[AnswerTypes] ([AnswerType]) VALUES (N'Opisowy')
GO
INSERT [dbo].[AnswerTypes] ([AnswerType]) VALUES (N'Wielokrotny')
GO
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'2df56727-5b8b-412e-8dd0-b0b01cbaec78', N'admin@admin.admin', N'ADMIN@ADMIN.ADMIN', NULL, NULL, 0, N'AQAAAAEAACcQAAAAED4HyZh0aUN7+z6CihzLt1/i8ghaUIsh1knKw1yDFKYSQRPU97abfnA+hPCQBfOs0A==', N'RX7SXOGVZGFTWWA7UAOCS3VEOOJKEPWI', N'a646307e-3a45-4b57-83cd-767e4eada78c', NULL, 0, 0, NULL, 1, 0)
GO
SET IDENTITY_INSERT [dbo].[Questionnaries] ON 
GO
INSERT [dbo].[Questionnaries] ([ID], [Name], [Code], [Active], [MaxAnswers], [Description]) VALUES (5, N'ANKIETA KOŃCOWA', N'4ba2a', 1, 25, N'# Wypełnienie poniższej ankiety i Państwa sugestie, pomogą nam w przyszłości do organizacji kolejnych kursów/szkoleń/ warsztatów, takich jakich Państwo oczekują i w jakich chętnie wzięliby udział. 
# Ankieta jest anonimowa. Czas na wypełnienie to maksymalnie 3  minuty :)
# Dziękujemy za poświęcony czas na udzielenie odpowiedzi. 
')
GO
SET IDENTITY_INSERT [dbo].[Questionnaries] OFF
GO
INSERT [dbo].[QuestionnariesQuestion] ([QuestionnarieID], [QuestionID], [QuestionNumber]) VALUES (5, 1, 1)
GO
INSERT [dbo].[QuestionnariesQuestion] ([QuestionnarieID], [QuestionID], [QuestionNumber]) VALUES (5, 2, 2)
GO
INSERT [dbo].[QuestionnariesQuestion] ([QuestionnarieID], [QuestionID], [QuestionNumber]) VALUES (5, 3, 3)
GO
INSERT [dbo].[QuestionnariesQuestion] ([QuestionnarieID], [QuestionID], [QuestionNumber]) VALUES (5, 4, 4)
GO
INSERT [dbo].[QuestionnariesQuestion] ([QuestionnarieID], [QuestionID], [QuestionNumber]) VALUES (5, 5, 5)
GO
INSERT [dbo].[QuestionnariesQuestion] ([QuestionnarieID], [QuestionID], [QuestionNumber]) VALUES (5, 6, 6)
GO
INSERT [dbo].[QuestionnariesQuestion] ([QuestionnarieID], [QuestionID], [QuestionNumber]) VALUES (5, 7, 7)
GO
INSERT [dbo].[QuestionnariesQuestion] ([QuestionnarieID], [QuestionID], [QuestionNumber]) VALUES (5, 8, 8)
GO
INSERT [dbo].[QuestionnariesQuestion] ([QuestionnarieID], [QuestionID], [QuestionNumber]) VALUES (5, 9, 9)
GO
INSERT [dbo].[QuestionnariesQuestion] ([QuestionnarieID], [QuestionID], [QuestionNumber]) VALUES (5, 10, 10)
GO
INSERT [dbo].[QuestionnariesQuestion] ([QuestionnarieID], [QuestionID], [QuestionNumber]) VALUES (5, 11, 11)
GO
INSERT [dbo].[QuestionnariesQuestion] ([QuestionnarieID], [QuestionID], [QuestionNumber]) VALUES (5, 12, 12)
GO
INSERT [dbo].[QuestionnariesQuestion] ([QuestionnarieID], [QuestionID], [QuestionNumber]) VALUES (5, 13, 13)
GO
INSERT [dbo].[QuestionnariesQuestion] ([QuestionnarieID], [QuestionID], [QuestionNumber]) VALUES (5, 14, 14)
GO
INSERT [dbo].[QuestionnariesQuestion] ([QuestionnarieID], [QuestionID], [QuestionNumber]) VALUES (5, 15, 15)
GO
SET IDENTITY_INSERT [dbo].[Questions] ON 
GO
INSERT [dbo].[Questions] ([ID], [Question], [AnswerType], [AddWhyName], [AddWhy]) VALUES (1, N'CZY UWAŻASZ, ŻE TEMAT KURSU ROZWINĄŁ TWOJE UMIEJĘTNOŚCI PRZYDATNE
 W PROGRAMOWANIU?', N'Jednokrotny', NULL, 0)
GO
INSERT [dbo].[Questions] ([ID], [Question], [AnswerType], [AddWhyName], [AddWhy]) VALUES (2, N'JAK OCENIASZ ORGANIZACJĘ PROWADZONEGO KURSU? UZASADNIJ SWÓJ WYBÓR.', N'Jednokrotny', N'Uzasadnij jeśli inna niż spodziewana', 1)
GO
INSERT [dbo].[Questions] ([ID], [Question], [AnswerType], [AddWhyName], [AddWhy]) VALUES (3, N'JAK DOWIEDZIAŁEŚ/AŚ SIĘ O KURSIE? ', N'Jednokrotny', N'Jakie źródło', 1)
GO
INSERT [dbo].[Questions] ([ID], [Question], [AnswerType], [AddWhyName], [AddWhy]) VALUES (4, N'JAK OCENIASZ MATERIAŁY PODCZAS KURSU (prezentacja/to, z czego korzystałeś na zajęciach/materiały na platformie)  (1 – słabo przygotowana; 5 – bardzo dobrze przygotowana)', N'Jednokrotny', N'Uzasadnij swoją ocenę', 1)
GO
INSERT [dbo].[Questions] ([ID], [Question], [AnswerType], [AddWhyName], [AddWhy]) VALUES (5, N'JAK OCENIASZ SPOSÓB PROWADZENIA KURSU PRZEZ TRENERA ? (1 – mało profesjonalnie 5 – bardzo profesjonalnie)
', N'Jednokrotny', N'Uzasadnij swoją ocenę', 1)
GO
INSERT [dbo].[Questions] ([ID], [Question], [AnswerType], [AddWhyName], [AddWhy]) VALUES (6, N'O CZYM CHCIAŁBYŚ/CHCIAŁABYŚ POSŁUCHAĆ NA BEZPŁATNYCH WARSZTATACH, ORGANIZOWANYCH PRZEZ SZKOŁĘ PROGRAMOWANIA WSEI', N'Opisowy', NULL, 0)
GO
INSERT [dbo].[Questions] ([ID], [Question], [AnswerType], [AddWhyName], [AddWhy]) VALUES (7, N'JAKIE DNI TYGODNIA SĄ DLA CIEBIE NAJBARDZIEJ ODPOWIEDNIE NA TAKIE WARSZTATY ?', N'Wielokrotny', NULL, 0)
GO
INSERT [dbo].[Questions] ([ID], [Question], [AnswerType], [AddWhyName], [AddWhy]) VALUES (8, N'JAKIE  GODZINY SĄ DLA CIEBIE NAJBARDZIEJ ODPOWIEDNIE NA TAKIE WARSZTATY ?', N'Wielokrotny', NULL, 0)
GO
INSERT [dbo].[Questions] ([ID], [Question], [AnswerType], [AddWhyName], [AddWhy]) VALUES (9, N'OKREŚL SWÓJ STATUS ZAWODOWY. MOŻESZ WYBRAĆ WIĘCEJ NIŻ 1 ODPOWIEDŹ.', N'Wielokrotny', N'Status zawodowy', 1)
GO
INSERT [dbo].[Questions] ([ID], [Question], [AnswerType], [AddWhyName], [AddWhy]) VALUES (10, N'CZY JESTEŚ STUDENTEM/STUDENTKĄ WSEI?   ', N'Jednokrotny', NULL, 0)
GO
INSERT [dbo].[Questions] ([ID], [Question], [AnswerType], [AddWhyName], [AddWhy]) VALUES (11, N'CZY JESTEŚ ZAINTERESOWANY/A PODJĘCIEM NAUKI W SZKOLE PROGRAMOWANIA WSEI?', N'Jednokrotny', NULL, 0)
GO
INSERT [dbo].[Questions] ([ID], [Question], [AnswerType], [AddWhyName], [AddWhy]) VALUES (12, N'CO W OFERCIE SZKOŁY PROGRAMOWANIA WSEI ZWRACA TWOJĄ UWAGĘ? ', N'Opisowy', NULL, 0)
GO
INSERT [dbo].[Questions] ([ID], [Question], [AnswerType], [AddWhyName], [AddWhy]) VALUES (13, N'#Czy chciałbyś/aś jeszcze coś dodać? Tutaj możesz zostawić nam cenną informację :)', N'Jednokrotny', N'Opisz cenną informację', 1)
GO
INSERT [dbo].[Questions] ([ID], [Question], [AnswerType], [AddWhyName], [AddWhy]) VALUES (14, N'#Metryczka', N'Jednokrotny', NULL, 0)
GO
INSERT [dbo].[Questions] ([ID], [Question], [AnswerType], [AddWhyName], [AddWhy]) VALUES (15, N'#Wiek', N'Jednokrotny', NULL, 0)
GO
SET IDENTITY_INSERT [dbo].[Questions] OFF
GO
INSERT [dbo].[QuestionsAnswer] ([QuestionID], [AnswerID]) VALUES (1, 1)
GO
INSERT [dbo].[QuestionsAnswer] ([QuestionID], [AnswerID]) VALUES (10, 1)
GO
INSERT [dbo].[QuestionsAnswer] ([QuestionID], [AnswerID]) VALUES (11, 1)
GO
INSERT [dbo].[QuestionsAnswer] ([QuestionID], [AnswerID]) VALUES (13, 1)
GO
INSERT [dbo].[QuestionsAnswer] ([QuestionID], [AnswerID]) VALUES (1, 2)
GO
INSERT [dbo].[QuestionsAnswer] ([QuestionID], [AnswerID]) VALUES (10, 2)
GO
INSERT [dbo].[QuestionsAnswer] ([QuestionID], [AnswerID]) VALUES (11, 2)
GO
INSERT [dbo].[QuestionsAnswer] ([QuestionID], [AnswerID]) VALUES (13, 2)
GO
INSERT [dbo].[QuestionsAnswer] ([QuestionID], [AnswerID]) VALUES (4, 3)
GO
INSERT [dbo].[QuestionsAnswer] ([QuestionID], [AnswerID]) VALUES (5, 3)
GO
INSERT [dbo].[QuestionsAnswer] ([QuestionID], [AnswerID]) VALUES (4, 4)
GO
INSERT [dbo].[QuestionsAnswer] ([QuestionID], [AnswerID]) VALUES (5, 4)
GO
INSERT [dbo].[QuestionsAnswer] ([QuestionID], [AnswerID]) VALUES (4, 5)
GO
INSERT [dbo].[QuestionsAnswer] ([QuestionID], [AnswerID]) VALUES (5, 5)
GO
INSERT [dbo].[QuestionsAnswer] ([QuestionID], [AnswerID]) VALUES (4, 6)
GO
INSERT [dbo].[QuestionsAnswer] ([QuestionID], [AnswerID]) VALUES (5, 6)
GO
INSERT [dbo].[QuestionsAnswer] ([QuestionID], [AnswerID]) VALUES (4, 7)
GO
INSERT [dbo].[QuestionsAnswer] ([QuestionID], [AnswerID]) VALUES (5, 7)
GO
INSERT [dbo].[QuestionsAnswer] ([QuestionID], [AnswerID]) VALUES (1, 8)
GO
INSERT [dbo].[QuestionsAnswer] ([QuestionID], [AnswerID]) VALUES (2, 9)
GO
INSERT [dbo].[QuestionsAnswer] ([QuestionID], [AnswerID]) VALUES (2, 10)
GO
INSERT [dbo].[QuestionsAnswer] ([QuestionID], [AnswerID]) VALUES (3, 11)
GO
INSERT [dbo].[QuestionsAnswer] ([QuestionID], [AnswerID]) VALUES (3, 12)
GO
INSERT [dbo].[QuestionsAnswer] ([QuestionID], [AnswerID]) VALUES (3, 13)
GO
INSERT [dbo].[QuestionsAnswer] ([QuestionID], [AnswerID]) VALUES (3, 14)
GO
INSERT [dbo].[QuestionsAnswer] ([QuestionID], [AnswerID]) VALUES (3, 15)
GO
INSERT [dbo].[QuestionsAnswer] ([QuestionID], [AnswerID]) VALUES (3, 16)
GO
INSERT [dbo].[QuestionsAnswer] ([QuestionID], [AnswerID]) VALUES (3, 17)
GO
INSERT [dbo].[QuestionsAnswer] ([QuestionID], [AnswerID]) VALUES (7, 19)
GO
INSERT [dbo].[QuestionsAnswer] ([QuestionID], [AnswerID]) VALUES (7, 20)
GO
INSERT [dbo].[QuestionsAnswer] ([QuestionID], [AnswerID]) VALUES (7, 21)
GO
INSERT [dbo].[QuestionsAnswer] ([QuestionID], [AnswerID]) VALUES (7, 22)
GO
INSERT [dbo].[QuestionsAnswer] ([QuestionID], [AnswerID]) VALUES (7, 23)
GO
INSERT [dbo].[QuestionsAnswer] ([QuestionID], [AnswerID]) VALUES (7, 24)
GO
INSERT [dbo].[QuestionsAnswer] ([QuestionID], [AnswerID]) VALUES (8, 25)
GO
INSERT [dbo].[QuestionsAnswer] ([QuestionID], [AnswerID]) VALUES (8, 26)
GO
INSERT [dbo].[QuestionsAnswer] ([QuestionID], [AnswerID]) VALUES (8, 27)
GO
INSERT [dbo].[QuestionsAnswer] ([QuestionID], [AnswerID]) VALUES (9, 28)
GO
INSERT [dbo].[QuestionsAnswer] ([QuestionID], [AnswerID]) VALUES (9, 29)
GO
INSERT [dbo].[QuestionsAnswer] ([QuestionID], [AnswerID]) VALUES (9, 30)
GO
INSERT [dbo].[QuestionsAnswer] ([QuestionID], [AnswerID]) VALUES (9, 31)
GO
INSERT [dbo].[QuestionsAnswer] ([QuestionID], [AnswerID]) VALUES (9, 32)
GO
INSERT [dbo].[QuestionsAnswer] ([QuestionID], [AnswerID]) VALUES (14, 33)
GO
INSERT [dbo].[QuestionsAnswer] ([QuestionID], [AnswerID]) VALUES (14, 34)
GO
INSERT [dbo].[QuestionsAnswer] ([QuestionID], [AnswerID]) VALUES (15, 35)
GO
INSERT [dbo].[QuestionsAnswer] ([QuestionID], [AnswerID]) VALUES (15, 36)
GO
INSERT [dbo].[QuestionsAnswer] ([QuestionID], [AnswerID]) VALUES (15, 37)
GO
INSERT [dbo].[QuestionsAnswer] ([QuestionID], [AnswerID]) VALUES (15, 38)
GO
INSERT [dbo].[QuestionsAnswer] ([QuestionID], [AnswerID]) VALUES (2, 39)
GO
INSERT [dbo].[QuestionsAnswer] ([QuestionID], [AnswerID]) VALUES (11, 40)
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

