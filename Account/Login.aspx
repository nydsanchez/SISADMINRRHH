<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="NominaRRHH.Account.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>
    <%--<link href="../Content/bootstrap1.css" rel="stylesheet" />--%>
    <link href="../Content/bootstrap.css" rel="stylesheet" />
    <link href="../Content/Styles.css" rel="stylesheet" />
    <script src="../Scripts/jquery-1.8.2.js"></script>
</head>
<body runat="server" class="frmLogin backgroundLogin">
    <div class="container" style="margin-top: 90px">
        <div class="row">
            <div class="col-sm-6 col-md-4 col-md-offset-4">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        Sistema de Nomina y RRHH
                    </div>
                    <div class="panel-body">
                        <div class="alert alert-danger" id="alertLog" runat="server" visible="false">
                            <asp:Label ID="LblMsg" runat="server"></asp:Label>
                        </div>
                        <form id="frm" runat="server">
                            <fieldset>
                                <div class="row">
                                    <div class="col-sm-12 col-md-10  col-md-offset-1 ">
                                        <div class="form-group">
                                            <div class="input-group">
                                                <span class="input-group-addon borderCalendar"><i class="glyphicon glyphicon-user"></i></span>
                                                <asp:TextBox class="form-control" placeholder="Usuario" ID="TxtUsuario" runat="server"
                                                    AutoCompleteType="Disabled" autofocus="true"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="input-group">
                                                <span class="input-group-addon borderCalendar"><i class="glyphicon glyphicon-lock"></i></span>
                                                <asp:TextBox class="form-control" ID="TxtPass" placeholder="Clave" runat="server"
                                                    TextMode="Password"></asp:TextBox>
                                            </div>
                                        </div>
                                         <div class="form-group">
                                             <label class="control-label" style="margin-left: 98px;" for="focusedInput">Empresa</label>
                                            <asp:DropDownList class="form-control" ID="ddlEmpresa" runat="server">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-group">
                                            <asp:Button ID="btnIngresar" runat="server" class="btn btn-lg btn-primary btn-block"
                                                Text="Ingresar" OnClick="btnIngresar_Click" />
                                        </div>
                                    </div>
                                </div>
                            </fieldset>
                        </form>
                    </div>
                    <div class="panel-footer ">
                    </div>
                </div>
            </div>
        </div>
    </div>
</body>
</html>
