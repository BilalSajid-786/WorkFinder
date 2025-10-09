ALTER PROCEDURE [dbo].[GetApplicantAvailableJobs] 
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
		AND jbs.IsActive = 1;

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
