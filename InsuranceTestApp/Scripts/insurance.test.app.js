$(document).ready(function() {

    // Create a jtable for insurance policies (http://jtable.org/)
    $('#policy-table-container').jtable({
        title: 'Policies',
        paging: true, // Enable paging
        pageSize: 10, // Set page size (default: 10)
        sorting: true, // Enable sorting
        defaultSorting: 'PolicyNumber ASC', //Set default sorting
        actions: {
            listAction: '/Home/PolicyList',
            //createAction: '/Home/CreatePolicy'
            //deleteAction: '',
            //updateAction: '',
        },
        toolbar: {
            items: [
                {
                    //icon: '',
                    text: 'Create New Policy',
                    click: function() {
                        $('#create-policy-dialog').dialog('open');
                    }
                }
            ]
        },
        fields: {
            Id: {
                key: true,
                create: false,
                edit: false,
                list: false
            },
            "PrimaryInsured.Id": {
                create: false,
                edit: false,
                list: false
            },
            "Risk.Id": {
                create: false,
                edit: false,
                list: false
            },
            PolicyNumber: {
                title: 'Policy Number',
                width: '20%'
            },
            "PrimaryInsured.Name": {
                title: 'Primary Insured Name',
                width: '20%',
                display: function(data) {
                    return '<p>' + data.record.PrimaryInsured.Name + '</p>';
                }
            },
            EffectiveDate: {
                title: 'Effective Date',
                width: '20%',
                type: 'date',
                displayFormat: "mm-dd-yy",
            },
            ExpirationDate: {
                title: 'Expiration Date',
                width: '20%',
                type: 'date',
                displayFormat: "mm-dd-yy",
            },
            "PrimaryInsured.MailingAddress": {
                title: 'Primary Insured Mailing Address',
                list: false
            },
            "PrimaryInsured.City": {
                title: 'Primary Insured City',
                list: false
            },
            "PrimaryInsured.State": {
                title: 'Primary Insured State',
                list: false
            },
            "PrimaryInsured.ZipCode": {
                title: 'Primary Insured Zip Code',
                list: false
            },
            "Risk.Construction": {
                title: 'Risk Construction Type',
                options: '/Home/GetRiskConstructionTypes',
                list: false
            },
            "Risk.YearBuilt": {
                title: 'Risk Year Built',
                list: false,
                type: 'date',
                displayFormat: "mm-dd-yy",
            },
            "Risk.Address": {
                title: 'Risk Address',
                list: false
            },
            "Risk.City": {
                title: 'Risk City',
                list: false
            },
            "Risk.State": {
                title: 'Risk State',
                list: false
            },
            "Risk.ZipCode": {
                title: 'Risk Zip Code',
                list: false
            }
        }
    });

    // Populate the policy list with data from the server.
    $('#policy-table-container').jtable('load');

    // Create the policy dialog
    $("#create-policy-dialog").dialog({
        autoOpen: false,
        dialogClass: 'custom-dialog-titlebar',
        position: { my: "top", at: "top+60", of: window },
        width: 550,
        resizable: false,
        title: 'Create New Policy',
        modal: true,
        open: function() {

            // Load the partial view for creating policies.
            $(this).load('Home/CreatePolicyPartialView',
                function() {

                    // After loading the dialog contents, re-position the dialog, since the size has now changed.
                    $('#create-policy-dialog').dialog('widget').position({ my: "top", at: "top+60", of: window });

                    // Update the style of the titlebar close (X) button.
                    var widget = $(this).dialog("widget");
                    $(".ui-dialog-titlebar-close span", widget).addClass("ui-icon-custom-close-button");

                    // Subscribe to the form submit event.
                    $('#create-policy-form').submit(function (event) {
                        // Cancel the form's submit action.
                        event.preventDefault();

                        // Send the ajax request with proper error handling.
                        if ($(this).valid()) {
                            sendCreatePolicyRequest();
                        }
                    });

                    // Handle date pickers
                    $(".date-picker").datepicker();

                    // Handle form validation
                    $("#create-policy-form").removeData("validator");
                    $("#create-policy-form").removeData("unobtrusiveValidation");
                    $.validator.unobtrusive.parse("#create-policy-form");
                });

        },
        buttons: [
            {
                text: "Cancel",
                "class": 'custom-dialog-button',
                style: "color: greenyellow; background: seagreen;",
                click: function() {
                    $(this).dialog("close");
                }
            },
            {
                text: "Create Policy",
                "class": 'custom-dialog-button',
                style: "color: greenyellow; background: seagreen;",
                click: function () {

                    // Submit the form to perform client-side validation.
                    $('#create-policy-form').submit();
                }
            }
        ],
        close: function () {
            $(this).dialog("close");
        }
    });

    // Sends Create Policy request to the server
    function sendCreatePolicyRequest() {
        $.ajax({
            url: 'Home/CreatePolicy',
            type: 'POST',
            data: $("#create-policy-form").serialize(),
            success: function (data) {
                if (data.Result === "OK") {
                    $('#policy-table-container').jtable('reload');
                    $("#create-policy-dialog").dialog("close");
                } else {
                    $("#errorMessage").text(data.Message);
                }
            }
        });
    }
});