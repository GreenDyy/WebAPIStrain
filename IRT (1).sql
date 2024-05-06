CREATE DATABASE IRT
GO
USE IRT
GO

-- Bảng Ngành: mã ngành, tên ngành
CREATE TABLE Phylum (
    ID_Phylum INT IDENTITY(1,1) PRIMARY KEY,
    Name_Phylum NVARCHAR(255)
);
GO

-- Bảng Lớp: mã lớp, tên lớp
CREATE TABLE Class(
    ID_Class INT IDENTITY(1,1) PRIMARY KEY,
    Name_Class NVARCHAR(255),
    ID_Phylum INT,
    CONSTRAINT FK_Class_Phylum FOREIGN KEY (ID_Phylum) REFERENCES Phylum(ID_Phylum)
);
GO

-- Bảng Chi:
CREATE TABLE Genus(
	ID_Genus INT IDENTITY(1,1) PRIMARY KEY,
	Name_Genus NVARCHAR(255),
	ID_Class INT,
    CONSTRAINT FK_Genus_Class FOREIGN KEY (ID_Class) REFERENCES Class(ID_Class)
);
GO

-- Bảng loài:
CREATE TABLE Species(
	ID_Species INT IDENTITY(1,1) PRIMARY KEY,
	Name_Species NVARCHAR(255),
	ID_Genus INT,
	CONSTRAINT FK_Species_Genus FOREIGN KEY (ID_Genus) REFERENCES Genus(ID_Genus)
);
GO

--1 strain chỉ có 1 điều kiện, ngược lại 1 điều kiện có thể thuộc nhiều strain
--Bảng điều kiện
CREATE TABLE ConditionalStrain (
	ID_Condition INT IDENTITY(1,1) PRIMARY KEY,
	Medium NVARCHAR(255) DEFAULT NULL,
	Temperature NVARCHAR(255) DEFAULT NULL,
	Light_Intensity NVARCHAR(255) DEFAULT NULL,
	Duration NVARCHAR(255) DEFAULT NULL,
);
GO

--Bảng Strain:
CREATE TABLE Strain(
    ID_Strain INT IDENTITY(1,1) PRIMARY KEY,
	Strain_Number NVARCHAR(100) DEFAULT NULL,
    ID_Species INT,
	ID_Condition INT,
	Image_Strain VARBINARY(MAX) DEFAULT NULL,
    Scientific_Name NVARCHAR(255), --Tên khoa học của strain
    Synonym_Strain NVARCHAR(255), --Đồng danh
    Former_Name NVARCHAR(255), --Tên ban đầu
    Common_Name NVARCHAR(255), --Tên thường gọi
    Cell_Size NVARCHAR(255), --Kích thước
    Organization NVARCHAR(255), --Tổ chức cơ thể
    Characteristics NVARCHAR(255), --Đặc điểm
    Collection_Site NVARCHAR(MAX), --Vị trí thu mẫu
    Continent NVARCHAR(255), --Châu lục
    Country NVARCHAR(255), --Quốc gia
    Isolation_Source NVARCHAR(255), --Môi trường thu được mẫu
    Toxin_Producer NVARCHAR(255), --Sản sinh độc tố
    State_of_Strain NVARCHAR(255), --Tình trạng của chủng
    Agitation_Resistance NVARCHAR(255), --Khả năng chống kích động
    Remarks NVARCHAR(255), --Nhận xét
    Gene_Information NVARCHAR(MAX), --Thông tin về gen
    Publications NVARCHAR(255), --Ấn phẩm
    Recommended_For_Teaching NVARCHAR(20), --Có khuyến khích cho việc giảng dạy? (Yes/No)
	Price DECIMAL(10, 2),
	Quality INT,
	Status NVARCHAR(255),

    CONSTRAINT FK_Strain_Species FOREIGN KEY (ID_Species) REFERENCES Species(ID_Species),
	CONSTRAINT FK_Strain_Condition FOREIGN KEY (ID_Condition) REFERENCES ConditionalStrain(ID_Condition)
);
GO

