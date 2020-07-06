
/****** Object:  StoredProcedure [dbo].[sp_Delete_Patient]    Script Date: 7/6/2020 4:28:56 PM ******/
Create PROCEDURE [dbo].[sp_Delete_Patient]
    @Id int
AS
BEGIN

	DELETE FROM [dbo].[Patient] WHERE [Id]=@Id

END
GO
/****** Object:  StoredProcedure [dbo].[sp_Delete_PatientMetaData]    Script Date: 7/6/2020 4:28:56 PM ******/
Create PROCEDURE [dbo].[sp_Delete_PatientMetaData]
    @Id int
AS
BEGIN

	DELETE FROM [dbo].[PatientMetaData] WHERE [Id]=@Id

END
GO
/****** Object:  StoredProcedure [dbo].[sp_Delete_Record]    Script Date: 7/6/2020 4:28:56 PM ******/
Create PROCEDURE [dbo].[sp_Delete_Record]
    @Id int
AS
BEGIN

	DELETE FROM [dbo].[Record] WHERE [Id]=@Id

END
GO
/****** Object:  StoredProcedure [dbo].[sp_Get_Similar_Patient]    Script Date: 7/6/2020 4:28:56 PM ******/
CREATE PROCEDURE [dbo].[sp_Get_Similar_Patient]
	 @PatientId int
AS
BEGIN

SELECT P.Id,P.[Name]
FROM Patient P
INNER JOIN [dbo].[Record] R ON P.Id=R.PatientId
WHERE P.Id<>@PatientId AND R.DiseaseName IN 
(SELECT [DiseaseName] FROM Record
	GROUP BY PatientId,[DiseaseName]
	HAVING PatientId=@PatientId)
GROUP BY P.Id,P.[Name]
HAVING COUNT(P.Id)>=2

END
GO
/****** Object:  StoredProcedure [dbo].[sp_GetAll_Patient]    Script Date: 7/6/2020 4:28:56 PM ******/
CREATE PROCEDURE [dbo].[sp_GetAll_Patient]
    @PageSize int
   ,@PageIndex int
   ,@Sort nvarchar(200)
AS
BEGIN


	DECLARE @sqlCommand varchar(max)

	SET @sqlCommand=
		' SELECT Q1.[Id],Q1.[Name],Q1.[DateOfBirth],Q1.[TimeOfEntry] AS LastTimeOfEntry,COUNT(PMD.PatientId) AS MetaDataCount FROM
			(SELECT
			   P.[Id]
			  ,P.[Name]
			  ,P.[DateOfBirth]
			  ,R.[TimeOfEntry]
			  ,ROW_NUMBER() OVER (Partition BY P.Id ORDER BY R.TimeOfEntry DESC) AS RowNum
			 FROM [dbo].[Patient] P
			 LEFT JOIN [dbo].[Record] R ON R.PatientId=P.Id)Q1
			 LEFT JOIN [dbo].[PatientMetaData] PMD ON Q1.Id=PMD.PatientId
			 WHERE Q1.RowNum=1
			 GROUP BY Q1.[Id],Q1.[Name],Q1.DateOfBirth,Q1.[TimeOfEntry]'+
		' ORDER BY '+ CAST(@Sort as nvarchar(20))+
		' OFFSET '+ CAST(@PageSize as nvarchar(20)) +' * ('+ CAST(@PageIndex as nvarchar(20)) +') ROWS'+
		' FETCH NEXT '+ CAST(@PageSize as nvarchar(20)) +' ROWS ONLY OPTION (RECOMPILE);'

	EXEC (@sqlCommand)

END
GO
/****** Object:  StoredProcedure [dbo].[sp_GetAll_Patient_ByPatientId]    Script Date: 7/6/2020 4:28:56 PM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_GetAll_Record]    Script Date: 7/6/2020 4:28:56 PM ******/
CREATE PROCEDURE [dbo].[sp_GetAll_Record]
    @PageSize int
   ,@PageIndex int
   ,@Sort nvarchar(200)
AS
BEGIN


	DECLARE @sqlCommand varchar(max)

	SET @sqlCommand=
		'SELECT'+
		'   R.[Id]'+
		'  ,R.[DiseaseName]'+
		'  ,R.[TimeOfEntry]'+
		'  ,P.[Name]'+
		'  ,P.[Id] AS PatientId'+
		' FROM [dbo].[Record] R'+
		' LEFT JOIN [dbo].[Patient] P ON R.PatientId=P.Id'+
		' ORDER BY '+ CAST(@Sort as nvarchar(20))+
		' OFFSET '+ CAST(@PageSize as nvarchar(20)) +' * ('+ CAST(@PageIndex as nvarchar(20)) +') ROWS'+
		' FETCH NEXT '+ CAST(@PageSize as nvarchar(20)) +' ROWS ONLY OPTION (RECOMPILE);'

	EXEC (@sqlCommand)

END
GO
/****** Object:  StoredProcedure [dbo].[sp_GetAll_Record_ByRecordId]    Script Date: 7/6/2020 4:28:56 PM ******/
Create PROCEDURE [dbo].[sp_GetAll_Record_ByRecordId]
    @Id int
AS
BEGIN
	SELECT [Id]
      ,[CreateUtc]
      ,[PatientId]
      ,[DiseaseName]
      ,[TimeOfEntry]
      ,[Description]
      ,[Bill]
    FROM [dbo].[Record]
	WHERE Id = @Id

