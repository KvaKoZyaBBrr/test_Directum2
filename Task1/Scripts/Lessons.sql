USE [directumTest]
GO

/****** Object:  Table [dbo].[Lessons]    Script Date: 21.08.2022 2:07:12 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Lessons](
	[IDStudent] [int] NOT NULL,
	[IDCourse] [int] NOT NULL,
	[time] [datetime] NULL,
 CONSTRAINT [PK_Lessons] PRIMARY KEY CLUSTERED 
(
	[IDStudent] ASC,
	[IDCourse] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Lessons]  WITH CHECK ADD  CONSTRAINT [FK_Lessons_Schedule] FOREIGN KEY([IDCourse], [time])
REFERENCES [dbo].[Schedule] ([IDcourse], [time])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Lessons] CHECK CONSTRAINT [FK_Lessons_Schedule]
GO

ALTER TABLE [dbo].[Lessons]  WITH CHECK ADD  CONSTRAINT [FK_Lessons_Student] FOREIGN KEY([IDStudent])
REFERENCES [dbo].[Student] ([ID])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Lessons] CHECK CONSTRAINT [FK_Lessons_Student]
GO

