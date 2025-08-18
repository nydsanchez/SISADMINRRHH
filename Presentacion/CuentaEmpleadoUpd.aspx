<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CuentaEmpleadoUpd.aspx.cs" Inherits="NominaRRHH.Presentacion.CuentaEmpleadoUpd" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../Content/Styles.css" rel="stylesheet" />
    <link href="../Content/fileinput.css" rel="stylesheet" />
    <script src="../Scripts/fileinput.js"></script>
    <script src="../Scripts/bootstrap-datetimepicker.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
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
                                        <label class="control-label" for="focusedInput">Tipo</label>
                                        <asp:DropDownList class="form-control" ID="ddlTipoCuenta" runat="server">
                                            <asp:ListItem Value="1">Cuenta Bancaria</asp:ListItem>
                                            <asp:ListItem Value="2">Seguro Social</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                               
                                </div>
                              
                                <div class="row" id="divexcel" runat="server">
                                    <div class="col-md-12" style="margin-left: 50px; margin-top: 15px;">
                                        <div class="col-md-6" style="margin-left: 110px; margin-top: 15px;">
                                            <label class="control-label" for="focusedInput">Cargar Archivo</label>
                                            <asp:FileUpload ID="FileVac" class="file" runat="server" />
                                        </div>
                                        <div class="col-md-2" style="margin-left: 15px; margin-top: 40px;">
                                            <asp:Button ID="BtnExcelEmp" Class="btn btn-success" runat="server" Text="Cargar" OnClick="BtnExcelEmp_Click" />
                                        </div>
                                        <div class="col-md-2" style="margin-left: -90px; margin-top: 40px;">
                                        <asp:Button ID="btnProc" Class="btn btn-success" runat="server" Text="Guardar" OnClick="btnProc_Click"/>
                                    </div>
                                    </div>
                                </div>      
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                        <asp:GridView ID="GridView1" class="table table-striped table-hover" runat="server">
                        </asp:GridView>
                    </div>
                                </div>
                                
                            </div>
                        </div>
            </div>
        </div>
    </div>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
        <ProgressTemplate>
            <div class="overlay" />
            <div class="overlayContent">
                <h1>
                    <p style="margin-left: -118px; margin-top: 300px;">Procesando...</p>
                </h1>
                <div class="whirly-loader" style="margin-top: 108px;">
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <script type="text/javascript">
        $("#MainContent_ChkPerSem").click(function () {
            if ($(this).is(':checked')) {
                $("#procPlanSemana").fadeIn();
                $("#procPlanPeriodo").fadeOut();
                $("#MainContent_ChkQuincenal").prop("checked", false);
            }
            else {
                $("#procPlanSemana").fadeOut();
            }
        });

        $("#MainContent_btnTipoPlanilla").click(function () {
            $("#MainContent_divCargArch").fadeIn();
        });
        $(function () {
            $('#datePicker').datepicker({
                format: 'dd/mm/yyyy'
            })
            .on('changeDate', function (e) {
                $('.datepicker dropdown-menu').hide();
            });
        });
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
