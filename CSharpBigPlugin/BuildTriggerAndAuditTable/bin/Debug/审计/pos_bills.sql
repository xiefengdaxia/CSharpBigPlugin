--2016-12-07 11:46:38 : 
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Audit_pos_bills]'))
drop table [dbo].[Audit_pos_bills]
GO
CREATE TABLE [dbo].[Audit_pos_bills](
[auditID] [numeric](18, 0) PRIMARY KEY IDENTITY(1,1) NOT NULL,
[action] [varchar](50) NULL,
[from] [varchar](100) NULL,
[auditTimestamp] [datetime] NULL,
[billcode] [nvarchar](26) NULL,
[billtype] [tinyint] NULL,
[rec_status] [tinyint] NULL,
[bill_status] [tinyint] NULL,
[pay_status] [tinyint] NULL,
[billshop] [nvarchar](5) NULL,
[saleshop] [nvarchar](5) NULL,
[payshop] [nvarchar](5) NULL,
[billdate] [datetime] NULL,
[opentime] [datetime] NULL,
[closedate] [datetime] NULL,
[closetime] [datetime] NULL,
[openuserid] [nvarchar](26) NULL,
[closeuserid] [nvarchar](26) NULL,
[dailysno] [smallint] NULL,
[tableno] [nvarchar](60) NULL,
[tableno_ext] [nvarchar](5) NULL,
[noofguest] [tinyint] NULL,
[serverno] [nvarchar](12) NULL,
[periodno] [nvarchar](5) NULL,
[shiftno] [nvarchar](5) NULL,
[closeshift] [nvarchar](5) NULL,
[parentshop] [nvarchar](5) NULL,
[grand_billcode] [nvarchar](26) NULL,
[checkin_id] [nvarchar](26) NULL,
[cardno] [nvarchar](18) NULL,
[memno] [nvarchar](18) NULL,
[memid] [int] NULL,
[guest_identity] [nvarchar](5) NULL,
[name] [nvarchar](60) NULL,
[sex] [nchar](1) NULL,
[billtotal] [decimal](9,2) NULL,
[discountpercent] [decimal](9,2) NULL,
[discounttotal] [decimal](9,2) NULL,
[gratuitypercent] [decimal](9,2) NULL,
[gratuitytotal] [decimal](9,2) NULL,
[taxpercent] [decimal](9,2) NULL,
[taxtotal] [decimal](9,2) NULL,
[tipstotal] [decimal](9,2) NULL,
[rebatetotal] [decimal](9,2) NULL,
[totalpayable] [decimal](9,2) NULL,
[paymethod] [nvarchar](5) NULL,
[paydesp] [nvarchar](250) NULL,
[printno] [tinyint] NULL,
[invoiceno] [nvarchar](20) NULL,
[invoiceflag] [tinyint] NULL,
[generateflag] [tinyint] NULL,
[payflag] [tinyint] NULL,
[reception] [nvarchar](12) NULL,
[saletype] [tinyint] NULL,
[lockstatus] [tinyint] NULL,
[sttime] [datetime] NULL,
[endtime] [datetime] NULL,
[fcount_hh] [decimal](18,2) NULL,
[fcount_mm] [decimal](18,2) NULL,
[fcditemno] [varchar](10) NULL,
[otherstatus] [tinyint] NULL,
[club_id] [nvarchar](12) NULL,
[memsid] [varchar](18) NULL,
[sequence] [int] NULL,
[keyno] [varchar](10) NULL,
[modify] [varchar](26) NULL,
[modifytime] [datetime] NULL,
[clubcode] [varchar](5) NULL,
[Fpcno] [varchar](10) NULL,
[fclub] [varchar](12) NULL,
[fEntityNo] [varchar](10) NULL,
[ch_payno] [varchar](18) NULL,
[fcreatedate] [datetime] NULL,
[fmodifydate] [datetime] NULL,
[fUserNo] [varchar](8) NULL,
[n_last] [numeric](18,0) NULL,
[ifdown] [char](1) NULL,
[is_check] [int] NULL,
[bm_ck] [varchar](16) NULL,
[dzxh] [numeric](8,0) NULL,
[checker] [varchar](16) NULL,
[rq_check] [char](8) NULL,
[cardbalance] [decimal](18,2) NULL,
[zb_bm_ck] [varchar](16) NULL,
[depositall] [decimal](18,2) NULL,
[fcusttdno] [varchar](26) NULL,
[syscode] [varchar](16) NULL,
[f_paykind] [varchar](10) NULL,
[fRemark] [varchar](100) NULL,
[saleid] [varchar](26) NULL,
[paycode] [varchar](16) NULL,
[accuserid] [varchar](26) NULL,
[fbillnohbaccno] [varchar](26) NULL,
[fhbbillflash] [int] NULL,
[fRemarkdoc] [varchar](100) NULL,
[quitydje] [decimal](18,2) NULL,
[ydallje] [decimal](18,2) NULL,
[fydbillno] [varchar](30) NULL)
GO
IF EXISTS(select * from sysobjects where type='tr' and name='TGR_pos_bills_INSERT')
DROP  TRIGGER TGR_pos_bills_INSERT
GO
CREATE TRIGGER TGR_pos_bills_INSERT
ON [pos_bills] FOR INSERT
AS
BEGIN
DECLARE @from VARCHAR(100)
SELECT @from=(SELECT '客户机名:'+rtrim(hostname)+' 程序名:'+program_name FROM master..sysprocesses WHERE spid=(SELECT @@SPID))
INSERT INTO Audit_pos_bills SELECT 'INSERT',@from,GETDATE(),billcode,billtype,rec_status,bill_status,pay_status,billshop,saleshop,payshop,billdate,opentime,closedate,closetime,openuserid,closeuserid,dailysno,tableno,tableno_ext,noofguest,serverno,periodno,shiftno,closeshift,parentshop,grand_billcode,checkin_id,cardno,memno,memid,guest_identity,name,sex,billtotal,discountpercent,discounttotal,gratuitypercent,gratuitytotal,taxpercent,taxtotal,tipstotal,rebatetotal,totalpayable,paymethod,paydesp,printno,invoiceno,invoiceflag,generateflag,payflag,reception,saletype,lockstatus,sttime,endtime,fcount_hh,fcount_mm,fcditemno,otherstatus,club_id,memsid,sequence,keyno,modify,modifytime,clubcode,Fpcno,fclub,fEntityNo,ch_payno,fcreatedate,fmodifydate,fUserNo,n_last,ifdown,is_check,bm_ck,dzxh,checker,rq_check,cardbalance,zb_bm_ck,depositall,fcusttdno,syscode,f_paykind,fRemark,saleid,paycode,accuserid,fbillnohbaccno,fhbbillflash,fRemarkdoc,quitydje,ydallje,fydbillno FROM INSERTED
End
GO
IF EXISTS(select * from sysobjects where type='tr' and name='TGR_pos_bills_UPDATE')
DROP  TRIGGER TGR_pos_bills_UPDATE
GO
CREATE TRIGGER TGR_pos_bills_UPDATE
ON [pos_bills] FOR UPDATE
AS
BEGIN
DECLARE @from VARCHAR(100)
SELECT @from=(SELECT '客户机名:'+rtrim(hostname)+' 程序名:'+program_name FROM master..sysprocesses WHERE spid=(SELECT @@SPID))
INSERT INTO Audit_pos_bills SELECT 'UPDATE',@from,GETDATE(),billcode,billtype,rec_status,bill_status,pay_status,billshop,saleshop,payshop,billdate,opentime,closedate,closetime,openuserid,closeuserid,dailysno,tableno,tableno_ext,noofguest,serverno,periodno,shiftno,closeshift,parentshop,grand_billcode,checkin_id,cardno,memno,memid,guest_identity,name,sex,billtotal,discountpercent,discounttotal,gratuitypercent,gratuitytotal,taxpercent,taxtotal,tipstotal,rebatetotal,totalpayable,paymethod,paydesp,printno,invoiceno,invoiceflag,generateflag,payflag,reception,saletype,lockstatus,sttime,endtime,fcount_hh,fcount_mm,fcditemno,otherstatus,club_id,memsid,sequence,keyno,modify,modifytime,clubcode,Fpcno,fclub,fEntityNo,ch_payno,fcreatedate,fmodifydate,fUserNo,n_last,ifdown,is_check,bm_ck,dzxh,checker,rq_check,cardbalance,zb_bm_ck,depositall,fcusttdno,syscode,f_paykind,fRemark,saleid,paycode,accuserid,fbillnohbaccno,fhbbillflash,fRemarkdoc,quitydje,ydallje,fydbillno FROM INSERTED
End
GO

