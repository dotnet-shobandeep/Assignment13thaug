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
    public partial class ConnectedObjects : System.Web.UI.Page
    {
        
        SqlConnection conn = new SqlConnection("Data Source=.;Initial Catalog=julDotNetBatch2020;Integrated Security=True");
        SqlCommand cmd;
        SqlDataReader dr;
        DataTable dt;
        public void ShowGrid()
        {
            conn.Open();
            cmd = new SqlCommand("select * from Employeetb1", conn);
            dr = cmd.ExecuteReader();
            dt = new DataTable();
            dt.Load(dr);
            GridView1.DataSource = dt;
            GridView1.DataBind();
            dr.Close();
            conn.Close();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) 
            { 
            ShowGrid();
            }

            //while (dr.Read())
            //{
            //    GridView1.DataSource = dr;
            //    //DropDownList1.DataSource = dr;
            //   // DropDownList1. = dr[1].ToString();
            //    GridView1.DataBind();
            //   // DropDownList1.DataBind();
            //}

            
            


        }

        protected void btninsertEmp_Click(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("insert into employeetb1(empName,empSal) values('"+TextBox2.Text+"',"+TextBox3.Text+")",conn);
            int x= cmd.ExecuteNonQuery();
            conn.Close();
            ShowGrid();
        }

        protected void btnUpdatePara_Click(object sender, EventArgs e)
        {
            conn.Open();
            cmd = new SqlCommand("update EmployeeTb1 set empName=@empname,empsal=@empsal where empId=@empid", conn);
            cmd.Parameters.Add("@empid", SqlDbType.Int).Value = Convert.ToInt32(TextBox1.Text);
            cmd.Parameters.Add("@empname", SqlDbType.VarChar,20).Value = TextBox2.Text;
            cmd.Parameters.Add("@empsal", SqlDbType.Float).Value = Convert.ToSingle(TextBox3.Text);
            //if (cmd.ExecuteNonQuery() > 0)
            //{
            //    Response.Write("alart(one row updated)");
            //}
            int x = cmd.ExecuteNonQuery();
            conn.Close();
            ShowGrid();

        }

        protected void BtnDeleteWithsp_Click(object sender, EventArgs e)
        {
            conn.Open();
            cmd = new SqlCommand("sp_delEmp",conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@empid", SqlDbType.Int).Value = Convert.ToInt32(TextBox1.Text);
            int x = cmd.ExecuteNonQuery();
            conn.Close();
            ShowGrid();


        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            conn.Open();
            cmd = new SqlCommand("sp_searchEmp", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add
                ("@empid", SqlDbType.Int).Value = Convert.ToInt32(TextBox1.Text);
            dr = cmd.ExecuteReader();
            if(dr.Read())
            {
                TextBox2.Text = dr[0].ToString();
                TextBox3.Text = dr["empsal"].ToString();
            }
            else
            {
                Label1.Text = "enter valid employee id";
                //TextBox2.Text = "emp does not exists";
                TextBox3.Visible = false;
            }
            dr.Close();
            conn.Close();
        }

        protected void btnShow_Click(object sender, EventArgs e)
        {

        }

        protected void btnInserP_Click(object sender, EventArgs e)
        {
            conn.Open();
            cmd = new SqlCommand("insert into EmployeeTb1(empName,empSal) values(@empname,@empsal)", conn);
            cmd.Parameters.AddWithValue("@empname", TextBox2.Text);
            cmd.Parameters.AddWithValue("@empsal", TextBox3.Text);
            int x = cmd.ExecuteNonQuery();
            conn.Close();
            ShowGrid();
        }

        protected void btnUpdateQ_Click(object sender, EventArgs e)
        {
            conn.Open();
            cmd = new SqlCommand("update EmployeeTb1 set empName='" + TextBox2.Text + "',empSal=" + TextBox3.Text + " where empId=" + TextBox1.Text+"", conn);
            int x = cmd.ExecuteNonQuery();
            conn.Close();
            ShowGrid();
        }

        protected void btnDeleteP_Click(object sender, EventArgs e)
        {
            conn.Open();
            cmd = new SqlCommand("delete from EmployeeTb1 where empId=@empid", conn);
            SqlParameter para = new SqlParameter("@empid", TextBox1.Text);
            cmd.Parameters.Add(para);
            int x = cmd.ExecuteNonQuery();
            conn.Close();
            ShowGrid();
        }

        protected void btnDeleteQ_Click(object sender, EventArgs e)
        {
            conn.Open();
            cmd = new SqlCommand("delete from EmployeeTb1 where empId=" + TextBox1.Text + "", conn);

            int x = cmd.ExecuteNonQuery();
            conn.Close();
            ShowGrid();
        }

        protected void btnWithS_Click(object sender, EventArgs e)
        {
            conn.Open();
            cmd = new SqlCommand("sp_insertEmp", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@empname", SqlDbType.VarChar, 20).Value = TextBox2.Text;
            cmd.Parameters.Add("@empsal", SqlDbType.Float).Value = Convert.ToSingle(TextBox3.Text);
            int x = cmd.ExecuteNonQuery();
            conn.Close();
            ShowGrid();
        }

        protected void btnUpdateS_Click(object sender, EventArgs e)
        {
            conn.Open();
            cmd = new SqlCommand("sp_updateEmp", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@empid", SqlDbType.Int).Value = Convert.ToInt32(TextBox1.Text);
            cmd.Parameters.Add("@empname", SqlDbType.VarChar, 20).Value = TextBox2.Text;
            cmd.Parameters.Add("@empsal", SqlDbType.Float).Value = Convert.ToSingle(TextBox3.Text);
            int x = cmd.ExecuteNonQuery();
            conn.Close();
            ShowGrid();
        }
    }
}