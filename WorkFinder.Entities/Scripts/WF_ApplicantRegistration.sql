USE [WorkFinderDb]
GO

/****** Object:  StoredProcedure [dbo].[GetSkillByName]    Script Date: 9/18/2025 9:54:48 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetSkillByName]
	-- Add the parameters for the stored procedure here
	@SearchName NVARCHAR(400)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM Skills
	WHERE SkillName LIKE '%' + @SearchName + '%'
END
GO


USE [WorkFinderDb]
GO

/****** Object:  StoredProcedure [dbo].[IsApplicantExist]    Script Date: 9/18/2025 9:54:59 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[IsApplicantExist]
	-- Add the parameters for the stored procedure here
	@ApplicantId UNIQUEIDENTIFIER
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	IF EXISTS (SELECT 1 FROM Applicants WHERE ApplicantId = @ApplicantId)
    SELECT CAST(1 AS bit);
ELSE
    SELECT CAST(0 AS bit);
END
GO




USE [WorkFinderDb]
GO

/****** Object:  StoredProcedure [dbo].[InsertApplicantSkill]    Script Date: 9/18/2025 9:55:26 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[InsertApplicantSkill]
	-- Add the parameters for the stored procedure here
	@ApplicantId UNIQUEIDENTIFIER,
	@SkillId INT,
	@SkillName NVARCHAR(300)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	  -- 1. Check if skill already exists

    SELECT @SkillId = SkillId 
    FROM Skills 
    WHERE SkillName = @SkillName;

    -- 2. If skill doesn't exist, insert into Skills
    IF @SkillId=0
    BEGIN
        INSERT INTO Skills (SkillName) VALUES (@SkillName);
        SET @SkillId = SCOPE_IDENTITY();  -- get newly inserted SkillId
    END

	--3. In the last, insert into ApplicantSkills
	INSERT INTO ApplicantSkills(ApplicantId,SkillId) VALUES (@ApplicantId,@SkillId);
END
GO





