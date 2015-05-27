CREATE PROCEDURE proc_tk_Variables 
@name nvarchar(20)
AS
BEGIN
	DECLARE @name2 nvarchar(20); 
	DECLARE @id int;
	DECLARE @isNameHere int;

	SET @name2 = @name;
	SELECT @isNameHere = CASE WHEN (Select name FROM Variables WHERE name = @name2)IS NULL THEN 1 ELSE 0 END;
	
	if(@isNameHere!=0)
		BEGIN
			INSERT INTO Variables(name) VALUES (@name2);
			SET @id =(Select @@IDENTITY)
			--PRINT 'Vlozena hodnota ' +CAST(@name as NVARCHAR (20))+ ' a pod ID '+CAST(@id as NVARCHAR(20));
			--Select @id = variableID from Variables Where name = @name;
			RETURN @id;
		END 
	ELSE
		BEGIN
			SET @id = (SELECT variableID from Variables Where name = @name2);
			--PRINT 'Hodnota jiz byla vlozena pod ID ' + CAST(@id as NVARCHAR(20));
			RETURN @id;
		END
	
END
go

--DECLARE @i int;
--EXECUTE @i = proc_tk_Variables @name='impedance7';
--PRINT CAST(@i as NVARCHAR(20));