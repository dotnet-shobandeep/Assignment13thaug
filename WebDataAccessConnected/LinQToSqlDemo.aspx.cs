using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebDataAccessConnected
{
    public partial class LinQToSqlDemo : System.Web.UI.Page
    {
        EmployeeDataClassDataContext db = new
            EmployeeDataClassDataContext();
        public void ShowGrid()
        {
            var methodEmps = db.EmployeeTb1s.OrderByDescending(em => em.empName);
            GridView1.DataSource = methodEmps;
            GridView1.DataBind();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            //var emps = from emp in db.EmployeeTb1s
            //           where emp.empName.StartsWith("S")
            //           orderby emp.empName
            //           select emp;
            //var methodEmps = db.EmployeeTb1s.OrderByDescending(em => em.empName);

            ////GridView1.DataSource =emps;
            //GridView1.DataSource = methodEmps;
            //GridView1.DataBind();
            if (!IsPostBack)
            {
                ShowGrid();
            }
            
        }

        protected void btnInsert_Click(object sender, EventArgs e)
        {
            EmployeeTb1 emp = new EmployeeTb1();
            emp.empName = TextBox2.Text;
            emp.empSal = Convert.ToSingle(TextBox3.Text);
            db.GetTable<EmployeeTb1>().InsertOnSubmit(emp);
            db.SubmitChanges();
            ShowGrid();
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            EmployeeTb1 emptb1 = db.EmployeeTb1s.Single(em => em.empId == Convert.ToInt32(TextBox1.Text));
            emptb1.empName = TextBox2.Text;
            emptb1.empSal = Convert.ToSingle( TextBox3.Text);
            db.SubmitChanges();
            ShowGrid();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            EmployeeTb1 emptb1 = db.EmployeeTb1s.Single(em => em.empId == Convert.ToInt32(TextBox1.Text));
            db.EmployeeTb1s.DeleteOnSubmit(emptb1);
            db.SubmitChanges();
            ShowGrid();
        }

        protected void btnSp_Click(object sender, EventArgs e)
        {
            db.sp_searchEmp(Convert.ToInt32(GridView1.SelectedRow.Cells[1].Text));
           // TextBox2.Text=
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            db.sp_searchEmp(Convert.ToInt32(GridView1.SelectedRow.Cells[1].Text));
            TextBox2.Text = GridView1.SelectedRow.Cells[1].Text;
            TextBox3.Text = GridView1.SelectedRow.Cells[3].Text;

        }
    }
}