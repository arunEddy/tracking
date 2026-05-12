var modal;

const API = AppConfig.apiBaseUrl + "/api/DRS";

$(document).ready(function () {

    modal = new bootstrap.Modal(document.getElementById('drsModal'));

    loadData();

    $("#btnAdd").click(function () {
        clearForm();
        modal.show();
    });

    $("#btnSave").click(saveData);

    $(document).on("click", ".btn-edit", function () {
        edit($(this).data("id"));
    });

    $(document).on("click", ".btn-delete", function () {
        deleteRec($(this).data("id"));
    });

    $("#btnPrintDRS").click(function () {
        printPage("DRS PRINT");
    });

    $("#btnPrintSummary").click(function () {
        printPage("SUMMARY PRINT");
    });

    $("#btnPrintDetail").click(function () {
        printPage("DETAIL PRINT");
    });

    $("#btnPrintDeliveryRunSheet").click(function () {
        printPage("DELIVERY RUN SHEET");
    });

});

function showLoader() {
    $("#loader").show();
}

function hideLoader() {
    $("#loader").hide();
}

function loadData() {

    showLoader();

    $.ajax({
        url: API,
        type: "GET",
        success: function (res) {

            let data = res?.data || res;

            let html = "";

            data.forEach((x, i) => {

                html += `
                    <tr>
                        <td>${i + 1}</td>
                        <td>${x.drsNo || ''}</td>
                        <td>${x.deliveryBoy || ''}</td>
                        <td>${x.mobileNo || ''}</td>
                        <td>${x.deliveryOffice || ''}</td>
                        <td class="text-center">

                            <button class="btn btn-warning btn-sm btn-edit" data-id="${x.drsid}">
                                <i class="bi bi-pencil-square"></i>
                            </button>

                            <button class="btn btn-danger btn-sm btn-delete" data-id="${x.drsid}">
                                <i class="bi bi-trash"></i>
                            </button>

                        </td>
                    </tr>
                `;

            });

            $("#tblData").html(html);

        },
        complete: function () {
            hideLoader();
        }
    });

}

function validate() {

    if (!$("#DeliveryOffice").val()) {
        Swal.fire("Validation", "Delivery Office Required", "warning");
        return false;
    }

    if (!$("#DeliveryDate").val()) {
        Swal.fire("Validation", "Delivery Date Required", "warning");
        return false;
    }

    if (!$("#AwbNo").val()) {
        Swal.fire("Validation", "AWB No Required", "warning");
        return false;
    }

    return true;
}

function saveData() {

    if (!validate()) return;

    showLoader();

    let id = parseInt($("#drsid").val()) || 0;

    let obj = {

        drsid: id,

        DRSMadeFor: $("#DRSMadeFor").val(),
        DeliveryOffice: $("#DeliveryOffice").val(),
        ConnectionNumber: $("#ConnectionNumber").val(),
        DeliveryBoy: $("#DeliveryBoy").val(),

        VehicleNumber: $("#VehicleNumber").val()
            ? new Date($("#VehicleNumber").val()).toISOString()
            : null,

        DeliveryDate: $("#DeliveryDate").val(),
        DRSNo: $("#DRSNo").val(),

        AllowMobileApplication: $("#AllowMobileApplication").is(":checked") ? "Y" : "N",

        MobileNo: $("#MobileNo").val(),
        SimNo: $("#SimNo").val(),

        AwbNo: $("#AwbNo").val(),
        ConsigneeName: $("#ConsigneeName").val(),

        IsActive: "Y"
    };

    $.ajax({

        url: id > 0 ? API + "/" + id : API,
        type: id > 0 ? "PUT" : "POST",
        contentType: "application/json",
        data: JSON.stringify(obj),

        success: function () {

            Swal.fire({
                icon: "success",
                title: id > 0 ? "Updated Successfully" : "Saved Successfully",
                timer: 1500,
                showConfirmButton: false
            });

            modal.hide();
            loadData();

        },

        error: function (xhr) {

            console.log(xhr.responseText);

            Swal.fire({
                icon: "error",
                title: "Error",
                text: "Something went wrong"
            });

        },

        complete: function () {
            hideLoader();
        }

    });

}

function edit(id) {

    showLoader();

    $.ajax({

        url: API + "/" + id,
        type: "GET",

        success: function (res) {

            let x = res?.data || res;

            $("#drsid").val(x.drsid);

            $("#DRSMadeFor").val(x.drsMadeFor);
            $("#DeliveryOffice").val(x.deliveryOffice);
            $("#ConnectionNumber").val(x.connectionNumber);
            $("#DeliveryBoy").val(x.deliveryBoy);

            if (x.vehicleNumber) {
                $("#VehicleNumber").val(
                    new Date(x.vehicleNumber).toISOString().slice(0, 16)
                );
            }

            $("#DeliveryDate").val(formatDate(x.deliveryDate));

            $("#DRSNo").val(x.drsNo);

            $("#AllowMobileApplication").prop(
                "checked",
                x.allowMobileApplication === "Y"
            );

            $("#MobileNo").val(x.mobileNo);
            $("#SimNo").val(x.simNo);

            $("#AwbNo").val(x.awbNo);
            $("#ConsigneeName").val(x.consigneeName);

            $("#PrintDRSNo").val(x.drsNo);

            modal.show();

        },

        complete: function () {
            hideLoader();
        }

    });

}

function deleteRec(id) {

    Swal.fire({
        title: "Delete Record?",
        icon: "warning",
        showCancelButton: true,
        confirmButtonText: "Yes Delete"
    }).then((res) => {

        if (res.isConfirmed) {

            showLoader();

            $.ajax({

                url: API + "/" + id,
                type: "DELETE",

                success: function () {

                    Swal.fire({
                        icon: "success",
                        title: "Deleted Successfully",
                        timer: 1500,
                        showConfirmButton: false
                    });

                    loadData();

                },

                complete: function () {
                    hideLoader();
                }

            });

        }

    });

}

function clearForm() {

    $("#drsid").val("");

    $("input").val("");
    $("select").prop("selectedIndex", 0);

    $("#AllowMobileApplication").prop("checked", false);

    $("#DeliveryOffice").val("HO - HEAD OFFICE");

}

function formatDate(date) {

    if (!date) return "";

    return new Date(date).toISOString().split('T')[0];

}

function printPage(title) {

    let content = `
        <html>
        <head>
            <title>${title}</title>
        </head>
        <body style="font-family:Verdana;padding:20px;">

            <h2>${title}</h2>

            <table border="1" cellspacing="0" cellpadding="8" width="100%">

                <tr>
                    <td><b>DRS No</b></td>
                    <td>${$("#DRSNo").val()}</td>
                </tr>

                <tr>
                    <td><b>Delivery Boy</b></td>
                    <td>${$("#DeliveryBoy").val()}</td>
                </tr>

                <tr>
                    <td><b>Mobile No</b></td>
                    <td>${$("#MobileNo").val()}</td>
                </tr>

                <tr>
                    <td><b>AWB No</b></td>
                    <td>${$("#AwbNo").val()}</td>
                </tr>

                <tr>
                    <td><b>Consignee Name</b></td>
                    <td>${$("#ConsigneeName").val()}</td>
                </tr>

            </table>

        </body>
        </html>
    `;

    let win = window.open('', '', 'width=900,height=700');

    win.document.write(content);

    win.document.close();

    win.print();

}