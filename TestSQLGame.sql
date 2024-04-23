
go
USE GAME;
go
CREATE TABLE GAME
(
    GameId INT PRIMARY KEY IDENTITY,
    GameName NVARCHAR(100),
    Price FLOAT,
    Description NVARCHAR(MAX)
);
go
-- Tạo bảng Genre
CREATE TABLE Genre (
    GenreId INT PRIMARY KEY IDENTITY,
    GenreName NVARCHAR(255) NOT NULL
);
go
-- Tạo bảng GameGenre (bảng trung gian giữa Game và Genre)
CREATE TABLE GameGenre (
    GameId INT,
    GenreId INT,
    PRIMARY KEY (GameId, GenreId),
    FOREIGN KEY (GameId) REFERENCES Game(GameId),
    FOREIGN KEY (GenreId) REFERENCES Genre(GenreId)
);
go
INSERT INTO GAME (GameName, Price, Description)
VALUES 
('DeadCell', 49.99, 'Awesome game with stunning graphics and immersive gameplay.'),
('Gunny', 59.99, 'Exciting AAA title featuring cutting-edge mechanics and thrilling storylines.'),
('Minecraft', 39.99, 'Popular AAA game known for its multiplayer action and intense battles.'),
('Elden Ring', 69.99, 'Critically acclaimed AAA title with expansive open-world exploration.'),
('Grounded', 54.99, 'Award-winning AAA game offering hours of entertainment and replayability.');
go
-- Thêm dữ liệu vào bảng Genre
INSERT INTO Genre (GenreName) VALUES ('Action');
INSERT INTO Genre (GenreName) VALUES ('Survival');
INSERT INTO Genre (GenreName) VALUES ('FPS');
INSERT INTO Genre (GenreName) VALUES ('RPG');
INSERT INTO Genre (GenreName) VALUES ('Adventure');
GO

INSERT INTO GameGenre (GameId, GenreId) VALUES
(1,1),
(1,2),
(2,2),
(2,4),
(3,5),
(4,1),
(5,4)
go
select * from game
go
-- Lấy tên thể loại của một trò chơi cụ thể (ví dụ, trò chơi có GameId = 1) mà không sử dụng JOIN
SELECT Genre.GenreName
FROM Genre
WHERE Genre.GenreId IN (SELECT GameGenre.GenreId FROM GameGenre WHERE GameGenre.GameId = 1);