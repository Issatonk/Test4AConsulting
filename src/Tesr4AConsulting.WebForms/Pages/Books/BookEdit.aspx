<%@ Page Title="Редактировать книгу"
    Language="C#"
    MasterPageFile="~/Site.Master"
    AutoEventWireup="true"
    Async="true"
    CodeBehind="BookEdit.aspx.cs"
    Inherits="Tesr4AConsulting.WebForms.Pages.Books.BookEdit" %>

<asp:Content ID="Content1"
    ContentPlaceHolderID="MainContent"
    runat="server">

    <h2>Редактирование книги</h2>

    <div class="mb-3">
        <label>Название</label>
        <asp:TextBox
            ID="TitleTextBox"
            runat="server"
            CssClass="form-control" />

        <asp:RequiredFieldValidator
            runat="server"
            ControlToValidate="TitleTextBox"
            ErrorMessage="Введите название."
            ForeColor="Red"
            Display="Dynamic" />
    </div>

    <div class="mb-3">
        <label>Автор</label>
        <asp:TextBox
            ID="AuthorTextBox"
            runat="server"
            CssClass="form-control" />

        <asp:RequiredFieldValidator
            runat="server"
            ControlToValidate="AuthorTextBox"
            ErrorMessage="Введите автора."
            ForeColor="Red"
            Display="Dynamic" />
    </div>

    <div class="mb-3">
        <label>Год издания</label>
        <asp:TextBox
            ID="YearTextBox"
            runat="server"
            CssClass="form-control"
            TextMode="Number" />
    </div>

    <div class="mb-3">
        <label>Издательство</label>
        <asp:TextBox
            ID="PublisherTextBox"
            runat="server"
            CssClass="form-control" />
    </div>

    <div class="mb-3">
        <label>ISBN</label>
        <asp:TextBox
            ID="IsbnTextBox"
            runat="server"
            CssClass="form-control" />
    </div>

    <div class="mb-3">
        <label>Описание</label>
        <asp:TextBox
            ID="DescriptionTextBox"
            runat="server"
            CssClass="form-control"
            TextMode="MultiLine"
            Rows="4" />
    </div>

    <div class="mb-3">
        <label>Оглавление</label>

        <asp:TextBox
            ID="ContentsTextBox"
            runat="server"
            CssClass="form-control html-editor"
            TextMode="MultiLine"
            ValidateRequestMode="Disabled"
            Rows="10">
        </asp:TextBox>
    </div>

    <asp:Label
        ID="ErrorLabel"
        runat="server"
        ForeColor="Red"
        Visible="false" />

    <br />

    <asp:Button
        ID="SaveButton"
        runat="server"
        Text="Сохранить"
        CssClass="btn btn-primary"
        OnClick="SaveButton_Click" />

    <asp:HyperLink
        ID="CancelLink"
        runat="server"
        CssClass="btn btn-secondary"
        Text="Отмена" />

    <script src="https://cdn.jsdelivr.net/npm/tinymce@7/tinymce.min.js"></script>

    <script>
        tinymce.init({
            selector: '.html-editor',
            height: 350,
            menubar: false,
            plugins: 'lists link code',
            toolbar: 'undo redo | bold italic | bullist numlist | link | code'
        });
    </script>
</asp:Content>
