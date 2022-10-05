using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace student_registration_form
{
    public partial class home : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection();
        protected void Page_Load(object sender, EventArgs e)
        {
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["stdkey"].ConnectionString;
            if(conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
        }            


        protected void Save_btn_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "insert student_table values(@stdid,@stdname,@stdclass,@stdsub,@stdemail)";
            cmd.Connection = conn;
            cmd.Parameters.AddWithValue("stdid", Textroll.Text);
            cmd.Parameters.AddWithValue("stdname", Textname.Text);
            cmd.Parameters.AddWithValue("stdclass", Textclass.Text);
            cmd.Parameters.AddWithValue("stdsub", Textsub.Text);
            cmd.Parameters.AddWithValue("stdemail", Textemail.Text);
            cmd.ExecuteNonQuery();
            clr_refactor();
            dis_refactor();
        }

        private void clr_refactor()
        {
            Textroll.Text = string.Empty;
            Textname.Text = string.Empty;
            Textclass.Text = string.Empty;
            Textsub.Text = string.Empty;
            Textemail.Text = string.Empty;
            Textroll.Focus();
        }

        protected void Display_btn_Click(object sender, EventArgs e)
        {
            dis_refactor();
        }

        private void dis_refactor()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select * from student_table";
            cmd.Connection = conn;
            SqlDataReader stdReader;
            stdReader = cmd.ExecuteReader();
            ListBox1.DataTextField = "stdName";
            ListBox1.DataValueField = "stdRoll";
            ListBox1.DataSource = stdReader;
            ListBox1.DataBind();
            stdReader.Close();
            cmd.Dispose();

        }

        protected void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select * from student_table where stdRoll=@stdid";
            cmd.Connection = conn;
            cmd.Parameters.AddWithValue("@stdid", ListBox1.SelectedValue);
            SqlDataReader stdReader = cmd.ExecuteReader();
            if (stdReader.HasRows)
            {
                stdReader.Read();
                Textroll.Text = stdReader["stdRoll"].ToString();
                Textname.Text = stdReader["stdName"].ToString();
                Textclass.Text = stdReader["stdClass"].ToString();
                Textsub.Text = stdReader["stdSub"].ToString();
                Textemail.Text = stdReader["stdEmail"].ToString();
            }
            stdReader.Close();
            cmd.Dispose();
        }

        protected void Update_btn_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "update student_table set stdName=@stdname, stdClass=@stdclass, stdSub=@stdsub, stdEmail=@stdemail where stdRoll=@stdid";
            cmd.Connection = conn;
            cmd.Parameters.AddWithValue("@stdid", Textroll.Text);
            cmd.Parameters.AddWithValue("@stdname", Textname.Text);
            cmd.Parameters.AddWithValue("@stdclass", Textclass.Text);
            cmd.Parameters.AddWithValue("@stdsub", Textroll.Text);
            cmd.Parameters.AddWithValue("@stdemail", Textemail.Text);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            clr_refactor();
            dis_refactor();

        }

        protected void Delete_btn_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "delete from student_table where stdRoll=@stdid";
            cmd.Connection = conn;
            cmd.Parameters.AddWithValue("@stdid", Textroll.Text);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            dis_refactor();
            clr_refactor();
        }
    }
}