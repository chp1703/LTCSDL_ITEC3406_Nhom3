create database QuanLyQuanCafe
go

use QuanLyQuanCafe
go

CREATE TABLE [dbo].[FoodCategory](
	[id] [int] IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[name] [nvarchar](100) NOT NULL,
	--CONSTRAINT FoodCategory_PK PRIMARY KEY (id)
	)
GO
SET IDENTITY_INSERT [dbo].[FoodCategory] ON
INSERT [dbo].[FoodCategory] ([id], [name]) VALUES (1, N'Cafe pha phin')
INSERT [dbo].[FoodCategory] ([id], [name]) VALUES (2, N'Trà')
INSERT [dbo].[FoodCategory] ([id], [name]) VALUES (3, N'Freeze')
INSERT [dbo].[FoodCategory] ([id], [name]) VALUES (4, N'Bánh mì')
INSERT [dbo].[FoodCategory] ([id], [name]) VALUES (5, N'Trà sữa')
INSERT [dbo].[FoodCategory] ([id], [name]) VALUES (6, N'Soda')
INSERT [dbo].[FoodCategory] ([id], [name]) VALUES (7, N'Đá xay')
INSERT [dbo].[FoodCategory] ([id], [name]) VALUES (8, N'Yaourt')
INSERT [dbo].[FoodCategory] ([id], [name]) VALUES (9, N'Đồ ăn nhanh')
INSERT [dbo].[FoodCategory] ([id], [name]) VALUES (10, N'Thức uống khác')
SET IDENTITY_INSERT [dbo].[FoodCategory] OFF

CREATE TABLE [dbo].[Food](
	[id] [int] IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[name] [nvarchar](100) NOT NULL,
	[idCategory] [int] NOT NULL,
	[price] [float] NOT NULL,
	CONSTRAINT fk_Food_FoodCategory
	FOREIGN KEY (idCategory)
	REFERENCES FoodCategory	(id)    
	)
GO


SET IDENTITY_INSERT [dbo].[Food] ON
INSERT [dbo].[Food] ([id], [name], [idCategory], [price]) VALUES (1, N'Phin Sữa Đá', 1, 29000)
INSERT [dbo].[Food] ([id], [name], [idCategory], [price]) VALUES (2, N'Phin Đen Đá', 1, 29000)
INSERT [dbo].[Food] ([id], [name], [idCategory], [price]) VALUES (3, N'Bạc Xỉu Đá', 1, 29000)
INSERT [dbo].[Food] ([id], [name], [idCategory], [price]) VALUES (4, N'Trà Thạch Đào', 2, 39000)
INSERT [dbo].[Food] ([id], [name], [idCategory], [price]) VALUES (5, N'Trà Đào Cam Xả', 2, 29000)
INSERT [dbo].[Food] ([id], [name], [idCategory], [price]) VALUES (6, N'Trà Thạch Vải', 2, 39000)
INSERT [dbo].[Food] ([id], [name], [idCategory], [price]) VALUES (7, N'Trà Xanh Đậu Đỏ', 2, 29000)

INSERT [dbo].[Food] ([id], [name], [idCategory], [price]) VALUES (8, N'Freeze Trà Xanh', 3, 49000)
INSERT [dbo].[Food] ([id], [name], [idCategory], [price]) VALUES (9, N'Freeze Sô-co-la', 3, 49000)
INSERT [dbo].[Food] ([id], [name], [idCategory], [price]) VALUES (10, N'Cookies & Cream', 3, 59000)
INSERT [dbo].[Food] ([id], [name], [idCategory], [price]) VALUES (11, N'Caramel Phin Freeze', 3, 49000)

