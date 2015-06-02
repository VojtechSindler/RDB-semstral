ALTER procedure proc_tk_Groups
 @descriptionGroup NVARCHAR(20),
 @groupID int
as 
BEGIN
	DECLARE @groupID2 int;
	DECLARE @descriptionGroup2 NVARCHAR(20);
	DECLARE @isGroupHere int;
	DECLARE @isGroupHere_desc NVARCHAR(20);
	
	SET @groupID2 = @groupID;
	SET @descriptionGroup2 = @descriptionGroup;
	SELECT @isGroupHere = CASE WHEN (Select descriptionGroup FROM Groups WHERE groupID = @groupID2)IS NULL THEN 1 ELSE 0 END;
	Select @isGroupHere_desc = descriptionGroup from Groups Where groupID = @groupID2; 

	if(@isGroupHere != 0)
		BEGIN
			--PRINT 'Vlozena hodnota ' +CAST(@descriptionGroup as NVARCHAR (20))+' pod id '+CAST(@groupID as NVARCHAR(20));
			INSERT INTO Groups(groupID,descriptionGroup) VALUES (@groupID2,@descriptionGroup2);
			RETURN @groupID;
		END
	else
		BEGIN
			if(@descriptionGroup = @isGroupHere_desc)
				BEGIN
					--PRINT 'Hodnota jiz byla vlozena pod ID ' + CAST(@groupID as NVARCHAR(20));
					RETURN @groupID2;
				END
			else
				BEGIN
					--PRINT 'Nekonzistentní data';
					RETURN 0;
				END
		END
END
go
DECLARE @i int;
EXECUTE @i = proc_tk_Groups @descriptionGroup= 'pokus5', @groupID = 3;
PRINT CAST(@i as NVARCHAR(20));
