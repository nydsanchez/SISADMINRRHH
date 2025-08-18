<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="VLiquidacion.aspx.cs" Inherits="NominaRRHH.Presentacion.VLiquidacion" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
        function PrintReport() {            
            var iframe = document.getElementById('frmPrint');
            var ifWin = iframe.contentWindow || iframe;          
            iframe.focus();
            ifWin.print();
        }
    </script>
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
                <div class="col-md-3">
                    <asp:Button ID="btnProcesar" Class="btn btn-success" runat="server" Style="margin-top: 25px" Text="Imprimir" OnClick="btnProcesar_Click" CommandName="PDF" />
                </div>
                <%-- <div class="row">
                    <div class="col-md-12">
                        <div class="col-md-2">
                            <label class="control-label" for="focusedInput">Buscar</label>
                            <asp:TextBox ID="txtCodigo" class="form-control" placeholder="Digite un Codigo de Empleado"
                                runat="server" autocomplete="off"></asp:TextBox>
                        </div>
                         <div class="col-md-3">
                            <asp:Button ID="btnProcesar" Class="btn btn-success" runat="server" Style="margin-top: 25px" Text="Aceptar" OnClick="btnProcesar_Click" />
                        </div>
                        <div class="col-md-3">
                            <asp:ScriptManager ID="ScriptManager1" runat="server">
                            </asp:ScriptManager>
                        </div>
                    </div>
                </div>--%>
                <div class="col-md-3">
                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-12">
                        <div class="col-md-8">
                            <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="1106" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
                                <LocalReport ReportEmbeddedResource="NominaRRHH.Reportes.Liquidacion2.rdlc">
                                    <DataSources>
                                        <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" Name="DataSet1" />
                                        <rsweb:ReportDataSource DataSourceId="ObjectDataSource2" Name="DataSet2" />
                                    </DataSources>
                                </LocalReport>
                            </rsweb:ReportViewer>
                            <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" SelectMethod="GetData" TypeName="CompanicsaRHDataSetTableAdapters.spLiquidacionDetalleSelTableAdapter"></asp:ObjectDataSource>
                            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetData" TypeName="CompanicsaRHDataSetTableAdapters.spLiquidacionSelTableAdapter"></asp:ObjectDataSource>
                            <asp:ObjectDataSource ID="ObjectDataSource3" runat="server" SelectMethod="GetData" TypeName="CompanicsaRHDataSetTableAdapters.spMostrarEmpleadoTableAdapter"></asp:ObjectDataSource>
                        </div>
                    </div>
                </div>
                <iframe id="frmPrint" name="IframeName" width="500" clientidmode="Static" style="display:none;"
                    height="200" runat="server" runat="server"></iframe>
            </div>
        </div>
    </div>
</asp:Content>

