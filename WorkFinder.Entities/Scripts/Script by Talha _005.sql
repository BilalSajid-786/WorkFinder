USE [WorkFinderDb]
GO

/****** Object:  Table [dbo].[Users]    Script Date: 8/27/2025 9:34:13 PM ******/
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



/****** Object:  Table [dbo].[Industries]    Script Date: 8/27/2025 5:49:58 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Industries](
	[IndustryId] [uniqueidentifier] NOT NULL,
	[IndustryName] [nvarchar](200) NOT NULL,
 CONSTRAINT [PK_Industries] PRIMARY KEY CLUSTERED 
(
	[IndustryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Industries] ADD  CONSTRAINT [DF_Industries_IndustryId]  DEFAULT (newid()) FOR [IndustryId]
GO


INSERT INTO [dbo].[Industries] (IndustryId, IndustryName)
VALUES 
    (NEWID(), 'Services'),
    (NEWID(), 'Finance / Insurance / Real Estate'),
    (NEWID(), 'Construction'),
    (NEWID(), 'Retail & Wholesale'),
    (NEWID(), 'Manufacturing'),
    (NEWID(), 'Automotive'),
    (NEWID(), 'Mechanical Engineering'),
    (NEWID(), 'Chemical & Pharmaceutical'),
    (NEWID(), 'Electronics & Electrical Engineering'),
    (NEWID(), 'ICT / Software & Telecommunications'),
    (NEWID(), 'Renewable Energy / Environmental Technology'),
    (NEWID(), 'Aerospace'),
    (NEWID(), 'Healthcare / Medical Technology'),
    (NEWID(), 'Food & Beverage / Agriculture'),
    (NEWID(), 'Logistics & Transportation'),
    (NEWID(), 'Metals / Mining');


/****** Object:  Table [dbo].[Employers]    Script Date: 8/27/2025 5:52:29 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Employers](
	[EmployerId] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[IndustryId] [uniqueidentifier] NOT NULL,
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




/****** Object:  StoredProcedure [dbo].[InsertUser]    Script Date: 8/27/2025 9:36:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[InsertUser]
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



/****** Object:  StoredProcedure [dbo].[InsertEmployer]    Script Date: 8/27/2025 5:54:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:    YourName
-- Create date: 2025-08-26
-- Description: Insert Employer and return EmployerId
-- =============================================
CREATE PROCEDURE [dbo].[InsertEmployer]
    @UserId UNIQUEIDENTIFIER,
    @IndustryId UNIQUEIDENTIFIER,
    @CompanyName NVARCHAR(200),
    @WebsiteUrl NVARCHAR(200) = NULL,
    @CompanySize NVARCHAR(50) = NULL,
    @ContactPerson NVARCHAR(150),
    @RegistrationNumber NVARCHAR(100) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @NewEmployerId UNIQUEIDENTIFIER = NEWID();

    INSERT INTO [dbo].[Employers] (
        EmployerId,
        UserId,
        IndustryId,
        CompanyName,
        WebsiteUrl,
        CompanySize,
        ContactPerson,
        RegistrationNumber
    )
    VALUES (
        @NewEmployerId,
        @UserId,
        @IndustryId,
        @CompanyName,
        @WebsiteUrl,
        @CompanySize,
        @ContactPerson,
        @RegistrationNumber
    );

    -- Return the new EmployerId
    SELECT @NewEmployerId;
END


/****** Object:  StoredProcedure [dbo].[GetAllEmployers]    Script Date: 8/28/2025 2:54:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetAllEmployers]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        usr.UserId,
        usr.UserName,
        usr.Email,
        usr.City,
        usr.Country,
        usr.Phone,
        usr.IsActive,

        emp.EmployerId,
        emp.CompanyName,
        emp.WebsiteUrl,
        emp.CompanySize,
        emp.ContactPerson,
        emp.RegistrationNumber,

        rl.RoleId,
        rl.RoleName,

        ind.IndustryId,
        ind.IndustryName
    FROM [dbo].[Users] usr
    INNER JOIN [dbo].[Employers] emp ON usr.UserId = emp.UserId
    INNER JOIN [dbo].[Roles] rl ON usr.RoleId = rl.RoleId
    INNER JOIN [dbo].[Industries] ind ON emp.IndustryId = ind.IndustryId;
END

