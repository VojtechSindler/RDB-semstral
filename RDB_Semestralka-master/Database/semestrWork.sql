-- pridany nonclustred indexy
CREATE TABLE Variables(
	variableID INT IDENTITY (1,1),
	name NVARCHAR (20) NOT NULL,
	PRIMARY KEY (variableID),
);
go

CREATE TABLE [Groups](
	groupID INT,
	[descriptionGroup] NVARCHAR(20) NOT NULL,
	PRIMARY KEY (groupID)
);

go
CREATE TABLE Machines(
	machineID NVARCHAR(20),
	[descriptionMachine] NVARCHAR(20) NOT NULL,
	machineAccuracy FLOAT NOT NULL,
	PRIMARY KEY(machineID)
);

go

CREATE TABLE Points( -- kam se souradnicema 
	pointID INT,
	[description] NVARCHAR(20) NOT NULL,
	X FLOAT NOT NULL,
	Y FLOAT NOT NULL,
	value1 FLOAT NOT NULL,
	value2 FLOAT NOT NULL,
	accuracy FLOAT NOT NULL,
	variableID INT NOT NULL,
	PRIMARY KEY (pointID,X,Y),
	FOREIGN KEY (variableID) REFERENCES Variables (variableID) ON DELETE CASCADE,
);

go

CREATE TABLE Measurement(
	[time] DATETIME NOT NULL,
	pointID INT NOT NULL,
	X FLOAT NOT NULL,
	Y FLOAT NOT NULL,
	machineID NVARCHAR(20) NOT NULL,
	groupID INT NOT NULL,
	FOREIGN KEY (pointID,X,Y) REFERENCES Points (pointID,X,Y) ON DELETE CASCADE,
	FOREIGN KEY (machineID) REFERENCES Machines (machineID) ON DELETE CASCADE,
	FOREIGN KEY (groupID) REFERENCES [Groups] (groupID) ON DELETE CASCADE
);

/*CREATE UNIQUE NONCLUSTERED INDEX pkGroups on Groups (groupID);
CREATE UNIQUE NONCLUSTERED INDEX pkVariables on Variables (variableID);
CREATE UNIQUE NONCLUSTERED INDEX pkPoints on Points (pointID);
CREATE UNIQUE NONCLUSTERED INDEX pkMachines on Machines (machineID);*/