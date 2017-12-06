CREATE PROCEDURE Book_Borrow @in_ReaderID char(8),@in_ISBN char(18),@in_BookID char(10),@out_str char(30) OUTPUT
AS
BEGIN
	IF NOT EXISTS(SELECT * FROM TReader WHERE ReaderID=@in_ReaderID)
	BEGIN
		SET @out_str='�ö��߲�����'
		RETURN 0
	END
	IF NOT EXISTS(SELECT * FROM TBook WHERE	ISBN=@in_ISBN)
	BEGIN
		SET @out_str='��ͼ�鲻����'
		RETURN 0
	END
	IF(SELECT Num FROM TReader WHERE ReaderID=@in_ReaderID)=5
	BEGIN
		SET @out_str='���߽��������ܴ���5'
		RETURN 0
	END
	IF(SELECT SNum FROM TBook WHERE ISBN=@in_ISBN)=0
	BEGIN 
		SET @out_str='ͼ������Ϊ0'
		RETURN 0
	END
	IF @in_ISBN IN (SELECT ISBN FROM TLend WHERE ReaderID=@in_ReaderID)
	BEGIN
		SET @out_str='�����Ѿ��������'
		RETURN 0
	END
	IF EXISTS (SELECT * FROM TLend WHERE BookID=@in_BookID)
	BEGIN
		SET @out_str='��ͼ���Ѿ����'
		RETURN 0
	END
	BEGIN TRAN
	INSERT INTO TLend VALUES(@in_BookID,@in_ReaderID,@in_ISBN,GETDATE())
	IF @@ERROR>0
	BEGIN
		ROLLBACK TRAN
		SET @out_str='ִ�й�������������'
		RETURN 0
	END
	UPDATE TReader SET Num=Num+1 WHERE ReaderID=@in_ReaderID
	IF @@ERROR>0
	BEGIN
		ROLLBACK TRAN
		SET @out_str='ִ�й�������������'
		RETURN 0
	END
	UPDATE TBook SET SNum=SNum-1 WHERE ISBN=@in_ISBN
	IF @@ERROR=0
	BEGIN
		COMMIT TRAN
		SET @out_str='����ɹ�'
		RETURN 1
	END
	ELSE
	BEGIN
		ROLLBACK TRAN
		SET @out_str='ִ�й�������������'
		RETURN 0
	END
END