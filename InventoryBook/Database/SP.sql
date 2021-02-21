USE [InventoryBook]
GO

CREATE PROCEDURE [uspSelectCategories]
AS
BEGIN
	SELECT * 
	FROM [Category]
END
GO

CREATE PROCEDURE [uspDeleteCategoryByID]
	@CategoryID [int]
AS
BEGIN	
	DELETE FROM [Category] 
	WHERE [CategoryID] = @CategoryID;
END
GO

CREATE PROCEDURE [uspInsertCategory]
(
    @Name [nvarchar](50)
)
AS
BEGIN
	INSERT INTO [Category] ([Name])
	VALUES (@Name)
END
GO

CREATE PROCEDURE [uspDeleteUserByID]
	@UserID [int]
AS
BEGIN
	DELETE FROM [User] 
	WHERE [UserID] = @UserID
END
GO

CREATE PROCEDURE [uspInsertUser]
(
    @Login [nvarchar](50),
	@Role [int],
	@PasswordHash [varbinary](max),
	@PasswordSalt [varbinary](max)
)
AS
BEGIN
	INSERT INTO [User] 
		([Login]
		,[Role]
		,[PasswordHash]
		,[PasswordSalt]) 
	VALUES (@Login, @Role, @PasswordHash, @PasswordSalt)
END
GO

CREATE PROCEDURE [uspIsLoginExists]
(
	@Login [nvarchar](50)
)
AS
BEGIN
    IF EXISTS (SELECT * 
				FROM [User] 
				WHERE LOWER([Login]) = LOWER(@Login))
        SELECT 1
    ELSE
        SELECT 0  
END
GO

CREATE PROCEDURE [uspIsLoginExistsForUpdate]
(
	@UserID [int],
	@Login [nvarchar](50)
)
AS
BEGIN
	IF EXISTS (SELECT * 
				FROM [User] 
				WHERE LOWER([Login]) = LOWER(@Login) 
				AND @UserID <> [UserID])
        SELECT 1
    ELSE
        SELECT 0  
END
GO

CREATE PROCEDURE [uspSelectUsers]
AS
BEGIN
	SELECT [UserID], [Login], [Role]
	FROM [User]
END
GO

CREATE PROCEDURE [uspSelectUserByID]
	@UserID [int]
AS
BEGIN
	SELECT [Login], [Role] 
	FROM [User] 
	WHERE [UserID] = @UserID
END
GO

CREATE PROCEDURE [uspSelectHashByLogin]
	@Login [nvarchar](50)
AS
BEGIN
	SELECT [PasswordHash]
	FROM [User] 
	WHERE LOWER([Login]) = LOWER(@Login)
END
GO

CREATE PROCEDURE [uspSelectRoleForLogin]
(
	@Login [nvarchar](50),
	@PasswordHash [varbinary](max)
)
AS
BEGIN
	SELECT [UserID],[Role] 
	FROM [User] 
	WHERE LOWER([Login]) = LOWER(@Login) 
	AND [PasswordHash] = @PasswordHash
END
GO

CREATE PROCEDURE [uspSelectSaltByLogin]
	@Login [nvarchar](50)
AS
BEGIN
	SELECT [PasswordSalt]
	FROM [User] 
	WHERE LOWER([Login]) = LOWER(@Login)
END
GO

CREATE PROCEDURE [uspUpdateUser]
(
	@UserID [int],
    @Login [nvarchar](50),
	@Role [int]
)
AS
BEGIN
	UPDATE [User] 
	SET [Login]=@Login
		,[Role]=@Role 
	WHERE [UserID]=@UserID
END
GO

CREATE PROCEDURE [uspDeleteItemByID]
	@ItemID [int]
AS
DECLARE @CategoryID [int]
BEGIN	
	SET @CategoryID = (SELECT [CategoryID] 
						FROM [Item] 
						WHERE [ItemID] = @ItemID);
	DELETE FROM [Item] 
	WHERE [ItemID] = @ItemID;
	IF NOT EXISTS (SELECT * 
					FROM [Item] 
					WHERE [CategoryID] = @CategoryID)
        EXEC [uspDeleteCategoryByID] @CategoryID = @CategoryID   
END
GO

