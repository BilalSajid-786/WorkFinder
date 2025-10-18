USE [WorkFinderDb]
GO

/****** Object:  Table [dbo].[ApplicantJobs]    Script Date: 10/18/2025 1:39:50 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ApplicantJobs](
	[ApplicantId] [uniqueidentifier] NOT NULL,
	[JobId] [int] NOT NULL,
	[JobStatus] [varchar](100) NOT NULL,
 CONSTRAINT [PK_ApplicantJobs] PRIMARY KEY CLUSTERED 
(
	[ApplicantId] ASC,
	[JobId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[ApplicantJobs]  WITH CHECK ADD  CONSTRAINT [FK_ApplicantJobs_Jobs] FOREIGN KEY([JobId])
REFERENCES [dbo].[Jobs] ([JobId])
GO

ALTER TABLE [dbo].[ApplicantJobs] CHECK CONSTRAINT [FK_ApplicantJobs_Jobs]
GO

ALTER TABLE [dbo].[ApplicantJobs]  WITH CHECK ADD  CONSTRAINT [FK_ApplicationJobs_Applicants] FOREIGN KEY([ApplicantId])
REFERENCES [dbo].[Applicants] ([ApplicantId])
GO

ALTER TABLE [dbo].[ApplicantJobs] CHECK CONSTRAINT [FK_ApplicationJobs_Applicants]
GO


USE [WorkFinderDb]
GO

/****** Object:  Table [dbo].[SavedJobs]    Script Date: 10/18/2025 1:40:53 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[SavedJobs](
	[ApplicantId] [uniqueidentifier] NOT NULL,
	[JobId] [int] NOT NULL,
 CONSTRAINT [PK_SavedJobs] PRIMARY KEY CLUSTERED 
(
	[ApplicantId] ASC,
	[JobId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[SavedJobs]  WITH CHECK ADD  CONSTRAINT [FK_SavedJobs_Applicants] FOREIGN KEY([ApplicantId])
REFERENCES [dbo].[Applicants] ([ApplicantId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[SavedJobs] CHECK CONSTRAINT [FK_SavedJobs_Applicants]
GO

ALTER TABLE [dbo].[SavedJobs]  WITH CHECK ADD  CONSTRAINT [FK_SavedJobs_Jobs] FOREIGN KEY([JobId])
REFERENCES [dbo].[Jobs] ([JobId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[SavedJobs] CHECK CONSTRAINT [FK_SavedJobs_Jobs]
GO








