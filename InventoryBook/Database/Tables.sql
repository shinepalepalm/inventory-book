USE [InventoryBook]
GO

CREATE TABLE [User](
	[UserID] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[Login] [nvarchar](50) UNIQUE NOT NULL,
	[Role] [int] NOT NULL,
	[PasswordHash] [varbinary](max) NOT NULL,
	[PasswordSalt] [varbinary](max) NOT NULL,
);
GO

CREATE TABLE [Category](
	[CategoryID] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[Name] [nvarchar](50) NOT NULL
);
GO

CREATE TABLE [Item](
	[ItemID] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[Name] [nvarchar](50) NOT NULL,
	[Number] [nvarchar](50) NOT NULL,
	[Condition] [int] NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[CategoryID] [int] NOT NULL CONSTRAINT [FK_Item_Category_CategoryID] FOREIGN KEY REFERENCES [Category](CategoryID)
);
GO