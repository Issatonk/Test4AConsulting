<%@ Page Language="C#"
    MasterPageFile="~/Site.Master"
    AutoEventWireup="true"
    Async="true"
    CodeBehind="Books.aspx.cs"
    Inherits="Tesr4AConsulting.WebForms.Pages.Books.Books" %>

<asp:Content ID="Content1"
    ContentPlaceHolderID="MainContent"
    runat="server">

    <h2>Домашняя библиотека</h2>

    <div style="margin-bottom: 20px;" class="mb-3">

        <div class="input-group">
            <asp:TextBox
                ID="SearchTextBox"
                runat="server"
                CssClass="form-control"
                placeholder="Поиск по оглавлению">
            </asp:TextBox>

            <asp:Button
                ID="SearchButton"
                runat="server"
                Text="Найти"
                CssClass="btn btn-primary"
                OnClick="SearchButton_Click" />

            <asp:Button
                ID="ResetButton"
                runat="server"
                Text="Сбросить"
                CssClass="btn btn-secondary"
                OnClick="ResetButton_Click" />
        </div>

    </div>

    <asp:HyperLink
        ID="CreateLink"
        runat="server"
        NavigateUrl="~/BookCreate.aspx"
        Text="Добавить книгу"
        CssClass="btn btn-success">
    </asp:HyperLink>

    <br />
    <br />

    <asp:GridView
        ID="BooksGrid"
        runat="server"
        AutoGenerateColumns="False"
        CssClass="table">

        <Columns>

            <asp:BoundField
                DataField="Title"
                HeaderText="Название" />

            <asp:BoundField
                DataField="Author"
                HeaderText="Автор" />

            <asp:BoundField
                DataField="PublicationYear"
                HeaderText="Год" />

            <asp:BoundField
                DataField="Publisher"
                HeaderText="Издательство" />

            <asp:BoundField
                DataField="FirstContentItem"
                HeaderText="Первый пункт оглавления" />

            <asp:TemplateField>
                <ItemTemplate>
                    <asp:HyperLink
                        runat="server"
                        CssClass="btn btn-primary btn-sm"
                        NavigateUrl='<%# Eval("Id", "BookDetails.aspx?id={0}") %>'
                        Text="Просмотр" />
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField>
                <ItemTemplate>
                    <asp:HyperLink
                        runat="server"
                        CssClass="btn btn-warning btn-sm"
                        NavigateUrl='<%# Eval("Id", "BookEdit.aspx?id={0}") %>'
                        Text="Изменить" />
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField>
                <ItemTemplate>
                    <asp:HyperLink
                        runat="server"
                        CssClass="btn btn-danger btn-sm"
                        NavigateUrl='<%# Eval("Id", "BookDelete.aspx?id={0}") %>'
                        Text="Удалить" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>

    </asp:GridView>

</asp:Content>
