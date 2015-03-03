

ALTER TABLE [dbo].[OPC_RMA] ADD	[Reason] [nvarchar](200)
ALTER TABLE [dbo].[OPC_RMA] ADD	[BackDate] [datetime] 
ALTER TABLE [dbo].[OPC_RMA] ADD	[CustomerAuthDate] [datetime] 
ALTER TABLE [dbo].[OPC_RMA] ADD	[StoreFee] [decimal](18, 2)
ALTER TABLE [dbo].[OPC_RMA] ADD	[CustomFee] [decimal](18, 2) 
ALTER TABLE [dbo].[OPC_RMA] ADD	[RMAMemo] [nvarchar](150) 
ALTER TABLE [dbo].[OPC_RMA] ADD	[CompensationFee] [decimal](18, 2) 
ALTER TABLE [dbo].[OPC_RMA] ADD	[SaleRMASource] [nvarchar](20)
ALTER TABLE [dbo].[OPC_RMA] ADD	[RMAStatus] [int] 
ALTER TABLE [dbo].[OPC_RMA] ADD	[RMACashStatus] [int] 
ALTER TABLE [dbo].[OPC_RMA] ADD	[RealRMASumMoney] [decimal](18, 2) 
ALTER TABLE [dbo].[OPC_RMA] ADD	[RecoverableSumMoney] [decimal](18, 2) 
