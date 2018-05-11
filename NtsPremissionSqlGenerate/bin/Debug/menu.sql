
delete from premission where id=-436477463;
INSERT INTO premission(id, auto_created, CODE, NAME, premission_type, parent_id)
  SELECT -436477463, 1, 'report-sales', '销售收入报表', 1, 0
  FROM dummy
  WHERE NOT EXISTS (SELECT 1 FROM premission WHERE id = -436477463);

delete from t_menu where id=-436477463;
INSERT INTO t_menu(id, auto_created, CODE, menu_name, icon_class, parent_id, state, url, premission_id, `group`, sort)
SELECT -436477463, 1, 'report-sales', '销售收入报表',
    'icon-list-ul', 0, 0, '', -436477463, '', 90000
FROM dummy
WHERE NOT EXISTS (SELECT 1 FROM t_menu WHERE id = -436477463);

INSERT INTO t_role_menu(id, VERSION, role_id, menu_id)
SELECT -436477463 + id, 0, id, -436477463
FROM t_role
WHERE tenant_type = 'club' AND NOT EXISTS(SELECT 1 FROM t_role_menu WHERE id = -436477463 + t_role.id);

delete from premission where id=-4364774631;
INSERT INTO premission(id, auto_created, CODE, NAME, premission_type, parent_id)
  SELECT -4364774631, 1, 'report-sales-ticketSale', '门票销售情况统计表', 1, -436477463
  FROM dummy
  WHERE NOT EXISTS (SELECT 1 FROM premission WHERE id = -4364774631);

delete from t_menu where id=-4364774631;
INSERT INTO t_menu(id, auto_created, CODE, menu_name, icon_class, parent_id, state, url, premission_id, `group`, sort)
SELECT -4364774631, 1, 'report-sales-ticketSale', '门票销售情况统计表',
    'icon-list-ul', -436477463, 1, '#!/ticketSale/', -4364774631, '', 90001
FROM dummy
WHERE NOT EXISTS (SELECT 1 FROM t_menu WHERE id = -4364774631);

INSERT INTO t_role_menu(id, VERSION, role_id, menu_id)
SELECT -4364774631 + id, 0, id, -4364774631
FROM t_role
WHERE tenant_type = 'club' AND NOT EXISTS(SELECT 1 FROM t_role_menu WHERE id = -4364774631 + t_role.id);

delete from premission where id=-4364774632;
INSERT INTO premission(id, auto_created, CODE, NAME, premission_type, parent_id)
  SELECT -4364774632, 1, 'report-sales-posbillOneLook', '账单一览表', 1, -436477463
  FROM dummy
  WHERE NOT EXISTS (SELECT 1 FROM premission WHERE id = -4364774632);

delete from t_menu where id=-4364774632;
INSERT INTO t_menu(id, auto_created, CODE, menu_name, icon_class, parent_id, state, url, premission_id, `group`, sort)
SELECT -4364774632, 1, 'report-sales-posbillOneLook', '账单一览表',
    'icon-list-ul', -436477463, 1, '#!/posbillOneLook/', -4364774632, '', 90002
FROM dummy
WHERE NOT EXISTS (SELECT 1 FROM t_menu WHERE id = -4364774632);

INSERT INTO t_role_menu(id, VERSION, role_id, menu_id)
SELECT -4364774632 + id, 0, id, -4364774632
FROM t_role
WHERE tenant_type = 'club' AND NOT EXISTS(SELECT 1 FROM t_role_menu WHERE id = -4364774632 + t_role.id);

delete from premission where id=-4364774633;
INSERT INTO premission(id, auto_created, CODE, NAME, premission_type, parent_id)
  SELECT -4364774633, 1, 'report-sales-returnPosbill', '账单调整日志表', 1, -436477463
  FROM dummy
  WHERE NOT EXISTS (SELECT 1 FROM premission WHERE id = -4364774633);