INSERT [dbo].[Food] ([id], [name], [idCategory], [price]) VALUES (12, N'Bánh Mì Gà Xé', 4, 19000)
INSERT [dbo].[Food] ([id], [name], [idCategory], [price]) VALUES (13, N'Bánh Mì Thịt Nướng', 4, 19000)
INSERT [dbo].[Food] ([id], [name], [idCategory], [price]) VALUES (14, N'Bánh Mì Pate', 4, 19000)
INSERT [dbo].[Food] ([id], [name], [idCategory], [price]) VALUES (15, N'Bánh Mì Cá Ngừ', 4, 19000)

INSERT [dbo].[Food] ([id], [name], [idCategory], [price]) VALUES (16, N'Trà Sữa Trân Trâu Đường Đen', 5, 30000)
INSERT [dbo].[Food] ([id], [name], [idCategory], [price]) VALUES (17, N'Trà Sữa Phomai Kem Sữa', 5, 35000)
INSERT [dbo].[Food] ([id], [name], [idCategory], [price]) VALUES (18, N'Trà Sữa Thái Xanh', 5, 30000)
INSERT [dbo].[Food] ([id], [name], [idCategory], [price]) VALUES (19, N'Trà Sữa Việt Quất', 5, 30000)
INSERT [dbo].[Food] ([id], [name], [idCategory], [price]) VALUES (20, N'Trà Sữa Machiato', 5, 30000)

INSERT [dbo].[Food] ([id], [name], [idCategory], [price]) VALUES (21, N'Soda Việt Quất', 6, 31000)
INSERT [dbo].[Food] ([id], [name], [idCategory], [price]) VALUES (22, N'Soda Kiwi ', 6, 31000)
INSERT [dbo].[Food] ([id], [name], [idCategory], [price]) VALUES (23, N'Soda Táo Xanh', 6, 31000)

INSERT [dbo].[Food] ([id], [name], [idCategory], [price]) VALUES (24, N'Cookie Cream', 7, 28000)
INSERT [dbo].[Food] ([id], [name], [idCategory], [price]) VALUES (25, N'Cafe Đá Xay', 7, 25000)
INSERT [dbo].[Food] ([id], [name], [idCategory], [price]) VALUES (26, N'Cookie Matcha', 7, 32000)

INSERT [dbo].[Food] ([id], [name], [idCategory], [price]) VALUES (27, N'Yaourt Đá', 8, 15000)
INSERT [dbo].[Food] ([id], [name], [idCategory], [price]) VALUES (28, N'Yaourt Việt Quất', 8, 20000)
INSERT [dbo].[Food] ([id], [name], [idCategory], [price]) VALUES (29, N'Yaourt Dâu', 8, 20000)
INSERT [dbo].[Food] ([id], [name], [idCategory], [price]) VALUES (30, N'Yaourt Chanh Dây', 8, 15000)

INSERT [dbo].[Food] ([id], [name], [idCategory], [price]) VALUES (31, N'Tiramisu', 9, 19000)
INSERT [dbo].[Food] ([id], [name], [idCategory], [price]) VALUES (32, N'Bánh Chuối', 9, 10000)
INSERT [dbo].[Food] ([id], [name], [idCategory], [price]) VALUES (33, N'Phomai Trà Xanh', 9, 29000)
INSERT [dbo].[Food] ([id], [name], [idCategory], [price]) VALUES (34, N'Khoai Tây Chiên', 9, 10000)

INSERT [dbo].[Food] ([id], [name], [idCategory], [price]) VALUES (35, N'7Up', 10, 15000)
INSERT [dbo].[Food] ([id], [name], [idCategory], [price]) VALUES (36, N'Sting Dâu', 10, 15000)
INSERT [dbo].[Food] ([id], [name], [idCategory], [price]) VALUES (37, N'Coca Cola', 10, 15000)
SET IDENTITY_INSERT [dbo].[Food] OFF


CREATE TABLE [dbo].[TableFood](
	[id] [int] IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[name] [nvarchar](100) NOT NULL default N'Bàn chưa có tên',
	[status] [nvarchar](100) NOT NULL default N'Trống',
	)
GO

