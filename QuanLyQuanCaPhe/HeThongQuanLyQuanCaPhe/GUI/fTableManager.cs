//Main
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using HeThongQuanLyQuanCaPhe.DAO;
using HeThongQuanLyQuanCaPhe.DTO;
using System.Data;
using static HeThongQuanLyQuanCaPhe.fAccountProfile;

namespace HeThongQuanLyQuanCaPhe
{
    public partial class fTableManager : Form
    {
        #region Lấy thông tin Account
        public Account loginAccount;
        public Account LoginAccount
        {
            get { return loginAccount; }
            set { loginAccount = value; ChangeAccount(loginAccount.Type); }
        }
        #endregion

        #region Load form
        public fTableManager(Account acc)
        {
            InitializeComponent();

            this.LoginAccount = acc;

            LoadTable();
            LoadCategory();
            LoadComboboxTable(cbSwitchTable);
        }
        void LoadTable()
        {
            flpTable.Controls.Clear();

            List<Table> tableList = TableDAO.Instance.LoadTableList();

            foreach (Table item in tableList)
            {
                Button btn = new Button() { Width = TableDAO.TableWidth, Height = TableDAO.TableHeight };
                btn.Text = item.Name + Environment.NewLine + item.Status;
                btn.Click += btn_Click;
                btn.Tag = item;

                switch (item.Status)
                {
                    case "Trống":
                        btn.BackColor = Color.Aqua;
                        break;
                    default:
                        btn.BackColor = Color.LightPink;
                        break;
                }

                flpTable.Controls.Add(btn);
            }
        }
        void LoadCategory()
        {
            List<Category> listCategory = CategoryDAO.Instance.GetListCategory();
            cbCategory.DataSource = listCategory;
            cbCategory.DisplayMember = "Name";

        }
        void LoadComboboxTable(ComboBox cb)
        {
            cb.DataSource = TableDAO.Instance.LoadTableList();
            cb.DisplayMember = "Name";
        }
        void btn_Click(object sender, EventArgs e)
        {
            int tableID = ((sender as Button).Tag as Table).ID;
            lsvBill.Tag = (sender as Button).Tag;
            ShowBill(tableID);
        }
        #endregion

        #region Thêm món
        void btnAddFood_Click(object sender, EventArgs e)
        {
            Table table = lsvBill.Tag as Table;

            if (table == null)
            {
                MessageBox.Show("Hãy chọn bàn");
                return;
            }

            string sqlStr = string.Format("update TableFood set status = N'Có người' where id = {0}", table.ID);
            DataProvider.Instance.ExecuteNonQuery(sqlStr);
            int idBill = BillDAO.Instance.GetUncheckBillByTableID(table.ID);
            int foodID = (cbFood.SelectedItem as Food).ID;
            int count = (int)nmFoodCount.Value;

            if (idBill == -1)
            {
                BillDAO.Instance.InsertBill(table.ID);
                BillInfoDAO.Instance.InsertBillInfo(BillDAO.Instance.GetMaxBill(), foodID, count);
            }
            else
            {
                BillInfoDAO.Instance.InsertBillInfo(idBill, foodID, count);
            }

            ShowBill(table.ID);

            LoadTable();
        }
        #endregion

