<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="Home.aspx.cs" Inherits="Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <div class="body-content">
            <form class="body-form" runat="server">
                <div class="form-header">
                    <h2>Nomination Form</h2>
                </div>
                <div class="user-details">
                    <div>
                        <asp:Label CssClass="user-name" Text="NAME - " runat="server" />
                        <asp:Literal runat="server" ID="show_name"></asp:Literal>
                    </div>
                    <div id="lb_PSRN">
                        <asp:Label CssClass="user-PSRN" Text="PSRN NO. - " runat="server" />
                        <asp:Literal runat="server" ID="show_PSRN"></asp:Literal>
                    </div>
                </div>
                <div class="form">
                    <div class="row">
                        <div class="col-md-3">
                            <asp:Label CssClass="label additional" runat="server">Employee Name</asp:Label>
                            <asp:DropDownList ID="dd_Emplist" runat="server" class="form-control" AutoPostBack="true" OnTextChanged="dd_Emplist_TextChanged">
                                <asp:ListItem ID="Edit_Name" runat="server" Text="Select from list" Value="" Disabled="true" Selected="True"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                ControlToValidate="dd_Emplist"
                                InitialValue=""
                                ErrorMessage="Required Field"
                                ForeColor="Red"
                                CssClass="validation-error"
                                Display="Dynamic">
                            </asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-3">
                            <asp:Label CssClass="label additional" runat="server">Employee Type</asp:Label>
                            <asp:TextBox ID="tb_Etype" runat="server" class="form-control" placeholder="Employee Type" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                ControlToValidate="tb_Etype"
                                InitialValue=""
                                ErrorMessage="Required Field"
                                ForeColor="Red"
                                CssClass="validation-error"
                                Display="Dynamic">
                            </asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-3">
                            <asp:Label CssClass="label additional" runat="server">Employee Designation</asp:Label>
                            <asp:TextBox ID="tb_ED" runat="server" class="form-control" placeholder="Employee Designation" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                ControlToValidate="tb_ED"
                                InitialValue=""
                                ErrorMessage="Required Field"
                                ForeColor="Red"
                                CssClass="validation-error"
                                Display="Dynamic">
                            </asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-9">
                            <asp:Label CssClass="label additional" runat="server">Justification (preferable in bullet points)</asp:Label>
                            <asp:TextBox ID="tb_Reason" TextMode="MultiLine" Height="80" runat="server" class="form-control" placeholder="Write here....!!" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                                ControlToValidate="tb_Reason"
                                InitialValue=""
                                ErrorMessage="Required Field"
                                ForeColor="Red"
                                CssClass="validation-error"
                                Display="Dynamic">
                            </asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-9">
                            <asp:Label CssClass="label additional" runat="server">Please mention an instance where the person concerned has truly impressed you</asp:Label>
                            <asp:TextBox ID="tb_Remark" TextMode="MultiLine" Height="80" runat="server" class="form-control" placeholder="Write here....!!" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server"
                                ControlToValidate="tb_Remark"
                                InitialValue=""
                                ErrorMessage="Required Field"
                                ForeColor="Red"
                                CssClass="validation-error"
                                Display="Dynamic">
                            </asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-9">
                            <asp:Label CssClass="label additional" runat="server">Extra qualifications acquired after joining BITS: Yes/ No. If Yes, please specify</asp:Label>
                            <asp:TextBox ID="tb_Qualification" TextMode="MultiLine" Height="80" runat="server" class="form-control" placeholder="Write here....!!" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server"
                                ControlToValidate="tb_Qualification"
                                InitialValue=""
                                ErrorMessage="Required Field"
                                ForeColor="Red"
                                CssClass="validation-error"
                                Display="Dynamic">
                            </asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="table_rating">
                        <table class="col-md-9">
                            <tr>
                                <th class="additional">Rating</th>
                            </tr>
                            <tr>
                                <td>Dedication</td>
                                <td>
                                    <asp:RadioButtonList ID="RadioButtonList3" runat="server" DataTextField="ans2"
                                        DataValueField="ans2" RepeatDirection="Horizontal" RepeatColumns="5" RepeatLayout="Table" CssClass="rbl">
                                        <asp:ListItem Text="" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="" Value="3"></asp:ListItem>
                                        <asp:ListItem Text="" Value="4"></asp:ListItem>
                                        <asp:ListItem Text="" Value="5"></asp:ListItem>
                                    </asp:RadioButtonList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"
                                ControlToValidate="RadioButtonList3"
                                InitialValue=""
                                ErrorMessage="Required Field"
                                ForeColor="Red"
                                CssClass="validation-error"
                                Display="Dynamic">
                            </asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>Regularity</td>
                                <td>
                                    <asp:RadioButtonList ID="RadioButtonList1" runat="server" DataTextField="ans2"
                                        DataValueField="ans2" RepeatDirection="Horizontal" RepeatColumns="5" RepeatLayout="Table" CssClass="rbl">
                                        <asp:ListItem Text="" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="" Value="3"></asp:ListItem>
                                        <asp:ListItem Text="" Value="4"></asp:ListItem>
                                        <asp:ListItem Text="" Value="5"></asp:ListItem>
                                    </asp:RadioButtonList>
                                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server"
                                ControlToValidate="RadioButtonList1"
                                InitialValue=""
                                ErrorMessage="Required Field"
                                ForeColor="Red"
                                CssClass="validation-error"
                                Display="Dynamic">
                            </asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>Team Work</td>
                                <td>
                                    <asp:RadioButtonList ID="RadioButtonList2" runat="server" DataTextField="ans2"
                                        DataValueField="ans2" RepeatDirection="Horizontal" RepeatColumns="5" RepeatLayout="Table" CssClass="rbl">
                                        <asp:ListItem Text="" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="" Value="3"></asp:ListItem>
                                        <asp:ListItem Text="" Value="4"></asp:ListItem>
                                        <asp:ListItem Text="" Value="5"></asp:ListItem>
                                    </asp:RadioButtonList>
                                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server"
                                ControlToValidate="RadioButtonList2"
                                InitialValue=""
                                ErrorMessage="Required Field"
                                ForeColor="Red"
                                CssClass="validation-error"
                                Display="Dynamic">
                            </asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>Meeting the Deadlines</td>
                                <td>
                                    <asp:RadioButtonList ID="RadioButtonList4" runat="server" DataTextField="ans2"
                                        DataValueField="ans2" RepeatDirection="Horizontal" RepeatColumns="5" RepeatLayout="Table" CssClass="rbl">
                                        <asp:ListItem Text="" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="" Value="3"></asp:ListItem>
                                        <asp:ListItem Text="" Value="4"></asp:ListItem>
                                        <asp:ListItem Text="" Value="5"></asp:ListItem>
                                    </asp:RadioButtonList>
                                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server"
                                ControlToValidate="RadioButtonList4"
                                InitialValue=""
                                ErrorMessage="Required Field"
                                ForeColor="Red"
                                CssClass="validation-error"
                                Display="Dynamic">
                            </asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>Helping Others</td>
                                <td>
                                    <asp:RadioButtonList ID="RadioButtonList5" runat="server" DataTextField="ans2"
                                        DataValueField="ans2" RepeatDirection="Horizontal" RepeatColumns="5" RepeatLayout="Table" CssClass="rbl">
                                        <asp:ListItem Text="" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="" Value="3"></asp:ListItem>
                                        <asp:ListItem Text="" Value="4"></asp:ListItem>
                                        <asp:ListItem Text="" Value="5"></asp:ListItem>
                                    </asp:RadioButtonList>
                                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server"
                                ControlToValidate="RadioButtonList5"
                                InitialValue=""
                                ErrorMessage="Required Field"
                                ForeColor="Red"
                                CssClass="validation-error"
                                Display="Dynamic">
                            </asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>Ready to learn new things</td>
                                <td>
                                    <asp:RadioButtonList ID="RadioButtonList6" runat="server" DataTextField="ans2"
                                        DataValueField="ans2" RepeatDirection="Horizontal" RepeatColumns="5" RepeatLayout="Table" CssClass="rbl">
                                        <asp:ListItem Text="" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="" Value="3"></asp:ListItem>
                                        <asp:ListItem Text="" Value="4"></asp:ListItem>
                                        <asp:ListItem Text="" Value="5"></asp:ListItem>
                                    </asp:RadioButtonList>
                                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server"
                                ControlToValidate="RadioButtonList6"
                                InitialValue=""
                                ErrorMessage="Required Field"
                                ForeColor="Red"
                                CssClass="validation-error"
                                Display="Dynamic">
                            </asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>Behavior towards Co-workers</td>
                                <td>
                                    <asp:RadioButtonList ID="RadioButtonList7" runat="server" DataTextField="ans2"
                                        DataValueField="ans2" RepeatDirection="Horizontal" RepeatColumns="5" RepeatLayout="Table" CssClass="rbl">
                                        <asp:ListItem Text="" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="" Value="3"></asp:ListItem>
                                        <asp:ListItem Text="" Value="4"></asp:ListItem>
                                        <asp:ListItem Text="" Value="6"></asp:ListItem>
                                    </asp:RadioButtonList>
                                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server"
                                ControlToValidate="RadioButtonList7"
                                InitialValue=""
                                ErrorMessage="Required Field"
                                ForeColor="Red"
                                CssClass="validation-error"
                                Display="Dynamic">
                            </asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>Overall Ranking</td>
                                <td>
                                    <asp:RadioButtonList ID="RadioButtonList8" runat="server" DataTextField="ans2"
                                        DataValueField="ans2" RepeatDirection="Horizontal" RepeatColumns="5" RepeatLayout="Table" CssClass="rbl">
                                        <asp:ListItem Text="" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="" Value="3"></asp:ListItem>
                                        <asp:ListItem Text="" Value="4"></asp:ListItem>
                                        <asp:ListItem Text="" Value="5"></asp:ListItem>
                                    </asp:RadioButtonList>
                                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server"
                                ControlToValidate="RadioButtonList8"
                                InitialValue=""
                                ErrorMessage="Required Field"
                                ForeColor="Red"
                                CssClass="validation-error"
                                Display="Dynamic">
                            </asp:RequiredFieldValidator>
                                </td>
                            </tr>
                        </table>
                    </div>

                    <div class="btn">
                        <div>
                            <asp:Button ID="btn_nominate" CssClass="btn-submit_Enable" runat="server" Text="Nominate" class="btn-submit" OnClick="btn_nominate_Click" />
                            <asp:Button ID="btn_delete" CssClass="btn-submit" runat="server" Text="Delete" class="btn-submit" OnClick="btn_delete_Click" />
                            <asp:Button ID="btn_save" CssClass="btn-submit" runat="server" Text="Save" class="btn-submit" OnClick="btn_save_Click" />
                            <asp:Button ID="btn_cancel" CssClass="btn-submit" runat="server" Text="Cancel" class="btn-submit" OnClick="btn_cancel_Click" />
                        </div>
                        <asp:Literal runat="server" ID="show_numberEmployee"></asp:Literal>
                    </div>
                </div>

            </form>
            <div class="body-list">
                <div class="form-header">
                    <h2>Nominated Employees</h2>
                </div>
                <div class="list_items">
                    <asp:Literal runat="server" ID="show"></asp:Literal>

                </div>

            </div>
        </div>
    </div>
</asp:Content>

