CREATE TRIGGER trig_UpdateInfo on dbo.tb_Employee
FOR insert,update
AS
if exists(select ID from inserted where ID in(select ID from tb_Salary))
begin
update tb_Salary set Name=(select Name from inserted) where ID=(select ID from inserted) 
end
else
print '不存在该职工编号'