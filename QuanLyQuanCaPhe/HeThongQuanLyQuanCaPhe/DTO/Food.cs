using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeThongQuanLyQuanCaPhe.DTO
{
    public class Food
    {
        public Food() { }
        public Food (int id, string name, int categoryID, float price)
        {
            this.ID = id;
            this.CategoryID = categoryID;
            this.Name = name;
            this.Price = price;
        }
        public Food(DataRow row)
        {
            this.ID = (int)row["id"];
            this.CategoryID = (int)row["idCategory"];//Phai de ten trung voi ten truong trong csdl no moi tham chieu duoc.
            this.Name = row["name"].ToString();
            this.Price = (float)Convert.ToDouble(row["price"].ToString());
        }

        private int iD;

        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }

        private int categoryID;
        public int CategoryID
        {
            get { return categoryID; }
            set { categoryID = value; }
        }

        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private float price;
        public float Price
        {
            get { return price; }
            set { price = value; }
        }
    }
}