--Bảng quyền (nhân viên, nghiên cứu viên, nhân viên quản lý...,...)
CREATE TABLE RoleForEmployee (
    ID_Role INT PRIMARY KEY IDENTITY(1,1),
    RoleName NVARCHAR(100),
    RoleDescription NVARCHAR(255)
);
GO

CREATE TABLE Employee (
    ID_Employee NVARCHAR(50) PRIMARY KEY, -- Mã nhân viên (Định dạng: NV0001->NV9999)
    ID_Role INT,
    FirstName NVARCHAR(100),
    LastName NVARCHAR(100),
    FullName NVARCHAR(100),
    ID_Card NVARCHAR(12),
    Date_of_Birth DATE,
    Gender NVARCHAR(10),
    Email NVARCHAR(255),
    Phone_Number NVARCHAR(20),
    Degree NVARCHAR(100), -- Bằng cấp
    Address NVARCHAR(255), -- Địa chỉ ở hiện tại
    Join_Date DATE, -- Ngày tham gia công ty
	Image_Employee VARBINARY(MAX),

    CONSTRAINT FK_Employee_RoleForEmployee FOREIGN KEY (ID_Role) REFERENCES RoleForEmployee(ID_Role)
);
GO

CREATE TABLE AccountForEmployee(
    ID_Employee NVARCHAR(50) PRIMARY KEY,
    Username NVARCHAR(255),
    Password NVARCHAR(255),
    Status NVARCHAR(100),
    CONSTRAINT FK_AccountForEmployee_Employee FOREIGN KEY (ID_Employee) REFERENCES Employee(ID_Employee)
);
GO



--1 nghiên cứu viên có thể nghiên cứu nhiều strain và ngược lại
--Bảng nghiên cứu viên và strain
CREATE TABLE IsolatorStrain (
    ID_Employee NVARCHAR(50),
    ID_Strain INT,
	Year_of_Isolator INT,
    PRIMARY KEY (ID_Employee, ID_Strain),

    CONSTRAINT FK_EmployeeStrain_Employee FOREIGN KEY (ID_Employee) REFERENCES Employee(ID_Employee),
    CONSTRAINT FK_EmployeeStrain_Strain FOREIGN KEY (ID_Strain) REFERENCES Strain(ID_Strain)
);
GO

--Bảng định danh: để cho biết nhân viên nào định danh strain nào, khi nào?
CREATE TABLE IdentifyStrain(
	ID_Employee NVARCHAR(50),
    ID_Strain INT,
	Year_of_Identify INT,
	PRIMARY KEY (ID_Employee, ID_Strain),

    CONSTRAINT FK_IdentifyStrain_Employee FOREIGN KEY (ID_Employee) REFERENCES Employee(ID_Employee),
    CONSTRAINT FK_IdentifyStrain_Strain FOREIGN KEY (ID_Strain) REFERENCES Strain(ID_Strain)
);
GO

--Bảng bài báo khoa học
CREATE TABLE ScienceNewspaper (
    ID_Newspaper INT IDENTITY(1,1) PRIMARY KEY,
    Title NVARCHAR(255),
	Content NVARCHAR(MAX),
    URL NVARCHAR(255),
);
GO

--Bảng tác giả bài báo khoa học (nhân viên-bài báo-n,n)
CREATE TABLE AuthorNewspaper(
	ID_Newspaper INT,
	ID_Employee NVARCHAR(50),
	Post_Date DATE, --Ngày đăng
	Role_Of_Author NVARCHAR(255), --Vai trò: tác giả chính, tác giả liên hệ, thành viên

    CONSTRAINT FK_AuthorNewspaper_Employee FOREIGN KEY (ID_Employee) REFERENCES Employee(ID_Employee),
	CONSTRAINT FK_AuthorNewspaper_ScienceNewspaper FOREIGN KEY (ID_Newspaper) REFERENCES ScienceNewspaper(ID_Newspaper),
);
GO

