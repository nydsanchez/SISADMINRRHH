<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="HorasExtras.aspx.cs" Inherits="NominaRRHH.Presentacion.HorasExtras" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="../Scripts/jquery-ui-1.10.4.custom.min.js"></script>
    <link href="../Content/jquery-ui-1.10.4.custom.min.css" rel="stylesheet" />
    <link href="../Content/fileinput.css" rel="stylesheet" />
    <script src="../Scripts/fileinput.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mar-top">
        <div class="panel panel-info">
            <div class="panel-body">
                <div class="row">
                    <div class="alert alert-dismissible alert-warning" id="alertValida" runat="server" visible="false">
                        <button type="button" class="close" data-dismiss="alert">×</button>
                        <asp:Label ID="lblAlert" runat="server" Visible="false"></asp:Label>
                    </div>
                    <div class="alert alert-dismissible alert-success" id="alertSucces" runat="server" visible="false">
                        <button type="button" class="close" data-dismiss="alert">×</button>
                        <asp:Label ID="LblSuccess" runat="server" Visible="false"></asp:Label>
                    </div>
                    <div class="col-md-12">
                        <div class="col-md-2">
                            <label class="control-label" for="focusedInput">Ubicacion</label>
                            <asp:DropDownList class="form-control" ID="ddlUbicacion" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlUbicacion_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-2">
                            <label class="control-label" for="focusedInput">Tipos De Planilla</label>
                            <asp:DropDownList class="form-control" ID="ddlTiposPlanilla" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlTiposPlanilla_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-1">
                            <label class="control-label" for="focusedInput">Periodo</label>
                            <asp:TextBox ID="txtPeriodo" class="form-control" runat="server" onkeypress="return soloNumeros(event)" ReadOnly="true" autocomplete="off"></asp:TextBox>
                        </div>
                        <div class="col-md-1" id="divSemana" runat="server" visible="false">
                            <label class="control-label" for="focusedInput">Semana</label>
                            <asp:DropDownList class="form-control" Style="width: 67px;" ID="ddlSemana" runat="server">
                                <asp:ListItem Value="1"> 1</asp:ListItem>
                                <asp:ListItem Value="2"> 2</asp:ListItem>
                            </asp:DropDownList>
                        </div>

                    </div>
                    <br />
              <%--      <div class="col-md-12">
                        <div class="col-md-2">
                            <label class="control-label" for="focusedInput">Codigo</label>
                            <asp:TextBox ID="txtcodigoAsig" class="form-control"
                                runat="server" autocomplete="off" OnTextChanged="txtcodigoAsig_TextChanged" AutoPostBack="true"></asp:TextBox>
                        </div>
                        <div class="col-md-4">
                            <label class="control-label" for="focusedInput">Nombre Empleado</label>
                            <asp:TextBox ID="TxtNombEmp2" class="form-control" ReadOnly="true"
                                runat="server" autocomplete="off"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            <label class="control-label" for="focusedInput">Horas</label>
                            <asp:TextBox ID="txtHoras" class="form-control" runat="server" onkeypress="return soloNumeros(event)" autocomplete="off"></asp:TextBox>
                        </div>
                        <div class="col-md-1">
                            <asp:Button ID="btnGuardar" Class="btn btn-success" Style="margin-top: 22px;" runat="server" Text="Asignar" OnClick="btnGuardar_Click" />
                        </div>
                    </div>--%>

                </div>
                 <div class="col-md-12">
                                <div class="row">
                                    <div class="col-md-2" id="divChkDptoVac">
                                        <div class="checkbox">
                                            <label>
                                                <asp:CheckBox ID="ChkFecha" runat="server" Checked="false"/>
                                                <strong>Horas Extras por Fecha</strong>
                                            </label>
                                        </div>
                                    </div>
                                    </div>

                 </div>
                <br />
                <div class="row" runat="server" id="div1">
                    <div class="col-md-6">
                        <label class="control-label" for="focusedInput">Cargar Horas Extras</label>
                        <asp:FileUpload ID="file" class="file" runat="server" />
                    </div>
                    <div class="col-md-6">
                        <label class="control-label" for="focusedInput">Cargar Otros Ingresos y Deducciones</label>
                        <asp:FileUpload ID="FileUpload1" class="file" runat="server" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <div class="col-md-2">
                            <asp:Button ID="btnCargar" Class="btn btn-success" Style="margin-top: 22px;" runat="server" Text="Cargar" OnClick="btnCargar_Click" />
                        </div>
                        <div class="col-md-2">
                            <asp:Button ID="btnProcesar" Class="btn btn-warning" Visible="false" Style="margin-top: 22px;" runat="server" Text="Guardar" OnClick="btnProcesar_Click" />
                        </div>
                    </div>
                    <div class="col-md-6">

                        <div class="col-md-2">
                            <asp:Button ID="Button1" Class="btn btn-success" Style="margin-top: 22px;" runat="server" Text="Cargar" OnClick="Button1_Click" />
                        </div>
                        <div class="col-md-2">
                            <asp:Button ID="Button2" Class="btn btn-warning" Visible="false" Style="margin-top: 22px;" runat="server" Text="Guardar" OnClick="Button2_Click" />
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <asp:GridView ID="GVDevDeduc" class="table table-striped table-hover" runat="server">
                        </asp:GridView>
                    </div>
                     <div class="col-md-6">
                        <asp:GridView ID="GridView1" class="table table-striped table-hover" runat="server">
                        </asp:GridView>
                    </div>
                </div>
                <br />
              
            </div>
        </div>
    </div>
    <script type="text/javascript">
        $(function () {
          <%--  $('#<%= TxtNombEmp2.ClientID %>').autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: "JefeInmediato.asmx/getNombreJefe",
                        data: "{ 'nombreJefe': '" + request.term + "' }",
                        type: "POST",
                        dataType: "json",
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            response(data.d);
                        },
                        error: function (result) {
                            alert('Ocurrio un error con su busqueda');
                        }
                    });
                },
                minLength: 0
            });--%>
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

