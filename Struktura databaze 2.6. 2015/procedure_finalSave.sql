--SET QUOTED_IDENTIFIER ON

ALTER PROCEDURE finaly_save
@IDgroup INT, @descGroup NVARCHAR(20),@IDmachine NVARCHAR(20), @decsMachine NVARCHAR(20), @accuracyPoint FLOAT,
@time DATETIME, @IDpoint INT, @descPoint NVARCHAR(20), @X FLOAT, @Y FLOAT, @pointValue1 FLOAT,@pointValue2 FLOAT,
@variableName NVARCHAR(20),@machineAccuracy FLOAT
as
SET NOCOUNT ON
BEGIN
--------------------------------------------------------------------------------------------------------------------------
DECLARE @IDgroup2 INT;
DECLARE @descGroup2 NVARCHAR(20);
DECLARE @IDmachine2 NVARCHAR(20);
DECLARE @decsMachine2 NVARCHAR(20); 
DECLARE @accuracyPoint2 FLOAT;
DECLARE @time2 DATETIME;
DECLARE @IDpoint2 INT;
DECLARE @descPoint2 NVARCHAR(20);
DECLARE @X2 FLOAT; 
DECLARE @Y2 FLOAT; 
DECLARE @pointValue1_2 FLOAT;
DECLARE @pointValue2_2 FLOAT;
DECLARE @variableName2 NVARCHAR(20);
DECLARE @machineAccuracy2 FLOAT;

SET @IDgroup2 = @IDgroup;
SET @descGroup2 = @descGroup;
SET @IDmachine2 = @IDmachine;
SET @decsMachine2 = @decsMachine; 
SET @accuracyPoint2 = @accuracyPoint;
SET @time2 = @time;
SET @IDpoint2 = @IDpoint;
SET @descPoint2 = @descPoint;
SET @X2 = @X; 
SET @Y2 = @Y; 
SET @pointValue1_2 = @pointValue1;
SET @pointValue2_2 = @pointValue2;
SET @variableName2 = @variableName;
SET @machineAccuracy2 = @machineAccuracy;
--------------------------------------------------------------------------------------------------------------------------
DECLARE @countPoint int;
DECLARE @idVar INT;
DECLARE @idGroups INT;
DECLARE @idMach_check INT;

SET @countPoint = (SELECT COUNT(pointID) FROM Points Where pointID = @IDpoint2 AND value1 = @pointValue1 AND value2 = @pointValue2);
-----------------------------------------------------------------------------------------------------------------------------
	if(@countPoint = 0)
		BEGIN			
			EXECUTE @idGroups = proc_tk_Groups @descriptionGroup= @descGroup2, @groupID = @IDgroup2;
		END
	ELSE
		BEGIN
			RAISERROR ('Argument primary key exception, this key already exist',10,1);
--			ROLLBACK TRAN;
			RETURN;
		END
---------------------------------------------------------------------------	----------------------------------------------------------------------------
	if(@countPoint = 0)
		BEGIN
			if(@idGroups!=0)
				BEGIN
					EXECUTE @idMach_check = proc_tk_Machines @descriptionMachine=@decsMachine2,@machineID=@IDmachine2,@machineAcurracy=@machineAccuracy2;
				END
			ELSE
				BEGIN
					RAISERROR ('Inconsistent data',10,1);
					RETURN;
				END
		END
	ELSE
		BEGIN
			RAISERROR ('Argument primary key exception, this key already exist',10,1);
--			ROLLBACK TRAN;
			RETURN;
		END
---------------------------------------------------------------------------------------------------------------------------------------------------------	
	if(@countPoint = 0)
		BEGIN
			if(@idGroups!=0 AND @idMach_check!=0)
				BEGIN
					EXECUTE @idVar = proc_tk_Variables @name=@variableName2;
					--PRINT CAST(@i as NVARCHAR(20));
				END
			ELSE
				BEGIN
					RAISERROR ('Inconsistent data',10,1);
					RETURN;
				END
		END
	ELSE
		BEGIN
			RAISERROR ('Argument primary key exception, this key already exist',10,1);
--			ROLLBACK TRAN;
			RETURN;
		END

----------------------------------------------------------------------------------------------------------------------------------------------------------
	if(@countPoint = 0)
		BEGIN
			if(@idVar!=0 AND @idGroups!=0 AND @idMach_check!=0)
				BEGIN
					INSERT INTO Points (pointID,[description],X,Y,value1,value2,accuracy,variableID) VALUES (@IDpoint2,@descPoint2,@X2,@Y2,@pointValue1_2,@pointValue2_2,@accuracyPoint2,@idVar);
					INSERT INTO Measurement ([time],pointID,value1,value2,machineID,groupID) VALUES (@time2,@IDpoint2,@pointValue1_2,@pointValue2_2,@IDmachine2,@IDgroup2)
					
				END
			ELSE
				BEGIN
	--				Print 'NEKOZISTENCE';
					RAISERROR ('Inconsistent data',10,1);
					--ROLLBACK TRAN;
					RETURN
				END			
		END
	ELSE
		BEGIN
			RAISERROR ('Argument primary key exception, this key already exist',10,1);
--			ROLLBACK TRAN;
			RETURN;
		END
END
go
EXECUTE finaly_save
@IDgroup = 10012, @descGroup = 'rano8',@IDmachine = '87', @decsMachine = 'Pokusny stroj85', @accuracyPoint = 1.148,
@time = '2015-01-01', @IDpoint = 1, @descPoint = 'Pokusny bod85', @X = 12.82471, @Y = 13.24, 
@pointValue1 = 122,@pointValue2 = 222,
@variableName = 'odpor87',@machineAccuracy = 1.1;
go
SELECT * from Multiple_data_select ORDER By pointID ASC;
SELECT * from Measurement ORDER By pointID DESC;
SELECT * from Temp ORDER BY IDpoint ASC;
