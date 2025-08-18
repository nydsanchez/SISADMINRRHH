<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CooLimpiarSaldo.aspx.cs" Inherits="NominaRRHH.Presentacion.CooLimpiarSaldo" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    
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
                                <div class="row">

                                    <div class="col-md-12 ">
                                        <div class="col-md-2">
                                            <label class="control-label" for="focusedInput">Codigo</label>
                                            <asp:TextBox ID="txtCodigo" class="form-control" autocomplete="off" TabIndex="4"
                                                runat="server" OnTextChanged="txtCodigo_TextChanged" AutoPostBack="true" onkeypress="return soloNumeros(event)"></asp:TextBox>
                                        </div>
                                        <div class="col-md-4">
                                            <label class="control-label" for="focusedInput">Nombre</label>
                                            <asp:TextBox ID="TxtNombreE" class="form-control"
                                                runat="server" autocomplete="off" ReadOnly="true" TabIndex="5"></asp:TextBox>
                                        </div>                                        
                                    </div>
                                </div>
                                
                                <div class="row">
                                    <div class="col-md-12 ">
                                        <div class="col-md-3">
                                            <asp:Button ID="btnLimpiarSaldo" Class="btn btn-success" runat="server" Text="Limpiar Saldo" Style="margin-top: 22px;" OnClick="btnLimpiarSaldo_Click"/>
                                        </div>
                                    </div>
                                </div>
                                <br />
                    </div>
                </div>
            </div>
        </div>
    </div>


    <script type="text/javascript">

        $(function () {
            $('#datePicker').datepicker({
                format: 'dd/mm/yyyy'
            })
                .on('changeDate', function (e) {
                    $('.datepicker dropdown-menu').hide();
                });

            $('#datePicker2').datepicker({
                format: 'dd/mm/yyyy'
            })
                .on('changeDate', function (e) {
                    $('.datepicker2 dropdown-menu').hide();
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

