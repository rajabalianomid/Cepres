Create PROCEDURE [dbo].[sp_Delete_Patient]
    @Id int
AS
BEGIN

	DELETE FROM [dbo].[Patient] WHERE [Id]=@Id

END
GO

Create PROCEDURE [dbo].[sp_Delete_PatientMetaData]
    @Id int
AS
BEGIN

	DELETE FROM [dbo].[PatientMetaData] WHERE [Id]=@Id

END
GO

CREATE PROCEDURE [dbo].[sp_GetAll_Patient]
    @PageSize int
   ,@PageIndex int
   ,@Sort nvarchar(200)
AS
BEGIN


	DECLARE @sqlCommand varchar(max)

	SET @sqlCommand=
		'SELECT'+
		'   P.[Id]'+
		'  ,P.[Name]'+
		'  ,P.[DateOfBirth]'+
		'  ,COUNT(PMD.PatientId) AS MetaDataCount'+
		' FROM [dbo].[Patient] P'+
		' LEFT JOIN [dbo].[PatientMetaData] PMD ON P.Id=PMD.PatientId'+
		' GROUP BY P.[Id],P.[Name],P.DateOfBirth'+
		' ORDER BY '+ CAST(@Sort as nvarchar(20))+
		' OFFSET '+ CAST(@PageSize as nvarchar(20)) +' * ('+ CAST(@PageIndex as nvarchar(20)) +') ROWS'+
		' FETCH NEXT '+ CAST(@PageSize as nvarchar(20)) +' ROWS ONLY OPTION (RECOMPILE);'

	EXEC (@sqlCommand)

END
GO

CREATE PROCEDURE [dbo].[sp_GetAll_Patient_ByPatientId]
    @PatientId int
AS
BEGIN
	SELECT [Id]
      ,[Key]
      ,[Value]
      ,[CreateUtc]
      ,[PatientId]
	FROM [dbo].[PatientMetaData]
	WHERE PatientId = @PatientId

END
GO

CREATE PROCEDURE [dbo].[sp_Insert_Patient]
	@Id int
   ,@OfficialId int
   ,@Name nvarchar(200)
   ,@DateOfBirth datetime2(7)
   ,@Email nvarchar(max)
   ,@CreateUtc datetime2(7)
AS
BEGIN

	DECLARE @ResultTable TABLE (Id int)

	INSERT INTO [dbo].[Patient]
           ([OfficialId]
           ,[Name]
           ,[DateOfBirth]
           ,[Email]
           ,[CreateUtc])
		   OUTPUT Inserted.ID as Id INTO @ResultTable
     VALUES
           (@OfficialId
           ,@Name
           ,@DateOfBirth
           ,@Email
           ,@CreateUtc)

	SELECT [Id]
      ,[OfficialId]
      ,[Name]
      ,[DateOfBirth]
      ,[Email]
      ,[CreateUtc]
	FROM [dbo].[Patient]
	WHERE Id IN (SELECT Id FROM @ResultTable)

END
GO

CREATE PROCEDURE [dbo].[sp_Insert_PatientMetaData]
    @Id int
   ,@Key nvarchar(200)
   ,@Value nvarchar(200)
   ,@CreateUtc datetime2(7)
   ,@PatientId int
AS
BEGIN

	DECLARE @ResultTable TABLE (Id int)

	INSERT INTO [dbo].[PatientMetaData]
           ([Key]
           ,[Value]
           ,[CreateUtc]
		   ,[PatientId])
		   OUTPUT Inserted.ID as Id INTO @ResultTable
     VALUES
           (@Key
           ,@Value
           ,@CreateUtc
           ,@PatientId)

	SELECT [Id]
      ,[Key]
      ,[Value]
      ,[CreateUtc]
      ,[PatientId]
	FROM [dbo].[PatientMetaData]
	WHERE Id IN (SELECT Id FROM @ResultTable)

END
GO

CREATE PROCEDURE [dbo].[sp_Update_Patient]
	@Id int
   ,@OfficialId int
   ,@Name nvarchar(200)
   ,@DateOfBirth datetime2(7)
   ,@Email nvarchar(max)
AS
BEGIN

	DECLARE @ResultTable TABLE (Id int)

	UPDATE [dbo].[Patient]
    SET [OfficialId] = @OfficialId
      ,[Name] = @Name
      ,[DateOfBirth] = @DateOfBirth
      ,[Email] = @Email
	WHERE Id = @Id

	SELECT [Id]
      ,[OfficialId]
      ,[Name]
      ,[DateOfBirth]
      ,[Email]
      ,[CreateUtc]
	FROM [dbo].[Patient]
	WHERE Id = @Id

END
GO