--Bảng đối tác quản lý dự án
CREATE TABLE Partner(
	ID_Partner INT IDENTITY(1,1) PRIMARY KEY,
	Name_Company NVARCHAR(255),
	Address_Company NVARCHAR(255),
	Name_Partner NVARCHAR(255),
	Position NVARCHAR(255),
	Phone_Number NVARCHAR(20),
	Bank_Number NVARCHAR(255),
	Bank_Name NVARCHAR(255),
	QHNS_Number NVARCHAR(255),
);
GO

--Bảng dự án nghiên cứu
CREATE TABLE Project (
    ID_Project NVARCHAR(50) PRIMARY KEY, -- Mã dự án nghiên cứu (Định dạng: DA0001->DA9999)
    ID_Employee NVARCHAR(50), -- Người chủ nhiệm dự án
	ID_Partner INT, --Đối tác quản lý dự án
    ProjectName NVARCHAR(MAX),
	Results NVARCHAR(MAX),
    StartDateProject DATE,
	ContractNo NVARCHAR(MAX),
	Description NVARCHAR(MAX),
	FileProject VARBINARY(MAX), 
	Status NVARCHAR(255),
	--Từng gia đoạn của dự án:
		--1. Chưa hoàn thành
		--2. Đã hoàn thành

    CONSTRAINT FK_Project_Employee FOREIGN KEY (ID_Employee) REFERENCES Employee(ID_Employee),
	CONSTRAINT FK_Project_Partner FOREIGN KEY (ID_Partner) REFERENCES Partner(ID_Partner)
);
GO

--Bảng nội dung cho bảng dự án nghiên cứu
CREATE TABLE ProjectContent(
	ID_ProjectContent INT IDENTITY(1,1) PRIMARY KEY,
	ID_Project NVARCHAR(50),
	Name_Content NVARCHAR(MAX),
	Results NVARCHAR(MAX),
	StartDate DATE,
	EndDate DATE,
	ContractNo NVARCHAR(MAX),
	Status NVARCHAR(255),

	CONSTRAINT FK_ProjectContent_Project FOREIGN KEY (ID_Project) REFERENCES Project(ID_Project)
);
GO

--Bảng công việc cho nội dung
CREATE TABLE ContentWork(
	ID_ContentWork INT IDENTITY(1,1) PRIMARY KEY,
	ID_ProjectContent INT,
	ID_Employee NVARCHAR(50),
	Name_Content NVARCHAR(MAX),
	Results NVARCHAR(MAX),
	StartDate DATE,
	EndDate DATE,
	ContractNo NVARCHAR(MAX),
	Status NVARCHAR(255),

	CONSTRAINT FK_ContentWork_ProjectContent FOREIGN KEY (ID_ProjectContent) REFERENCES ProjectContent(ID_ProjectContent),
	CONSTRAINT FK_ContentWork_Employee FOREIGN KEY (ID_Employee) REFERENCES Employee(ID_Employee)
);
GO

--Bảng khách hàng
CREATE TABLE Customer(
	ID_Customer NVARCHAR(50) PRIMARY KEY, -- Mã khách hàng (Định dạng: KH00001)
	FirstName NVARCHAR(100),
    LastName NVARCHAR(100),
    FullName NVARCHAR(100),
    Date_of_Birth DATE,
    Gender NVARCHAR(10),
    Email NVARCHAR(255),
    Phone_Number NVARCHAR(20),
);
GO

--Bảng tài khoản khách hàng
CREATE TABLE AccountForCustomer(
	ID_Customer NVARCHAR(50) PRIMARY KEY,
	Username NVARCHAR(255),
	Password NVARCHAR(255),
	Status NVARCHAR(100),

	CONSTRAINT FK_AccountForCustomer_Customer FOREIGN KEY (ID_Customer) REFERENCES Customer(ID_Customer)
);
GO

