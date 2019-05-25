@Code
    Layout = Nothing
End Code
<!-- using handlebars for templating, but DustJS might be better for the current purpose -->
<script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/handlebars.js/1.0.0-rc.3/handlebars.min.js"></script>

<script id="textbox-template" type="text/x-handlebars-template">
    <p><label class="control-label">Placeholder</label> <input type="text" name="placeholder" value=""></p>
</script>

<script id="firstname-textbox-template" type="text/x-handlebars-template">
    <p><label class="control-label">Placeholder</label> <input type="text" name="placeholder" value=""></p>
</script>

<script id="lastname-textbox-template" type="text/x-handlebars-template">
    <p><label class="control-label">Placeholder</label> <input type="text" name="placeholder" value=""></p>
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
