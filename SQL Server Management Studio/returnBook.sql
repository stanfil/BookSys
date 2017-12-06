USE MBOOK
GO
CREATE TRIGGER TLend_delete ON TLend
	AFTER DELETE
	AS
	BEGIN
		UPDATE TReader SET Num=Num-1 WHERE ReaderID=(SELECT ReaderID FROM deleted)
		UPDATE TBook SET SNum=SNum+1 WHERE ISBN=(SELECT ISBN FROM deleted)
	END