const API = AppConfig.apiBaseUrl + "/api/ReturnToCustomerManifesting";

let modal;

$(document).ready(function () {

    modal = new bootstrap.Modal(document.getElementById('manifestModal'));

    loadData();

    $("#btnAdd").click(function () {

        resetForm();

        modal.show();

    });

    $("#btnSave").click(function () {

        saveData();

    });

    $("#btnPrint").click(function () {

        printManifest();

    });

    $("#btnPrintLaser").click(function () {

        printManifest();

    });

});

function loadData() {

    $.ajax({

        url: API,
        type: "GET",

        success: function (res) {

            let data = res?.data || res;

            let html = "";

            $.each(data, function (i, item) {

                html += `
                    <tr>

                        <td>${i + 1}</td>

                        <td>${item.shipperName || ''}</td>

                        <td>${item.deliveryBoy || ''}</td>

                        <td>${formatDate(item.returnDate)}</td>

                        <td>${item.bagNumber || ''}</td>

                        <td>${item.remarks || ''}</td>

                        <td>${item.awbNumber || ''}</td>

                        <td>${item.reason || ''}</td>

                       
                        <td>

                            <button class="btn btn-primary btn-sm"
                                    onclick="edit(${item.rtcmid})">
                                <i class="bi bi-pencil"></i>
                            </button>

                            <button class="btn btn-danger btn-sm"
                                    onclick="deleteRec(${item.rtcmid})">
                                <i class="bi bi-trash"></i>
                            </button>

                        </td>

                    </tr>
                `;

            });

            $("#tblData").html(html);

        },

        error: function () {

            Swal.fire(
                'Error',
                'Failed to load data',
                'error'
            );

        }

    });

}

function saveData() {

    if ($("#ShipperName").val() == "") {

        Swal.fire(
            'Validation',
            'Shipper Name required',
            'warning'
        );

        return;

    }

    if ($("#DeliveryBoy").val() == "") {

        Swal.fire(
            'Validation',
            'Delivery Boy required',
            'warning'
        );

        return;

    }

    if ($("#ReturnDate").val() == "") {

        Swal.fire(
            'Validation',
            'Return Date required',
            'warning'
        );

        return;

    }

    if ($("#AWBNumber").val() == "") {

        Swal.fire(
            'Validation',
            'AWB Number required',
            'warning'
        );

        return;

    }

    if ($("#Reason").val() == "") {

        Swal.fire(
            'Validation',
            'Reason required',
            'warning'
        );

        return;

    }

    let obj = {

        rtcmid: $("#rtcmid").val() || 0,

        shipperName: $("#ShipperName").val(),
        deliveryBoy: $("#DeliveryBoy").val(),
        returnDate: $("#ReturnDate").val(),

        bagNumber: $("#BagNumber").val(),
        remarks: $("#Remarks").val(),

        awbNumber: $("#AWBNumber").val(),
        reason: $("#Reason").val(),

        manifestNo: $("#ManifestNo").val(),

        createdby: "ADMIN",
        isActive: "Y"

    };

    let id = $("#rtcmid").val();

    $.ajax({

        url: id ? `${API}/${id}` : API,

        type: id ? "PUT" : "POST",

        contentType: "application/json",

        data: JSON.stringify(obj),

        success: function (res) {

            Swal.fire({
                icon: 'success',
                title: id ? 'Updated Successfully' : 'Saved Successfully',
                timer: 2000,
                showConfirmButton: false
            });


            modal.hide();

            loadData();

            resetForm();

        },

        error: function () {

            Swal.fire(
                'Error',
                'Save failed',
                'error'
            );

        }

    });

}

function edit(id) {

    $.ajax({

        url: `${API}/${id}`,
        type: "GET",

        success: function (res) {

            let d = res?.data || res;

            $("#rtcmid").val(d.rtcmid);

            $("#ShipperName").val(d.shipperName);
            $("#DeliveryBoy").val(d.deliveryBoy);

            $("#ReturnDate").val(
                d.returnDate
                    ? new Date(d.returnDate).toISOString().split('T')[0]
                    : ''
            );

            $("#BagNumber").val(d.bagNumber);
            $("#Remarks").val(d.remarks);

            $("#AWBNumber").val(d.awbNumber);
            $("#Reason").val(d.reason);

            $("#ManifestNo").val(d.manifestNo);

            modal.show();

        }

    });

}

function deleteRec(id) {

    Swal.fire({

        title: 'Delete?',
        text: 'Are you sure?',
        icon: 'warning',
        showCancelButton: true

    }).then((result) => {

        if (result.isConfirmed) {

            $.ajax({

                url: `${API}/${id}`,
                type: "DELETE",

                success: function () {

                    Swal.fire(
                        'Deleted',
                        'Record deleted successfully',
                        'success'
                    );

                    loadData();

                },

                error: function () {

                    Swal.fire(
                        'Error',
                        'Delete failed',
                        'error'
                    );

                }

            });

        }

    });

}

function resetForm() {

    $("#rtcmid").val('');

    $("#ShipperName").val('');
    $("#DeliveryBoy").val('');
    $("#ReturnDate").val('');

    $("#BagNumber").val('');
    $("#Remarks").val('');

    $("#AWBNumber").val('');
    $("#Reason").val('');

    $("#ManifestNo").val('');

}

function formatDate(date) {

    if (!date)
        return '';

    return new Date(date).toLocaleDateString();

}

function printManifest() {

    let manifestNo = $("#ManifestNo").val();

    if (manifestNo == "") {

        Swal.fire(
            'Validation',
            'Enter Manifest No',
            'warning'
        );

        return;

    }

    window.open(
        `/Report/Manifest?manifestNo=${manifestNo}`,
        '_blank'
    );

}