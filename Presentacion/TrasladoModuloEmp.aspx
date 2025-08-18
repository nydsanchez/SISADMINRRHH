<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TrasladoModuloEmp.aspx.cs" Inherits="NominaRRHH.Presentacion.TrasladoModuloEmp" MaintainScrollPositionOnPostback="true" %>

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

                <asp:Label class="control-label" for="focusedInput" ID="Label1" runat="server" Text="Movimientos por Modulo" Style="font-weight: 700"></asp:Label>
               
                <div class="row">
                    <div class="col-md-12">
                        <ul class="nav nav-tabs">
                            <li class="active"><a href="#registro" data-toggle="tab" aria-expanded="true">Registro</a></li>                          
                             <li class=""><a href="#consulta" data-toggle="tab" aria-expanded="false">Consulta</a></li>
                             <li class=""><a href="#baja" data-toggle="tab" aria-expanded="false">Bajas</a></li>
                        </ul>
                        <div id="myTabContent" class="tab-content paddingTab">
                            <div class="tab-pane fade active in" id="registro">
                                <asp:Label class="control-label" for="focusedInput" ID="Label3" runat="server" Text="Aplicar Traslados" Style="font-weight: 700"></asp:Label>
                                 <br />
                                <asp:Panel ID="panelID" runat="server">
                                    <div class="row">

                                        <div class="col-md-12" runat="server" id="divFile">

                                            <div class="col-md-5">
                                                <asp:Label ID="Label2" runat="server" Font-Bold="True" ForeColor="Blue" Text="Cargar Achivo:  "></asp:Label>
                                                <asp:FileUpload ID="fileProtectedDz" runat="server" />
                                            </div>
                                            <div class="col-md-2">
                                                <asp:Button ID="Button6" Class="btn btn-success" runat="server" Text="Procesar" OnClick="Button6_Click" />
                                            </div>
                                            <div class="col-md-2">
                                                <asp:Button ID="Button1" Class="btn btn-success" runat="server" Text="Guardar" OnClick="Button1_Click" Visible="false" />
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-md-8" runat="server" id="divGrid">
                                         <asp:GridView ID="gvINGDD" class="table table-striped table-hover" runat="server">
                                        </asp:GridView>


                                        </div>
                                    </div>

                                </asp:Panel>
                                
                            </div>
                            <div class="tab-pane fade" id="baja">
                                <asp:Label class="control-label" for="focusedInput" ID="Label4" runat="server" Text="Dar Baja Empleado" Style="font-weight: 700"></asp:Label>
                                 <br />
                                  <div class="row">
                                       <div class="col-md-3">
                                            <label class="control-label" for="focusedInput">Codigo</label>
                                            <asp:TextBox ID="txtCodigoB" class="form-control"
                                                runat="server" autocomplete="off"></asp:TextBox>
                                        </div>
                                    <div class="col-md-3">
                                        <label class="control-label" for="focusedInput">Fecha Inicio</label>
                                        <div class="input-group input-append date" id="datePicker3">
                                            <asp:TextBox ID="txtfechainiB" class="form-control datepicker"
                                                runat="server"></asp:TextBox>
                                            <span class="input-group-addon add-on borderCalendar"><span class="glyphicon glyphicon-calendar"></span>
                                            </span>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <label class="control-label" for="focusedInput">Fecha Fin</label>
                                        <div class="input-group input-append date" id="datePicker4">
                                            <asp:TextBox ID="txtfechafinB" class="form-control datepicker"
                                                runat="server"></asp:TextBox>
                                            <span class="input-group-addon add-on borderCalendar"><span class="glyphicon glyphicon-calendar"></span>
                                            </span>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="col-md-3 col-sm-3 col-xs-5">
                                            <asp:Button ID="Button2" Class="btn btn-success" runat="server" Text="Buscar" OnClick="Button2_Click"/>
                                        </div>
                                    </div>


                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <asp:GridView ID="gvtraslados" class="table table-striped table-hover" runat="server" Width="100%"
                                            AutoGenerateColumns="False" DataKeyNames="codigo_empleado,fecha,codigo_depto" CellPadding="2" GridLines="Vertical"
                                            BorderStyle="None" BorderWidth="1px" EmptyDataText="No hay Datos" OnRowCommand="gvtraslados_RowCommand">
                                            <AlternatingRowStyle Width="100px" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Codigo">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtCodigo" class="form-control" ReadOnly="true" runat="server" Text='<% # Bind("codigo_empleado")%>'></asp:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="10px" />
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Nombre">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtNombre" class="form-control" ReadOnly="true" runat="server" Text='<% # Bind("nombrecompleto")%>'></asp:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="100px" />
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Depto">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtDepto" class="form-control" ReadOnly="true" runat="server" Text='<% # Bind("nombre_depto")%>'></asp:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="50px" />
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Op.">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtOperacion" class="form-control" ReadOnly="true" runat="server" Text='<% # Bind("operacion")%>'></asp:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="50px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Fecha">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtFecha" class="form-control" ReadOnly="true" runat="server" DataFormatString="{0:dd/MM/yyyy}" Text='<% # Bind("fecha")%>'></asp:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="10px" />
                                                </asp:TemplateField>                                              
                                              
                                                <asp:TemplateField HeaderText="Eliminar">
                                                    <ItemTemplate>
                                                        <asp:Button ID="btneliminar"
                                                            CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                            CommandName="eliminar" runat="server" Text="Eliminar" />
                                                    </ItemTemplate>
                                                    <ItemStyle Width="16px" />
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                                </div>
                            <div class="tab-pane fade" id="consulta">
                                 <asp:Label class="control-label" for="focusedInput" ID="Label5" runat="server" Text="Consultar Traslados" Style="font-weight: 700"></asp:Label>
                                 <br />
                                <div class="row">
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
                                        <div class="col-md-3 col-sm-3 col-xs-5">
                                            <asp:Button ID="Button4" Class="btn btn-success" runat="server" Text="Consultar" OnClick="Button4_Click" />
                                        </div>
                                    </div>


                                </div>
                                <div class="row" runat="server" id="div2">
                                   
                                    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="1121px" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
                                    </rsweb:ReportViewer>
                                </div>
                            </div>
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

            $('#datePicker2,#datePicker3,#datePicker4').datepicker({
                format: 'dd/mm/yyyy'
            })
                .on('changeDate', function (e) {
                    $('.datepicker2 dropdown-menu').hide();
                });

        });
    </script>
</asp:Content>

