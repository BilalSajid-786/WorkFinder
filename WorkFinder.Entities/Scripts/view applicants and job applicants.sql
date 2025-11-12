USE [WorkFinderDb];
GO

SET ANSI_NULLS ON;
GO
SET QUOTED_IDENTIFIER ON;
GO

CREATE TABLE [dbo].[Cities](
    [CityId]    INT            NOT NULL,
    [CityName]  NVARCHAR(300)  NOT NULL,
    [CountryId] INT            NOT NULL,
    CONSTRAINT [PK_Cities] PRIMARY KEY CLUSTERED ([CityId] ASC)
) ON [PRIMARY];
GO

ALTER TABLE [dbo].[Cities]  WITH CHECK ADD CONSTRAINT [FK_Cities_Countries]
    FOREIGN KEY ([CountryId]) REFERENCES [dbo].[Countries]([CountryId])
    ON UPDATE CASCADE
    ON DELETE NO ACTION;
GO

ALTER TABLE [dbo].[Cities] CHECK CONSTRAINT [FK_Cities_Countries];
GO

-- OPTIONAL:
ALTER TABLE [dbo].[Cities]
ADD CONSTRAINT [UQ_Cities_CountryId_CityName] UNIQUE ([CountryId], [CityName]);






GO
/****** Object:  StoredProcedure [dbo].[InsertCity]    Script Date: 11/11/2025 11:33:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[InsertCity]
	-- Add the parameters for the stored procedure here
	@CityId INT,
	@CityName NVARCHAR(300),
	@CountryId INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO Cities(CityId, CityName, CountryId) VALUES(@CityId,@CityName, @CountryId);
END




GO
/****** Object:  StoredProcedure [dbo].[GetCity]    Script Date: 11/11/2025 11:43:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetCity]
	-- Add the parameters for the stored procedure here
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT CityId,CityName FROM Cities;
END




GO
/****** Object:  StoredProcedure [dbo].[GetCitiesByCountryId]    Script Date: 11/12/2025 12:27:47 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetCitiesByCountryId]
	-- Add the parameters for the stored procedure here
	@CountryId INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT CityId,CityName FROM Cities where CountryId = @CountryId;
END


select * from dbo.Permissions

update dbo.Permissions set DisplayName = 'View Applicants' where PermissionId = 6



GO
/****** Object:  StoredProcedure [dbo].[GetApplicants]    Script Date: 11/12/2025 3:29:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetApplicants]
  @SortColumn   VARCHAR(50),
  @SortOrder    VARCHAR(50),
  @PageSize     INT,
  @PageNo       INT,
  @SkillId      INT = NULL,
  @City         VARCHAR(20) = NULL,
  @Country      VARCHAR(20) = NULL,
  @TotalCount   INT OUTPUT
AS
BEGIN
  SET NOCOUNT ON;

  IF (@PageNo < 1) SET @PageNo = 1;
  SET @City    = NULLIF(LTRIM(RTRIM(@City)), '');
  SET @Country = NULLIF(LTRIM(RTRIM(@Country)), '');
  IF (@SkillId IS NOT NULL AND @SkillId <= 0) SET @SkillId = NULL;

  /* Create a fixed-schema temp table (skill cols nullable) */
  CREATE TABLE #Filtered
  (
      UserId            UNIQUEIDENTIFIER,
      UserName          NVARCHAR(256),
      City              NVARCHAR(100),
      Country           NVARCHAR(100),
      Email             NVARCHAR(256),
      Phone             NVARCHAR(50),
      IsActive          BIT,
      ApplicantId       UNIQUEIDENTIFIER,
      Gender            NVARCHAR(50),
      [Resume]          NVARCHAR(MAX),
      QualificationId   INT,
      QualificationName NVARCHAR(200),
      SkillId           INT            NULL,
      SkillName         NVARCHAR(200)  NULL
  );

  IF @SkillId IS NULL
  BEGIN
      /* No skill filter: no join to ApplicantSkills/Skills; skill cols NULL */
      INSERT INTO #Filtered
      (
        UserId, UserName, City, Country, Email, Phone, IsActive,
        ApplicantId, Gender, [Resume],
        QualificationId, QualificationName,
        SkillId, SkillName
      )
      SELECT
        u.UserId, u.UserName, u.City, u.Country, u.Email, u.Phone, u.IsActive,
        a.ApplicantId, a.Gender, a.[Resume],
        q.QualificationId, q.QualificationName,
        NULL AS SkillId, NULL AS SkillName
      FROM dbo.Users u
      JOIN dbo.Applicants a        ON u.UserId = a.UserId
      JOIN dbo.Qualifications q    ON q.QualificationId = a.QualificationId
      WHERE
        (@City    IS NULL OR u.City    = @City) AND
        (@Country IS NULL OR u.Country = @Country);
  END
  ELSE
  BEGIN
      /* Skill filter present: join and filter skills */
      INSERT INTO #Filtered
      (
        UserId, UserName, City, Country, Email, Phone, IsActive,
        ApplicantId, Gender, [Resume],
        QualificationId, QualificationName,
        SkillId, SkillName
      )
      SELECT
        u.UserId, u.UserName, u.City, u.Country, u.Email, u.Phone, u.IsActive,
        a.ApplicantId, a.Gender, a.[Resume],
        q.QualificationId, q.QualificationName,
        s.SkillId, s.SkillName
      FROM dbo.Users u
      JOIN dbo.Applicants a          ON u.UserId = a.UserId
      JOIN dbo.Qualifications q      ON q.QualificationId = a.QualificationId
      JOIN dbo.ApplicantSkills apps  ON apps.ApplicantId = a.ApplicantId
      JOIN dbo.Skills s              ON s.SkillId = apps.SkillId
      WHERE
        (@City    IS NULL OR u.City    = @City) AND
        (@Country IS NULL OR u.Country = @Country) AND
        s.SkillId = @SkillId;
  END

  /* Total */
  SELECT @TotalCount = COUNT(*) FROM #Filtered;

  /* Paged result */
  SELECT
      f.UserId,
      f.UserName,
      f.City,
      f.Country,
      f.Email,
      f.Phone,
      f.IsActive,
      f.ApplicantId,
      f.Gender,
      f.[Resume],
      f.QualificationId,
      f.QualificationName,
      f.SkillId,          -- will be NULL if @SkillId was NULL
      f.SkillName         -- will be NULL if @SkillId was NULL
  FROM #Filtered AS f
  ORDER BY
      CASE WHEN (@SortColumn = 'Name'  AND @SortOrder = 'ASC')  THEN f.UserName END ASC,
      CASE WHEN (@SortColumn = 'Name'  AND @SortOrder = 'DESC') THEN f.UserName END DESC,
      CASE WHEN (@SortColumn = 'City'  AND @SortOrder = 'ASC')  THEN f.City     END ASC,
      CASE WHEN (@SortColumn = 'City'  AND @SortOrder = 'DESC') THEN f.City     END DESC,
      CASE WHEN (@SortColumn = 'Email' AND @SortOrder = 'ASC')  THEN f.Email    END ASC,
      CASE WHEN (@SortColumn = 'Email' AND @SortOrder = 'DESC') THEN f.Email    END DESC
  OFFSET @PageSize * (@PageNo - 1) ROWS
  FETCH NEXT @PageSize ROWS ONLY
  OPTION (RECOMPILE);

  DROP TABLE #Filtered;
END


GO
/****** Object:  StoredProcedure [dbo].[UpdateJobApplicantStatus]    Script Date: 11/12/2025 3:30:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================

CREATE PROCEDURE [dbo].[UpdateJobApplicantStatus]
    @ApplicantId UNIQUEIDENTIFIER,
    @JobId INT,
    @ApplicantStatus VARCHAR(20)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[ApplicantJobs]
    SET JobStatus = @ApplicantStatus
    WHERE JobId = @JobId AND ApplicantId = @ApplicantId;

    -- Return the updated status
    IF @@ROWCOUNT = 1
    BEGIN
      SELECT @ApplicantStatus;
    END                     -- success: return the Applicant Status
    ELSE
    BEGIN
      SELECT CAST(NULL AS VARCHAR);
    END
END