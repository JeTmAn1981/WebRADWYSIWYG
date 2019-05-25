<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0">

    @Styles.Render("~/Content/css")
    @RenderSection("styles", False)
    

    <title>
        @ViewBag.Title
    </title>
    <style>
        .hoverDroppable {
            background-color: lightgreen;
        }

        .draggableField {
            /* float: left; */
            padding-left: 5px;
            position: relative;
            /*border:2px solid black;*/
        }

            .draggableField > input, select, button, .checkboxgroup, .selectmultiple, .radiogroup {
                margin-top: 10px;
                margin-right: 10px;
                margin-bottom: 10px;
            }

            .draggableField:hover {
                background-color: #ccffcc;
            }

        .overlay {
            position: relative;
            top: 0;
            left: 0;
            z-index: 5;
        }

        .overlay2 {
            position: relative;
            top: -5em;
            left: 0;
            z-index: 10;
            background-color:white;
            border:solid 3px black;
        }


        .ConfigurationButton {
            width: 30px;
            margin: 5px;
        }
    </style>

    <style id="content-styles">
        /* Styles that are also copied for Preview */
        body {
            margin: 10px 0 0 10px;
        }

        .control-label {
            display: inline-block !important;
            padding-top: 5px;
            text-align: right;
            vertical-align: baseline;
            padding-right: 10px;
        }

        .droppedField {
            padding-left: 5px;
        }

            .droppedField > input, select, button, .checkboxgroup, .selectmultiple, .radiogroup {
                margin-top: 10px;
                margin-right: 10px;
                margin-bottom: 10px;
            }

        .action-bar .droppedField {
            float: left;
            padding-left: 5px;
        }
    </style>
