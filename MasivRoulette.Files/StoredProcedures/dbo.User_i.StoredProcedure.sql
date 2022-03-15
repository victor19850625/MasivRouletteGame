
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE OBJECT_ID = OBJECT_ID(N'[dbo].[User_i]') AND type in (N'P', N'PC'))
BEGIN
	EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[User_i] AS' 
END
GO
-- =============================================
-- Author:		Victor Bravo
-- Create date: 8-March-22 
-- Description:	Stored Procedure User_i
-- =============================================

ALTER PROCEDURE [dbo].[User_i]
	@IdUser [BIGINT] OUTPUT,
	@RegisterUser [DATETIME] OUTPUT,
	@NameUser [VARCHAR](100),
	@EmailUser [VARCHAR](200),
	@CreditUser [DECIMAL](20, 2)
AS
BEGIN
	SET NOCOUNT ON;
	
	DECLARE @Now DATETIME = GETDATE()
	INSERT INTO [Users]	(
		[RegisterUser],
		[NameUser],
		[EmailUser],
		[CreditUser],
		[StateUser],
        [LastModify] )
		VALUES (
		@Now,
		@NameUser,
		@EmailUser,
		@CreditUser,
		1,
		NULL  );
	SELECT @IdUser = SCOPE_IDENTITY();
	SELECT @RegisterUser = @Now;
END;
GO

	
