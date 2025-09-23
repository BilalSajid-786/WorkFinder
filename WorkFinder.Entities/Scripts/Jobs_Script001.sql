
--CREATE TABLE Jobs (
--    JobId INT IDENTITY(1,1) PRIMARY KEY,
--    Title NVARCHAR(200) NOT NULL,
--    Description NVARCHAR(MAX) NULL,
--	City NVARCHAR(200) NOT NULL,
--    ExpiryDate DATE NOT NULL,
--	EmployerId UNIQUEIDENTIFIER NOT NULL,
--    IndustryId INT NOT NULL,
--	IsActive BIT NOT NULL,
--	CreatedAt DateTime NOT NULL DEFAULT GETUTCDATE(),
--	UpdatedAt DateTime,
--	CreatedBy UNIQUEIDENTIFIER NOT NULL,
--	UpdatedBy UNIQUEIDENTIFIER,

--	CONSTRAINT FK_Jobs_Employer 
--        FOREIGN KEY (EmployerId) REFERENCES Employers(EmployerId),
    
--	CONSTRAINT FK_Jobs_Industry 
--        FOREIGN KEY (IndustryId) REFERENCES Industries(IndustryId),

--	CONSTRAINT FK_Jobs_Creator 
--        FOREIGN KEY (CreatedBy) REFERENCES Users(UserId),

--);

--CREATE TABLE JobSkills (
--    JobId INT NOT NULL,
--    SkillId INT NOT NULL,

--    -- Composite Primary Key
--    CONSTRAINT PK_JobSkills PRIMARY KEY (JobId, SkillId),

--    -- Foreign Keys
--    CONSTRAINT FK_JobSkills_Jobs FOREIGN KEY (JobId) REFERENCES Jobs(JobId),
--    CONSTRAINT FK_JobSkills_Skills FOREIGN KEY (SkillId) REFERENCES Skills(SkillId)
--);

USE [WorkFinderDb]
GO
/****** Object:  StoredProcedure [dbo].[InsertJob]    Script Date: 9/23/2025 10:19:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[InsertJob]
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
	DECLARE @BaseUserId UNIQUEIDENTIFIER;
	--If employer creating a job
	IF(@EmployerId = @CreatedBy)
	BEGIN
	  SET @BaseUserId = (SELECT UserId FROM Employers WHERE EmployerId = @CreatedBy);
	END
	ELSE -- If admin creating a job for an employer
	BEGIN
	  SET @BaseUserId = @CreatedBy;
	END

    -- Insert statements for procedure here
	INSERT INTO WorkFinderDb.dbo.Jobs (Title,Description,City,ExpiryDate,EmployerId,IndustryId,CreatedBy,IsActive)
	VALUES (@Title,@Description,@City,@ExpiryDate,@EmployerId,@IndustryId,@BaseUserId,1)

	SELECT SCOPE_IDENTITY()
END

USE [WorkFinderDb]
GO
/****** Object:  StoredProcedure [dbo].[GetEmployerId]    Script Date: 9/23/2025 10:20:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[GetEmployerId]
	-- Add the parameters for the stored procedure here
	@UserId UNIQUEIDENTIFIER
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT EmployerId FROM Employers WHERE UserId = @UserId;
	END



	USE [WorkFinderDb]
GO
/****** Object:  StoredProcedure [dbo].[GetApplicantId]    Script Date: 9/23/2025 10:20:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[GetApplicantId]
	-- Add the parameters for the stored procedure here
	@UserId UNIQUEIDENTIFIER
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT ApplicantId FROM Applicants WHERE UserId = @UserId;
	END
