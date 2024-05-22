<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMasterPage.master" AutoEventWireup="true" CodeFile="AdminDashboard.aspx.cs" Inherits="AdminDashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="main-content">
        <div class="Exportfile">
            <h3>Export Excel file of all the Nominated Employees</h3>
                        <div class="col-md-3">
                            <asp:Label CssClass="label" runat="server">Select Year</asp:Label>
                            <asp:DropDownList ID="dd_Year" runat="server" class="form-control">
                                <asp:ListItem ID="select_year" runat="server" Text="Select year" Value="" Disabled="true" Selected="True"></asp:ListItem>
                            </asp:DropDownList>

                        </div>
            <asp:Button runat="server" ID="btn_Export" OnClick="btn_Export_Click" Text="Export Year File" CssClass="Export_btn"/>
            <p>OR</p>
                        <asp:Button runat="server" ID="btn_FullExport" OnClick="btn_FullExport_Click" Text="Export Full File" CssClass="Export_btn"/>

        </div>
        <div class="Uploadfile">
            <h3>Set Timeline for Nomination</h3>
            <div class="Calendar_view">
                <div class="col-md-3">
                    <asp:TextBox class="form-control" ID="txtdtp" runat="server"></asp:TextBox>
                    <asp:Button CssClass="Export_btn" runat="server" ID="ChooseDate" OnClick="Choose_date" Text="Choose Start Date" Enabled="false"></asp:Button>
                    <asp:Calendar runat="server" ID="datepicker" OnSelectionChanged="datepicker_SelectionChanged"></asp:Calendar>
                </div>
                <div class="col-md-3">
                    <asp:TextBox ID="txtdtp1" class="form-control" runat="server"></asp:TextBox>
                    <asp:Button CssClass="Export_btn" runat="server" ID="ChooseDate1" OnClick="Choose_date1" Text="Choose Start Date" Enabled="false"></asp:Button>
                    <asp:Calendar runat="server" ID="datepicker1" OnSelectionChanged="datepicker_SelectionChanged1"></asp:Calendar>
                </div>
            </div>


            <!--<p>&#42&#42NOTE: First Delete Old data, then Upload new file to avoid Data Redundancy&#42&#42</p>
                        <asp:Button runat="server" ID="btn_Delete"  Text="Delete Old Data" CssClass="Delete_btn"/>
            <asp:FileUpload ID="FileUpload1" runat="server" />
                        <asp:Button runat="server" ID="btn_Upload"  Text="Upload new File" CssClass="Upload_btn"/>
            <p>Upload in this Excel format Only(Should Contain Column name as same in the given format)</p>
                                <img style="width:100%; margin-top:10px;" src="./images/FileFormat.png" />-->

        </div>
    </div>
</asp:Content>

