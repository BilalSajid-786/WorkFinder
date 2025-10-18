USE [WorkFinderDb]
GO

/****** Object:  StoredProcedure [dbo].[GetApplicantAppliedJobs]    Script Date: 10/18/2025 1:42:24 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetApplicantAppliedJobs] 
    @ApplicantId UNIQUEIDENTIFIER,
	@SearchValue NVARCHAR(100) = '',
	@SortColumn  NVARCHAR(50) = 'JOBTITLE',
	@SortOrder   NVARCHAR(50) = 'ASC',
	@PageSize    INT,
	@PageNo      INT,
	@TotalCount INT OUTPUT

AS
BEGIN
	SET NOCOUNT ON;

	SET @PageNo = ISNULL(@PageNo, 1);
	SET @PageSize = ISNULL(@PageSize, 5);

	DECLARE @Offset INT = (@PageNo - 1) * @PageSize;

	-- Create temp table
	IF OBJECT_ID('tempdb..#FilteredJobs') IS NOT NULL
		DROP TABLE #FilteredJobs;

	SELECT 
		jbs.JobId,
		jbs.Title,
		jbs.Description,
		jbs.City,
		jbs.JobType,
		jbs.Country,
		jbs.CreatedAt,
		ind.IndustryId,
		ind.IndustryName,
		emps.EmployerId,
		emps.CompanyName,
	    appljbs.JobStatus
	INTO #FilteredJobs
	FROM 
		ApplicantJobs AS appljbs
    INNER JOIN
	Jobs jbs on jbs.JobId = appljbs.JobId
	INNER JOIN 
		Industries AS ind ON jbs.IndustryId = ind.IndustryId
	INNER JOIN 
		Employers AS emps ON jbs.EmployerId = emps.EmployerId
	WHERE jbs.JobId IN (SELECT JobId FROM ApplicantJobs WHERE [ApplicantId] = @ApplicantId);

	--First result set: paginated data
	SELECT 
		f.JobId,
		f.Title,
		f.Description,
		f.City,
		f.JobType,
		f.Country,
		f.CreatedAt,
		f.IndustryId,
		f.IndustryName,
		f.EmployerId,
		f.CompanyName,
		f.JobStatus as Status
	FROM 
		#FilteredJobs AS f
	ORDER BY
		CASE WHEN (@SortColumn = 'JOBTITLE' AND @SortOrder = 'ASC')  THEN f.Title END ASC,
		CASE WHEN (@SortColumn = 'JOBTITLE' AND @SortOrder = 'DESC') THEN f.Title END DESC,
		f.Title ASC, f.JobId ASC
	OFFSET @Offset ROWS
	FETCH NEXT @PageSize ROWS ONLY;
	
	-- Second Result Set
	SELECT @TotalCount =
		COUNT(*)
	FROM #FilteredJobs;
END
GO


USE [WorkFinderDb]
GO

/****** Object:  StoredProcedure [dbo].[GetApplicantAvailableJobs]    Script Date: 10/18/2025 1:42:46 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetApplicantAvailableJobs] 
    @ApplicantId UNIQUEIDENTIFIER,
	@Location NVARCHAR(300),
	@IndustryId INT,
	@JobType VARCHAR(100),
	@SearchValue NVARCHAR(100) = '',
	@SortColumn  NVARCHAR(50) = 'JOBTITLE',
	@SortOrder   NVARCHAR(50) = 'ASC',
	@PageSize    INT,
	@PageNo      INT,
	@TotalCount INT OUTPUT

AS
BEGIN
	SET NOCOUNT ON;

	SET @PageNo = ISNULL(@PageNo, 1);
	SET @PageSize = ISNULL(@PageSize, 5);

	DECLARE @Offset INT = (@PageNo - 1) * @PageSize;

	-- Create temp table
	IF OBJECT_ID('tempdb..#FilteredJobs') IS NOT NULL
		DROP TABLE #FilteredJobs;

	SELECT 
		jbs.JobId,
		jbs.Title,
		jbs.Description,
		jbs.City,
		jbs.JobType,
		jbs.Country,
		jbs.CreatedAt,
		ind.IndustryId,
		ind.IndustryName,
		emps.EmployerId,
		emps.CompanyName
	INTO #FilteredJobs
	FROM 
		Jobs AS jbs
	INNER JOIN 
		Industries AS ind ON jbs.IndustryId = ind.IndustryId
	INNER JOIN 
		Employers AS emps ON jbs.EmployerId = emps.EmployerId
	WHERE
		(@Location IS NULL OR jbs.Country = @Location)
		AND (@IndustryID IS NULL OR ind.IndustryId = @IndustryID)
		AND (@JobType IS NULL OR jbs.JobType = @JobType)
		AND jbs.IsActive = 1
		AND jbs.JobId NOT IN (SELECT JobId FROM ApplicantJobs WHERE [ApplicantId] = @ApplicantId)
		AND jbs.JobId NOT IN (SELECT JobId FROM SavedJobs WHERE [ApplicantId] = @ApplicantId)

	--First result set: paginated data
	SELECT 
		f.JobId,
		f.Title,
		f.Description,
		f.City,
		f.JobType,
		f.Country,
		f.CreatedAt,
		f.IndustryId,
		f.IndustryName,
		f.EmployerId,
		f.CompanyName
	FROM 
		#FilteredJobs AS f
	ORDER BY
		CASE WHEN (@SortColumn = 'JOBTITLE' AND @SortOrder = 'ASC')  THEN f.Title END ASC,
		CASE WHEN (@SortColumn = 'JOBTITLE' AND @SortOrder = 'DESC') THEN f.Title END DESC,
		f.Title ASC, f.JobId ASC
	OFFSET @Offset ROWS
	FETCH NEXT @PageSize ROWS ONLY;
	
	-- Second Result Set
	SELECT @TotalCount =
		COUNT(*)
	FROM #FilteredJobs;
END
GO


USE [WorkFinderDb]
GO

/****** Object:  StoredProcedure [dbo].[GetApplicantSavedJobs]    Script Date: 10/18/2025 1:44:03 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[GetApplicantSavedJobs] 
    @ApplicantId UNIQUEIDENTIFIER,
	@SearchValue NVARCHAR(100) = '',
	@SortColumn  NVARCHAR(50) = 'JOBTITLE',
	@SortOrder   NVARCHAR(50) = 'ASC',
	@PageSize    INT,
	@PageNo      INT,
	@TotalCount INT OUTPUT

AS
BEGIN
	SET NOCOUNT ON;

	SET @PageNo = ISNULL(@PageNo, 1);
	SET @PageSize = ISNULL(@PageSize, 5);

	DECLARE @Offset INT = (@PageNo - 1) * @PageSize;

	-- Create temp table
	IF OBJECT_ID('tempdb..#FilteredJobs') IS NOT NULL
		DROP TABLE #FilteredJobs;

	SELECT 
		jbs.JobId,
		jbs.Title,
		jbs.Description,
		jbs.City,
		jbs.JobType,
		jbs.Country,
		jbs.CreatedAt,
		ind.IndustryId,
		ind.IndustryName,
		emps.EmployerId,
		emps.CompanyName
	INTO #FilteredJobs
	FROM 
	    SavedJobs sjbs
	INNER JOIN
		Jobs AS jbs ON sjbs.JobId = jbs.JobId
	INNER JOIN 
		Industries AS ind ON jbs.IndustryId = ind.IndustryId
	INNER JOIN 
		Employers AS emps ON jbs.EmployerId = emps.EmployerId
	WHERE
		--(@Location IS NULL OR jbs.Country = @Location)
		--AND (@IndustryID IS NULL OR ind.IndustryId = @IndustryID)
		--AND (@JobType IS NULL OR jbs.JobType = @JobType)
		jbs.IsActive = 1
		AND sjbs.ApplicantId = @ApplicantId
		AND sjbs.JobId NOT IN (SELECT JobId FROM ApplicantJobs WHERE [ApplicantId] = @ApplicantId);

	--First result set: paginated data
	SELECT 
		f.JobId,
		f.Title,
		f.Description,
		f.City,
		f.JobType,
		f.Country,
		f.CreatedAt,
		f.IndustryId,
		f.IndustryName,
		f.EmployerId,
		f.CompanyName
	FROM 
		#FilteredJobs AS f
	ORDER BY
		CASE WHEN (@SortColumn = 'JOBTITLE' AND @SortOrder = 'ASC')  THEN f.Title END ASC,
		CASE WHEN (@SortColumn = 'JOBTITLE' AND @SortOrder = 'DESC') THEN f.Title END DESC,
		f.Title ASC, f.JobId ASC
	OFFSET @Offset ROWS
	FETCH NEXT @PageSize ROWS ONLY;
	
	-- Second Result Set
	SELECT @TotalCount =
		COUNT(*)
	FROM #FilteredJobs;
END
GO



USE [WorkFinderDb]
GO

/****** Object:  StoredProcedure [dbo].[ApplyJob]    Script Date: 10/18/2025 1:41:27 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[ApplyJob]
	-- Add the parameters for the stored procedure here
	@ApplicantId UNIQUEIDENTIFIER,
	@JobId INT,
	@Status NVARCHAR(100)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO ApplicantJobs(ApplicantId,JobId,JobStatus) VALUES(@ApplicantId,@JobId,@Status);
END
GO