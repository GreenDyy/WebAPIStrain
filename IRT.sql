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
	Medium NVARCHAR(255) DEFAULT 'null',
	Temperature NVARCHAR(255) DEFAULT 'null',
	Light_Intensity NVARCHAR(255) DEFAULT 'null',
	Duration NVARCHAR(255) DEFAULT 'null',
);
GO

--Bảng Strain:
CREATE TABLE Strain(
    ID_Strain INT IDENTITY(1,1) PRIMARY KEY,
	Strain_Number NVARCHAR(100) DEFAULT 'null',
    ID_Species INT,
	ID_Condition INT,
	Image_Strain VARBINARY(MAX),
    Scientific_Name NVARCHAR(50), --Tên khoa học của strain
    Synonym_Strain NVARCHAR(255), --Đồng danh
    Former_Name NVARCHAR(255), --Tên ban đầu
    Common_Name NVARCHAR(255), --Tên thường gọi
    Cell_Size NVARCHAR(255), --Kích thước
    Organization NVARCHAR(255), --Tổ chức cơ thể
    Characteristics NVARCHAR(255), --Đặc điểm
    Deposition_Date DATE, --Ngày kí gửi mẫu
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
	Status_of_Strain NVARCHAR(255),

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

--Bảng tài khoản cho user: nhân viên, nghiên cứu viên, nhân viên quản lý...
CREATE TABLE AccountForEmployee(
	ID_Account INT PRIMARY KEY IDENTITY(1,1),
	Username NVARCHAR(255),
	Password NVARCHAR(255),
	Status NVARCHAR(100),
);
GO

--Bảng Nhân viên: nghiên cứu viên, nhân viên bán hàng, nhân viên quản lý bài báo khoa học, nhân viên quản lý tiến độ dự án,....
CREATE TABLE Employee (
    ID_Employee NVARCHAR(50) PRIMARY KEY, -- Mã nhân viên (Định dạng: NV0001->NV9999)
    ID_Role INT,
    ID_Account INT,
    FirstName NVARCHAR(100),
    LastName NVARCHAR(100),
    FullName NVARCHAR(100),
    ID_Card NVARCHAR(12),
    Date_of_Birth DATE,
    Gender NVARCHAR(10),
    Email NVARCHAR(255),
    Phone_Number NVARCHAR(20),
    Degree NVARCHAR(100), -- Bằng cấp
    Addresss NVARCHAR(255), -- Địa chỉ ở hiện tại
    Join_Date DATE, -- Ngày tham gia công ty
    Institution NVARCHAR(255), -- Tên cơ quan, tổ chức mà nhân viên đang công tác
    Department NVARCHAR(255), -- Phòng ban hoặc khoa mà nhân viên thuộc về
    Position NVARCHAR(100), -- Vị trí hiện tại của nhân viên
    Research_Field NVARCHAR(255), -- Lĩnh vực nghiên cứu chính

    CONSTRAINT FK_Employee_RoleForEmployee FOREIGN KEY (ID_Role) REFERENCES RoleForEmployee(ID_Role),
    CONSTRAINT FK_Employee_AccountForEmployee FOREIGN KEY (ID_Account) REFERENCES AccountForEmployee(ID_Account),
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
    ProjectName NVARCHAR(255),
    StartDate DATE,
    EndDate DATE,
	Description NVARCHAR(MAX), -- Mô tả dự án
    --Budget DECIMAL(18,2), -- Ngân sách dành cho dự án ?
	Status NVARCHAR(255),
	File_For_Project VARBINARY(MAX), 
	--Từng gia đoạn của dự án:
		--1. Lên kế hoạch (Xác định mục tiêu -> Lập kế hoạch)
		--2. Phân tích và thiết kế (Phân tích nhu cầu và yêu cầu -> Thiết kế)
		--3. Thực hiện nghiên cứu
		--4. Phân tích dữ liệu (Thực hiện nghiên cứu -> Thu thập dữ liệu)
		--5. Phân tích kết quả
		--6. Viết báo cáo
		--7. Đánh giá
		--8. Hoàn thành

    CONSTRAINT FK_Project_Employee FOREIGN KEY (ID_Employee) REFERENCES Employee(ID_Employee),
	CONSTRAINT FK_Project_Partner FOREIGN KEY (ID_Partner) REFERENCES Partner(ID_Partner)
);
GO

--Bảng nội dung cho bảng dự án nghiên cứu
CREATE TABLE ProjectContent(
	ID_ProjectContent INT IDENTITY(1,1) PRIMARY KEY,
	Name_Content NVARCHAR(MAX),
	ID_Project NVARCHAR(50),

	CONSTRAINT FK_ProjectContent_Project FOREIGN KEY (ID_Project) REFERENCES Project(ID_Project)
);
GO

--Bảng công việc cho nội dung
CREATE TABLE ContentWork(
	ID_ContentWork INT IDENTITY(1,1) PRIMARY KEY,
	Content NVARCHAR(MAX),
	ID_ProjectContent INT,
	ID_Employee NVARCHAR(50),

	CONSTRAINT FK_ContentWork_ProjectContent FOREIGN KEY (ID_ProjectContent) REFERENCES ProjectContent(ID_ProjectContent),
	CONSTRAINT FK_ContentWork_Employee FOREIGN KEY (ID_Employee) REFERENCES Employee(ID_Employee)
);
GO

--Bảng khách hàng
CREATE TABLE Customer(
	ID INT IDENTITY(1,1) PRIMARY KEY,
	ID_Customer NVARCHAR(50) UNIQUE, -- Mã khách hàng (Định dạng: KH00001)
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
	ID_AccountForCustomer INT IDENTITY(1,1) PRIMARY KEY,
	ID INT,
	Username NVARCHAR(255),
	Password NVARCHAR(255),
	Status NVARCHAR(100),

	CONSTRAINT FK_AccountForCustomer_Customer FOREIGN KEY (ID) REFERENCES Customer(ID)
);
GO

--Bảng hóa đơn
CREATE TABLE Bill(
	ID_Bill NVARCHAR(50) PRIMARY KEY, -- Mã hóa đơn (Định dạng: 159753xxxxxx)
	ID_Customer INT,
	ID_Employee NVARCHAR(50),
	BillDate DATE,
	Status_Of_Bill NVARCHAR(255),
	Type_Of_Bill NVARCHAR(255),
	Total FLOAT,

	CONSTRAINT FK_BillOffline_Customer FOREIGN KEY (ID_Customer) REFERENCES Customer(ID),
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

--Bảng số lượng sản phẩm có trong kho
CREATE TABLE Warehouse(
	ID_Strain INT PRIMARY KEY,
	Quantity_Of_Strain INT,

	CONSTRAINT FK_Warehouse_Strain FOREIGN KEY (ID_Strain) REFERENCES Strain(ID_Strain)
);

--Bảng giỏ hàng, khi mua online trên website
CREATE TABLE Cart(
	ID_Cart INT IDENTITY(1,1) PRIMARY KEY,
	ID_Customer INT,
	Toatal_Product INT,

	CONSTRAINT FK_Cart_Customer FOREIGN KEY (ID_Customer) REFERENCES Customer(ID)
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

--------------------------------------------------------------------
--Nháp
--SELECT
--    s.ID_Strain,
--    p.Name_Phylum,
--    c.Name_Class,
--    s.Scientific_Name,
--    s.Synonym_Strain,
--    s.Former_Name,
--    s.Common_Name,
--    s.Cell_Size,
--    s.Organization,
--    s.Characteristics,
--    r.FullName,
--    YEAR(rs.Research_Start_Date) AS 'Research_Start_Date_Of_Year',
--    s.Deposition_Date,
--    s.Collection_Site,
--    s.Continent,
--    s.Country,
--    s.Isolation_Source,
--    cs.Levels,
--    cs.DetailLevels,
--    cs.Temperature,
--    cs.Light_Intensity,
--    cs.Duration,
--    s.Toxin_Producer,
--    s.State_of_Strain,
--    s.Agitation_Resistance,
--    s.Remarks,
--    s.Gene_Information,
--    s.Publications,
--    s.Recommended_For_Teaching
--FROM
--    Strain s
--INNER JOIN
--    Class c ON s.ID_Class = c.ID_Class
--INNER JOIN
--    Phylum p ON c.ID_Phylum = p.ID_Phylum
--INNER JOIN
--    EmployeeStrain rs ON s.ID_Strain = rs.ID_Strain
--INNER JOIN
--    Employee r ON rs.ID_Employee = r.ID_Employee
--INNER JOIN
--    ConditionalStrain cs ON s.ID_Condition = cs.ID_Condition;
go
--Insert DATA
--Bảng role nhan vien
INSERT INTO RoleForEmployee (RoleName, RoleDescription) VALUES
(N'Nhân viên', N'non'),
(N'Nghiên cứu viên', N'Nghiên cứu viên'),
(N'Nhân viên quản lý', N'Nhân viên quản lý')
go
--Bảng account employee
INSERT INTO AccountForEmployee (Username, Password, Status) VALUES 
('duy', '1', 'Active'),
('quan', '1', 'Active'),
('tanh', '1', 'Active')
go
--bảng employee
INSERT INTO Employee (
    ID_Employee, 
    ID_Role, 
    ID_Account, 
    FirstName, 
    LastName, 
    FullName, 
    ID_Card, 
    Date_of_Birth, 
    Gender, 
    Email, 
    Phone_Number, 
    Degree, 
    Addresss, 
    Join_Date, 
    Institution, 
    Department, 
    Position, 
    Research_Field
) 
VALUES (
    'NV0001', -- Mã nhân viên (Định dạng: NV0001->NV9999)
    1, -- ID_Role của Researcher
    1, -- ID_Account của Account tương ứng
    N'Huỳnh Khánh', 
    N'Duy', 
    N'Huỳnh Khánh Duy', 
    '123456789012', 
    '2002-01-01', 
    N'Nam', 
    'duy.doe@example.com', 
    '123456789', 
    'PhD', 
    '123 Main St, City, Country', 
    '2000-01-01', 
    'ABC Company', 
    'Research Department', 
    'Researcher', 
    'Biology'
);

--INSERT INTO Employee VALUES 
--('NV0002', 1, 2, N'Quân', N'Châu Minh', N'Châu Minh Quân', '29293923', '2002-01-01', N'Nam', 'quan@gmail.com', '02939292', 'ASC', N'TP HCM', '2000-01-01', 'Company', 'Rearch', 'AduGa', 'AduGa')
--thêm customer
go
-- Thêm dữ liệu vào bảng Customer
INSERT INTO Customer (ID_Customer, FirstName, LastName, FullName, Date_of_Birth, Gender, Email, Phone_Number)
VALUES 
('KH00001', N'Linh', N'Đỗ', N'Đỗ Linh', '2002-03-15', N'Nữ', 'linh@gmail.com', '123456789'),
('KH00002', N'Trưởng', N'Bùi', N'Bùi Trưởng', '2002-03-15', N'Nam', 'truong@gmail.com', '123456789')
go
-- Thêm dữ liệu vào bảng AccountForCustomer
INSERT INTO AccountForCustomer (ID, Username, Password, Status)
VALUES
(1, 'linh', '123', 'Active'),
(2, 'truong', '123', 'Active');
go


select *  from Strain
select getdate()

select * from RoleForEmployee

select * from AccountForEmployee
select * from Employee

select * from Customer
select * from AccountForCustomer

select * from Genus --cao nhất	
select * from Class -- nhì
Select * from Phylum -- ba