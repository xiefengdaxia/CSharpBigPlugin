--2018-04-24 16:17:13 : 
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Audit_pos_bill_payment]'))
drop table [dbo].[Audit_pos_bill_payment]
GO
CREATE TABLE [dbo].[Audit_pos_bill_payment](
[auditID] [numeric](18, 0) PRIMARY KEY IDENTITY(1,1) NOT NULL,
[action] [varchar](50) NULL,
[from] [varchar](100) NULL,
[auditTimestamp] [datetime] NULL,
[paymentid] [numeric](24,0) NULL,
[billcode] [nvarchar](26) NULL,
[rec_status] [tinyint] NULL,
[pay_status] [tinyint] NULL,
[paymethod] [nvarchar](5) NULL,
[payparent] [nvarchar](5) NULL,
[payamount] [numeric](9,2) NULL,
[payforeign] [nvarchar](5) NULL,
[payshop] [nvarchar](5) NULL,
[paydate] [datetime] NULL,
[payflag] [tinyint] NULL,
[payrate] [numeric](9,2) NULL,
[payfamt] [numeric](9,2) NULL,
[paychanges] [numeric](9,2) NULL,
[paycardno] [nvarchar](30) NULL,
[ExpiryDate] [nvarchar](30) NULL,
[paysysid] [int] NULL,
[payname] [nvarchar](60) NULL,
[userid] [nvarchar](26) NULL,
[shiftno] [nvarchar](5) NULL,
[if_invoice] [tinyint] NULL,
[if_revenue] [tinyint] NULL,
[payno] [nvarchar](60) NULL,
[paymemo] [nvarchar](255) NULL,
[paytype] [varchar](30) NULL,
[memid] [varchar](26) NULL,
[clubcode] [varchar](12) NULL,
[fit_paymentid] [varchar](26) NULL,
[fEntityNo] [varchar](10) NULL,
[FClub] [varchar](12) NULL,
[Fpcno] [varchar](10) NULL,
[fcreatedate] [datetime] NULL,
[fmodifydate] [datetime] NULL,
[fUserNo] [varchar](8) NULL,
[n_last] [numeric](18,0) NULL,
[ifdown] [char](1) NULL,
[syscode] [varchar](16) NULL,
[paycode] [varchar](16) NULL,
[sale_kinds] [varchar](8) NULL,
[sale_category] [varchar](8) NULL,
[sale_special] [varchar](8) NULL,
[fifrepacc] [int] NULL,
[fifdhyhpay] [int] NULL,
[Fposuserid] [varchar](12) NULL,
[Fbankcard] [varchar](22) NULL,
[billcodepos] [varchar](26) NULL,
[Fposid] [varchar](12) NULL)
GO
IF EXISTS(select * from sysobjects where type='tr' and name='TGR_pos_bill_payment_INSERT')
DROP  TRIGGER TGR_pos_bill_payment_INSERT
GO
CREATE TRIGGER TGR_pos_bill_payment_INSERT
ON [pos_bill_payment] FOR INSERT
AS
BEGIN
DECLARE @from VARCHAR(100)
SELECT @from=(SELECT '客户机名:'+rtrim(hostname)+' 程序名:'+program_name FROM master..sysprocesses WHERE spid=(SELECT @@SPID))
INSERT INTO Audit_pos_bill_payment SELECT 'INSERT',@from,GETDATE(),paymentid,billcode,rec_status,pay_status,paymethod,payparent,payamount,payforeign,payshop,paydate,payflag,payrate,payfamt,paychanges,paycardno,ExpiryDate,paysysid,payname,userid,shiftno,if_invoice,if_revenue,payno,paymemo,paytype,memid,clubcode,fit_paymentid,fEntityNo,FClub,Fpcno,fcreatedate,fmodifydate,fUserNo,n_last,ifdown,syscode,paycode,sale_kinds,sale_category,sale_special,fifrepacc,fifdhyhpay,Fposuserid,Fbankcard,billcodepos,Fposid FROM INSERTED
End
GO
IF EXISTS(select * from sysobjects where type='tr' and name='TGR_pos_bill_payment_UPDATE')
DROP  TRIGGER TGR_pos_bill_payment_UPDATE
GO
CREATE TRIGGER TGR_pos_bill_payment_UPDATE
ON [pos_bill_payment] FOR UPDATE
AS
BEGIN
DECLARE @from VARCHAR(100)
SELECT @from=(SELECT '客户机名:'+rtrim(hostname)+' 程序名:'+program_name FROM master..sysprocesses WHERE spid=(SELECT @@SPID))
INSERT INTO Audit_pos_bill_payment SELECT 'UPDATE',@from,GETDATE(),paymentid,billcode,rec_status,pay_status,paymethod,payparent,payamount,payforeign,payshop,paydate,payflag,payrate,payfamt,paychanges,paycardno,ExpiryDate,paysysid,payname,userid,shiftno,if_invoice,if_revenue,payno,paymemo,paytype,memid,clubcode,fit_paymentid,fEntityNo,FClub,Fpcno,fcreatedate,fmodifydate,fUserNo,n_last,ifdown,syscode,paycode,sale_kinds,sale_category,sale_special,fifrepacc,fifdhyhpay,Fposuserid,Fbankcard,billcodepos,Fposid FROM INSERTED
End
GO

IF EXISTS(select * from sysobjects where type='tr' and name='TGR_pos_bill_payment_DELETE')
DROP  TRIGGER TGR_pos_bill_payment_DELETE
GO
CREATE TRIGGER TGR_pos_bill_payment_DELETE
ON [pos_bill_payment] FOR DELETE
AS
BEGIN
DECLARE @from VARCHAR(100)
SELECT @from=(SELECT '客户机名:'+rtrim(hostname)+' 程序名:'+program_name FROM master..sysprocesses WHERE spid=(SELECT @@SPID))
INSERT INTO Audit_pos_bill_payment SELECT 'DELETE',@from,GETDATE(),paymentid,billcode,rec_status,pay_status,paymethod,payparent,payamount,payforeign,payshop,paydate,payflag,payrate,payfamt,paychanges,paycardno,ExpiryDate,paysysid,payname,userid,shiftno,if_invoice,if_revenue,payno,paymemo,paytype,memid,clubcode,fit_paymentid,fEntityNo,FClub,Fpcno,fcreatedate,fmodifydate,fUserNo,n_last,ifdown,syscode,paycode,sale_kinds,sale_category,sale_special,fifrepacc,fifdhyhpay,Fposuserid,Fbankcard,billcodepos,Fposid FROM DELETED
End
GO