        #region Chuyển bàn
        private void btnSwitchTable_Click(object sender, EventArgs e)
        {
            Table table = lsvBill.Tag as Table;

            if (table == null)
            {
                MessageBox.Show("Hãy chọn bàn");
                return;
            }
            int id1 = (lsvBill.Tag as Table).ID;
            int id2 = (cbSwitchTable.SelectedItem as Table).ID;
            // Lấy trạng thái bàn cần chuyển
            string sqlstr = string.Format("select * from tablefood where id={0}",id1);
            DataTable table1 = DataProvider.Instance.ExecuteQuery(sqlstr);
            string status1 = table1.Rows[0].ItemArray[2].ToString();
            // Lấy trạng thái bàn được chuyển tới
            sqlstr = string.Format("select * from tablefood where id={0}", id2);
            table1 = DataProvider.Instance.ExecuteQuery(sqlstr);
            string status2 = table1.Rows[0].ItemArray[2].ToString();
            // Nếu bàn cần chuyển trống thì ko có hoá đơn => không thể chuyển
            // Nếu bàn đc chuyển tới có hoá đơn => Không thẻ chuyển
            if (status1=="Trống") MessageBox.Show(string.Format("Bàn {0} không có hoá đơn, không thể chuyển bàn!", id1 - 1));
            else
            if (status2=="Có người") MessageBox.Show(string.Format("Bàn {0} đã có hoá đơn, không thể chuyển bàn!",id2-1));
            else
            if (MessageBox.Show(string.Format("Bạn có thật sự muốn chuyển bàn {0} qua bàn {1}", (lsvBill.Tag as Table).Name, (cbSwitchTable.SelectedItem as Table).Name), "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                TableDAO.Instance.SwitchTable(id1, id2);
                string sqlStr = string.Format("update TableFood set status = N'Có người' where id = {0}", id2);
                DataProvider.Instance.ExecuteNonQuery(sqlStr);
                sqlStr = string.Format("update TableFood set status = N'Trống' where id = {0}", id1);
                DataProvider.Instance.ExecuteNonQuery(sqlStr);
                LoadTable();
            }
        }
        #endregion

        #region Thanh toán
        void btnCheckOut_Click(object sender, EventArgs e)
        {
            Table table = lsvBill.Tag as Table;

            if (table == null)
            {
                MessageBox.Show("Hãy chọn bàn");
                return;
            }

            int idBill = BillDAO.Instance.GetUncheckBillByTableID(table.ID);
            int discount = (int)nmDisCount.Value;

            double totalPrice = Convert.ToDouble(txbTotalPrice.Text);
            double finalTotalPrice = totalPrice - (totalPrice / 100) * discount;

            if (totalPrice==0)
            {
                MessageBox.Show("Hoá đơn trống, không thể thanh toán!");
            }    
            else
            if (idBill != -1)
            {
                if (MessageBox.Show(string.Format("Bạn có chắc thanh toán hóa đơn cho bàn {0}\nTổng tiền - (Tổng tiền / 100) x Giảm giá\n=> {1} - ({1} / 100) x {2} = {3}", table.Name, totalPrice, discount, finalTotalPrice), "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                {
                    BillDAO.Instance.CheckOut(idBill, discount, (float)finalTotalPrice);
                    ShowBill(table.ID);
                    string sqlStr = string.Format("update TableFood set status = N'Trống' where id = {0}", table.ID);
                    DataProvider.Instance.ExecuteNonQuery(sqlStr);
                    LoadTable();
                }
            }
        }
        #endregion

        #region Load tên người dùng
        void ChangeAccount(int type)
        {
            adminToolStripMenuItem.Enabled = type == 1;
            thôngTinTàiKhoảnToolStripMenuItem.Text += " (" + LoginAccount.DisplayName + ")";
        }
        #endregion

        #region Sự kiện click Menu Strip
        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void thôngTinCáNhânToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fAccountProfile f = new fAccountProfile(LoginAccount);
            f.UpdateAccount += f_UpdateAccount;
            f.ShowDialog();
        }
        private void adminToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fAdmin f = new fAdmin();
            f.loginAccount = LoginAccount;
            if (loginAccount.Type==1)
            {
                f.InsertFood += f_InsertFood;
                f.DeleteFood += f_DeleteFood;
                f.UpdateFood += f_UpdateFood;
                f.InsertFoodCategory += f_InsertFoodCategory;
                f.DeleteFoodCategory += f_DeleteFoodCategory;
                f.UpdateFoodCategory += f_UpdateFoodCategory;
                f.InsertTable += f_InsertTable;
                f.DeleteTable += f_DeleteTable;
                f.UpdateTable += f_UpdateTable;
                f.ShowDialog();
            }    
            
        }
        void f_UpdateAccount(object sender, AccountEvent e)
        {
            thôngTinTàiKhoảnToolStripMenuItem.Text = "Thông tin tài khoản (" + e.Acc.DisplayName + ")";
        }
        void f_UpdateFood(object sender, EventArgs e)
        {
            if (cbCategory.SelectedItem != null)
                LoadFoodListByCategoryID((cbCategory.SelectedItem as Category).ID);
            if (lsvBill.Tag != null)
                ShowBill((lsvBill.Tag as Table).ID);
        }
        void f_DeleteFood(object sender, EventArgs e)
        {
            if (cbCategory.SelectedItem != null)
                LoadFoodListByCategoryID((cbCategory.SelectedItem as Category).ID);
            if (lsvBill.Tag != null)
                ShowBill((lsvBill.Tag as Table).ID);
            LoadTable();
        }
        void f_InsertFood(object sender, EventArgs e)
        {
            if (cbCategory.SelectedItem != null)
                LoadFoodListByCategoryID((cbCategory.SelectedItem as Category).ID);
            if (lsvBill.Tag != null)
                ShowBill((lsvBill.Tag as Table).ID);
        }
        void f_InsertFoodCategory(object sender, EventArgs e)
        {
            if (cbCategory.SelectedItem != null)
                LoadFoodListByCategoryID((cbCategory.SelectedItem as Category).ID);
            if (lsvBill.Tag != null)
                ShowBill((lsvBill.Tag as Table).ID);
        }
        void f_UpdateFoodCategory(object sender, EventArgs e)
        {
            if (cbCategory.SelectedItem != null)
                LoadFoodListByCategoryID((cbCategory.SelectedItem as Category).ID);
            if (lsvBill.Tag != null)
                ShowBill((lsvBill.Tag as Table).ID);
        }
        void f_DeleteFoodCategory(object sender, EventArgs e)
        {
            if (cbCategory.SelectedItem != null)
                LoadFoodListByCategoryID((cbCategory.SelectedItem as Category).ID);
            if (lsvBill.Tag != null)
                ShowBill((lsvBill.Tag as Table).ID);
            LoadTable();
        }
        void f_InsertTable(object sender, EventArgs e)
        {
            if (cbSwitchTable.SelectedItem != null)
                LoadTableByTableID((cbSwitchTable.SelectedItem as Table).ID);
            if (lsvBill.Tag != null)
                ShowBill((lsvBill.Tag as Table).ID);
            LoadTable();
        }
        void f_UpdateTable(object sender, EventArgs e)
        {
            if (cbSwitchTable.SelectedItem != null)
                LoadTableByTableID((cbSwitchTable.SelectedItem as Table).ID);
            if (lsvBill.Tag != null)
                ShowBill((lsvBill.Tag as Table).ID);
            LoadTable();
        }
        void f_DeleteTable(object sender, EventArgs e)
        {
            if (cbSwitchTable.SelectedItem != null)
                LoadTableByTableID((cbSwitchTable.SelectedItem as Table).ID);
            if (lsvBill.Tag != null)
                ShowBill((lsvBill.Tag as Table).ID);
            LoadTable();
        }
        void cbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = 0;

