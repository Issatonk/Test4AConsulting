<%@ Page Title="Добавить книгу"
    Language="C#"
    MasterPageFile="~/Site.Master"
    AutoEventWireup="true"
    Async="true"
    CodeBehind="BookCreate.aspx.cs"
    Inherits="Tesr4AConsulting.WebForms.Pages.Books.BookCreate" %>

<asp:Content ID="Content1"
    ContentPlaceHolderID="MainContent"
    runat="server">

    <h2>Добавить книгу</h2>

    <div class="mb-3">
        <label>Название</label>

        <asp:TextBox
            ID="TitleTextBox"
            runat="server"
            CssClass="form-control">
        </asp:TextBox>

        <asp:RequiredFieldValidator
            ID="TitleValidator"
            runat="server"
            ControlToValidate="TitleTextBox"
            ErrorMessage="Введите название."
            ForeColor="Red"
            Display="Dynamic">
        </asp:RequiredFieldValidator>
    </div>

    <div class="mb-3">
        <label>Автор</label>

        <asp:TextBox
            ID="AuthorTextBox"
            runat="server"
            CssClass="form-control">
        </asp:TextBox>

        <asp:RequiredFieldValidator
            ID="AuthorValidator"
            runat="server"
            ControlToValidate="AuthorTextBox"
            ErrorMessage="Введите автора."
            ForeColor="Red"
            Display="Dynamic">
        </asp:RequiredFieldValidator>
    </div>

    <div class="mb-3">
        <label>Год издания</label>

        <asp:TextBox
            ID="YearTextBox"
            runat="server"
            CssClass="form-control"
            TextMode="Number">
        </asp:TextBox>
    </div>

    <div class="mb-3">
        <label>Издательство</label>

        <asp:TextBox
            ID="PublisherTextBox"
            runat="server"
            CssClass="form-control">
        </asp:TextBox>
    </div>

    <div class="mb-3">
        <label>ISBN</label>

        <asp:TextBox
            ID="IsbnTextBox"
            runat="server"
            CssClass="form-control">
        </asp:TextBox>
    </div>

    <div class="mb-3">
        <label>Описание</label>

        <asp:TextBox
            ID="DescriptionTextBox"
            runat="server"
            CssClass="form-control"
            TextMode="MultiLine"
            Rows="4">
        </asp:TextBox>
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
        Visible="false">
    </asp:Label>

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
        Text="Отмена">
    </asp:HyperLink>

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
