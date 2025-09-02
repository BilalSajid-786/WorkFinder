USE [WorkFinderDb]
GO

/****** Object:  Table [dbo].[Users]    Script Date: 9/1/2025 5:04:32 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Users](
	[UserId] [uniqueidentifier] NOT NULL,
	[UserName] [nvarchar](100) NOT NULL,
	[Email] [nvarchar](150) NOT NULL,
	[PasswordHash] [nvarchar](250) NOT NULL,
	[RoleId] [uniqueidentifier] NULL,
	[City] [nvarchar](20) NOT NULL,
	[Country] [nvarchar](20) NOT NULL,
	[Phone] [nvarchar](20) NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[UpdatedAt] [datetime2](7) NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[IsActive] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UQ_Users_Email] UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Users] ADD  DEFAULT (newid()) FOR [UserId]
GO

ALTER TABLE [dbo].[Users] ADD  DEFAULT ('') FOR [City]
GO

ALTER TABLE [dbo].[Users] ADD  DEFAULT ('') FOR [Country]
GO

ALTER TABLE [dbo].[Users] ADD  DEFAULT ('') FOR [Phone]
GO

ALTER TABLE [dbo].[Users] ADD  DEFAULT (sysutcdatetime()) FOR [CreatedAt]
GO

ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO

ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO

ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_Roles] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Roles] ([RoleId])
GO

ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_Roles]
GO



/****** Object:  Table [dbo].[Employers]    Script Date: 9/1/2025 5:05:01 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Employers](
	[EmployerId] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[IndustryId] [int] NOT NULL,
	[CompanyName] [nvarchar](150) NOT NULL,
	[WebsiteUrl] [nvarchar](150) NULL,
	[CompanySize] [nvarchar](150) NULL,
	[ContactPerson] [nvarchar](150) NOT NULL,
	[RegistrationNumber] [nvarchar](150) NULL,
 CONSTRAINT [PK_Employers] PRIMARY KEY CLUSTERED 
(
	[EmployerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Employers] ADD  DEFAULT (newid()) FOR [EmployerId]
GO

ALTER TABLE [dbo].[Employers]  WITH CHECK ADD  CONSTRAINT [FK_Employers_Industries] FOREIGN KEY([IndustryId])
REFERENCES [dbo].[Industries] ([IndustryId])
GO

ALTER TABLE [dbo].[Employers] CHECK CONSTRAINT [FK_Employers_Industries]
GO

ALTER TABLE [dbo].[Employers]  WITH CHECK ADD  CONSTRAINT [FK_Employers_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Employers] CHECK CONSTRAINT [FK_Employers_Users]
GO


/****** Object:  Table [dbo].[Roles]    Script Date: 9/1/2025 5:05:40 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Roles](
	[RoleId] [uniqueidentifier] NOT NULL,
	[RoleName] [nvarchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[RoleName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


/****** Object:  Table [dbo].[Skills]    Script Date: 9/1/2025 5:06:02 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Skills](
	[SkillId] [int] IDENTITY(1,1) NOT NULL,
	[SkillName] [nvarchar](250) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[SkillId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[SkillName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO




/****** Object:  Table [dbo].[Industries]    Script Date: 9/1/2025 5:06:22 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Industries](
	[IndustryId] [int] IDENTITY(1,1) NOT NULL,
	[IndustryName] [nvarchar](250) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IndustryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[IndustryName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


/****** Object:  StoredProcedure [dbo].[DeleteEmployer]    Script Date: 9/1/2025 5:06:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================


CREATE OR ALTER PROCEDURE [dbo].[DeleteEmployer]
    @UserId UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    Update  [dbo].[Users] set IsDeleted = 1  WHERE UserId = @UserId AND IsDeleted = 0;
    
    SELECT @@ROWCOUNT AS RowsAffected;
END



/****** Object:  StoredProcedure [dbo].[GetAllEmployers]    Script Date: 9/3/2025 12:44:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================

ALTER PROCEDURE [dbo].[GetAllEmployers]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        emp.EmployerId,
        emp.CompanyName,
        emp.WebsiteUrl,
        emp.CompanySize,
        emp.ContactPerson,
        emp.RegistrationNumber,

        usr.UserId,
        usr.UserName,
        usr.Email,
        usr.City,
        usr.Country,
        usr.Phone,
        usr.IsActive,

        rl.RoleId,
        rl.RoleName,

        ind.IndustryId,
        ind.IndustryName
    FROM [dbo].[Users] usr
    INNER JOIN [dbo].[Employers] emp ON usr.UserId = emp.UserId
    INNER JOIN [dbo].[Roles] rl ON usr.RoleId = rl.RoleId
    INNER JOIN [dbo].[Industries] ind ON emp.IndustryId = ind.IndustryId
    WHERE usr.IsDeleted = 0;
END


/****** Object:  StoredProcedure [dbo].[GetAllRoles]    Script Date: 9/1/2025 5:07:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE OR ALTER PROCEDURE [dbo].[GetAllRoles] 
	-- Add the parameters for the stored procedure here
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM Roles;
END


/****** Object:  StoredProcedure [dbo].[GetAllUsers]    Script Date: 9/1/2025 5:08:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE OR ALTER PROCEDURE [dbo].[GetAllUsers] 
	-- Add the parameters for the stored procedure here
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT usrs.UserId,usrs.Email,usrs.UserName,rls.RoleId,rls.RoleName FROM Users usrs LEFT JOIN Roles rls
	on usrs.RoleId = rls.RoleId WHERE usrs.IsDeleted = 0;
END


/****** Object:  StoredProcedure [dbo].[GetEmployerById]    Script Date: 9/3/2025 12:42:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================

ALTER PROCEDURE [dbo].[GetEmployerById]
    @EmployerId UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        emp.EmployerId,
        emp.CompanyName,
        emp.WebsiteUrl,
        emp.CompanySize,
        emp.ContactPerson,
        emp.RegistrationNumber,

        usr.UserId,
        usr.UserName,
        usr.Email,
        usr.City,
        usr.Country,
        usr.Phone,
        usr.IsActive,

        rl.RoleId,
        rl.RoleName,

        ind.IndustryId,
        ind.IndustryName
    FROM [dbo].[Employers] emp
    INNER JOIN [dbo].[Users] usr ON emp.UserId = usr.UserId
    INNER JOIN [dbo].[Roles] rl ON usr.RoleId = rl.RoleId
    INNER JOIN [dbo].[Industries] ind ON emp.IndustryId = ind.IndustryId
    WHERE emp.EmployerId = @EmployerId AND usr.IsDeleted = 0;
END


/****** Object:  StoredProcedure [dbo].[GetIndustries]    Script Date: 9/1/2025 5:09:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE OR ALTER PROCEDURE [dbo].[GetIndustries]
	-- Add the parameters for the stored procedure here
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM Industries;
END


/****** Object:  StoredProcedure [dbo].[GetSkills]    Script Date: 9/1/2025 5:09:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE OR ALTER PROCEDURE [dbo].[GetSkills]
	-- Add the parameters for the stored procedure here
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM Skills;
END


/****** Object:  StoredProcedure [dbo].[GetUserPasswordHash]    Script Date: 9/1/2025 5:09:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE OR ALTER PROCEDURE [dbo].[GetUserPasswordHash]
	-- Add the parameters for the stored procedure here
	@UserId UNIQUEIDENTIFIER
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT PasswordHash FROM Users WHERE UserId = @UserId;
END


/****** Object:  StoredProcedure [dbo].[InsertEmployer]    Script Date: 9/1/2025 5:10:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:    YourName
-- Create date: 2025-08-26
-- Description: Insert Employer and return EmployerId
-- =============================================
CREATE OR ALTER PROCEDURE [dbo].[InsertEmployer]
    @UserId UNIQUEIDENTIFIER,
    @IndustryId INT,
    @CompanyName NVARCHAR(200),
    @WebsiteUrl NVARCHAR(200) = NULL,
    @CompanySize NVARCHAR(50) = NULL,
    @ContactPerson NVARCHAR(150),
    @RegistrationNumber NVARCHAR(100) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @InsertedIds TABLE (EmployerId UNIQUEIDENTIFIER);
    --DECLARE @NewEmployerId UNIQUEIDENTIFIER = NEWID();

    INSERT INTO [dbo].[Employers] (
        UserId,
        IndustryId,
        CompanyName,
        WebsiteUrl,
        CompanySize,
        ContactPerson,
        RegistrationNumber
    )
    OUTPUT Inserted.EmployerId INTO @InsertedIds
    VALUES (
        @UserId,
        @IndustryId,
        @CompanyName,
        @WebsiteUrl,
        @CompanySize,
        @ContactPerson,
        @RegistrationNumber
    );

    -- Return the new EmployerId
   SELECT EmployerId FROM @InsertedIds;
END


/****** Object:  StoredProcedure [dbo].[InsertIndustry]    Script Date: 9/1/2025 5:10:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE OR ALTER PROCEDURE [dbo].[InsertIndustry]
	-- Add the parameters for the stored procedure here
	@IndustryName nvarchar(250)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO Industries(IndustryName) Values(@IndustryName);
END


/****** Object:  StoredProcedure [dbo].[InsertRole]    Script Date: 9/1/2025 5:12:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE OR ALTER PROCEDURE [dbo].[InsertRole]
	-- Add the parameters for the stored procedure here
	@RoleId UniqueIdentifier,
	@RoleName nvarchar(100)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO Roles (RoleID,RoleName) VALUES(@RoleId,@RoleName);
END


/****** Object:  StoredProcedure [dbo].[InsertSkill]    Script Date: 9/1/2025 5:13:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE OR  ALTER PROCEDURE [dbo].[InsertSkill]
	-- Add the parameters for the stored procedure here
	@SkillName nvarchar(250)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO Skills (SkillName) Values(@SkillName);
END


/****** Object:  StoredProcedure [dbo].[InsertUser]    Script Date: 9/1/2025 5:13:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE OR ALTER PROCEDURE [dbo].[InsertUser]
	-- Add the parameters for the stored procedure here
	@UserName nvarchar(150),
	@Email nvarchar(150),
	@PasswordHash nvarchar(150),
	@RoleId UniqueIdentifier,
	@City nvarchar(20),
	@Country nvarchar(20),
	@Phone nvarchar(20),
	@CreatedAt DateTime2
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	DECLARE @InsertedIds TABLE (UserId UNIQUEIDENTIFIER);
	--DECLARE @NewUserId UNIQUEIDENTIFIER = NEWID();

    -- Insert statements for procedure here
	INSERT INTO Users (UserName,Email,PasswordHash,RoleId, City, Country, Phone, CreatedAt) 
	OUTPUT Inserted.UserId INTO @InsertedIds
	VALUES(@UserName,@Email,@PasswordHash,@RoleId,@City,@Country,@Phone,@CreatedAt);

	SELECT UserId FROM @InsertedIds;
	--SELECT @NewUserId AS UserId;
END


/****** Object:  StoredProcedure [dbo].[UpdateEmployer]    Script Date: 9/3/2025 12:45:57 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================

ALTER PROCEDURE [dbo].[UpdateEmployer]

    @EmployerId UNIQUEIDENTIFIER,
    @CompanyName NVARCHAR(200),
    @WebsiteUrl NVARCHAR(200),
    @CompanySize NVARCHAR(50),
    @ContactPerson NVARCHAR(100),
    @RegistrationNumber NVARCHAR(50),
    @IndustryId INT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Employers
    SET CompanyName = @CompanyName,
        WebsiteUrl = @WebsiteUrl,
        CompanySize = @CompanySize,
        ContactPerson = @ContactPerson,
        RegistrationNumber = @RegistrationNumber,
        IndustryId = @IndustryId
    WHERE EmployerId = @EmployerId;

    IF(@@ROWCOUNT = 1)
    BEGIN
        SELECT 'SUCCESS' as Status;
    END
END


/****** Object:  StoredProcedure [dbo].[UpdateEmployerStatus]    Script Date: 9/1/2025 5:14:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================

CREATE OR ALTER PROCEDURE [dbo].[UpdateEmployerStatus]
    @UserId UNIQUEIDENTIFIER,
    @IsActive BIT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[Users]
    SET IsActive = @IsActive,
        UpdatedAt = SYSUTCDATETIME(),
        UpdatedBy = @UserId
    WHERE UserId = @UserId AND IsDeleted = 0;

    -- Return the updated status
    SELECT IsActive 
    FROM [dbo].[Users] 
    WHERE UserId = @UserId AND IsDeleted = 0;
END


/****** Object:  StoredProcedure [dbo].[UpdateUser]    Script Date: 9/3/2025 12:47:31 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================

CREATE PROCEDURE [dbo].[UpdateUser]

    @UserId UNIQUEIDENTIFIER,
    @UserName NVARCHAR(100),
    @Email NVARCHAR(150),
    @PasswordHash NVARCHAR(150),
    @City NVARCHAR(50),
    @Country NVARCHAR(50),
    @Phone NVARCHAR(20)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Users
    SET UserName = @UserName,
        Email = @Email,
        PasswordHash = @PasswordHash,
        City = @City,
        Country = @Country,
        Phone = @Phone,
        UpdatedAt = SYSUTCDATETIME(),
        UpdatedBy = @UserId
    WHERE UserId = @UserId;

    IF(@@ROWCOUNT = 1)
    BEGIN
        SELECT 'SUCCESS' as Status;
    END
END




/****** Object:  StoredProcedure [dbo].[DeleteUser]    Script Date: 9/3/2025 12:04:31 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================


CREATE PROCEDURE [dbo].[DeleteUser]
    @UserId UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    Update  [dbo].[Users] set IsDeleted = 1  WHERE UserId = @UserId AND IsDeleted = 0;
    
    SELECT @@ROWCOUNT AS RowsAffected;
END


/****** Object:  StoredProcedure [dbo].[UpdateUserStatus]    Script Date: 9/3/2025 12:50:11 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================

CREATE PROCEDURE [dbo].[UpdateUserStatus]
    @UserId UNIQUEIDENTIFIER,
    @IsActive BIT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[Users]
    SET IsActive = @IsActive,
        UpdatedAt = SYSUTCDATETIME(),
        UpdatedBy = @UserId
    WHERE UserId = @UserId AND IsDeleted = 0;

    -- Return the updated status
    SELECT IsActive 
    FROM [dbo].[Users] 
    WHERE UserId = @UserId AND IsDeleted = 0;
END


