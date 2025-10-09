

USE WorkFinderDb;

CREATE TABLE Applicants (
    ApplicantId UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWSEQUENTIALID(),
	UserId UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT FK_Applicants_Users FOREIGN KEY (UserId) REFERENCES Users(UserId),
    [Resume] NVARCHAR(300) NULL
);

USE [WorkFinderDb]
GO
/****** Object:  StoredProcedure [dbo].[InsertApplicant]    Script Date: 8/28/2025 5:10:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[InsertApplicant]
	-- Add the parameters for the stored procedure here
	@UserId UNIQUEIDENTIFIER,
	@Resume NVARCHAR(300)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO Applicants (UserId,[Resume]) VALUES (@UserId,@Resume);

	SELECT ApplicantId FROM Applicants WHERE UserId = @UserId;
END
