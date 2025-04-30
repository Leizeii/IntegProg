

CREATE TABLE tblMain
(
	MainID INT PRIMARY KEY IDENTITY,
	aDate DATE,
	aTime VARCHAR(15),
	TableName VARCHAR(10),
	WaiterName VARCHAR(15),
	status VARCHAR(15),
	orderType VARCHAR(15),
	total DECIMAL(10, 2),
	received DECIMAL(10, 2),
	change DECIMAL(10, 2)
);

CREATE TABLE tblDetails
(
	DetailID INT PRIMARY KEY IDENTITY,
	MainID INT,
	proID INT,
	qty INT,
	price DECIMAL(10, 2),
	amount DECIMAL(10, 2)
);   

TRUNCATE TABLE tblDetails;
TRUNCATE TABLE tblMain;

SELECT * FROM tblMain m
INNER JOIN tblDetails d ON m.MainID = d.MainID


SELECT * FROM tblMain;
SELECT * FROM tblDetails;

SELECT * FROM tblMain m
INNER JOIN tblDetails d ON m.MainID = d.MainID
INNER JOIN Products p ON p.pID = d.proID
INNER JOIN Category c ON c.catID = p.CategoryID
WHERE m.aDate BETWEEN '2025-01-01' AND '2025-12-31'