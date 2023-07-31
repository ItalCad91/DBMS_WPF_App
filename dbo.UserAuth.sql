CREATE TABLE [dbo].[userAuth]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [userID] INT NULL, 
    [UserName] VARCHAR(50) NULL, 
    [Email] VARCHAR(50) NULL, 
    [Password] VARCHAR(50) NULL
)