delete from t_menu where id=-4364774633;
INSERT INTO t_menu(id, auto_created, CODE, menu_name, icon_class, parent_id, state, url, premission_id, `group`, sort)
SELECT -4364774633, 1, 'report-sales-returnPosbill', '账单调整日志表',
    'icon-list-ul', -436477463, 1, '#!/returnPosbill/', -4364774633, '', 90003
FROM dummy
WHERE NOT EXISTS (SELECT 1 FROM t_menu WHERE id = -4364774633);

INSERT INTO t_role_menu(id, VERSION, role_id, menu_id)
SELECT -4364774633 + id, 0, id, -4364774633
FROM t_role
WHERE tenant_type = 'club' AND NOT EXISTS(SELECT 1 FROM t_role_menu WHERE id = -4364774633 + t_role.id);

delete from premission where id=-4364774634;
INSERT INTO premission(id, auto_created, CODE, NAME, premission_type, parent_id)
  SELECT -4364774634, 1, 'report-sales-operaterIncomeAcount', '各收银员收款汇总报表', 1, -436477463
  FROM dummy
  WHERE NOT EXISTS (SELECT 1 FROM premission WHERE id = -4364774634);

delete from t_menu where id=-4364774634;
INSERT INTO t_menu(id, auto_created, CODE, menu_name, icon_class, parent_id, state, url, premission_id, `group`, sort)
SELECT -4364774634, 1, 'report-sales-operaterIncomeAcount', '各收银员收款汇总报表',
    'icon-list-ul', -436477463, 1, '#!/operaterIncomeAcount/', -4364774634, '', 90004
FROM dummy
WHERE NOT EXISTS (SELECT 1 FROM t_menu WHERE id = -4364774634);

INSERT INTO t_role_menu(id, VERSION, role_id, menu_id)
SELECT -4364774634 + id, 0, id, -4364774634
FROM t_role
WHERE tenant_type = 'club' AND NOT EXISTS(SELECT 1 FROM t_role_menu WHERE id = -4364774634 + t_role.id);

delete from premission where id=-4364774635;
INSERT INTO premission(id, auto_created, CODE, NAME, premission_type, parent_id)
  SELECT -4364774635, 1, 'report-sales-productSaleDetail', '商品销售明细表', 1, -436477463
  FROM dummy
  WHERE NOT EXISTS (SELECT 1 FROM premission WHERE id = -4364774635);

delete from t_menu where id=-4364774635;
INSERT INTO t_menu(id, auto_created, CODE, menu_name, icon_class, parent_id, state, url, premission_id, `group`, sort)
SELECT -4364774635, 1, 'report-sales-productSaleDetail', '商品销售明细表',
    'icon-list-ul', -436477463, 1, '#!/productSaleDetail/', -4364774635, '', 90005
FROM dummy
WHERE NOT EXISTS (SELECT 1 FROM t_menu WHERE id = -4364774635);

INSERT INTO t_role_menu(id, VERSION, role_id, menu_id)
SELECT -4364774635 + id, 0, id, -4364774635
FROM t_role
WHERE tenant_type = 'club' AND NOT EXISTS(SELECT 1 FROM t_role_menu WHERE id = -4364774635 + t_role.id);

delete from premission where id=-436478469;
INSERT INTO premission(id, auto_created, CODE, NAME, premission_type, parent_id)
  SELECT -436478469, 1, 'report-mem', '会员管理报表', 1, 0
  FROM dummy
  WHERE NOT EXISTS (SELECT 1 FROM premission WHERE id = -436478469);

delete from t_menu where id=-436478469;
INSERT INTO t_menu(id, auto_created, CODE, menu_name, icon_class, parent_id, state, url, premission_id, `group`, sort)
SELECT -436478469, 1, 'report-mem', '会员管理报表',
    'icon-list-ul', 0, 0, '', -436478469, '', 95000
FROM dummy
WHERE NOT EXISTS (SELECT 1 FROM t_menu WHERE id = -436478469);

INSERT INTO t_role_menu(id, VERSION, role_id, menu_id)
SELECT -436478469 + id, 0, id, -436478469
FROM t_role
WHERE tenant_type = 'club' AND NOT EXISTS(SELECT 1 FROM t_role_menu WHERE id = -436478469 + t_role.id);

