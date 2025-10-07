USE [WorkFinderDb]
GO

/****** Object:  Table [dbo].[Qualifications]    Script Date: 9/30/2025 7:41:30 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Qualifications](
	[QualificationId] [int] NOT NULL,
	[QualificationName] [nvarchar](300) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[QualificationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


USE [WorkFinderDb]
GO

/****** Object:  StoredProcedure [dbo].[InsertQualification]    Script Date: 9/30/2025 7:41:50 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[InsertQualification] 
	-- Add the parameters for the stored procedure here
	@QualificationId INT,
	@QualificationName NVARCHAR(300)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO Qualifications(QualificationId,QualificationName) VALUES(@QualificationId,@QualificationName);
END
GO


USE [WorkFinderDb]
GO

/****** Object:  StoredProcedure [dbo].[GetQualification]    Script Date: 9/30/2025 7:42:09 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetQualification]
	-- Add the parameters for the stored procedure here
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT QualificationId,QualificationName FROM Qualifications;
END
GO


USE [WorkFinderDb]
GO

/****** Object:  Table [dbo].[Countries]    Script Date: 9/30/2025 7:42:34 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Countries](
	[CountryId] [int] NOT NULL,
	[CountryName] [nvarchar](300) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[CountryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


USE [WorkFinderDb]
GO

/****** Object:  StoredProcedure [dbo].[GetCountry]    Script Date: 9/30/2025 7:42:55 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetCountry]
	-- Add the parameters for the stored procedure here
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT CountryId,CountryName FROM Countries;
END
GO


USE [WorkFinderDb]
GO

/****** Object:  StoredProcedure [dbo].[InsertCountry]    Script Date: 9/30/2025 7:43:09 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[InsertCountry]
	-- Add the parameters for the stored procedure here
	@CountryId INT,
	@CountryName NVARCHAR(300)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO Countries(CountryId,CountryName) VALUES(@CountryId,@CountryName);
END
GO


USE [WorkFinderDb]
GO

/****** Object:  StoredProcedure [dbo].[InsertApplicant]    Script Date: 9/30/2025 7:43:53 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[InsertApplicant]
	-- Add the parameters for the stored procedure here
	@UserId UNIQUEIDENTIFIER,
	@Resume NVARCHAR(300),
	@Gender NVARCHAR(30)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO Applicants (UserId,[Resume],[Gender]) VALUES (@UserId,@Resume,@Gender);

	SELECT ApplicantId FROM Applicants WHERE UserId = @UserId;
END
GO


USE [WorkFinderDb]
GO

/****** Object:  StoredProcedure [dbo].[UpdateApplicantResume]    Script Date: 9/30/2025 7:44:32 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[UpdateApplicantResume] 
	-- Add the parameters for the stored procedure here
	@Resume NVARCHAR(200),
	@ApplicantId UNIQUEIDENTIFIER
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE Applicants SET [Resume] = @Resume
	WHERE ApplicantId = @ApplicantId;
END
GO


USE [WorkFinderDb]
GO

/****** Object:  StoredProcedure [dbo].[InsertJob]    Script Date: 9/30/2025 7:44:55 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[InsertJob]
	-- Add the parameters for the stored procedure here
	@Title NVARCHAR(300),
	@Description NVARCHAR(MAX),
	@City NVARCHAR(300),
	@ExpiryDate Date,
	@EmployerId UNIQUEIDENTIFIER,
	@IndustryId INT,
	@CreatedBy UNIQUEIDENTIFIER
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO WorkFinderDb.dbo.Jobs (Title,Description,City,ExpiryDate,EmployerId,IndustryId,CreatedBy,IsActive)
	VALUES (@Title,@Description,@City,@ExpiryDate,@EmployerId,@IndustryId,@CreatedBy,1)

	SELECT SCOPE_IDENTITY()
END
GO


USE [WorkFinderDb]
GO

/****** Object:  StoredProcedure [dbo].[GetIndustryById]    Script Date: 9/30/2025 7:45:12 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetIndustryById] 
	-- Add the parameters for the stored procedure here
	@IndustryId INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT [IndustryId],[IndustryName] FROM Industries WHERE [IndustryId] = @IndustryId;
END
GO


