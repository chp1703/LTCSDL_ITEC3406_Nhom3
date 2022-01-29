using HeThongQuanLyQuanCaPhe.DAO;
using HeThongQuanLyQuanCaPhe.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HeThongQuanLyQuanCaPhe
{
    public partial class fAdmin : Form
    {
        BindingSource foodList = new BindingSource();

        BindingSource foodListCategory = new BindingSource();

        BindingSource accountList = new BindingSource();

        public Account loginAccount;

        #region Load form
        void Load()
        {
            dtgvFood.DataSource = foodList;
            dtgvCategory.DataSource = foodListCategory;
            dtgvAccount.DataSource = accountList;

            LoadDateTimePickerBill();
            LoadListFood();
            LoadListTable();
            LoadAccount();
            LoadCategoryIntoCombobox(cbFoodCategory);
            LoadTableIntoCombobox(cbTableStatus);
            AddFoodBinding();
            AddFoodCategoryBinding();
            AddTableBiding();
            AddAccountBinding();
            LoadListFoodCategory();
        }
        public fAdmin()
        {
            InitializeComponent();
            Load();
        }
        void LoadAccount()
        {
            accountList.DataSource = AccountDAO.Instance.GetListAccount();
        }

        void LoadDateTimePickerBill()
        {
            DateTime today = DateTime.Now;
            dtpkFromDate.Value = new DateTime(today.Year, today.Month, 1);
            dtpkToDate.Value = dtpkFromDate.Value.AddMonths(1).AddDays(-1);
        }
        void LoadCategoryIntoCombobox(ComboBox cb)
        {
            cb.DataSource = CategoryDAO.Instance.GetListCategory();
            cb.DisplayMember = "Name";
        }

        void LoadTableIntoCombobox(ComboBox cb)
        {
            cb.DataSource = TableDAO.Instance.GetListTable();
            cb.DisplayMember = "Status";
        }

        void LoadListFood()
        {
            foodList.DataSource = FoodDAO.Instance.GetListFood();
        }
        void LoadListFoodCategory()
        {
            foodListCategory.DataSource = FoodCategoryDAO.Instance.GetListFoodCategory();
        }

        void LoadListTable()
        {
            dtgvTable.DataSource = TableDAO.Instance.GetListTable();
        }

        void AddAccountBinding()
        {
            txbUserName.DataBindings.Add(new Binding("Text", dtgvAccount.DataSource, "UserName", true, DataSourceUpdateMode.Never));
            txbDisPlayName.DataBindings.Add(new Binding("Text", dtgvAccount.DataSource, "DisplayName", true, DataSourceUpdateMode.Never));
            numericUpDown1.DataBindings.Add(new Binding("Value", dtgvAccount.DataSource, "Type", true, DataSourceUpdateMode.Never));
        }

        void AddFoodBinding()
        {
            txbFoodName.DataBindings.Add(new Binding("Text", dtgvFood.DataSource, "Name", true, DataSourceUpdateMode.Never));
            txbFoodID.DataBindings.Add(new Binding("Text", dtgvFood.DataSource, "ID", true, DataSourceUpdateMode.Never));
            nmFoodPrice.DataBindings.Add(new Binding("Value", dtgvFood.DataSource, "Price", true, DataSourceUpdateMode.Never));
        }

        void AddFoodCategoryBinding()
        {
            txbCategoryName.DataBindings.Add(new Binding("Text", dtgvCategory.DataSource, "Name", true, DataSourceUpdateMode.Never));
            txbCategoryID.DataBindings.Add(new Binding("Text", dtgvCategory.DataSource, "ID", true, DataSourceUpdateMode.Never));
        }

        void AddTableBiding()
        {
            txbTableID.DataBindings.Add(new Binding("Text", dtgvTable.DataSource, "ID", true, DataSourceUpdateMode.Never));
            txbTableName.DataBindings.Add(new Binding("Text", dtgvTable.DataSource, "Name", true, DataSourceUpdateMode.Never));
        }
        #endregion

        #region Tab Doanh thu
        private void btnViewBill_Click(object sender, EventArgs e)
        {
            LoadListBillByDate(dtpkFromDate.Value, dtpkToDate.Value);
        }

        void LoadListBillByDate(DateTime checkIn, DateTime checkOut)
        {
            dtgvBill.DataSource = BillDAO.Instance.GetBillListByDate(checkIn, checkOut);
        }
        #endregion

        #region Tab Thức ăn
        private void btnSearchFood_Click(object sender, EventArgs e)
        {
            foodList.DataSource = SearchFoodByName(txbSearchFoodName.Text);
        }
        private void btnAddFood_Click(object sender, EventArgs e)
        {
            string name = txbFoodName.Text;
            int categoryID = (cbFoodCategory.SelectedItem as Category).ID;
            float price = (float)nmFoodPrice.Value;

            // Kiểm tra món này có trong menu chưa
            bool isExistFood = FoodDAO.Instance.checkExistFoodInMenu(name);
            /*
             * Nếu chưa tồn tại food name trong data thì 
             * mới cho lưu xuống data, còn hông thì hông 
             * lưu
             */
            if (isExistFood == false)
            {
                if (FoodDAO.Instance.InsertFood(name, categoryID, price))
                {
                    MessageBox.Show("Thêm món thành công");
                    LoadListFood();
                    LoadCategoryIntoCombobox(cbFoodCategory);
                    if (insertFood != null)
                        insertFood(this, new EventArgs());
                }
                else
                {
                    MessageBox.Show("Có lỗi khi thêm thức ăn");
                }
            }
            else
            {
                MessageBox.Show("Đã có món này trong danh sách món!!");
            }
        }
        private void btnDeleteFood_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txbFoodID.Text);

            if (FoodDAO.Instance.DeleteFood(id))
            {
                MessageBox.Show("Xóa món thành công");
                LoadListFood();
                if (deleteFood != null)
                    deleteFood(this, new EventArgs());
            }
            else
            {
                MessageBox.Show("Có lỗi khi xóa thức ăn");
            }
        }
        private void btnEditFood_Click(object sender, EventArgs e)
        {
            string name = txbFoodName.Text;
            int categoryID = (cbFoodCategory.SelectedItem as Category).ID;
            float price = (float)nmFoodPrice.Value;
            int id = Convert.ToInt32(txbFoodID.Text);

            if (FoodDAO.Instance.UpdateFood(id, name, categoryID, price))
            {
                MessageBox.Show("Sửa món thành công");
                LoadListFood();
                if (updateFood != null)
                    updateFood(this, new EventArgs());
            }
            else
            {
                MessageBox.Show("Có lỗi khi sửa thức ăn");
            }
        }
        List<Food> SearchFoodByName(string name)
        {
            List<Food> listFood = FoodDAO.Instance.SearchFoodByName(name);

            return listFood;
        }
        private void btnShowFood_Click(object sender, EventArgs e)
        {
            LoadListFood();
        }
        private void txbFoodID_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (dtgvFood.SelectedCells.Count > 0)
                {
                    int id = (int)dtgvFood.SelectedCells[0].OwningRow.Cells["CategoryID"].Value;

                    Category cateogory = CategoryDAO.Instance.GetCategoryByID(id);

                    cbFoodCategory.SelectedItem = cateogory;

                    int index = -1;
                    int i = 0;
                    foreach (Category item in cbFoodCategory.Items)
                    {
                        if (item.ID == cateogory.ID)
                        {
                            index = i;
                            break;
                        }
                        i++;
                    }

                    cbFoodCategory.SelectedIndex = index;
                }
            }
            catch { }
        }
        #endregion

        #region Tab Danh mục
        private void btnAddCategory_Click(object sender, EventArgs e)
        {
            string name = txbCategoryName.Text;

            // Kiểm tra danh mục này có trong menu chưa
            bool isExistFoodCategory = FoodCategoryDAO.Instance.checkExistFoodCategoryInMenu(name);

            if (isExistFoodCategory == false)
            {
                if (FoodCategoryDAO.Instance.InsertFoodCategory(name))
                {
                    MessageBox.Show("Thêm danh mục thành công!!");
                    LoadListFoodCategory();
                    if (insertFoodCategory != null)
                        insertFoodCategory(this, new EventArgs());
                }
                else
                {
                    MessageBox.Show("Có lỗi khi thêm danh mục!!");
                }
            }
            else
            {
                MessageBox.Show("Danh mục này đã tồn tại!!");
            }

        }
        private void btnEditCategory_Click(object sender, EventArgs e)
        {
            string name = txbCategoryName.Text;
            int id = Convert.ToInt32(txbCategoryID.Text);

            if (FoodCategoryDAO.Instance.UpdateFoodCategory(name, id))
            {
                MessageBox.Show("Sửa danh mục thành công!!");
                LoadListFoodCategory();
                if (updateFoodCategory != null)
                    updateFoodCategory(this, new EventArgs());
            }
            else
            {
                MessageBox.Show("Có lỗi khi sửa danh mục");
            }
        }
        private void btnDeleteCategory_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txbCategoryID.Text);

            if (FoodCategoryDAO.Instance.DeleteFoodCategory(id))
            {
                MessageBox.Show("Xóa danh mục thành công!!");
                LoadListFoodCategory();
                if (deleteFoodCategory != null)
                    deleteFoodCategory(this, new EventArgs());
            }
            else
            {
                MessageBox.Show("Có lỗi khi xóa danh mục!!");
            }
        }
        private void bthShowCategory_Click(object sender, EventArgs e)
        {
            LoadListFoodCategory();
        }
        private void txbCategoryID_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (dtgvCategory.SelectedCells.Count > 0)
                {
                    int id = (int)dtgvCategory.SelectedCells[0].OwningRow.Cells["CategoryID"].Value;

                    Category cateogory = CategoryDAO.Instance.GetCategoryByID(id);

                    cbFoodCategory.SelectedItem = cateogory;

                    int index = -1;
                    int i = 0;
                    foreach (Category item in cbFoodCategory.Items)
                    {
                        if (item.ID == cateogory.ID)
                        {
                            index = i;
                            break;
                        }
                        i++;
                    }

                    cbFoodCategory.SelectedIndex = index;
                }
            }
            catch { }
        }
        #endregion

        #region Tab bàn ăn
        private void btnAddTable_Click(object sender, EventArgs e)
        {
            string name = txbTableName.Text;
            string status = (cbTableStatus.SelectedItem as Table).Status;

            // Kiểm tra danh mục này có trong menu chưa
            bool isExistTable = TableDAO.Instance.checkExistTable(name);

            if (isExistTable == false)
            {
                if (TableDAO.Instance.InsertTable(name, status))
                {
                    MessageBox.Show("Thêm bàn thành công!!");
                    LoadListTable();
                    LoadTableIntoCombobox(cbTableStatus);
                    if (insertFoodCategory != null)
                        insertFoodCategory(this, new EventArgs());
                }
                else
                {
                    MessageBox.Show("Có lỗi khi thêm bàn!!");
                }
            }
            else
            {
                MessageBox.Show("Bàn này đã tồn tại!!");
            }
        }
        private void btnEditTable_Click(object sender, EventArgs e)
        {
            string name = txbTableName.Text;
            string status = (cbTableStatus.SelectedItem as Table).Status;
            int id = Convert.ToInt32(txbTableID.Text);

            if (TableDAO.Instance.UpdateTable(name, id, status))
            {
                MessageBox.Show("Sửa bàn thành công!!");
                LoadListTable();
                if (updateFood != null)
                    updateFood(this, new EventArgs());
            }
            else
            {
                MessageBox.Show("Có lỗi khi sửa bàn!!");
            }
        }
        private void btnDeleteTable_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txbTableID.Text);

            if (TableDAO.Instance.DeleteTable(id))
            {
                MessageBox.Show("Xóa bàn thành công!!");
                LoadListTable();
                if (deleteFood != null)
                    deleteFood(this, new EventArgs());
            }
            else
            {
                MessageBox.Show("Có lỗi khi xóa bàn!!");
            }
        }
        private void btnShowTable_Click(object sender, EventArgs e)
        {
            LoadListTable();
        }
        #endregion

        #region Tab tài khoản
        private void btnAddAccount_Click(object sender, EventArgs e)
        {
            string userName = txbUserName.Text;
            string displayName = txbDisPlayName.Text;
            int type = (int)numericUpDown1.Value;

            AddAccount(userName, displayName, type);
        }
        private void btnDeleteAccount_Click(object sender, EventArgs e)
        {
            string userName = txbUserName.Text;

            DeleteAccount(userName);
        }
        private void btnEditAccount_Click(object sender, EventArgs e)
        {
            string userName = txbUserName.Text;
            string displayName = txbDisPlayName.Text;
            int type = (int)numericUpDown1.Value;

            EditAccount(userName, displayName, type);
        }
        private void btnResetPassword_Click(object sender, EventArgs e)
        {
            string userName = txbUserName.Text;

            ResetPass(userName);
        }
        private void btnSearchAccount_Click(object sender, EventArgs e)
        {
            accountList.DataSource = SearchAccountByName(txbSearchAccount.Text);
        }
        void AddAccount(string userName, string displayName, int type)
        {
            // Kiểm tra món này có trong menu chưa
            bool isExistAccount = AccountDAO.Instance.checkExistAccount(userName);

            if (isExistAccount == false)
            {
                if (AccountDAO.Instance.InsertAccount(userName, displayName, type))
                {
                    MessageBox.Show("Thêm tài khoản thành công");
                }
                else
                {
                    MessageBox.Show("Thêm tài khoản thất bại");
                }

                LoadAccount();
            }
            else
            {
                MessageBox.Show("Tên tài khoản bị trùng!!");
            }
        }
        void EditAccount(string userName, string displayName, int type)
        {
            if (AccountDAO.Instance.UpdateAccount(userName, displayName, type))
            {
                MessageBox.Show("Cập nhật tài khoản thành công");
            }
            else
            {
                MessageBox.Show("Cập nhật tài khoản thất bại");
            }

            LoadAccount();
        }
        void DeleteAccount(string userName)
        {
            //if (loginAccount.UserName.Equals(userName))
            //{
            //    MessageBox.Show("Vui lòng đừng xóa chính bạn chứ");
            //    return;
            //}
            if (AccountDAO.Instance.DeleteAccount(userName))
            {
                MessageBox.Show("Xóa tài khoản thành công");
            }
            else
            {
                MessageBox.Show("Xóa tài khoản thất bại");
            }

            LoadAccount();
        }
        void ResetPass(string userName)
        {
            if (AccountDAO.Instance.ResetPassword(userName))
            {
                MessageBox.Show("Đặt lại mật khẩu thành công");
            }
            else
            {
                MessageBox.Show("Đặt lại mật khẩu thất bại");
            }
        }
        List<Account> SearchAccountByName(string username)
        {
            List<Account> listAccount = AccountDAO.Instance.SearchAccountByName(username);

            return listAccount;
        }
        private void btnShowAccount_Click(object sender, EventArgs e)
        {
            LoadAccount();
        }
        #endregion

        #region EventHandler
        private event EventHandler insertFood;
        public event EventHandler InsertFood
        {
            add { insertFood += value; }
            remove { insertFood -= value; }
        }

        private event EventHandler deleteFood;
        public event EventHandler DeleteFood
        {
            add { deleteFood += value; }
            remove { deleteFood -= value; }
        }

        private event EventHandler updateFood;
        public event EventHandler UpdateFood
        {
            add { updateFood += value; }
            remove { updateFood -= value; }
        }

        private event EventHandler insertFoodCategory;
        public event EventHandler InsertFoodCategory
        {
            add { insertFoodCategory += value; }
            remove { insertFoodCategory -= value; }
        }

        private event EventHandler deleteFoodCategory;
        public event EventHandler DeleteFoodCategory
        {
            add { deleteFoodCategory += value; }
            remove { deleteFoodCategory -= value; }
        }

        private event EventHandler updateFoodCategory;
        public event EventHandler UpdateFoodCategory
        {
            add { updateFoodCategory += value; }
            remove { updateFoodCategory -= value; }
        }


         private event EventHandler insertTable;
        public event EventHandler InsertTable
        {
            add { insertTable += value; }
            remove { insertTable -= value; }
        }

        private event EventHandler deleteTable;
        public event EventHandler DeleteTable
        {
            add { deleteTable += value; }
            remove { deleteTable -= value; }
        }

        private event EventHandler updateTable;
        public event EventHandler UpdateTable
        {
            add { updateTable += value; }
            remove { updateTable -= value; }
        }
        #endregion
        
    }

}

