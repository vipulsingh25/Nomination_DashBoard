using DocumentFormat.OpenXml.EMMA;
using DocumentFormat.OpenXml.Office.Word;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Home : System.Web.UI.Page
{

    string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
    int limit = 0;

    protected void Page_Load(object sender, EventArgs e)
    {


        if (!IsPostBack)
        {
            if (Session["Email"] != null)
            {
                getcarddata(Session["Email"].ToString());

                string currentUrl = Request.Url.AbsoluteUri;
                if (currentUrl.Contains("?a="))
                {
                    string Fid = Request.QueryString["a"].ToString();
                    EditEmp(Fid);
                }
                else
                {
                    getEmp(Session["Email"].ToString());
                }
            }
        }

        if (Session["user"] == null)
        {
            string script = "<script>alert('Session Expired Please Login Again');</script>";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Alert", script);
            Response.Redirect("./Login.aspx");
        }

    }

    protected void getcarddata(string Email)
    {
        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string getdata = "SELECT Employee_ID,Employee_Type,Full_Name,Department,Designation FROM Recommended_list WHERE Mailid_of_Controlling_officer = @Email;";
                using (SqlCommand cmds = new SqlCommand(getdata, connection))
                {
                    cmds.Parameters.AddWithValue("@Email", Email);
                    using (SqlDataAdapter ad = new SqlDataAdapter(cmds))
                    {
                        DataTable dt = new DataTable();
                        ad.Fill(dt);

                        StringBuilder sb = new StringBuilder();
                        foreach (DataRow rows in dt.Rows)
                        {
                            sb.Append("<div class='xyz'>");
                            sb.Append("<div class='emp_data'><p>Name : " + rows[2] + "</p></div>");
                            sb.Append("<div class='emp_data'><asp:Label  runat='server'/><p>Type : " + rows[1] + "</p></div>");
                            sb.Append("<div class='emp_data'><asp:Label runat='server'/><p>Designation : " + rows[4] + "</p></div>");
                            sb.Append("<a class='btn_edit' href=\"Home.aspx?a=" + rows[0] + "\"><i class='fa fa-edit fa-lg'></i></a>");
                            sb.Append("</div>");
                        }
                        show.Text = sb.ToString();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void EditEmp(string ID)
    {
        try
        {
            btn_cancel.Visible = true;
            btn_delete.Visible = true;
            btn_save.Visible = true;
            Edit_Name.Value = ID;
            btn_nominate.Visible = false;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                StringBuilder sb = new StringBuilder();
                string getData = "SELECT * FROM Recommended_list inner join Employee_Rating on Recommended_list.Employee_ID = Employee_Rating.Employee_ID WHERE Recommended_list.Employee_ID = @ID";

                using (SqlCommand cmds = new SqlCommand(getData, connection))
                {
                    cmds.Parameters.AddWithValue("@ID", ID);
                    using (SqlDataAdapter ad = new SqlDataAdapter(cmds))
                    {
                        DataTable dt = new DataTable();
                        ad.Fill(dt);
                        foreach (DataRow rows in dt.Rows)
                        {
                            show_name.Text = rows[7].ToString();
                            show_PSRN.Text = rows[6].ToString();
                            Edit_Name.Text = rows[3].ToString();
                            tb_Etype.Text = rows[2].ToString();
                            tb_ED.Text = rows[4].ToString();
                            tb_Reason.Text = rows[10].ToString();
                            tb_Remark.Text = rows[11].ToString();
                            tb_Qualification.Text = rows[12].ToString();

                            RadioButtonList3.SelectedValue = rows[17].ToString();
                            RadioButtonList1.SelectedValue = rows[18].ToString();

                            RadioButtonList2.SelectedValue = rows[19].ToString();

                            RadioButtonList4.SelectedValue = rows[20].ToString();

                            RadioButtonList5.SelectedValue = rows[21].ToString();

                            RadioButtonList6.SelectedValue = rows[22].ToString();

                            RadioButtonList7.SelectedValue = rows[23].ToString();

                            RadioButtonList8.SelectedValue = rows[24].ToString();
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void getEmp(string email)
    {
        try
        {
            btn_cancel.Visible = false;
            btn_delete.Visible = false;
            btn_save.Visible = false;
            Edit_Name.Value = "";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                StringBuilder sb = new StringBuilder();
                string getEmplist = "SELECT * FROM Employee_Data WHERE Mailid_of_Controlling_officer = @Email and nominated = 'no';";
                using (SqlCommand cmds = new SqlCommand(getEmplist, connection))
                {
                    cmds.Parameters.AddWithValue("@Email", email);
                    using (SqlDataAdapter ad = new SqlDataAdapter(cmds))
                    {
                        DataTable dt = new DataTable();
                        ad.Fill(dt);

                        if (dt.Rows.Count <= 10)
                        {
                            sb.Append("<p>(You can nominate only 2 employees)</p>");
                            show_numberEmployee.Text = sb.ToString();
                            limit = 2;
                        }
                        else if (dt.Rows.Count > 10 && dt.Rows.Count <= 20)
                        {
                            sb.Append("<p>(You can nominate only 3 employees)</p>");
                            show_numberEmployee.Text = sb.ToString();
                            limit = 3;
                        }
                        else if (dt.Rows.Count > 20)
                        {
                            sb.Append("<p>(You can nominate only 5 employees)</p>");
                            show_numberEmployee.Text = sb.ToString();
                            limit = 5;
                        }
                        foreach (DataRow rows in dt.Rows)
                        {
                            show_name.Text = rows[8].ToString();
                            show_PSRN.Text = rows[7].ToString();
                            dd_Emplist.Items.Add(new ListItem(rows[4].ToString(), rows[2].ToString()));
                            if (limit == int.Parse(rows[12].ToString()))
                            {
                                btn_nominate.Text = "Not Allowed";
                                btn_nominate.Enabled = false;
                                btn_nominate.CssClass = "btn-submit_disable";
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void dd_Emplist_TextChanged(object sender, EventArgs e)
    {
        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string getEmplist = "SELECT Employee_Type, Designation FROM Employee_Data WHERE Employee_ID = @EmployeeID;";
                using (SqlCommand cmds = new SqlCommand(getEmplist, connection))
                {
                    cmds.Parameters.AddWithValue("@EmployeeID", dd_Emplist.SelectedItem.Value.ToString());
                    using (SqlDataAdapter ad = new SqlDataAdapter(cmds))
                    {
                        DataTable dt = new DataTable();
                        ad.Fill(dt);
                        foreach (DataRow rows in dt.Rows)
                        {
                            tb_Etype.Text = rows[0].ToString();
                            tb_ED.Text = rows[1].ToString();
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void btn_nominate_Click(object sender, EventArgs e)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            string Campus = "";
            string Department = "";
            string Controlling_officer_PSRN = "";
            string Department_of_Controlling_officer = "";
            string getdata = "SELECT Campus, Department, Controlling_officer_PSRN, Department_of_Controlling_officer FROM Employee_Data WHERE Employee_ID = @EmployeeID;";
            using (SqlCommand sqlCommand = new SqlCommand(getdata, connection))
            {
                sqlCommand.Parameters.AddWithValue("@EmployeeID", dd_Emplist.SelectedItem.Value);
                using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand))
                {
                    DataTable dt = new DataTable();
                    sqlDataAdapter.Fill(dt);
                    foreach (DataRow rows in dt.Rows)
                    {
                        Campus = rows[0].ToString();
                        Department = rows[1].ToString();
                        Controlling_officer_PSRN = rows[2].ToString();
                        Department_of_Controlling_officer = rows[3].ToString();
                    }
                }
            }

            string UPDATELIMIT = "update Employee_Data set Limit = Limit + " + 1 + "where Mailid_of_Controlling_officer = '" + Session["Email"].ToString() + "';";
            using (SqlCommand cmd = new SqlCommand(UPDATELIMIT, connection))
            {
                int iss = cmd.ExecuteNonQuery();
            }

            string NOMINATEDSTATUS = "update Employee_Data set nominated = 'yes' where Employee_ID = '" + dd_Emplist.SelectedItem.Value + "';";
            using (SqlCommand cmd = new SqlCommand(NOMINATEDSTATUS, connection))
            {
                int iss = cmd.ExecuteNonQuery();
            }

            string ip = GetUserIpAddress();

            string savedata = "INSERT INTO Recommended_List(Campus, Employee_ID, Employee_Type, Full_Name, Designation, Department,Controlling_Officer_PSRN , Controlling_Officer, Department_Of_Controlling_Officer,Mailid_of_Controlling_officer,Justification,Remark,Extra_Qualification,IP_Address) VALUES(@Campus, @EmployeeID, @EmployeeType, @EmployeeName, @Designation, @Department, @PSRN, @Username, @DepartmentOfControllingOfficer,@Mailid_of_Controlling_officer,@Justification,@Remark,@Extra_Qualification,@IP);" + "insert into Employee_Rating values(@EmployeeID,@Dedication,@Regularity,@Team_Work,@Meeting_the_Deadlines,@Helping_Others,@Ready_to_learn_new_things,@Behavior_towards_Coworkers,@Overall_Ranking);";
            using (SqlCommand cmd = new SqlCommand(savedata, connection))
            {
                cmd.Parameters.AddWithValue("@Campus", Campus);
                cmd.Parameters.AddWithValue("@EmployeeID", dd_Emplist.SelectedItem.Value);
                cmd.Parameters.AddWithValue("@EmployeeType", tb_Etype.Text);
                cmd.Parameters.AddWithValue("@EmployeeName", dd_Emplist.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@Designation", tb_ED.Text);
                cmd.Parameters.AddWithValue("@Department", Department);
                cmd.Parameters.AddWithValue("@PSRN", show_PSRN.Text);
                cmd.Parameters.AddWithValue("@Username", show_name.Text);
                cmd.Parameters.AddWithValue("@DepartmentOfControllingOfficer", Department_of_Controlling_officer);
                cmd.Parameters.AddWithValue("@Mailid_of_Controlling_officer", Session["Email"].ToString());
                cmd.Parameters.AddWithValue("@Justification", tb_Reason.Text);
                cmd.Parameters.AddWithValue("@Remark", tb_Remark.Text);
                cmd.Parameters.AddWithValue("@Extra_Qualification", tb_Qualification.Text);
                cmd.Parameters.AddWithValue("@IP", ip);


                cmd.Parameters.AddWithValue("@Dedication", RadioButtonList3.SelectedValue);
                cmd.Parameters.AddWithValue("@Regularity", RadioButtonList1.SelectedValue);

                cmd.Parameters.AddWithValue("@Team_Work", RadioButtonList2.SelectedValue);

                cmd.Parameters.AddWithValue("@Meeting_the_Deadlines", RadioButtonList4.SelectedValue);

                cmd.Parameters.AddWithValue("@Helping_Others", RadioButtonList5.SelectedValue);

                cmd.Parameters.AddWithValue("@Ready_to_learn_new_things", RadioButtonList6.SelectedValue);

                cmd.Parameters.AddWithValue("@Behavior_towards_Coworkers", RadioButtonList7.SelectedValue);

                cmd.Parameters.AddWithValue("@Overall_Ranking", RadioButtonList8.SelectedValue);




                int iss = cmd.ExecuteNonQuery();
                if (iss > 0)
                {
                    tb_Etype.Text = "";
                    tb_ED.Text = "";
                    tb_Reason.Text = "";
                    tb_Remark.Text = "";
                    tb_Qualification.Text = "";
                    dd_Emplist.SelectedIndex = -1;

                    Response.Redirect(Request.Url.AbsoluteUri);
                }
            }
        }
    }


    private string GetUserIpAddress()
    {
        // First, check if the application is behind a proxy or load balancer
        string ipAddress = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

        if (string.IsNullOrEmpty(ipAddress))
        {
            // If not, use the UserHostAddress property
            ipAddress = HttpContext.Current.Request.UserHostAddress;
        }

        return ipAddress;
    }
    protected void btn_delete_Click(object sender, EventArgs e)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();


            string NOMINATEDSTATUS = "update Employee_Data set nominated = 'no' where Employee_ID = '" + Request.QueryString["a"].ToString() + "';";
            using (SqlCommand cmd = new SqlCommand(NOMINATEDSTATUS, connection))
            {
                int iss = cmd.ExecuteNonQuery();
            }

            string UPDATELIMIT = "update Employee_Data set Limit = Limit - " + 1 + "where Mailid_of_Controlling_officer = '" + Session["Email"].ToString() + "';";
            using (SqlCommand cmd = new SqlCommand(UPDATELIMIT, connection))
            {
                int iss = cmd.ExecuteNonQuery();
            }

            string delete = "delete from Recommended_List where Employee_ID = @ID;" + "delete from Employee_Rating where Employee_ID = @ID;";
            using (SqlCommand sqlCommand = new SqlCommand(delete, connection))
            {
                sqlCommand.Parameters.AddWithValue("@ID", Request.QueryString["a"].ToString());
                int iss = sqlCommand.ExecuteNonQuery();
                if (iss > 0)
                {
                    Response.Redirect("./Home.aspx");
                }
            }
        }
    }

    protected void btn_save_Click(object sender, EventArgs e)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            string updatedata = "Update Recommended_List set Justification = @Justification, Remark = @Remark,Extra_Qualification = @Extra_Qualification,IP_Address = @IP where Employee_ID = @ID;" + "Update Employee_Rating Set Dedication = @Dedication , Regularity = @Regularity, Team_Work = @Team_Work, Meeting_the_Deadlines = @Meeting_the_Deadlines,Helping_Others = @Helping_Others,Ready_to_learn_new_things = @Ready_to_learn_new_things,Behavior_towards_Coworkers = @Behavior_towards_Coworkers,Overall_Ranking = @Overall_Ranking where Employee_ID = @ID";
            using (SqlCommand sqlCommand = new SqlCommand(updatedata, connection))
            {
                string ip = GetUserIpAddress();
                sqlCommand.Parameters.AddWithValue("@Justification", tb_Reason.Text);
                sqlCommand.Parameters.AddWithValue("@Remark", tb_Remark.Text);
                sqlCommand.Parameters.AddWithValue("@Extra_Qualification", tb_Qualification.Text);
                sqlCommand.Parameters.AddWithValue("@ID", Request.QueryString["a"].ToString());

                sqlCommand.Parameters.AddWithValue("@Dedication", RadioButtonList3.SelectedValue);
               sqlCommand.Parameters.AddWithValue("@Regularity", RadioButtonList1.SelectedValue);

               sqlCommand.Parameters.AddWithValue("@Team_Work", RadioButtonList2.SelectedValue);

               sqlCommand.Parameters.AddWithValue("@Meeting_the_Deadlines", RadioButtonList4.SelectedValue);

               sqlCommand.Parameters.AddWithValue("@Helping_Others", RadioButtonList5.SelectedValue);

               sqlCommand.Parameters.AddWithValue("@Ready_to_learn_new_things", RadioButtonList6.SelectedValue);

               sqlCommand.Parameters.AddWithValue("@Behavior_towards_Coworkers", RadioButtonList7.SelectedValue);

                sqlCommand.Parameters.AddWithValue("@Overall_Ranking", RadioButtonList8.SelectedValue);
                sqlCommand.Parameters.AddWithValue("@IP", ip);


                int iss = sqlCommand.ExecuteNonQuery();
                if (iss > 0)
                {
                    Response.Redirect("./Home.aspx");
                }
            }
        }
    }

    protected void btn_cancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("./Home.aspx");
    }
}