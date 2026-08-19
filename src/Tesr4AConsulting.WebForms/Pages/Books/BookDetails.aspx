<%@ Page Title="Карточка книги"
    Language="C#"
    MasterPageFile="~/Site.Master"
    AutoEventWireup="true"
    Async="true"
    CodeBehind="BookDetails.aspx.cs"
    Inherits="Tesr4AConsulting.WebForms.Pages.Books.BookDetails" %>

<asp:Content ID="Content1"
    ContentPlaceHolderID="MainContent"
    runat="server">

    <h2>Карточка книги</h2>

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

        <tr>
            <th>Издательство</th>
            <td>
                <asp:Label ID="PublisherLabel" runat="server" />
            </td>
        </tr>

        <tr>
            <th>ISBN</th>
            <td>
                <asp:Label ID="IsbnLabel" runat="server" />
            </td>
        </tr>

        <tr>
            <th>Описание</th>
            <td>
                <asp:Label ID="DescriptionLabel" runat="server" />
            </td>
        </tr>
    </table>

    <h3>Оглавление</h3>

    <asp:BulletedList
        ID="ContentsList"
        runat="server">
    </asp:BulletedList>

    <asp:Label
        ID="NoContentsLabel"
        runat="server"
        Text="Оглавление отсутствует."
        Visible="false">
    </asp:Label>

    <br />

    <asp:HyperLink
        ID="EditLink"
        runat="server"
        Text="Изменить"
        CssClass="btn btn-warning">
    </asp:HyperLink>
    &nbsp;

    <asp:HyperLink
        ID="DeleteLink"
        runat="server"
        Text="Удалить"
        CssClass="btn btn-danger">
    </asp:HyperLink>

    &nbsp;

    <asp:HyperLink
        ID="BackLink"
        runat="server"
        Text="Назад к списку"
        CssClass="btn btn-secondary">
    </asp:HyperLink>

</asp:Content>
