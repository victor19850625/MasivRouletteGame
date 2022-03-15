-- =============================================
-- Author:		Victor Bravo
-- Create date: 8-March-22 
-- Description:	Stored Procedure User_i
-- =============================================

CREATE PROCEDURE [dbo].[User_i]
	@IdUser [BIGINT] OUTPUT,
	@RegisterUser [DATETIME] OUTPUT,
	@NameUser [VARCHAR](100),
	@EmailUser [VARCHAR](200),
	@CreditUser [DECIMAL](12, 2)
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
