<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProcesarMarcas.aspx.cs" Inherits="NominaRRHH.Presentacion.ProcesarMarcas" %>


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
               <%--  <div class="row">
                    <div class="col-md-12" >
                    <div class="col-md-2" >
                                    <div class="checkbox">
                                        <label>
                                            <asp:CheckBox ID="ChkExcel" runat="server" AutoPostBack="True" OnCheckedChanged="ChkExcel_CheckedChanged" Checked="false"/>
                                            <strong>¿Cargar excel con Marcas?</strong>
                                        </label>
                                    </div>
                                </div> 
                        </div>
                     </div>--%>
               <div class="row" id="divgetmarca" visible="true" runat="server">
                    <div class="col-md-12" style="200px">                       
                         <div class="col-md-3">
                            <label class="control-label" for="focusedInput">Fecha</label>
                            <div class="input-group input-append date" id="datePicker">
                                <asp:TextBox ID="txtFechaIni" class="form-control datepicker"
                                    runat="server"></asp:TextBox>
                                <span class="input-group-addon add-on borderCalendar">
                                </span>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <asp:Button ID="btnMostrar" Visible="true" Class="btn btn-info" runat="server" Style="margin-top: 25px" Text="Aceptar" OnClick="btnMostrar_Click" />
                        </div>
                        <div class="col-md-14"><asp:TextBox ID="TxtMsg" runat="server" ReadOnly="True" Rows="6" TextMode="MultiLine" Width="500px"></asp:TextBox></div>
                    </div>
                </div>
              <%--  <div class="row invi" runat="server" id="divexcel" visible="false">
                       <div class="col-md-12" style="margin-left: 50px; margin-top: 15px;">
                        <div class="col-md-6" style="margin-left: 110px; margin-top: 15px;">
                            <label class="control-label" for="focusedInput">Cargar Archivo de Empleados</label>
                            <asp:FileUpload ID="file" class="file" runat="server" />
                        </div>
                        <div class="col-md-2" style="margin-left: 15px; margin-top: 18px;">
                            <asp:Button ID="btnCargar" Class="btn btn-success" Style="margin-top: 22px;" runat="server" Text="Cargar"  OnClick="btnCargar_Click" />
                        </div>  
                              <div class="col-md-2" style="margin-top: 30px; margin-left: -48px;">
                                    <asp:Button ID="btnProcesar" Class="btn btn-warning" Visible="false" Style="margin-top: 22px;" runat="server" Text="Procesar" OnClick="btnProcesar_Click" />
                                </div>                    
                    </div>
                           
                            <div class="col-md-12">
                                <asp:GridView ID="GVMarcas" class="table table-striped table-hover" runat="server">
                                </asp:GridView>
                            </div>
                        </div>--%>
                <br />
                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
               <%-- <div class="row">--%>
                    <div class="col-md-12">
                        <div>
                            <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="1122px" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
                                <LocalReport ReportEmbeddedResource="">
                                <%--<LocalReport ReportEmbeddedResource="NominaRRHH.Reportes.Ingresos.rdlc">--%>
                                <%--    <DataSources>
                                        <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" Name="DataSet1" />
                                    </DataSources>--%>
                                </LocalReport>
                            </rsweb:ReportViewer>
                        </div>
                    </div>
                <%--</div>--%>

         
            </div>
        </div>
    </div>
   <script type="text/javascript">
        $(function () {
            $('#datePicker').datepicker({
                format: 'yyyy/mm/dd'
            })
            .on('changeDate', function (e) {
                $('.datepicker dropdown-menu').hide();
            });
        });       
    </script>
</asp:Content>