SET IDENTITY_INSERT [dbo].[TableFood] ON
INSERT [dbo].[TableFood] ([id], [name], [status]) VALUES (1, N'Mang về', N'Trống')
INSERT [dbo].[TableFood] ([id], [name], [status]) VALUES (2, N'Bàn 1', N'Trống')
INSERT [dbo].[TableFood] ([id], [name], [status]) VALUES (3, N'Bàn 2', N'Trống')
INSERT [dbo].[TableFood] ([id], [name], [status]) VALUES (4, N'Bàn 3', N'Trống')
INSERT [dbo].[TableFood] ([id], [name], [status]) VALUES (5, N'Bàn 4', N'Trống')
INSERT [dbo].[TableFood] ([id], [name], [status]) VALUES (6, N'Bàn 5', N'Trống')
INSERT [dbo].[TableFood] ([id], [name], [status]) VALUES (7, N'Bàn 6', N'Trống')
INSERT [dbo].[TableFood] ([id], [name], [status]) VALUES (8, N'Bàn 7', N'Trống')
INSERT [dbo].[TableFood] ([id], [name], [status]) VALUES (9, N'Bàn 8', N'Trống')
INSERT [dbo].[TableFood] ([id], [name], [status]) VALUES (10, N'Bàn 9', N'Trống')
INSERT [dbo].[TableFood] ([id], [name], [status]) VALUES (11, N'Bàn 10', N'Trống')

CREATE TABLE [dbo].[Account](
	[UserName] [nvarchar](100) PRIMARY KEY NOT NULL,
	[DisplayName] [nvarchar](100) NOT NULL,
	[PassWord] [nvarchar](1000) NOT NULL,
	[Type] [int] NOT NULL,
	)
GO


INSERT [dbo].[Account] ([UserName], [DisplayName], [PassWord], [Type]) 
VALUES (N'Admin', N'Admin', N'1', 1)

SELECT * from dbo.Account




CREATE TABLE [dbo].[Bill](
	[id] [int] IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[DateCheckIn] [date] NOT NULL,
	[DateCheckOut] [date] NULL,
	[idTable] [int] NOT NULL,
	[status] [int] NOT NULL,
	[discount] [int] NULL,
	[totalPrice] [float] NULL,
	CONSTRAINT fk_Bill_Table
	FOREIGN KEY (idTable)
	REFERENCES TableFood	(id)    
	)
GO

CREATE TABLE [dbo].[BillInfo](
	[id] [int] IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[idBill] [int] NOT NULL,
	[idFood] [int] NOT NULL,
	[count] [int] NOT NULL,
	CONSTRAINT fk_BillInfo_Food
	FOREIGN KEY (idFood)
	REFERENCES Food	(id), 
	CONSTRAINT fk_BillInfo_Bill
	FOREIGN KEY (idBill)
	REFERENCES Bill	(id),    
	)
GO


CREATE PROC [dbo].[USP_GetAccountByUserName]
@userName nvarchar(100)
AS 
BEGIN
	SELECT * FROM dbo.Account WHERE UserName = @userName
END
GO

CREATE PROC [USP_Login]
@userName nvarchar(100), @passWord nvarchar(100)
AS
BEGIN
	SELECT * FROM dbo.Account WHERE UserName = @userName AND PassWord = @passWord
END
GO

CREATE PROC [dbo].[USP_GetTableList]
AS SELECT * FROM dbo.TableFood
GO

CREATE PROC [dbo].[USP_InsertBill]
@idTable INT
AS
BEGIN
	INSERT dbo.Bill 
	        ( DateCheckIn ,
	          DateCheckOut ,
	          idTable ,
	          status,
	          discount
	        )
	VALUES  ( GETDATE() , -- DateCheckIn - date
	          NULL , -- DateCheckOut - date
	          @idTable , -- idTable - int
	          0,  -- status - int
	          0
	        )
END
GO

