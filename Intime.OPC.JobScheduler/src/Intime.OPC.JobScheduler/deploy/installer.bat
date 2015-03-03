@echo 正在安装服务

sc create "Intime.OPC.JobScheduler" binpath= "%~dp0..\Intime.OPC.JobScheduler" displayname= "Intime.OPC.JobScheduler"
net start "Intime.OPC.JobScheduler"

pause
