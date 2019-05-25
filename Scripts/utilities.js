function SetControlType(control) {
    control.ControlType1 = controlTypes.filter(ct => ct.ID == control.ControlType)[0];
    console.log(control.ControlType);
    console.log(controlTypes);
    console.log('set control type');
    console.log(control.ControlType1);
}

function SetControlDataType(control) {
    if (!control.ControlType1) {
        console.log("No control type present for Control #" + control.ID + ' - ' + control.Name);
    }
    else if (!control.ControlType1.DataType) {
        console.log("No data type present for Control #" + control.ID + ' - ' + control.Name);
    }

    if (control.ControlType1) {
        control.ControlType1.ControlDataType = controlDataTypes.filter(cdt => cdt.ID == control.ControlType1.DataType)[0];
    }
    else {
        control.ControlType1 = -1;
    }

}

function CloseInsertControl() {
    $('#InsertControl').hide();
}

function showInsert() {
    $('#InsertControl').show();
}

function setInsertPosition(position) {
    $('#InsertPosition').val(position);
}


function CloseDeleteControl() {
    document.getElementById('DeleteControl').style.display = 'none';
}

function ConvertFormValues(form) {
    $(form).find('input[type="checkbox"]').map((index, checkbox) => {
        if ($(checkbox).val() == "on") {
            $(checkbox).val('1');
        }
    });

}

function MakeDraggable() {
    let placedControls = this;
    return;
    $('.sortable').droppable();

    $('.sortable').sortable({
        stop: function (event, ui) {
            let positionedControls = $('#PlacedControls li.ui-sortable-handle');
            let positionUpdates = [];
            let newParentID = $(this).closest('ul.sortable')[0].id;
            let currentParentID = $(ui.item).find('input[name="ParentControlID"]')[0].value;
            let controlID = $(ui.item).find('input[name="ID"]')[0].value;

            for (var currentPosition = 0; currentPosition < positionedControls.length; currentPosition++) {
                try {
                    let position = $(positionedControls[currentPosition]).find('[name="Position"]')[0].value;

                    if (position != currentPosition) {
                        let id = $(positionedControls[currentPosition]).find('[name="ID"]')[0].value;

                        positionUpdates.push({ ID: id, Position: currentPosition });
                    }
                } catch (e) {

                }
            }

            if (positionUpdates.length > 0) {
                $.ajax({
                    url: updatePositionURL,
                    type: "POST",
                    data: { positionUpdates: positionUpdates },
                    success: function (info) {
                    }
                });
            }

            $.ajax({
                url: updateParentURL,
                type: "POST",
                data: { controlID: controlID, parentControlID: newParentID },
                success: function (info) {
                    console.log(info);
                    console.log('set control #' + controlID + ' parent control ID to ' + newParentID);
                }
            });
        }
    });

    $('#PlacedControls li').draggable({
        axis: "y",
        snap: true,
        connectToSortable: ".sortable",
        obstacle: ".butNotHere",
        preventCollision: true,
        containment: "#PlacedControls",
        start: function (event, ui) {
            $(this).removeClass('butNotHere');
        },
        stop: function (event, ui) {
            $(this).addClass('butNotHere');
        }
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

function DeleteControl() {
    console.log($('#DeleteControlID').val());
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



function ToggleEditable(target) {
    var editor = $('#' + target.id.replace('Text', ''));
    var label = $('#' + target.id.replace('Text', '') + 'Text');

    if ($(editor).is(":visible")) {
        $(label).html($(editor).val());
        $(label).show();
        $(editor).hide();

    }
    else {
        $(editor).val($(label).html());
        $(editor).show();
        $(label).hide();
    }


    $(editor).focus();
}