CREATE PROC [dbo].[USP_GetListBillByDate]
@checkIn date, @checkOut date
AS 
BEGIN
	SELECT t.name AS [Tên bàn], b.totalPrice AS [Tổng tiền], DateCheckIn AS [Ngày vào], DateCheckOut AS [Ngày ra], discount AS [Giảm giá]
	FROM dbo.Bill AS b,dbo.TableFood AS t
	WHERE DateCheckIn >= @checkIn AND DateCheckOut <= @checkOut AND b.status = 1
	AND t.id = b.idTable
END
GO

CREATE PROC [dbo].[USP_SwitchTabel]
@idTable1 INT, @idTable2 int
AS BEGIN

	DECLARE @idFirstBill int
	DECLARE @idSeconrdBill INT
	
	DECLARE @isFirstTablEmty INT = 1
	DECLARE @isSecondTablEmty INT = 1
	
	
	SELECT @idSeconrdBill = id FROM dbo.Bill WHERE idTable = @idTable2 AND status = 0
	SELECT @idFirstBill = id FROM dbo.Bill WHERE idTable = @idTable1 AND status = 0
	
	PRINT @idFirstBill
	PRINT @idSeconrdBill
	PRINT '-----------'
	
	IF (@idFirstBill IS NULL)
	BEGIN
		PRINT '0000001'
		INSERT dbo.Bill
		        ( DateCheckIn ,
		          DateCheckOut ,
		          idTable ,
		          status
		        )
		VALUES  ( GETDATE() , -- DateCheckIn - date
		          NULL , -- DateCheckOut - date
		          @idTable1 , -- idTable - int
		          0  -- status - int
		        )
		        
		SELECT @idFirstBill = MAX(id) FROM dbo.Bill WHERE idTable = @idTable1 AND status = 0
		
	END
	
	SELECT @isFirstTablEmty = COUNT(*) FROM dbo.BillInfo WHERE idBill = @idFirstBill
	
	PRINT @idFirstBill
	PRINT @idSeconrdBill
	PRINT '-----------'
	
	IF (@idSeconrdBill IS NULL)
	BEGIN
		PRINT '0000002'
		INSERT dbo.Bill
		        ( DateCheckIn ,
		          DateCheckOut ,
		          idTable ,
		          status
		        )
		VALUES  ( GETDATE() , -- DateCheckIn - date
		          NULL , -- DateCheckOut - date
		          @idTable2 , -- idTable - int
		          0  -- status - int
		        )
		SELECT @idSeconrdBill = MAX(id) FROM dbo.Bill WHERE idTable = @idTable2 AND status = 0
		
	END
	
	SELECT @isSecondTablEmty = COUNT(*) FROM dbo.BillInfo WHERE idBill = @idSeconrdBill
	
	PRINT @idFirstBill
	PRINT @idSeconrdBill
	PRINT '-----------'

	SELECT id INTO IDBillInfoTable FROM dbo.BillInfo WHERE idBill = @idSeconrdBill
	
	UPDATE dbo.BillInfo SET idBill = @idSeconrdBill WHERE idBill = @idFirstBill
	
	UPDATE dbo.BillInfo SET idBill = @idFirstBill WHERE id IN (SELECT * FROM IDBillInfoTable)
	
	DROP TABLE IDBillInfoTable
	
	IF (@isFirstTablEmty = 0)
		UPDATE dbo.TableFood SET status = N'Trống' WHERE id = @idTable2
		
	IF (@isSecondTablEmty= 0)
		UPDATE dbo.TableFood SET status = N'Trống' WHERE id = @idTable1
END
GO