            ComboBox cb = sender as ComboBox;

            if (cb.SelectedItem == null)
                return;

            Category selected = cb.SelectedItem as Category;
            id = selected.ID;

            LoadFoodListByCategoryID(id);
        }
        #endregion

        void LoadFoodListByCategoryID(int id)
        {
            List<Food> listFood = FoodDAO.Instance.GetFoodByCategoryID(id);
            cbFood.DataSource = listFood;
            cbFood.DisplayMember = "Name";
        }
        void ShowBill(int id)
        {
            lsvBill.Items.Clear();
            List<HeThongQuanLyQuanCaPhe.DTO.Menu> listBillInfo = MenuDAO.Instance.GetListMenuByTable(id);
            float totalPrice = 0;
            foreach (HeThongQuanLyQuanCaPhe.DTO.Menu item in listBillInfo)
            {
                ListViewItem lsvItem = new ListViewItem(item.FoodName.ToString());
                lsvItem.SubItems.Add(item.Count.ToString());
                lsvItem.SubItems.Add(item.Price.ToString());
                lsvItem.SubItems.Add(item.TotalPrice.ToString());
                totalPrice += item.TotalPrice;
                lsvBill.Items.Add(lsvItem);
            }
            CultureInfo culture = new CultureInfo("vi-VN");

            Thread.CurrentThread.CurrentCulture = culture;

            txbTotalPrice.Text = totalPrice.ToString();

        }
        void LoadTableByTableID(int id)
        {
            List<Table> listTable = TableDAO.Instance.GetTableID(id);
        }
    }
}
