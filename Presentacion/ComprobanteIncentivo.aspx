<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ComprobanteIncentivo.aspx.cs" Inherits="NominaRRHH.Presentacion.ComprobanteIncentivo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
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
                    <div class="col-xs-12">

                        <div class="col-xs-3">
                            <label class="control-label" for="focusedInput">Periodo de Pago</label>
                            <asp:TextBox ID="txtperiodo" class="form-control" placeholder="Digite el Periodo"
                                runat="server" autocomplete="off"></asp:TextBox>
                        </div>
                        <div class="col-xs-9">
                            <div class="checkbox">
                                <label>

                                    <asp:RadioButtonList ID="rbllistImpresion" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="rbllistImpresion_SelectedIndexChanged" AutoPostBack="True" TextAlign="Left">
                                        <asp:ListItem Value="1">Por Módulos</asp:ListItem>
                                        <asp:ListItem Value="2">Por Código</asp:ListItem>
                                        <asp:ListItem Value="3">Por Carga de Excel</asp:ListItem>
                                        <asp:ListItem Value="4" Selected="True">TODOS</asp:ListItem>
                                    </asp:RadioButtonList>

                                </label>
                            </div>
                        </div>



                        <br />
                        <div>
                        </div>

                    </div>


                </div>
                <div class="row">
                    <div class="col-xs-12">
                        <div class="col-xs-3">
                            <asp:Panel ID="pnlModulo" runat="server">
                                <label class="control-label" for="focusedInput">Módulos</label>
                                <asp:DropDownList class="form-control" ID="ddlProceso" runat="server" DataTextField="ModuloNombre" DataValueField="MODULO">
                                </asp:DropDownList>
                            </asp:Panel>
                        </div>

                        <div class="col-xs-3">
                            <asp:Panel ID="pnlCodigo" runat="server">
                                <label class="control-label" for="TxtCodigoE">Codigo Empleado</label>
                                <asp:TextBox ID="TxtCodigoE" class="form-control" placeholder="Digite el Codigo"
                                    runat="server" autocomplete="off"></asp:TextBox>
                            </asp:Panel>
                        </div>
                        <div class="col-md-6" style="margin-left: 110px; margin-top: 15px;">
                            <asp:Panel ID="pnlExcel" runat="server">
                                <label class="control-label" for="focusedInput">Cargar Archivo con códigos de Empleados</label>
                                <asp:FileUpload ID="file" class="file" runat="server" />
                            </asp:Panel>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-12" style="margin-top: 26px;">
                        <asp:Button ID="btnAceptar" Class="btn btn-success" runat="server" Text="Aceptar" OnClick="btnAceptar_Click" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