END
GO
/****** Object:  StoredProcedure [dbo].[sp_Insert_Patient]    Script Date: 7/6/2020 4:28:56 PM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_Insert_PatientMetaData]    Script Date: 7/6/2020 4:28:56 PM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_Insert_Record]    Script Date: 7/6/2020 4:28:56 PM ******/
Create PROCEDURE [dbo].[sp_Insert_Record]
	@Id int
   ,@PatientId int
   ,@DiseaseName nvarchar(200)
   ,@TimeOfEntry datetime2(7)
   ,@Description nvarchar(max)
   ,@CreateUtc datetime2(7)
   ,@Bill decimal(18,2)
AS
BEGIN

	DECLARE @ResultTable TABLE (Id int)

	INSERT INTO [dbo].[Record]
           ([CreateUtc]
           ,[PatientId]
           ,[DiseaseName]
           ,[TimeOfEntry]
           ,[Description]
           ,[Bill])
		   OUTPUT Inserted.ID as Id INTO @ResultTable
     VALUES
           (@CreateUtc
           ,@PatientId
           ,@DiseaseName
           ,@TimeOfEntry
		   ,@Description
           ,@Bill)

	SELECT [Id]
      ,[CreateUtc]
      ,[PatientId]
      ,[DiseaseName]
      ,[TimeOfEntry]
      ,[Description]
      ,[Bill]
    FROM [dbo].[Record]
	WHERE Id IN (SELECT Id FROM @ResultTable)

END
GO
/****** Object:  StoredProcedure [dbo].[sp_Report_MetaData]    Script Date: 7/6/2020 4:28:56 PM ******/
CREATE PROCEDURE [dbo].[sp_Report_MetaData]

AS
BEGIN

	SELECT [Key],[Value] FROM
	(SELECT CAST(AVG(Q1.MDCount)as nvarchar(100)) AS System_Generate_Avrage,CAST(MAX(Q1.MDCount) as nvarchar(100)) System_Generate_MaxCount FROM
		(SELECT P.Id,COUNT(P.Id) MDCount FROM [dbo].[Patient] P
		INNER JOIN [dbo].[PatientMetaData] PMD
		ON P.Id=PMD.PatientId
		GROUP BY P.Id,P.[Name]) AS Q1) Q2
		UNPIVOT
		(
			[Value]
			FOR [Key] in (System_Generate_Avrage, System_Generate_MaxCount)
		) AS SchoolUnpivot
	UNION
	SELECT PMD.[Key],CAST(COUNT(PMD.[Key]) as nvarchar(100)) [Value] FROM [dbo].[PatientMetaData] PMD
	GROUP BY PMD.[Key]

END
GO
/****** Object:  StoredProcedure [dbo].[sp_Report_Patient]    Script Date: 7/6/2020 4:28:56 PM ******/
CREATE PROCEDURE [dbo].[sp_Report_Patient]

AS
BEGIN

	SELECT *,
	[OutliersStandardDiviations] = CASE WHEN Q1.Avrage>Q1.StandardDiviations+2 OR Q1.Avrage<Q1.StandardDiviations-2 THEN NULL ELSE CAST(Q1.StandardDiviations as float) END
	FROM (SELECT R.PatientId
			,CAST(AVG(R.Bill) as float)AS Avrage
			,CAST(STDEV(R.Bill)as float)AS StandardDiviations 
		  FROM [dbo].[Record] R
	GROUP BY R.PatientId) Q1
	INNER JOIN 
	(SELECT
		P.[Id]
	   ,R1.Bill
	   ,R1.[Id] AS RecordId
	   ,P.[Name]
	   ,R1.[DiseaseName]
	   ,CAST(DATEDIFF(YY,P.[DateOfBirth],GETDATE())as int) AS Age
	   ,ROW_NUMBER() OVER (Partition BY P.Id ORDER BY R1.TimeOfEntry DESC) AS RowNum
	   ,(SELECT TOP 1 DATENAME(month, R3.TimeOfEntry) as x FROM [dbo].[Record] R3 
			WHERE R3.PatientId=P.Id
			GROUP BY DATENAME(month, R3.TimeOfEntry)
			ORDER BY COUNT(DATENAME(month, R3.TimeOfEntry)) DESC) AS MaxVisitMonth
	FROM [dbo].[Patient] P
	INNER JOIN [dbo].[Record] R1 ON P.Id=R1.PatientId) Q2
	ON Q1.PatientId=Q2.Id
	WHERE Q2.RowNum=1 

END
GO
/****** Object:  StoredProcedure [dbo].[sp_Update_Patient]    Script Date: 7/6/2020 4:28:56 PM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_Update_Record]    Script Date: 7/6/2020 4:28:56 PM ******/
CREATE PROCEDURE [dbo].[sp_Update_Record]
	@Id int
   ,@DiseaseName nvarchar(200)
   ,@Description nvarchar(max)
   ,@TimeOfEntry datetime2(7)
   ,@Bill decimal(18,2)
   ,@PatientId int
AS
BEGIN

	UPDATE [dbo].[Record]
    SET [DiseaseName] = @DiseaseName
      ,[TimeOfEntry] = @TimeOfEntry
      ,[Description] = @Description
      ,[Bill] = @Bill
	  ,[PatientId]= @PatientId
	WHERE Id = @Id

	SELECT [Id]
      ,[CreateUtc]
      ,[PatientId]
      ,[DiseaseName]
      ,[TimeOfEntry]
      ,[Description]
      ,[Bill]
    FROM [dbo].[Record]
	WHERE Id = @Id

END
GO