delete from premission where id=-4364775636;
INSERT INTO premission(id, auto_created, CODE, NAME, premission_type, parent_id)
  SELECT -4364775636, 1, 'report-mem-newMember', '新增会员统计报表', 1, -436478469
  FROM dummy
  WHERE NOT EXISTS (SELECT 1 FROM premission WHERE id = -4364775636);

delete from t_menu where id=-4364775636;
INSERT INTO t_menu(id, auto_created, CODE, menu_name, icon_class, parent_id, state, url, premission_id, `group`, sort)
SELECT -4364775636, 1, 'report-mem-newMember', '新增会员统计报表',
    'icon-list-ul', -436478469, 1, '#!/newMember/', -4364775636, '', 96006
FROM dummy
WHERE NOT EXISTS (SELECT 1 FROM t_menu WHERE id = -4364775636);

INSERT INTO t_role_menu(id, VERSION, role_id, menu_id)
SELECT -4364775636 + id, 0, id, -4364775636
FROM t_role
WHERE tenant_type = 'club' AND NOT EXISTS(SELECT 1 FROM t_role_menu WHERE id = -4364775636 + t_role.id);

delete from premission where id=-4364775637;
INSERT INTO premission(id, auto_created, CODE, NAME, premission_type, parent_id)
  SELECT -4364775637, 1, 'report-mem-memCardBalance', '会员余额情况统计表', 1, -436478469
  FROM dummy
  WHERE NOT EXISTS (SELECT 1 FROM premission WHERE id = -4364775637);

delete from t_menu where id=-4364775637;
INSERT INTO t_menu(id, auto_created, CODE, menu_name, icon_class, parent_id, state, url, premission_id, `group`, sort)
SELECT -4364775637, 1, 'report-mem-memCardBalance', '会员余额情况统计表',
    'icon-list-ul', -436478469, 1, '#!/memCardBalance/', -4364775637, '', 96007
FROM dummy
WHERE NOT EXISTS (SELECT 1 FROM t_menu WHERE id = -4364775637);

INSERT INTO t_role_menu(id, VERSION, role_id, menu_id)
SELECT -4364775637 + id, 0, id, -4364775637
FROM t_role
WHERE tenant_type = 'club' AND NOT EXISTS(SELECT 1 FROM t_role_menu WHERE id = -4364775637 + t_role.id);

delete from premission where id=-436479471;
INSERT INTO premission(id, auto_created, CODE, NAME, premission_type, parent_id)
  SELECT -436479471, 1, 'report-mem_RC', '会员充值与消费报表', 1, 0
  FROM dummy
  WHERE NOT EXISTS (SELECT 1 FROM premission WHERE id = -436479471);

delete from t_menu where id=-436479471;
INSERT INTO t_menu(id, auto_created, CODE, menu_name, icon_class, parent_id, state, url, premission_id, `group`, sort)
SELECT -436479471, 1, 'report-mem_RC', '会员充值与消费报表',
    'icon-list-ul', 0, 0, '', -436479471, '', 96000
FROM dummy
WHERE NOT EXISTS (SELECT 1 FROM t_menu WHERE id = -436479471);

INSERT INTO t_role_menu(id, VERSION, role_id, menu_id)
SELECT -436479471 + id, 0, id, -436479471
FROM t_role
WHERE tenant_type = 'club' AND NOT EXISTS(SELECT 1 FROM t_role_menu WHERE id = -436479471 + t_role.id);

delete from premission where id=-4364776638;
INSERT INTO premission(id, auto_created, CODE, NAME, premission_type, parent_id)
  SELECT -4364776638, 1, 'report-mem-memConsumptionDetail', '会员消费情况统计表', 1, -436479471
  FROM dummy
  WHERE NOT EXISTS (SELECT 1 FROM premission WHERE id = -4364776638);

