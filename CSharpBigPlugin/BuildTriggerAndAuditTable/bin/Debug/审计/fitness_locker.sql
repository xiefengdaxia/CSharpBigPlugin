--2016-12-07 11:46:38 : 
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Audit_fitness_locker]'))
drop table [dbo].[Audit_fitness_locker]
GO
CREATE TABLE [dbo].[Audit_fitness_locker](
[auditID] [numeric](18, 0) PRIMARY KEY IDENTITY(1,1) NOT NULL,
[action] [varchar](50) NULL,
[from] [varchar](100) NULL,
[auditTimestamp] [datetime] NULL,
[code] [nvarchar](18) NULL,
[status] [tinyint] NULL,
[memo] [varchar](50) NULL,
[kind] [varchar](5) NULL,
[fEntityNo] [varchar](10) NULL,
[FClub] [varchar](12) NULL,
[Fpcno] [varchar](10) NULL,
[fcreatedate] [datetime] NULL,
[fmodifydate] [datetime] NULL,
[fUserNo] [varchar](8) NULL,
[n_last] [numeric](18,0) NULL,
[ifdown] [char](1) NULL,
[fbillNo] [nvarchar](26) NULL,
[finout] [char](1) NULL,
[fhyinmpno] [varchar](20) NULL,
[Sicardno] [varchar](20) NULL,
[statusinout] [int] NULL,
[ftype] [int] NULL,
[fpowerdoor] [int] NULL,
[ftickh] [varchar](30) NULL,
[fifygmp] [int] NULL,
[namec] [varchar](20) NULL,
[cardsno] [decimal](10,0) NULL,
[q_id] [varchar](20) NULL,
[issue_date] [datetime] NULL,
[issue_card] [int] NULL,
[fbillNosub] [varchar](26) NULL,
[ifictype] [int] NULL,
[ccard_catecode] [varchar](4) NULL,
[ccard_media] [tinyint] NULL,
[CCARD_STATUS] [tinyint] NULL,
[CARD_ROOM_NO] [varchar](6) NULL,
[CARD_M_ID] [varchar](18) NULL,
[card_identity] [varchar](20) NULL,
[card_sex] [varchar](1) NULL,
[CARD_METHOD] [varchar](20) NULL,
[CARD_USER_ID] [varchar](12) NULL,
[CARD_TIME] [datetime] NULL,
[CARD_CHECKIN_ID] [varchar](23) NULL,
[CARD_MEMO] [varchar](255) NULL,
[CARD_LOST] [tinyint] NULL,
[ccard_guest] [varchar](30) NULL,
[if_pos] [tinyint] NULL,
[CCARD_VALID_DATE] [datetime] NULL,
[tickh] [varchar](30) NULL,
[cardbalance] [decimal](18,2) NULL,
[fxhbalance] [decimal](18,2) NULL,
[billnomaster] [varchar](26) NULL,
[billnochild] [varchar](26) NULL,
[fcusttdno] [varchar](26) NULL,
[fmp] [varchar](20) NULL,
[ftdnewbill] [varchar](26) NULL,
[fxjhnewbill] [varchar](26) NULL,
[fifaccdel] [int] NULL,
[fifmybill] [int] NULL,
[memhy] [varchar](18) NULL,
[fhygysplink] [int] NULL,
[fhynolink] [varchar](18) NULL,
[fsttdate] [datetime] NULL,
[fenddate] [datetime] NULL,
[fgsbkflash] [int] NULL,
[v_artNo] [varchar](26) NULL,
[fmpjeall] [decimal](18,2) NULL,
[fyjjeall] [decimal](18,2) NULL,
[fenddate02] [datetime] NULL)
GO
IF EXISTS(select * from sysobjects where type='tr' and name='TGR_fitness_locker_INSERT')
DROP  TRIGGER TGR_fitness_locker_INSERT
GO
CREATE TRIGGER TGR_fitness_locker_INSERT
ON [fitness_locker] FOR INSERT
AS
BEGIN
DECLARE @from VARCHAR(100)
SELECT @from=(SELECT '客户机名:'+rtrim(hostname)+' 程序名:'+program_name FROM master..sysprocesses WHERE spid=(SELECT @@SPID))
INSERT INTO Audit_fitness_locker SELECT 'INSERT',@from,GETDATE(),code,status,memo,kind,fEntityNo,FClub,Fpcno,fcreatedate,fmodifydate,fUserNo,n_last,ifdown,fbillNo,finout,fhyinmpno,Sicardno,statusinout,ftype,fpowerdoor,ftickh,fifygmp,namec,cardsno,q_id,issue_date,issue_card,fbillNosub,ifictype,ccard_catecode,ccard_media,CCARD_STATUS,CARD_ROOM_NO,CARD_M_ID,card_identity,card_sex,CARD_METHOD,CARD_USER_ID,CARD_TIME,CARD_CHECKIN_ID,CARD_MEMO,CARD_LOST,ccard_guest,if_pos,CCARD_VALID_DATE,tickh,cardbalance,fxhbalance,billnomaster,billnochild,fcusttdno,fmp,ftdnewbill,fxjhnewbill,fifaccdel,fifmybill,memhy,fhygysplink,fhynolink,fsttdate,fenddate,fgsbkflash,v_artNo,fmpjeall,fyjjeall,fenddate02 FROM INSERTED
End
GO
IF EXISTS(select * from sysobjects where type='tr' and name='TGR_fitness_locker_UPDATE')
DROP  TRIGGER TGR_fitness_locker_UPDATE
GO
CREATE TRIGGER TGR_fitness_locker_UPDATE
ON [fitness_locker] FOR UPDATE
AS
BEGIN
DECLARE @from VARCHAR(100)
SELECT @from=(SELECT '客户机名:'+rtrim(hostname)+' 程序名:'+program_name FROM master..sysprocesses WHERE spid=(SELECT @@SPID))
INSERT INTO Audit_fitness_locker SELECT 'UPDATE',@from,GETDATE(),code,status,memo,kind,fEntityNo,FClub,Fpcno,fcreatedate,fmodifydate,fUserNo,n_last,ifdown,fbillNo,finout,fhyinmpno,Sicardno,statusinout,ftype,fpowerdoor,ftickh,fifygmp,namec,cardsno,q_id,issue_date,issue_card,fbillNosub,ifictype,ccard_catecode,ccard_media,CCARD_STATUS,CARD_ROOM_NO,CARD_M_ID,card_identity,card_sex,CARD_METHOD,CARD_USER_ID,CARD_TIME,CARD_CHECKIN_ID,CARD_MEMO,CARD_LOST,ccard_guest,if_pos,CCARD_VALID_DATE,tickh,cardbalance,fxhbalance,billnomaster,billnochild,fcusttdno,fmp,ftdnewbill,fxjhnewbill,fifaccdel,fifmybill,memhy,fhygysplink,fhynolink,fsttdate,fenddate,fgsbkflash,v_artNo,fmpjeall,fyjjeall,fenddate02 FROM INSERTED
End
GO

