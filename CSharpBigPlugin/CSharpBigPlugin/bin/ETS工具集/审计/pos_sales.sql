--2017-08-30 16:04:39 : 
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Audit_pos_sales]'))
drop table [dbo].[Audit_pos_sales]
GO
CREATE TABLE [dbo].[Audit_pos_sales](
[auditID] [numeric](18, 0) PRIMARY KEY IDENTITY(1,1) NOT NULL,
[action] [varchar](50) NULL,
[from] [varchar](100) NULL,
[auditTimestamp] [datetime] NULL,
[saleid] [numeric](24,0) NULL,
[billcode] [nvarchar](26) NULL,
[grand_billcode] [nvarchar](26) NULL,
[rec_status] [tinyint] NULL,
[sale_status] [tinyint] NULL,
[pay_status] [tinyint] NULL,
[sale_type] [tinyint] NULL,
[billshop] [nvarchar](5) NULL,
[saleshop] [nvarchar](5) NULL,
[payshop] [nvarchar](5) NULL,
[produceshop] [nvarchar](5) NULL,
[saledate] [datetime] NULL,
[saletime] [datetime] NULL,
[closedate] [datetime] NULL,
[paydate] [datetime] NULL,
[userid] [nvarchar](26) NULL,
[itemcode] [nvarchar](20) NULL,
[itemname1] [nvarchar](100) NULL,
[itemname2] [nvarchar](100) NULL,
[itemspec] [nvarchar](12) NULL,
[saleprice] [decimal](9,2) NULL,
[quantity] [decimal](9,2) NULL,
[saletotal] [decimal](9,2) NULL,
[discountpercent] [decimal](9,2) NULL,
[discounttotal] [decimal](9,2) NULL,
[gratuitypercent] [decimal](9,2) NULL,
[gratuitytotal] [decimal](9,2) NULL,
[rebatetotal] [decimal](9,2) NULL,
[taxpercent] [decimal](9,2) NULL,
[taxtotal] [decimal](9,2) NULL,
[totalpayable] [decimal](9,2) NULL,
[saleweight] [decimal](9,2) NULL,
[stockprice] [decimal](9,2) NULL,
[stockqty] [decimal](9,2) NULL,
[modifier_memo] [nvarchar](40) NULL,
[serverno] [nvarchar](26) NULL,
[if_discount] [tinyint] NULL,
[if_gratuity] [tinyint] NULL,
[sale_prop] [tinyint] NULL,
[if_revenue] [tinyint] NULL,
[if_generate] [tinyint] NULL,
[if_other] [tinyint] NULL,
[if_invoice] [tinyint] NULL,
[closemethod] [nvarchar](5) NULL,
[paymethod] [nvarchar](5) NULL,
[checkin_id] [nvarchar](12) NULL,
[orderno] [tinyint] NULL,
[seatno] [nvarchar](5) NULL,
[printno] [tinyint] NULL,
[otherid] [int] NULL,
[if_import] [tinyint] NULL,
[if_tax] [tinyint] NULL,
[pricemethod] [tinyint] NULL,
[sale_kinds] [nvarchar](8) NULL,
[parent_kinds] [nvarchar](8) NULL,
[sale_category] [nvarchar](8) NULL,
[parent_category] [nvarchar](8) NULL,
[sale_special] [nvarchar](8) NULL,
[parent_special] [nvarchar](8) NULL,
[stat_code] [nvarchar](5) NULL,
[paymentid] [int] NULL,
[fit_paymentid] [varchar](26) NULL,
[payflag] [tinyint] NULL,
[itemid] [int] NULL,
[autocdflag] [tinyint] NULL,
[otherflag] [tinyint] NULL,
[complimentary_flag] [nchar](1) NULL,
[modifier] [nvarchar](50) NULL,
[reason] [nvarchar](50) NULL,
[kitchen_type] [nchar](1) NULL,
[seat_no] [int] NULL,
[is_meal] [nchar](1) NULL,
[is_backup] [nchar](2) NULL,
[meal_id] [int] NULL,
[meal_type] [nchar](1) NULL,
[is_inv] [nchar](1) NULL,
[is_addtion] [nchar](1) NULL,
[club_id] [nvarchar](12) NULL,
[holidaymethod] [int] NULL,
[clubcode] [varchar](12) NULL,
[sttime] [datetime] NULL,
[endtime] [datetime] NULL,
[fcount_hh] [decimal](18,2) NULL,
[fcount_mm] [decimal](18,2) NULL,
[fcditemno] [varchar](10) NULL,
[Fpcno] [varchar](10) NULL,
[fclub] [varchar](12) NULL,
[fEntityNo] [varchar](10) NULL,
[fit_saleid] [varchar](26) NULL,
[fcreatedate] [datetime] NULL,
[fmodifydate] [datetime] NULL,
[fUserNo] [varchar](8) NULL,
[n_last] [numeric](18,0) NULL,
[ifdown] [char](1) NULL,
[xh] [int] NULL,
[dzxh] [numeric](8,0) NULL,
[dj_cb] [decimal](18,7) NULL,
[je_cb] [decimal](14,4) NULL,
[bm_ck] [varchar](16) NULL,
[is_check] [int] NULL,
[walkcust_ftype] [int] NULL,
[keyno] [varchar](18) NULL,
[memno] [nvarchar](18) NULL,
[Fremark] [varchar](60) NULL,
[deposit] [decimal](18,2) NULL,
[fcusttdno] [varchar](26) NULL,
[fbillNosub] [varchar](26) NULL,
[disccode] [varchar](18) NULL,
[spandhyacc] [varchar](18) NULL,
[ch_printflag] [int] NULL,
[accuserid] [varchar](26) NULL,
[fdayandhousr] [decimal](18,1) NULL,
[fmpdaycount] [int] NULL,
[zbcardno] [varchar](18) NULL,
[v_artno] [varchar](26) NULL,
[Fmp_m_id] [varchar](18) NULL,
[acc_clubkinds1] [varchar](8) NULL,
[acc_clubkinds2] [varchar](8) NULL,
[acc_clubkinds3] [varchar](16) NULL,
[fcustno] [varchar](10) NULL)
GO
IF EXISTS(select * from sysobjects where type='tr' and name='TGR_pos_sales_INSERT')
DROP  TRIGGER TGR_pos_sales_INSERT
GO
CREATE TRIGGER TGR_pos_sales_INSERT
ON [pos_sales] FOR INSERT
AS
BEGIN
DECLARE @from VARCHAR(100)
SELECT @from=(SELECT '客户机名:'+rtrim(hostname)+' 程序名:'+program_name FROM master..sysprocesses WHERE spid=(SELECT @@SPID))
INSERT INTO Audit_pos_sales SELECT 'INSERT',@from,GETDATE(),saleid,billcode,grand_billcode,rec_status,sale_status,pay_status,sale_type,billshop,saleshop,payshop,produceshop,saledate,saletime,closedate,paydate,userid,itemcode,itemname1,itemname2,itemspec,saleprice,quantity,saletotal,discountpercent,discounttotal,gratuitypercent,gratuitytotal,rebatetotal,taxpercent,taxtotal,totalpayable,saleweight,stockprice,stockqty,modifier_memo,serverno,if_discount,if_gratuity,sale_prop,if_revenue,if_generate,if_other,if_invoice,closemethod,paymethod,checkin_id,orderno,seatno,printno,otherid,if_import,if_tax,pricemethod,sale_kinds,parent_kinds,sale_category,parent_category,sale_special,parent_special,stat_code,paymentid,fit_paymentid,payflag,itemid,autocdflag,otherflag,complimentary_flag,modifier,reason,kitchen_type,seat_no,is_meal,is_backup,meal_id,meal_type,is_inv,is_addtion,club_id,holidaymethod,clubcode,sttime,endtime,fcount_hh,fcount_mm,fcditemno,Fpcno,fclub,fEntityNo,fit_saleid,fcreatedate,fmodifydate,fUserNo,n_last,ifdown,xh,dzxh,dj_cb,je_cb,bm_ck,is_check,walkcust_ftype,keyno,memno,Fremark,deposit,fcusttdno,fbillNosub,disccode,spandhyacc,ch_printflag,accuserid,fdayandhousr,fmpdaycount,zbcardno,v_artno,Fmp_m_id,acc_clubkinds1,acc_clubkinds2,acc_clubkinds3,fcustno FROM INSERTED
End
GO
IF EXISTS(select * from sysobjects where type='tr' and name='TGR_pos_sales_UPDATE')
DROP  TRIGGER TGR_pos_sales_UPDATE
GO
CREATE TRIGGER TGR_pos_sales_UPDATE
ON [pos_sales] FOR UPDATE
AS
BEGIN
DECLARE @from VARCHAR(100)
SELECT @from=(SELECT '客户机名:'+rtrim(hostname)+' 程序名:'+program_name FROM master..sysprocesses WHERE spid=(SELECT @@SPID))
INSERT INTO Audit_pos_sales SELECT 'UPDATE',@from,GETDATE(),saleid,billcode,grand_billcode,rec_status,sale_status,pay_status,sale_type,billshop,saleshop,payshop,produceshop,saledate,saletime,closedate,paydate,userid,itemcode,itemname1,itemname2,itemspec,saleprice,quantity,saletotal,discountpercent,discounttotal,gratuitypercent,gratuitytotal,rebatetotal,taxpercent,taxtotal,totalpayable,saleweight,stockprice,stockqty,modifier_memo,serverno,if_discount,if_gratuity,sale_prop,if_revenue,if_generate,if_other,if_invoice,closemethod,paymethod,checkin_id,orderno,seatno,printno,otherid,if_import,if_tax,pricemethod,sale_kinds,parent_kinds,sale_category,parent_category,sale_special,parent_special,stat_code,paymentid,fit_paymentid,payflag,itemid,autocdflag,otherflag,complimentary_flag,modifier,reason,kitchen_type,seat_no,is_meal,is_backup,meal_id,meal_type,is_inv,is_addtion,club_id,holidaymethod,clubcode,sttime,endtime,fcount_hh,fcount_mm,fcditemno,Fpcno,fclub,fEntityNo,fit_saleid,fcreatedate,fmodifydate,fUserNo,n_last,ifdown,xh,dzxh,dj_cb,je_cb,bm_ck,is_check,walkcust_ftype,keyno,memno,Fremark,deposit,fcusttdno,fbillNosub,disccode,spandhyacc,ch_printflag,accuserid,fdayandhousr,fmpdaycount,zbcardno,v_artno,Fmp_m_id,acc_clubkinds1,acc_clubkinds2,acc_clubkinds3,fcustno FROM INSERTED
End
GO

