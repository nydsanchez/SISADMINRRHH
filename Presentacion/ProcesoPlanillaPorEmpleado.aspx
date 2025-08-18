<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProcesoPlanillaPorEmpleado.aspx.cs" Inherits="NominaRRHH.Presentacion.ProcesoPlanillaPorEmpleado" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../Content/Styles.css" rel="stylesheet" />
    <link href="../Content/fileinput.css" rel="stylesheet" />
    <script src="../Scripts/fileinput.js"></script>
    <link rel="stylesheet" href="http://css-spinners.com/css/spinner/whirly.css" type="text/css">
    <script src="../Scripts/jquery-ui-1.10.4.custom.min.js"></script>
    <link href="../Content/jquery-ui-1.10.4.custom.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mar-top">
        <div class="panel panel-info">
            <div class="panel-body">
                <div class="alert alert-dismissible alert-warning" id="alertValida" runat="server" visible="false">
                    <button type="button" class="close" data-dismiss="alert">×</button>
                    <asp:Label ID="lblAlert" runat="server" Visible="false"></asp:Label>
                </div>
                <div class="alert alert-dismissible alert-success" id="alertSucces" runat="server" visible="false">
                    <button type="button" class="close" data-dismiss="alert">×</button>
                    <asp:Label ID="LblSuccess" runat="server" Visible="false"></asp:Label>
                </div>
                <div class="row">
                    <div class="col-md-12" style="margin-left: 95px;">
                        <div class="col-md-1">
                            <label class="control-label" for="focusedInput">Periodo</label>
                            <asp:TextBox ID="txtPeriodo" class="form-control" runat="server" autocomplete="off"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            <label class="control-label" for="focusedInput">Semana</label>
                            <asp:DropDownList class="form-control" ID="ddlSemana" runat="server">
                                <asp:ListItem Value="1"> 1</asp:ListItem>
                                <asp:ListItem Value="2"> 2</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-5">
                            <label class="control-label" for="focusedInput">Nombre Empleado</label>
                            <asp:TextBox ID="txtNombEmpl" class="form-control"
                                runat="server" autocomplete="off"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            <asp:Button ID="btnRecalc" Class="btn btn-success" Style="margin-top: 22px; margin-left: 10px;" runat="server" Text="Procesar" OnClick="btnRecalc_Click"/>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        $(function () {
            $('#<%= txtNombEmpl.ClientID %>').autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: "JefeInmediato.asmx/getNombreJefe",
                        data: "{ 'nombreJefe': '" + request.term + "' }",
                        type: "POST",
                        dataType: "json",
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            response(data.d);
                        },
                        error: function (result) {
                            alert('Ocurrio un error con su busqueda');
                        }
                    });
                },
                minLength: 0
            });
        });
    </script>
</asp:Content>

