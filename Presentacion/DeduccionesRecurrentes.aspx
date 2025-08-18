<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DeduccionesRecurrentes.aspx.cs" Inherits="NominaRRHH.Presentacion.DeduccionesRecurrentes" %>

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
                            <label class="control-label" for="focusedInput">Codigo</label>
                            <asp:TextBox ID="txtCodigo" class="form-control"
                                runat="server" autocomplete="off"></asp:TextBox>
                        </div>
                        <div class="col-md-3">
                            <label class="control-label" for="focusedInput">Tipo Deducciones</label>
                            <asp:DropDownList class="form-control" ID="ddlTipDeduc" runat="server">
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-2">
                            <div class="col-md-1 marginChkActivo">
                                <div class="checkbox">
                                    <label class="control-label" for="focusedInput">
                                        <asp:CheckBox ID="ChkEspecial" runat="server" />
                                        <strong>Prestamo Especial</strong>
                                    </label>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-1">
                            <label class="control-label" for="focusedInput">Total</label>
                            <asp:TextBox ID="txtTotal" class="form-control" runat="server" onkeypress="return soloNumeros(event)" autocomplete="off"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            <div class="col-md-1 marginChkActivo">
                                <div class="checkbox">
                                    <label class="control-label" for="focusedInput">
                                        <asp:CheckBox ID="chkPorc" runat="server" />
                                        <strong>Porcentual</strong>
                                    </label>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-1">
                            <asp:Button ID="btnGuardar" Class="btn btn-success" Style="margin-top: 22px;" runat="server" Text="Asignar" OnClick="btnGuardar_Click" />
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

