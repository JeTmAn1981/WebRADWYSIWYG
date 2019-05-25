
<!DOCTYPE HTML>
<Html Class="no-js" lang="en" xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title> WebRAD | @ViewBag.ProjectName | Information Systems | Whitworth University</title>
    <meta charset="utf-8" />
    @Html.Partial("TemplateHead")
    @Styles.Render("~/Content/css")
    <link rel="stylesheet" href="//cdn.quilljs.com/1.2.6/quill.snow.css">
    @RenderSection("styles", False)

    <script src="https://cdn.ckeditor.com/ckeditor5/10.0.0/inline/ckeditor.js"></script>
    
</head>
<body class="body">

    @*@{
            var blah = @"\forms\forms-templates\mvc\template-body.htm";
            Response.WriteFile(Server.MapPath(blah));
        }*@
    @Html.Partial("TemplateBody")


    @RenderBody()
    @*@{
            blah = @"\forms\forms-templates\mvc\template-footer.htm";
            Response.WriteFile(Server.MapPath(blah));
        }*@

    @Html.Partial("TemplateFooter")

    @Scripts.Render("~/bundles/jqueryval")
    @*@Scripts.Render("~/Scripts/tinymce")*@


    @RenderSection("scripts", False)

    @*<!-- Main Quill library -->
        <script src="//cdn.quilljs.com/1.3.6/quill.min.js"></script>

        <!-- Theme included stylesheets -->
        <link href="//cdn.quilljs.com/1.3.6/quill.snow.css" rel="stylesheet">
        <link href="//cdn.quilljs.com/1.3.6/quill.bubble.css" rel="stylesheet">*@

    @*<script>
        tinymce.init({
            selector: '.editable',
                auto_focus: '.editable',

                    inline: true,
                    toolbar: 'undo redo',
                    menubar: true
                });</script>*@


</body>
</Html>

