<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="R_ObtenerMarcas.aspx.cs" Inherits="NominaRRHH.Presentacion.R_ObtenerMarcas" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../Content/bootstrap-clockpicker.css" rel="stylesheet" />
    <link href="../Content/clockpicker.css" rel="stylesheet" />
    <link href="../Content/standalone.css" rel="stylesheet" />
    <link href="../Content/Styles.css" rel="stylesheet" />
    <script src="../Scripts/bootstrap-datetimepicker.js"></script>
    <script src="../Scripts/bootstrap-clockpicker.js"></script>
    <script src="../Scripts/jquery-clockpicker.js"></script>
</asp:Content>
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
                    <div class="col-md-12">
                        <div class="col-md-2">
                            <label class="control-label" for="focusedInput"><strong>Obtener Marcas</strong></label>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12" >   
                              <div class="col-md-3">
                                <label class="control-label" for="focusedInput">Filtrar por</label>
                                <asp:DropDownList class="form-control" ID="ddlAsigPerm" runat="server" OnSelectedIndexChanged="ddlAsigPerm_SelectedIndexChanged" AutoPostBack="True">
                                    <asp:ListItem Value="1"> Todos</asp:ListItem>
                                    <asp:ListItem Value="2"> Ubicacion</asp:ListItem>
                                    <asp:ListItem Value="3"> Deptos x Ubicacion</asp:ListItem>
                                    <asp:ListItem Value="4"> Por Departamento</asp:ListItem>                                  
                                </asp:DropDownList>
                            </div>
                         <div class="col-md-3" runat="server" id="divubic" visible="false">
                                <label class="control-label" for="focusedInput">Ubicacion</label>
                                <asp:DropDownList class="form-control" ID="ddlubicacion" runat="server">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-3" runat="server" id="divproceso" visible="false">
                                <label class="control-label" for="focusedInput">Proceso</label>
                                <asp:DropDownList class="form-control" ID="ddlProceso" runat="server">
                                </asp:DropDownList>
                            </div>               
                        
                    </div>
                </div>
                 <div class="row">
                    <div class="col-md-12" >   
                         <div class="col-md-3">
                            <label class="control-label" for="focusedInput">Fecha Inicio</label>
                            <div class="input-group input-append date" id="datePicker">
                                <asp:TextBox ID="txtFechaIni" class="form-control datepicker"
                                    runat="server"></asp:TextBox>
                                <span class="input-group-addon add-on borderCalendar"><span class="glyphicon glyphicon-calendar"></span>
                                </span>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <label class="control-label" for="focusedInput">Fecha Fin</label>
                            <div class="input-group input-append date" id="datePicker2">
                                <asp:TextBox ID="txtFechaFin" class="form-control datepicker"
                                    runat="server"></asp:TextBox>
                                <span class="input-group-addon add-on borderCalendar"><span class="glyphicon glyphicon-calendar"></span>
                                </span>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <asp:Button ID="btnMostrar" Visible="true" Class="btn btn-info" runat="server" Style="margin-top: 25px" Text="Aceptar" OnClick="btnMostrar_Click" />
                        </div>
                        </div>
                     </div>
                <br />
                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                <div class="row">
                    <div class="col-md-12">
                        <div>
                            <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="1122px" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
                                <LocalReport ReportEmbeddedResource="NominaRRHH.Reportes.Ingresos.rdlc">
                                    <DataSources>
                                        <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" Name="DataSet1" />
                                    </DataSources>
                                </LocalReport>
                            </rsweb:ReportViewer>
                        </div>
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
        });
        $(function () {
            $('#datePicker2').datepicker({
                format: 'dd/mm/yyyy'
            })
            .on('changeDate', function (e) {
                $('.datepicker dropdown-menu').hide();
            });
        });
    </script>
</asp:Content>