IF EXISTS(select * from sysobjects where type='tr' and name='TGR_fitness_locker_DELETE')
DROP  TRIGGER TGR_fitness_locker_DELETE
GO
CREATE TRIGGER TGR_fitness_locker_DELETE
ON [fitness_locker] FOR DELETE
AS
BEGIN
DECLARE @from VARCHAR(100)
SELECT @from=(SELECT '客户机名:'+rtrim(hostname)+' 程序名:'+program_name FROM master..sysprocesses WHERE spid=(SELECT @@SPID))
INSERT INTO Audit_fitness_locker SELECT 'DELETE',@from,GETDATE(),code,status,memo,kind,fEntityNo,FClub,Fpcno,fcreatedate,fmodifydate,fUserNo,n_last,ifdown,fbillNo,finout,fhyinmpno,Sicardno,statusinout,ftype,fpowerdoor,ftickh,fifygmp,namec,cardsno,q_id,issue_date,issue_card,fbillNosub,ifictype,ccard_catecode,ccard_media,CCARD_STATUS,CARD_ROOM_NO,CARD_M_ID,card_identity,card_sex,CARD_METHOD,CARD_USER_ID,CARD_TIME,CARD_CHECKIN_ID,CARD_MEMO,CARD_LOST,ccard_guest,if_pos,CCARD_VALID_DATE,tickh,cardbalance,fxhbalance,billnomaster,billnochild,fcusttdno,fmp,ftdnewbill,fxjhnewbill,fifaccdel,fifmybill,memhy,fhygysplink,fhynolink,fsttdate,fenddate,fgsbkflash,v_artNo,fmpjeall,fyjjeall,fenddate02 FROM DELETED
End
GO