CREATE PROCEDURE [uspInsertItem]
(
    @ItemName [nvarchar](50),
	@Condition [int],
	@CategoryName [nvarchar](50),
	@Description [nvarchar](MAX),
	@Number [nvarchar](50)

)
AS
DECLARE @CategoryID [nvarchar](50)
BEGIN
	IF NOT EXISTS (SELECT * 
					FROM [Category] 
					WHERE LOWER([Name]) = LOWER(@CategoryName))
		EXEC [uspInsertCategory] @Name = @CategoryName;
	SET @CategoryID = (SELECT [CategoryID]
						FROM [Category] 
						WHERE LOWER([Name]) = LOWER(@CategoryName))
	INSERT INTO [Item] 
		([Name]
		,[Number]
		,[Condition]
		,[CategoryID]
		,[Description]) 
	VALUES (@ItemName, @Number, @Condition, @CategoryID, @Description)
END
GO

CREATE PROCEDURE [uspSelectItems]
AS
BEGIN
	SELECT [Item].[ItemID], [Item].[Name], [Item].[Number], [Item].[Condition], [Item].[CategoryID], [Item].[Description],
	[Category].[Name] AS [CategoryName]
	FROM [Item] 
		INNER JOIN [Category] 
		ON [Item].[CategoryID] = [Category].[CategoryID]
END
GO

CREATE PROCEDURE [uspSelectItemByID]
	@ItemID [int]
AS
BEGIN
	SELECT [Item].[ItemID], [Item].[Name], [Item].[Number], [Item].[Condition], [Item].[CategoryID], [Item].[Description],
	[Category].[Name] AS [CategoryName]
	FROM [Item] 
		INNER JOIN [Category] 
		ON [Item].[CategoryID] = [Category].[CategoryID]
	WHERE Item.[ItemID] = @ItemID
END
GO

CREATE PROCEDURE [uspSelectItemsByFilter]
(
	@Filter [int],
	@SearchText [nvarchar](max)
)
AS
BEGIN
	SELECT [Item].[ItemID], [Item].[Name], [Item].[Number], [Item].[Condition], [Item].[CategoryID], [Item].[Description],
	[Category].[Name] AS [CategoryName]
	FROM [Item] 
		INNER JOIN [Category] 
		ON [Item].[CategoryID] = [Category].[CategoryID]
	WHERE 
	(@Filter = 0 AND [Item].[Description] LIKE '%' + @SearchText + '%')
	OR (@Filter = 1 AND [Item].[Name] LIKE '%' + @SearchText + '%')
	OR (@Filter = 2 AND [Item].[Number] LIKE '%' + @SearchText + '%')
	OR (@Filter = 3 AND [Category].[Name] LIKE '%' + @SearchText + '%')
END
GO

CREATE PROCEDURE [uspUpdateItem]
(
	@ItemID [int],
	@Condition [int],
	@CategoryName [nvarchar](50),
	@Description [nvarchar](MAX),
    @ItemName [nvarchar](50),
	@Number [nvarchar](50)
)
AS
DECLARE @CategoryID [int], @OldCategoryID [int]
BEGIN
  
	SET @CategoryID = (SELECT [CategoryID] 
						FROM [Item] 
						WHERE [ItemID] = @ItemID);
	SET @OldCategoryID = @CategoryID;
	IF NOT EXISTS (SELECT [Item].[CategoryID], [Category].[Name] 
					FROM [Item] 
						INNER JOIN [Category] 
						ON [Item].[CategoryID] = [Category].[CategoryID]
					WHERE [Item].[CategoryID] = @CategoryID 
					AND LOWER([Category].[Name]) = LOWER(@CategoryName))
		BEGIN
			IF NOT EXISTS (SELECT * 
							FROM [Category] 
							WHERE LOWER([Name]) = LOWER(@categoryName))
				EXEC [uspInsertCategory] @Name = @CategoryName;
				SET @CategoryID = (SELECT [CategoryID] 
									FROM [Category] 
									WHERE LOWER([Name]) = LOWER(@CategoryName))
		END
	UPDATE [Item] 
	SET [Name] = @ItemName
		,[Number] = @Number
		,[Condition] = @Condition
		,[CategoryId] = @CategoryID
		,[Description] = @Description 
	WHERE [ItemID]=@ItemID;
	IF NOT EXISTS (SELECT * 
					FROM [Item]	
					WHERE [CategoryID] = @OldCategoryID)
		EXEC [uspDeleteCategoryByID] @CategoryID = @OldCategoryID 
END
GO