</head>
<body>
    @Scripts.Render("~/bundles/jquery")
    @Scripts.Render("~/bundles/bootstrap")
    @Scripts.Render("~/bundles/modernizr")
    @RenderSection("scripts", False)


    <div class="container body-content">
        @RenderBody()
    </div>

    


    
    
    <script>
        function makeDraggable() {
//            $(".selectorField").draggable({ helper: "clone", stack: "div", cursor: "move", cancel: null });
            $(".PlacedControl").draggable({ helper: "clone", stack: "div", cursor: "move", cancel: null });
  //          $(".droppedField").draggable({ helper: "clone", stack: "div", cursor: "move", cancel: null });
        }

        var _ctrl_index = 1001;

        $(document).ready(docReady);

        function docReady() {
            console.log("document ready");

            compileTemplates();
            makeDraggable();

            $(".droppedFields").droppable({
                activeClass: "activeDroppable",
                hoverClass: "hoverDroppable",
                accept: ":not(.ui-sortable-helper)",
                drop: function (event, ui) {
                    //console.log(event, ui);
                    var draggable = ui.draggable;
                    draggable = draggable.clone();
                    draggable.removeClass("selectorField");
                    draggable.addClass("droppedField");
                    draggable[0].id = "CTRL-DIV-" + (_ctrl_index++); // Attach an ID to the rendered control
                    draggable.appendTo(this);
                    makeDraggable();
                }
            });

            /* Make the droppedFields sortable and connected with other droppedFields containers*/
            $(".droppedFields").sortable({
                //cancel: null, // Cancel the default events on the controls
                connectWith: ".droppedFields"
            });

            $(".selectorField").click(function () {

                //var draggable = $(this).html().clone();
                var controlTypeContent = $(this).clone();
                console.log('id - ' + $(this).attr('id'));
                controlTypeContent.removeClass("selectorField");
                controlTypeContent.addClass("droppedField");
                controlTypeContent[0].id = "CTRL-DIV-" + (_ctrl_index++); // Attach an ID to the rendered control

                $(controlTypeContent).hover(ShowConfigurationOptions);
                $(controlTypeContent).blur(ShowConfigurationOptions);
                $(controlTypeContent).find("[id$=Button]").click(ShowConfigurationPanel);
                $(controlTypeContent).appendTo($("#selected-action-column"));
                makeDraggable();
            });

            $(".PlacedControl").each(function (index) {
                var controlTypeContent = $(this).clone();
                console.log('id - ' + $(this).attr('id'));
                controlTypeContent.removeClass("PlacedControl");
                controlTypeContent.addClass("droppedField");
                controlTypeContent[0].id = "CTRL-DIV-" + (_ctrl_index++); // Attach an ID to the rendered control

                //$(controlTypeContent).hover(ShowConfigurationOptions);
                //$(controlTypeContent).blur(ShowConfigurationOptions);
                //$(controlTypeContent).find("[id$=Button]").click(ShowConfigurationPanel);
                //$(controlTypeContent).appendTo($("#selected-action-column"));
                makeDraggable();
            });

        }


        function ShowConfigurationOptions() {

            $(this).find('#ConfigurationButtons').toggle();
            $(this).find('#RequiredSelection').toggle();
            
            if (!$(this).find("#ConfigurationButtons").is(":visible")) {
                $(this).find("#Required").is(":checked") ? $(this).find("#RequiredText").show() : $(this).find("#RequiredText").hide();
                $(this).find("#RequiredCheckbox").hide();
            }
            else {
                $(this).find("#RequiredText").hide();
                $(this).find("#RequiredCheckbox").show();
            }

        }

        function ShowConfigurationPanel() {
            var configurationPanel = $(this).parent().siblings().find("#ConfigurationPanel");

            $(configurationPanel).tabs();
            var index = $(configurationPanel).find('a[href="#' + this.id.replace("Button", "Tab") + '"]').parent().index();
            $(configurationPanel).tabs('option', 'active', index);
            $(configurationPanel).show();

        }

        function CloseConfigurationPanel(item) {
            console.log($(item).parent().parent().parent().closest('#RequiredCheckbox').attr('id'));
            $(item).parent().parent().hide();
            UpdateControl.apply($(item).parent().parent().parent().parent().parent());
        }

        function UpdateControl() {
            var controlSettings = {};
            var currentID;
            console.log($(this).attr('id'));
            $(this).find("input:checkbox").each(function () {
                controlSettings[$(this).attr('id')] = this.checked == true ? "1" : "0";
            });
            
            $(this).find("input:text").each(function () {
                controlSettings[$(this).attr('id')] = $(this).val();
            });

            console.log(controlSettings);

            $.ajax({
                url: 'Home/updateControl',
                data: "controlSettings=" + JSON.stringify(controlSettings),
                success: function () {
                    console.log('Added');
                }
            });
        }


        /* Compile the templates for use */
        function compileTemplates() {
            window.templates = {};
            window.templates.common = Handlebars.compile($("#control-customize-template").html());

            /* HTML Templates required for specific implementations mentioned below */

            // Mostly we donot need so many templates

            window.templates.textbox = Handlebars.compile($("#textbox-template").html());
            window.templates.firstnametextbox = Handlebars.compile($("#firstname-textbox-template").html());
            window.templates.lastnametextbox = Handlebars.compile($("#firstname-textbox-template").html());
            window.templates.passwordbox = Handlebars.compile($("#textbox-template").html());
            window.templates.combobox = Handlebars.compile($("#combobox-template").html());
            window.templates.selectmultiplelist = Handlebars.compile($("#combobox-template").html());
            window.templates.radiogroup = Handlebars.compile($("#combobox-template").html());
            window.templates.checkboxgroup = Handlebars.compile($("#combobox-template").html());

        }

    </script>

    <!-- using handlebars for templating, but DustJS might be better for the current purpose -->
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/handlebars.js/1.0.0-rc.3/handlebars.min.js"></script>

    <!--
        Starting templates declaration
        DEV-NOTE: Keeping the templates and code simple here for demo  -- use some better template inheritance for multiple controls
    --->
    
    <script id="textbox-template" type="text/x-handlebars-template">
        <p><label class="control-label">Placeholder</label> <input type="text" name="placeholder" value=""></input></p>
    </script>

    <script id="firstname-textbox-template" type="text/x-handlebars-template">
        <p><label class="control-label">Placeholder</label> <input type="text" name="placeholder" value=""></input></p>
    </script>

    <script id="lastname-textbox-template" type="text/x-handlebars-template">
        <p><label class="control-label">Placeholder</label> <input type="text" name="placeholder" value=""></input></p>
    </script>


    <script id="combobox-template" type="text/x-handlebars-template">
        <p><label class="control-label">Options</label> <textarea name="options" rows="5"></textarea></p>
    </script>



    <script id="entry-template" type="text/x-handlebars-template">
        <div class="entry">
            <h1>{{title}}</h1>
            <div class="body">
                {{body}}
            </div>
            {{other}}
        </div>
    </script>
    
    <script type="text/javascript">
        $('#FormTitle').blur(ToggleFormTitle);

        function ToggleFormTitle() {
            if ($('#FormTitle').is(":visible")) {
                $('#FormTitleText').html($('#FormTitle').val());
                $('#FormTitleText').show();
                $('#FormTitle').hide();

            }
            else {
                $('#FormTitle').val($('#FormTitleText').html());
                $('#FormTitle').show();
                $('#FormTitleText').hide();


            }


            $('#FormTitle').focus();
        }
    </script>

    <script type="text/javascript">
        //$("#content").sortable();
        //$("#content").disableSelection();
        //$('.PlacedControl').draggable();
        //$('.PlacedControl').sortable();
        makeDraggable();

    </script>

</body>
</html>
