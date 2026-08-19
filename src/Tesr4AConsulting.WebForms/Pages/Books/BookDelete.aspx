<%@ Page Title="Удалить книгу"
    Language="C#"
    MasterPageFile="~/Site.Master"
    AutoEventWireup="true"
    Async="true"
    CodeBehind="BookDelete.aspx.cs"
    Inherits="Tesr4AConsulting.WebForms.Pages.Books.BookDelete" %>

<asp:Content ID="Content1"
    ContentPlaceHolderID="MainContent"
    runat="server">

    <h2>Удаление книги</h2>

    <div class="alert alert-warning">
        Вы действительно хотите удалить эту книгу?
    </div>

    <table class="table">
        <tr>
            <th style="width: 200px;">Название</th>
            <td>
                <asp:Label ID="TitleLabel" runat="server" />
            </td>
        </tr>

        <tr>
            <th>Автор</th>
            <td>
                <asp:Label ID="AuthorLabel" runat="server" />
            </td>
        </tr>

        <tr>
            <th>Год издания</th>
            <td>
                <asp:Label ID="YearLabel" runat="server" />
            </td>
        </tr>
    </table>

    <asp:Label
        ID="ErrorLabel"
        runat="server"
        ForeColor="Red"
        Visible="false" />

    <br />

    <asp:Button
        ID="DeleteButton"
        runat="server"
        Text="Удалить"
        CssClass="btn btn-danger"
        OnClick="DeleteButton_Click" />

    <asp:HyperLink
        ID="CancelLink"
        runat="server"
        CssClass="btn btn-secondary"
        Text="Отмена" />

</asp:Content>
