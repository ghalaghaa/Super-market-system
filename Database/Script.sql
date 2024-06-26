USE [master]
GO

CREATE DATABASE [SuperMarket]
GO

USE [SuperMarket]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 11/15/2023 07:49:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Products](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ProdectName] [varchar](50) NULL,
	[Category] [varchar](50) NULL,
	[Brand] [varchar](50) NULL,
	[Price] [float] NULL,
	[QuantityInStatick] [int] NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Usres]    Script Date: 11/15/2023 07:49:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Usres](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Username] [varchar](50) NULL,
	[Password] [varchar](50) NULL,
	[UserType] [varchar](50) NULL,
	[PhoneNumber] [varchar](12) NULL,
 CONSTRAINT [PK_Usres] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[Usres] ON
INSERT [dbo].[Usres] ([ID], [Username], [Password], [UserType], [PhoneNumber]) VALUES (1, N'Amira', N'11', N'Manager', N'966541254221')
INSERT [dbo].[Usres] ([ID], [Username], [Password], [UserType], [PhoneNumber]) VALUES (2, N'Mona', N'22', N'Customer', N'966545664448')
SET IDENTITY_INSERT [dbo].[Usres] OFF
/****** Object:  Table [dbo].[PurchaseHistory]    Script Date: 11/15/2023 07:49:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PurchaseHistory](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CID] [int] NULL,
	[PID] [int] NULL,
	[UserID] [int] NULL,
	[Quantity] [int] NULL,
	[PurcgaseDate] [date] NULL,
	[PaymentMethod] [varchar](50) NULL,
 CONSTRAINT [PK_PurchaseHistory] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Payments]    Script Date: 11/15/2023 07:49:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Payments](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CID] [int] NULL,
	[TotalAmount] [float] NULL,
	[PaymentMethod] [varchar](50) NULL,
 CONSTRAINT [PK_Payments] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Card]    Script Date: 11/15/2023 07:49:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Card](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Category] [varchar](50) NULL,
	[ProdectId] [int] NULL,
	[ProdectName] [varchar](50) NULL,
	[Brand] [varchar](50) NULL,
	[Price] [float] NULL,
	[Quantity] [int] NULL,
	[Total] [int] NULL,
	[Reg_date] [date] NULL,
	[UserId] [int] NULL,
 CONSTRAINT [PK_Card] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  ForeignKey [FK_Card_Products1]    Script Date: 11/15/2023 07:49:20 ******/
ALTER TABLE [dbo].[Card]  WITH CHECK ADD  CONSTRAINT [FK_Card_Products1] FOREIGN KEY([ProdectId])
REFERENCES [dbo].[Products] ([ID])
GO
ALTER TABLE [dbo].[Card] CHECK CONSTRAINT [FK_Card_Products1]
GO
/****** Object:  ForeignKey [FK_Card_Usres]    Script Date: 11/15/2023 07:49:20 ******/
ALTER TABLE [dbo].[Card]  WITH CHECK ADD  CONSTRAINT [FK_Card_Usres] FOREIGN KEY([UserId])
REFERENCES [dbo].[Usres] ([ID])
GO
ALTER TABLE [dbo].[Card] CHECK CONSTRAINT [FK_Card_Usres]
GO
/****** Object:  ForeignKey [FK_Payments_Usres]    Script Date: 11/15/2023 07:49:20 ******/
ALTER TABLE [dbo].[Payments]  WITH CHECK ADD  CONSTRAINT [FK_Payments_Usres] FOREIGN KEY([CID])
REFERENCES [dbo].[Usres] ([ID])
GO
ALTER TABLE [dbo].[Payments] CHECK CONSTRAINT [FK_Payments_Usres]
GO
/****** Object:  ForeignKey [FK_PurchaseHistory_Usres]    Script Date: 11/15/2023 07:49:20 ******/
ALTER TABLE [dbo].[PurchaseHistory]  WITH CHECK ADD  CONSTRAINT [FK_PurchaseHistory_Usres] FOREIGN KEY([UserID])
REFERENCES [dbo].[Usres] ([ID])
GO
ALTER TABLE [dbo].[PurchaseHistory] CHECK CONSTRAINT [FK_PurchaseHistory_Usres]
GO
