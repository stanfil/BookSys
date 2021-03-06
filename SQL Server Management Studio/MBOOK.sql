USE [master]
GO
/****** Object:  Database [MBOOK]    Script Date: 2017/12/11 13:41:09 ******/
CREATE DATABASE [MBOOK]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'MBOOK', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\MBOOK.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'MBOOK_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\MBOOK_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [MBOOK] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [MBOOK].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [MBOOK] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [MBOOK] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [MBOOK] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [MBOOK] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [MBOOK] SET ARITHABORT OFF 
GO
ALTER DATABASE [MBOOK] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [MBOOK] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [MBOOK] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [MBOOK] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [MBOOK] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [MBOOK] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [MBOOK] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [MBOOK] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [MBOOK] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [MBOOK] SET  DISABLE_BROKER 
GO
ALTER DATABASE [MBOOK] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [MBOOK] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [MBOOK] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [MBOOK] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [MBOOK] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [MBOOK] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [MBOOK] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [MBOOK] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [MBOOK] SET  MULTI_USER 
GO
ALTER DATABASE [MBOOK] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [MBOOK] SET DB_CHAINING OFF 
GO
ALTER DATABASE [MBOOK] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [MBOOK] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [MBOOK] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [MBOOK] SET QUERY_STORE = OFF
GO
USE [MBOOK]
GO
ALTER DATABASE SCOPED CONFIGURATION SET IDENTITY_CACHE = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO
USE [MBOOK]
GO
/****** Object:  Table [dbo].[TReader]    Script Date: 2017/12/11 13:41:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TReader](
	[ReaderID] [char](8) NOT NULL,
	[Name] [char](8) NOT NULL,
	[Sex] [bit] NOT NULL,
	[Born] [date] NOT NULL,
	[Spec] [char](12) NOT NULL,
	[Num] [int] NOT NULL,
	[Photo] [varbinary](max) NULL,
	[Detail] [ntext] NULL,
 CONSTRAINT [PK_TReader] PRIMARY KEY CLUSTERED 
(
	[ReaderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TBook]    Script Date: 2017/12/11 13:41:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBook](
	[ISBN] [char](18) NOT NULL,
	[BookName] [char](40) NOT NULL,
	[Author] [char](16) NOT NULL,
	[Publisher] [char](30) NOT NULL,
	[Price] [float] NOT NULL,
	[CNum] [int] NOT NULL,
	[SNum] [int] NOT NULL,
	[Summary] [varchar](200) NULL,
	[Photo] [varbinary](max) NULL,
 CONSTRAINT [PK_TBook] PRIMARY KEY CLUSTERED 
(
	[ISBN] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TLend]    Script Date: 2017/12/11 13:41:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TLend](
	[BookID] [char](10) NOT NULL,
	[ReaderID] [char](8) NOT NULL,
	[ISBN] [char](18) NOT NULL,
	[LTime] [datetime] NOT NULL,
 CONSTRAINT [PK_Tlend] PRIMARY KEY CLUSTERED 
(
	[BookID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[RBL]    Script Date: 2017/12/11 13:41:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[RBL]
AS
SELECT   dbo.TReader.ReaderID, dbo.TLend.BookID, dbo.TLend.ISBN, dbo.TBook.BookName, dbo.TBook.Publisher, 
                dbo.TBook.Price, dbo.TLend.LTime
FROM      dbo.TBook INNER JOIN
                dbo.TLend ON dbo.TBook.ISBN = dbo.TLend.ISBN INNER JOIN
                dbo.TReader ON dbo.TLend.ReaderID = dbo.TReader.ReaderID
GO
ALTER TABLE [dbo].[TLend]  WITH CHECK ADD  CONSTRAINT [FK_TLend_TBook] FOREIGN KEY([ISBN])
REFERENCES [dbo].[TBook] ([ISBN])
GO
ALTER TABLE [dbo].[TLend] CHECK CONSTRAINT [FK_TLend_TBook]
GO
ALTER TABLE [dbo].[TLend]  WITH CHECK ADD  CONSTRAINT [FK_TLend_TReader] FOREIGN KEY([ReaderID])
REFERENCES [dbo].[TReader] ([ReaderID])
GO
ALTER TABLE [dbo].[TLend] CHECK CONSTRAINT [FK_TLend_TReader]
GO
/****** Object:  StoredProcedure [dbo].[Book_Borrow]    Script Date: 2017/12/11 13:41:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Book_Borrow] @in_ReaderID char(8),@in_ISBN char(18),@in_BookID char(10),@out_str char(30) OUTPUT
AS
BEGIN
	IF NOT EXISTS(SELECT * FROM TReader WHERE ReaderID=@in_ReaderID)
	BEGIN
		SET @out_str='该读者不存在'
		RETURN 0
	END
	IF NOT EXISTS(SELECT * FROM TBook WHERE	ISBN=@in_ISBN)
	BEGIN
		SET @out_str='该图书不存在'
		RETURN 0
	END
	IF(SELECT Num FROM TReader WHERE ReaderID=@in_ReaderID)=5
	BEGIN
		SET @out_str='读者借书量不能大于5'
		RETURN 0
	END
	IF(SELECT SNum FROM TBook WHERE ISBN=@in_ISBN)=0
	BEGIN 
		SET @out_str='图书库存量为0'
		RETURN 0
	END
	IF @in_ISBN IN (SELECT ISBN FROM TLend WHERE ReaderID=@in_ReaderID)
	BEGIN
		SET @out_str='读者已经借过该书'
		RETURN 0
	END
	IF EXISTS (SELECT * FROM TLend WHERE BookID=@in_BookID)
	BEGIN
		SET @out_str='该图书已经外借'
		RETURN 0
	END
	BEGIN TRAN
	INSERT INTO TLend VALUES(@in_BookID,@in_ReaderID,@in_ISBN,GETDATE())
	IF @@ERROR>0
	BEGIN
		ROLLBACK TRAN
		SET @out_str='执行过程中遇到错误'
		RETURN 0
	END
	UPDATE TReader SET Num=Num+1 WHERE ReaderID=@in_ReaderID
	IF @@ERROR>0
	BEGIN
		ROLLBACK TRAN
		SET @out_str='执行过程中遇到错误'
		RETURN 0
	END
	UPDATE TBook SET SNum=SNum-1 WHERE ISBN=@in_ISBN
	IF @@ERROR=0
	BEGIN
		COMMIT TRAN
		SET @out_str='借书成功'
		RETURN 1
	END
	ELSE
	BEGIN
		ROLLBACK TRAN
		SET @out_str='执行过程中遇到错误'
		RETURN 0
	END
END
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "TBook"
            Begin Extent = 
               Top = 174
               Left = 88
               Bottom = 314
               Right = 243
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "TLend"
            Begin Extent = 
               Top = 20
               Left = 353
               Bottom = 160
               Right = 497
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "TReader"
            Begin Extent = 
               Top = 6
               Left = 79
               Bottom = 146
               Right = 223
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'RBL'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'RBL'
GO
USE [master]
GO
ALTER DATABASE [MBOOK] SET  READ_WRITE 
GO
