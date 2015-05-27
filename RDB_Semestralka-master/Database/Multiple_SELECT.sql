CREATE VIEW Multiple_data_select
AS
SELECT [time],Measurement.pointID,Points.[description],Points.X,Points.Y,Points.value1,
Points.value2,Points.accuracy as 'PointAccuracy',Points.variableID,Variables.name as variableName, Measurement.machineID, Machines.descriptionMachine, Machines.machineAccuracy,
Groups.groupID, Groups.descriptionGroup  from Measurement 
JOIN Points ON Measurement.pointID = Points.pointID  AND Points.X = Measurement.X AND Points.Y = Measurement.Y JOIN Machines 
ON Measurement.machineID = Machines.machineID JOIN Groups ON Measurement.groupID = Groups.groupID
JOIN Variables ON Points.variableID = Variables.variableID;

SELECT * from Multiple_data_select;