IF EXISTS(select * from sysobjects where type='tr' and name='TGR_pos_sales_DELETE')
DROP  TRIGGER TGR_pos_sales_DELETE
GO
CREATE TRIGGER TGR_pos_sales_DELETE
ON [pos_sales] FOR DELETE
AS
BEGIN
DECLARE @from VARCHAR(100)
SELECT @from=(SELECT '客户机名:'+rtrim(hostname)+' 程序名:'+program_name FROM master..sysprocesses WHERE spid=(SELECT @@SPID))
INSERT INTO Audit_pos_sales SELECT 'DELETE',@from,GETDATE(),saleid,billcode,grand_billcode,rec_status,sale_status,pay_status,sale_type,billshop,saleshop,payshop,produceshop,saledate,saletime,closedate,paydate,userid,itemcode,itemname1,itemname2,itemspec,saleprice,quantity,saletotal,discountpercent,discounttotal,gratuitypercent,gratuitytotal,rebatetotal,taxpercent,taxtotal,totalpayable,saleweight,stockprice,stockqty,modifier_memo,serverno,if_discount,if_gratuity,sale_prop,if_revenue,if_generate,if_other,if_invoice,closemethod,paymethod,checkin_id,orderno,seatno,printno,otherid,if_import,if_tax,pricemethod,sale_kinds,parent_kinds,sale_category,parent_category,sale_special,parent_special,stat_code,paymentid,fit_paymentid,payflag,itemid,autocdflag,otherflag,complimentary_flag,modifier,reason,kitchen_type,seat_no,is_meal,is_backup,meal_id,meal_type,is_inv,is_addtion,club_id,holidaymethod,clubcode,sttime,endtime,fcount_hh,fcount_mm,fcditemno,Fpcno,fclub,fEntityNo,fit_saleid,fcreatedate,fmodifydate,fUserNo,n_last,ifdown,xh,dzxh,dj_cb,je_cb,bm_ck,is_check,walkcust_ftype,keyno,memno,Fremark,deposit,fcusttdno,fbillNosub,disccode,spandhyacc,ch_printflag,accuserid,fdayandhousr,fmpdaycount,zbcardno,v_artno,Fmp_m_id,acc_clubkinds1,acc_clubkinds2,acc_clubkinds3,fcustno FROM DELETED
End
GO



