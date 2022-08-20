USE [directumTest]
GO

/****** Object:  Table [dbo].[Schedule]    Script Date: 21.08.2022 2:06:57 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Schedule](
	[IDcourse] [int] NOT NULL,
	[time] [datetime] NOT NULL,
	[count] [nchar](10) NULL,
 CONSTRAINT [PK_Schedule] PRIMARY KEY CLUSTERED 
(
	[IDcourse] ASC,
	[time] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Schedule]  WITH CHECK ADD  CONSTRAINT [FK_Schedule_Course] FOREIGN KEY([IDcourse])
REFERENCES [dbo].[Course] ([ID])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Schedule] CHECK CONSTRAINT [FK_Schedule_Course]
GO

