INSERT INTO TReader VALUES('1','ZS',1,GETDATE(),'CS',4,null,null);
INSERT INTO TReader VALUES('2','LS',0,GETDATE(),'PS',2,null,null);

INSERT INTO TBook VALUES('1','����','����','����ʦ����ѧ������',20.00,5,3,null,null);
INSERT INTO TBook VALUES('2','�ߵ���ѧ','����ʦ����ѧϵ','����ʦ����ѧ������',35.00,4,2,null,null);
INSERT INTO TBook VALUES('3','����ѧ������','��Ͷ�','�����ʵ������',79.00,4,3,null,null);
INSERT INTO TBook VALUES('4','�������','ĪС��','�Ϻ����������',36.00,3,2,null,null);

INSERT INTO TLend VALUES('1','1','1',GETDATE());
INSERT INTO TLend VALUES('5','1','2',GETDATE());
INSERT INTO TLend VALUES('13','1','3',GETDATE());
INSERT INTO TLend VALUES('26','1','4',GETDATE());
INSERT INTO TLend VALUES('2','2','1',GETDATE());
INSERT INTO TLend VALUES('6','2','2',GETDATE());
