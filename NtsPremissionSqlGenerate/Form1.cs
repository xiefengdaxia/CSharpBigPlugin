using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NtsPremissionSqlGenerate
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        const long pid = -4364774630;

        private void button1_Click(object sender, EventArgs e)
        {
            string template = @"
delete from premission where id={0};
INSERT INTO premission(id, auto_created, CODE, NAME, premission_type, parent_id)
  SELECT {0}, 1, '{1}', '{2}', 1, {5}
  FROM dummy
  WHERE NOT EXISTS (SELECT 1 FROM premission WHERE id = {0});

delete from t_menu where id={0};
INSERT INTO t_menu(id, auto_created, CODE, menu_name, icon_class, parent_id, state, url, premission_id, `group`, sort)
SELECT {0}, 1, '{1}', '{2}',
    'icon-list-ul', {5}, {6}, '{3}', {0}, '', {4}
FROM dummy
WHERE NOT EXISTS (SELECT 1 FROM t_menu WHERE id = {0});

INSERT INTO t_role_menu(id, VERSION, role_id, menu_id)
SELECT {0} + id, 0, id, {0}
FROM t_role
WHERE tenant_type = 'club' AND NOT EXISTS(SELECT 1 FROM t_role_menu WHERE id = {0} + t_role.id);";
            int i = 1;
            List<PMenu> root = new List<PMenu>();
            PMenu p1 = new PMenu()
            {
                id = -436477463,
                code = "report-sales",
                name = "销售收入报表",
                parent_id = 0,
                url = null,
                sort = 90000,
                state=0
            };
            p1.subMenu = new List<Menu>();

            Menu m1 = new Menu()
            {
                id = -4364774630 - i,
                code = "report-sales-ticketSale",
                name = "门票销售情况统计表",
                parent_id = p1.id,
                url = "#!/ticketSale/",
                sort = p1.sort + i,
                state=1
            };
            i++;
            Menu m2 = new Menu()
            {
                id = -4364774630 - i,
                code = "report-sales-posbillOneLook",
                name = "账单一览表",
                parent_id = p1.id,
                url = "#!/posbillOneLook/",
                sort = p1.sort + i,
                state = 1
            };
            i++;
            Menu m3 = new Menu()
            {
                id = -4364774630 - i,
                code = "report-sales-returnPosbill",
                name = "账单调整日志表",
                parent_id = p1.id,
                url = "#!/returnPosbill/",
                sort = p1.sort + i,
                state = 1
            };
            i++;

            Menu m4 = new Menu()
            {
                id = -4364774630 - i,
                code = "report-sales-operaterIncomeAcount",
                name = "各收银员收款汇总报表",
                parent_id = p1.id,
                url = "#!/operaterIncomeAcount/",
                sort = p1.sort + i,
                state = 1
            };
            i++;

            Menu m5 = new Menu()
            {
                id = -4364774630 - i,
                code = "report-sales-productSaleDetail",
                name = "商品销售明细表",
                parent_id = p1.id,
                url = "#!/productSaleDetail/",
                sort = p1.sort + i,
                state = 1
            };
            i++;

            p1.subMenu.Add(m1);
            p1.subMenu.Add(m2);
            p1.subMenu.Add(m3);
            p1.subMenu.Add(m4);
            p1.subMenu.Add(m5);

            i += 1000;
            PMenu p2 = new PMenu()
            {
                id = -436477463 -i,
                code = "report-mem",
                name = "会员管理报表",
                parent_id = 0,
                url = null,
                sort = 95000,
                state = 0
            };
            p2.subMenu = new List<Menu>();
            Menu n1 = new Menu()
            {
                id = -4364774630 - i,
                code = "report-mem-newMember",
                name = "新增会员统计报表",
                parent_id = p2.id,
                url = "#!/newMember/",
                sort = p2.sort + i,
                state = 1
            };
            i++;
            Menu n2 = new Menu()
            {
                id = -4364774630 - i,
                code = "report-mem-memCardBalance",
                name = "会员余额情况统计表",
                parent_id = p2.id,
                url = "#!/memCardBalance/",
                sort = p2.sort + i,
                state = 1
            };
            i++;
            p2.subMenu.Add(n1);
            p2.subMenu.Add(n2);
            i += 1000;
            PMenu p3 = new PMenu()
            {
                id = -436477463 - i,
                code = "report-mem_RC",
                name = "会员充值与消费报表",
                parent_id = 0,
                url = null,
                sort = 96000,
                state = 0,
            };
            p3.subMenu = new List<Menu>();
            Menu o1 = new Menu()
            {
                id = -4364774630 - i,
                code = "report-mem-memConsumptionDetail",
                name = "会员消费情况统计表",
                parent_id = p3.id,
                url = "#!/memConsumptionDetail/",
                sort = p3.sort + i,
                state = 1
            };
            i++;
            Menu o2 = new Menu()
            {
                id = -4364774630 - i,
                code = "report-mem-memCardRechargeLog",
                name = "会员储值情况统计表",
                parent_id = p3.id,
                url = "#!/memCardRechargeLog/",
                sort = p3.sort + i,
                state = 1
            };
            i++;
            p3.subMenu.Add(o1);
            p3.subMenu.Add(o2);

            i += 1000;
            PMenu p4 = new PMenu()
            {
                id = -436477463 - i,
                code = "report-warehouse",
                name = "库存管理报表",
                parent_id = 0,
                url = null,
                sort = 97000,
                state = 0
            };

            i += 1000;
            PMenu p5 = new PMenu()
            {
                id = -436477463 - i,
                code = "report-reserve",
                name = "场地预订报表",
                parent_id = 0,
                url = null,
                sort = 98000,
                state = 0
            };

            root.Add(p1);
            root.Add(p2);
            root.Add(p3);
            root.Add(p4);
            root.Add(p5);

            foreach (PMenu item in root)
            {
                var str = string.Format(template, item.id, item.code, item.name, item.url, item.sort, item.parent_id,item.state);
                CSHelper.saveTextFile(str, "menu", "sql", true);

                if (item.subMenu == null)
                    continue;
                foreach (Menu subItem in item.subMenu)
                {
                    var str1 = string.Format(template, subItem.id, subItem.code, subItem.name, subItem.url, subItem.sort, subItem.parent_id,subItem.state);
                    CSHelper.saveTextFile(str1, "menu", "sql", true);
                }
            }
        }
    }

    public class Menu
    {
        public long id { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public long parent_id { get; set; }
        public string url { get; set; }
        public int sort { get; set; }
        public int state { get; set; }
    }

    public class PMenu : Menu
    {
        public List<Menu> subMenu { get; set; }
    }
}
