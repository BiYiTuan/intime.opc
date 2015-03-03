update [YintaiHZhou].[dbo].[OPC_AuthMenu] set Url ='BrandList' where Id=32
select * from [YintaiHZhou].[dbo].[OPC_AuthMenu] where Id=32


update [YintaiHZhou].[dbo].[OPC_AuthMenu] set Url ='CounterList' where Id=43
select * from [YintaiHZhou].[dbo].[OPC_AuthMenu] where Id=43

insert into [YintaiHZhou].[dbo].[OPC_AuthMenu] (Id,MenuName, CreateUserId, CreateDate,UpdateDate,UpdateUserId,PraentMenuId,IsValid,Sort,Url) 
values (44,N'迷你银退货收货确认',1,GETDATE(),GETDATE(),1,4,1,8,'MiniIntimeReturnConsignment')

insert into [YintaiHZhou].dbo.OPC_AuthRoleMenu (OPC_AuthMenuId, OPC_AuthRoleId,CreateDate,CreateUserId,UpdateDate,UpdateUserId) values (44,1,GETDATE(),1,GETDATE(),1)

insert into [YintaiHZhou].[dbo].[OPC_AuthMenu] (Id,MenuName, CreateUserId, CreateDate,UpdateDate,UpdateUserId,PraentMenuId,IsValid,Sort,Url) 
values (45,N'迷你银开店申请审核',1,GETDATE(),GETDATE(),1,2,1,8,'StoreApplication')

insert into [YintaiHZhou].dbo.OPC_AuthRoleMenu (OPC_AuthMenuId, OPC_AuthRoleId,CreateDate,CreateUserId,UpdateDate,UpdateUserId) values (45,1,GETDATE(),1,GETDATE(),1)

--礼品卡销售统计
insert into [YintaiHZhou].[dbo].[OPC_AuthMenu] (Id,MenuName, CreateUserId, CreateDate,UpdateDate,UpdateUserId,PraentMenuId,IsValid,Sort,Url) 
values (46,N'礼品卡销售统计',1,GETDATE(),GETDATE(),1,25,1,5,'GiftCardStatisticsView')

insert into [YintaiHZhou].dbo.OPC_AuthRoleMenu (OPC_AuthMenuId, OPC_AuthRoleId,CreateDate,CreateUserId,UpdateDate,UpdateUserId) values (46,1,GETDATE(),1,GETDATE(),1)

--导购未提佣金统计
insert into [YintaiHZhou].[dbo].[OPC_AuthMenu] (Id,MenuName, CreateUserId, CreateDate,UpdateDate,UpdateUserId,PraentMenuId,IsValid,Sort,Url) 
values (47,N'导购未提佣金统计',1,GETDATE(),GETDATE(),1,25,1,6,'UncashedCommisionStatisticsView')

insert into [YintaiHZhou].dbo.OPC_AuthRoleMenu (OPC_AuthMenuId, OPC_AuthRoleId,CreateDate,CreateUserId,UpdateDate,UpdateUserId) values (47,1,GETDATE(),1,GETDATE(),1)


--导购已提佣金统计
insert into [YintaiHZhou].[dbo].[OPC_AuthMenu] (Id,MenuName, CreateUserId, CreateDate,UpdateDate,UpdateUserId,PraentMenuId,IsValid,Sort,Url) 
values (48,N'导购已提佣金统计',1,GETDATE(),GETDATE(),1,25,1,7,'CashedCommisionStatisticsView')

insert into [YintaiHZhou].dbo.OPC_AuthRoleMenu (OPC_AuthMenuId, OPC_AuthRoleId,CreateDate,CreateUserId,UpdateDate,UpdateUserId) values (48,1,GETDATE(),1,GETDATE(),1)