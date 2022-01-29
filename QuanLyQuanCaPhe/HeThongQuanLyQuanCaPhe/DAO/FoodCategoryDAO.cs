using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HeThongQuanLyQuanCaPhe.DTO;

namespace HeThongQuanLyQuanCaPhe.DAO
{
    class FoodCategoryDAO
    {
        public static FoodCategoryDAO instance;

        public static FoodCategoryDAO Instance
        {
            get { if (instance == null) instance = new FoodCategoryDAO(); return FoodCategoryDAO.instance; }
            private set { FoodCategoryDAO.instance = value; }
        }

        public FoodCategoryDAO() { }

        public List<FoodCategory> GetListFoodCategory()
        {
            List<FoodCategory> list = new List<FoodCategory>();
            string query = "select * from FoodCategory";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                FoodCategory foodCategory = new FoodCategory(item);
                list.Add(foodCategory);
            }
            return list;
        }

        public bool InsertFoodCategory(string name)
        {
            string query = string.Format("INSERT dbo.FoodCategory ( name) VALUES  ( N'{0}')", name);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }

        public bool checkExistFoodCategoryInMenu(string name)
        {
            // Lấy id của sản phẩm dựa trên tên sản phẩm (name)
            string query = string.Format("Select TOP 1 * from dbo.FoodCategory where name = N'{0}'", name);
            /*
             * Nếu mà id > 0 => có sản phẩm này trong data rồi
             * = 0 hoặc = null => chưa có sản phẩm này
             */
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            //int result = data.Rows;
            //int result = DataProvider.Instance.ExecuteNonQuery(query);

            return data.Rows.Count > 0;
        }

        public bool UpdateFoodCategory(string name, int id)
        {
            string query = string.Format("UPDATE dbo.FoodCategory SET name = N'{0}' WHERE id = {1}", name, id);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }

        public bool DeleteFoodCategory(int id)
        {
            string query = string.Format("DELETE FoodCategory WHERE id = {0}", id);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }
    }
}
