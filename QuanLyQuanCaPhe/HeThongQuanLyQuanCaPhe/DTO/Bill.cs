using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeThongQuanLyQuanCaPhe.DTO
{
    public class Bill
    {
        public Bill(int id, DateTime? dateCheckin, DateTime? dateCheckOut, int status, int discount = 0)
        {
            this.ID = id;
            this.DateCheckIn = dateCheckin;
            this.DateCheckOut = dateCheckOut;
            this.Status = status;
            this.Discount = discount;   
        }

        public int ID;
        public int _ID
        {
            get { return ID; }
            set { ID = value; }
        }

        public DateTime? dateCheckOut;
        public DateTime? DateCheckOut 
        { 
            get { return dateCheckOut; } 
            set { dateCheckOut = value; } 
        }


        public DateTime? dateCheckIn;  
        public DateTime? DateCheckIn 
        { 
            get { return dateCheckIn; } 
            set { dateCheckIn = value; }    
        }

        public int discount;

        public int Discount
        {
            get { return discount; }
            set { discount = value; }
        }

        public int status;
        public int Status
        {
            get { return status; }
            set { status = value; }
        }

        public Bill(DataRow row)
        {
            this.ID = (int)row["id"];
            this.DateCheckIn = (DateTime?)row["dateCheckin"];

            var dateCheckOutTemp = row["dateCheckOut"];
            if (dateCheckOutTemp.ToString() != "")
                this.DateCheckOut = (DateTime?)dateCheckOutTemp;

            this.Status = (int)row["status"];

            if (row["discount"].ToString() != "")
                this.Discount = (int)row["discount"];
        }
    }
}
