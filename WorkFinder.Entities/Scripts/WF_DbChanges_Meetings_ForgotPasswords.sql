CREATE TABLE PasswordResetRequests (
    Id INT PRIMARY KEY IDENTITY(1,1),
    UserId UNIQUEIDENTIFIER NOT NULL,
    Token NVARCHAR(255) NOT NULL,
    ExpiresAt DATETIME NOT NULL,
    Used BIT NOT NULL DEFAULT 0,

    CONSTRAINT FK_PasswordReset_User
        FOREIGN KEY (UserId) REFERENCES Users(UserId)
        ON DELETE CASCADE
);


USE [WorkFinderDb]
GO
/****** Object:  StoredProcedure [dbo].[CreatePasswordResetRequest]    Script Date: 11/24/2025 8:54:44 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[CreatePasswordResetRequest]
	-- Add the parameters for the stored procedure here
	@UserId UNIQUEIDENTIFIER,
	@Token NVARCHAR(100),
	@ExpiresAt DateTime,
	@Used BIT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO PasswordResetRequests(UserId, Token, ExpiresAt, Used) VALUES (@UserId, @Token, @ExpiresAt, @Used);
END
GO

USE [WorkFinderDb]
GO
/****** Object:  StoredProcedure [dbo].[MarkAsUsed]    Script Date: 11/24/2025 8:55:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[MarkAsUsed]
	-- Add the parameters for the stored procedure here
	@Id INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE PasswordResetRequests SET Used = 1
	WHERE Id = @Id;
END
GO

USE [WorkFinderDb]
GO
/****** Object:  StoredProcedure [dbo].[IsValidToken]    Script Date: 11/24/2025 8:55:33 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[IsValidToken]
	-- Add the parameters for the stored procedure here
	@Token NVARCHAR(200)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM PasswordResetRequests
	WHERE [Token] = @Token
END
GO

USE [WorkFinderDb]
GO
/****** Object:  StoredProcedure [dbo].[UpdateUserPassword]    Script Date: 11/24/2025 8:55:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[UpdateUserPassword]
	-- Add the parameters for the stored procedure here
	@Password NVARCHAR(200),
	@UserId UNIQUEIDENTIFIER
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE Users
	SET [PasswordHash] = @Password
	WHERE UserId = @UserId
END
GO

USE [WorkFinderDb]
GO
/****** Object:  StoredProcedure [dbo].[UnsaveJob]    Script Date: 11/24/2025 8:56:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[UnsaveJob] 
	-- Add the parameters for the stored procedure here
	@ApplicantId UNIQUEIDENTIFIER,
	@JobId INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	DELETE FROM SavedJobs WHERE JobId = @JobId AND ApplicantId = @ApplicantId
END
