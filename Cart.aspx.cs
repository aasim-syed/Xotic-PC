using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class Cart : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           
            BindProductCart();
        }

    }

    protected void btnRemoveItem_Click(object sender, EventArgs e)
    {

    }

    protected void btnBuyNow_Click(object sender, EventArgs e)
    {

    }

    private void BindProductCart()
    {
        
            string CookieData = Request.Cookies["CartPID"].Value.Split('=')[1];
            string[] CookieDataArray = CookieData.Split(',');

            if (CookieDataArray.Length > 0)
            {
                noofitems.InnerText = "MY CART (" + CookieDataArray.Length + " Items)";
                DataTable dtBrands = new DataTable();
                Int64 CartTotal = 0;
                Int64 Total = 0;
                for (int i = 0; i < CookieDataArray.Length; i++)
                {
                    string PID = CookieDataArray[i].ToString().Split('-')[0];
                    string SizeID = CookieDataArray[i].ToString().Split('-')[1];

                    string strConnString = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;

                    SqlConnection con = new SqlConnection(strConnString);


                    SqlCommand cmd = new SqlCommand("select top(1) A.*,B.* from Products A inner join ProductsImages B on A.pid = '"
                        + PID + "'", con);
                        
                            cmd.CommandType = CommandType.Text;
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    { 
                                sda.Fill(dtBrands);
                            

                        
                    }
                    CartTotal += Convert.ToInt64(dtBrands.Rows[i]["price"]);
                    Total += Convert.ToInt64(dtBrands.Rows[i]["sellingprice"]);
                }
                rptrCartProducts.DataSource = dtBrands;
                rptrCartProducts.DataBind();
                divPriceDetails.Visible = true;

                spanCartTotal.InnerText = CartTotal.ToString();
                spanTotal.InnerText = "Rs. " + Total.ToString();
                spanDiscount.InnerText = "- " + (CartTotal - Total).ToString();
            }
           else
            {
                //TODO Show Empty Cart
                noofitems.InnerText = "Your Shopping Cart is Empty";
                divPriceDetails.Visible = false;
            } 
        }
     
    

}