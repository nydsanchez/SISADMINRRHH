<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="IngresoxVacaciones.aspx.cs" Inherits="NominaRRHH.Presentacion.IngresoxVacaciones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="../Scripts/jquery-ui-1.10.4.custom.min.js"></script>
    <link href="../Content/jquery-ui-1.10.4.custom.min.css" rel="stylesheet" />
    <link href="../Content/fileinput.css" rel="stylesheet" />
    <script src="../Scripts/fileinput.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mar-top">
        <div class="panel panel-info">
            <div class="panel-body">
                <div class="row">
                    <div class="alert alert-dismissible alert-warning" id="alertValida" runat="server" visible="false">
                        <button type="button" class="close" data-dismiss="alert">×</button>
                        <asp:Label ID="lblAlert" runat="server" Visible="false"></asp:Label>
                    </div>
                    <div class="alert alert-dismissible alert-success" id="alertSucces" runat="server" visible="false">
                        <button type="button" class="close" data-dismiss="alert">×</button>
                        <asp:Label ID="LblSuccess" runat="server" Visible="false"></asp:Label>
                    </div>
                  
                            <div class="col-md-12">
                                <div class="alert alert-dismissible alert-warning" id="alertValida1" runat="server" visible="false">
                                    <button type="button" class="close" data-dismiss="alert">×</button>
                                    <asp:Label ID="lblAlert1" runat="server" Visible="false"></asp:Label>
                                </div>
                                <div class="alert alert-dismissible alert-success" id="alertSucces1" runat="server" visible="false">
                                    <button type="button" class="close" data-dismiss="alert">×</button>
                                    <asp:Label ID="LblSuccess1" runat="server" Visible="false"></asp:Label>
                                </div>
                            </div>
                            <div class="col-md-12">
                                <div class="row">                                                      
                                 <div class="col-md-2">
                                    <label class="control-label" for="focusedInput">Ubicacion</label>
                                    <asp:DropDownList class="form-control" ID="ddlUbicacionVac" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlUbicacionVac_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div> 
                                      <div class="col-md-2">
                            <label class="control-label" for="focusedInput">Tipos De Planilla</label>
                            <asp:DropDownList class="form-control" ID="ddlTiposPlanilla" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlTiposPlanilla_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                                <div class="col-md-2">
                                    <label class="control-label" for="focusedInput">Periodo</label>
                                    <asp:TextBox ID="txtPeriodo" class="form-control" runat="server" autocomplete="off" ReadOnly="true"></asp:TextBox>
                                </div>
                                </div>
                                <div class="row">
                                <div id="empleado" runat="server">
                                 <div class="col-md-2" >
                                    <label class="control-label" for="focusedInput">Codigo Empleado</label>
                                    <asp:TextBox ID="txtcodigoEmpleadoVacaciones" class="form-control" runat="server" autocomplete="off" onkeypress="return soloNumeros(event)" OnTextChanged="txtcodigoEmpleadoVacaciones_TextChanged" AutoPostBack="true"></asp:TextBox>
                                </div>
                                <div class="col-md-4" >
                                    <label class="control-label" for="focusedInput">Nombre</label>
                                    <asp:TextBox ID="TxtNombreEmpVac" class="form-control" runat="server" autocomplete="off" ReadOnly="true"></asp:TextBox>
                                </div>
                                    <div class="col-md-3" >
                                    <label class="control-label" for="focusedInput">Departamento</label>
                                    <asp:TextBox ID="TxtDeptoVac" class="form-control" runat="server" autocomplete="off" ReadOnly="true"></asp:TextBox>
                                </div>
                                                                    <div class="col-md-2" >
                                    <label class="control-label" for="focusedInput">Dias Vacaciones</label>
                                    <asp:TextBox ID="TxtSaldoVac" class="form-control" runat="server" autocomplete="off" onkeypress="return soloNumeros(event)" ReadOnly="true"></asp:TextBox>
                                </div>
                                    </div>
                                    </div>
                                <div class="row">
                                <div class="col-md-2" style="margin-top: 26px;">
                                    <asp:Button ID="btnProcVacaciones" Class="btn btn-success" runat="server" Text="Procesar" OnClick="btnProcVacaciones_Click" />
                                </div>
                         
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

