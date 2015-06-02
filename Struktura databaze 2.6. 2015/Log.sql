CREATE TABLE Logs
(
	[date] Datetime,
	Operation NVARCHAR(20),
	Primary Key ([date])
);
INSERT INTO [Logs] VALUES (GETDATE(),'Vkladani dat');

Select * from [Logs]