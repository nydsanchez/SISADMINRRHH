<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="IncReporteDeCumplimiento.aspx.cs" Inherits="NominaRRHH.Presentacion.IncReporteDeCumplimiento" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="../Scripts/jquery-ui-1.10.4.custom.min.js"></script>
    <link href="../Content/jquery-ui-1.10.4.custom.min.css" rel="stylesheet" />
    <script src="../Scripts/bootstrap-datetimepicker.js"></script>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mar-top" id="marcas">
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
                            <div class="col-md-2">
                            <label class="control-label" for="focusedInput">Periodo</label>
                            <asp:TextBox ID="txtPeriodo" autofocus="true" class="form-control" placeholder="Digite Periodo"
                                runat="server" onkeypress="return soloNumeros(event)" autocomplete="off"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            <asp:Button ID="btnBuscar" Class="btn btn-success" Style="margin-top: 22px;" runat="server" Text="Buscar" OnClick="btnBuscar_Click" />
                        </div>
                   
                </div>              
                <br />
                <div class="row">
                    <div class="col-md-12">
                        <asp:GridView ID="gvRpt" class="table table-striped table-hover" runat="server"
                            AutoGenerateColumns="True" CellPadding="2" GridLines="Vertical"
                            BorderStyle="None" BorderWidth="1px" EmptyDataText="No hay Datos" ClientIDMode="Static">
                            <AlternatingRowStyle Width="100px" />
                            <FooterStyle BackColor="#CCCCCC" ForeColor="Black" CssClass="FooterStyle" />
                        </asp:GridView>
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

        function soloNumerosHora(e) {
            key = e.keyCode || e.which;
            tecla = String.fromCharCode(key).toLowerCase();
            letras = "0123456789:";
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
