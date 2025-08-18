<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Empresa.aspx.cs" Inherits="NominaRRHH.Presentacion.Empresa" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../Content/Styles.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mar-top">
        <div class="panel panel-info" style="width: 1220px;">
            <div class="panel-body">
                <div class="alert alert-dismissible alert-warning" id="alertValida" runat="server" visible="false">
                    <button type="button" class="close" data-dismiss="alert">×</button>
                    <asp:Label ID="lblAlert" runat="server" Visible="false"></asp:Label>
                </div>
                <div class="alert alert-dismissible alert-success" id="alertSucces" runat="server" visible="false">
                    <button type="button" class="close" data-dismiss="alert">×</button>
                    <asp:Label ID="LblSuccess" runat="server" Visible="false"></asp:Label>
                </div>
                <fieldset>
                    <legend>Empresa</legend>
                <%--    <div class="row">
                        <div class="col-md-12">
                            <div class="col-md-2">
                                <label class="control-label" for="focusedInput">Nombre Empresa</label>
                                <asp:TextBox ID="txtNombEmprs" class="form-control" placeholder="Nombre Empresa"
                                    runat="server"  autocomplete="off"></asp:TextBox>
                            </div>
                            <div class="col-md-1">
                                <asp:ImageButton ID="btnBuscar" Class="btnSearch" ImageUrl="~/Images/lupa3.png" runat="server" OnClick="btnBuscar_Click" />
                            </div>
                        </div>
                    </div>--%>
                    <div class="row">
                        <div class="col-md-12 paddingContentTabs">
                            <asp:HiddenField runat="server" ID="hfempresa" Value="0" />
                            <div class="col-md-3">
                                <label class="control-label" for="focusedInput">Empresa</label>
                                <asp:TextBox ID="txtEmpresa" class="form-control"
                                    runat="server" autocomplete="off"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <label class="control-label" for="focusedInput">Gerente RRHH</label>
                                <asp:TextBox ID="txtGerRrhh" class="form-control"
                                    runat="server" autocomplete="off"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <label class="control-label" for="focusedInput">Pais</label>
                                <asp:DropDownList class="form-control" ID="ddlPais" runat="server">
                                </asp:DropDownList>
                            </div>
                             <div class="col-md-3">
                                        <label class="control-label" for="focusedInput">Moneda de Pago</label>
                                        <asp:DropDownList class="form-control" ID="ddlMoneda" runat="server">
                                        </asp:DropDownList>
                                    </div>
                           <%-- <div class="col-md-3">
                                <label class="control-label" for="focusedInput">Idioma Por Defecto</label>
                                <asp:TextBox ID="txtIdioma" class="form-control"
                                    runat="server" autocomplete="off"></asp:TextBox>
                            </div>--%>
                        </div>
                        <div class="col-md-12 paddingContentTabs">
                            <div class="col-md-3">
                                <label class="control-label" for="focusedInput">Tipo de Nomina</label>
                                <asp:DropDownList class="form-control" ID="ddlTipNomina" runat="server">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-3">
                                <label class="control-label" for="focusedInput">Salario Minimo</label>
                                <asp:TextBox ID="txtSalarMin" class="form-control"
                                    runat="server" autocomplete="off"></asp:TextBox>
                            </div>
                           <%-- <div class="col-md-3">
                                <label class="control-label" for="focusedInput">% SS Empleado</label>
                                <asp:TextBox ID="txtPorcSEmple" class="form-control"
                                    runat="server" autocomplete="off"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <label class="control-label" for="focusedInput">% SS Empresa</label>
                                <asp:TextBox ID="txtPorcSEmpresa" class="form-control"
                                    runat="server" autocomplete="off"></asp:TextBox>
                            </div>--%>
                        </div>
                        <%--<div class="col-md-12 paddingContentTabs">
                            <div class="col-md-3">
                                <label class="control-label" for="focusedInput">% Educacion Empresa</label>
                                <asp:TextBox ID="txtPorcEdcEmp" class="form-control"
                                    runat="server" autocomplete="off"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <label class="control-label" for="focusedInput">Salario Maximo SS</label>
                                <asp:TextBox ID="txtSalarMaxSS" class="form-control"
                                    runat="server" autocomplete="off"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <label class="control-label" for="focusedInput">Maximo SS Empleado</label>
                                <asp:TextBox ID="txtMaxSSEmp" class="form-control"
                                    runat="server" autocomplete="off"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <label class="control-label" for="focusedInput">Maximo SS Empresa</label>
                                <asp:TextBox ID="txtMaxSSEmpr" class="form-control"
                                    runat="server" autocomplete="off"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-12 paddingContentTabs">
                            <div class="col-md-3">
                                <label class="control-label" for="focusedInput">Minimo SS 4 Sem Empleado</label>
                                <asp:TextBox ID="txtMinS4Sempl" class="form-control"
                                    runat="server" autocomplete="off"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <label class="control-label" for="focusedInput">Minimo SS 5 Sem Empleado</label>
                                <asp:TextBox ID="txtMinS5Sem" class="form-control"
                                    runat="server" autocomplete="off"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <label class="control-label" for="focusedInput">Valor SS 4 Sem Empleado</label>
                                <asp:TextBox ID="txtValorS4Sem" class="form-control"
                                    runat="server" autocomplete="off"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <label class="control-label" for="focusedInput">Valor SS 5 Sem Empleado</label>
                                <asp:TextBox ID="txtValorS5Sem" class="form-control"
                                    runat="server" autocomplete="off"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-12 paddingContentTabs">
                            <div class="col-md-3">
                                <label class="control-label" for="focusedInput">Minimo SS 4 Sem Empresa</label>
                                <asp:TextBox ID="txtMinS4Sempresa" class="form-control"
                                    runat="server" autocomplete="off"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <label class="control-label" for="focusedInput">Minimo SS 5 Sem Empresa</label>
                                <asp:TextBox ID="txtMinS5Sempresa" class="form-control"
                                    runat="server" autocomplete="off"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <label class="control-label" for="focusedInput">Valor SS 4 Sem Empresa</label>
                                <asp:TextBox ID="txtValorSS4Semprs" class="form-control"
                                    runat="server" autocomplete="off"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <label class="control-label" for="focusedInput">Valor SS 5 Sem Empresa</label>
                                <asp:TextBox ID="txtValorSS5Semprs" class="form-control"
                                    runat="server" autocomplete="off"></asp:TextBox>
                            </div>
                        </div>--%>
                        <div class="col-md-12 paddingContentTabs">
                           <%-- <div class="col-md-3">
                                <label class="control-label" for="focusedInput">Factor de Cambio</label>
                                <asp:TextBox ID="txtFactCamb" class="form-control"
                                    runat="server" autocomplete="off"></asp:TextBox>
                            </div>
                            <div class="col-md-2">
                                <div class="col-md-1 marginChkActivo">
                                    <div class="checkbox">
                                        <label class="control-label" for="focusedInput">
                                            <asp:CheckBox ID="ChkAntiguedad" runat="server" />
                                            <strong>Paga Antiguedad?</strong>
                                        </label>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="col-md-1 marginChkActivo">
                                    <div class="checkbox">
                                        <label class="control-label" for="focusedInput">
                                            <asp:CheckBox ID="chkRedondeo" runat="server" />
                                            <strong>Redondeo Salarios?</strong>
                                        </label>
                                    </div>
                                </div>
                            </div>--%>
                             <div class="col-md-2">
                                <div class="col-md-1 marginChkActivo">
                                    <div class="checkbox">
                                        <label class="control-label" for="focusedInput">
                                            <asp:CheckBox ID="chkVacPromedio" runat="server" />
                                            <strong>Prom. Vac. Desc.?</strong>
                                        </label>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <%--<asp:Button ID="btnGuardar" Class="btn btn-info" Visible="false" Style="margin-top: 22px;" runat="server" Text="Guardar" OnClick="btnGuardar_Click" />--%>
                                <asp:Button ID="btnAgregar" Class="btn btn-success" Style="margin-top: 22px;" runat="server" Text="Guardar" OnClick="btnAgregar_Click"/>
                            </div>
                            <%--<div class="col-md-2">
                                <asp:Button ID="btnEliminar" Class="btn btn-danger" Visible="false"  Style="margin-top: 22px;" runat="server" Text="Eliminar" OnClick="btnEliminar_Click"/>
                            </div>--%>
                        </div>
                    </div>
                </fieldset>
            </div>
        </div>
    </div>
</asp:Content>

