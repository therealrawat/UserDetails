CREATE TABLE UserDetails (
    ID INT PRIMARY KEY IDENTITY(1,1),
    FirstName VARCHAR(50) NOT NULL,
    LastName VARCHAR(50) NOT NULL,
    Gender VARCHAR(10) NOT NULL,
    Dob DATE NOT NULL,
    PhoneNumber VARCHAR(10) NOT NULL,
    Email VARCHAR(100) NOT NULL,
    Address VARCHAR(100) NOT NULL,
    CityID INT, FOREIGN KEY (CityID) REFERENCES CityTable(ID),
    StateID INT, FOREIGN KEY (StateID) REFERENCES StateTable(ID),
    CountryID INT, FOREIGN KEY (CountryID) REFERENCES CountryTable(ID),
    PostalCode VARCHAR(10),
    ImageFilename VARCHAR(100),
	CreatedBy VARCHAR (100) DEFAULT 'Admin',
    CreatedAt DATETIME DEFAULT GETDATE(),	
	UpdatedBy VARCHAR (100),
    UpdatedAt DATETIME,
	DeletedBy VARCHAR (100),
	DeletedAt DATETIME,
	isDeleted bit,
	IsActive bit
);



INSERT INTO UserDetails (FirstName, LastName, Gender, Dob, PhoneNumber, Email, Address, CityID, StateID, CountryID, PostalCode, ImageFilename,)
VALUES
    ('John', 'Doe', 'Male', '1990-05-15', '1234567890', 'john.doe@example.com', '123 Main St', 1, 1, 1, '10001', 'png',);


--COUNTRY

CREATE TABLE CountryTable (
    ID INT PRIMARY KEY IDENTITY(1,1),
    Name VARCHAR(50) NOT NULL
);

INSERT INTO CountryTable ( Name) VALUES 
('India'),
('Nepal'),('Bhutan');

CREATE TABLE StateTable (
    ID INT PRIMARY KEY IDENTITY(1,1),
    Name VARCHAR(50) NOT NULL,
    CountryID INT NOT NULL,
    FOREIGN KEY (CountryID) REFERENCES CountryTable(ID)
);

INSERT INTO StateTable ( Name, CountryID) VALUES 
('Uttarakhand' , 1),
('Uttar Pradesh', 1),('Karnataka', 1),
('Lumbini Province' , 2),
('Karnali Province', 2),('Province No. 1', 2),
('Samchi' , 3),
('Ha', 3),('Daga', 3);


CREATE TABLE CityTable (
    ID INT PRIMARY KEY IDENTITY(1,1),
    Name VARCHAR(50) NOT NULL,
    StateID INT NOT NULL,
    FOREIGN KEY (StateID) REFERENCES StateTable(ID)
);

INSERT INTO Citytable ( Name, StateID) VALUES 
('Dehradun' , 1),
('Chamoli', 1),('Noida', 2),
('Lucknow' , 2),
('Bangaluru', 3),('Coimbatore', 3),
('Lumbini' , 4),
('Namcha', 4),('Karnali', 5),('Birendranagar', 5),('Biratnagar',6),('Karnali City',6),('Samchi',7),('Ha',8),('Daga Lu',9);


Select * from UserDetails;
Select * from CountryTable;
Select * from StateTable;
Select * from CityTable;


--drop procedure UserDetailsInsert

--  exec procedure UserDetailsInsert 'John','Doe','Male','2001-09-09','0987654321','john@mail.com','10 Lane',1,1,1,'248001','png'
-----------PROCEDURES INSERT------------------------
create procedure UserDetailsInsert
(
	@FirstName VARCHAR(50),
    @LastName VARCHAR(50) ,
    @Gender VARCHAR(10),
    @Dob DATE,
    @PhoneNumber VARCHAR(10),
    @Email VARCHAR(100),
    @Address VARCHAR(100),
    @CityID INT, 
    @StateID INT, 
    @CountryID INT,
    @PostalCode VARCHAR(10),
    @ImageFilename VARCHAR(100)
)
as
begin
insert into UserDetails 
(
FirstName,
LastName,
Gender,
Dob,
PhoneNumber,
Email,
Address,
CityID,
StateID,
CountryID,
PostalCode,
ImageFilename,
CreatedBy,
CreatedAt,
isDeleted,
IsActive
)
values
(
@FirstName,
@LastName,
@Gender,
@Dob,
@PhoneNumber,
@Email,
@Address,
@CityID,
@StateID,
@CountryID,
@PostalCode,
@ImageFilename,
'Admin',
GETDATE(),
0,
1
)
end

------------PROCEDURE UPDATE----------------------

--drop procedure UserDetailsUpdate
create procedure UserDetailsUpdate
(
	@ID INT,
	@FirstName VARCHAR(50),
    @LastName VARCHAR(50) ,
    @Gender VARCHAR(10),
    @Dob DATE,
    @PhoneNumber VARCHAR(10),
    @Email VARCHAR(100),
    @Address VARCHAR(100),
    @CityID INT, 
    @StateID INT, 
    @CountryID INT,
    @PostalCode VARCHAR(10),
    @ImageFilename VARCHAR(100),
    @UpdateAt DATETIME
)
as
begin
update UserDetails set

FirstName = @FirstName,
LastName = @LastName,
Gender = @Gender,
Dob = @Dob,
PhoneNumber = @PhoneNumber,
Email = @Email,
Address = @Address,
CityID = @CityID,
StateID = @StateID,
CountryID = @CountryID,
PostalCode = @PostalCode,
ImageFilename = @ImageFilename,
UpdatedAt = GETDATE(),
UpdatedBy = 'Admin'

where ID = @ID
end