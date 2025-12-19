USE [WorkFinderDb];
GO
SET ANSI_NULLS ON;
GO
SET QUOTED_IDENTIFIER ON;
GO

CREATE OR ALTER PROCEDURE [dbo].[UpdateJob]
    @Title       NVARCHAR(200),
    @Description NVARCHAR(MAX),
    @City        NVARCHAR(100),
    @Country     NVARCHAR(100),
    @JobType     NVARCHAR(50),
    @ExpiryDate  DATETIME2,
    @EmployerId  UNIQUEIDENTIFIER,
    @IndustryId  INT,
    @UpdatedBy   UNIQUEIDENTIFIER = NULL,
    @JobId       INT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE dbo.Jobs
    SET
        Title        = @Title,
        [Description]= @Description,
        City         = @City,
        Country      = @Country,
        JobType      = @JobType,
        ExpiryDate   = @ExpiryDate,
        EmployerId   = @EmployerId,
        IndustryId   = @IndustryId,
        UpdatedBy    = @UpdatedBy,
        UpdatedAt    = SYSUTCDATETIME()
    WHERE JobId = @JobId
      AND (IsDeleted = 0 OR IsDeleted IS NULL);  

   
    IF @@ROWCOUNT = 0 SELECT CAST(0 AS INT) AS JobId; ELSE SELECT @JobId AS JobId;

END
GO
--------------------------------------------------------------------------------------------------------
GO
SET ANSI_NULLS ON;
GO
SET QUOTED_IDENTIFIER ON;
GO

/* ===========================================================
   Get a single Job by Id (with Employer & Industry join)
   Dapper splitOn: "EmpSplit,IndSplit"
   =========================================================== */
CREATE OR ALTER PROCEDURE [dbo].[GetJobById]
    @JobId INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT TOP (1)
        
        j.JobId,
        j.Title,
        j.[Description],
        j.City,
        j.Country,
        j.JobType,
        j.ExpiryDate,
        j.EmployerId,
        j.IndustryId,
        j.IsActive,
        j.CreatedAt,

        
        e.EmployerId AS EmpSplit,         
        e.EmployerId AS EmployerId,      
        e.CompanyName,

        
        i.IndustryId AS IndSplit,         
        i.IndustryId AS IndustryId,       
        i.IndustryName

    FROM dbo.Jobs       AS j
    INNER JOIN dbo.Employers  AS e ON e.EmployerId  = j.EmployerId
    INNER JOIN dbo.Industries AS i ON i.IndustryId  = j.IndustryId
    WHERE j.JobId = @JobId
      AND j.IsDeleted = 0 AND j.IsActive = 1     
    OPTION (RECOMPILE);
END

-------------------------------------------------------------------------------------------------------
GO
SET ANSI_NULLS ON;
GO
SET QUOTED_IDENTIFIER ON;
GO

CREATE OR ALTER PROCEDURE [dbo].[DeleteJobSkills]
    @JobId INT
AS
BEGIN
    SET NOCOUNT ON;

   
    DELETE FROM dbo.JobSkills
    WHERE JobId = @JobId;

  
    SELECT @@ROWCOUNT AS DeletedCount;
END
GO