--Bảng hóa đơn
CREATE TABLE Bill(
	ID_Bill NVARCHAR(50) PRIMARY KEY, -- Mã hóa đơn (Định dạng: 159753xxxxxx)
	ID_Customer NVARCHAR(50),
	ID_Employee NVARCHAR(50),
	BillDate DATE,
	Status_Of_Bill NVARCHAR(255),
	Type_Of_Bill NVARCHAR(255),
	Total FLOAT,

	CONSTRAINT FK_BillOffline_Customer FOREIGN KEY (ID_Customer) REFERENCES Customer(ID_Customer),
	CONSTRAINT FK_BillOffline_Employee FOREIGN KEY (ID_Employee) REFERENCES Employee(ID_Employee)
);
GO

--Bảng chi tiết hóa đơn
CREATE TABLE BillDetail(
	ID_BillDetail INT IDENTITY(1,1) PRIMARY KEY,
	ID_Bill NVARCHAR(50),
	ID_Strain INT,
	Quantity INT,

	CONSTRAINT FK_BillOfflineDetail_Bill FOREIGN KEY (ID_Bill) REFERENCES Bill(ID_Bill),
	CONSTRAINT FK_BillOfflineDetail_Strain FOREIGN KEY (ID_Strain) REFERENCES Strain(ID_Strain)
);
GO

--Bảng giỏ hàng, khi mua online trên website
CREATE TABLE Cart(
	ID_Cart INT IDENTITY(1,1) PRIMARY KEY,
	ID_Customer NVARCHAR(50),
	Toatal_Product INT,

	CONSTRAINT FK_Cart_Customer FOREIGN KEY (ID_Customer) REFERENCES Customer(ID_Customer)
);
GO

--Bảng chi tiết giỏ hàng
CREATE TABLE CartDetail(
	ID_CartDetail INT IDENTITY(1,1) PRIMARY KEY,
	ID_Cart INT,
	ID_Strain INT,
	Quantity_Of_Strain INT,

	CONSTRAINT FK_CartDetail_Cart FOREIGN KEY (ID_Cart) REFERENCES Cart(ID_Cart),
	CONSTRAINT FK_CartDetail_Strain FOREIGN KEY (ID_Strain) REFERENCES Strain(ID_Strain)
);
GO

--=================================================================================================================================================================
--=================================================================================================================================================================

--CREATE OR ALTER PROCEDURE sp_InsertRoleForEmployee
--    @RoleName NVARCHAR(100),
--    @RoleDescription NVARCHAR(255)
--AS
--BEGIN
--    INSERT INTO RoleForEmployee (RoleName, RoleDescription)
--    VALUES (@RoleName, @RoleDescription);
--END;
--GO

--EXEC sp_InsertRoleForEmployee
--    @RoleName = N'Viện trưởng',
--    @RoleDescription = N'Quản lý các hoạt động của công ty';
--EXEC sp_InsertRoleForEmployee
--    @RoleName = N'Nhân viên bán hàng',
--    @RoleDescription = N'Nhân viên thực hiện các nhiệm vụ được giao';
--EXEC sp_InsertRoleForEmployee
--    @RoleName = N'Nghiên cứu viên',
--    @RoleDescription = N'Nghiên cứu viên nghiên cứu sản phẩm';


--------------------------------------------

--CREATE OR ALTER PROCEDURE sp_InsertEmployee
--    @ID_Role INT,
--    @FirstName NVARCHAR(100),
--    @LastName NVARCHAR(100),
--    @ID_Card NVARCHAR(12),
--    @Date_of_Birth DATE,
--    @Gender NVARCHAR(10),
--    @Email NVARCHAR(255),
--    @Phone_Number NVARCHAR(20),
--    @Degree NVARCHAR(100),
--    @Address NVARCHAR(255),
--    @Join_Date DATE,
--    @Institution NVARCHAR(255),
--    @Department NVARCHAR(255),
--    @Position NVARCHAR(100),
--    @Research_Field NVARCHAR(255),
--    @Image_Employee VARBINARY(MAX) = NULL
--AS
--BEGIN
--    DECLARE @ID_Employee NVARCHAR(50);
--    DECLARE @MaxNumber INT;

