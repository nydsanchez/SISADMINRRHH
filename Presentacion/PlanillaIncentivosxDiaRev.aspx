<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PlanillaIncentivosxDiaRev.aspx.cs" Inherits="NominaRRHH.Presentacion.PlanillaIncentivosxDiaRev" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="../Scripts/jquery-ui-1.10.4.custom.min.js"></script>
    <link href="../Content/jquery-ui-1.10.4.custom.min.css" rel="stylesheet" />
    <script src="../Scripts/bootstrap-datetimepicker.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>


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
                       
                        <div class="col-md-2">
                            <asp:Label class="control-label" for="focusedInput" ID="Label3" runat="server" Text="Periodo" Style="font-weight: 700"></asp:Label>
                            <asp:TextBox ID="txtPeriodo" class="form-control" placeholder="Digite un Periodo"
                                runat="server" autocomplete="off"></asp:TextBox>
                        </div>
                         <div class="col-md-1">

                            <asp:Label class="control-label" for="focusedInput" ID="lblSemana" runat="server" Text="Semana" Style="font-weight: 700"></asp:Label>
                            <asp:DropDownList class="form-control" ID="ddlTipo" runat="server">
                                <asp:ListItem>1</asp:ListItem>
                                <asp:ListItem>2</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                     <div class="col-md-3">

                            <asp:Label class="control-label" for="focusedInput" ID="Label2" runat="server" Text="Semana" Style="font-weight: 700"></asp:Label>
                            <asp:DropDownList class="form-control" ID="ddlReporte" runat="server" >
                                <asp:ListItem Value="1">Detalle Modulo</asp:ListItem>
                                <asp:ListItem Value="2">Detalle Empleado</asp:ListItem>
                                <asp:ListItem Value="3">Incentivo Total</asp:ListItem>                              
                                <asp:ListItem Value="4">Produccion Pendiente</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    
                       
                    </div>
                     <br />
                    <div class="row">
                     <div class="col-md-3">
                            <div class="col-md-3 col-sm-3 col-xs-5">
                                <asp:Button ID="Button4" Class="btn btn-success" runat="server" Text="Generar" OnClick="Button4_Click" />
                            </div>
                        </div>
                       
                        </div>
                    
             
                 <br />
               
                     <br />
                    <div class="row" runat="server" id="div5" visible="false">
                          <div class="col-md-2">
                            <asp:Label class="control-label" for="focusedInput" ID="Label1" runat="server" Text="Codigo" Style="font-weight: 700"></asp:Label>
                            <asp:TextBox ID="TxtCodigo" class="form-control" placeholder="Digite un Codigo"
                                runat="server" autocomplete="off"></asp:TextBox>
                        </div>
                        <div class="col-md-3" style="margin-top:23px;">
                            <div class="col-md-3 col-sm-3 col-xs-5">
                                <asp:Button ID="Button5" Class="btn btn-success" runat="server" Text="Consultar" OnClick="Button5_Click" />
                            </div>
                        </div>
                        </div>
                     <br />
                    <div class="row">
                       
                    </div>

                </div>                            

               
                <br />
                 <div class="row" visible="false" runat="server" id="div1">
                     <label class="control-label" for="focusedInput">Produccion por Modulo</label>
                    <rsweb:ReportViewer ID="ReportViewer3" runat="server" Width="1121px" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
                    </rsweb:ReportViewer>
                </div>
                <div class="row" visible="false" runat="server" id="div2">
                     <label class="control-label" for="focusedInput">Detalle Empleado por Dia</label>
                    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="1121px" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
                    </rsweb:ReportViewer>
                </div>
                <div class="row" visible="false" runat="server" id="div3">
                     <label class="control-label" for="focusedInput">Incentivo total</label>
                     <rsweb:ReportViewer ID="ReportViewer2" runat="server" Width="1121px" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
                    </rsweb:ReportViewer>
                </div>
                 <div class="row" visible="false" runat="server" id="div4">
                     <label class="control-label" for="focusedInput">Desgloce pago por Empleado</label>
                     <rsweb:ReportViewer ID="ReportViewer4" runat="server" Width="1121px" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
                    </rsweb:ReportViewer>
                </div>
                  <div class="row" visible="false" runat="server" id="div6">
                     <label class="control-label" for="focusedInput">Pendiente de pago</label>
                    <rsweb:ReportViewer ID="ReportViewer5" runat="server" Width="1121px" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
                    </rsweb:ReportViewer>
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
            $('#datePicker3').datepicker({
                format: 'dd/mm/yyyy'
            })
                .on('changeDate', function (e) {
                    $('.datepicker3 dropdown-menu').hide();
                });
        });
    </script>
</asp:Content>

