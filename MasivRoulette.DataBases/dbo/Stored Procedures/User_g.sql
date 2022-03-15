-- =============================================
-- Author:		Victor Bravo
-- Create date: 8-March-22 
-- Description:	Stored Procedure User_g
-- =============================================
CREATE PROCEDURE [dbo].[User_g]
	@IdUser [BigInt]
AS
BEGIN	
	SET NOCOUNT ON;
	SELECT *
		FROM [Users] WITH(NOLOCK)
		WHERE [IdUser] = @IdUser
END;
