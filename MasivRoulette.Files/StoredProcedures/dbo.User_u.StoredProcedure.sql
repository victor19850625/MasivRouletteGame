
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE OBJECT_ID = OBJECT_ID(N'[dbo].[User_u]') AND type in (N'P', N'PC'))
BEGIN
	EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[User_u] AS' 
END
GO
-- =============================================
-- Author:		Victor Bravo
-- Create date: 8-March-22 
-- Description:	Stored Procedure User_u
-- =============================================
ALTER PROCEDURE [dbo].[User_u]
	@IdUser [BigInt],
	@NameUser [varchar](100) = NULL,
	@EmailUser [varchar](200) = NULL,
	@CreditUser [DECIMAL](20, 2) = NULL,
	@StateUser [bit] = NULL
AS
BEGIN	
	SET NOCOUNT ON;
	UPDATE [Users] 
		SET	
			NameUser  = CASE
                              WHEN @NameUser IS NULL
                              THEN [NameUser]
                              ELSE @NameUser
                          END,
			EmailUser  = CASE
                              WHEN @EmailUser IS NULL
                              THEN [EmailUser]
                              ELSE @EmailUser
                          END,
			CreditUser  = CASE
                              WHEN @CreditUser IS NULL
                              THEN [CreditUser]
                              ELSE @CreditUser
                          END,
			StateUser  = CASE
                              WHEN @StateUser IS NULL
                              THEN [StateUser]
                              ELSE @StateUser
                          END,
			LastModify = GETDATE()
			
		FROM [Users] WITH(NOLOCK)
		WHERE [IdUser] = @IdUser		
		
		SELECT * FROM Users WHERE [IdUser] = @IdUser
END;
GO