--    SELECT @MaxNumber = COALESCE(MAX(CAST(SUBSTRING(ID_Employee, 3, LEN(ID_Employee) - 2) AS INT)), 0) + 1 FROM Employee;
--    SET @ID_Employee = FORMAT(@MaxNumber, 'NV000');

--    INSERT INTO Employee (
--        ID_Employee, ID_Role, FirstName, LastName, FullName,
--        ID_Card, Date_of_Birth, Gender, Email, Phone_Number,
--        Degree, Address, Join_Date, Institution, Department,
--        Position, Research_Field, Image_Employee
--    )
--    VALUES (
--        @ID_Employee, @ID_Role, @FirstName, @LastName, @LastName + ' ' + @FirstName,
--        @ID_Card, @Date_of_Birth, @Gender, @Email, @Phone_Number,
--        @Degree, @Address, @Join_Date, @Institution, @Department,
--        @Position, @Research_Field, @Image_Employee
--    );
--END;
--GO

--EXEC sp_InsertEmployee
--    @ID_Role = 1,
--    @FirstName = N'Tuấn Anh',
--    @LastName = N'Phạm Lê',
--    @ID_Card = '123456780123',
--    @Date_of_Birth = '2002-12-05',
--    @Gender = N'Nam',
--    @Email = 'tuananh@gmail.com',
--    @Phone_Number = '0123456789',
--    @Degree = 'Đại học',
--    @Address = N'Quận 1, TP. HCM',
--    @Join_Date = '2024-05-01',
--    @Institution = '-',
--    @Department = '-',
--    @Position = '-',
--    @Research_Field = N'Công nghệ thông tin',
--	@Image_Employee = NULL;

--EXEC sp_InsertEmployee
--    @ID_Role = 2,
--    @FirstName = N'Quân',
--    @LastName = N'Châu Minh',
--    @ID_Card = '567801231234',
--    @Date_of_Birth = '2002-12-27',
--    @Gender = N'Nam',
--    @Email = 'quan@gmail.com',
--    @Phone_Number = '0123456789',
--    @Degree = 'Đại học',
--    @Address = N'Bình Tân, TP. HCM',
--    @Join_Date = '2024-05-01',
--    @Institution = '',
--    @Department = '',
--    @Position = '',
--    @Research_Field = N'Công nghệ thông tin',
--	@Image_Employee = NULL;


--------------------------------------------

--CREATE OR ALTER PROCEDURE sp_InsertAccountForEmployee
--    @ID_Employee NVARCHAR(50),
--    @Username NVARCHAR(255),
--    @Password NVARCHAR(255),
--    @Status NVARCHAR(100)
--AS
--BEGIN
--    INSERT INTO AccountForEmployee (ID_Employee, Username, Password, Status)
--    VALUES (@ID_Employee, @Username, @Password, @Status);
--END;
--GO

--EXEC sp_InsertAccountForEmployee
--    @ID_Employee = N'NV001',
--    @Username = N'tuananh',
--    @Password = N'123',
--    @Status = N'Đang hoạt động';
--EXEC sp_InsertAccountForEmployee
--    @ID_Employee = N'NV002',
--    @Username = N'quan',
--    @Password = N'456',
--    @Status = N'Đang hoạt động';



SELECT 
    s.Strain_Number,
    sp.Name_Species AS Species_Name,
    ge.Name_Genus AS Name_Genus,
    cs.Name_Class AS Name_Class,
    ph.Name_Phylum AS Name_Phylum,
    s.Image_Strain
FROM 
    Strain s
JOIN 
    Species sp ON s.ID_Species = sp.ID_Species
JOIN 
    Genus ge ON sp.ID_Genus = ge.ID_Genus
JOIN 
    Class cs ON ge.ID_Class = cs.ID_Class
JOIN 
    Phylum ph ON cs.ID_Phylum = ph.ID_Phylum
WHERE
	Strain_Number = '' OR Name_Species = ''


	select * from Customer
	select * from AccountForCustomer

	
	select * from Employee
	select * from AccountForEmployee

	select * from RoleForEmployee