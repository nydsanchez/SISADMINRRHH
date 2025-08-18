<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="solicitudCambioSalario.aspx.cs" Inherits="NominaRRHH.Presentacion.solicitudCambioSalario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
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
                    <div class="col-md-12">
                        <div class="col-md-2">
                            <label class="control-label" for="focusedInput">Codigo Empleado</label>
                            <asp:TextBox ID="txtCodigo" class="form-control" autofocus="true"
                                runat="server" autocomplete="off" OnTextChanged="txtCodigo_TextChanged" AutoPostBack="True" onkeypress="return soloNumeros(event)"></asp:TextBox>
                        </div>
                        <div class="col-md-5">
                            <label class="control-label" for="focusedInput">Nombre Empleado</label>
                            <asp:TextBox ID="txtNombreEmpleado" class="form-control"
                                runat="server" autocomplete="off" ReadOnly="true"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                                        <label class="control-label" for="focusedInput">Ubicacion</label>
                                        <asp:DropDownList class="form-control" ID="ddlUbicacion" runat="server">
                                        </asp:DropDownList>
                                    </div>
                        <div class="col-md-3">
                            <label class="control-label" for="focusedInput">Departamento</label>
                            <asp:DropDownList class="form-control" ID="ddlProceso" runat="server">
                            </asp:DropDownList>
                        </div>
                        </div>
                    <div class="col-md-12">
                        <div class="col-md-2">
                            <label class="control-label" for="focusedInput">Salario Actual</label>
                            <asp:TextBox ID="txtSalarioActual" autofocus="true" class="form-control" 
                                runat="server" onkeypress="return soloNumeros(event)" autocomplete="off" ReadOnly="true"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            <label class="control-label" for="focusedInput">Nuevo Salario</label>
                            <asp:TextBox ID="TxtSalarioNew" autofocus="true" class="form-control" placeholder="Digite Salario"
                                runat="server" onkeypress="return soloNumeros(event)" autocomplete="off"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-12" style="margin-top: 20px;">
                        <div class="col-md-10">
                            <label class="control-label" for="focusedInput">Observacion</label>
                            <asp:TextBox ID="TxtObservacion" autofocus="true" class="form-control"
                                runat="server" autocomplete="off"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            <asp:Button ID="btnEnviarS" Class="btn btn-success" Style="margin-top: 22px;" runat="server" Text="Guardar" OnClick="btnEnviarS_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        function soloNumeros(e) {
            key = e.keyCode || e.which;
            tecla = String.fromCharCode(key).toLowerCase();
            letras = "0123456789.";
            especiales = "8-37-39-46";

            tecla_especial = false
            for (var i in especiales) {
                if (key == especiales[i]) {
                    tecla_especial = true;
                    break;
                }
            }

            if (letras.indexOf(tecla) == -1 && !tecla_especial) {
                return false;
            }
        }
    </script>
</asp:Content>
