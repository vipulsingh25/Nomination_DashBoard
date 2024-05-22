<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>Nomination Login</title>
    <link rel="stylesheet" href="./CSS/Login.css" />
</head>
<body>
    <div class="navbar">
        <div class="nav-logo">
            <img style="height: 60px;" src="./images/BITS_Pilanilogo.gif" />
        </div>
    </div>
    <div class="img_tagline1">
        <img src="./images/TAGLine.jpg"/>
    </div>
    <form id="form1" runat="server">

        <div class="login_section">
            <h3>Login to Nomination Dashboard</h3>
            <asp:Button CssClass="login-with-google-btn" ID="btnGoogleLogin" Text="login with BITS Email" runat="server" OnClick="Unnamed_Click" />
        </div>
        <div class="footer">
            <div class="img_tagline2">
                <img src="./images/TAGLine.jpg" />
            </div>

            <div class="footer_content">
                <div class="footer-tagline">
                    <span>©<span>2024 </span>Nomination Dashboard, BITS Pilani-Pilani Campus</span><br>
                    <span><small>Designed &amp; Developed by Centre for Software Development,SDET Unit, BITS-Pilani, India.</small></span>
                </div>
                <div class="footer-logo">
                    <img style="" src="./images/footer-logo.gif" />
                </div>
            </div>
        </div>
    </form>
</body>
</html>
