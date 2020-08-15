using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;


namespace WebDataAccessConnected
{
    public partial class Disconnected : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection("Data Source=.;Initial Catalog=julDotNetBatch2020;Integrated Security=True");
            SqlCommand cmd = new SqlCommand("select * from employeetb1", conn);
            SqlCommand cmd1 = new SqlCommand("select * from BookTable", conn);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
            DataSet ds = new DataSet();
            da.Fill(ds,"Emp");
            da1.Fill(ds, "Book");
            GridView1.DataSource = ds.Tables["Emp"];
            GridView1.DataBind();
            GridView2.DataSource = ds.Tables[1];
            GridView2.DataBind();




        }
    }
}