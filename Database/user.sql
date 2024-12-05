USE [ClewbayFarm]
CREATE LOGIN ApiUser WITH PASSWORD = 'StrongPasswordHere';
GO
/****** Object:  User [ApiUser]    Script Date: 05/12/2024 16:06:06 ******/
CREATE USER [ApiUser] FOR LOGIN [ApiUser] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_datareader] ADD MEMBER [ApiUser]
GO
ALTER ROLE [db_datawriter] ADD MEMBER [ApiUser]
GO
-- Create a SQL Server Login

