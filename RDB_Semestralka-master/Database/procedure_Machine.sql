CREATE PROCEDURE proc_tk_Machines
@descriptionMachine nvarchar(20),@machineID NVARCHAR(20),@machineAcurracy FLOAT
as
Begin

	DECLARE @descriptionMachine2 nvarchar(20);
	DECLARE @machineID2 NVARCHAR(20);
	DECLARE @machineAcurracy2 FLOAT;

	DECLARE @isMachineHere int;
	DECLARE @isMachineHere_Name NVARCHAR(20);
	DECLARE @isMachineHere_Acurracy NVARCHAR(20);
	

	SET @descriptionMachine2 = @descriptionMachine;
	SET @machineID2 = @machineID;
	SET @machineAcurracy2 = @machineAcurracy;

	SELECT @isMachineHere = CASE WHEN (Select machineID FROM Machines WHERE machineID = @machineID2)IS NULL THEN 1 ELSE 0 END;
	SET @isMachineHere_Name = (SELECT descriptionMachine FROM Machines WHERE machineID = @machineID2);
	SET @isMachineHere_Acurracy = (SELECT machineAccuracy FROM Machines WHERE machineID = @machineID2);

	if(@isMachineHere!=0)
		BEGIN
			--PRINT 'Vlozena hodnota ' +CAST(@descriptionMachine as NVARCHAR (20))+' pod id '+@machineID;
			INSERT INTO Machines (machineID,descriptionMachine,machineAccuracy) VALUES (@machineID2,@descriptionMachine2,@machineAcurracy2);
			RETURN 2
		END
	ELSE
		BEGIN
			if(@isMachineHere_Name = @descriptionMachine)
			BEGIN
				if(@isMachineHere_Acurracy = @machineAcurracy)
				BEGIN
					RETURN 1;
				END
				ELSE
				BEGIN
					RETURN 0;
				END
			END
			ELSE
				BEGIN
					--PRINT 'Byla vložena nekonzistentní data';
					RETURN 0;
				END
		END
		
END
go
--DECLARE @mechID nvarchar(20);
--EXECUTE @mechID = proc_tk_Machines @descriptionMachine='Pokus4',@machineID='222A';
--PRINT ''+CAST(@mechID as NVARCHAR(20));