delete from t_menu where id=-4364776638;
INSERT INTO t_menu(id, auto_created, CODE, menu_name, icon_class, parent_id, state, url, premission_id, `group`, sort)
SELECT -4364776638, 1, 'report-mem-memConsumptionDetail', '会员消费情况统计表',
    'icon-list-ul', -436479471, 1, '#!/memConsumptionDetail/', -4364776638, '', 98008
FROM dummy
WHERE NOT EXISTS (SELECT 1 FROM t_menu WHERE id = -4364776638);

INSERT INTO t_role_menu(id, VERSION, role_id, menu_id)
SELECT -4364776638 + id, 0, id, -4364776638
FROM t_role
WHERE tenant_type = 'club' AND NOT EXISTS(SELECT 1 FROM t_role_menu WHERE id = -4364776638 + t_role.id);

delete from premission where id=-4364776639;
INSERT INTO premission(id, auto_created, CODE, NAME, premission_type, parent_id)
  SELECT -4364776639, 1, 'report-mem-memCardRechargeLog', '会员储值情况统计表', 1, -436479471
  FROM dummy
  WHERE NOT EXISTS (SELECT 1 FROM premission WHERE id = -4364776639);

delete from t_menu where id=-4364776639;
INSERT INTO t_menu(id, auto_created, CODE, menu_name, icon_class, parent_id, state, url, premission_id, `group`, sort)
SELECT -4364776639, 1, 'report-mem-memCardRechargeLog', '会员储值情况统计表',
    'icon-list-ul', -436479471, 1, '#!/memCardRechargeLog/', -4364776639, '', 98009
FROM dummy
WHERE NOT EXISTS (SELECT 1 FROM t_menu WHERE id = -4364776639);

INSERT INTO t_role_menu(id, VERSION, role_id, menu_id)
SELECT -4364776639 + id, 0, id, -4364776639
FROM t_role
WHERE tenant_type = 'club' AND NOT EXISTS(SELECT 1 FROM t_role_menu WHERE id = -4364776639 + t_role.id);

delete from premission where id=-436480473;
INSERT INTO premission(id, auto_created, CODE, NAME, premission_type, parent_id)
  SELECT -436480473, 1, 'report-warehouse', '库存管理报表', 1, 0
  FROM dummy
  WHERE NOT EXISTS (SELECT 1 FROM premission WHERE id = -436480473);

delete from t_menu where id=-436480473;
INSERT INTO t_menu(id, auto_created, CODE, menu_name, icon_class, parent_id, state, url, premission_id, `group`, sort)
SELECT -436480473, 1, 'report-warehouse', '库存管理报表',
    'icon-list-ul', 0, 0, '', -436480473, '', 97000
FROM dummy
WHERE NOT EXISTS (SELECT 1 FROM t_menu WHERE id = -436480473);

INSERT INTO t_role_menu(id, VERSION, role_id, menu_id)
SELECT -436480473 + id, 0, id, -436480473
FROM t_role
WHERE tenant_type = 'club' AND NOT EXISTS(SELECT 1 FROM t_role_menu WHERE id = -436480473 + t_role.id);

delete from premission where id=-436481473;
INSERT INTO premission(id, auto_created, CODE, NAME, premission_type, parent_id)
  SELECT -436481473, 1, 'report-reserve', '场地预订报表', 1, 0
  FROM dummy
  WHERE NOT EXISTS (SELECT 1 FROM premission WHERE id = -436481473);

delete from t_menu where id=-436481473;
INSERT INTO t_menu(id, auto_created, CODE, menu_name, icon_class, parent_id, state, url, premission_id, `group`, sort)
SELECT -436481473, 1, 'report-reserve', '场地预订报表',
    'icon-list-ul', 0, 0, '', -436481473, '', 98000
FROM dummy
WHERE NOT EXISTS (SELECT 1 FROM t_menu WHERE id = -436481473);

INSERT INTO t_role_menu(id, VERSION, role_id, menu_id)
SELECT -436481473 + id, 0, id, -436481473
FROM t_role
WHERE tenant_type = 'club' AND NOT EXISTS(SELECT 1 FROM t_role_menu WHERE id = -436481473 + t_role.id);