IF EXISTS(select * from sysobjects where type='tr' and name='TGR_pos_bills_DELETE')
DROP  TRIGGER TGR_pos_bills_DELETE
GO
CREATE TRIGGER TGR_pos_bills_DELETE
ON [pos_bills] FOR DELETE
AS
BEGIN
DECLARE @from VARCHAR(100)
SELECT @from=(SELECT '客户机名:'+rtrim(hostname)+' 程序名:'+program_name FROM master..sysprocesses WHERE spid=(SELECT @@SPID))
INSERT INTO Audit_pos_bills SELECT 'DELETE',@from,GETDATE(),billcode,billtype,rec_status,bill_status,pay_status,billshop,saleshop,payshop,billdate,opentime,closedate,closetime,openuserid,closeuserid,dailysno,tableno,tableno_ext,noofguest,serverno,periodno,shiftno,closeshift,parentshop,grand_billcode,checkin_id,cardno,memno,memid,guest_identity,name,sex,billtotal,discountpercent,discounttotal,gratuitypercent,gratuitytotal,taxpercent,taxtotal,tipstotal,rebatetotal,totalpayable,paymethod,paydesp,printno,invoiceno,invoiceflag,generateflag,payflag,reception,saletype,lockstatus,sttime,endtime,fcount_hh,fcount_mm,fcditemno,otherstatus,club_id,memsid,sequence,keyno,modify,modifytime,clubcode,Fpcno,fclub,fEntityNo,ch_payno,fcreatedate,fmodifydate,fUserNo,n_last,ifdown,is_check,bm_ck,dzxh,checker,rq_check,cardbalance,zb_bm_ck,depositall,fcusttdno,syscode,f_paykind,fRemark,saleid,paycode,accuserid,fbillnohbaccno,fhbbillflash,fRemarkdoc,quitydje,ydallje,fydbillno FROM DELETED
End
GO



