ALTER PROCEDURE [savefile] (@filePath VARCHAR(MAX))
AS
BEGIN
    /*CREATE TABLE Temp
    (
      IDgroup INT, descGroup NVARCHAR(20),IDmachine NVARCHAR(20), decsMachine NVARCHAR(20), accuracyMachine FLOAT,
	  [time] DATETIME2, IDpoint INT, descPoint NVARCHAR(20), X FLOAT, Y FLOAT, pointValue1 FLOAT,pointValue2 FLOAT,
      variableName NVARCHAR(20), 
    );*/
	
	CREATE TABLE Temp
	( -- in na datetime
		[time] BIGINT, variableName NVARCHAR(20),IDpoint INT,X FLOAT,Y FLOAT,descPoint NVARCHAR(20), pointValue1 FLOAT,pointValue2 FLOAT,
		 accuracyPoint FLOAT,IDmachine NVARCHAR(20),machineAccuracy FLOAT,decsMachine NVARCHAR(20),
		IDgroup INT, descGroup NVARCHAR(20)

	);
    DECLARE @SQL NVARCHAR(MAX) = ''
    SET @SQL = '
    BULK INSERT Temp
      FROM ''' + @filePath + '''
      WITH (
        FIELDTERMINATOR = '';'',
        ROWTERMINATOR = ''\n''
      )'

EXEC sp_executesql @SQL;
-- predelat time z intu na datetime to same plati i u temporaly table
DECLARE @IDgroup BIGINT, @descGroup NVARCHAR(20),@IDmachine NVARCHAR(20), @decsMachine NVARCHAR(20), @accuracyPoint FLOAT,
@time2 BIGINT, @IDpoint INT, @descPoint NVARCHAR(20), @X FLOAT, @Y FLOAT, @pointValue1 FLOAT,@pointValue2 FLOAT,
@variableName NVARCHAR(20),@machineAccuracy2 FLOAT;


DECLARE contact_cursor CURSOR FOR SELECT IDgroup, descGroup,IDmachine,machineAccuracy, decsMachine, accuracyPoint,
		[time], IDpoint, descPoint, X , Y , pointValue1 ,pointValue2 , variableName FROM Temp;

OPEN contact_cursor;

FETCH NEXT FROM contact_cursor INTO @IDgroup, @descGroup,@IDmachine ,@machineAccuracy2, @decsMachine, @accuracyPoint ,
@time2 , @IDpoint , @descPoint , @X, @Y, @pointValue1,@pointValue2,@variableName ;


WHILE  @@FETCH_STATUS = 0
    BEGIN
	DECLARE @time_in_dateTime DATETIME;
	EXECUTE @time_in_dateTime = ConvertToDateTime @Datetime = @time2;
	
	EXECUTE finaly_save
	@IDgroup = @IDgroup, @descGroup = @descGroup,@IDmachine = @IDmachine, @decsMachine = @decsMachine, @accuracyPoint = @accuracyPoint,
	@time = @time_in_dateTime, @IDpoint = @IDpoint, @descPoint = @descPoint, @X = @X, @Y = @Y, 
	@pointValue1 = @pointValue1,@pointValue2 = @pointValue2,
	@variableName = @variableName,@machineAccuracy=@machineAccuracy2;
	
	FETCH NEXT FROM contact_cursor INTO @IDgroup, @descGroup,@IDmachine , @machineAccuracy2,@decsMachine, @accuracyPoint,
	@time2 , @IDpoint , @descPoint , @X, @Y, @pointValue1,@pointValue2,@variableName ;
    
	END

	CLOSE contact_cursor;
	DEALLOCATE contact_cursor;
	DROP TABLE Temp;
	 
END

--SELECT * from Temp
--SET LOCK_TIMEOUT DEFAULT;
--SELECT @@LOCK_TIMEOUT ;
--TRUNCATE TABLE Temp;

SELECT * FROM Multiple_data_select ;