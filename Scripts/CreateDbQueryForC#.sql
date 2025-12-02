-- СОЗДАНИЕ БАЗЫ ДАННЫХ
USE master;
GO

IF EXISTS (SELECT name FROM sys.databases WHERE name = 'TechnoTransDb')
BEGIN
    ALTER DATABASE TechnoTransDb SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE TechnoTransDb;
END
GO

CREATE DATABASE TechnoTransDb;
GO

USE TechnoTransDb;
GO

ALTER DATABASE TechnoTransDb COLLATE Cyrillic_General_CI_AS;
GO

-- СОЗДАНИЕ ТАБЛИЦЫ
CREATE TABLE RepairRequests (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    CreationDate DATETIME NOT NULL DEFAULT GETDATE(),
    CarType NVARCHAR(100) NOT NULL,
    CarModel NVARCHAR(100) NOT NULL,
    ProblemDescription NVARCHAR(MAX) NOT NULL,
    ClientName NVARCHAR(200) NOT NULL,
    PhoneNumber NVARCHAR(20) NOT NULL,
    Status INT NOT NULL DEFAULT 0,
    ResponsibleMechanic NVARCHAR(200) NULL,
    Comments NVARCHAR(MAX) NULL,
    SpareParts NVARCHAR(MAX) NULL
);
GO

-- ИНДЕКСЫ
CREATE INDEX IX_RepairRequests_CreationDate ON RepairRequests(CreationDate);
CREATE INDEX IX_RepairRequests_Status ON RepairRequests(Status);
GO

-- ЗАПОЛНЕНИЕ 500 ЗАПИСЯМИ
DECLARE @counter INT = 1;

WHILE @counter <= 500
BEGIN
    INSERT INTO RepairRequests (
        CreationDate,
        CarType,
        CarModel,
        ProblemDescription,
        ClientName,
        PhoneNumber,
        Status,
        ResponsibleMechanic,
        Comments,
        SpareParts
    )
    VALUES (
        DATEADD(DAY, -CAST(RAND() * 60 AS INT), GETDATE()),
        
        CASE CAST(RAND() * 5 AS INT)
            WHEN 0 THEN N'Седан'
            WHEN 1 THEN N'Универсал'
            WHEN 2 THEN N'Хэтчбек'
            WHEN 3 THEN N'Внедорожник'
            WHEN 4 THEN N'Минивэн'
            ELSE N'Купе'
        END,
        
        CASE CAST(RAND() * 8 AS INT)
            WHEN 0 THEN N'Mercedes C-class'
            WHEN 1 THEN N'Audi A4'
            WHEN 2 THEN N'BMW 3 Series'
            WHEN 3 THEN N'Toyota Camry'
            WHEN 4 THEN N'Honda Accord'
            WHEN 5 THEN N'Volkswagen Passat'
            WHEN 6 THEN N'Hyundai Sonata'
            WHEN 7 THEN N'Skoda Octavia'
            ELSE N'Kia Optima'
        END,
        
        CASE CAST(RAND() * 7 AS INT)
            WHEN 0 THEN N'Не заводится двигатель'
            WHEN 1 THEN N'Стук в подвеске'
            WHEN 2 THEN N'Проблемы с тормозами'
            WHEN 3 THEN N'Течет масло'
            WHEN 4 THEN N'Не работает кондиционер'
            WHEN 5 THEN N'Проблемы с коробкой передач'
            WHEN 6 THEN N'Электронные неисправности'
            ELSE N'Кузовной ремонт'
        END,
        
        CASE CAST(RAND() * 10 AS INT)
            WHEN 0 THEN N'Иванов Иван'
            WHEN 1 THEN N'Петров Петр'
            WHEN 2 THEN N'Сидоров Алексей'
            WHEN 3 THEN N'Кузнецова Мария'
            WHEN 4 THEN N'Смирнов Дмитрий'
            WHEN 5 THEN N'Попова Анна'
            WHEN 6 THEN N'Васильев Сергей'
            WHEN 7 THEN N'Новикова Екатерина'
            WHEN 8 THEN N'Федоров Андрей'
            WHEN 9 THEN N'Морозова Ольга'
            ELSE N'Волков Павел'
        END,
        
        '+79' + RIGHT('00000000' + CAST(CAST(RAND() * 100000000 AS BIGINT) AS VARCHAR(8)), 8),
        
        CASE CAST(RAND() * 4 AS INT)
            WHEN 0 THEN 0
            WHEN 1 THEN 1
            WHEN 2 THEN 2
            ELSE 3
        END,
        
        CASE WHEN RAND() > 0.3 THEN 
            CASE CAST(RAND() * 5 AS INT)
                WHEN 0 THEN N'Петров П.А.'
                WHEN 1 THEN N'Сидоров М.В.'
                WHEN 2 THEN N'Козлов И.С.'
                WHEN 3 THEN N'Никитин А.П.'
                ELSE N'Фомин Д.К.'
            END
        ELSE NULL END,
        
        CASE WHEN RAND() > 0.4 THEN 
            CASE CAST(RAND() * 5 AS INT)
                WHEN 0 THEN N'Требуется диагностика'
                WHEN 1 THEN N'Клиент ждет звонка'
                WHEN 2 THEN N'Необходима замена деталей'
                WHEN 3 THEN N'Ремонт по гарантии'
                WHEN 4 THEN N'Срочный ремонт'
                ELSE N'Обычный сервис'
            END
        ELSE NULL END,
        
        CASE WHEN RAND() > 0.5 THEN 
            CASE CAST(RAND() * 6 AS INT)
                WHEN 0 THEN N'Тормозные колодки, диски'
                WHEN 1 THEN N'Аккумулятор, свечи'
                WHEN 2 THEN N'Фильтры, масло'
                WHEN 3 THEN N'Амортизаторы, пружины'
                WHEN 4 THEN N'ШРУС, сайлентблоки'
                WHEN 5 THEN N'Генератор, стартер'
                ELSE N'Датчики, проводка'
            END
        ELSE NULL END
    );
    
    SET @counter = @counter + 1;
END;
GO

SELECT 'База создана, добавлено 500 записей' as Результат;
GO