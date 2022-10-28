<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="Tecnocuisine.Account.login" %>

<!DOCTYPE html>
<html>

<head>

    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">

    <title>Tecnocuisine </title>

    <link href="../../css/bootstrap.min.css" rel="stylesheet">
    <link href="../../font-awesome/css/font-awesome.css" rel="stylesheet">

    <link href="../../css/animate.css" rel="stylesheet">
    <link href="../../css/style.css" rel="stylesheet">

</head>

<body class="gray-bg">

    <div class="middle-box text-center loginscreen  animated fadeInDown">
        <div>
            <div>

                <h1 class="logo-name">TC+</h1>

            </div>
            <asp:Label runat="server" ID="lblTitulo" Text="Bienvenido a Tecnocuisine" meta:resourcekey="lblTitulo" Font-Size="Larger"> </asp:Label> 
                <!--Continually expanded and constantly improved Inspinia Admin Them (IN+)-->
            <p>Inicie Sesion.</p>
            <form class="m-t" role="form" runat="server" method="post" >
                <div class="form-group">
                    <asp:TextBox type="text" ID="txtUsuario" CssClass="form-control"  runat="server" />
                </div>
                <div class="form-group">
                    <asp:TextBox type="password" ID="txtpassword" CssClass="form-control"  runat="server"/>
                </div>
                <asp:LinkButton id="btnLogin"  OnClick="btnLogin_Click" class="btn btn-primary block full-width m-b" Text="Login" runat="server" />
            </form>
        </div>
    </div>

    <!-- Mainly scripts -->
    <script src="../../js/jquery-2.1.1.js"></script>
    <script src="../../js/js/bootstrap.min.js"></script>

</body>

</html>
