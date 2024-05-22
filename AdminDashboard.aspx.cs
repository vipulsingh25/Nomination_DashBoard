using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OfficeOpenXml;
using LicenseContext = OfficeOpenXml.LicenseContext;
using System.Data.OleDb;
using Irony;
using DocumentFormat.OpenXml.Office.Word;
using DocumentFormat.OpenXml.Math;
using System.Text;


public partial class AdminDashboard : System.Web.UI.Page
{
    string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;

    private DateTime? selectedDate, selectedDate1;

    protected void Page_Load(object sender, EventArgs e)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            string CheckData = "SELECT * FROM Employee_Data;";
            using (SqlCommand cmds = new SqlCommand(CheckData, connection))
            {
                using (SqlDataAdapter ad = new SqlDataAdapter(cmds))
                {
                    DataTable dt = new DataTable();
                    ad.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        btn_Upload.Enabled = false;
                        btn_Upload.CssClass = "btn_Upload_disable";
                        btn_Delete.Enabled = true;
                        btn_Delete.CssClass = "Delete_btn";
                    }
                    else
                    {
                        btn_Upload.Enabled = true;
                        btn_Upload.CssClass = "Upload_btn";
                        btn_Delete.Enabled = false;
                        btn_Delete.CssClass = "btn_Upload_disable";
                    }
                }
            }
        }

        if (IsPostBack)
        {
            // Restore selected date from ViewState if available
            selectedDate = (DateTime?)ViewState["SelectedDate"];
            if (selectedDate.HasValue)
            {
                // Highlight the previously selected date (optional)
                datepicker.SelectedDates.Add(selectedDate.Value.Date);
            }

            selectedDate1 = (DateTime?)ViewState["SelectedDate"];
            if (selectedDate1.HasValue)
            {
                // Highlight the previously selected date (optional)
                datepicker1.SelectedDates.Add(selectedDate1.Value.Date);
            }
        }

        if (!IsPostBack)
        {
            getYear();
        }
    }


    protected void btn_Export_Click(object sender, EventArgs e)
    {
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Define the SQL query to retrieve data from the database
                string getdata = "SELECT * FROM Recommended_List inner join Employee_Rating on Recommended_List.Employee_ID = Employee_Rating.Employee_ID WHERE Recommended_List.Year = @Year";

                using (SqlCommand cmd = new SqlCommand(getdata, connection))
                {
                    // Create a DataTable to hold the query results
                    cmd.Parameters.AddWithValue("@Year", dd_Year.SelectedItem.Value.ToString());

                    DataTable dt1 = new DataTable();

                    // Execute the SQL query and fill the DataTable
                    using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                    {
                        ad.Fill(dt1);
                    }

                    // Check if there is data to export
                    if (dt1.Rows.Count > 0)
                    {
                        using (var package = new ExcelPackage())
                        {
                            // Create a worksheet named "Sheet1"
                            var worksheet = package.Workbook.Worksheets.Add("Sheet1");

                            // Load data from the DataTable into the worksheet
                            worksheet.Cells["A1"].LoadFromDataTable(dt1, true);

                            // Define the file path and save the Excel file
                            string filePath = Server.MapPath("~/Recommended_List.xlsx");
                            FileInfo fileInfo = new FileInfo(filePath);
                            package.SaveAs(fileInfo);

                            // Provide a success message or redirect to download the file
                            Response.Clear();
                            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                            Response.AddHeader("content-disposition", "attachment; filename=Recommended_List.xlsx");
                            Response.TransmitFile(filePath);
                            Response.Flush(); // Flush the response to ensure all content is sent
                        }
                    }
                    else
                    {
                        // Show a message if no data found to export
                        Response.Write("No data found to export.");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Handle the exception appropriately, e.g., log it or display an error message
            Response.Write("Error: " + ex.Message);
        }
    }

    /*protected void btn_Delete_Click(object sender, EventArgs e)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {

            connection.Open();
            string CheckData = "DELETE FROM Employee_Data;";
            using (SqlCommand cmds = new SqlCommand(CheckData, connection))
            {
                int iss = cmds.ExecuteNonQuery();
                if (iss > 0)
                {
                    string script = "<script>alert('Delete Successfully');</script>";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Alert", script);
                }
            }
        }

    }

    protected void btn_Upload_Click(object sender, EventArgs e)
    {
        if (FileUpload1.HasFile)
        {
            string fileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
            string filePath = Server.MapPath(fileName);
            FileUpload1.SaveAs(filePath);

            DataTable excelData = ReadExcel(filePath);

            if (excelData != null && excelData.Rows.Count > 0)
            {
                InsertIntoDatabase(excelData);
            }


        }

    }*/

    private DataTable ReadExcel(string filePath)
    {
        DataTable dt = new DataTable();

        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;


        using (ExcelPackage package = new ExcelPackage(new FileInfo(filePath)))
        {
            ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
            int rowCount = worksheet.Dimension.Rows;
            int colCount = worksheet.Dimension.Columns;

            for (int col = 1; col <= colCount; col++)
            {
                dt.Columns.Add(worksheet.Cells[1, col].Value.ToString());
            }

            for (int row = 2; row <= rowCount; row++)
            {
                DataRow dataRow = dt.NewRow();
                for (int col = 1; col <= colCount; col++)
                {
                    dataRow[col - 1] = worksheet.Cells[row, col].Value;
                }
                dt.Rows.Add(dataRow);
            }
        }

        return dt;
    }

    protected void Choose_date(object sender, EventArgs e)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            string savedata = "Update Admin_Setting set Start_Time = @Start_time where Sno = 1;";
            using (SqlCommand cmd = new SqlCommand(savedata, connection))
            {
                cmd.Parameters.AddWithValue("@Start_Time", selectedDate);
                int iss = cmd.ExecuteNonQuery();
                if (iss > 0)
                {
                    txtdtp.Text = datepicker.SelectedDate.ToLongDateString();
                }
            }
        }
    }

    protected void getYear()
    {
        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                StringBuilder sb = new StringBuilder();
                string getEmplist = "SELECT Year FROM Recommended_list;";
                using (SqlCommand cmds = new SqlCommand(getEmplist, connection))
                {
                    using (SqlDataAdapter ad = new SqlDataAdapter(cmds))
                    {
                        DataTable dt = new DataTable();
                        ad.Fill(dt);

                        foreach (DataRow rows in dt.Rows)
                        {
                            dd_Year.Items.Add(new ListItem(rows[0].ToString()));
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


    protected void datepicker_SelectionChanged(object sender, EventArgs e)
    {
        /*DateTime selectedDate = datepicker.SelectedDate;
        ChooseDate.Enabled = true; // Enable button after selection

        txtdtp.Text = datepicker.SelectedDate.ToLongDateString();
        datepicker.Visible = false;
        if (!IsPostBack)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string savedata = "INSERT INTO Admin_Setting(Start_Time) VALUES(@Start_time);";
                using (SqlCommand cmd = new SqlCommand(savedata, connection))
                {
                    cmd.Parameters.AddWithValue("@Start_Time", datepicker.SelectedDate.ToLongDateString());
                    int iss = cmd.ExecuteNonQuery();
                    if (iss > 0)
                    {
                    }
                }
            }
        }*/

        selectedDate = datepicker.SelectedDate;
        ChooseDate.Enabled = true; // Enable button after selection
        ViewState["SelectedDate"] = selectedDate; // Save to ViewState

    }

    protected void Choose_date1(object sender, EventArgs e)
    {

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            string savedata = "Update Admin_Setting set End_Time = @End_time where Sno = 1;";
            using (SqlCommand cmd = new SqlCommand(savedata, connection))
            {
                cmd.Parameters.AddWithValue("@End_Time", datepicker1.SelectedDate.ToLongDateString());
                int iss = cmd.ExecuteNonQuery();
                if (iss > 0)
                {
                    txtdtp1.Text = datepicker1.SelectedDate.ToLongDateString();

                }
            }
        }
    }

    protected void datepicker_SelectionChanged1(object sender, EventArgs e)
    {
        selectedDate1 = datepicker1.SelectedDate;
        ChooseDate1.Enabled = true; // Enable button after selection
        ViewState["SelectedDate"] = selectedDate1; // Save to ViewState
    }

    private void InsertIntoDatabase(DataTable dt)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connection))
            {
                bulkCopy.DestinationTableName = "Employee_Data";

                // Explicitly map columns and exclude identity column 'sno'
                bulkCopy.ColumnMappings.Add("Campus", "Campus");
                bulkCopy.ColumnMappings.Add("Employee ID", "Employee_ID");
                bulkCopy.ColumnMappings.Add("Employee Type", "Employee_Type");
                bulkCopy.ColumnMappings.Add("Full Name", "Full_Name");
                bulkCopy.ColumnMappings.Add("Designation", "Designation");
                bulkCopy.ColumnMappings.Add("Department", "Department");
                bulkCopy.ColumnMappings.Add("Controlling officer PSRN", "Controlling_officer_PSRN");
                bulkCopy.ColumnMappings.Add("Controlling Officer", "Controlling_Officer");
                bulkCopy.ColumnMappings.Add("Department of Controlling officer", "Department_of_Controlling_officer");
                bulkCopy.ColumnMappings.Add("Mail Id of Controlling officer", "MailId_of_Controlling_officer");
                bulkCopy.ColumnMappings.Add("Eligible for Award", "Eligible_for_Award");
                // Map other columns similarly

                // Set default values for 'limit' and 'nominated' columns

                bulkCopy.WriteToServer(dt);
            }

            string SetData = "update Employee_Data set Limit = 0, nominated = 'no';";
            using (SqlCommand cmd = new SqlCommand(SetData, connection))
            {
                int iss = cmd.ExecuteNonQuery();
            }
        }
    }

    protected void btn_FullExport_Click(object sender, EventArgs e)
    {
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Define the SQL query to retrieve data from the database
                string getdata = "SELECT * FROM Recommended_List inner join Employee_Rating on Recommended_List.Employee_ID = Employee_Rating.Employee_ID";

                using (SqlCommand cmd = new SqlCommand(getdata, connection))
                {
                    // Create a DataTable to hold the query results

                    DataTable dt1 = new DataTable();

                    // Execute the SQL query and fill the DataTable
                    using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                    {
                        ad.Fill(dt1);
                    }

                    // Check if there is data to export
                    if (dt1.Rows.Count > 0)
                    {
                        using (var package = new ExcelPackage())
                        {
                            // Create a worksheet named "Sheet1"
                            var worksheet = package.Workbook.Worksheets.Add("Sheet1");

                            // Load data from the DataTable into the worksheet
                            worksheet.Cells["A1"].LoadFromDataTable(dt1, true);

                            // Define the file path and save the Excel file
                            string filePath = Server.MapPath("~/Recommended_List.xlsx");
                            FileInfo fileInfo = new FileInfo(filePath);
                            package.SaveAs(fileInfo);

                            // Provide a success message or redirect to download the file
                            Response.Clear();
                            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                            Response.AddHeader("content-disposition", "attachment; filename=Recommended_List.xlsx");
                            Response.TransmitFile(filePath);
                            Response.Flush(); // Flush the response to ensure all content is sent
                        }
                    }
                    else
                    {
                        // Show a message if no data found to export
                        Response.Write("No data found to export.");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Handle the exception appropriately, e.g., log it or display an error message
            Response.Write("Error: " + ex.Message);
        }
    }
}