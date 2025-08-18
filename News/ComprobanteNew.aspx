<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ComprobanteNew.aspx.cs" Inherits="NominaRRHH.Presentacion.ComprobanteNew" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mar-top">
        <div class="panel panel-info">
            <div class="panel-body" style="margin-top: 0px">
                <div class="alert alert-dismissible alert-warning" id="alertValida" runat="server" visible="false">
                    <button type="button" class="close" data-dismiss="alert">×</button>
                    <asp:Label ID="lblAlert" runat="server" Visible="false"></asp:Label>
                </div>
                <div class="alert alert-dismissible alert-success" id="alertSucces" runat="server" visible="false">
                    <button type="button" class="close" data-dismiss="alert">×</button>
                    <asp:Label ID="LblSuccess" runat="server" Visible="false"></asp:Label>
                </div>
                <div class="row">
                    <div class="col-xs-12">
                        <div class="col-xs-2">
                            <label class="control-label" for="focusedInput">Periodo 1</label>
                            <asp:TextBox ID="TxtBuscar" class="form-control" placeholder="Digite un Periodo"
                                runat="server" autocomplete="off"></asp:TextBox>
                        </div>
                            <div class="col-md-2" runat="server" visible="false" id="divp">
                           <asp:Label  class="control-label"  for="focusedInput" ID="Label2" runat="server" Text="Periodo 2" style="font-weight: 700"></asp:Label>
                            <asp:TextBox ID="txtPeriodo2" class="form-control" placeholder="Digite un Periodo" onkeypress="return soloNumeros(event)"
                                runat="server" autocomplete="off"></asp:TextBox>
                            </div>
                           <div class="col-xs-2">
                            <%--<div class="col-md-1 marginChkActivo">--%>
                            <div class="checkbox">
                                <label>
                                    <asp:CheckBox ID="ChkConsolida" runat="server" OnCheckedChanged="ChkConsolida_CheckedChanged" Checked="false" AutoPostBack="true" />
                                    Consolidar Periodos
                                </label>
                            </div>
                            <%--</div>--%>
                        </div>
                        <div class="col-xs-1">
                            <%--<div class="col-md-1 marginChkActivo">--%>
                            <div class="checkbox">
                                <label>
                                    <asp:CheckBox ID="ChkAll" runat="server" OnCheckedChanged="ChkAll_CheckedChanged" Checked="true" AutoPostBack="true" />
                                    Tarjeta
                                </label>
                            </div>
                            <%--</div>--%>
                        </div>
                        <div class="col-xs-2">
                            <%--<div class="col-md-1 marginChkActivo">--%>
                            <div class="checkbox">
                                <label>
                                    <asp:CheckBox ID="ChkEfectivo" runat="server" Checked="false" OnCheckedChanged="ChkEfectivo_CheckedChanged" AutoPostBack="true" />
                                    Efectivo
                                </label>
                            </div>
                            <%--</div>  --%>
                        </div>
                         <div class="col-xs-2">
                            <%--<div class="col-md-1 marginChkActivo">--%>
                            <div class="checkbox">
                                <label>
                                    <asp:CheckBox ID="ChkEmail" runat="server" Checked="true"/>
                                    Excluir Correos
                                </label>
                            </div>
                            <%--</div>  --%>
                        </div>
                       
                        </div>
                    </div>
                <div class="row">
                    <div class="col-xs-12">
                         <div class="col-xs-3">
                            <label class="control-label" for="focusedInput">Por Proceso</label>
                            <asp:DropDownList class="form-control" ID="ddlProceso" runat="server">
                            </asp:DropDownList>
                        </div>
                        <div class="col-xs-2">
                            <label class="control-label" for="TxtCodigoE">Por Codigo Empleado</label>
                            <asp:TextBox ID="TxtCodigoE" class="form-control" placeholder="Digite el Codigo"
                                runat="server" autocomplete="off"></asp:TextBox>
                        </div>

                        <br />
                        
                    </div>
                    </div>
                <div class="row">
                    <div class="col-xs-12" style="margin-top: 26px;">
                        <asp:Button ID="btnAceptar" Class="btn btn-success" runat="server" Text="Imprimir" OnClick="btnAceptar_Click" />
                        <asp:Button ID="Button1" Class="btn btn-success" runat="server" Text="Enviar Correos" OnClick="Button1_Click" style="margin-left: 26px;"/>
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
                letras = "0123456789./";
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
