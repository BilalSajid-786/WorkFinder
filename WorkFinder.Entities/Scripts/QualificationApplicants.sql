---- Step 1: Add the column (if not already added)
--ALTER TABLE Applicants
--ADD QualificationId INT;


---- Step 2: Add the foreign key constraint
--ALTER TABLE Applicants
--ADD CONSTRAINT FK_Applicants_Qualification
--FOREIGN KEY (QualificationId)
--REFERENCES Qualifications(QualificationId);


--USE [WorkFinderDb]
--GO
--/****** Object:  StoredProcedure [dbo].[InsertApplicant]    Script Date: 10/22/2025 10:38:46 PM ******/
--SET ANSI_NULLS ON
--GO
--SET QUOTED_IDENTIFIER ON
--GO
---- =============================================
---- Author:		<Author,,Name>
---- Create date: <Create Date,,>
---- Description:	<Description,,>
---- =============================================
--ALTER PROCEDURE [dbo].[InsertApplicant]
--	-- Add the parameters for the stored procedure here
--	@UserId UNIQUEIDENTIFIER,
--	@Resume NVARCHAR(300),
--	@Gender NVARCHAR(30),
--	@QualificationId INT
--AS
--BEGIN
--	-- SET NOCOUNT ON added to prevent extra result sets from
--	-- interfering with SELECT statements.
--	SET NOCOUNT ON;

--    -- Insert statements for procedure here
--	INSERT INTO Applicants (UserId,[Resume],[Gender],[QualificationId]) VALUES (@UserId,@Resume,@Gender,@QualificationId);

--	SELECT ApplicantId FROM Applicants WHERE UserId = @UserId;
--END
