DROP TABLE Employees;

CREATE TABLE Employees (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(MAX),
    Role NVARCHAR(MAX),
    Password NVARCHAR(MAX) -- New column
);
