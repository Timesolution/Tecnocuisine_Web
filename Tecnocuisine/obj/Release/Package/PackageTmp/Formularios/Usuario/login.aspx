<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="Tecnocuisine.Formularios.Usuario.login" %>

<!DOCTYPE html>
<html>

<head>

    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">

    <title>INSPINIA | Login</title>

    <link href="../../css/bootstrap.min.css" rel="stylesheet">
    <link href="../../font-awesome/css/font-awesome.css" rel="stylesheet">

    <link href="../../css/animate.css" rel="stylesheet">
    <link href="../../css/style.css" rel="stylesheet">

</head>

<body class="gray-bg">

    <div class="middle-box text-center loginscreen  animated fadeInDown">
        <div>
            <div>

                <h1 class="logo-name">TC</h1>

            </div>
            <h3>Bienvenido a TecnoCuisine</h3>
                <!--Continually expanded and constantly improved Inspinia Admin Them (IN+)-->
            <p>Inicie Sesion.</p>
            <form class="m-t" role="form" runat="server" method="post" >
                <div class="form-group">
                    <asp:TextBox type="email" ID="txtEmail" CssClass="form-control"  runat="server" />
                </div>
                <div class="form-group">
                    <asp:TextBox type="password" ID="txtPassword" CssClass="form-control"  runat="server"/>
                </div>
                    <asp:LinkButton id="btnLogin" OnClick="btnLogin_Click" class="btn btn-primary block full-width m-b" Text="Login" runat="server" />
                <a href="#"><small>Olvidaste tu contraseña?</small></a>
                <p class="text-muted text-center"><small>No tenes una cuenta?</small></p>
                <a class="btn btn-sm btn-white btn-block" href="register.html">Crear una cuenta</a>
            </form>
        </div>
    </div>

    <!-- Mainly scripts -->
    <script src="../../js/jquery-2.1.1.js"></script>
    <script src="../../js/js/bootstrap.min.js"></script>

</body>

</html>
