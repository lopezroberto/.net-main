-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetArtistDetails] 
	@artistID INT = 1
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	-- Artist Info
	SELECT a.title as artistName, a.heroURL, a.imageURL, a.biography, b.title as albumTitle
	FROM Artist AS a
	INNER JOIN Album AS b on b.artistID = a.artistID
	WHERE a.artistID = @artistID

	-- Top Songs
	-- TODO: Review bpm
	-- TODO: Review time signaturen
	-- TODO: Review criteria for top songs
	SELECT TOP 3 
		s.albumID, s.title AS songTitle, floor(s.bpm) as bpmConverted,  
		CASE   
		WHEN s.timeSignature = 3 THEN '4/4'   
		WHEN s.timeSignature = 13 THEN '3/4'   
		WHEN s.timeSignature = 1 THEN '2/4'   
		WHEN s.timeSignature = 18 THEN '6/8'   
		END AS 'timeSignatureString',
		a.imageURL, a.title AS albumTitle 
	FROM Song AS s
	INNER JOIN Album AS a on a.albumID = s.albumID
	WHERE s.artistID = @artistID 
	ORDER BY s.multitracks DESC

	-- Albums Info
	SELECT a.title AS albumTitle, a.imageURL, b.title AS artistTitle 
	FROM Album AS a
	INNER JOIN Artist AS b on b.artistID = a.artistID
	WHERE a.artistID = @artistID
END