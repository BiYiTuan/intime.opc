Use [YintaiHZhou]

if not exists (select * from syscolumns where id=object_id('ShipVia') and name='TemplateName')
begin
	alter table ShipVia add  TemplateName varchar(100)
end

Update ShipVia set TemplateName='Express_EMS' where Id= 1

Update ShipVia set TemplateName='Express_SF' where Id= 36