CREATE PROC [dbo].[USP_InsertBillInfo]
@idBill INT, @idFood INT, @count INT
AS
BEGIN

	DECLARE @isExitsBillInfo INT
	DECLARE @foodCount INT = 1
	
	SELECT @isExitsBillInfo = id, @foodCount = b.count 
	FROM dbo.BillInfo AS b 
	WHERE idBill = @idBill AND idFood = @idFood

	IF (@isExitsBillInfo > 0)
	BEGIN
		DECLARE @newCount INT = @foodCount + @count
		IF (@newCount > 0)
			UPDATE dbo.BillInfo	SET count = @foodCount + @count WHERE idFood = @idFood
		ELSE
			DELETE dbo.BillInfo WHERE idBill = @idBill AND idFood = @idFood
	END
	ELSE
	BEGIN
		INSERT	dbo.BillInfo
        ( idBill, idFood, count )
		VALUES  ( @idBill, -- idBill - int
          @idFood, -- idFood - int
          @count  -- count - int
          )
	END
END
GO

CREATE PROC [dbo].[USP_UpdateAccount]
@userName NVARCHAR(100), @displayName NVARCHAR(100), @password NVARCHAR(100), @newPassword NVARCHAR(100)
AS
BEGIN
	DECLARE @isRightPass INT = 0
	
	SELECT @isRightPass = COUNT(*) FROM dbo.Account WHERE USERName = @userName AND PassWord = @password
	
	IF (@isRightPass = 1)
	BEGIN
		IF (@newPassword = NULL OR @newPassword = '')
		BEGIN
			UPDATE dbo.Account SET DisplayName = @displayName WHERE UserName = @userName
		END		
		ELSE
			UPDATE dbo.Account SET DisplayName = @displayName, PassWord = @newPassword WHERE UserName = @userName
	end
END
GO
CREATE FUNCTION [dbo].[fuConvertToUnsign1] ( @strInput NVARCHAR(4000) ) RETURNS NVARCHAR(4000) AS BEGIN IF @strInput IS NULL RETURN @strInput IF @strInput = '' RETURN @strInput DECLARE @RT NVARCHAR(4000) DECLARE @SIGN_CHARS NCHAR(136) DECLARE @UNSIGN_CHARS NCHAR (136) SET @SIGN_CHARS = N'ăâđêôơưàảãạáằẳẵặắầẩẫậấèẻẽẹéềểễệế ìỉĩịíòỏõọóồổỗộốờởỡợớùủũụúừửữựứỳỷỹỵý ĂÂĐÊÔƠƯÀẢÃẠÁẰẲẴẶẮẦẨẪẬẤÈẺẼẸÉỀỂỄỆẾÌỈĨỊÍ ÒỎÕỌÓỒỔỖỘỐỜỞỠỢỚÙỦŨỤÚỪỬỮỰỨỲỶỸỴÝ' +NCHAR(272)+ NCHAR(208) SET @UNSIGN_CHARS = N'aadeoouaaaaaaaaaaaaaaaeeeeeeeeee iiiiiooooooooooooooouuuuuuuuuuyyyyy AADEOOUAAAAAAAAAAAAAAAEEEEEEEEEEIIIII OOOOOOOOOOOOOOOUUUUUUUUUUYYYYYDD' DECLARE @COUNTER int DECLARE @COUNTER1 int SET @COUNTER = 1 WHILE (@COUNTER <=LEN(@strInput)) BEGIN SET @COUNTER1 = 1 WHILE (@COUNTER1 <=LEN(@SIGN_CHARS)+1) BEGIN IF UNICODE(SUBSTRING(@SIGN_CHARS, @COUNTER1,1)) = UNICODE(SUBSTRING(@strInput,@COUNTER ,1) ) BEGIN IF @COUNTER=1 SET @strInput = SUBSTRING(@UNSIGN_CHARS, @COUNTER1,1) + SUBSTRING(@strInput, @COUNTER+1,LEN(@strInput)-1) ELSE SET @strInput = SUBSTRING(@strInput, 1, @COUNTER-1) +SUBSTRING(@UNSIGN_CHARS, @COUNTER1,1) + SUBSTRING(@strInput, @COUNTER+1,LEN(@strInput)- @COUNTER) BREAK END SET @COUNTER1 = @COUNTER1 +1 END SET @COUNTER = @COUNTER +1 END SET @strInput = replace(@strInput,' ','-') RETURN @strInput END
GO